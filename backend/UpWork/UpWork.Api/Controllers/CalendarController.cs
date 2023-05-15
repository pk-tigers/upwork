using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UpWork.Common.Interfaces;
using UpWork.Common.Models.DatabaseModels;
using UpWork.Common.Models;
using UpWork.Api.Extensions;

namespace UpWork.Api.Controllers
{
    [Route("api/[controller]")]
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
        public ActionResult<PaginatedResult<AbsenceModel>> GetCalendarAbsencesForUser(DateTime? fromDate, DateTime? toDate, int skip = 0, int take = 10)
        {
            Guid userId = User.Identity.GetUserId();
            var res = _callendarService.GetCalendarAbsencesByUserId(userId, skip, take, fromDate, toDate);
            if (res.Count == 0)
            {
                return NotFound();
            }
                
            return Ok(res);
        }

    }
}
