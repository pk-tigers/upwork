using UpWork.Common.Dto;
using UpWork.Common.DTO;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Common.Interfaces
{
    public interface IAbsenceService
    {
        AbsenceModel SetAbsenceApprovalState(AbsenceApprovalStateDto absenceApprovalState, Guid supervisorId);
        bool CancelRequestForUser(Guid absenceId, Guid userId);
        AbsenceModel CreateAbsenceRequestForUser(Guid userId, CreateAbsenceRequestDto requestDto);
        AbsenceModelDto UpdateAbsence(Guid userId, UpdateAbsenceDto updateAbsenceDto);
    }
}
