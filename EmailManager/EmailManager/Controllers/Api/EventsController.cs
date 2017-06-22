using EmailManager.Data.UnitOfWork;
using EmailManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager.Controllers.Api
{
    [Route("api/events")]
    [Authorize]
    public class EventsController
    {
        private IUoW _unitOfWork;
        private ILogger<EventsController> _logger;

        public EventsController(IUoW unitOfWork, ILogger<EventsController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        //[Authorize]
        //public IActionResult Events()
        //{
        //    try
        //    {
        //        var enEventsRepo = _unitOfWork.Get<EnEvent>().Value;

        //        return View(enEventsRepo.GetAll());
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Failed to get trips in Index page: {ex.Message}");
        //        return Redirect("/error");
        //    }
        //}
    }
}
