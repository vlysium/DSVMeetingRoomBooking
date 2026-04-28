using DSVMeetingRoomBooking.Models;
using DSVMeetingRoomBooking.Repositories;

namespace DSVMeetingRoomBooking.Services
{
	public class MeetingRoomService
	{
		private readonly IMeetingRoomRepository _meetingRoomRepository;
		private readonly BookingService _bookingService;
		public List<MeetingRoom> _meetingRooms;

		public MeetingRoomService(IMeetingRoomRepository meetingRoomRepository, BookingService bookingService)
		{
			_meetingRoomRepository = meetingRoomRepository;
			_bookingService = bookingService;
		}

		public List<MeetingRoom> GetAllMeetingRooms()
		{
			_meetingRooms = _meetingRoomRepository.GetAllMeetingRooms();
			return _meetingRooms;
        }
		public void AddRoom(MeetingRoom meetingRoom)
		{
			_meetingRoomRepository.AddRoom(meetingRoom);
        }

		public MeetingRoom GetMeetingRoomById(string id)
		{
			return _meetingRoomRepository.GetMeetingRoomById(id);
		}

        public List<MeetingRoom> FilterMeetingRooms(string selectedCapacity, List<Equipment> selectedEquipment)
        {
            List<MeetingRoom> allRooms = GetAllMeetingRooms();
            List<MeetingRoom> filteredRooms = new List<MeetingRoom>();

            foreach (MeetingRoom room in allRooms)
            {
                if (!MatchesCapacity(room, selectedCapacity))
                {
                    continue;
                }

                if (!MatchesEquipment(room, selectedEquipment))
                {
                    continue;
                }

                filteredRooms.Add(room);
            }

            return filteredRooms;
        }

        // Private helper methods for filtering
        private bool MatchesCapacity(MeetingRoom room, string selectedCapacity)
        {
            if (selectedCapacity == "all")
            {
                return true;
            }
            if (selectedCapacity == "small" && room.Capacity <= 25)
            {
                return true;
            }
            if (selectedCapacity == "medium" && room.Capacity > 25 && room.Capacity <= 50)
            {
                return true;
            }
            if (selectedCapacity == "large" && room.Capacity > 50 && room.Capacity <= 75)
            {
                return true;
            }
            if (selectedCapacity == "xlarge" && room.Capacity > 75)
            {
                return true;
            }

            return false;
        }

        private bool MatchesEquipment(MeetingRoom room, List<Equipment> selectedEquipment)
        {
            // If no equipment is selected, we consider it a match and show all rooms
            if (selectedEquipment == null || selectedEquipment.Count == 0)
            {
                return true;
            }

            foreach (Equipment equipment in selectedEquipment)
            {
                // Return false if the room does not contain any of the selected equipment
                if (!room.Equipment.Contains(equipment))
                {
                    return false; 
                }
            }

            return true;
        }
    }
}
