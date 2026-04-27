namespace DSVMeetingRoomBooking.Models
{
	public class MeetingRoom
	{

		public int RoomId { get; set; }
		
        public string NameOfRoom { get; set; }

		public bool IsAvailable { get; set; }

		public int Capacity { get; set; }


		public MeetingRoom()
		{



		}

		//public MeetingRoom ( int roomid , string nameOfRoom , bool isavailble , int capacity)
		//{
		//	RoomId = roomid;
		//	nameOfRoom = nameOfRoom;
		//	IsAvailable = isavailble;
		//	Capacity = capacity;



		//}
    }
}
