using LionPetManagement_NguyenHangNhatHuy.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LionPetManagement_NguyenHangNhatHuy.Pages.LionProfile
{
	[Authorize(Roles = "2")]
	public class CreateModel : PageModel
	{
		private readonly LionProfileService _lionProfileService;
		private readonly LionTypeService _lionTypeService;

		public CreateModel(LionProfileService lionProfileService, LionTypeService lionTypeService)
		{
			_lionProfileService = lionProfileService;
			_lionTypeService = lionTypeService;
		}

		public IActionResult OnGet()
		{
			ViewData["LionTypeId"] = new SelectList(_lionTypeService.GetLionType(), "LionTypeId", "LionTypeName");
			return Page();
		}

		[BindProperty] public LionPetManagement_NguyenHangNhatHuy.DAL.Models.LionProfile? LionProfile { get; set; } = default!;
		public async Task<IActionResult> OnPostAsync()
		{
			// Đảm bảo không gán giá trị cho LionProfileId khi thêm mới
			LionProfile.LionProfileId = 0; // hoặc bỏ dòng này nếu không cần thiết
			_lionProfileService.Add(LionProfile);
			return RedirectToPage("./Index");
		}
	}
}
