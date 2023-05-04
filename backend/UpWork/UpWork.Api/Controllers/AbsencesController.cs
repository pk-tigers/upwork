﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UpWork.Common.Enums;
using UpWork.Common.Interfaces;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AbsencesController : ControllerBase
    {
        private readonly IAbsencesService _absencesService;

        public AbsencesController(IAbsencesService absencesService)
        {
            _absencesService = absencesService;
        }

        [HttpGet("GetAbsencesByOrganizationId")]
        public ActionResult<IEnumerable<AbsenceModel>> GetAbsencesByOrganizationId(Guid organizationId, DateTime from, DateTime to, int skip = 0, int take = 10)
        {
            var res = _absencesService.GetAbsencesByOrganizationId(organizationId, from, to, skip, take);
            return Ok(res);
        }

        [HttpGet("GetAbsencesByUserId")]
        public ActionResult<IEnumerable<AbsenceModel>> GetAbsencesByUserId(Guid userId, DateTime from, DateTime to, int skip = 0, int take = 10)
        {
            var res = _absencesService.GetAbsencesByUserId(userId, from, to, skip, take);
            return Ok(res);
        }

        [HttpGet("GetPendingAbsencesRequestsBySupervisorId")]
        public ActionResult<IEnumerable<AbsenceModel>> GetPendingAbsencesRequestsBySupervisorId(Guid supervisorId, int skip = 0, int take = 10)
        {
            var res = _absencesService.GetPendingAbsencesRequestsBySupervisorId(supervisorId, skip, take);
            return Ok(res);
        }

        [HttpGet("GetSupervisedAbsencesRequestsBySupervisorId")]
        public ActionResult<IEnumerable<AbsenceModel>> GetSupervisedAbsencesRequestsBySupervisorId(Guid supervisorId, int skip = 0, int take = 10)
        {
            var res = _absencesService.GetSupervisedAbsencesRequestsBySupervisorId(supervisorId, skip, take);
            return Ok(res);
        }
    }
}