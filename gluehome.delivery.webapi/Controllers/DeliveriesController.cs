using System;
using System.Net;
using System.Net.Http;
using gluehome.delivery.repository.models;
using gluehome.delivery.services;
using gluehome.delivery.webapi.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace gluehome.delivery.webapi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class DeliveriesController : ControllerBase
    {
        private readonly IDeliveryService _deliverySvc;
        private readonly ILogger<DeliveriesController> _logger;

        public DeliveriesController(ILogger<DeliveriesController> logger, IDeliveryService deliverySvc)
        {
            _logger = logger;
            _deliverySvc = deliverySvc;
        }

        [HttpGet]
        public IActionResult Get(){
            return Ok(_deliverySvc.GetAllDeliveries());
        }

        [HttpPost]
        public IActionResult Post(DeliveryCreation delivery)
        {
            return Ok(_deliverySvc.CreateDelivery(delivery.recipientId, delivery.partnerId, delivery.orderNumber));
        }

        [HttpPut]
        [Route("{deliveryId}/approve")]
        public IActionResult ApproveDelivery([FromRoute] Guid deliveryId, AccessWindow accessWindow)
        {
            return Ok(_deliverySvc.ApproveDelivery(deliveryId, accessWindow));
        }
        [HttpPut]
        [Route("{deliveryId}/complete")]
        public IActionResult CompleteDelivery([FromRoute] Guid deliveryId)
        {
            return Ok(_deliverySvc.CompleteDelivery(deliveryId));
        }
        [HttpPut]
        [Route("{deliveryId}/cancel")]
        public IActionResult CancelDelivery([FromRoute] Guid deliveryId)
        {
            return Ok(_deliverySvc.CancelDelivery(deliveryId));
        }
    }
}