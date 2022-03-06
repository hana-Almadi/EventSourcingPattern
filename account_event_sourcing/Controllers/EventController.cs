using account_event_sourcing.Controllers.Dto;
using account_event_sourcing.Domain.Event;
using account_event_sourcing.Repository;
using account_event_sourcing.Service.EventHandler;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace account_event_sourcing.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly AccountEventService _accountEventService;

        public EventController(AccountEventService accountEventService)
        {
            _accountEventService=accountEventService;
        }
        [HttpGet("/reset/{accountAggergateId}")]
        public async Task<EventDto> EventReset(Guid accountAggergateId)
        {
           return await _accountEventService.EventReset(accountAggergateId);

        }
    }
}
