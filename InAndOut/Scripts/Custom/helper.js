function IsOverlappingMinMax(min,max , minNew,maxNew) {
    if ((min <= minNew && max >= minNew) ||
        (max>=minNew && max<=maxNew) ||
        (min<=minNew && max>=maxNew)||
        (maxNew>=min && maxNew<=max)
        )
    {
        return true;
    }
    return false;
}
function replaceAll(str, find, replace) {
    return str.replace(new RegExp(find, 'g'), replace);
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
  if(charCode == 189)
    {
        return false;
    }
   else if (charCode == 189|| charCode == 46 || ( charCode > 31 && (charCode < 48 || charCode > 57)))
    {
        return false;
    }
   
    else if (charCode == 45 )
    {
        if (evt.currentTarget.id == "TxtTo")
        {
            $("#TxtTo").val(' '); return false;
        }
        else if (evt.currentTarget.id == "TxtFrom")
        {
            $("#TxtFrom").val(' '); return false;
        }
        else if (evt.currentTarget.id == "Txtquantitymin") {
            $("#Txtquantitymin").val(' '); return false;
        }
        else if (evt.currentTarget.id == "Txtquantitymax") {
            $("#Txtquantitymax").val(' '); return false;
        }
        else if (evt.currentTarget.id == "TxtFrom") {
            $("#TxtFrom").val(' '); return false;
        }
        else if (evt.currentTarget.id == "Txtmax") {
            $("#Txtmax").val(' '); return false;
        }
        else if (evt.currentTarget.id == "Txtmin") {
            $("#Txtmin").val(' '); return false;
        }
    }
    return true;
}


function isNumberKey2(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    var val = evt.target.value;
    var lastcode=$(evt.target).attr("data-last-code");
    $(evt.target).attr("data-last-code", charCode);
    var hasDecimal = (val * 1 % 1 != 0);
    if (lastcode == 46 || lastcode == 44) {
        hasDecimal = true;
    }

    if (charCode == 45) {
        return false;
    }
    if ((hasDecimal && (charCode == 46 || charCode == 44)) && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }   
    
    return true;
}

function NotAllowDecimalAndNegative(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode == 189) {
        return false;
    }
    else if (charCode == 189 || charCode == 46 || (charCode > 31 && (charCode < 48 || charCode > 57))) {
        return false;
    }

}


function AllowDecimalAndNegative(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    var val = evt.target.value;
    var lastcode = $(evt.target).attr("data-last-code");
    $(evt.target).attr("data-last-code", charCode);
    var hasDecimal = (val * 1 % 1 != 0);
    if (lastcode == 46 || lastcode == 44) {
        hasDecimal = true;
    }

 
    if ((hasDecimal && (charCode == 46 || charCode == 44)) && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }

    return true;
}

function discountrange(evt) {
    var value = evt.target.value * 1;
  //  var dis = $("#TxtDiscount").val();
    var dis = evt.target.value;
    if (value >=100 )
    {
        //   $("#TxtDiscount").val(dis.slice(0, 2));
       
        $("#"+evt.target.id).val(dis.slice(0, 2));
        return true;

    }
    else   if (  value <=-100)
    {
        $("#" + evt.target.id).val(dis.slice(0, 3));
        return true;


    }
        return false;
}
function uiBlock(msg, el, loaderOnTop) {
    lastBlockedUI = el;
    if (msg == '' || msg == null || msg == "") {
        msg = "LOADING";
    }
    if (el) {
        jQuery(el).block({
            message: '<div class="loading-message loading-message-boxed"><img src="Images/loading-spinner-grey.gif" align="absmiddle"><span>&nbsp;&nbsp;' + msg + '...</span></div>',
            baseZ: 2000,
            css: {
                border: 'none',
                padding: '2px',
                backgroundColor: 'none',
            },
            overlayCSS: {
                backgroundColor: '#000',
                opacity: 0.05,
                cursor: 'wait'
            }
        });
    } else {
        if (msg == '' || msg == null || msg == "") {
            msg = "LOADING";
        }
        $.blockUI({
            message: '<div class="loading-message loading-message-boxed"><img src="Images/loading-spinner-grey.gif" align="absmiddle"><span>&nbsp;&nbsp;' + msg + '...</span></div>',
            baseZ: 2000,
            css: {
                border: 'none',
                padding: '2px',
                backgroundColor: 'none'
            },
            overlayCSS: {
                backgroundColor: '#000',
                opacity: 0.05,
                cursor: 'wait'
            }
        });
    }
}

function uiUnBlock(msg, el) {
    if (msg == '' || msg == null || msg == "") {
        msg = "LOADING";
    }
    if (el) {
        jQuery(el).unblock({
            onUnblock: function () {
                jQuery(el).removeAttr("style");
            }
        });
    }
    else {
      //  $.unblockUI();
    }
}

function showConfirm(_text, _success, _cancel) {

    notyfy({
        layout: 'center',
        text: _text,
        modal: true,
        buttons: [
          {
              addClass: 'btn btn-primary', text: 'OK', onClick: function ($noty) {
                  _success();
                  $noty.close();
              }
          },
          {
              addClass: 'btn btn',
              text: 'Cancel',
              onClick: function ($noty) {
                  if (_cancel) {
                      _cancel();
                  }
                  $noty.close();
              }
          }
        ]
    });
    //notyfy(
    //{
    //    layout: 'top',
    //    text: text,
    //    type: 'confirm',
    //    dismissQueue: true,
    //    buttons: [{
    //        addClass: 'btn btn-success btn-small glyphicons btn-icon ok_2',
    //        text: '<i></i> Ok',
    //        onClick: function ($notyfy) {
    //            $notyfy.close();
    //            success();
    //        }
    //    }, {
    //        addClass: 'btn btn-danger btn-small glyphicons btn-icon remove_2',
    //        text: 'Cancel',
    //        onClick: function ($notyfy) {
    //            $notyfy.close();
    //        }
    //    }]
    //});
}

function showNotification(_message, _type) { 

    switch (_type) {
        case 'success':
            toastr.success(_message);
            break;
        case 'error':
            toastr.error(_message)
            break;
        case 'warning':
            toastr.warning(_message)
            break;
    }
}


function XHRPOSTRequestForFormData(_url, _data, _onsuccess) {
    return $.ajax({
        type: "POST",
        url: _url,
        processData: false,
        contentType: false,
        data: _data,
        dataType: "json",
        beforeSend: function () {
            // uiBlock();
        },
        success: _onsuccess,
        error: function (error) {
            //uiUnBlock();
            if (error.status == 403) {
                RedirectLogin();
            }
        }
    });
}

function XHRGETRequest(_url, _data, _onsuccess) {
    $.ajax({
        type: "GET",
        url: _url,
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: _data,
        beforeSend: function () {
            // uiBlock();
        },
        success: _onsuccess,
        error: function (error) { uiUnBlock(); }
    });
}

function XHRPOSTRequest(_url, _data, _onsuccess) { 
    return $.ajax({
        type: "POST",
        url: _url,
        data: _data,
        dataType: "json",
        beforeSend: function () {
            // uiBlock();
        },
        success: _onsuccess,
        error: function (error) {
            //uiUnBlock();
            if (error.status == 403) {
                RedirectLogin();
            }
        }
    });
}

function XHRPOSTRequestAsync(_url, _data, _onsuccess) {
    $.ajax({
        type: "POST",
        url: _url,
        data: _data,
        dataType: "json",
        success: _onsuccess,
        error: function (error) {

            //  uiUnBlock();
            if (error.status == 403) {
                RedirectLogin();
               
            }

        }
    });
}

function RedirectLogin() {
    swal({
        title: "Session Expire",
        text: "Need to Re-Login",
        type: "error",
        showCancelButton: false,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Ok",
        closeOnConfirm: false
    }, function () {

        window.location.href = "/Account/Index";
        // swal("Deleted!", "Your imaginary file has been deleted.", "success");
    });
}
var roles = {
    AdministrativeAssistants: "1",
    SalesPeople: "2",
    Customer: "3",
    PurchaseManager: "4",
    Scheduler: "5",
    Vendor: "6",
    SubContractor: "7",
    Account: "8",
    Manager: "9",
    Admin: "10"
}

var visitstatus = {
    Pending: "Pending",
    Incomplete: "Incomplete",
    Complete: "Complete"
}

var messages = {

    //Added
    SendEmail: "Email Send Successfully Successfully",
    OrderIncomplete: "Please fill items in Product List",
    OrderEmpty: "Please Add Items in Product List",
    OrderAdded: "",
    useradded: "adicional correctamente",
    indicatoradded: "Indicator Added Successfully",
    knowlegdeareaadded: "Knowlegde Area Added Successfully",
    visitadded: "Visit Added Successfully",
    visitfinalized: "Visit in now marked as Finalized",
    actionplanadedd: "Action Plan saved Successfully",
    passwordchange: "Password changed Successfully",
    trainingadded: "Training Added Successfully",

    //Update
    userupdate: "Actualizado correctamente",
    indicatorupdate: "Indicator Updated Successfully",
    knowlegdeareaupdate: "Knowlegde Area Updated Successfully",
    visitupdate: "Visit Updated Successfully",
    trainingupdate: "Training Updated Successfully",

    //Delete
    userdelete: "eliminado correctamente",
    indicatordelete: "Indicator deleted Successfully",
    knowlegdeareadelete: "Knowlegde Area deleted Successfully",
    visitadelete: "Visit deleted Successfully",
    trainingdelete: "Training deleted Successfully",


    //Confirm
    roledeleteconfirm: "Are you sure you want to delete this role?",
    productdeleteconfirm: "Are you sure you want to delete this product?",
    categorydeleteconfirm: "Are you sure you want to delete this category?",
    budgetdeleteconfirm: "Are you sure you want to delete this budget?",
    departmentdeleteconfirm: "Are you sure you want to delete this department?",
    userdeleteconfirm: "Are you sure you want to delete this user?",
    indicatordeleteconfirm: "Are you sure you want to delete this indicator?",
    knowlegdeareadeleteconfirm: "Are you sure you want to delete this knowlegde area?",
    visitdeleteconfirm: "Are you sure you want to delete this visit?",
    visitfinalizeconfirm: "Did you answer all the indicators for each knowlegde area & you want to finalize this scan?",
    trainingdeleteconfirm: "Are you sure you want to delete this Training ?",


    //Alert
    budgetalreadydefine: "Your already define the budget of department agains this category.",
    emailexist: "The emailAddress is already exists. Please enter another.",
    startscan: "Do you want to start the scan now.?",
    passwordnotmatch: "Password did not match with your current password.",
    selectall: "You cannot mark any indicator blank. Please mark highlighted Indicators.",
    //answerall: "Please mark all Indicators in order to complete this Knowlegde Area.",
    answerall: "Please finalized knowledged areas",
    indicatorsunanswerederror: "Scan cannot be finalized because there are some indicators are left unanswered.",
    indicatorssignatureerror: "Scan cannot be finalized because system doesn't have the signature against this visit.",
    leavescan: "You are leaving screen without saving. Do you want to save changes ?",
    leavescan2: "If you left any unsaved data we will save it for you !"
};

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

Number.prototype.todigits = function () {
    var tem = '', z, d, s = this.toString(),
    x = s.match(/^(\d+)\.(\d+)[eE]([-+]?)(\d+)$/);
    if (x) {
        d = x[2];
        z = (x[3] == '-') ? x[4] - 1 : x[4] - d.length;
        while (z--) tem += '0';
        if (x[3] == '-') {
            return '0.' + tem + x[1] + d;
        }
        return x[1] + d + tem;
    }
    return s;
}
function getCurrentPageRole(actionname) {
    var url = location.pathname.split('/');
    for (var i = 0; i < roleacess.length; i++) {
        var item = roleacess[i];
        if (url[1].toLowerCase() == item.controller.toLowerCase() && actionname.indexOf(item.action.toLowerCase()) > -1 && item.ischecked) {
            return true;
        }
    }
    return false;
}
function getControllerPageRole(controller, actionname) {
    var url = location.pathname.split('/');
    for (var i = 0; i < roleacess.length; i++) {
        var item = roleacess[i];
        if (controller.toLowerCase() == item.controller.toLowerCase() && actionname.indexOf(item.action.toLowerCase()) > -1 && item.ischecked) {
            return true;
        }
    }
    return false;
}
function getDateFrom_DDMMYYY(strdate) {
    strdate = strdate.replace('-', '/');
    var dt1 = strdate.substring(0, 2);
    var mon1 = strdate.substring(3, 5);
    var yr1 = strdate.substring(6, 10);
    temp1 = mon1 + "/" + dt1 + "/" + yr1;
    return new Date(temp1);
}
function ChecValidityDate(from_datetxt, to_datetxt) {
    if ($(from_datetxt).val() != null && $(to_datetxt).val() != '') {
        var from_date = getDateFrom_DDMMYYY($(from_datetxt).val());
        var to_date = getDateFrom_DDMMYYY($(to_datetxt).val());
        if (to_date.getTime() < from_date.getTime()) {

            showNotification("To Date Will Not Be Less Then From Date", "error");
            $(to_datetxt).datepicker('setDate', '');

        }
    }
}
function UploadFile(file, callback, element) {

    var mydata = new FormData();
    mydata.append('file', file);
    $.ajax({
        type: "POST",
        url: "/Common/UploadFile",
        data: mydata,
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (response) {
            callback(response, element);
        },
        error: function (error) {
            console.log(JSON.stringify(error));
        }
    });
}
function RemoveFile(filename, callback) {

    var mydata = new FormData();
    mydata.append('filename', filename);
    $.ajax({
        type: "POST",
        url: "/Common/DeleteFile",
        data: mydata,
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (response) {
            callback(response);
        },
        error: function (error) {
            console.log(JSON.stringify(error));
        }
    });
}