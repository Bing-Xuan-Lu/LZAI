//普通
function Autocomplete(textBoxId, url) {


    $('#' + textBoxId).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: url,
                type: "POST",
                dataType: "json",
                data: { data: $('#' + textBoxId).val() },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Value, value: item.Value, Code: item.Key };
                    }))
                }
            })
        }
       
    });


}
//test
function BankLinkAutocomplete(tbxBankNameId, tbxBankCodeId, tbxBranchNameId, tbxBranchCodeId,  UrlAutoBank, UrlAutoBranch) {

    $('#' + tbxBankNameId).autocomplete({
 
        source: function (request, response) {
            $.ajax({
                url: UrlAutoBank,
                type: "POST",
                dataType: "json",
                data: { data: $('#' + tbxBankNameId).val() },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Value, value: item.Value, Code: item.Key };
                    }))
                }
            })
        },
        change: function (e, ui) {

            if (!(ui.item)) {
                e.target.value = "";
                $('#' + tbxBankCodeId).val("");
                $('#' + tbxBranchCodeId).val("");
                $('#' + tbxBranchNameId).val("");
            }


        },
        select: function (e, ui) {
            $('#' + tbxBankCodeId).val(ui.item ? ui.item.Code : "");
  
            $('#' + tbxBranchNameId).val('');
            $('#' + tbxBranchNameId).attr("disabled", false);
        
        }

    });


    $('#' + tbxBranchNameId).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: UrlAutoBranch,
                type: "POST",
                dataType: "json",
                data: { BankCode: $('#' + tbxBankCodeId).val(), data: $('#' + tbxBranchNameId).val() },
                success: function (data) {
                    if (data.length === 0) {
                        $('#' + tbxBranchNameId).val("");
                        $('#' + tbxBankCodeId).val("");                     
                    } else {
                        response($.map(data, function (item) {

                            return { label: item.Value, value: item.Value, Code: item.Key };

                        }))
                    }
                }
            })
        },
        change: function (e, ui) {
            if (!(ui.item)) {
                e.target.value = "";
                $('#' + tbxBranchCodeId).val("");
            }
        },
        select: function (e, ui) {
            $('#' + tbxBranchCodeId).val(ui.item.Code);

        }

    });





}

//連動
function LinkAutocomplete(PrimaryKeyValue1, PrimaryKeyValue2, Url1, Url2, PrimaryKeyValue1Key , PrimaryKeyValue2Key) {

   
    $('#' + PrimaryKeyValue1).autocomplete({

        source: function (request, response) {
            $.ajax({
                url: Url1,
                type: "POST",
                dataType: "json",
                data: { Data: $('#' + PrimaryKeyValue1).val() },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Value, value: item.Value, Code: item.Key };
                    }))
                }
            })
        },
        change: function (e, ui) {

            if (!(ui.item)) {
                e.target.value = "";
                $('#' + PrimaryKeyValue2).val("");

                if (PrimaryKeyValue1Key != null || PrimaryKeyValue1Key != undefined) {
                    $('#' + PrimaryKeyValue1Key).val("");
                }
                if (PrimaryKeyValue2Key != null || PrimaryKeyValue2Key != undefined) {
                    $('#' + PrimaryKeyValue2Key).val("");
                }
            }


        },
        select: function (e, ui) {
            if (PrimaryKeyValue1Key != null || PrimaryKeyValue1Key != undefined) {
                $('#' + PrimaryKeyValue1Key).val(ui.item ? ui.item.Code : "");
            }
            $('#' + PrimaryKeyValue2).val('');
            $('#' + PrimaryKeyValue2).attr("disabled", false);

        }

    });


    $('#' + PrimaryKeyValue2).autocomplete({


        source: function (request, response) {
            $.ajax({
                url: Url2,
                type: "POST",
                dataType: "json",
                data: {
                    Data1Key: $('#' + PrimaryKeyValue1Key).val()
                    , Data: $('#' + PrimaryKeyValue2).val()
                },
                success: function (data) {
                    if (data.length === 0) {
                        $('#' + PrimaryKeyValue2).val("");
                        if (PrimaryKeyValue2Key != null || PrimaryKeyValue2Key != undefined) {
                            $('#' + PrimaryKeyValue2Key).val("");
                        }
                    } else {
                        response($.map(data, function (item) {

                            return { label: item.Value, value: item.Value, Code: item.Key };

                        }))
                    }
                }
            })
        },
        change: function (e, ui) {
            if (!(ui.item)) {
                e.target.value = "";
                if (PrimaryKeyValue2Key != null || PrimaryKeyValue2Key != undefined) {
                    $('#' + PrimaryKeyValue2Key).val("");
                }
            }
        },
        select: function (e, ui) {
            if (PrimaryKeyValue2Key != null || PrimaryKeyValue2Key != undefined) {
                $('#' + PrimaryKeyValue2Key).val("");
            }

        }

    });





}
