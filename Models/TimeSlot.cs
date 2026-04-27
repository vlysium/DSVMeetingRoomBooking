namespace DSVMeetingRoomBooking.Models
{
	public class TimeSlot
	{
		/// <summary>
		/// The start time of the time slot.
		/// </summary>
		public DateTime StartTime { get; set; }
		/// <summary>
		/// The end time of the time slot.
		/// </summary>
		public DateTime EndTime { get; set; }

		/// <summary>
		/// Initializes a new instance of the TimeSlot class with the specified start and end times.
		/// </summary>
		/// <param name="startTime">The start time of the time slot.</param>
		/// <param name="endTime">The end time of the time slot.</param>
		public TimeSlot(DateTime startTime, DateTime endTime)
		{
			StartTime = startTime;
			EndTime = endTime;
		}

		/// <summary>
		/// Calculates the duration of the time slot as a TimeSpan.
		/// </summary>
		/// <returns>
		/// A TimeSpan representing the duration of the time slot, calculated as the difference between EndTime and StartTime.
		/// </returns>
		public TimeSpan Duration()
		{
			return EndTime - StartTime;
		}

		public override string ToString()
		{
			return $"{StartTime:HH:mm} - {EndTime:HH:mm}";
		}
	}
}
