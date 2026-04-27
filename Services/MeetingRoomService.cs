using DSVMeetingRoomBooking.Models;
using DSVMeetingRoomBooking.Repositories;

namespace DSVMeetingRoomBooking.Services
{
	public class MeetingRoomService
	{
		private readonly IMeetingRoomRepository _meetingRoomRepository;
		private List<MeetingRoom> _meetingRooms;
		public MeetingRoomService(IMeetingRoomRepository meetingRoomRepository)
		{
			_meetingRoomRepository = meetingRoomRepository;
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

    }
}
