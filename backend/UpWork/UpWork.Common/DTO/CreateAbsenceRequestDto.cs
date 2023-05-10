namespace UpWork.Common.DTO
{
    public class CreateAbsenceRequestDto
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsActive { get; set; }
        public Guid AbsenceTypeId { get; set; }
    }

}
