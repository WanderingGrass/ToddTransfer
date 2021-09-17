﻿using Microsoft.Extensions.DependencyInjection;
using System;
using Transfer.Types;

namespace Transfer
{
    public  interface ITransferBuilder
    {
        IServiceCollection Services { get; }
        bool TryRegister(string name);
        void AddBuildAction(Action<IServiceProvider> execute);
        void AddInitializer(IInitializer initializer);
        void AddInitializer<TInitializer>() where TInitializer : IInitializer;
        IServiceProvider Build();

    }
}
