$(document).ready(function () {
    $('#dialog-form').hide();
    //getAllMessagesEmail();
    //$('#ddlEamil').change(function () {
    //    getAllMessagesEmail();
    //});

    //$("#dvSort").hide();

   //SortTable = $('#tblSort').dataTable({
   //     "paging": false,
   //     "ordering": true,
   //     "info": true,
   //     "scrollY": "200px",
   //     "scrollCollapse": true,
   //     "paging": false,
   //     "jQueryUI": true,
   //     "columns": [
   //         { "title": "ID", "width": "8%" },
   //         { "title": "Email", "width": "9%" },
   //         { "title": "LHS", "width": "9%" },
   //         { "title": "LT", "width": "15%" },
   //         { "title": "LCC", "width": "12%" },
   //         { "title": "LV", "width": "10%" },
   //         { "title": "LO", "width": "10%" },
   //         { "title": "CDT" },
   //         { "title": "MDT", "width": "10%" }
   //     ]
   // });
});
$(function () {
   
});


function getAllMessages() {
    var tbl = $('#messagesTable');
    var ManagerEmail = $("#ManagerEmailFilter").val();
    var LimitHardOrSoft = $("#LimitHardOrSoftFilter").val();
    var LimitType = $("#LimitTypeFilter").val();
    var LimitCurrencyCode = $("#LimitCurrencyCodeFilter").val();
    $.ajax({
        cache: false,
        type: "GET",
        url: '/home/ApplyFilter/',
        data: { ManagerEmail: ManagerEmail, LimitHardOrSoft: LimitHardOrSoft, LimitType: LimitType, LimitCurrencyCode: LimitCurrencyCode },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
    }).success(function (result) {
        // alert(result);
        tbl.empty().append(result);

    }).error(function () {

    });
}

function getAllMessagesEmail() {
    debugger;
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "/home/Email/", /// <reference path="../../Webservices/HistoryService.asmx" />
        data: "{}",
        dataType: "json",
        success: function (data) {
            console.log(data);
           
            for (var i = 0; i < data.d.length; i++) {

                SortTable.fnAddData([
"<tbody><tr>" +
"<td>" + data.d[i].ManagerEmail, "</td></tr></tbody>"
                ]);
            } // End For
        },
        error: function (e) {
            console.log(e.responseText);
            alert("Error");
        }
    });
    debugger;
}

function Insert() {
 
    debugger;
    var Lcl_TradingLimits = new Object();
    Lcl_TradingLimits.ManagerEmail = $('#Email').val();
    Lcl_TradingLimits.LimitHardOrSoft = $('#HardwareorSoftware').val();
    Lcl_TradingLimits.LimitType = $("#LimitType").val();
    Lcl_TradingLimits.LimitCurrencyCode = $("#LimitCurrencyCode").val();
    Lcl_TradingLimits.LimitValue = $("#LimitValue").val();
    Lcl_TradingLimits.LimitOverride = $("#LimitOverride").val();
   
    //if (TimeFrom == "") {
    //    alert("Please Enter Time From");
    //    return;
    //}

    //if (TimeTo == "") {
    //    alert("Please Enter Time To");
    //    return;
    //}


    //if (Activity == "0") {
    //    alert("Please Select Activity");
    //    return;
    //}

    //if (SocialEmotion == "") {
    //    alert("Please enter Social Emotion");
    //    return;
    //}

    //if (GrossMotor == "") {
    //    alert("Please Enter Gross Motor");
    //    return;
    //}

    $.ajax({
        type: "POST",
        url: "/home/Insert/",
        data: "{Lcl_TradingLimits:" + JSON.stringify(Lcl_TradingLimits) + "}",
        contentType: "application/json; charset=utf-8",
        datatype: "jsondata",
        async: "true",
        success: function () {
            alert("Data have been saved");
        },
        error: function (response) {
            alert("Data not save");
            $('#dialog-form').hide();
        }
    }); debugger;
}

function bindRecordToEdit(that) {
    $("#btnSave").show();
    var Lcl_EmailValue = $(that).closest("tr").find("#EmailValue").val();
    var Lcl_LimitHardOrSoftValue = $(that).closest("tr").find("#LimitHardOrSoftValue").val();
    var Lcl_LimitTypeValue = $(that).closest("tr").find("#LimitTypeValue").val();
    var Lcl_LimitCurrencyCodeValue = $(that).closest("tr").find("#LimitCurrencyCodeValue").val();


        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "/home/Bind",
          
            data: "{Lcl_EmailValue:'" + Lcl_EmailValue + "',Lcl_LimitHardOrSoftValue:'" + Lcl_LimitHardOrSoftValue + "',Lcl_LimitTypeValue:'" + Lcl_LimitTypeValue + "',Lcl_LimitCurrencyCodeValue:'" + Lcl_LimitCurrencyCodeValue + "'}",
            dataType: "json",
            success: function (data) {
                $("#btnSave").hide();
                $("#btnUpdate").show();
                $('#dialog-form').fadeIn(1000);
                console.log(data);

                    $("#Email").val(data.ManagerEmail);
                    $("#editingEmail").val(data.ManagerEmail);
                    $("#HardwareorSoftware").val(data.LimitHardOrSoft);
                    $("#editingLimitHardOrSoft").val(data.LimitHardOrSoft);
                    $("#LimitType").val(data.LimitType);
                    $("#editingLimitType").val(data.LimitType);
                    $("#LimitCurrencyCode").val(data.LimitCurrencyCode);
                    $("#editingLimitCurrencyCode").val(data.LimitCurrencyCode);
                    $("#LimitValue").val(data.LimitValue);
                    $("#LimitOverride").val(data.LimitOverride);

            },
            error: function (e) {
                console.log(e.responseText);
            }
        });

        //var tr = $(that).closest("tr");
        //$("#uEmail").val("");
        //$("#uHardwareorSoftware").val("");
        //$("#uLimitType").val(LimitType);
        //$("#uLimitCurrencyCode").val("");
        //$("#uLimitValue").val("");
        //$("#uLimitOverride").val("");

        //$('#Update-form').fadeIn(1000);
        //var ManagerEmail = $('#EmailValue', tr).val();
        //var LimitHardOrSoft = $('#LimitHardOrSoftValue', tr).val();
        //var LimitType = $("#LimitTypeValue", tr).val();
        //var LimitCurrencyCode = $("#LimitCurrencyCodeValue", tr).val();
        //var LimitValue = $("#LimitValueValue", tr).val();
        //var LimitOverride = $("#LimitOverrideValue", tr).val();

        //$("#uEmail").val(ManagerEmail);
        //$("#editingEmail").val(ManagerEmail);
        //$("#uHardwareorSoftware").val(LimitHardOrSoft);
        //$("#uLimitType").val(LimitType);
        //$("#uLimitCurrencyCode").val(LimitCurrencyCode);
        //$("#uLimitValue").val(LimitValue);
        //$("#uLimitOverride").val(LimitOverride);
    }

function Update() {
 
    var Lcl_TradingLimits = new Object();
    Lcl_TradingLimits.ManagerEmail = $('#Email').val();
    Lcl_TradingLimits.LimitHardOrSoft = $('#HardwareorSoftware').val();
    Lcl_TradingLimits.LimitType = $("#LimitType").val();
    Lcl_TradingLimits.LimitCurrencyCode = $("#LimitCurrencyCode").val();
    Lcl_TradingLimits.LimitValue = $("#LimitValue").val();
    Lcl_TradingLimits.LimitOverride = $("#LimitOverride").val();
    var editingEmail = $("#editingEmail").val();
    var editingLimitHardOrSoft = $("#editingLimitHardOrSoft").val();
    var editingLimitType = $("#editingLimitType").val();
    var editingLimitCurrencyCode = $("#editingLimitCurrencyCode").val();
    $.ajax({
        type: "POST",
        url: "/home/update/",
        data: "{Lcl_TradingLimits:" + JSON.stringify(Lcl_TradingLimits) + ", 'editingEmail': '" + editingEmail + "', 'editingLimitHardOrSoft': '" + editingLimitHardOrSoft + "', 'editingLimitType': '" + editingLimitType + "', 'editingLimitCurrencyCode': '" + editingLimitCurrencyCode + "'}",

        contentType: "application/json; charset=utf-8",
        datatype: "jsondata",
        async: "true",
        success: function (response) {
           
            alert("Data has been updated");
                  
            $('#dialog-form').hide();
        },
        error: function (response) {
            alert(response.status + ' ' + response.statusText);
            $('#dialog-form').hide();
        }
    });
}

function Delete(that) {
    debugger;
 
    //var Lcl_EmailValue = $('#EmailValue').val();
    var Lcl_EmailValue = $(that).closest("tr").find("#EmailValue").val();
    var Lcl_LimitHardOrSoft = $(that).closest("tr").find("#LimitHardOrSoftValue").val();
    var Lcl_LimitType = $(that).closest("tr").find("#LimitTypeValue").val();
    var Lcl_LimitCurrencyCode = $(that).closest("tr").find("#LimitCurrencyCodeValue").val();
    debugger;
    $.ajax({
        type: "POST",
        url: "/home/Delete/",
        data: "{Lcl_EmailValue:'" + Lcl_EmailValue + "',Lcl_LimitHardOrSoft:'" + Lcl_LimitHardOrSoft + "',Lcl_LimitType:'" + Lcl_LimitType + "',Lcl_LimitCurrencyCode:'" + Lcl_LimitCurrencyCode + "'}",
        contentType: "application/json; charset=utf-8",
        datatype: "jsondata",
        async: "true",
        success: function (response) {
            $('#dialog-form').hide();
        },
        error: function (response) {
            alert(response.status + ' ' + response.statusText);
            $('#dialog-form').hide();
        }
    });
}

//$(function () {
//    var dialog, form,

//      // From http://www.whatwg.org/specs/web-apps/current-work/multipage/states-of-the-type-attribute.html#e-mail-state-%28type=email%29
//      emailRegex = /^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/,
//      name = $("#name"),
//      email = $("#email"),
//      password = $("#password"),
//      allFields = $([]).add(name).add(email).add(password),
//      tips = $(".validateTips");

//    function updateTips(t) {
//        tips
//          .text(t)
//          .addClass("ui-state-highlight");
//        setTimeout(function () {
//            tips.removeClass("ui-state-highlight", 1500);
//        }, 500);
//    }


//    function checkRegexp(o, regexp, n) {
//        if (!(regexp.test(o.val()))) {
//            o.addClass("ui-state-error");
//            updateTips(n);
//            return false;
//        } else {
//            return true;
//        }
//    }    

//    dialog = $("#dialog-form").dialog({
//        autoOpen: false,
//        height: 530,
//        width: 400,
//        modal: true,
//        buttons: {
//            "Create New": Insert,
//    Cancel: function () {
//                dialog.dialog("close");
//            }
//        },
//        close: function () {
//            form[0].reset();
//            allFields.removeClass("ui-state-error");
//        }
//    });

//    form = dialog.find("form").on("submit", function (event) {
//        event.preventDefault();
//        Insert();
//        dialog.dialog("close");
//    });

//    $("#create-user").button().on("click", function () {
//        $('#Update-form').hide();
//        dialog.dialog("open");
//    });
//});


function GetEmail() {

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Home/Email/",
        data: "{}",
        dataType: "json",
        success: function (data) {
            console.log(data);
            $('#ddlEamil').find("option").remove();
            $('#ddlEamil').append('<option value="0">' + ".....Search Email Wise....." + '</option>');
            $('#ddlEamil').append('<option value=' + data.ManagerEmail + '>' + data.ManagerEmail + '</option>');
                    },
        error: function (e) {
            console.log(e.responseText);
        }
    });
}

function CreateNewfad() {
    Clear();
    $("#dialog-form").fadeIn(1000);
    $("#btnSave").show();
    $("#btnUpdate").hide();
}

function bindSelects() {
    var ManagerEmail = $("#ManagerEmailFilter");
    $(ManagerEmail).append($("<option/>").text("Select email...").val(""));
    $.get("/home/getSelect", { col: "ManagerEmail" }, function (data) {
        $.each(data, function (i, el) {
            $(ManagerEmail).append($("<option/>").text(el).val(el));
        });
    });
    var LimitHardOrSoft = $("#LimitHardOrSoftFilter");
    $(LimitHardOrSoft).append($("<option/>").text("Select Limit Hard Or Soft...").val(""));
    $.get("/home/getSelect", { col: "LimitHardOrSoft" }, function (data) {
        $.each(data, function (i, el) {
            $(LimitHardOrSoft).append($("<option/>").text(el).val(el));
        });
    });
    var LimitType = $("#LimitTypeFilter");
    $(LimitType).append($("<option/>").text("Select Limit Type...").val(""));
    $.get("/home/getSelect", { col: "LimitType" }, function (data) {
        $.each(data, function (i, el) {
            $(LimitType).append($("<option/>").text(el).val(el));
        });
    });
    var LimitCurrencyCode = $("#LimitCurrencyCodeFilter");
    $(LimitCurrencyCode).append($("<option/>").text("Select Limit Currency Code...").val(""));
    $.get("/home/getSelect", { col: "LimitCurrencyCode" }, function (data) {
        $.each(data, function (i, el) {
            $(LimitCurrencyCode).append($("<option/>").text(el).val(el));
        });
    });
}
bindSelects();
$(".filterItem").change(ApplyFilter);

function ShowFilter() {
    $("#filterRow").toggleClass("hidden");
    if ($("#filterRow").hasClass("hidden"));
    //getAllMessages();
}


function ApplyFilter(e) {
    var tbl = $('#messagesTable');
    var ManagerEmail = $("#ManagerEmailFilter").val();
    var LimitHardOrSoft = $("#LimitHardOrSoftFilter").val();
    var LimitType = $("#LimitTypeFilter").val();
    var LimitCurrencyCode = $("#LimitCurrencyCodeFilter").val();
    $.ajax({
        cache: false,
        type: "GET",
        url: '/home/ApplyFilter/',
        data: { ManagerEmail: ManagerEmail, LimitHardOrSoft: LimitHardOrSoft, LimitType: LimitType, LimitCurrencyCode: LimitCurrencyCode },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
    }).success(function (result) {
        // alert(result);
        tbl.empty().append(result);

    }).error(function () {

    });
}

function Clear() {
    $("#Email").val("");
    $("#editingEmail").val("");
    $("#HardwareorSoftware").val("");
    $("#editingLimitHardOrSoft").val("");
    $("#LimitType").val("");
    $("#editingLimitType").val("");
    $("#LimitCurrencyCode").val("");
    $("#editingLimitCurrencyCode").val("");
    $("#LimitValue").val("");
    $("#LimitOverride").val("");
}

function hidePanel()
{
    $("#dialog-form").fadeOut();
}
