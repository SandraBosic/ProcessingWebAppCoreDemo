// Write your JavaScript code.
$(document).ready(function () {
    $('#pr-menu').BootSideMenu({
        // 'left' or 'right'
        side: "left",
        // animation speed
        duration: 500,
        // restore last menu status on page refresh
        remember: true,
        // auto close
        autoClose: false,
        // push the whole page
        pushBody: true,
        // close on click
        closeOnClick: false,
        // width
        width: "15%",
        // icons
        icons: {
            left: 'fa fa-chevron-left',
            right: 'fa fa-chevron-right',
            down: 'fa fa-chevron-down'
        }
    });

    $('#BatchFilterFromDate').datepicker({
        todayHighlight: true,
        container: '#BatchFilterFromDateContainer',
        format: "dd.mm.yyyy"
    });

    $('#BatchFilterToDate').datepicker({
        todayHighlight: true,
        container: '#BatchFilterToDateContainer',
        format: "dd.mm.yyyy"
    });

    $('#FilterTextSearch').keypress(function (e) {
        if (e.which === 13) {
            SubmitForm('#FilterForm');
            return false;
        }
    });
});

function SubmitForm(formId) {
    $(formId).submit();
}

function SubmitFilter(pageNumber) {
    $("#PageNumber").val(pageNumber);
    SubmitForm('#FilterForm');
}

function GetBatchMetadata(batchId) {
    var rowDisplayStatus = $("#pr-batch-metadata-row-" + batchId).css("display");
    if (rowDisplayStatus === "none") {
        $("#pr-batch-metadata-row-" + batchId).toggle();
        $.ajax({
            type: 'GET',
            url: 'RecurringBatch/GetBatchMetadata',
            data: { batchId: batchId },
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $("#pr-batch-metadata-container-" + batchId).html(data);
            }
        });
    } else {
        $("#pr-batch-metadata-row-" + batchId).toggle();
    }
}

function GetPage(pageNumber, url) {
    $.ajax({
        type: 'GET',
        url: url,
        data: { pageNumber: pageNumber },
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $("#pr-table-container").html(data);
        }
    });
}

function DownloadExport(url, downloadUrl) {
    $.ajax({
        url: url,
        dataType: 'json',
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            window.location = downloadUrl + '?key=' + data;
        },
        error: function (error) {
            alert(error);
        }
    });
}

function GetFullCardnumber(transactionId, virtualAccountNumber, isNew) {
    var isFullCardVisible = "0";
    if (isNew) {
        isFullCardVisible = $("#pr-new-cardnumber-for-transaction-" + transactionId).attr("FullCardVisible");
    } else {
        isFullCardVisible = $("#pr-cardnumber-for-transaction-" + transactionId).attr("FullCardVisible");
    }
    console.log(isFullCardVisible);
    if (isFullCardVisible === "1") {
        $.ajax({
            type: 'GET',
            url: 'RecurringBatchTransaction/GetSixPlusFourCardnumber',
            data: { virtualAccountNumber: virtualAccountNumber },
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (isNew) {
                    $("#pr-new-cardnumber-for-transaction-" + transactionId).html(data);
                    $("#pr-new-cardnumber-for-transaction-" + transactionId).attr("FullCardVisible", 0);
                } else {
                    $("#pr-cardnumber-for-transaction-" + transactionId).html(data);
                    $("#pr-cardnumber-for-transaction-" + transactionId).attr("FullCardVisible", 0);
                }
            }
        });
    } else {
        $.ajax({
            type: 'GET',
            url: 'RecurringBatchTransaction/GetFullCardNumber',
            data: { virtualAccountNumber: virtualAccountNumber },
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (isNew) {
                    $("#pr-new-cardnumber-for-transaction-" + transactionId).html(data);
                    $("#pr-new-cardnumber-for-transaction-" + transactionId).attr("FullCardVisible", 1);
                } else {
                    $("#pr-cardnumber-for-transaction-" + transactionId).html(data);
                    $("#pr-cardnumber-for-transaction-" + transactionId).attr("FullCardVisible", 1);
                }
            }
        });
    }
}
