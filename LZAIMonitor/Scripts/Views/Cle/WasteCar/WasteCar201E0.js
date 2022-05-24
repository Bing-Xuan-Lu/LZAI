$(function () {
    Sweet2PartialdataTableSubmit("UpdateDocForm", "btnCleWasteCarUpdate");
    if (document.getElementById('CarMachine').checked) {
        if (document.getElementById('CarMachine').value == '1') {
            $('#Plate_No').val('');
            $('#Plate_No').attr("disabled", "disabled");
        }
    } 
});

$(document).on('change', 'input[type=radio][name=CarMachine]', function (event) {

    switch ($(this).val()) {
        case '1':
            $('#Plate_No').val('');
            $('#Plate_No').attr("disabled", "disabled");
            swal.fire('車機裝設位置', '車機裝設位置選擇頭車者不提供尾車選填');
            break;
        case '2':
            $("#Plate_No").removeAttr("disabled");
            break;
        case '3':
            $("#Plate_No").removeAttr("disabled");
            break;
    }
});


