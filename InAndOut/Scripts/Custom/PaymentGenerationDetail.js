function Trigger() {
    getList();
}

var oTable;

function getList() { debugger
    var myUrl = "/PaymentGeneration/getAttDetailPayment";
    var mydata = { empid: getParameterByName("id") };
    XHRGETRequest(myUrl, mydata, function (result) {
        data = result;
        if (oTable) { oTable.fnDestroy() }
        $('#listPaymentEmptb tbody').html('');
        if (!result || !result.length) { return; }
        for (var i = 0; i < result.length; i++) {
            var item = result[i];

            var tr = $('<tr></tr>');
            $(tr).attr('data-id', item.AttendanceId);
            $(tr).append('<td>' + (i + 1) + '</td>');
            $(tr).append('<td>' + item.EmpName + '</td>');
            $(tr).append('<td>' + moment(item.CheckinTime).format('DD-MM-YYYY hh:mm A') + '</td>');
            $(tr).append('<td>' + moment(item.CheckoutTime).format('DD-MM-YYYY hh:mm A') + '</td>');
            $(tr).append('<td>' + getEnumName(TimeOff, item.TimeOff) + '</td>');
            $(tr).append('<td>' + item.Status + '</td>');
            $('#listPaymentEmptb tbody').append(tr);
        }
        oTable = $('#listPaymentEmptb').dataTable({ "destroy": true });
    });
}
