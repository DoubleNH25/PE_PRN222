using LionPetManagement_NguyenHangNhatHuy.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LionProfileModel = LionPetManagement_NguyenHangNhatHuy.DAL.Models.LionProfile;

namespace LionPetManagement_NguyenHangNhatHuy.Pages.LionProfile
{
	[Authorize(Roles = "2")]
	public class UpdateModel : PageModel
	{
		private readonly LionProfileService _lionProfileService;
		private readonly LionTypeService _lionTypeService;

		public UpdateModel(LionProfileService lionProfileService, LionTypeService lionTypeService)
		{
			_lionProfileService = lionProfileService;
			_lionTypeService = lionTypeService;
		}

		[BindProperty]
		public LionProfileModel LionProfile { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var lionProfile = _lionProfileService.GetIntById(id.Value);
			if (lionProfile == null)
			{
				return NotFound();
			}

			LionProfile = lionProfile;

			// Load LionType dropdown
			ViewData["LionTypeId"] = new SelectList(_lionTypeService.GetLionType(), "LionTypeId", "LionTypeName");

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{

			if (!ModelState.IsValid)
			{
				ViewData["LionTypeId"] = new SelectList(_lionTypeService.GetLionType(), "LionTypeId", "LionTypeName");
				return Page();
			}

			if (LionProfile.LionTypeId > 0)
			{
				var lionType = _lionTypeService.GetLionType().FirstOrDefault(lt => lt.LionTypeId == LionProfile.LionTypeId);
				if (lionType != null)
				{
					LionProfile.LionType = lionType;
				}
			}

			try
			{
				var existingLionProfile = _lionProfileService.GetIntById(LionProfile.LionProfileId);
				if (existingLionProfile == null)
				{
					return NotFound();
				}
				existingLionProfile.LionName = LionProfile.LionName;
				existingLionProfile.LionTypeId = LionProfile.LionTypeId;
				existingLionProfile.Weight = LionProfile.Weight;
				existingLionProfile.Characteristics = LionProfile.Characteristics;
				existingLionProfile.Warning = LionProfile.Warning;
				existingLionProfile.ModifiedDate = DateTime.Now;

				_lionProfileService.Update(existingLionProfile);

				return RedirectToPage("./Index");
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", "An error occurred while updating the lion profile. Please try again.");

				ViewData["LionTypeId"] = new SelectList(_lionTypeService.GetLionType(), "LionTypeId", "LionTypeName");

				return Page();
			}
		}
	}
}
