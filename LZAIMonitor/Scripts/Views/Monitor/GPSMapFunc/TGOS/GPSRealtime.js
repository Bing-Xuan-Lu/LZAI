import { GPSIcon } from '../../TrackCarIcon.js'
import { ClearHistory } from './GPSHistory.js'
import { DateConvert, TimeConvert, ReplaceNull } from '../../../../Common/DateUtility.js'

//即時軌跡
function NowTrajectory(row, TGmap) {
    ClearHistory(TGmap);
    var option = {
        icon: GPSIcon.CarIcon('Not', parseFloat(row.Heading), parseInt(row.IO1), parseInt(row.Speed))
        , IsSetZoom: false
    }
    var realTimePoint = new Point({ x: row.WGS_LON, y: row.WGS_LAT }, TGmap);
    realTimePoint.ShowMarker(option);
    var StrContent = '';
    StrContent += '<table width="400" style="text-align:center;font-size: 17px;margin-left: 9px;height: 100%;">';
    StrContent += '<tr class="InfoWindowTrTitle" ><td colspan="2" height="35px" style="padding: 5px;"><a href="#" target="_blank" style="color: white;">' + row.CleHandPlate_no + '</a></td></tr>';
    StrContent += '<tr class="InfoWindowTrContent"><td style="padding: 5px;">時間:</td><td style="padding: 5px 10px;">' + row.DateTime + '</td></tr>';
    StrContent += '<tr class="InfoWindowTrContent"><td style="padding: 5px;">車速:</td><td style="padding: 5px 10px;">' + row.Speed + '</td></tr>';
    StrContent += '<tr class="InfoWindowTrContent"><td style="padding: 5px;">衛星數:</td><td style="padding: 5px 10px;">' + row.Sat + '</td></tr>';
    StrContent += '<tr class="InfoWindowTrContent"><td style="padding: 5px;">車頭方向:</td><td style="padding: 5px 10px;">' + row.Heading + '</td></tr>';
    StrContent += '<tr class="InfoWindowTrContent"><td style="padding: 5px;">經緯度:</td><td style="padding: 5px 10px;"><a href="https://www.google.com/maps?q=loc:' + row.WGS_LAT + ',' + row.WGS_LON + '&z=15" target="_blank">' + row.WGS_LON + "&nbsp; &nbsp;" + row.WGS_LAT + '</a></td></tr>';
    //StrContent += '<tr><td colspan="2" data-plate_no="' + row.Plate_no + '"  style="height:50px;"></td></tr>';
    StrContent += '</table>';
    realTimePoint.SetInfoWindow(StrContent);
    realTimePoint.AddEvent('click', function () {
        realTimePoint.OpenInfoWindow();
    });
    realTimePoint.OpenInfoWindow();
}

export { NowTrajectory };