using DSVMeetingRoomBooking.Models;
using DSVMeetingRoomBooking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSVMeetingRoomBooking.Pages
{
    public class BookingDetailModel : PageModel
    {
        private BookingService _bookingService;
        private MeetingRoomService _meetingRoomService;

        [BindProperty]
        public Booking Booking { get; set; }

        [BindProperty]
        public bool Confirmation { get; set; }

        public MeetingRoom MeetingRoom { get; set; }

        public BookingDetailModel(BookingService bookingService, MeetingRoomService meetingRoomService)
        {
            _bookingService = bookingService;
            _meetingRoomService = meetingRoomService;
            
        }
        public IActionResult OnGet(string id, bool confirmation)
        {
            try
            {
                Booking = _bookingService.GetBooking(id);
                Confirmation = confirmation;
                MeetingRoom = _meetingRoomService.GetMeetingRoomById(Booking.RoomId);
                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/Index"); // If the booking ID is not found, redirect to the index page
            }
        }
    }
}
