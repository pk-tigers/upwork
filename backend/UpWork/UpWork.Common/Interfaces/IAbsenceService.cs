using UpWork.Common.Dto;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Common.Interfaces
{
    public interface IAbsenceService
    {
        AbsenceModel SetAbsenceApprovalState(AbsenceApprovalStateDto absenceApprovalState);
    }
}
