using LionPetManagement_NguyenHangNhatHuy.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LionProfileModel = LionPetManagement_NguyenHangNhatHuy.DAL.Models.LionProfile;

namespace LionPetManagement_NguyenHangNhatHuy.Pages.LionProfile
{
	[Authorize(Roles = "2, 3")]
	public class SearchModel : PageModel
    {
        private readonly LionProfileService _lionProfileService;

        public SearchModel(LionProfileService lionProfileService)
        {
            _lionProfileService = lionProfileService;
        }

        [BindProperty(SupportsGet = true)]
        public string? SearchLionName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchLionTypeName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchLionWeight { get; set; }

        		public List<LionProfileModel> Lions { get; set; } = new List<LionProfileModel>();
		public int PageNumber { get; set; } = 1;
		public int TotalPages { get; set; }
		public int TotalItems { get; set; }

		public async Task<IActionResult> OnGetAsync(int pageNumber = 1, int pageSize = 3)
		{
			PageNumber = pageNumber;

			if (!string.IsNullOrEmpty(SearchLionName) || 
				!string.IsNullOrEmpty(SearchLionTypeName) || 
				!string.IsNullOrEmpty(SearchLionWeight))
			{
				var result = await _lionProfileService.SearchLionsWithPaginationAsync(SearchLionName, SearchLionTypeName, SearchLionWeight, pageNumber, pageSize);
				Lions = result.Items;
				TotalItems = result.TotalItems;
				TotalPages = (int)Math.Ceiling(TotalItems / (double)pageSize);

				if (PageNumber > TotalPages && TotalPages > 0)
				{
					PageNumber = TotalPages;
				}
			}

			return Page();
		}
    }
} 