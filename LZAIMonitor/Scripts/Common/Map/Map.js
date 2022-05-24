"use strict";

var IsTypeError = function (obj, type) {
    var objType = Object.getPrototypeOf(obj).constructor.name;
    if (objType !== type) {
        console.log(new TypeError("'" + objType + "' is not '" + type + "'."));
        //console.log(`typeError: '${objType}' is not '${type}'`);
        return true;
    } else {
        return false;
    }
}

var TransferCoord = function (coordinate) {
    var wgs84List = ["EPSG3857", "EPSG4326"];
    var twd97List = ["EPSG3826"];

    if (wgs84List.includes(coordinate)) {
        return "wgs84";
    } else if (twd97List.includes(coordinate)) {
        return "twd97";
    }
}

var SplitByKeyword = function (polyString, keyword, left, right) {
    var arr = polyString.split(keyword);
    var str = "";
    var result = [];
    var IsFrontOfKeyword = function (item) {
        return left ? Boolean(item.trim().lastIndexOf(left) === item.trim().split('').length - left.trim().split('').length) : true;
    }
    //var IsBehindKeyword = function (item) {
    //    return right ? Boolean(item.trim().indexOf(right) === 0) : true;
    //}

    arr.forEach(function (item, key) {
        str += str === "" ? item : keyword + item;
        if (IsFrontOfKeyword(item)) {
            result.push(str.trim());
            str = "";
        } else if (key === arr.length - 1) {
            result.push(str.trim());
        }
    });
    console.log(result);
    return result;
}

var mapOptions = {
    navigationControl: false,
    //scaleControl: false,
    //scaleControlOptions: {
    //    controlPosition: TGOS.TGControlPosition.RIGHT_BOTTOM,
    //    scaleControlStyle: TGOS.TGScaleControlStyle.TEXT
    //},
    scrollwheel: true,
    mapTypeControl: false,
    mapTypeControlOptions: {
        //mapTypeId: TGOS.TGMapTypeId.TGOSMAP,
        //, ["TGOSMAP", "NLSCMAP", undefined, "F2IMAGE", "ROADMAP", "HILLSHADEMIX", "HILLSHADE", "IMAGENLSC"]
        //mapTypeIds: [TGOS.TGMapTypeId.TGOSMAP, TGOS.TGMapTypeId.ROADMAP, TGOS.TGMapTypeId.IMAGENLSC],
        //controlPosition: TGOS.TGControlPosition.LEFT_BOTTOM,
        //mapTypeControlStyle: TGOS.TGMapTypeControlStyle.DEFAULT,

        controlPosition: "LEFT_BOTTOM",
        mapTypeControlStyle: "DEFAULT",
        mapTypeIds: ["TGOSMAP", "IMAGENLSC", "HILLSHADEMIX", "HILLSHADE"],
        //mapTypeIds: ["TGOSMAP", "NLSCMAP", "F2IMAGE", "ROADMAP", "HILLSHADEMIX", "HILLSHADE", "IMAGENLSC"],
        zIndex: 99,

    },
    scaleControl: true,  //scaleControl(是否開啟比例尺控制項)
    scaleControlOptions: {  //scaleControlOptions(提供指定比例尺控制項)
        controlPosition: TGOS.TGControlPosition.RIGHT_BOTTOM,  //position(設定比例尺控制項在地圖的位置)
        //scaleControlStyle: TGOS.TGScaleControlStyle.TEXT
    },
    draggable: true,
    keyboardShortcuts: false,
    //disableDefaultUI: true
};

//var option = {
//    id: "#map",
//    mapType: "TGOS",
//    zoom: 8,
//    center: { x: 120.51399546171601, y: 24.36730759959248},
//    coordinate: "EPSG3857",
//    mapOptions: mapOptions
//};

var PST = function () {
    return {
        Map: function (option) {
            switch (option.mapType) {
                case "TGOS": {
                    return new Maps(option);
                }
                    break;
                case "Google": {
                    alert('Google地圖尚未開放');
                }
                    break;
                default: {
                    alert('暫無此地圖類型');
                }
            }
        },
        Locate: function (map) {
            switch (map.mapType) {
                case "TGOS": {
                    return new Locate(map);
                }
                    break;
                case "Google": {
                    alert('Google地圖尚未開放');
                }
                    break;
                default: {
                    alert('暫無此地圖類型');
                }
            }
        }
    }
}();

function Maps(option) {
    if (!(this instanceof Maps)) {
        return new Maps(option);
    }

    if (option === undefined)
        return false;


    this.id = option.id;
    this.mapType = option.mapType;
    this.mapOptions = option.mapOptions ? option.mapOptions : mapOptions;
    this.coordinate = option.coordinate;
    this.zoom = option.zoom;
    this.zoomRange = option.zoomRange;
    this.center = option.center;

    this.container = {
        Point: [],
        Circle: [],
        Line: [],
        Polygon: [],
        Label: [],
        MultiPoint: [],
        ClusterPoint: [],
        WMSLayer: [],
        WMTSLayer: [],
        KMLLayer: [],
        Drawing: []
    }

    this.Init();
}


Maps.prototype.Init = function () {
    var id = document.querySelector(this.id);
    //id.innerHTML = "";

    switch (this.mapType) {
        case "TGOS": {
            this.mapObject = new TGOS.TGOnlineMap(id, TGOS.TGCoordSys[String(this.coordinate)]);
            this.mapObject.setOptions(this.mapOptions);

            this.SetZoom(parseInt(this.zoom));
            this.SetCenter(this.center);
            //console.log(this.mapObject)
            this.SetZoomRange(this.zoomRange.min, this.zoomRange.max);
        }
            break;
        case "Leaflet": {
            this.mapObject = L.map(id.id, {
                center: [this.center.y, this.center.x],
                zoom: parseInt(this.zoom),
                crs: L.CRS[this.coordinate] || L.CRS.EPSG3857,
                zoomControl: false,
                tms: false,
                renderer: L.canvas()
            });
            //this.mapObject.setOptions(this.mapOptions);
            //this.SetZoomRange(this.zoomRange.min, this.zoomRange.max);
            this.mapObject.setMinZoom(this.zoomRange.min);
            this.mapObject.setMaxZoom(this.zoomRange.max);

            this.baseLayer = L.tileLayer('https://wmts.nlsc.gov.tw/wmts/EMAP/default/GoogleMapsCompatible/{z}/{y}/{x}', {
                //subdomains: ['a', 'b', 'c'],
                //minZoom: 8,
                //maxZoom: 18
            }).addTo(this.mapObject);

        }
            break;
    }
}

Maps.prototype.SetCenter = function (obj) {
    this.mapObject.setCenter(obj);
}

Maps.prototype.SetZoom = function (obj) {
    this.mapObject.setZoom(obj);
}

Maps.prototype.GetZoom = function () {
    return this.mapObject.getZoom();
}

Maps.prototype.SetZoomRange = function (min, max) {
    var el = this;
    min = min || 0;
    max = max || 99;
    TGOS.TGEvent.addListener(el.mapObject, "zoom_changed", function () {
        if (el.mapObject.getZoom() < min) {
            el.mapObject.setZoom(min);
        } else if (el.mapObject.getZoom() > max) {
            el.mapObject.setZoom(max);
        }
    });
}

Maps.prototype.ZoomIn = function () {
    this.mapObject.setZoom(this.mapObject.getZoom() + 1);
}

Maps.prototype.ZoomOut = function () {
    this.mapObject.setZoom(this.mapObject.getZoom() - 1);
}
Maps.prototype.GetBounding = function () {
    var envelope = this.mapObject.getBounds();
    this.bounding = {
        minX: envelope.left,
        maxX: envelope.right,
        minY: envelope.bottom,
        maxY: envelope.top
    };

    return this.bounding;
}

Maps.prototype.AddEvent = function (event, callback) {
    TGOS.TGEvent.addListener(this.mapObject, event, function (e) {
        callback(e);
    });
}

Maps.prototype.ClearEvent = function (event) {
    TGOS.TGEvent.clearListeners(this.mapObject, event);
}

Maps.prototype.SetBounding = function (obj) {
    obj = {
        minX: obj.minX,
        maxX: obj.maxX,
        minY: obj.minY,
        maxY: obj.maxY
    };
    this.mapObject.fitBounds(new TGOS.TGEnvelope(obj.minX, obj.minY, obj.maxX, obj.maxY));
}

Maps.prototype.ClearPoint = function (key) {
    if (typeof key === 'number') {
        this.container.Point[key].marker.setMap(null);
        if (this.container.Point[key].infoWindow) {
            this.container.Point[key].infoWindow.close();
        }

    } else {
        this.container.Point.forEach(function (item) {
            item.marker.setMap(null);

            if (item.infoWindow) {
                item.infoWindow.close();
            }
        });
        this.container.Point = [];
    }
}

Maps.prototype.ClearCircle = function (key) {
    //for (var i = 0; i < this.container.Circle.length; i++) {
    //    this.container.Circle[i].circle.setMap(null);
    //    if (this.container.Circle[i].center) {
    //        this.container.Circle[i].center.setMap(null);
    //    }
    //}

    //this.container.Circle = [];

    if (typeof key === 'number') {
        this.container.Circle[key].circle.setMap(null);
    } else {
        for (var i = 0; i < this.container.Circle.length; i++) {
        this.container.Circle[i].circle.setMap(null);
            if (this.container.Circle[i].center) {
                this.container.Circle[i].center.setMap(null);
            }
        }
        this.container.Circle = [];
    }

}

//Map.Clear = function () {
//    return {
//        Point: function () {
//            for (var i = 0; i < this.container.Point.length; i++) {
//                this.container.Point[i].setMap(null);
//            }
//            this.container.Point = [];
//        },
//    }
//}

Maps.prototype.ClearLabel = function () {
    for (var i = 0; i < this.container.Label.length; i++) {
        this.container.Label[i].setMap(null);
    }
    this.container.Label = [];
}

Maps.prototype.ClearLine = function () {
    for (var i = 0; i < this.container.Line.length; i++) {
        this.container.Line[i].setMap(null);
    }
    this.container.Line = [];
}

Maps.prototype.ClearPolygon = function (key) {
    //for (var i = 0; i < this.container.Polygon.length; i++) {
    //    this.container.Polygon[i].setMap(null);
    //}

    if (typeof key === 'number') {
        this.container.Polygon[key].setMap(null);
    } else {
        this.container.Polygon.forEach(function (polygon) {
            if (polygon) {
                polygon.setMap(null);
            }
        });
        this.container.Polygon = [];
    }
}

Maps.prototype.ClearMultiPoint = function (key) {
    if (typeof key === 'number') {
        if (this.container.MultiPoint[key].cluster) {
            this.container.MultiPoint[key].SetBounding();
            this.container.MultiPoint[key].RemoveCluster();
        } else {
            this.container.MultiPoint[key].markerList.forEach(function (item) {
                item.setMap(null);
            });
        }
        this.container.MultiPoint[key].labelList.forEach(function (item) {
            item.setMap(null);
        });
        if (this.container.MultiPoint[key].infoWindow) {
            this.container.MultiPoint[key].infoWindow.close();
        }
    } else {
        this.container.MultiPoint.forEach(function (multipoint) {
            if (multipoint.cluster) {
                multipoint.SetBounding();
                multipoint.RemoveCluster();
            } else {
                multipoint.markerList.forEach(function (item) {
                    item.setMap(null);
                });
            }
            multipoint.labelList.forEach(function (item) {
                item.setMap(null);
            });
            if (multipoint.infoWindow) {
                multipoint.infoWindow.close();
            }
        });
        this.container.MultiPoint = [];
    }
}

Maps.prototype.ClearClusterPoint = function () {
    this.container.ClusterPoint.forEach(function (clusterPoint) {
        clusterPoint.markerList.forEach(function (item) {
            item.setMap(null);
        });
        clusterPoint.labelList.forEach(function (item) {
            item.setMap(null);
        });
    });
    this.container.ClusterPoint = [];
}

Maps.prototype.ClearWMSLayer = function (key) {
    if (typeof key === 'number') {
        this.container.WMSLayer[key].removeWmsLayer();
        //this.container.WMSLayer.splice(key, 1);
    } else {
        this.container.WMSLayer.forEach(function (item) {
            item.removeWmsLayer();
        });
        this.container.WMSLayer = [];
    }
    //map.container.WMSLayer = map.container.WMSLayer.filter(function (item) {
    //    return Boolean(item);
    //});
}

Maps.prototype.ClearWMTSLayer = function (key) {
    if (typeof key === 'number') {
        this.container.WMTSLayer[key].removeWmtsLayer();
        this.container.WMTSLayer[key] = {};
    } else {
        this.container.WMTSLayer.forEach(function (item) {
            item.removeWmtsLayer();
        });
        this.container.WMTSLayer = [];
    }
}

Maps.prototype.ClearKMLLayer = function (key) {
    if (typeof key === 'number') {
        this.container.KMLLayer[key].removeKml();
        this.container.KMLLayer[key] = {};
    } else {
        this.container.KMLLayer.forEach(function (item) {
            item.removeKml();
        });
        this.container.KMLLayer = [];
    }
}

Maps.prototype.ClearDrawing = function () {
    this.container.Drawing.forEach(function (item) {
        TGOS.TGEvent.removeListener(item.listener);
        item.drawing.clearAllGraphics();
        item.drawing.setDefaultControlVisible(false);
        item.drawing.setDrawingMode('none');
    });
    this.container.Drawing = [];
}

Maps.prototype.AddWMSLayer = function (url, option) {
    option = option || {}
    this.container.WMSLayer.push(new TGOS.TGWmsLayer(url, {
        map: this.mapObject,
        preserveViewport: true,
        wsVisible: true,
        zIndex: option.zIndex || 0,
        opacity: option.opacity || 0.65
    }));

    return this.container.WMSLayer.length - 1;
}

Maps.prototype.AddWMTSLayer = function (url, option, parameter) {
    parameter = parameter || {}
    option = option || {}
    this.container.WMTSLayer.push(new TGOS.TGWmtsLayer(url, this.mapObject, {
        matrixSet: parameter.matrixSet,  // MatrixSet設定, 必要參數, 可進WMTS的Capabilities去看所需圖層使用的MatrixSet
        layer: parameter.layer,  // Layer Name, 必要參數
        format: parameter.format || "image/jpeg",
        style: parameter.style || "default"
    }, {
            wmtsVisible: true,
            zIndex: option.zIndex || 0,
            opacity: option.opacity || 0.6
        }));

    return this.container.WMTSLayer.length - 1;
}

Maps.prototype.AddKMLLayer = function (url, option) {
    option = option || {};
    this.container.KMLLayer.push(new TGOS.TGKmlLayer(url, {
        map: this.mapObject,
        suppressInfoWindows: false,
        preserveViewport: true,
        zIndex: option.zIndex || 1,
    }));

    return this.container.KMLLayer.length - 1;
}

Maps.prototype.SetWMSLayerOpacity = function (key, opacity) {
    map.container.WMSLayer[key].setOpacity(parseFloat(opacity));
}

Maps.prototype.SetKMLLayerOpacity = function (key, opacity) {
}

Maps.prototype.SetWMTSLayerOpacity = function (key, opacity) {
    map.container.WMTSLayer[key].setTileOpacity(parseFloat(opacity));
}

// WKTFormat 待續
Maps.prototype.WKTFormatToObject = function (polyString) {
    var IsOK, geoType, pointList = [];

    if (IsTypeError(polyString, "String")) {
        IsOK = false;
        return {
            IsOK: IsOK,
            geoType: geoType,
            pointList: pointList,
        }
    }

    geoType = polyString.slice(0, polyString.indexOf('(')).trim().toUpperCase();

    if (geoType === "GeometryCollection") {
        polyString = polyString.slice(polyString.indexOf('(') + 1, polyString.lastIndexOf(')')).trim();
        // 未完待續

    } else if (geoType === "POLYGON") {

        polyString = polyString.slice(polyString.indexOf('(') + 1, polyString.lastIndexOf(')')).trim();
        pointList = SplitByKeyword(polyString, ',', ')');
        pointList = pointList.map(function (item) {
            item = item.slice(item.indexOf('(') + 1, item.lastIndexOf(')')).trim().split(',');
            item = item.map(function (item) {  // 重組陣列
                return {
                    x: item.trim().split(' ')[0],
                    y: item.trim().split(' ')[1]
                };
            });
            return item;
        });
        IsOK = true;
        //polyString = polyString.split('((')[1].replace('))', '');  // 拆除(( ))
        //pointList = polyString.split(',');  // 分割各個點存成陣列
        //pointList = pointList.map(function (item) {  // 重組陣列
        //    return {
        //        x: item.trim().split(' ')[0],
        //        y: item.trim().split(' ')[1]
        //    };
        //});
        //IsOK = true;

    } else if (geoType === "LINESTRING") {
        polyString = polyString.split('(')[1].replace(')', '');
        pointList = polyString.split(',');
        pointList = pointList.map(function (item) {
            return {
                x: item.trim().split(' ')[0],
                y: item.trim().split(' ')[1]
            };
        });
        IsOK = true;

    } else if (geoType === "POINT") {
        polyString = polyString.slice(polyString.indexOf('(') + 1, polyString.lastIndexOf(')')).trim();
        pointList.push({
            x: polyString.split(' ')[0],
            y: polyString.split(' ')[1]
        });
        IsOK = true;
    } else {
        IsOK = false;
    }

    return {
        IsOK: IsOK,
        geoType: geoType,
        pointList: pointList,
    };
}

Maps.prototype.MeasureDistance = function (pointList, coordinateType) {
    var measureService = new TGOS.TGMeasureService();
    var result = {};
    var line = new Line(pointList, map);
    coordinateType = coordinateType || TransferCoord(this.coordinate);

    switch (coordinateType) {
        case "twd97": {
            measureService.twd97LineMeasure(line.GetLineString(), function (distance, status) {
                result = {
                    distance: distance,
                    status: status
                }
            });
        }
            break;
        case "wgs84": {
            measureService.wgs84LineMeasure(line.GetLineString(), function (distance, status) {
                result = {
                    distance: distance,
                    status: status
                }
            });
        }
            break;
    }

    return result;
}

Maps.prototype.MeasureArea = function (pointList, coordinateType) {
    var measureService = new TGOS.TGMeasureService();
    var measureMethod = function () { }
    var result = {};
    var poly = new Polygon(pointList, map);
    coordinateType = coordinateType || TransferCoord(this.coordinate);
    measureMethod = coordinateType + "PolygonMeasure";

    if (measureService[measureMethod] instanceof Function) {
        measureService[measureMethod](poly.polygon, function (area, status) {
            result = {
                area: area,
                status: status
            }
        });
    } else {
        console.log(new TypeError('MeasureArea is not a Function.'));
    }

    //switch (coordinateType) {
    //    case "twd97": {
    //        measureService.twd97PolygonMeasure(poly.polygon, function (area, status) {
    //            result = {
    //                area: area,
    //                status: status
    //            }
    //        });
    //    }
    //        break;
    //    case "wgs84": {
    //        measureService.wgs84PolygonMeasure(poly.polygon, function (area, status) {
    //            result = {
    //                area: area,
    //                status: status
    //            }
    //        });
    //    }
    //        break;
    //}

    return result;
}

//行政區套疊

Maps.prototype.CountyTownPosition = function (str, callback) {
    if (str === "高雄市三民東區" || str === "高雄市三民西區") { str = "高雄市三民區"; }
    if (str === "高雄市南鳳山區" || str === "高雄市北鳳山區") { str = "高雄市鳳山區"; }
    var pgn = new TGOS.TGPolygon();
    var locator = new TGOS.TGLocateService();
    locator.locateWGS84({
        district: str
    },function (e, status) {
        if (status != TGOS.TGLocatorStatus.OK) {
            alert('查無行政區');
        }
        map.mapObject.fitBounds(e[0].geometry.viewport);

        //map.mapObject.setZoom(13);
        //調整畫面符合行政區邊界
            pgn = e[0].geometry.geometry;
            console.log(pgn);
            callback(pgn);
    });

}



function Locate(map) {
    if (!(this instanceof Locate)) {
        return new Locate(map);
    }

    this.map = Object.create(map);
    this.CoordinateType = TransferCoord(this.map.coordinate);
}

Locate.prototype.ByAddress = function (address, callback) {
    var locator = new TGOS.TGLocateService();
    var callback = callback || function () { }
    var result = {
        x: undefined,
        y: undefined,
        pointList: [],
        type: this.CoordinateType,
        errorMessage: ''
    }

    switch (this.CoordinateType) {
        case "twd97": {
            locator.locateTWD97({ address: address }, function (e, status) {
                if (status === TGOS.TGLocatorStatus.OK) {
                    result.x = e[0].geometry.location.x;
                    result.y = e[0].geometry.location.y;
                    result.pointList.push({
                        county: e[0].addressComponents.county,
                        town: e[0].addressComponents.town,
                        addressComponents: e[0].addressComponents,
                        address: e[0].formattedAddress,
                        x: e[0].geometry.location.x,
                        y: e[0].geometry.location.y
                    });

                } else if (status === TGOS.TGLocatorStatus.TOO_MANY_RESULTS) {
                    result.pointList = e.map(function (item) {
                        return {
                            county: item.addressComponents.county,
                            town: item.addressComponents.town,
                            addressComponents: item.addressComponents,
                            address: item.formattedAddress,
                            x: item.geometry.location.x,
                            y: item.geometry.location.y
                        }
                    });
                    result.errorMessage = '地址查詢結果過多！';

                } else {
                    console.log('查無地址！');
                    result.errorMessage = '查無地址！';
                }

                callback(result);
            });
        }
            break;
        case "wgs84": {
            locator.locateWGS84({ address: address }, function (e, status) {
                //if (status != TGOS.TGLocatorStatus.OK) {
                //    console.log('查無地址！');
                //    console.log(e);
                //    console.log(status);
                //    result.errorMessage = '查無地址！';
                //} else {
                //    result.x = e[0].geometry.location.x;
                //    result.y = e[0].geometry.location.y;
                //}


                if (status === TGOS.TGLocatorStatus.OK) {
                    result.x = e[0].geometry.location.x;
                    result.y = e[0].geometry.location.y;
                    result.pointList.push({
                        county: e[0].addressComponents.county,
                        town: e[0].addressComponents.town,
                        addressComponents: e[0].addressComponents,
                        address: e[0].formattedAddress,
                        x: e[0].geometry.location.x,
                        y: e[0].geometry.location.y
                    });

                } else if (status === TGOS.TGLocatorStatus.TOO_MANY_RESULTS) {
                    result.pointList = e.map(function (item) {
                        return {
                            county: item.addressComponents.county,
                            town: item.addressComponents.town,
                            addressComponents: item.addressComponents,
                            address: item.formattedAddress,
                            x: item.geometry.location.x,
                            y: item.geometry.location.y
                        }
                    });
                    result.errorMessage = '地址查詢結果過多！';

                } else {
                    console.log('查無地址！');
                    result.errorMessage = '查無地址！';
                }

                callback(result);
            });
        }
            break;
    }

    this.result = result;
    return result;
}

Locate.prototype.ByDistrict = function (district, callback) {
    var locator = new TGOS.TGLocateService();
    var callback = callback || function () { }
    var result = {
        pointList: [],
        type: this.CoordinateType,
        errorMessage: ''
    }

    switch (this.CoordinateType) {
        case "twd97": {
            locator.locateTWD97({ district: district }, function (e, status) {
                if (status != TGOS.TGLocatorStatus.OK) {
                    console.log('查無行政區！');
                    result.errorMessage = '查無行政區！';
                } else {
                    result.pointList = e[0].geometry.geometry.rings_[0].linestring.path.map(function (item) {
                        return {
                            x: item.x,
                            y: item.y
                        }
                    });
                }

                callback(result);
            });
        }
            break;
        case "wgs84": {
            locator.locateWGS84({ district: district }, function (e, status) {
                if (status != TGOS.TGLocatorStatus.OK) {
                    console.log('查無行政區！');
                    result.errorMessage = '查無行政區！';
                } else {
                    result.pointList = e[0].geometry.geometry.rings_[0].linestring.path.map(function (item) {
                        return {
                            x: item.x,
                            y: item.y
                        }
                    });
                }

                callback(result);
            });
        }
            break;
    }

    this.result = result;
    return result;
}

Locate.prototype.ByIntersection = function (request, callback) {
    var locate = new TGOS.TGLocate();
    var callback = callback || function () { }
    var request = {
        geometryInfo: true,
        county: request.county,//縣市
        firstRoad: request.firstRoad,   // 第一條道路
        secondRoad: request.secondRoad,  // 第二條道路
        //pageNumber: pageNumber,  // 查詢結果之頁數(30筆一頁)，預設為第1頁。
        //firstCounty: firstCounty,  // 第一條道路所在縣市
        //secondCounty: secondCounty  // 第二條道路所在縣市
    }
    var result = {
        pointList: [],
        type: this.CoordinateType,
        errorMessage: ''
    }

    locate.roadCross(request, this.map.coordinate, function (e, status, items, pages) {
        console.log(arguments);
        if (status !== TGOS.TGLocatorStatus.OK && status !== TGOS.TGLocatorStatus.TOO_MANY_RESULTS) {
            console.log(status);
            result.errorMessage = '查無交叉路口！';
        } else {
            result.pointList = e.map(function (item) {
                return {
                    county: item.county,
                    town: item.town,
                    firstRoad: item.firstRoad,
                    secondRoad: item.secondRoad,
                    x: item.geometry.location.x,
                    y: item.geometry.location.y
                }
            });
        }

        callback(result);

    });

    this.result = result;
    return result;
}

Locate.prototype.ByLandmark = function (request, callback) {
    var locator = new TGOS.TGLocateService();
    var callback = callback || function () { }
    var request = {
        county: request.county || '',
        town: request.town || '',
        poi: request.poi,
        pageNumber: request.page || 1,
        center: new TGOS.TGPoint(121, 23)
    }
    var result = {
        pointList: [],
        type: this.CoordinateType,
        errorMessage: '',
        totalCount: '',
        totalPage: ''
    }

    switch (this.CoordinateType) {
        case "twd97": {
            locator.locateTWD97(request, function (e, status, totalCount, totalPage) {
                console.log(arguments);
                if (status == TGOS.TGLocatorStatus.ZERO_RESULTS) {
                    console.log(status);
                    result.errorMessage = '查無地標！';
                } else {
                    result.pointList = e.map(function (item) {
                        return {
                            county: item.county,
                            town: item.town,
                            poiName: item.poiName,
                            x: item.geometry.location.x,
                            y: item.geometry.location.y
                        }
                    });
                    result.totalCount = totalCount;
                    result.totalPage = totalPage;
                }

                callback(result);
            });
        }
            break;
        case "wgs84": {
            locator.locateWGS84(request, function (e, status, totalCount, totalPage) {
                console.log(arguments);
                if (status == TGOS.TGLocatorStatus.ZERO_RESULTS) {
                    console.log(status);
                    result.errorMessage = '查無地標！';
                } else {
                    result.pointList = e.map(function (item) {
                        return {
                            county: item.county,
                            town: item.town,
                            poiName: item.poiName,
                            x: item.geometry.location.x,
                            y: item.geometry.location.y
                        }
                    });
                    result.totalCount = totalCount;
                    result.totalPage = totalPage;
                }

                callback(result);
            });
        }
            break;
    }

    this.result = result;
    return result;
}

//var obj = { x: 121, y: 23 };
function Point(obj, map) {
    if (!(this instanceof Point)) {
        return new Point(obj, map);
    }

    this.x = obj.x;
    this.y = obj.y;
    this.map = Object.create(map);
    //this.ExtendMap(map);
}

//Point.prototype.ExtendMap = function (map) {
//    this.map = Object.create(map);
//}

Point.prototype.TransCoordinate = function (transType, obj) {
    var trans = new TGOS.TGTransformation();
    var type, x, y;
    obj = obj || {};

    if (!obj.x || !obj.y) {
        x = this.x;
        y = this.y;
    } else {
        x = obj.x;
        y = obj.y;
    }

    switch (transType) {
        case "twd97towgs84": {
            trans.twd97towgs84(Number(x), Number(y));
            type = "wgs84"
        }
            break;
        case "wgs84totwd97": {
            trans.wgs84totwd97(Number(x), Number(y));
            type = "twd97"
        }
            break;
    }

    this.x = trans.transResult.x;
    this.y = trans.transResult.y;

    return {
        x: this.x,
        y: this.y,
        type: type
    }
}

Point.prototype.ShowMarker = function (option) {
    option = option || {};
    option = {
        cursor: option.cursor || 'pointer',
        title: option.title || '',
        icon: new TGOS.TGImage(option.icon || '#',
            new TGOS.TGSize((option.width || 40), (option.height || 40))
            , new TGOS.TGPoint(0, 0)
            , new TGOS.TGPoint((option.PointWidth) || 20, (option.PointHeight || 15))),
        IsSetCenter: String(option.IsSetCenter) === 'false' ? false : true,
        IsSetZoom: String(option.IsSetZoom) === 'false' ? false : true,
        flat: !option.IsShadow || true
    }
    this.point = new TGOS.TGPoint(this.x, this.y);
    this.marker = new TGOS.TGMarker(this.map.mapObject, this.point, option.title, option.icon, option);

    if (option.IsSetCenter) {
        this.SetCenter();
    }
    if (option.IsSetZoom) {
        this.SetZoom();
    }
    this.SavePoint(this.point, this.marker);
}

Point.prototype.SetCenter = function () {
    this.map.mapObject.setCenter(this.point);
}

Point.prototype.SetZoom = function () {
    this.map.mapObject.setZoom(19);
}

Point.prototype.SavePoint = function () {
    this.map.container.Point.push(this);
}

Point.prototype.AddEvent = function (event, eventCallback) {
    eventCallback = eventCallback || function () { }
    TGOS.TGEvent.addListener(this.marker, event, function (e) {
        eventCallback(e);
    });
}

Point.prototype.OpenInfoWindow = function () {
    this.infoWindow.open(this.map.mapObject);
}

Point.prototype.CloseInfoWindow = function () {
    this.infoWindow.close();
}

Point.prototype.SetInfoWindow = function (content) {
    var infoWindowOptions = {
        maxWidth: 460,
        pixelOffset: new TGOS.TGSize(5, -5),  // InfoWindow起始位置的偏移量, 向右X為正, 向上Y為負  
        zIndex: 99999,
        disableAutoPan: true,
        opacity: 1
    };
    this.infoWindow = new TGOS.TGInfoWindow(content, this.point, infoWindowOptions);
    //eventCallback = eventCallback || function () { }
    //TGOS.TGEvent.addListener(marker, "click", function () {
    //    messageBox.open(el.map.mapObject);
    //});
    //TGOS.TGEvent.addListener(marker, "mouseout", function () {
    //    messageBox.close();
    //});
}

Point.prototype.AddLabel = function (text, option) {
    option = option || {};
    this.label = new TGOS.TGLabel({
        map: this.map.mapObject,
        fontColor: option.fontColor || "#ff0100",	//字型顏色
        fontSize: option.fontSize || 18,		    //字體大小
        font: option.font || "微軟正黑體",	        //字型
        fontWeight: option.fontWeight || "bold",	//字體粗細
        opacity: option.opacity || 0.8,			//透明度
        position: this.point,			                //註記位置
        label: text
    });
    this.map.container.Label.push(this.label);
}

Point.prototype.LocateByAddress = function (address, option, callback) {
    var locate = new Locate(this.map);
    var callback = callback || function () { }
    var el = this;
    var locateStatus = {
        IsOk: false,
        errorMessage: '',
        x: '',
        y: ''
    };
    option = option || {};

    locate.ByAddress(address, function (result) {
        if (result.errorMessage) {
            locateStatus.errorMessage = result.errorMessage;
            locateStatus.IsOk = false;
        } else {
            el.x = result.x;
            el.y = result.y;
            el.ShowMarker({ icon: option.icon });

            locateStatus.x = result.x;
            locateStatus.y = result.y;
            locateStatus.IsOk = true;
        }

        callback(locateStatus);
    });
}


function MultiPoint(pointList, map) {
    if (!(this instanceof MultiPoint)) {
        return new MultiPoint(pointList, map);
    }
    this.map = Object.create(map);
    this.pointList = pointList;
    this.markerList = [];
    this.labelList = [];
}

MultiPoint.prototype = Object.create(Point.prototype);
MultiPoint.prototype.constructor = MultiPoint;
MultiPoint.prototype.superClass = Object.create(Point.prototype);

//MultiPoint.prototype.ShowMultiPoint = function (event, eventCallback) {
//    var el = this;
//    eventCallback = eventCallback || function () { }
//    this.pointList.forEach(function (item, index) {
//        var point = new TGOS.TGPoint(item.x, item.y);
//        var marker = new TGOS.TGMarker(el.map.mapObject, point, '', new TGOS.TGImage('http://api.tgos.tw/TGOSMAPAPI/images/Sample/marker.png', new TGOS.TGSize(30, 30)));
//        el.markerList.push(marker);

//        if (event) {
//            (function (item, index) {
//                TGOS.TGEvent.addListener(marker, event, function (e) {
//                    eventCallback(item, index, e);
//                });
//            })(item, index);
//        }
//    });
//    this.SetBounding();
//    this.map.container.MultiPoint.push(this);
//}


//var eventList = [{
//    event: 'click',
//    eventCallback: function () { },
//}, {}
//];

MultiPoint.prototype.ShowMultiPoint = function (option, eventList) {
    var el = this;
    var eventList = eventList instanceof Array ? eventList.map(function (item) {
        return {
            event: item.event,
            eventCallback: item.eventCallback || function () { },
        };
    }) : [];
    option = option || {};
    option = {
        icon: option.icon || 'Build/img/marker.png',
        width: option.width,
        height: option.height,
        IsDifferentIcon: option.IsDifferentIcon || false,
        IsSetBounding: option.IsSetBounding || false,
        PointWidth: option.PointWidth,
        PointHeight: option.PointHeight,
    };
    if (option.IsDifferentIcon) {
        return this.ShowMultiPoint_differentIcon.call(this, option, eventList);
    }

    this.pointList.forEach(function (item, index) {
        el.x = item.x;
        el.y = item.y;
        el.ShowMarker(option);
        eventList.forEach(function (eventItem) {
            if (eventItem.event) {
                (function (item, index) {
                    el.AddEvent(eventItem.event, function (e) {
                        el.BackToDefault(option);
                        el.x = item.x;
                        el.y = item.y;
                        el.point = new TGOS.TGPoint(item.x, item.y);
                        el.marker = el.markerList[index];
                        eventItem.eventCallback(item, index, e);
                    });
                })(item, index);
            }
        });
    });
    if (option.IsSetBounding) {
        this.SetBounding();
    }
    this.SaveMultiPoint();
}

MultiPoint.prototype.ShowMultiPoint_differentIcon = function (option, eventList) {
    var el = this;
    var eventList = eventList instanceof Array ? eventList.map(function (item) {
        return {
            event: item.event,
            eventCallback: item.eventCallback || function () { },
        };
    }) : [];

    this.pointList.forEach(function (item, index) {
        el.x = item.x;
        el.y = item.y;
        el.ShowMarker({
            icon: item.icon,
            width: option.width,
            height: option.height,
            IsSetCenter: false,
            IsSetZoom: false,
            PointWidth: option.PointWidth,
            PointHeight: option.PointHeight,
        });

        eventList.forEach(function (eventItem) {
            if (eventItem.event) {
                (function (item, index) {
                    el.AddEvent(eventItem.event, function (e) {
                        el.x = item.x;
                        el.y = item.y;
                        el.point = new TGOS.TGPoint(item.x, item.y);
                        el.marker = el.markerList[index];
                        eventItem.eventCallback(item, index, e);
                    });
                })(item, index);
            }
        });
    });
    if (option.IsSetBounding) {
        this.SetBounding();
    }
    this.SaveMultiPoint();
}

//MultiPoint.prototype.ChangeMultiPoint_differentIcon = function (IconList,option) {
//    el = this;
//    el.markerList.forEach(function (item, index) {
//        var OutputIcon = el.pointList[item].icon || 'Build/img/Circle_Davys-Grey_Solid.svg'
//        item.setIcon()
//    });
//};

MultiPoint.prototype.SetZoom = function () { }

MultiPoint.prototype.SetCenter = function () { }

MultiPoint.prototype.SavePoint = function (point, marker) {
    this.markerList.push(marker);
}

MultiPoint.prototype.SaveMultiPoint = function () {
    this.map.container.MultiPoint.push(this);
}

MultiPoint.prototype.SetInfoWindow = function (content) {
    if (this.infoWindow) {
        this.infoWindow.close();
    }
    this.superClass.SetInfoWindow.call(this, content);
}

MultiPoint.prototype.HighLight = function (key, defaultOption, highLightOption) {
    option = option || {}
    defaultOption = {
        icon: defaultOption.icon,
        height: defaultOption.height,
        width: defaultOption.width,
    }
    highLightOption = {
        icon: highLightOption.icon,
        height: highLightOption.height,
        width: highLightOption.width
    }

    //this.markerList.forEach(function (item) {
    //    item.setIcon(new TGOS.TGImage(defaultOption.icon || 'Build/img/marker.png', new TGOS.TGSize(30, 30)));
    //});
    this.BackToDefault(defaultOption);
    this.x = this.markerList[key].x;
    this.y = this.markerList[key].y;
    this.point = new TGOS.TGPoint(this.x, this.y);
    this.marker = this.markerList[key];
    this.markerList[key].setIcon(new TGOS.TGImage(highLightOption.icon || 'https://image.flaticon.com/icons/svg/423/423558.svg', new TGOS.TGSize(highLightOption.width || 40, highLightOption.height || 40), new TGOS.TGPoint(0, 0), new TGOS.TGPoint(20, 20)));
}

MultiPoint.prototype.GetBounding = function () {
    var xList = this.pointList.map(function (item) {
        return item.x;
    });
    var yList = this.pointList.map(function (item) {
        return item.y;
    });

    this.bounding = {
        minX: Math.min.apply(null, xList),
        maxX: Math.max.apply(null, xList),
        minY: Math.min.apply(null, yList),
        maxY: Math.max.apply(null, yList)
    };

    return this.bounding;
}

MultiPoint.prototype.SetBounding = function () {
    this.GetBounding();
    this.map.mapObject.fitBounds(new TGOS.TGEnvelope(this.bounding.minX, this.bounding.minY, this.bounding.maxX, this.bounding.maxY));
    this.map.mapObject.setZoom(this.map.mapObject.getZoom() - 1);
}

MultiPoint.prototype.SetCluster = function (option) {
    option = option || {}
    this.cluster = new TGOS.TGMarkerCluster(this.map.mapObject, this.markerList, {
        maxZoom: option.maxZoom || 20,
        bounds: option.bounds || 50,

    });
    this.cluster.setVisible(true);
    this.cluster.redrawAll(true);
}

MultiPoint.prototype.RemoveCluster = function () {
    this.cluster.removeMarkers(this.markerList, true);
}

MultiPoint.prototype.BackToDefault = function (defaultOption) {
    this.marker.setIcon(new TGOS.TGImage(defaultOption.icon || 'Build/img/marker.png', new TGOS.TGSize((defaultOption.width || 40), (defaultOption.height || 40)), new TGOS.TGPoint(0, 0), new TGOS.TGPoint(20, 15)));
}

MultiPoint.prototype.AddLabel = function (option) {
    var el = this;
    option = option || {};
    this.labelList = [];
    this.pointList.forEach(function (item, index) {
        el.point = new TGOS.TGPoint(item.x, item.y);
        el.superClass.AddLabel.call(el, item.label, option);
        el.labelList.push(el.label);
    });
}

function ClusterPoint(pointList, map) {
    if (!(this instanceof ClusterPoint)) {
        return new ClusterPoint(pointList, map);
    }
    this.map = Object.create(map);
    this.pointList = pointList;
    this.markerList = [];
    this.labelList = [];
}

ClusterPoint.prototype = Object.create(Point.prototype);
ClusterPoint.prototype.constructor = ClusterPoint;
ClusterPoint.prototype.superClass = Object.create(Point.prototype);

ClusterPoint.prototype.ShowClusterPoint = function (eventList) {
    var el = this;
    var eventList = eventList instanceof Array ? eventList.map(function (item) {
        return {
            event: item.event,
            eventCallback: item.eventCallback || function () { },
        };
    }) : [];

    this.pointList.forEach(function (item, index) {
        el.x = item.x;
        el.y = item.y;
        el.ShowMarker({ icon: item.icon || 'Build/img/Circle_Davys-Grey_Solid.svg', IsSetCenter: false, IsSetZoom: false });
        //el.AddLabel();       

        eventList.forEach(function (eventItem) {
            if (eventItem.event) {
                (function (item, index) {
                    el.AddEvent(eventItem.event, function (e) {
                        el.x = item.x;
                        el.y = item.y;
                        el.point = new TGOS.TGPoint(item.x, item.y);
                        el.marker = el.markerList[index];
                        eventItem.eventCallback(item, index, e);
                    });
                })(item, index);
            }
        });
    });
    this.SaveMultiPoint();
}

ClusterPoint.prototype.AddLabel = function (option) {
    var el = this;
    this.pointList.forEach(function (item, index) {
        el.point = new TGOS.TGPoint(item.x, item.y);
        el.superClass.AddLabel.call(el, item.label, option);
        el.labelList.push(el.label);
    });
}

ClusterPoint.prototype.SetInfoWindow = function (content) {
    if (this.infoWindow) {
        this.infoWindow.close();
    }
    this.superClass.SetInfoWindow.call(this, content);
}

ClusterPoint.prototype.SavePoint = function (point, marker) {
    this.markerList.push(marker);
}

ClusterPoint.prototype.SaveMultiPoint = function () {
    this.map.container.ClusterPoint.push(this);
}

//var pointList = [{ x: 121, y: 23 }, { x: 120, y: 23.5 }, { x: 122, y: 25 }];
function Line(pointList, map) {
    if (!(this instanceof Line)) {
        return new Line(pointList, map);
    }
    //this.__proto__ = Object.create(Point.prototype);
    //Object.setPrototypeOf(this, Object.create(Point.prototype));

    this.map = Object.create(map);
    this.pointList = pointList;
}

Line.prototype = Object.create(Point.prototype);
Line.prototype.constructor = Line;

Line.prototype.GetLineString = function (pointList) {
    pointList = pointList || this.pointList;
    pointList = pointList.map(function (item) {
        return new TGOS.TGPoint(item.x, item.y);
    });

    return new TGOS.TGLineString(pointList);
}

Line.prototype.GetBounding = function () {
    var xList = this.pointList.map(function (item) {
        return item.x;
    });
    var yList = this.pointList.map(function (item) {
        return item.y;
    });

    this.bounding = {
        minX: Math.min.apply(null, xList),
        maxX: Math.max.apply(null, xList),
        minY: Math.min.apply(null, yList),
        maxY: Math.max.apply(null, yList)
    };

    return this.bounding;
}

Line.prototype.SetBounding = function () {
    this.GetBounding();
    this.map.mapObject.fitBounds(new TGOS.TGEnvelope(this.bounding.minX, this.bounding.minY, this.bounding.maxX, this.bounding.maxY));
    this.map.mapObject.setZoom(this.map.mapObject.getZoom() - 1);
}

Line.prototype.GetCenter = function () {
    this.GetBounding();
    this.center = {
        x: (this.bounding.minX + this.bounding.maxX) * 0.5,
        y: (this.bounding.minY + this.bounding.maxY) * 0.5,
    };

    return this.center;
}

Line.prototype.ShowLine = function (lineOption) {
    var lineString = this.GetLineString();
    var line = new TGOS.TGLine(this.map.mapObject, lineString,
        lineOption === undefined ? {
        strokeColor: '#00AA88',
        strokeWeight: 5
        } : lineOption);
    this.SetBounding();
    this.map.mapObject.setCenter(this.GetCenter());
    this.map.container.Line.push(line);
}

function Polygon(pointList, map) {
    if (!(this instanceof Polygon)) {
        return new Polygon(pointList, map);
    }

    this.map = Object.create(map);
    this.pointList = pointList;
    this.polygon = new TGOS.TGPolygon([new TGOS.TGLinearRing(this.GetLineString())]);
}

Polygon.prototype = Object.create(Line.prototype);
Polygon.prototype.constructor = Polygon;

Polygon.prototype.ShowPolygon = function (option) {
    option = option || {}
    this.polygon = new TGOS.TGPolygon([new TGOS.TGLinearRing(this.GetLineString())]);
    this.fill = new TGOS.TGFill(this.map.mapObject, this.polygon, {
        fillColor: option.fillColor || '#00FFFF',
        fillOpacity: option.fillOpacity || 0.5,
        strokeColor: option.strokeColor || '#00FF00',
        strokeWeight: option.strokeWeight || 3,
        strokeOpacity: option.strokeOpacity || 0.5
    });

    this.SetBounding();
    this.map.mapObject.setCenter(this.GetCenter());
    this.map.container.Polygon.push(this.fill);
}

//客製化圖層 by TGOS TGPolyGon使用
Polygon.prototype.ShowPolygonByOwner = function (option,poly) {
    option = option || {}
    this.polygon = poly;
    map.ClearPolygon();
    this.fill = new TGOS.TGFill(map.mapObject, this.polygon, {
        fillColor: option.fillColor || '#00FFFF',
        fillOpacity: option.fillOpacity || 0.5,
        strokeColor: option.strokeColor || '#00FF00',
        strokeWeight: option.strokeWeight || 3,
        strokeOpacity: option.strokeOpacity || 0.5
    });
    var e = poly.getEnvelope();
    map.mapObject.fitBounds(e);
    map.mapObject.setCenter(map.mapObject.getCenter());
    map.container.Polygon.push(this.fill);
}

Polygon.prototype.LocateByDistrict = function (district, callback) {
    var locate = new Locate(this.map);
    var callback = callback || function () { }
    var el = this;
    var locateStatus = {
        IsOk: false,
        errorMessage: ''
    };

    locate.ByDistrict(district, function (result) {
        if (result.errorMessage) {
            locateStatus.errorMessage = result.errorMessage;
            locateStatus.IsOk = false;
        } else {
            el.pointList = result.pointList;
            el.ShowPolygon();
            locateStatus.IsOk = true;
        }

        callback(locateStatus);
    });
}

Polygon.prototype.AddMultiPolygon = function (pointList) {
    var lineString = this.GetLineString(pointList);
    this.polygon.addInterior(new TGOS.TGLinearRing(lineString));
    this.fill.setMap(null);
    this.fill = new TGOS.TGFill(this.map.mapObject, this.polygon, {
        fillColor: '#00FFFF',
        fillOpacity: 0.5,
        strokeColor: '#00FF00',
        strokeWeight: 3,
        strokeOpacity: 0.5
    });
    this.map.container.Polygon.push(this.fill);

}

Polygon.prototype.AddLabel = function (text, option) {
    var pt = this.GetCenter();
    var point = new TGOS.TGPoint(pt.x, pt.y);
    option = option || {};
    this.label = new TGOS.TGLabel({
        map: this.map.mapObject,
        fontColor: option.fontColor || "#ff0100",	//字型顏色
        fontSize: option.fontSize || 18,		    //字體大小
        font: option.font || "微軟正黑體",	        //字型
        fontWeight: option.fontWeight || "bold",	//字體粗細
        opacity: option.opacity || 0.8,			//透明度
        position: point,			                //註記位置
        label: text,
    });
    this.map.container.Label.push(this.label);
}

//var multiPointList = [[{ x: 121, y: 23 }, { x: 121, y: 24 }], [{ x: 121, y: 23 }, { x: 121, y: 25 }]];
function MultiPolygon(multiPointList) {
}




function Circle(point, radius, map) {
    if (!(this instanceof Circle)) {
        return new Circle(point, radius, map);
    }
    this.map = Object.create(map);
    this.x = point.x;
    this.y = point.y;
    this.point = new TGOS.TGPoint(this.x, this.y);
    this.radius = radius;
}

Circle.prototype = Object.create(Point.prototype);
Circle.prototype.constructor = Circle;

Circle.prototype.ShowBuffer = function (option) {
    var circle = new TGOS.TGCircle();	//建立一個圓形物件(TGCircle)
    circle.setCenter(this.point);				//以滑鼠點擊位置為圓心
    circle.setRadius(this.radius);			//設定半徑
    option = option || {};

    if (option.zoomIn)
        this.map.mapObject.setZoom(option.zoomIn);
    if (option.IsSetCenter)
        this.map.mapObject.setCenter(this.point);
    this.circle = new TGOS.TGFill(this.map.mapObject, circle, {	//設定圖形樣式
        fillColor: option.color || "#0099FF",
        fillOpacity: option.opacity || 0.1,
        strokeWeight: 2,
        strokeColor: "#444444"
    });
    this.SaveCircle(this);
}

Circle.prototype.ShowCenter = function (option) {
    this.ShowMarker(option);
}

Circle.prototype.SaveCircle = function (el) {
    this.map.container.Circle.push(el);
}

Circle.prototype.SavePoint = function (point, marker) {
    this.center = marker;
}

Circle.prototype.AddLabel = function (text, option) {
    option = option || {};
    this.label = new TGOS.TGLabel({
        map: this.map.mapObject,
        fontColor: option.fontColor || "#ff0100",	//字型顏色
        fontSize: option.fontSize || 18,		    //字體大小
        font: option.font || "微軟正黑體",	        //字型
        fontWeight: option.fontWeight || "bold",	//字體粗細
        opacity: option.opacity || 0.8,			//透明度
        position: this.point,			                //註記位置
        label: text
    });
    this.map.container.Label.push(this.label);
}


function Drawing(map, drawingMode, option) {
    if (!(this instanceof Drawing)) {
        return new Drawing(map, drawingMode, option);
    }
    this.map = Object.create(map);
    this.drawingMode = drawingMode;
    this.option = option || {}
}

Drawing.prototype.Start = function (completeCallback) {
    var drawingModeList = ["CIRCLE", "MARKER", "POLYGON", "LINESTRING", "ENVELOPE", "LABEL", "NONE"];
    this.map.ClearDrawing();
    this.drawing = new TGOS.TGDrawing();

    if (drawingModeList.indexOf(this.drawingMode.toUpperCase()) !== -1) {
        this.drawing.setMap(this.map.mapObject);
        this.drawing.setDrawingMode(this.drawingMode.toUpperCase());
        this.drawing.setOptions(this.option);
        this.listener = TGOS.TGEvent.addListener(this.drawing, this.drawingMode.toLowerCase() + '_complete', function (e) {
            completeCallback(e);
            this.setDrawingMode("NONE");
        });
        this.SaveDrawing();

    } else {
        console.log(new Error('invalid drawingMode.'));
    }
}

Drawing.prototype.StartWithAdapter = function (adapter, completeCallback) {
    adapter = adapter instanceof Function ? adapter : function (e) {
        console.log(new TypeError('adapter is not a Function.'));
        return e;
    }
    this.Start(function (e) {
        completeCallback(adapter(e));
    });
}

Drawing.prototype.SaveDrawing = function () {
    this.map.container.Drawing.push(this);
}



