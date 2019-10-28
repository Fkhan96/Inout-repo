var data = [];

function Trigger() {
    getEmployee();

    $('.empupdate').on('input', function () {
        $("#btnupdateemp").attr("disabled", false);
    });

    $(".JoiningDate").datepicker({
        format: "dd/mm/yyyy",
        autoclose: true
    }).on("change", function () {
        $("#btnupdateemp").attr("disabled", false);
    });

    $('#btnupdateemp').on('click', function () {
        editEmployee(this);
    });

    $('[type=checkbox]').on('change', function () {
        $("#btnupdateemp").attr("disabled", false);
    });
}

function getEmployee() {
    var myurl = "/Employee/getDetails";
    var mydata = { empid: getParameterByName("id") };
    XHRGETRequest(myurl, mydata, function (result) {
        if (result.UserPictureUrl != null) {
            result.UserPictureUrl = "data:image/png;base64," + result.UserPictureUrl;
        }
        else {
            result.UserPictureUrl = "../images/dummy.jpg";
        }

        $('#user-pic').attr('src', result.UserPictureUrl);
        $('[name="SelfId"]').val(result.SelfId == null ? '----------' : result.SelfId);
        $('[name="Name"]').val(result.Name);
        $('[name="FatherName"]').val(result.FatherName);
        $('[name="Designation"]').val(result.Designation);
        $('[name="Salary"]').val(result.Salary);
        $('[name="JobType"]').val(result.JobType);
        $('[name="BloodGroup"]').val(result.BloodGroup);
        $('[name="Shift"]').val(result.Shift);
        $('[name="Address"]').val(result.Address);
        $('[name="LateDeduction"]').val(result.LateDeduction); 
        $('[name="JoiningDate"]').val(moment(result.JoiningDate).format('DD/MM/YYYY'));
        $('[name="ContactNumber"]').val(result.ContactNumber);
        $('[name="EmergencyContact"]').val(result.EmergencyContact);
        var empWorkingDays = result.WorkingDays.split(',');
        
        var parent = $('.workingdays');
        $('[name]', parent).each(function (index,value) {
            if (this.type == "checkbox") {
                this.checked = empWorkingDays.includes(this.value) ? true : false;
            }
        });
    });
}

function editEmployee(e) {
    var data = { empid: getParameterByName("id") };
    var workingDays = "";
    var parent = $('#empDetailModal');
    var days = {
        "Monday": 1,
        "Tuesday": 2,
        "Wednesday": 3,
        "Thursday": 4,
        "Friday": 5,
        "Saturday": 6,
        "Sunday": 7
    }

    $('[name]', parent).each(function () {
        if (this.name == "UserPictureUrl") {
            data[$(this).attr('name')] = this.src.split(',')[1];
        }
        else if (this.name == "JoiningDate") {
            data[$(this).attr('name')] = moment($(this).val() || $(this).data('val'), 'DD/MM/YYYY').format('MM/DD/YYYY');
        }
        else if (this.type == "checkbox") {
            if ($(this).prop('checked')) {
                if (days[this.name] != undefined) {
                    workingDays += this.value + ",";
                }
            }
        }
        else {
            data[$(this).attr('name')] = $(this).val();
        }
    });
    data["WorkingDays"] = workingDays.slice(0, -1);

    var myurl = "/Employee/Edit";
    XHRPOSTRequest(myurl, data, function (result) {
        showNotification('Employee Detail is updated', 'success');
    });

}