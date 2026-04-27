using DSVMeetingRoomBooking.Models;
using DSVMeetingRoomBooking.Services;
using System.Text.Json;

namespace DSVMeetingRoomBooking.Repositories
{
	public class MeetingRoomRepository : IMeetingRoomRepository
    {
		private string _filePath = @"Data/rooms.json";

		public List<MeetingRoom> _meetingRooms;

		public MeetingRoomRepository()
		{
			if (File.Exists(_filePath))
			{
				LoadFile();
			}
		}

        public void LoadFile()
		{
			string json = File.ReadAllText(_filePath);
			_meetingRooms = JsonSerializer.Deserialize<List<MeetingRoom>>(json);		
        }
		public void SaveFile()
		{
			string json = JsonSerializer.Serialize(_meetingRooms);
            File.WriteAllText(_filePath, json);
        }

		public List<MeetingRoom> GetAllMeetingRooms()
		{
			return _meetingRooms;
		}

        public void AddRoom(MeetingRoom meetingRoom)
		{
			_meetingRooms.Add(meetingRoom);
			SaveFile();
		}

		public MeetingRoom GetMeetingRoomById(string id)
		{
			foreach (MeetingRoom room in _meetingRooms)
			{
				if (room.RoomId == id)
				{
					return room;
				}
			}
			return null;
		}
	}
}
