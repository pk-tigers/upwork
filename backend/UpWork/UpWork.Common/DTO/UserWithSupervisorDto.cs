namespace UpWork.Common.Dto
{
    public class UserWithSupervisorDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SupervisorFirstName { get; set; }
        public string SupervisorLastName { get; set; }
    }
}
