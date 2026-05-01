using DSVMeetingRoomBooking.Models;
using DSVMeetingRoomBooking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;

namespace DSVMeetingRoomBooking.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MeetingRoomService _meetingRoomService;
        private readonly BookingService _bookingService;

        public List<MeetingRoom> MeetingRooms { get; set; }

        public Dictionary<MeetingRoom, bool> AvailableRooms { get; set; }

        // List of options to loop through in the view for filtering
        public List<(string Value, string Label)> CapacityOptions = new()
        {
            ("all", "All"),
            ("small", "0-25"),
            ("medium", "25-50"),
            ("large", "50-75"),
            ("xlarge", "75+")
        };

        // List of options to loop through in the view for filtering
        public List<(Equipment Value, string Label)> EquipmentOptions = new()
        {
            (Equipment.Projector, "Projector"),
            (Equipment.WhiteBoard, "Whiteboard"),
            (Equipment.Microphone, "Microphone")
        };

        
        [BindProperty(SupportsGet = true)]
        public string SelectedCapacity { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<Equipment> SelectedEquipment { get; set; } = new List<Equipment>();

        [BindProperty(SupportsGet = true)]
        public DateTime SelectedDay { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime TimeStart { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime TimeEnd { get; set; }

        public IndexModel(MeetingRoomService meetingRoomService, BookingService bookingService)
        {
            _meetingRoomService = meetingRoomService;
            _bookingService = bookingService;
            MeetingRooms = meetingRoomService.GetAllMeetingRooms();
        }

        public void OnGet(string selectedCapacity, List<Equipment> selectedEquipment, string selectedDay, string timeStart, string timeEnd)
        {
			TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Europe/Copenhagen"); // Band-aid fix for timezone issues for now

            SelectedCapacity = selectedCapacity ?? "all"; // Default to "all" if no capacity is selected
            SelectedEquipment = selectedEquipment;

            SelectedDay = string.IsNullOrEmpty(selectedDay) // Default to current day if no date is selected on first load
                ? TimeZoneInfo.ConvertTime(DateTime.Now.Date, tz)
                : DateTime.Parse(selectedDay);
            TimeStart = string.IsNullOrEmpty(timeStart) // Default to current time if no start time is selected on first load
                ? TimeZoneInfo.ConvertTime(DateTime.Now, tz)
                : DateTime.Parse(timeStart);
            TimeEnd = string.IsNullOrEmpty(timeEnd) // Default to one hour from now if no end time is selected on first load
                ? TimeZoneInfo.ConvertTime(DateTime.Now.AddHours(1), tz)
                : DateTime.Parse(timeEnd);

            MeetingRooms = _meetingRoomService.FilterMeetingRooms(SelectedCapacity, SelectedEquipment);
            ShowRoomAvailability();
        }

        private void ShowRoomAvailability()
        {
            TimeSlot timeSlot = new TimeSlot(TimeStart, TimeEnd);
            timeSlot.FormatTimeSlot(SelectedDay, TimeStart, TimeEnd);

            AvailableRooms = new Dictionary<MeetingRoom, bool>();
            foreach (MeetingRoom room in MeetingRooms)
            {
                bool isAvailable = _bookingService.IsRoomAvailable(room.RoomId, timeSlot);
                AvailableRooms.Add(room, isAvailable);
            }
        }
    }
}
