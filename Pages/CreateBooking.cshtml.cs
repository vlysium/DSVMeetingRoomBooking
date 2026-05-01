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
        public required DateTimeOffset SelectedDay { get; set; }

        [BindProperty]
        public required DateTimeOffset TimeStart { get; set; }

        [BindProperty]
        public required DateTimeOffset TimeEnd { get; set; }

        [BindProperty]
        public required string EmployeeId { get; set; }

        [BindProperty]
        public string? Comment { get; set; }

        public List<MeetingRoom> MeetingRooms { get; set; }

        private BookingService _bookingService;
        private MeetingRoomService _meetingRoomService;

        public CreateBookingModel(BookingService bookingService, MeetingRoomService meetingRoomService)
        {
            _bookingService = bookingService;
            _meetingRoomService = meetingRoomService;
            MeetingRooms = meetingRoomService.GetAllMeetingRooms();
        }

        public IActionResult OnGet(string id, string date, string timeStart, string timeEnd)
        {
            try
            {
                SelectedDay = DateTimeOffset.Parse(date);
                TimeStart = DateTimeOffset.Parse(timeStart);
                TimeEnd = DateTimeOffset.Parse(timeEnd);

                foreach (var meetingRoom in MeetingRooms)
                {
                    if (meetingRoom.RoomId == id)
                    {
                        RoomId = id;
                        return Page();
                    }
                }
                
                // If the room ID is not found, redirect to the index page
                return RedirectToPage("/Index");
            }
            catch (Exception)
            {
                return RedirectToPage("/Index");
            }
        }

        public IActionResult OnPost()
        {
            try
            {
                TimeSlot timeSlot = new TimeSlot(TimeStart, TimeEnd);
                timeSlot.FormatTimeSlot(SelectedDay, TimeStart, TimeEnd);

                // Check if the room is available for the selected time slot
                if (!_bookingService.IsRoomAvailable(RoomId, timeSlot))
                {
                    ModelState.AddModelError("RoomId", "The selected room is not available for the chosen time slot. Please select a different time or room.");
                    return Page();
                }

                string comment = "";
                if (Comment != null)
                {
                    comment = Comment;
                }
                
                Booking booking = new Booking(EmployeeId, RoomId, timeSlot, comment);
                _bookingService.CreateBooking(booking);
                return RedirectToPage("/BookingDetail", new { id = booking.Id, created = true });
            }
            catch (Exception)
            {
                return RedirectToPage("/Index");
            }
        }
    }
}
