/// <reference path="jquery-1.10.2.js" />


// Ajax data types..
var dataType = {
    applicationJson: 'application/json; charset=utf-8',
    json: 'json',
    text: 'text/plan'
};
var type = {
    get: 'GET',
    post: 'POST',
    put: 'PUT',
    delete: 'Delete'
};

// Execuation Status ...
var execuStatus = {
    successSave: 'Data has been save successfully',
    successEdit: 'Data has been updated successfully',
    successDelete: 'Data has been deleted successfully',
    fail: 'Something went wrong'
}
function clearDropdown(eletementId) {
    $('#' + eletementId + ' option:not(:first)').remove();
}

// Loading dropdown using ajax..
// All the Parameters are required accept contextKey
function LoadDropDown(url, type, elementId, contextKey) {
    if (contextKey === undefined && $.trim(contextKey) == '') {
        $.ajax({
            dataType: 'json',
            type: type,
            url: url,
            success: function (result) {
                console.log(result);
                $.each(result, function (index, item) {
                    console.log(result);
                    var option = "<option value='" + item.value + "'>" + item.text + "</option>";
                    elementId.append(option);
                });

            },
            error: function (err) {
                console.log(err);
            }
        });
    }
    else {
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: type,
            url: url,
            data: contextKey,
            success: function (result) {
                console.log(result);
                $.each(result, function (index, item) {
                    console.log(result);
                    var option = "<option value='" + item.value + "'>" + item.text + "</option>";
                    elementId.append(option);
                });
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
}

function LoadDropDown2(url, type, elementId, contextKey) {
    if (contextKey === undefined && $.trim(contextKey) == '') {
       $.ajax({
            dataType: 'json',
            type: type,
            url: url,
            success: function (result) {
                console.log(result);
                $.each(result, function (index, item) {
                    var option = "<option value='" + item.value + "'>" + item.text + "</option>";
                    elementId.append(option);
                })
                elementId.val('7');
                elementId.trigger('change');
            },
            error: function (err) {
                console.log(err);
            }
        })
    }
    else {
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: type,
            url: url,
            data: contextKey,
            success: function (result) {
                console.log(result);
            },
            error: function (err) {
                console.log(err);
            }
        })
    }
}


function callaBack() {
    $()
}

// Checking user existence
// All the Parameters are required 
function IsUserExist(username, id, isEdit) {
    var returnVal;
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '/Common/IsUserExist',
        async: false,
        data: JSON.stringify({ username: username, id: id, isEdit: isEdit }),
        success: function (result) {
            console.log(result);
            returnVal = result
        },
        error: function (err) {
            console.log(err);
        }
    })
    return returnVal;
}

// Checking user email existence
// All the Parameters are required 
function IsEmailExist(id, email, isEdit) {
    var returnVal;
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '/Common/IsEmailExist',
        async: false,
        data: JSON.stringify({ id: id, email: email, isEdit: isEdit }),
        success: function (result) {
            console.log(result);
            returnVal = result
        },
        error: function (err) {
            console.log(err);
        }
    })
    return returnVal;
}

// Parameterized Ajax Get Request
// All the Parameters are required 
function getReqWithData(dataType, type, url, data) {
    return $.ajax({
        dataType: dataType,
        type: type,
        url: url,
        data: data,
        success: function (result) {
            console.log(result);
        },
        error: function (err) {
            console.log(err);
        }
    })
}

// Parameterized Ajax POST Request
// All the Parameters are required 
function postReqWithData(contentType, dataType, type, url, data) {
    return $.ajax({
        contentType: contentType,
        dataType: dataType,
        type: type,
        url: url,
        data: data,
        success: function (result) {
            console.log(result);
            //alert(result);
        },
        error: function (err) {
            console.log(err);
        }
    })
}

// Parameterized Ajax POST Request
// All the Parameters are required 
function postReqWithToken(contentType, dataType, type, url, data, token) {
    return $.ajax({
        contentType: contentType,
        dataType: dataType,
        type: type,
        url: url,
        headers: token,
        data: data,
        success: function (result) {
            console.log(result);
            //alert(result);
        },
        error: function (err) {
            console.log(err);
        }
    })
}

function postReqWithFile(dataType,type,url,data,token) {
    return $.ajax({
        dataType: dataType,
        method: type,
        url: url,
        data: data,
        processData: false,
        contentType: false,
        headers: token,
        success: function (result) {
            console.log(result);
        },
        error: function (result) {
            console.log(result);
        }
    });
}

// when() function for save/edit
// All the Parameters are required
function fnSaveWhen(obj) {
    $.when(obj).then(function (res) {
        console.log(res);
        if (res == true) {
            alert(execuStatus.successSave);
            fnReloadPage(true);
        }
        else {
            alert(execuStatus.fail);
        }
    }).fail(function (err) {
        console.log(err);
    })
}

// when() function for delete
// All the Parameters are required
function fnDeleteWhen(obj, tblId, rowIndex) {
    $.when(obj).then(function (res) {
        console.log(res);
        if (res == true) {
            alert(execuStatus.successDelete);
            removeTableRow(tblId, rowIndex);
        }
        else {
            alert(execuStatus.fail);
        }
    }).fail(function (err) {
        console.log(err);
    })
}

// Reloading the current page
// All the Parameters are required [Boolean Parameter]
function fnReloadPage(isReload) {
    if (isReload) {
        location.reload();
    }
}

// Removing a row from the table with id
// All the Parameters are required 
function removeTableRow(tblId, rowIndex) {
    var tbl = tblId + ' tr';
    $(tbl).eq(rowIndex).css("background-color", "#ff6347").fadeOut(800,
        function () {
            $(tbl).eq(rowIndex).remove();
        });
}

function fnFixTheTbodyRowSerialInDecsOrder(tableId, rowIndex) {
    var tableLen = $(tableId + " tbody tr").length;
    if (tableLen !== 0) {
        var count = 1;
        $.each($(tableId + " tbody tr"), function (index, item) {
            if (rowIndex !== index) {
                $(this).find('td:eq(0)').text(tableLen - count);
                count++;
            }
        });
    }
}

// token
function getToken() {
    var headers = {};
    headers['__RequestVerificationToken'] = $('input[name=__RequestVerificationToken]').val();
    return headers;
}

// Reload the entrire child ui
function UIReloading(url) {
    $.ajax({
        url: url,
        success: function (data) {
            $("div#renderBody").empty();
            //var myNode = document.getElementById("renderBody");
            //while (myNode.firstChild) {
            //    myNode.removeChild(myNode.firstChild);
            //}
            //console.log(data);
            $("div#renderBody").html(data);
        }
    })
}

// Modal Black Screen Disappearing From UI
function modalBackdropRemove() {
    $('.modal-backdrop').remove();
}
// Navigation 
$(document).ready(function () {
    //$(document).on('click', 'a.navLink', function (e) {
    //    e.preventDefault();
    //    UIReloading($(this).attr('data-url'));
    //})
})

function ajaxBooleanChecker(data, url, token) {
    var returnVal = false;
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: url,
        async: false,
        data: data,
        headers: token,
        success: function (result) {
            console.log(result);
            returnVal = result;
        },
        error: function (err) {
            console.log(err);
        }
    })
    return returnVal;
}

// Changing Modal Heading And Button Text
function fnModalHeadChange(HeadText) {
    if ($.trim(HeadText) != '') {
        $("#modalHeading").empty();
        $("#modalHeading").text(HeadText);
        //console.log($("div.modal-header:contains('Update')"));
        var btnText = HeadText.indexOf("Update") > -1 ? 'Update' : 'Save';
        //console.log(btnText);
        $("#btnSubmit").empty();
        $("#btnSubmit").text(btnText);
    }
}

// Try ParseInt
function TryParseInt(str, defaultValue) {
    var retValue = defaultValue;
    if (str !== null) {
        if (str.length > 0) {
            if (!isNaN(str)) {
                retValue = parseInt(str);
            }
        }
    }
    return retValue;
}

// Try ParseFloat
function TryParseFloat(str, defaultValue) {
    var retValue = defaultValue;
    if (str !== null) {
        if (str.length > 0) {
            if (!isNaN(str)) {
                retValue = parseFloat(str);
            }
        }
    }
    return retValue;
}

// The month name with format [MM,MMM]
function getMonthName(monthNo, format) {
    var monthName = ''
    if (format == 'MM') {
        switch (monthNo) {
            case 1:
                monthName = 'Jan'
                break;
            case 2:
                monthName = 'Feb'
                break;
            case 3:
                monthName = 'Mar'
                break;
            case 4:
                monthName = 'Apr'
                break;
            case 5:
                monthName = 'May'
                break;
            case 6:
                monthName = 'Jun'
                break;
            case 7:
                monthName = 'Jul'
                break;
            case 8:
                monthName = 'Aug'
                break;
            case 9:
                monthName = 'Sep'
                break;
            case 10:
                monthName = 'Oct'
                break;
            case 11:
                monthName = 'Nov'
                break;
            case 12:
                monthName = 'Dec'
                break;
            default:
                monthName = ''
                break;
        }
    }
    else if (format == 'MMM')
    {
        switch (monthNo) {
            case 1:
                monthName = 'January'
                break;
            case 2:
                monthName = 'February'
                break;
            case 3:
                monthName = 'March'
                break;
            case 4:
                monthName = 'April'
                break;
            case 5:
                monthName = 'May'
                break;
            case 6:
                monthName = 'June'
                break;
            case 7:
                monthName = 'July'
                break;
            case 8:
                monthName = 'Augest'
                break;
            case 9:
                monthName = 'September'
                break;
            case 10:
                monthName = 'October'
                break;
            case 11:
                monthName = 'November'
                break;
            case 12:
                monthName = 'December'
                break;
            default:
                monthName = ''
                break;
        }
    }
    return monthName;
}

// Count the tbody trs of a table
function fntableRowCount(tableId,countElementId) {
    var rowCount = $("#" + tableId + " tbody tr").length;
    console.log("rowcount: "+ rowCount);
    if (rowCount == 1) {
        if (!($("#" + tableId +" tbody tr:first-child").hasClass("empty-row"))) {
            $("#" + countElementId).text(rowCount);
        }
        else {
            $("#" + countElementId).text('0');
        }
    }
    else {
        $("#" + countElementId).text(rowCount);
    }
}

// Remove a row from the table body with the table id and tbody tr row index.
function fnRemoveARowFromTbody(tableId, rowIndex) {
    if ($(tableId + " tbody tr").length > 0) {

        $(tableId + " tbody tr").eq(rowIndex).css("background-color", "#ff6347").fadeOut(800,
            function () {
                $(tableId + " tbody tr").eq(rowIndex).remove();
            });
    }
}

// Fix serial number of table tbody trs
function fnFixTheTbodyRowSerial(tableId, rowIndex) {
    $.each($(tableId + " tbody tr"), function (index, item) {
        if (rowIndex != index) {
            var td = $(this).find('td:eq(0)');
            var sumSL = (index > rowIndex) ? 0 : 1;
            $(this).find('td:eq(0)').text(index + sumSL);
        }
    })
}

// Search By Anything on the table
function fnSearchByAnything(searchElementId, tableId) {
    var value = $(searchElementId).val().toLowerCase();
    $("#" + tableId+" tbody tr").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
}