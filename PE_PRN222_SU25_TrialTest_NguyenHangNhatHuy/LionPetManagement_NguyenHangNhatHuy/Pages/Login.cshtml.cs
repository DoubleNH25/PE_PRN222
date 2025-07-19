using LionPetManagement_NguyenHangNhatHuy.BLL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace LionPetManagement_NguyenHangNhatHuy.Pages
{
	public class LoginModel : PageModel
	{
		private readonly LionAccountService _accountService;

		public LoginModel(LionAccountService accountService)
		{
			_accountService = accountService;
		}

		[BindProperty] public string Email { get; set; } = default!;
		[BindProperty] public string Password { get; set; } = default!;

		public string ErrorMessage { get; set; } = string.Empty;

		public async Task<IActionResult> OnGetAsync()
		{
			if (User.Identity != null && User.Identity.IsAuthenticated)
			{
				return RedirectToAction("/LionProfile/Index");
			}
			return Page();
		}

		//Login
		public async Task<IActionResult> OnPostAsync()
		{
			if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
			{
				ErrorMessage = "Email and password are required!";
				ModelState.AddModelError(string.Empty, ErrorMessage);
				return Page();
			}

			var Account = _accountService.Login(Email, Password);

			if (Account == null)
			{
				ErrorMessage = "Invalid Email or Password! Please try again.";
				ModelState.AddModelError(string.Empty, ErrorMessage);
				return Page();
			}

			if (Account.RoleId != 2 && Account.RoleId != 3)
			{
				ErrorMessage = "You do not have permission to do this function!";
				ModelState.AddModelError(string.Empty, ErrorMessage);
				return RedirectToPage("/LionProfile/Index");
			}

			//Cookie
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, Account.AccountId.ToString()),
				new Claim(ClaimTypes.Role, Account.RoleId.ToString()),
				new Claim(ClaimTypes.Name, Account.UserName)
			};

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var authenProperties = new AuthenticationProperties
			{
				IsPersistent = false
			};

			await HttpContext.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimsIdentity),
				authenProperties
			);
			return RedirectToPage("/LionProfile/Index");

		}
	}
}
