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

    }
}
