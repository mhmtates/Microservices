//using FreeCourse.Services.Basket.Dtos;
//using FreeCourse.Services.Basket.Services;
//using MassTransit;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace FreeCourse.Services.Basket.Consumers
//{
//    public class CourseNameChangedEventConsumer : IConsumer<CourseNameChangedEventConsumer>
//    {
//        private readonly BasketService _basketService;
//        private readonly RedisService _redisService;
//        public CourseNameChangedEventConsumer(BasketService basketService,RedisService redisService)
//        {
//            _basketService = basketService;
//            _redisService = redisService;
//        }
//        public async Task Consume(ConsumeContext<CourseNameChangedEventConsumer> context)
//        {
//            var basketItems = await _redisService.GetDb().StringSetAsync(BasketDto)
//    }
//}
