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
            ShowRoomAvailability();
        }

        public void OnGet(string selectedCapacity, List<Equipment> selectedEquipment, DateTime selectedDay, DateTime timeStart, DateTime timeEnd)
        {
            SelectedCapacity = selectedCapacity ?? "all"; // Default to "all" if no capacity is selected
            SelectedEquipment = selectedEquipment; 
            SelectedDay = selectedDay.Date == DateTime.MinValue.Date ? DateOnly.FromDateTime(DateTime.Now).ToDateTime(TimeOnly.MinValue) : selectedDay; // Default to current day if no date is selected
            TimeStart = timeStart == DateTime.MinValue ? DateTime.Now : timeStart; // Default to current time if no start time is selected
            TimeEnd = timeEnd == DateTime.MinValue ? DateTime.Now.AddHours(1) : timeEnd; // Default to one hour from now if no end time is selected

            MeetingRooms = _meetingRoomService.FilterMeetingRooms(SelectedCapacity, SelectedEquipment);
            ShowRoomAvailability();
        }

        private void ShowRoomAvailability()
        {
            TimeSlot timeSlot = new TimeSlot(TimeStart, TimeEnd).FormatTimeSlot(SelectedDay, TimeStart, TimeEnd);

            AvailableRooms = new Dictionary<MeetingRoom, bool>();
            foreach (MeetingRoom room in MeetingRooms)
            {
                bool isAvailable = _bookingService.IsRoomAvailable(room.RoomId, timeSlot);
                AvailableRooms.Add(room, isAvailable);
            }
        }
    }
}
