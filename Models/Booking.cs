namespace DSVMeetingRoomBooking.Models
{
	public class Booking
	{
		/// <summary>
		/// The unique identifier for the booking.
		/// </summary>
		public string Id { get; set; }

        /// <summary>
        /// The unique identifier for the employee
        /// </summary>
        public string EmployeeCPR { get; set; }

		
		/// <summary>
		/// Reference to the unique identifier of the room being booked.
		/// </summary>
		public int RoomId { get; set; }

		/// <summary>
		/// The time slot for which the booking is made, represented as a TimeSlot object
		/// containing the start and end times of the booking.
		/// </summary>
		public TimeSlot TimeSlot { get; set; }

		/// <summary>
		/// An optional comment or note associated with the booking, allowing users
		/// to provide additional information or context about the booking.
		/// </summary>
		public string? Comment { get; set; }

		/// <summary>
		/// Initializes a new instance of the Booking class with the specified room ID and time slot.
		/// </summary>
		/// <param name="roomId">
		/// The unique identifier referencing the room being booked, represented as an integer.
		/// </param>
		/// <param name="timeSlot">
		/// The time slot for which the booking is made, represented as a TimeSlot object
		/// containing the start and end times of the booking.
		/// </param>
		public Booking(int roomId, TimeSlot timeSlot)
		{
			// Generate a unique identifier for the booking and take the first 8 characters
			Id = Guid.NewGuid().ToString().Substring(0, 8);
			RoomId = roomId;
			TimeSlot = timeSlot;
		}

		/// <summary>
		/// Initializes a new instance of the Booking class with the specified room ID, time slot, and comment.
		/// </summary>
		/// <param name="roomId">
		/// The unique identifier referencing the room being booked, represented as an integer.
		/// </param>
		/// <param name="timeSlot">
		/// The time slot for which the booking is made, represented as a TimeSlot object
		/// containing the start and end times of the booking.
		/// </param>
		/// <param name="comment">
		/// An optional comment or note associated with the booking, allowing users
		/// to provide additional information or context about the booking.
		/// </param>
		public Booking(int roomId, TimeSlot timeSlot, string comment): this(roomId, timeSlot)
		{
			Comment = comment;
		}

		public override string ToString()
		{
			return $"Booking ID: {Id}, Room ID: {RoomId}, Time Slot: {TimeSlot}, Comment: {Comment}";
		}
	}
}
