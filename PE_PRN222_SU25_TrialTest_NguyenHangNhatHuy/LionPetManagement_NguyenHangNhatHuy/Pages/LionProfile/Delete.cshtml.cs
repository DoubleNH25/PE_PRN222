using LionPetManagement_NguyenHangNhatHuy.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace LionPetManagement_NguyenHangNhatHuy.Pages.LionProfile
{
	[Authorize(Roles = "2")]
	public class DeleteModel : PageModel
    {
        private readonly LionProfileService _lionProfileService;
        private readonly IHubContext<LionPetManagement_NguyenHangNhatHuy.LionProfileHub> _hubContext;
        public DeleteModel(LionProfileService lionProfileService, IHubContext<LionPetManagement_NguyenHangNhatHuy.LionProfileHub> hubContext)
        {
            _lionProfileService = lionProfileService;
            _hubContext = hubContext;
        }
        [BindProperty] public LionPetManagement_NguyenHangNhatHuy.DAL.Models.LionProfile? LionProfile { get; set; } = default!;
        public IActionResult OnGet(int id)
        {
            LionProfile = _lionProfileService.GetById(id.ToString());
            if (LionProfile == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (LionProfile != null)
            {
                _lionProfileService.Delete(LionProfile);
                await _hubContext.Clients.All.SendAsync("LionProfileDeleted", LionProfile.LionProfileId);
            }
            return RedirectToPage("Index");
        }
    }
}
