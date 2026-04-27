namespace DSVMeetingRoomBooking.Models
{

    public class MeetingRoom
	{      

        public int RoomId { get; set; }
		
        public string Name { get; set; }		

		public int Capacity { get; set; }
		public Equipment?[] equipment { get; set; }
		public string Description { get; set; }



        public MeetingRoom()
		{



		}

		//public MeetingRoom ( int roomid , string nameOfRoom , bool isavailble , int capacity)
		//{
		//	RoomId = roomid;
		//	nameOfRoom = nameOfRoom;
		//	IsAvailable = isavailble;
		//	_Capacity = capacity;



		//}
    }
}
