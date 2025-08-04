using System.Drawing.Printing;

namespace ECommerceManager.Dtos.RoleDtos
{
    public class UserRoleDto
    {
      
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName  { get; set; }
        public string Roles { get; set; }
        public List<string> AllRoles { get; set; }  
        public string SelectedRole { get; set; }
    
    }
}
