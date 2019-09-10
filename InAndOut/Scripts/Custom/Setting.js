function Trigger() {
    getSetting();
}

function bindDateTime() {
    $('.clockpicker').clockpicker({
        donetext: 'Done', 'default': 'now'
    });
}
function checkBoxClick(target) {
    if (target.checked) {
        switch (target.name) {
            case 'Morning':
                $('#morning-start-time')[0].classList.remove('hidden');
                $('#morning-end-time')[0].classList.remove('hidden')
                break;
            case 'Afternoon':
                $('#afternoon-start-time')[0].classList.remove('hidden');
                $('#afternoon-end-time')[0].classList.remove('hidden');
                break;
            case 'Evening':
                $('#evening-start-time')[0].classList.remove('hidden');
                $('#evening-end-time')[0].classList.remove('hidden');
                break;
            case 'Night':
                $('#night-start-time')[0].classList.remove('hidden');
                $('#night-end-time')[0].classList.remove('hidden');
                break;
        }
    }
    else {
        switch (target.name) {
            case 'Morning':
                $('#morning-start-time')[0].classList.add('hidden');
                $('#morning-end-time')[0].classList.add('hidden');
                break;
            case 'Afternoon':
                $('#afternoon-start-time')[0].classList.add('hidden');
                $('#afternoon-end-time')[0].classList.add('hidden');
                break;
            case 'Evening':
                $('#evening-start-time')[0].classList.add('hidden');
                $('#evening-end-time')[0].classList.add('hidden');
                break;
            case 'Night':
                $('#night-start-time')[0].classList.add('hidden');
                $('#night-end-time')[0].classList.add('hidden');
                break;
        }
    }

}
function getSetting() {
    var myurl = "/Setting/GetList";
    var myData = {};
    myData["FK_CompanyID"] = $('#companyId').attr('data-companyId');
    XHRGETRequest(myurl, myData, function (result) {
        if (result.length > 0) {
            result.forEach(function (each) {
                var checkElement = 'input[name="' + each.u.Shift.ShiftName + '"]';
                var target = {
                    checked: each.u.IsSet,
                    name: each.u.Shift.ShiftName
                }
                checkBoxClick(target);
                if (each.u.IsSet) {
                    $(checkElement).iCheck('check');
                    $('input[name=' + each.u.Shift.ShiftName.toLowerCase() + '_start_time')[0].value = (each.u.StartTime != null) ? each.u.StartTime : '';
                    $('input[name=' + each.u.Shift.ShiftName.toLowerCase() + '_end_time')[0].value = (each.u.EndTime != null) ? each.u.EndTime : '';

                }
                else {
                    $(checkElement).iCheck('uncheck');
                }
            });
        }
      
        if (result.length > 0) {
            $('input[name="NoOfDays"]').val(result[0].x.NoOfDays);
            $('input[name="NoOfHalfDays"]').val(result[0].x.NoOfHalfDays);
        }

    });
}



function isRecordAlreadyExists(tableName, columnName, columnValue, ignorecondition) {
    var mydata = {};
    var exists;
    mydata["table"] = tableName;
    mydata["column"] = columnName;
    mydata["value"] = columnValue;
    mydata["ignorecondition"] = ignorecondition;
    var myurl = "/Common/isRecordAlreadExist";
    XHRGETRequest(myurl, mydata, function (result) {
        exists = result.status;
    });
    return exists;
}

function UpdateSetting(e) {
    var parent = $('#updateSettingdiv');
    var FK_CompanyID = e.id;
    var _id = 0;

    var isRecordExists = isRecordAlreadyExists("CompanyShift", "FK_CompanyID", FK_CompanyID, "FK_ShiftID in (1,2,3,4)", function (data) {
        return data
    });
    var data = {};
    var shifts = {
        "Morning": 1,
        "Afternoon": 2,
        "Evening": 3,
        "Night": 4
    }
    var callback = function () {
        var ShiftSettingDTO = [];

        $('[name]', parent).each(function () {
            if (this.type == "checkbox") {
                if ($(this).prop('checked')) {
                    if (shifts[this.name] != undefined) {
                        var shiftSetting = {};
                        shiftSetting['ShiftType'] = this.name;
                        shiftSetting['StartTime'] = $('input[name=' + this.name.toLowerCase() + '_start_time')[0].value;
                        shiftSetting['EndTime'] = $('input[name=' + this.name.toLowerCase() + '_end_time')[0].value;
                        shiftSetting['IsSet'] = true;
                        ShiftSettingDTO.push(shiftSetting);
                    }
                }
                else {
                    if (shifts[this.name] != undefined) {
                        var shiftSetting = {};
                        shiftSetting['ShiftType'] = this.name;
                        shiftSetting['StartTime'] = "";
                        shiftSetting['EndTime'] = "";
                        shiftSetting['IsSet'] = false;
                        //data[this.name] = false;
                        ShiftSettingDTO.push(shiftSetting);

                    }
                }
            }

        });
        data["FK_CompanyID"] = FK_CompanyID;
        data["shiftSetting"] = ShiftSettingDTO;
        var myurl = "/Setting/Add";
        if (isRecordExists) {
            myurl = "/Setting/Edit";
        }
        ShowAjaxLoader();
        XHRPOSTRequest(myurl, data, function (result) {
            HideAjaxLoader();
            showNotification("Update Successfully", "success");
        });
    }
    callback();
    return false;
}

function UpdateSalarySetting(e) {
    var parent = $('#updateSalarySetting');
    var FK_CompanyID = e.id;
    var _id = 0;
    var isRecordExists = isRecordAlreadyExists("SalaryDeduction", "FK_CompanyID", FK_CompanyID, "SalaryDeductionID!=" + _id, function (data) {
        return data
    });
    var data = {};
    var callback = function () {
        $('[name]', parent).each(function () {
            data[$(this).attr('name')] = $(this).val() || $(this).data('val');
        });

        data["FK_CompanyID"] = FK_CompanyID;

        var myurl = "/Setting/AddSalary";
        if (isRecordExists) {
            myurl = "/Setting/EditSalary";
        }
        ShowAjaxLoader();
        XHRPOSTRequest(myurl, data, function (result) {
            HideAjaxLoader();
            showNotification("Update Successfully", "success");
        });
    }
    callback();
    return false;
}


