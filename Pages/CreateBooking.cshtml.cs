using DSVMeetingRoomBooking.Models;
using DSVMeetingRoomBooking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSVMeetingRoomBooking.Pages
{
    public class CreateBookingModel : PageModel
    {
        [BindProperty]
        public string RoomId { get; set; }

        [BindProperty]
        public string? Comment { get; set; }

        private BookingService _bookingService;

        public CreateBookingModel(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public void OnGet(string roomId)
        {
            ViewData["roomId"] = roomId;
        }

        public IActionResult OnPost()
        {
            TimeSlot timeSlot = new TimeSlot(DateTime.Now, DateTime.Now.AddHours(1)); // temporary time slot for testing
            Booking booking = new Booking(100, timeSlot, Comment); // temporary booking ID for testing
            _bookingService.CreateBooking(booking);
            return RedirectToPage("/Index");
        }
    }
}
