namespace ECommerceManager.Dtos.RoleDtos
{
    public class UserWithAllRolesDto
    {
        public List<RoleDto> AllRoles { get; set; }
        public List<UserRoleDto> Users { get; set; }
        public RoleUpdateDto RoleUpdateDto { get; set; }
    }
}
