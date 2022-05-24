//TGOS圖資依賴TGAgent_OL.js & json5.js & proj4.js

//TODO:TGOS官方範例，尚未重構
let olMap;

var appID = document.currentScript.getAttribute('data-id');
var apiKey = document.currentScript.getAttribute('data-key');

var key_ = new TGOS.TGKey(appID, apiKey);

var url_ = TGOS.getTileAgentPath(TGOS.TGMapTileId.TGOSMAP, TGOS.CoordSys.EPSG3857);
var tileUrl = url_ + '/GetCacheImage' +
    '?APPID=' + key_.getAppID() + '&APIKEY=' + key_.getApiKey() +
    '&L=0&S={z}&X={x}&Y={y}';

var extent_ = [];
var resolution_length_;

var option = {
    zoom: 11,
    minZoom: 7,
    maxZoom: 19,
    projection: "EPSG:3826",
    center: [334539.696942298, 2728397.22501146]
};

// 載完圖層設定後, 會呼叫 createMap 建立地圖
$(function() {
    loadLayerDef(url_ + '/GetCacheConfig?FORMAT=JSON');
});

// 建立地圖          
function createMap(tileGrid) {
    //OpenLayer使用Proj4強制使用TWD97語法
    ol.proj.setProj4(proj4);
    proj4.defs("EPSG:3825", "+proj=tmerc +lat_0=0 +lon_0=119 +k=0.9999 +x_0=250000 +y_0=0 +ellps=GRS80 +towgs84=0,0,0,0,0,0,0 +units=m +no_defs");
    proj4.defs("EPSG:3826", "+proj=tmerc +lat_0=0 +lon_0=121 +k=0.9999 +x_0=250000 +y_0=0 +ellps=GRS80 +towgs84=0,0,0,0,0,0,0 +units=m +no_defs");
    proj4.defs("EPSG:3827", "+proj=tmerc +lat_0=0 +lon_0=119 +k=0.9999 +x_0=250000 +y_0=0 +ellps=aust_SA +units=m +no_defs");
    proj4.defs("EPSG:3828", "+proj=tmerc +lat_0=0 +lon_0=121 +k=0.9999 +x_0=250000 +y_0=0 +ellps=aust_SA +units=m +no_defs");
    var proj3826 = ol.proj.get('EPSG:3826');


    olMap = new ol.Map({
        // 這裡是宣告圖層陣列，範例加入的是 TGOS 圖磚圖層。
        layers: [
            new ol.layer.Tile({
                source: new ol.source.OSM(),
                opacity: 1
            }),
            new ol.layer.Tile({
                extent: extent_,
                source: new ol.source.XYZ({
                    tileGrid: tileGrid,
                    tileUrlFunction: createTileUrl
                }),
                opacity: 0.7
            })
        ],
        target: 'map',
        view: new ol.View(option)
        , loadTilesWhileAnimating: true
    });
}

// 換算圖磚索引
function createTileUrl(tileCoord) {
    var z = resolution_length_ - tileCoord[0] - 1; // 要換算 Level (z 的順序是相反的)
    var x = tileCoord[1];
    var y = tileCoord[2];
    var s = tileUrl.replace('{z}', z.toString())
        .replace('{x}', x.toString())
        .replace('{y}', y.toString());
    return s;
}

function loadLayerDef(url) {
    fetch(url).then(function (response) {
        return response.text();
    }).then(function (text) {
        // 剔除字串中, 多餘的內容
        var temp = text.replace('var result =', '');
        if (temp[temp.length - 1] == ';') {
            temp = temp.substring(0, temp.length - 1);
        }
        var tileGrid;
        var resolutions = [];
        //var result = JSON.parse(temp);    // 無法解譯 key:'value' 的字串, 掛 JSON5 來處理
        var tileDef = JSON5.parse(temp);
        var pNodeRes = tileDef.Infomation;				//tileDef 是解出來的物件, 取得其設定內容
        if (pNodeRes) {
            //var _resource = pNodeRes.ResourceName;		//取得TGOS圖磚服務名稱
            var dCLeft = parseFloat(pNodeRes.CornerLeft);	//取得服務的的原點(CornerLeft,CornerLower)坐標值
            var dCLower = parseFloat(pNodeRes.CornerLower);
            var ImgWidth = parseInt(pNodeRes.TileWidth);	//取得單張圖磚的寬度(TileWidth)和高度(TileHeight)
            var ImgHeight = parseInt(pNodeRes.TileHeight);
            var pEnv = pNodeRes.Envelope;					//取得圖磚的上下左右邊界值
            var dCacheLeft = parseFloat(pEnv.Left);
            var dCacheTop = parseFloat(pEnv.Top);
            var dCacheRight = parseFloat(pEnv.Right);
            var dCacheBottom = parseFloat(pEnv.Bottom);
            var pSclss = pNodeRes.Scales;
            var pScls = pSclss.Scale;				//取得服務內的所有縮放層級
            if (pScls) {
                if (pScls.length > 0) {
                    for (var i = 0; i < pScls.length; i++) {
                        var pScl = pScls[i];
                        var fac = parseFloat(pScl.Factor);	//依序取出各個縮放層級, 並存入陣列 resolutions
                        resolutions.push(fac);
                    }
                }
                resolution_length_ = resolutions.length; // 計算 z 時需要反過來算索引
            }
            extent_ = [dCacheLeft, dCacheBottom, dCacheRight, dCacheTop];
            tileGrid = new ol.tilegrid.createXYZ({
                tileSize: [ImgWidth, ImgHeight],
                origin: [dCLeft, dCLower],
                resolutions: resolutions
            });
        }
        return tileGrid;
    }).then(function (tileGrid) {
        createMap(tileGrid); // 完成 tileGrid 之後, 再建立地圖.
    });
}

function coloredSvgMarker(lonLat, name, color, circleFill) {
    if (!color) color = 'red';
    if (!circleFill) circleFill = 'white';
    var feature = new ol.Feature({
        geometry: new ol.geom.Point(ol.proj.fromLonLat(lonLat)),
        name: name
    });
    var svg = '<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" width="30px" height="30px" viewBox="0 0 30 30" enable-background="new 0 0 30 30" xml:space="preserve">' +
        '<path fill="' + color + '" d="M22.906,10.438c0,4.367-6.281,14.312-7.906,17.031c-1.719-2.75-7.906-12.665-7.906-17.031S10.634,2.531,15,2.531S22.906,6.071,22.906,10.438z"/>' +
        '<circle fill="' + circleFill + '" cx="15" cy="10.677" r="3.291"/></svg>';

    feature.setStyle(
        new ol.style.Style({
            image: new ol.style.Icon({
                anchor: [0.5, 1.0],
                anchorXUnits: 'fraction',
                anchorYUnits: 'fraction',
                src: 'data:image/svg+xml,' + escape(svg),
                scale: 2,
                imgSize: [30, 30],
            })
        })
    );
    return feature;
}