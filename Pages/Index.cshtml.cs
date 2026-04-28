using DSVMeetingRoomBooking.Models;
using DSVMeetingRoomBooking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSVMeetingRoomBooking.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MeetingRoomService _meetingRoomService;
        private readonly BookingService _bookingService;
        public List<MeetingRoom> MeetingRooms { get; set; }

        [BindProperty]
        public string SearchTerm { get; set; }
        public IndexModel(MeetingRoomService meetingRoomService, BookingService bookingService)
        {            
            _meetingRoomService = meetingRoomService;
            _bookingService = bookingService;
            MeetingRooms = _meetingRoomService.GetAllMeetingRooms();

        }
        public void OnGet()
        {
            
        }
        public IActionResult OnPostSearchTerm()
        {
            Console.WriteLine($"Search term: {SearchTerm}");

            if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                return RedirectToPage("/Index");
            }
            return RedirectToPage("/Bookings", new { EmployeeId = SearchTerm });

        }
        public IActionResult OnPost()
        {
            return RedirectToPage("/Index");
        }
    }
}
