using System;
using System.Threading.Tasks;
using Transfer.CQRS.Queries;
using Transfer.Persistence.MongoDB;
using Transferor.Services.Orders.Domain;
using Transferor.Services.Orders.DTO;

namespace Transferor.Services.Orders.Queries.Handlers
{
    public class GetOrderHandler : IQueryHandler<GetOrder, OrderDto>
    {
        private readonly IMongoRepository<Order, Guid> _repository;

        public GetOrderHandler(IMongoRepository<Order, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<OrderDto> HandleAsync(GetOrder query)
        {
            var order = await _repository.GetAsync(query.OrderId);

            return order is null
                ? null
                : new OrderDto {Id = order.Id, CustomerId = order.CustomerId, TotalAmount = order.TotalAmount};
        }
    }
}
