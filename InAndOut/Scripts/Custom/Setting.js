function Trigger() {
    getSetting();
}

function getSetting() { 
    var myurl = "/Setting/GetList";
    var myData = {};
    myData["FK_CompanyID"] = $('#companyId').attr('data-companyId');
    XHRPOSTRequest(myurl, myData, function (result) {
        if (result.u.Morning) {
            $('input[name="morning"]').iCheck('check');
        }
        else {
            $('input[name="morning"]').iCheck('uncheck');
        }
        if (result.u.Afternoon) {
            $('input[name="afternoon"]').iCheck('check');
        }
        else {
            $('input[name="afternoon"]').iCheck('uncheck');
        }
        if (result.u.Evening) {
            $('input[name="evening"]').iCheck('check');
        }
        else {
            $('input[name="evening"]').iCheck('uncheck');
        }
        if (result.u.Night) {
            $('input[name="night"]').iCheck('check');
        }
        else {
            $('input[name="night"]').iCheck('uncheck');
        }

        $('input[name="NoOfDays"]').val(result.x.NoOfDays);
        $('input[name="NoOfHalfDays"]').val(result.x.NoOfHalfDays);
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
    var isRecordExists = isRecordAlreadyExists("Shift", "FK_CompanyID", FK_CompanyID, "ShiftID!=" + _id, function (data) {
        return data
    });
    var data = {};
    var callback = function () {
        $('[name]', parent).each(function () {
            if (this.type == "checkbox") {
                if ($(this).prop('checked')) {
                    data[this.name] = true;
                }
                else {
                    data[this.name] = false;
                }
            }
        });
        data["FK_CompanyID"] = FK_CompanyID;

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


