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

		/// <summary>
		/// A helper method to create a TimeSlot object from a given date, start time, and end time,
		/// formatting the times in the `yyyy-MM-dd HH:mm` format. This method also accounts for bookings
		/// that span past midnight by adding a day to the end time if the start time is after the end time.
		/// </summary>
		/// <param name="date">
		/// The date for the time slot.
		/// </param>
		/// <param name="startTime">
		/// The start time for the time slot.
		/// </param>
		/// <param name="endTime">
		/// The end time for the time slot.
		/// </param>
		/// <returns>
		/// A new TimeSlot object representing the specified date and times in the format `yyyy-MM-dd HH:mm`.
		/// </returns>
		public TimeSlot FormatTimeSlot(DateTime date, DateTime startTime, DateTime endTime)
		{
			DateTime formattedTimeStart = DateTime.Parse($"{date:yyyy-MM-dd} {startTime:HH:mm}");

            // Add a day to the end time if the start time is after the end time,
            // to account for bookings that span past midnight
            if (startTime > endTime)
            {
                date = date.AddDays(1);
            }

            DateTime formattedTimeEnd = DateTime.Parse($"{date:yyyy-MM-dd} {endTime:HH:mm}");
            return new TimeSlot(formattedTimeStart, formattedTimeEnd);
		}

		public override string ToString()
		{
			return $"{StartTime:HH:mm} - {EndTime:HH:mm}";
		}
	}
}
