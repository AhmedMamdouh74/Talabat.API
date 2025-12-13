using Application.DTOs.Order;
using Application.Services.Contract;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Entities.Order_Aggregate;
using Domain.Exceptions;
using Domain.Specifications.Orders;

namespace Application.Services
{
    internal class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;

        public OrderService(IUnitOfWork _unitOfWork, IBasketRepository _basketRepository, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            basketRepository = _basketRepository;
            mapper = _mapper;
        }
        public async Task<ReadOrderDto> CreateOrderAsync(CreateOrderDto orderDto)
        {
            // get basket from basket repo
            var basket = await basketRepository.GetBasketAsync(orderDto.BasketId);
            // get selected items at basket from product repo
            var productRepo = unitOfWork.GetRepository<Product>();
            var products = await productRepo.GetAllAsync();
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = products.FirstOrDefault(p => p.Id == item.Id);
                var itemOrdered = new ProductOrderedItem(product.Id, product.Name, product.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, product.Price, item.Quantity);
                orderItems.Add(orderItem);
            }
            // calc subtotal
            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);

            // get delivery method from deliveryMethod repo
            var deliveryMethodRepo = unitOfWork.GetRepository<DeliveryMethod>();
            var deliveryMethod = await deliveryMethodRepo.GetByIdAsync(orderDto.DeliveryMethodId);
            // create order
            var address = mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);
            var order = new Order(orderDto.BuyerEmail, address, deliveryMethod, orderItems, subtotal);

            // save to db
            var orderRepo = unitOfWork.GetRepository<Order>();
            await orderRepo.AddAsync(order);
            await unitOfWork.CompleteAsync();
            // return order


            return mapper.Map<Order, ReadOrderDto>(order);
        }

        public async Task<ReadOrderDto> GetOrderByIdAsync(int orderId, string buyerEmail)
        {
            var orderRepo = unitOfWork.GetRepository<Order>();
            var orderSpec = new OrderWithIdAndBuyerEmailSpec(orderId, buyerEmail);
            var order = await orderRepo.GetByIdWithSpecAsync(orderSpec);
            if (order == null)
                throw new NotFoundException("order not found");
            return mapper.Map<ReadOrderDto>(order);

        }

        public async Task<IReadOnlyList<ReadOrderDto>> GetOrdersForUserAsync(string buyerEmail)
        {
            var orderRepo = unitOfWork.GetRepository<Order>();
            var orderSpec = new OrderWithBuyerEmailSpec(buyerEmail);
            var orders = await orderRepo.GetAllWithSpecAsync(orderSpec);
            if (orders == null || !orders.Any())
                throw new NotFoundException("orders not found");
            return mapper.Map<List<ReadOrderDto>>(orders);
        }
    }
}
