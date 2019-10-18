function Trigger() {
    //RegisterCompany();

 
}


function RegisterCompany(e) {
    var parent = $('#RegisterCompany');
    
    var data = {};
    $('[name]', parent).each(function () {
        data[$(this).attr('name')] = $(this).val() || $(this).data('val');
    });
    data['PackageType'] = $('input[name="PackageType"]:checked').val();

    var myurl = "/Account/Register"

    XHRPOSTRequest(myurl, data, function (result) {
        showNotification(data.Name + " Company Register Successfully" , "success");
    });
    return false;
}


