namespace UpWork.Common.Identity
{
    public static class IdentityData
    {
        public const string AdminUserClaimName = "admin";
        public const string OwnerUserClaimName = "owner";
        public const string UserIdClaimName = "userId";
        public const string OrganizationIdClaimName = "organizationId";
        public const string PermissionsClaimName = "permissions";


        public const string AdminUserPolicy = "Admin";
        public const string CreateUserPolicy = "CreateUserPolicy";
        public const string MatchOrganizationIdQueryPolicy = "MatchOrganizationIdQueryPolicy";
        public const string MatchOrganizationIdBodyPolicy = "MatchOrganizationIdBodyPolicy";
    }
}
