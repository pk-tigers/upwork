namespace UpWork.Common.Dto
{
    public class UpdateAbsenceDto
    {
        public Guid Id { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Guid? TimeOffSupervisorId { get; set; }
    }
}
