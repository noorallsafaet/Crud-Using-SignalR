
$(function () {
    // Declare a proxy to reference the hub.
    var notifications = $.connection.messagesHub;

    //debugger;
    // Create a function that the hub can call to broadcast messages.
    notifications.client.updateMessages = function () {
        getAllMessages();
        $(".notification-div").show().delay(15000).fadeOut();

    };
    // Start the connection.
    $.connection.hub.start().done(function () {

        getAllMessages();
    }).fail(function (e) {
        alert(e);
    });
});


function getAllMessages() {
    var tbl = $('#messagesTable');
    var UserID = 'admin';
    $.ajax({
      
        type: "GET",
        url: '/home/GetMessages/',
        //data: JSON.stringify({ "UserID": "abc"}),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
    }).success(function (result)
    {
       // alert(result);
        tbl.empty().append(result);

    }).error(function () {

    });
}

function bindRecordToEdit() {

    debugger;
   var Lcl_Phone = $('#Hidden1').val();

   $.ajax({

       url: "WebService/CustomerService.asmx?op=CustomerList",
       contentType: 'application/html ; charset:utf-8',
       type: 'GET',
       dataType: 'html',           
        data: "{Lcl_Phone:'" + Lcl_Phone + "'}",
        dataType: "json",
        success: function (data) {
            console.log(data);
            debugger;
            for (var i = 0; i < data.d.length; i++) {

                $("#Text1").val(data.d[i].Lcl_Phone);
                //$("#NewChildFirstName").val(data.d[i].FirstName);
                //$("#NewChildLastName").val(data.d[i].LastName);
                //$("#NewChildDOB").val(data.d[i].BirthDay);
                //$("#NewChildGender").val(data.d[i].Gender);
                //$("#ddlNewLocation").val(data.d[i].Location);
                //$("#NewChildClass").val(data.d[i].ClassRoom);
                //var Image = data.d[i].Photo;
                //$('#imgChild').attr('src', "Images/" + Image);
              
            } // End For
        },
        error: function (e) {
            console.log(e.responseText);
        }
    });
}