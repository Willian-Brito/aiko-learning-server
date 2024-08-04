using AikoLearning.Core.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace AikoLearning.Infrastructure.Data.Security;

public class SeedUserRoleInitial : ISeedUserRoleInitial 
{
    #region Properties
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    #endregion
    
    #region Constructor
    public SeedUserRoleInitial(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        this.roleManager = roleManager;
        this.userManager = userManager;
    }
    #endregion

    #region Methods
    public void SeedRoles()
    {
        #region User
        var hasUserRole = roleManager.RoleExistsAsync("User").Result;
        if(!hasUserRole)
        {
            var role = new IdentityRole();

            role.Name = "User";
            role.NormalizedName = "USER";

            var result = roleManager.CreateAsync(role).Result;
        }
        #endregion

        #region Admin
        var hasAdminRole = roleManager.RoleExistsAsync("Admin").Result;
        if(!hasAdminRole)
        {
            var role = new IdentityRole();

            role.Name = "Admin";
            role.NormalizedName = "ADMIN";

            var result = roleManager.CreateAsync(role).Result;
        }
        #endregion
    }

    public void SeedUsers()
    {
        #region User

        var hasUser = userManager.FindByEmailAsync("usuario@localhost").Result == null;
        
        if (hasUser)
        {
            var user = new ApplicationUser();
            
            user.UserName = "usuario@localhost";
            user.Email = "usuario@localhost";
            user.NormalizedUserName = "USUARIO@LOCALHOST";
            user.NormalizedEmail = "USUARIO@LOCALHOST";
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            user.SecurityStamp = Guid.NewGuid().ToString();

            var result = userManager.CreateAsync(user, "Numsey#2024").Result;

            if (result.Succeeded)
                userManager.AddToRoleAsync(user, "User").Wait();            
        }
        #endregion
   
        #region Admin
        
        var hasAdmin = userManager.FindByEmailAsync("admin@localhost").Result == null;

        if (hasAdmin)
        {
            var admin = new ApplicationUser();
            
            admin.UserName = "admin@localhost";
            admin.Email = "admin@localhost";
            admin.NormalizedUserName = "ADMIN@LOCALHOST";
            admin.NormalizedEmail = "ADMIN@LOCALHOST";
            admin.EmailConfirmed = true;
            admin.LockoutEnabled = false;
            admin.SecurityStamp = Guid.NewGuid().ToString();

            var result = userManager.CreateAsync(admin, "Numsey#2024").Result;

            if (result.Succeeded)
                userManager.AddToRoleAsync(admin, "Admin").Wait();            
        }
        #endregion
    }
    #endregion
    
}