using UpWork.Common.Dto;
using UpWork.Common.DTO;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Common.Interfaces
{
    public interface IAbsenceService
    {
<<<<<<< HEAD
        AbsenceModel SetAbsenceApprovalState(AbsenceApprovalStateDto absenceApprovalState);
        bool CancelRequestIfNotStarted(Guid id);
        AbsenceModel CreateAbsenceRequest(Guid userId, CreateAbsenceRequestDto requestDto);
=======
        AbsenceModel SetAbsenceApprovalState(AbsenceApprovalStateDto absenceApprovalState, Guid supervisorId);
        bool CancelRequestForUser(Guid requestId, Guid userId);
        AbsenceModel CreateAbsenceRequestForUser(Guid userId, CreateAbsenceRequestDto requestDto);
>>>>>>> dev
    }
}
