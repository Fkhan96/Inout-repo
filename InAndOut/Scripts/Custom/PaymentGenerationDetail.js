﻿function Trigger() {
    DateFilter();
    getPayment();

    $('#paymentdetailrange').daterangepicker({
        opens: 'right'
    }, function (start, end, label) {
        console.log("A new date selection was made: " + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD'));
        $(document).trigger('datefilterevent');
    });

    $(document).on('datefilterevent', function () {
        getPayment();
    });

    $('#paymentdetailrange').on('showCalendar.daterangepicker', function (ev, picker, start, end) {
        var startDate = picker.startDate.format('MM/DD/YYYY');
        var endDate = picker.endDate.format('MM/DD/YYYY');
        $('#paymentdetailrange').val(startDate + ' - ' + endDate);
    });
}

var data = [];
var oTable;

function DateFilter() {
    var currentDate = moment().format("MM/DD/YYYY");
    var lastMonthDate = moment().subtract(31, "days").format("MM/DD/YYYY");

    $('#paymentdetailrange').val(lastMonthDate + ' - ' + currentDate);
}

function getPayment() { 
    var startDate = $('#paymentdetailrange').val().split('-')[0].trim();
    var endDate = $('#paymentdetailrange').val().split('-')[1].trim();
    var empId = getParameterByName("id");
    var Parameters = {
        EmpId : empId,
        startDate: startDate,
        endDate: endDate
    };

    var myurl = "/PaymentGeneration/GetPaymentDetails";
    var mydata = new Object();
    mydata = Parameters;

    XHRGETRequest(myurl, mydata, function (result) {
        data = result.Data;
        if (oTable) { oTable.fnDestroy() }
        $('#listPaymentEmptb tbody').html('');
        if (!data || !data.length) { return; }
        for (var i = 0; i < data.length; i++) {
            var item = data[i];

            var tr = $('<tr></tr>');
            $(tr).attr('data-id', item.AttDetailsID);
            $(tr).append('<td>' + (i + 1) + '</td>');
            $(tr).append('<td>' + item.Name + '</td>');
            $(tr).append('<td>' + moment(item.CheckinTime).format('DD-MM-YYYY hh:mm A') + '</td>');
            $(tr).append('<td>' + moment(item.CheckoutTime).format('DD-MM-YYYY hh:mm A') + '</td>');
            $(tr).append('<td>' + getEnumName(TimeOff, item.TimeOff) + '</td>');
            $(tr).append('<td>' + item.Status + '</td>');
            $('#listPaymentEmptb tbody').append(tr);
        }
        oTable = $('#listPaymentEmptb').dataTable({ "destroy": true });
    });
}

function GeneratePaySlip() {
    var startDate = $('#paymentdetailrange').val().split('-')[0].trim();
    var endDate = $('#paymentdetailrange').val().split('-')[1].trim();
    var empId = getParameterByName("id");
    var Parameters = {
        EmpId: empId,
        startDate: startDate,
        endDate: endDate
    };

    var myurl = "/PaymentGeneration/GeneratePaySlip";
    var oReq = new XMLHttpRequest();
    // The Endpoint of your server 

    var URLToPDF = myurl + "?" + "EmpId=" + Parameters.EmpId + "&startDate=" + Parameters.startDate + "&endDate=" + Parameters.endDate;

    // Configure XMLHttpRequest
    oReq.open("GET", URLToPDF, true);

    // Important to use the blob response type
    oReq.responseType = "blob";

    // When the file request finishes
    // Is up to you, the configuration for error events etc.
    oReq.onload = function () {
        // Once the file is downloaded, open a new window with the PDF
        // Remember to allow the POP-UPS in your browser
        var file = new Blob([oReq.response], {
            type: 'application/pdf'
        });

        // Generate file download directly in the browser !
        saveAs(file, Parameters.EmpId + "_PaySlip.pdf");
    };

    oReq.send();
}
