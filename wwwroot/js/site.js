// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Listens for changes on any input element with a `data-post-on` attribute
// and submits the form when the specified event occurs.
const FormInputs = document.querySelectorAll("[data-post-on]");
FormInputs.forEach(input => {
	input.addEventListener(input.dataset.postOn, () => {
		input.form.submit();
	});
});
