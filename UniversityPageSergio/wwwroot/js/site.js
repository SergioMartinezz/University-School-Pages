// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    $("#cmbInstructor").change(function () {
        saludar();
    });
    $("#cmbCourse").change(function () {
        CheckInscribed();
    });
    $("#cmbCourse").change(function () {
        CheckCourse();
    });
    $("#cmbStudent").change(function () {
        studentChange();
    });
})

function CheckInscribed() {
    if ($("#cmbStudent option:selected").val() > 0 && $("#cmbCourse option:selected").val() > 0) {
        $.ajax({
            type: "GET",
            url: "/Enrollment/AddEnrollment?handler=CheckEnrollment",
            data: {
                idCourse: $("#cmbCourse option:selected").val(),
                idStudent: $("#cmbStudent option:selected").val()
            },
            datatype: "json",
            success: function (response) {
                if (response == false) {
                    alert("Sorry, this student is inscribed in this course. You can try with another one or another course.");
                    //$("#cmbStudent").val(0);
                    //$("#cmbCourse").val(0);
                    //$("#add").prop("disabled", true);
                    $("#seatCapacity").css("display", "none");
                    $("#instructor").css("display", "none");
                    $("#add").prop("disabled", true);
                    $("#cmbCourse").val(0);
                    $("#cmbStudent").val(0);
                }
            }
        })
    }
}

function CheckCourse() {
    var courseSelected = $("#cmbCourse option:selected").val();
    localStorage.setItem("idCourseSelected", courseSelected);

    if (courseSelected == 0) {
        $("#seatCapacity").css("display", "none");
        $("#instructor").css("display", "none");
        $("#add").prop("disabled", true);
    }
    else
    {
        $.ajax({
            type: "GET",
            url: "/Enrollment/AddEnrollment?handler=CourseBack",
            data: { id: courseSelected },
            datatype: "json",
            success: function (response) {
                if (response.seatCapacity <= 0) {
                    alert("So sorry, the course capacity are complete at this moment.");
                    $("#seatCapacity").css("display", "none");
                    $("#instructor").css("display", "none");
                    $("#add").prop("disabled", true);
                    $("#cmbCourse").val(0);
                } else {
                    $("#seatCapacity").css("display", "block");
                    $("#instructor").css("display", "block");
                    $("#add").prop("disabled", false);
                    $("#seatCapacity").text("The course capacity at this moment is: " + response.seatCapacity);
                    $("#instructor").text("The course instructor is: " + response.instructor.instructorName);

                    localStorage.setItem("capacity", response.seatCapacity)
                    localStorage.setItem("instructor", response.Instructor.InstructorName)
                }
            }
        });
        if ($("#cmbStudent option:selected").val() > 0) {
            $("#add").prop("disabled", false);
        }
    }
}

function studentChange() {
    var studentSelected = $("#cmbStudent option:selected").val();
    localStorage.setItem("idStudentSelected", studentSelected);

    if (studentSelected == 0) {
        $("#add").prop("disabled", true);
    }
    else
    {
        if ($("#cmbCourse option:selected").val() > 0) {
            $("#add").prop("disabled", false);
        }
    }
}

function saludar() {
    var nombre = "Sergio";
    $.ajax({
        type: "GET",
        url: "/Course/AddCourse?handler=RecibirNombre",
        data: { dato: nombre },
        datatype: "json",
        success: function (response) {
            alert(response);
        },
        error: function () {
            alert("Error");
        }
    })
}

