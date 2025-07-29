using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace ECommerceManager.Managers
{
    public interface IUserManager
    {
        Task<string> GetUserEmailAsync();
        

        
    }
}
