using AikoLearning.Core.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace AikoLearning.Infrastructure.Data.Security;

public class AuthenticateService : IAuthenticate
{
    #region Properties
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    #endregion

    #region Constructors
    public AuthenticateService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        this.signInManager = signInManager;        
        this.userManager = userManager;
    }
    #endregion

    #region Methods
    public async Task<bool> Authenticate(string email, string password)
    {
        var result = await signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);
        return result.Succeeded;
    }

    public async Task Logout()
    {
        await signInManager.SignOutAsync();
    }

    public async Task<bool> RegisterUser(string email, string password)
    {
        var applicationUser = new ApplicationUser
        {
            UserName = email,
            Email = email,
        };

        var result = await userManager.CreateAsync(applicationUser, password);

        if (result.Succeeded)
            await signInManager.SignInAsync(applicationUser, isPersistent: false);
        
        return result.Succeeded;
    }
    #endregion
}