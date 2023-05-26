namespace UpWork.Common.Dto
{
    public class RegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid? SupervisorId { get; set; }
        public Guid? OrganizationId { get; set; }
    }
}
