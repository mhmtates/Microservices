using FreeCourse.Services.FakePayment.Models;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Messages;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FreeCourse.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : CustomBaseController
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public FakePaymentsController(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost]
        public async Task<IActionResult> ReceivePayment(PaymentDto paymentDto)
        {
            //paymentDto ile ödeme işlemi gerçekleştir.

            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-order-service"));

            var createorderMessageCommand = new CreateOrderMessageCommand();
            createorderMessageCommand.BuyerId = paymentDto.Order.BuyerId;
            createorderMessageCommand.Province = paymentDto.Order.Address.Province;
            createorderMessageCommand.District = paymentDto.Order.Address.District;
            createorderMessageCommand.Street = paymentDto.Order.Address.Street;
            createorderMessageCommand.Line =  paymentDto.Order.Address.Line;
            createorderMessageCommand.ZipCode = paymentDto.Order.Address.ZipCode;

            paymentDto.Order.OrderItems.ForEach(y =>
            {
                createorderMessageCommand.OrderItems.Add(new OrderItem
                {
                    PictureUrl = y.PictureUrl,
                    Price  = y.Price,
                    ProductId = y.ProductId,
                    ProductName = y.ProductName
                });
            });

            await sendEndpoint.Send<CreateOrderMessageCommand>(createorderMessageCommand);
            return CreateActionResultInstance<NoContent>(Shared.Dtos.Response<NoContent>.Success(200));
        }


    }
}
