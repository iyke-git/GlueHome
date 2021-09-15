using System;
using System.Net;
using System.Net.Http;
using gluehome.delivery.repository.models;
using gluehome.delivery.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace gluehome.delivery.webapi.Controllers
{
    [ApiController]
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

        [HttpPost]
        public IActionResult Post(Delivery delivery)
        {
            return Ok(_deliverySvc.CreateDelivery(delivery.recipient.id, delivery.partner.id, delivery.order.orderNumber));
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