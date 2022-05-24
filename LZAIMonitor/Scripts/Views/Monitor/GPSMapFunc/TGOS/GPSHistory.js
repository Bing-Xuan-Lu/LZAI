import { GPSIcon } from '../../TrackCarIcon.js'
import { DateConvert, TimeConvert, ReplaceNull } from '../../../../Common/DateUtility.js'
//歷史軌跡
var Num = 0;
var HistoryPoint = null || [];//歷史軌跡點(畫線用)

//車輛歷史軌跡&停頓點
function HistoryTrajectory(gpsResponse, plate_no, TGmap) {
    ClearHistory(TGmap);
    let response = gpsResponse;
    HistoryPoint = [];
    if (response.length > 0) {
        $("#timePlay").show();
        Num = response.length;
        response.forEach((item, key) => {
            HistoryPoint.push(
                {
                    ...item
                    ,icon: ((key + 1) % 10 === 0) ? GPSIcon.GetArrowImg(parseFloat(item.Heading)) : ''
                    , x: item.WGS_LON
                    , y: item.WGS_LAT
                });
        });

        var multiPoint = new MultiPoint(HistoryPoint, TGmap);
        multiPoint.note = "歷史軌跡";
        multiPoint.ShowMultiPoint({
            width: 20,
            height: 20
            , PointWidth: 7
            , PointHeight: 7
            , IsDifferentIcon: true
        }, [{
            event: 'click',
            eventCallback: function (item, key) {
                multiPoint.SetInfoWindow(infoWindowStrContent(item, plate_no));
                multiPoint.OpenInfoWindow();
            }
        }]);
        var lineString = new Line(HistoryPoint, TGmap);
        var lineOption = {
            strokeColor: '#123456',
            strokeWeight: 3,
            strokeOpacity: 0.6,
            zIndex: -999
        };
        lineString.ShowLine(lineOption);

        $("#slider").slider({
            animate: "slow",
            range: "min",
            value: 1,
            min: 1,
            max: Num,
            change: function (event, ui) {
                HistoryMarker(response, ui.value, plate_no, TGmap);
                Change = ui.value;
            },
            slide: function (event, ui) {
            }
        });
        $("#slider").slider("value", 1);
    }
    else {
        $("#timePlay").hide();
        alert("查無歷史軌跡。");
    }
}

//滑條Function
function HistoryMarker(response, Seq, plate_no, TGmap) {
    if (response.length > 0) {
        $.each(response,
            function (key, item) {
                if ((key + 1) === Seq) {
                    TGmap.ClearPoint();
                    if (Seq === 1)
                        TGmap.SetCenter({ x: item.WGS_LON, y: item.WGS_LAT });
                    var option = {
                        icon: GPSIcon.CarIcon('Not', parseFloat(item.Heading), parseInt(item.IO1), parseInt(item.Speed))
                        , IsSetZoom: false
                    }
                    var realTimePoint = new Point({ x: item.WGS_LON, y: item.WGS_LAT }, TGmap);
                    realTimePoint.ShowMarker(option);
                    realTimePoint.SetInfoWindow(infoWindowStrContent(item, plate_no));
                    realTimePoint.AddEvent('click', function () {
                        realTimePoint.OpenInfoWindow();
                    });
                    realTimePoint.OpenInfoWindow();
                    TGmap.SetCenter({ x: item.WGS_LON, y: item.WGS_LAT });

                    var DateTime = item.DateTime.replace('T', ' ').replace('Z', '');
                    var tooltip =
                        '<div class="tooltip" style="margin-left:-7px;opacity: 1;margin-left: -7px;opacity: 1;padding: 5px;bottom: 120%;">';
                    tooltip += '<div class="tooltip-inner" style="width: 7em;">' +
                        DateTime  +
                        '</div>';
                    tooltip += '<div class="tooltip-arrow"></div></div>';

                    $('.ui-slider-handle').html(tooltip);
                }
            });
    }
}

//資訊視窗
function infoWindowStrContent(item, plate_no) {
    //資訊視窗
    var StrContent = '';
    var DateTime = item.DateTime.replace('T', ' ').replace('Z', '');
    StrContent += '<table width="400" style="text-align:center;font-size: 17px;margin-left: 9px;height: 100%;">';
    StrContent += '<tr class="InfoWindowTrTitle" ><td colspan="2" height="35px" style="padding: 5px;"><a href="#" target="_blank" style="color: white;">' + plate_no + '</a></td></tr>';
    StrContent += '<tr class="InfoWindowTrContent"><td style="padding: 5px;">時間:</td><td style="padding: 5px 10px;">' + DateTime + '</td></tr>';
    StrContent += '<tr class="InfoWindowTrContent"><td style="padding: 5px;">車速:</td><td style="padding: 5px 10px;">' + item.Speed + '</td></tr>';
    StrContent += '<tr class="InfoWindowTrContent"><td style="padding: 5px;">衛星數:</td><td style="padding: 5px 10px;">' + item.Sat + '</td></tr>';
    StrContent += '<tr class="InfoWindowTrContent"><td style="padding: 5px;">車頭方向:</td><td style="padding: 5px 10px;">' + item.Heading + '</td></tr>';
    StrContent += '<tr class="InfoWindowTrContent"><td style="padding: 5px;">經緯度:</td><td style="padding: 5px 10px;"><a href="https://www.google.com/maps?q=loc:' + item.WGS_LAT + ',' + item.WGS_LON + '&z=15" target="_blank">' + item.WGS_LON + "&nbsp; &nbsp;" + item.WGS_LAT + '</a></td></tr>';
    StrContent += '</table>';
    return StrContent;
}

var Change = 0;
var N = 1;
var timer = null;
//播放、暫停鍵
function TimeLine() {
    var now = $("#Control").attr("class");
    if (now.indexOf("play") !== -1) {
        $('#Control').attr('class', 'fa fa-pause');
        if (Change != 0)
            N = Change;
        timer = setInterval(function () {
            N++;
            $("#slider").slider("value", N);
            var Now = $('#slider').slider("option", "value");
            if (Now == Num) {
                $('#Control').attr('class', 'fa fa-play');
                clearInterval(timer);
                N = 0;
                Change = 0;
            }
        }, 500);
    }
    else {
        $('#Control').attr('class', 'fa fa-play');
        clearInterval(timer);
    }
}

//清除歷史軌跡相關圖層
function ClearHistory(TGmap) {
    $("#timePlay").hide();
    TGmap.ClearPoint();
    TGmap.ClearLabel();
    TGmap.ClearCircle();
    TGmap.ClearPolygon();
    TGmap.ClearLine();
    TGmap.ClearMultiPoint();
}

export { HistoryTrajectory, ClearHistory, TimeLine };