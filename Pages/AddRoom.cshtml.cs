using DSVMeetingRoomBooking.Models;
using DSVMeetingRoomBooking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSVMeetingRoomBooking.Pages
{
    public class AddRoomModel : PageModel
    {       
        private readonly MeetingRoomService _meetingRoomService;
        public AddRoomModel(MeetingRoomService meetingRoomService)
        {
            _meetingRoomService = meetingRoomService;
        }

        [BindProperty]
        public MeetingRoom MeetingRoom { get; set; }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _meetingRoomService.AddRoom(MeetingRoom);
            return RedirectToPage("/Index");
        }
    }
}
