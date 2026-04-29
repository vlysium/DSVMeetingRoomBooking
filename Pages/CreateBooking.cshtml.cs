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
        public required DateTime SelectedDay { get; set; }

        [BindProperty]
        public required DateTime TimeStart { get; set; }

        [BindProperty]
        public required DateTime TimeEnd { get; set; }

        [BindProperty]
        public required string EmployeeId { get; set; }

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

        public IActionResult OnGet(string id, string date, string timeStart, string timeEnd)
        {
            MeetingRoom = _meetingRoomService.GetMeetingRoomById(id);

            SelectedDay = DateTime.Parse(date);
            TimeStart = DateTime.Parse(timeStart);
            TimeEnd = DateTime.Parse(timeEnd);

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
            TimeSlot timeSlot = new TimeSlot(TimeStart, TimeEnd).FormatTimeSlot(SelectedDay, TimeStart, TimeEnd);

            Console.WriteLine($"StartTime: {TimeStart}, EndTime: {TimeEnd}");

            string comment = "";
            if (Comment != null)
            {
                comment = Comment;
            }
            
            Booking booking = new Booking(EmployeeId, RoomId, timeSlot, comment);
            _bookingService.CreateBooking(booking);
            return RedirectToPage("/Index");
        }
    }
}
