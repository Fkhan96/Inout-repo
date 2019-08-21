var isvalid = true;
function Validate(list, callback, errorcallback) { 
    isvalid = true;
    var promisearry = [];
    for (var i = 0; i < list.length; i++) {
        var item = list[i];
        var p = new Promise((resolve, reject) => {
            var myurl = "/Common/isAlreadExist";
        var data = item;
        data.index = i;
        XHRPOSTRequest(myurl, data, function (result) {
            if (result.status) {
                isvalid = false;
                showNotification(list[result.index].error, "error");
            }
            resolve();
        });
    });
    promisearry.push(p);
}
Promise.all(promisearry).then(values => {
    if (isvalid) { callback(); }
else { if (errorcallback) errorcallback(); }
});
}