namespace UpWork.Common.Dto
{
    public class UpdateUserSupervisorDto
    {
        public Guid OrganizationId { get; set; }
        public Guid UserId { get; set; }
        public Guid NewSupervisorId { get; set; }
    }
}
