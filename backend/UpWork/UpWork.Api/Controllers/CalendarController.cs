using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UpWork.Common.Interfaces;
using UpWork.Common.Models.DatabaseModels;
using UpWork.Common.Models;
using UpWork.Api.Extensions;

namespace UpWork.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarService _callendarService;

        public CalendarController(ICalendarService callendarService)
        {
            _callendarService = callendarService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserAbsenceModel>> GetCalendarAbsencesForUser(DateTime from, DateTime to)
        {
            Guid userId = User.Identity.GetUserId();
            var res = _callendarService.GetCalendarAbsencesByUserId(userId, from, to);
                
            return Ok(res);
        }

    }
}
