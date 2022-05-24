import { GPSIcon } from './TrackCarIcon.js'
function RealTimePosition(row) {
    //TODO:OSM即時軌跡 因時間不足，未完成，僅能定位展圖，沒有其他功能
    //row為gps_realtime資訊
    olMap.getView().animate({
        center: [row.TM97X, row.TM97Y],
        zoom: 15,
        rotation: undefined,
        duration: 1000
    });
    const pos = ol.proj.fromLonLat([row.TM97X, row.TM97Y]);
    var iconFeature = new ol.Feature({
        geometry: new ol.geom.Point([row.TM97X, row.TM97Y]),
        name: 'Null Island',
        population: 4000,
        rainfall: 500
    });

    var iconStyle = new ol.style.Style({
        image: new ol.style.Icon(/** @type {olx.style.IconOptions} */({
            anchor: [0.5, 46],
            anchorXUnits: 'fraction',
            anchorYUnits: 'pixels',
            src: '../' + GPSIcon.CarIcon('Not', parseFloat(row.Heading), parseInt(row.IO1), parseInt(row.Speed))
        }))
    });
    iconFeature.setStyle(iconStyle);
    var vectorSource = new ol.source.Vector({
        features: [iconFeature]
    });
    var markerVectorLayer = new ol.layer.Vector({
        source: vectorSource,
    });
    olMap.addLayer(markerVectorLayer);

    const container = document.getElementById('popup');
    const content = document.getElementById('popup-content');
    const closer = document.getElementById('popup-closer');
    var popup = new ol.Overlay({
        element: container,
        stopEvent: false,
        offset: [0, -50]
    });
    olMap.addOverlay(popup);

    closer.onclick = function () {
        popup.setPosition(undefined);
        closer.blur();
        return false;
    };

    olMap.on('click', function (evt) {
        var feature = olMap.forEachFeatureAtPixel(evt.pixel,
            function (feature) {
                return feature;
            });
        if (feature) {
            var coordinates = feature.getGeometry().getCoordinates();
            popup.setPosition(coordinates);
            content.innerHTML = '<p>You clicked here:</p><code>555</code>';
        } else {
            $(container).popover('hide');
        }
    });
}
export { RealTimePosition };