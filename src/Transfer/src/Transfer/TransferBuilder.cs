using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Transfer.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Transfer
{
    public sealed class TransferBuilder : ITransferBuilder
    {
        private readonly ConcurrentDictionary<string, bool> _registry = new ConcurrentDictionary<string, bool>();
        private readonly List<Action<IServiceProvider>> _buildActions;
        private readonly IServiceCollection _services;
        IServiceCollection ITransferBuilder.Services => _services;

        private TransferBuilder(IServiceCollection services)
        {
            _buildActions = new List<Action<IServiceProvider>>();
            _services = services;
            _services.AddSingleton<IStartupInitializer>(new StartupInitializer());
        }

        public static ITransferBuilder Create(IServiceCollection services)
            => new TransferBuilder(services);

        public bool TryRegister(string name) => _registry.TryAdd(name, true);

        public void AddBuildAction(Action<IServiceProvider> execute)
            => _buildActions.Add(execute);

        public void AddInitializer(IInitializer initializer)
            => AddBuildAction(sp =>
            {
                var startupInitializer = sp.GetService<IStartupInitializer>();
                startupInitializer.AddInitializer(initializer);
            });

        public void AddInitializer<TInitializer>() where TInitializer : IInitializer
            => AddBuildAction(sp =>
            {
                var initializer = sp.GetService<TInitializer>();
                var startupInitializer = sp.GetService<IStartupInitializer>();
                startupInitializer.AddInitializer(initializer);
            });

        public IServiceProvider Build()
        {
            var serviceProvider = _services.BuildServiceProvider();
            _buildActions.ForEach(a => a(serviceProvider));
            return serviceProvider;
        }
    }
}