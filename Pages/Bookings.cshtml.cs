using DSVMeetingRoomBooking.Models;
using DSVMeetingRoomBooking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSVMeetingRoomBooking.Pages
{
    public class BookingsModel : PageModel
    {
        [BindProperty]
        public string SearchTerm { get; set; }

        [BindProperty]
        public bool IsDeleted { get; set; }
        public List<Booking> Bookings { get; set; }
        public List<MeetingRoom> MeetingRooms { get; set; }
        private readonly BookingService _bookingService;
        private readonly MeetingRoomService _meetingRoomService;
        public BookingsModel(BookingService bookingservice, MeetingRoomService meetingRoomService)
        {
            _bookingService = bookingservice;
            _meetingRoomService = meetingRoomService;
            Bookings = _bookingService.GetAllBookings();
            MeetingRooms = _meetingRoomService.GetAllMeetingRooms();
        }

        public void OnGet(string employeeId, bool deleted)
        {
            IsDeleted = deleted;

            if (!string.IsNullOrEmpty(employeeId))
            {
                Bookings = _bookingService.GetBookingsByEmployeeId(employeeId);
            }
        }

        public IActionResult OnPostSearchTerm()
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                return Page();
            }
            return RedirectToPage("/Bookings", new { EmployeeId = SearchTerm });
        }
    }
}

