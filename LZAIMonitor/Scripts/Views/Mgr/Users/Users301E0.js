$(function () {
    $('#mgrUsers_Account').attr("disabled", "disabled");
})



function Update() {
    var message = "";
    var result = true;
    var worps = document.getElementById("worp");
    var emailRule = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    var cellphone = /^09[0-9]{2}-[0-9]{6}$/;
    var phone = /\d{2}-\d{7,8}#?(\d+)?/g;
    var acct = /^[\d|a-zA-Z]+$/;


    if ($('#mgrUsers_UserName').val() == "") {
        //MessageShowDialog("帳號格式錯誤！！英數不能超過6碼", "訊息");
        message += "請填寫姓名！！" + "\r\n";
        result = false;
    }
 
 


    if ($('#mgrUsers_Tel').val() !== "" && !phone.test($('#mgrUsers_Tel').val())) {
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
    if ($('#mgrUsers_Email').val() !== "" && !emailRule.test($('#mgrUsers_Email').val())) {
        //MessageShowDialog("Email格式錯誤！！", "訊息");
        message += "Email格式錯誤！" + "\r\n";
        result = false;
    }
    if ($('#mgrUsers_Email').val() == "") {
        message += "請輸入Email！" + "\r\n";
        result = false;
    }


    if ($('#mgrUsers_Tel').val() === "" && $('#mgrUsers_PhoneNumber').val() === "") {
        message += "電話或手機，請擇一輸入！！" + "\r\n";
        result = false;
        //MessageShowDialog("請選擇性別！！", "訊息");
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

                    swal.fire({
                        title: '編輯成功!',

                        icon: 'success',
                    }).then(function (result) {
                        console.log(result);

                        window.parent.swal.close();
                        window.parent.$('#update').trigger('click');
                   
                    });
                }
                else {
                    swal.fire({
                        title: t + '失敗!',
                        html: data.Message,
                        icon: 'error'
                    });
                }
            }

            , error: function (xhr, status, message) {
                //alert(xhr);
                if (xhr.status == "500") {
                    swal.fire({
                        title: '訊息',
                        html: xhr.responseText,
                        icon: 'error'
                    });
                    
                }
            }


        });
    }
    else {
        alert(message);//輸出錯誤訊息
    }

}