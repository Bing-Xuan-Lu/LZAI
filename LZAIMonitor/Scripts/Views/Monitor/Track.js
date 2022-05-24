import { TrackHistory } from './TrackHistory.js'
import { TrackRealTime } from './TrackRealTime.js'
import { TrackExhibition } from './TrackExhibition.js'

$(function () {
    var TGMap = new Maps(option);
    console.log('main JS');
    MapIconFunc.Init();
    //滑鼠座標匯入
    pointEvent(TGMap);

    TrackRealTime(TGMap);
    TrackHistory(TGMap);
    TrackExhibition(TGMap);
});