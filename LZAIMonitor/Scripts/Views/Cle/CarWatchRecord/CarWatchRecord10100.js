$(document).ready(function () {

    $("#Fac_No").prepend(new Option('全部', -1, true));
    $("#Fac_No").val(-1).change();
    $("#Fac_No").select2({
        allowClear: true,
        minimumInputLength: 0,
        width: 250,
        placeholder: "請選擇清除機構管編"
    });

    $("#Fac_Name").prepend(new Option('全部', -1, true));
    $("#Fac_Name").val(-1).change();
    $("#Fac_Name").select2({
        allowClear: true,
        minimumInputLength: 0,
        width: 250,
        placeholder: "請選擇清除機構名稱"
    });

    $("#CARNum").prepend(new Option('全部', -1, true));
    $("#CARNum").val(-1).change();
    $("#CARNum").select2({
        allowClear: true,
        minimumInputLength: 0,
        width: 250,
        placeholder: "請選擇車號"
    });
    $('#RegionId').prepend(new Option('全部', -1, true));
    $("#RegionId").val(-1).change();
    $("#RegionId").select2({
        allowClear: true,
        minimumInputLength: 0,
        width: 250,
        placeholder: "請選擇警示區"
    });
    $("#SDate").datepicker({ dateFormat: 'yy-mm-dd' });
    $("#EDate").datepicker({ dateFormat: 'yy-mm-dd' });
})

function dateTypeToyyyyMMdd(data, type, row, meta) {
    if (data != null) {
        var date = new Date(parseInt(data.substr(6)));
        var dateStr = date.getFullYear() + "/" + ("00" + (date.getMonth() + 1)).slice(-2) + "/" + ("00" + date.getDate()).slice(-2) + " " + ("00" + date.getHours()).slice(-2) + ":" + ("00" + date.getMinutes()).slice(-2) + ":" + ("00" + date.getSeconds()).slice(-2);
        return dateStr;
    } else {
        return "";
    }
}

function operateFormatter(data, type, row, meta) {
    var result = '無軌跡查看';
    if (row.IsGPSHistory) {
        var result =
            [
                //`<input type="button" value="查看軌跡" class="btn btn-info"/>`
                `<a  class="btn btn-info" onclick="sweet2load()" href="${UrlMonitor}?EventId=${EncodeParam(row.EventId)}" target="_blank">查看軌跡</a>`
            ].join('');
    }
    return result;
}

function EncodeParam(data) {
    data = window.btoa(window.encodeURIComponent(data));
    return data;
}

function CarImgFormatter(data, type, row, meta) {

    var PicCarImg = row.CarImg;
    var result =
        [
            `<div id="imagebox"><img id="Pid" onerror="this.src='/images/NoImage.jpg'"    style=" width: auto; height: auto;  max-width: 100%;max-height: 100%;border:black 0px solid"></div>`,
            `<div  onmouseover="displayImg('${PicCarImg}')" onmouseout="vanishImg()"><img src="/images/Car/Y/Not/car_l.png" onclick="sweetimg('${row.CHIsSetGPS}','${row.InsTime}','${row.Head_No}','${row.Plate_No}','${row.CarImg}','${row.Fac_No}','${row.Fac_Name}','${row.RegionName}')"></div>`

        ].join('');

    return result;
}

function sweetimg(CHIsSetGPS, InsTime, Head_No, Plate_No, CarImg, Fac_No, Fac_Name, RegionName) {

    var time = dateTypeToyyyyMMdd(InsTime);
    if (RegionName == 'null') { RegionName = '無' }
    swal.fire({
        title: '車輛資訊',
        html: `<div style="text-align: center"><div class="Data-Title">車牌號碼:<br/>車牌辨識時間:<br/>清除機構管編:<br/>清除機構名稱:<br/>進入警示區:<br/></div><div class= "Data-Items">${Head_No} / ${Plate_No}<br/>${time}<br/>${Fac_No}<br/> ${Fac_Name}<br/>${RegionName}<br/></div></div>`,
        width: 500,
        showCancelButton: false,//顯示取消按鈕
        showConfirmButton: false,

        showCloseButton: true,//顯示右上角x
        imageUrl: CarImg,
    })
}


//圖片出現
function displayImg(pic) {
    var imgbox = document.getElementById("imagebox");
    var Pid = document.getElementById("Pid");
    var x = event.clientX + document.body.scrollLeft + 20;
    var y = event.clientY + document.body.scrollTop - 150;
    var Pic = pic;
    imgbox.style.left = x + "px";
    imgbox.style.top = y + "px";
    imgbox.style.display = "block";
    imgbox.style.border = "black 1px solid";
    Pid.src = Pic;
}

//圖片消失
function vanishImg() {
    var imgbox = document.getElementById("imagebox");
    imgbox.style.display = "none";
}

//不寫SweetDialog的iframe抓不到;刷新table用
//var parentQueryTable;
//$(function () {
//    parentQueryTable = queryTable; 

//})

$(function () {
    $('<div id="update"> </div>').click(() => {
        $("#queryTable").DataTable().draw(false);
        // tableDraw(queryTable, 'QueryForm', _queryTableConfig);
    }).appendTo($('body'))
})

function Download() {
    var _form = $("#QueryForm");
    $.ajax({
        type: "POST",
        url: UrlDownload,
        cache: false,
        data: _form.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            if (data.Status) {
                var UrlName = data.FileDownload[0].FileName;
                Swal.fire({
                    title: '檔案下載?',
                    text: '是否下載' + UrlName,
                    icon: 'question',
                    showCancelButton: true,//顯示取消按鈕
                    showConfirmButton: true,//不顯示確認按鈕
                    cancelButtonText: "取消", //顯示cancel中文名稱
                    confirmButtonText: "確認",//顯示confirm中文名稱
                }).then((result) => {

                    if (result.isConfirmed) {
                        window.location.href = data.FileDownload[0].FileUrl;
                    }

                })
            } else {
                swal.fire({
                    title: '下載失敗!',
                    text: data.Message,
                    icon: 'error'
                });
            }
        },
        error: function (xhrInstance, status, xhrException) {
            swal.fire({
                title: '請再次查詢!',
                text: xhrInstance,
                icon: 'error'
            });
        }
    });
}
