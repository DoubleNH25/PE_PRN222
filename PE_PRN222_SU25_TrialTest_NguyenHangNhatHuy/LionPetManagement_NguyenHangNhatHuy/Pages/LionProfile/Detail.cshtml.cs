using LionPetManagement_NguyenHangNhatHuy.BLL;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LionPetManagement_NguyenHangNhatHuy.Pages.LionProfile
{
	public class DetailModel : PageModel
	{
		private readonly LionProfileService _lionProfileService;

		public DetailModel(LionProfileService lionProfileService)
		{
			_lionProfileService = lionProfileService;
		}

		public LionPetManagement_NguyenHangNhatHuy.DAL.Models.LionProfile? lionProfile { get; set; }

		public async Task<IActionResult> OnGetAsync(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return NotFound();
			}

			lionProfile = _lionProfileService.GetById(id);
			if (lionProfile == null)
			{
				return NotFound();
			}

			return Page();
		}
	}
}
