// AJAX for teacher Add can go in here!
// This file is connected to the project via Shared/_Layout.cshtml

// Adding a Teacher
function AddTeacher() {

	//goal: send a request which looks like this:
	//POST : http://localhost:55900/api/TeacherData/AddTeacher/
	//with POST data of teachername, employeenumber etc.

	var URL = "http://localhost:55900/api/TeacherData/AddTeacher/";

	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?

	var teacherfname = document.getElementById('teacherfname').value;
	var teacherlname = document.getElementById('teacherlname').value;
	var employeenumber = document.getElementById('employeenumber').value;
	var hiredate = document.getElementById('hiredate').value;
	var salary = document.getElementById('salary').value;

	var FNameError = document.getElementById("FNameError");
	var LNameError = document.getElementById("LNameError");
	var NumberError = document.getElementById("NumberError");
	var DateError = document.getElementById("DateError");
	var SalaryError = document.getElementById("SalaryError");

	var infosubmit = document.getElementById("infosubmit");

	var validEntries = [];
	const totalEntries = 5;

	//initializaion of the array.
	for (var i = 0; i < totalEntries; i++) {
		validEntries[i] = false;
	}


	//Client Side Validation to ensure that the user doesn’t accidentally submit a form with missing information
	if (teacherfname == "" || teacherfname == null) {
		FNameError.innerHTML = "Enter First name.";
	}
	else {
		FNameError.innerHTML = "";
		validEntries[0] = true;
	}

	if (teacherlname == "" || teacherlname == null) {
		LNameError.innerHTML = "Enter Last Name.";
	}
	else {
		LNameError.innerHTML = "";
		validEntries[1] = true;
	}

	if (employeenumber == "" || employeenumber == null) {
		NumberError.innerHTML = "Enter Employee Number.";
	}
	else {
		NumberError.innerHTML = "";
		validEntries[2] = true;
	}

	if (hiredate == "" || hiredate == null) {
		DateError.innerHTML = "Enter Hire Date.";
	}
	else {
		DateError.innerHTML = "";
		validEntries[3] = true;
	}

	if (salary == "" || salary == null) {
		SalaryError.innerHTML = "Enter Salary.";
	}
	else {
		SalaryError.innerHTML = "";
		validEntries[4] = true;
	}

	var submitInfo = true;
	for (var i = 0; i < totalEntries; i++) {
		if (validEntries[i] === false) {
			submitInfo = false;
		}
	}

	if (submitInfo == true) {

		var TeacherData = {
			"teacherfname": teacherfname,
			"teacherlname": teacherlname,
			"employeenumber": employeenumber,
			"hiredate": hiredate,
			"salary": salary
		};


		rq.open("POST", URL, true);
		rq.setRequestHeader("Content-Type", "application/json");
		rq.onreadystatechange = function () {
			//ready state should be 4 AND status should be 200
			if (rq.readyState == 4 && rq.status == 200) {
				//request is successful and the request is finished

				//nothing to render, the method returns nothing.


			}

		}
		//POST information sent through the .send() method
		rq.send(JSON.stringify(TeacherData));

		infosubmit.innerHTML = "The information has been successfully submitted.";

	}

}

//updates a teacher using javascript and Ajax
function UpdateTeacher(TeacherId) {

	//goal: send a request which looks like this:
	//POST : http://localhost:55900/api/TeacherData/UpdateTeacher/{id}
	//with POST data of authorname, bio, email, etc.

	var URL = "http://localhost:55900/api/TeacherData/UpdateTeacher/"+TeacherId;

	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?

	var teacherfname = document.getElementById('teacherfname').value;
	var teacherlname = document.getElementById('teacherlname').value;
	var employeenumber = document.getElementById('employeenumber').value;
	var hiredate = document.getElementById('hiredate').value;
	var salary = document.getElementById('salary').value;

	var infoupdate = document.getElementById("infoupdate");

	var TeacherData = {
		"teacherfname": teacherfname,
		"teacherlname": teacherlname,
		"employeenumber": employeenumber,
		"hiredate": hiredate,
		"salary": salary
	};


	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.


		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));
	infoupdate.innerHTML = "The information has been successfully submitted.";
}
