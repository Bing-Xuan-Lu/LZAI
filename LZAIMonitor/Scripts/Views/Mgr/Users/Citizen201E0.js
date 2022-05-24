function Update() {
    var message = "";
    var result = true;
    var worps = document.getElementById("worp");
    var emailRule = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    var cellphone = /^09[0-9]{2}-[0-9]{6}$/;
    var phone = /\d{2}-\d{7,8}#?(\d+)?/g;
    var acct = /^[\d|a-zA-Z]+$/;

    if ($('#_CName').val() == "") {
        //MessageShowDialog("帳號格式錯誤！！英數不能超過6碼", "訊息");
        message += "請填寫姓名！！" + "\r\n";
        result = false;
    }
    if (!phone.test($('#Tel').val())) {
        message += "電話格式錯誤！！ 格式：03-3860569#000" + "\r\n";
        result = false;
        //MessageShowDialog("電話格式錯誤！！ 格式：03-3860569#000", "訊息");
    }
    if (!cellphone.test($('#PhoneNumber').val())) {
        //alert($('#mgrUsers_Gender').val());
        //MessageShowDialog("手機格式錯誤！！格式：0912-345678", "訊息");
        message += "手機格式錯誤！！格式：0912-345678" + "\r\n";
        result = false;
    }
    if (!emailRule.test($('#Email').val())) {
        //MessageShowDialog("Email格式錯誤！！", "訊息");
        message += "Email格式錯誤！" + "\r\n";
        result = false;
    }
    if ($('#PasswordExpired').val() == "") {
        message += "請選擇密碼期限！！" + "\r\n";
        result = false;
    }

    if (result) {
        $.ajax({
            type: "POST",
            url: UrlUpdate,
            dataType: "Json",
            cache: false,
            data: $('#UpdateDocForm').serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                if (data.Status) {

                    MessageShowDialog(data, "訊息");
                    $('#divEdit').dialog('close');
                }
                else {
                    MessageShowDialog(data.Message, "訊息");
                }
            }

            , error: function (xhr, status, message) {
                //alert(xhr);
                if (xhr.status == "500") {
                    MessageShowDialog(xhr.responseText, "訊息");
                }
            }


        });
    }
    else {
        alert(message);//輸出錯誤訊息
    }
   
}

function SendPwdMail(Key, Email,IDNO) {

    $.ajax({
        type: "POST",
        url: UrlSendMail,
        dataType: "Json",
        cache: false,
        data: {
            SerId: Key,
            Email: Email,
            IDNO: IDNO
        },
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            if (data.Status) {

                MessageShowDialog(data, "訊息");
                $('#divEdit').dialog('close');
            }
            else {
                MessageShowDialog(data.Message, "訊息");
            }
        }

        , error: function (xhr, status, message) {
            //alert(xhr);
            if (xhr.status == "500") {
                MessageShowDialog(xhr.responseText, "訊息");
            }
        }
    });
}