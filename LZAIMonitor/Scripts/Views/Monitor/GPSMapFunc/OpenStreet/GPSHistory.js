export const HistoryRoute = function (wkt) {
    //TODO:OSM歷史軌跡 因時間不足，未完成，僅能播放動畫，沒有其他功能
    //wkt只接受LineString
    var feature = new ol.format.WKT().readFeature(wkt).getGeometry();
    var routeCoords = feature.getCoordinates();
    var routeLength = routeCoords.length;

    var routeFeature = new ol.Feature({
        type: 'route',
        geometry: feature
    });
    //放軌跡球
    var startMarker = new ol.Feature({
        type: 'icon',
        geometry: new ol.geom.Point(routeCoords[0])
    });
    var endMarker = new ol.Feature({
        type: 'icon',
        geometry: new ol.geom.Point(routeCoords[routeLength - 1])
    });
    const position = startMarker.getGeometry().clone();
    var geoMarker = new ol.Feature({
        type: 'geoMarker',
        geometry: position
    });
    //Marker樣式
    var styles = {
        'route': new ol.style.Style({
            stroke: new ol.style.Stroke({
                width: 6,
                color: [237, 212, 0, 0.8]
            })
        }),
        'icon': new ol.style.Style({
            image: new ol.style.Icon({
                anchor: [0.5, 1],
                src: 'https://tygps.tycg.gov.tw/TGOSGisWeb/Images/factory.png'
            })
        }),
        'geoMarker': new ol.style.Style({
            image: new ol.style.Icon({
                anchor: [0.5, 0.5],
                src: '../images/Car/B/Not/car_b.png',
                scale: 0.6,
                rotation: -Math.atan2(routeCoords[0][1] - routeCoords[1][1], routeCoords[0][0] - routeCoords[1][0]),
                rotateWithView: true
            }),
        })
    };
    var speed, startTime;
    const speedInput = document.getElementById('speed');
    const startButton = document.getElementById('start-animation');
    let animating = false;

    var vectorLayer = new ol.layer.Vector({
        source: new ol.source.Vector({
            features: [routeFeature, geoMarker, startMarker, endMarker]
        }),
        style: function (feature) {
            if (animating && feature.get('type') === 'geoMarker') {
                return null;
            }
            return styles[feature.get('type')];
        }
    });
    olMap.addLayer(vectorLayer);

    function moveFeature(event) {
        // console.log(event)
        var vectorContext = event.vectorContext;
        var frameState = event.frameState;
        var carStyle, rotation;
        if (animating) {
            var elapsedTime = frameState.time - startTime;
            var index = Math.round(speed * elapsedTime / 1000);
            if (index >= routeCoords.length) {
                stopAnimation(true);
                return;
            }
            if (routeCoords[index] && routeCoords[index + 1]) {
                // console.log(route.A[index],route.A[index+1])
                let x = routeCoords[index][0] - routeCoords[index + 1][0];
                let y = routeCoords[index][1] - routeCoords[index + 1][1];
                // 路線弧度
                rotation = Math.atan2(y, x);
                //console.log("角度：" + rotation);
                //車輛圖標
                carStyle = new ol.style.Style({
                    image: new ol.style.Icon({
                        src: '../images/Car/B/Not/car_b.png',
                        rotateWithView: false,
                        rotation: (-rotation + (Math.atan2(routeCoords[0][1] - routeCoords[1][1], routeCoords[0][0] - routeCoords[1][0]) / 2)),
                        scale: 0.6,
                        anchor: [0.5, 0.5],
                    })
                });
                var currentPoint = new ol.geom.Point(routeCoords[index]);
                var feature = new ol.Feature(currentPoint);
                vectorContext.drawFeature(feature, carStyle);
            }

        }
        olMap.render();
    }

    function startAnimation() {
        // console.log(animating)
        if (animating) {
            stopAnimation(false);
        } else {
            animating = true;
            startTime = new Date().getTime();
            speed = speedInput.value;
            startButton.textContent = 'Cancel Animation';
            geoMarker.changed();
            // just in case you pan somewhere else
            olMap.on('postcompose', moveFeature);
            olMap.render();
        }
    }
    function stopAnimation(ended) {
        animating = false;
        startButton.textContent = 'Start Animation';
        // if animation cancelled set the marker at the beginning
        var coord = feature.getCoordinateAt(ended ? 1 : 0);
        geoMarker.getGeometry().setCoordinates(coord);
        // remove listener
        olMap.un('postcompose', moveFeature);
    }

    startButton.addEventListener('click', function () {
        if (animating) {
            stopAnimation();
        } else {
            startAnimation();
        }
    });

    var stylesFunction = function (feature) {
        let resolution = olMap.getView().getResolution();
        var geometry = feature.getGeometry();
        var length = geometry.getLength();
        var radio = (100 * resolution) / length; //間隔
        var dradio = 100000000;
        // 線條樣式
        var styles = [
            new ol.style.Style({
                stroke: new ol.style.Stroke({
                    color: "orange",
                    width: 6,
                })
            })
        ];

        // 增加箭頭樣式
        for (var i = 0; i <= 1; i += radio) {
            var arrowLocation = geometry.getCoordinateAt(i);
            geometry.forEachSegment(function (start, end) {
                // if (start[0] == end[0] || start[1] == end[1]) return; 
                var dx1 = end[0] - arrowLocation[0];
                var dy1 = end[1] - arrowLocation[1];
                var dx2 = arrowLocation[0] - start[0];
                var dy2 = arrowLocation[1] - start[1];
                if (dx1 != dx2 && dy1 != dy2) {
                    if (Math.abs(dradio * dx1 * dy2 - dradio * dx2 * dy1) < 0.001) {
                        var dx = end[0] - start[0];
                        var dy = end[1] - start[1];
                        var rotation = Math.atan2(dy, dx); // 得到箭頭方向角度
                        styles.push(
                            new ol.style.Style({
                                geometry: new ol.geom.Point(arrowLocation),
                                image: new ol.style.Icon({
                                    src: '../images/GPS_detail/gps_direction_1.gif',
                                    anchor: [0.75, 0.5],
                                    scale: 1,
                                    rotateWithView: false,
                                    rotation: -rotation

                                })
                            })
                        );
                    }
                }
            });
        }
        return styles;
    }
}