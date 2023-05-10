using UpWork.Common.Dto;
using UpWork.Common.DTO;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Common.Interfaces
{
    public interface IAbsenceService
    {
        AbsenceModel SetAbsenceApprovalState(AbsenceApprovalStateDto absenceApprovalState);
        bool CancelRequestIfNotStarted(Guid id);
        AbsenceModel CreateAbsenceRequest(Guid userId, CreateAbsenceRequestDto requestDto);
    }
}
