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

    });
}

function editEmployee(e) {
    var data = { empid: getParameterByName("id") };
    var parent = $('#empDetailModal');

    $('[name]', parent).each(function () {
        if (this.name == "UserPictureUrl") {
            data[$(this).attr('name')] = this.src.split(',')[1];
        }
        else if (this.name == "JoiningDate") {
            data[$(this).attr('name')] = moment($(this).val() || $(this).data('val'), 'DD/MM/YYYY').format('MM/DD/YYYY');
        }
        else {
            data[$(this).attr('name')] = $(this).val();
        }
    });

    var myurl = "/Employee/Edit";
    XHRPOSTRequest(myurl, data, function (result) {
        showNotification('Employee Detail is updated', 'success');
    });

}