using DSVMeetingRoomBooking.Models;
using DSVMeetingRoomBooking.Services;
using System.Text.Json;

namespace DSVMeetingRoomBooking.Repositories
{
	public class MeetingRoomRepository: IMeetingRoomRepository
    {
		private readonly string _filePath = @"Data/MeetingRooms.json";

		private readonly MeetingRoomService _meetingRoomService;
		private List<MeetingRoom> _meetingRooms;

        public MeetingRoomRepository(MeetingRoomService meetingRoomService)
		{
			_meetingRoomService = meetingRoomService;
        }

		public List<MeetingRoom> GetAllMeetingRooms()
		{
			LoadFile();
			return _meetingRooms;
		}

        public void LoadFile()
		{
			string json = File.ReadAllText(_filePath);
			var _meetingRooms = JsonSerializer.Deserialize<List<MeetingRoom>>(json);		
        }
		public void SaveFile()
		{
			string json = JsonSerializer.Serialize(_filePath);
            File.WriteAllText(_filePath, json);
        }

		public void AddRoom(MeetingRoom meetingRoom)
		{
			LoadFile();
			_meetingRooms.Add(meetingRoom);
			SaveFile();
		}
    }
}
