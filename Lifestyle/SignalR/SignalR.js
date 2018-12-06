function displayMessage(message) {
    var text = message

    new Noty({
        text: text,
        theme: 'relax', // or semanticui
        layouts: 'bottomRight',
        type: 'notification', // success, error, warning, information, notification
    }).show()
}

var myHub = $.connection.notificationsHub;
myHub.client.sendNotification = function (message) {
    displayMessage(message)
};

$.connection.hub.start().done(function () {
    console.log("IT WORKED!")
    $('#productsAddButton').click(function () {
        if (document.querySelectorAll("input[name=custom]")[0].value == 'true') {
            var nameProduct = $("#CustomProducts_Id option:selected").text();
        }
        else {
            var nameProduct = $("#DefaultProducts_Id option:selected").text();
        }
        console.log(nameProduct)
        myHub.server.sendNotification(nameProduct);
        $('#message').val('').focus();
    })
})