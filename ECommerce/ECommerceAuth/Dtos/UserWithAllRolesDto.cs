namespace ECommerceAuth.Dtos
{
    public class UserWithAllRolesDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Roles { get; set; }
        public List<string> AllRoles { get; set; }
    }
}
