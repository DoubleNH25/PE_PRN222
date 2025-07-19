using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LionPetManagement_NguyenHangNhatHuy.Pages
{
	public class LogOutModel : PageModel
	{
		public async Task<IActionResult> OnGet()
		{
			await HttpContext.SignOutAsync();

			foreach (var cookie in Request.Cookies.Keys)
			{
				Response.Cookies.Delete(cookie);
			}

			return RedirectToPage("/Login");
		}
	}
}
