using DSVMeetingRoomBooking.Models;
using DSVMeetingRoomBooking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSVMeetingRoomBooking.Pages
{
    public class CreateBookingModel : PageModel
    {
        [BindProperty]
        public required string RoomId { get; set; }

        [BindProperty]
        public string? Comment { get; set; }

        public MeetingRoom MeetingRoom { get; set; }

        private BookingService _bookingService;
        private MeetingRoomService _meetingRoomService;

        public CreateBookingModel(BookingService bookingService, MeetingRoomService meetingRoomService)
        {
            _bookingService = bookingService;
            _meetingRoomService = meetingRoomService;
        }

        public IActionResult OnGet(string id)
        {
            MeetingRoom = _meetingRoomService.GetMeetingRoomById(id);

            // If the meeting room with the specified ID does not exist, redirect to the index page
            if (MeetingRoom == null)
            {
                return RedirectToPage("/Index");
            }
            
            // Do nothing if the meeting room exists
            return Page();
        }

        public IActionResult OnPost()
        {
            TimeSlot timeSlot = new TimeSlot(DateTime.Now, DateTime.Now.AddHours(1)); // temporary time slot for testing

            string comment = "";
            if (Comment != null)
            {
                comment = Comment;
            }
            
            Booking booking = new Booking(RoomId, timeSlot, comment);
            _bookingService.CreateBooking(booking);
            return RedirectToPage("/Index");
        }
    }
}
