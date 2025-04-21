using Ikea.BLL.Common.EmailSettings;
using Ikea.DAL.Entities.Identity;
using Ikea.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Threading.Tasks;

namespace Ikea.PL.Controllers.Auth
{
    public class AuthController(UserManager<ApplicationUser> userManager,
                               SignInManager<ApplicationUser> signInManager) : Controller
    {
        #region Services

        // three main Services [ UserMAanager , SignInManager , RoleManager ]
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

        #endregion

        #region SignUp

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel Model)
        {
            // validate ModelState
            if (!ModelState.IsValid)
                return BadRequest();

            var User = await _userManager.FindByNameAsync(Model.UserName);

            if (User is { })
            {
                ModelState.AddModelError(nameof(SignUpViewModel.UserName), "This UserName has been already In Use in another Account ");
                // return View(Model);
            }
            else
            {

                // check on username [ if it existed or not ]

                // Manual Mapping [SignUpViewModel => IdentityUser]
                User = new ApplicationUser()
                {
                    FName = Model.FName,
                    LName = Model.LName,
                    UserName = Model.UserName,
                    Email = Model.Email,
                    IsAgree = Model.IsAgree,

                };

                var Result = await _userManager.CreateAsync(User, Model.Password); //CreateAsync Depened on Some Services of UserMananger So UMust to Allow services to DI Container by using AddEntityFrameWorkStores();  

                if (Result.Succeeded)
                    return RedirectToAction(nameof(SignIn));
                else// if not succeeded
                    foreach (var Error in Result.Errors)
                        ModelState.AddModelError(string.Empty, Error.Description);
            }
            return View(Model);
        }

        #endregion

        #region SignIn | Login

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel Model)
        {
            // check on modelState
            if (ModelState.IsValid)
            {
                // find User
                var user = await _userManager.FindByEmailAsync(Model.Email);
                // check on user 
                if (user is { })
                {   // check on User Password
                    var PasswordFlag = await _userManager.CheckPasswordAsync(user, Model.Password);
                    // if password is correct 
                    if (PasswordFlag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, Model.Password, Model.RememberMe, true);

                        if (result.IsNotAllowed)// if account Confirmed 
                            ModelState.AddModelError(string.Empty, "Your Account IsNot Confirmed ");

                        if (result.IsLockedOut) // If Account is LockedOut 
                            ModelState.AddModelError(string.Empty, "Your Account is Locked");
                        ///if (result.RequiresTwoFactor)
                        ///{
                        ///    // WorkShop 
                        ///}
                        if (result.Succeeded) // if Succeed
                            return RedirectToAction("Index", "Home");// Index , Controller

                        // then GenerateToken 
                    }
                    // if password is not correct
                    else
                        ModelState.AddModelError(string.Empty, "Invalid Login Attempt ."); // lock mafrood Mwada74 hewa 8altan fy eh 
                }

                else // UserNot Found
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt ."); // lock mafrood Mwada74 hewa 8altan fy eh 
                //return View(Model);
            }

            return View(Model);
        }

        #endregion

        #region SignOut

        public async new Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }




        #endregion


        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel Model)
        {
            if (ModelState.IsValid)
            {
                var User = await _userManager.FindByEmailAsync(Model.Email);
                if (User is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(User);
                    // Token is Valid Only For one use 
                    // Https://localhost:44311/Auth/ResetPassword?email=mo5195278@gmail.com/Token
                    var ResetPasswordLink = Url.Action("ResetPassword", "Auth", new { email = User.Email, Token = token }, Request.Scheme);
                    // Send Email 
                    var email = new Email()
                    {
                        To = Model.Email,
                        Subject = "Reset Password",
                        Body = ResetPasswordLink,
                    };
                    EmailSetting.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
                else
                    ModelState.AddModelError(string.Empty, "Your Email is Not Exists");
            }
            return View("ForgetPassword", Model);
        }

        [HttpGet]
        public IActionResult CheckYourInbox()
        {
            return View();
        }


        #region Reset Password
        // OldPassword = P@ssw0rd
        // NewPassword = Pa$$w0rd
        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            // TempData => To Transfere data From Action to action 
            TempData["Email"] = email;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                string? Token = TempData["token"] as string;
                string? email = TempData["email"] as string;
                var User = await _userManager.FindByEmailAsync(email!);

                var Result = await _userManager.ResetPasswordAsync(User, Token, model.NewPassword);
                if (Result.Succeeded)
                    return RedirectToAction(nameof(SignIn));
                else
                    foreach (var error in Result.Errors)
                        ModelState.AddModelError(string.Empty, "Invalid Password ");
            }
            return View(model);
        }

        #endregion
    }
}
