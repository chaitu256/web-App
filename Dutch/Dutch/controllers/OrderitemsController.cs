using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dutch.Data;
using Dutch.ViewModels;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dutch.controllers
{
    [Route("/api/orders/{orderid}/items")]
    [Authorize(AuthenticationSchemes  = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderitemsController : Controller
    {

        private readonly IDutchRepository _repository;
        private readonly ILogger<OrderitemsController> _logger;
        private readonly IMapper _mapper;
        public OrderitemsController(IDutchRepository repository, ILogger<OrderitemsController> logger,IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;

        }

        [HttpGet]
        public IActionResult Get(int orderId)
        {
            var order = _repository.GetOrderById(User.Identity.Name, orderId);
            if (order != null) return Ok(_mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int orderId, int id)
        {

            var order = _repository.GetOrderById(User.Identity.Name, orderId);
            if (order != null)
            {
                var item = order.Items.Where(i => i.Id == id).FirstOrDefault();
                if (item != null)
                {

                    return Ok(_mapper.Map <OrderItem, OrderItemViewModel>(item));
                }
            }
            return NotFound();
        }

    }
}
