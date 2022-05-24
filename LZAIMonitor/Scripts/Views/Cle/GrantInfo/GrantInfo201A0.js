$(function () {
    Sweet2PartialdataTableSubmit("InsertDocForm", "btnCleGrantInfoInsert");
});

$(document).ready(function () {
   
    $('#CitySelect').select2({
        minimumInputLength: 0,
        placeholder: "請選擇縣市"
    });
    $('#DistrictSelect').select2({
        minimumInputLength: 0,
        placeholder: "請選擇行政區"
    });

    $("#CarSelect").select2({
        minimumInputLength: 0,
        placeholder: "請選擇車輛"
    });
   
})

$(function () {
    
    $('#CitySelect').change(function () {
        GetSelectedDistrictList($(this).val());
    });

});
function GetSelectedDistrictList(_CityId) {
    $.ajax({
        url: 'GetSelectedDistrictList',
        data: { CityId: _CityId },
        type: 'post',
        cache: false,
        async: false,
        dataType: 'json',
        success: function (data) {
            //debugger;
            if (data.length > 0) {
                $('#DistrictSelect').empty();
              
                $.each(data,
                    function (i, item) {
                        $('#DistrictSelect')
                            .append($('<option></option>').val(item.DistrictId).text(item.DistrictName));
                    });
                //清空選擇之選項
                $('#DistrictSelect').trigger('change');
            } else {
                $('#DistrictSelect').empty();
               
            }
        }
    });

}