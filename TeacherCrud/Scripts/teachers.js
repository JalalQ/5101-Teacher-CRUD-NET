// AJAX for teacher Add can go in here!
// This file is connected to the project via Shared/_Layout.cshtml


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


	//Client Side Validation to ensure that the user doesn’t accidentally submit a form with missing information
	if (teacherfname == "" || teacherfname == null) {
		alert("Enter First Name.");
	}

	else if (teacherlname == "" || teacherlname == null) {
		alert("Enter Last Name.");
	}

	else if (employeenumber == "" || employeenumber == null) {
		alert("Enter Employee Number.");
	}

	else if (hiredate == "" || hiredate == null) {
		alert("Enter Hire Date.");
	}

	else if (salary == "" || salary == null) {
		alert("Enter Salary.");
	}

	else {

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

		alert("Successfully Submitted !");

	}

}

