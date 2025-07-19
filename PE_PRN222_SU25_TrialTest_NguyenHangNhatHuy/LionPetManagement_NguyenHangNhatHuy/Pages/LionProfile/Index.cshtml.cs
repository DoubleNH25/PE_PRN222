using LionPetManagement_NguyenHangNhatHuy.BLL;
using LionPetManagement_NguyenHangNhatHuy.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LionPetManagement_NguyenHangNhatHuy.Pages.LionProfile
{
	public class IndexModel : PageModel
	{
		private readonly LionProfileService _lionProfileService;

		public IndexModel(LionProfileService lionProfileService)
		{
			_lionProfileService = lionProfileService;
		}

		public IList<LionPetManagement_NguyenHangNhatHuy.DAL.Models.LionProfile?> LionProfile { get; set; } = default!;
		public int PageNumber { get; set; } = 1;
		public int TotalPages { get; set; }
		public int TotalItems { get; set; }

		public async Task OnGetAsync(int pageNumber = 1, int pageSize = 3)
		{
			PageNumber = pageNumber;

			int totalItems;
			LionProfile = _lionProfileService.GetList(pageNumber, pageSize, out totalItems, null, null, null);

			TotalItems = totalItems;
			TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

			if (PageNumber > TotalPages && TotalPages > 0)
			{
				PageNumber = TotalPages;
			}
		}
	}
}
