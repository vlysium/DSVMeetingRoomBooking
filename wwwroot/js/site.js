// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// `Index.cshtml` Booking time
const bookingTimePeriodForm = document.getElementById("booking-time-period-form");
const bookingTimePeriodFormInputs = bookingTimePeriodForm.querySelectorAll("#selected-day, #time-start, #time-end");

bookingTimePeriodFormInputs.forEach(input => {
	input.addEventListener("blur", () => {
		bookingTimePeriodForm.submit();
	});
});

