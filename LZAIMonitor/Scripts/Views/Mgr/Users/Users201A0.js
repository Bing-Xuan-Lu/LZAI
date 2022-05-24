$(function () {


    $('#DialogGPKI').dialog({
        title: '注意',
        autoOpen: false,
        width: 800,
        height: 600,
        show: "blind",
        resizable: false,
        draggable: false,
        modal: true,
        buttons: [
            {
                text: "OK",
                click: function () { $(this).dialog("close"); }
            }
        ]
    });

    //全選跟取消
    /*$('input[name="mgrUserGroup.GroupId"]').first().change(function() {
        if (this.checked) {
            $('input[name="mgrUserGroup.GroupId"]').prop('checked','true');
        } else {
            $('input[name="mgrUserGroup.GroupId"]').prop('checked', false);
        }
    });*/


    //先限定單選
    $('input[name="mgrUserGroup.GroupId"]').on('change', function () {
        $('input[name="mgrUserGroup.GroupId"]').not(this).prop('checked', false);
    });



    //有可以選的
    if ($('input[name="UnitType"]').val() != undefined) {
        //下拉是選單
        InitDownItem();
        //單選查詢 getlist
        $('input[name="UnitType"]').change(function () {
            var value = $('input[name="UnitType"]:checked').val();
            //alert(value);
            GetDownItem(value);
        });
    }
    
});

function InitDownItem() {
    //預設先勾選第一個
    $('input[name=UnitType]')[0].checked = true;
    var value = $('input[name="UnitType"]:checked').val();
    //alert(value);
    GetDownItem(value);
}
function GetDownItem(unitType) {
    //清除
    $('#mgrUsers_UnitId').empty();
    //取得數值並新增
    $.ajax({
        url: UrlUnitData,
        data: { unitType: unitType },
        type: 'post',
        cache: false,
        async: false,
        dataType: 'json',
        success: function (data) {
            ///Mgr/Users/UnitListForUser
            if (data.length > 0) {
                $('#mgrUsers.UnitId').empty();
                //$('#mgrUsers_UnitId').append($('<option></option>').val('').text('請選擇類型'));

                $(data).each(function (index, item) {
                    //$("#mgrUsers.UnitId").append($('<option></option>').val(item.Text).html(item.Value));
                    $('#mgrUsers_UnitId').append($('<option></option>').val(item.Value).text(item.Text));
                });
               
            }
        }
    });
}


function Insert() {
    var message = "";
    var result = true;
    var worps = document.getElementById("worp");
    var emailRule = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    var cellphone = /^09[0-9]{2}-[0-9]{6}$/;
    var phone = /\d{2}-\d{7,8}#?(\d+)?/g;
    var acct = /^[\d|a-zA-Z]+$/;

    if (!acct.test($('#mgrUsers_Account').val())) {
        //MessageShowDialog("帳號格式錯誤！！英數不能超過6碼", "訊息");
        message += "帳號格式錯誤！！英數不能超過10碼" + "\r\n";
        result = false;
    }
    if ($('#mgrUsers_UserName').val()=="" ) {
        //MessageShowDialog("帳號格式錯誤！！英數不能超過6碼", "訊息");
        message += "請填寫姓名！！" + "\r\n";
        result = false;
    }
    /*if (worps.value.length < 6) {
      //  MessageShowDialog("密碼必須6~8碼！！", "訊息");
        $('#worp').val('');
        $('#checkworp').val('');
        message += "密碼必須6~8碼！！" + "\r\n";
        result = false;
    }*/
    /*
     if ($('#worp').val() != $('#checkworp').val()) {

       // MessageShowDialog("密碼輸入不相同請檢查！！", "訊息");
        $('#worp').val('');
        $('#checkworp').val('');
         message += "密碼輸入不相同請檢查！！" + "\r\n";
        result = false;
    }   */
    if ($("#mgrUsers_UnitId").find(":selected").val() === "") {

        message += "請選擇單位！！" + "\r\n";
        result = false;
    }  
    if ($('#mgrUsers_Tel').val() !=="" && !phone.test($('#mgrUsers_Tel').val() ))
    {
         message += "電話格式錯誤！！ 格式：03-8069750#000" + "\r\n";
        result = false;
        //MessageShowDialog("電話格式錯誤！！ 格式：03-3860569#000", "訊息");
    }
    if ($('#mgrUsers_PhoneNumber').val() !== "" && !cellphone.test($('#mgrUsers_PhoneNumber').val())) {
        //alert($('#mgrUsers_Gender').val());
        //MessageShowDialog("手機格式錯誤！！格式：0912-345678", "訊息");
         message += "手機格式錯誤！！格式：0912-345678" + "\r\n";
        result = false;
    }

    if ($('#mgrUsers_Email').val() == "") {
        message += "請輸入Email！" + "\r\n";
        result = false;
    }

    if ($('#mgrUsers_Email').val() !== "" && !emailRule.test($('#mgrUsers_Email').val())) {
        //MessageShowDialog("Email格式錯誤！！", "訊息");
         message += "Email格式錯誤！" + "\r\n";
        result = false;
    }

    if ($('#mgrUsers_Tel').val() === "" && $('#mgrUsers_PhoneNumber').val() === "") {
        message += "電話或手機，請擇一輸入！！" + "\r\n";
        result = false;
        //MessageShowDialog("請選擇性別！！", "訊息");
    }
    /*
    if ($('input[name="mgrUsers.Gender"]:checked').val() == undefined)
    {
         message += "請選擇性別！！" + "\r\n";
        result = false;
        //MessageShowDialog("請選擇性別！！", "訊息");
    }*/
    if ($('input[name="mgrGroup.GroupId"]:checked').val() == undefined) {
        message += "請選擇勾選群組！！" + "\r\n";
        result = false;
        //MessageShowDialog("請選擇性別！！", "訊息");
    }

    if (result) {
        var checkedValue = $('input:checkbox[name="mgrUserGroup.GroupId"]:checked').map(function ()
        {
            return $(this).val();

         }).get().join(',');

        $("#checkValue").val(checkedValue);
        $.ajax({
            type: "POST",
            url: UrlInsert,
            dataType: "Json",
            cache: false,
            data: $('#InsertDocForm').serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                if (data.Status) {

                    swal.fire({
                        title: '新增成功!',
                        text: '已成功新增',
                        icon: 'success'
                    }).then(function (result) {
                        console.log(result);
                        window.parent.swal.close();
                    });
                }
                else {
                    swal.fire({
                        title: '新增失敗!',
                        text: data.Message,
                        icon: 'error'
                    })
                
                }
            }

            , error: function (xhr, status, message) {
                //alert(xhr);
                if (xhr.status == "500") {
                    
                    swal.fire({
                        title: '新增失敗!',
                        text: '500',
                        icon: 'error'
                    })
                }
            }


        });
    }
    else {
        swal.fire({
            title: '錯誤!',
            text: message,
            icon: 'warning'
        });
        
    }
}