using DSVMeetingRoomBooking.Models;
using DSVMeetingRoomBooking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSVMeetingRoomBooking.Pages
{
    public class EditBookingModel : PageModel
    {
        private BookingService _bookingService;
        private MeetingRoomService _meetingRoomService;

        [BindProperty]
        public required string RoomId { get; set; }

        [BindProperty]
        public DateTimeOffset SelectedDay { get; set; }

        [BindProperty]
        public DateTimeOffset TimeStart { get; set; }

        [BindProperty]
        public DateTimeOffset TimeEnd { get; set; }

        [BindProperty]
        public required string EmployeeId { get; set; }

        [BindProperty]
        public string? Comment { get; set; }

        public Booking Booking { get; set; }

        public List<MeetingRoom> MeetingRooms { get; set; }

        public EditBookingModel(BookingService bookingService, MeetingRoomService meetingRoomService)
        {
            _bookingService = bookingService;
            _meetingRoomService = meetingRoomService;
            MeetingRooms = meetingRoomService.GetAllMeetingRooms();
        }
        public IActionResult OnGet(string id)
        {
            try
            {
                Booking = _bookingService.GetBooking(id);

                RoomId = Booking.RoomId;
                SelectedDay = Booking.TimeSlot.StartTime.Date;
                TimeStart = Booking.TimeSlot.StartTime;
                TimeEnd = Booking.TimeSlot.EndTime;
                EmployeeId = Booking.EmployeeId;
                Comment = Booking.Comment;
                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/Index"); // If the booking ID is not found, redirect to the index page
            }
        }

        public IActionResult OnPostUpdate(string id)
        {
            try
            {
                Booking = _bookingService.GetBooking(id);
                TimeSlot timeSlot = new TimeSlot(TimeStart, TimeEnd).FormatTimeSlot(SelectedDay, TimeStart, TimeEnd);

                // Check if the room is available for the new time slot, excluding the current booking
                if (!_bookingService.IsRoomAvailable(RoomId, timeSlot, id))
                {
                    ModelState.AddModelError("RoomId", "The selected room is not available for the chosen time slot. Please select a different time or room.");
                    return Page();
                }

                Booking.TimeSlot = timeSlot;
                Booking.EmployeeId = EmployeeId;
                Booking.RoomId = RoomId;
                Booking.Comment = Comment;

                _bookingService.UpdateBooking(Booking);
                return RedirectToPage("/BookingDetail", new { id = Booking.Id, updated = true });
            }
            catch (Exception)
            {
                return Page();
            }
        }
    }
}
