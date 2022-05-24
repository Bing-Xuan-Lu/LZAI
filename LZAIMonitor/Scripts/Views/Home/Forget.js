function ForGet() {
    if ($('#ForgetAccount').val() == '') {
       
        swal.fire({
            title: '請輸入帳號!',
            icon: 'error'
        });
        return;
    }
    else if ($('#Email').val() == '') {
        swal.fire({
            title: '請輸入信箱!',
            icon: "error"
        });
       
        return;
    }
    else {
        $.ajax({
            type: "POST",
            url: UrlForGet,
            dataType: "Json",
            cache: false,
            data: $('#DocForm').serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                if (data.Status) {
                    MessageShowDialog(data, "訊息");
                    $('#divEdit').dialog('close');
                }
                else {
                    MessageShowDialog(data, "訊息");
                }
            }
            , error: function (xhr, status, message) {
                if (xhr.status === "500") {
                    MessageShowDialog(xhr.responseText, "訊息");
                }
            }
        });
    }
}

function Code() {
    $("#imgCode").attr("src", VCode);
}


function Sweet2forgetDialog(title, urlAction, Ifwidth, Ifheight) {
 

    swal.fire({
        title: title,
        type:"question",
        width: Ifwidth,
        height: Ifheight,
        allowOutsideClick: false,//外部點擊
        allowEscapeKey: false,//esc

        //html: "<iframe  width=" + Ifwidth + "  height= " + Ifheight + "   frameborder=no border=0 marginwidth=0 marginheight=0  src=" + urlAction + "></iframe>",
        html: "<iframe  width=100% height=" + Ifheight + "   frameborder=no border=0 marginwidth=0 marginheight=0  src=" + urlAction + "></iframe>",
        showCancelButton: false,//顯示取消按鈕
        showConfirmButton: false,//不顯示確認按鈕
        cancelButtonText: "取消", //顯示cancel中文名稱
        confirmButtonText: "確認",//顯示confirm中文名稱

      
    })
}