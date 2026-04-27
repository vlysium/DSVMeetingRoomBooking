using DSVMeetingRoomBooking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSVMeetingRoomBooking.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MeetingRoomService _meetingRoomService;
        public IndexModel(MeetingRoomService meetingRoomService)
        {
            _meetingRoomService = meetingRoomService;
        }
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            return RedirectToPage("/Index");
        }
}
