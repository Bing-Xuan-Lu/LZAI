import { TGmap } from './CarWatch.js'

export const CleStopRegionColumns = [
    {
        field: 'ID',
        title: '編號',
        visible: false
    },
    {
        title: '編號',
        formatter: function (value, row, index) {
            return index + 1;
        }
    },
    {
        field: 'Note',
        title: '類別'
    },
    {
        field: 'RegionName',
        title: '警示區'
    },
    {
        field: 'action',
        title: '定位',
        align: 'center',
        formatter: function () {
            return '<a href="javascript:" class="like"><i class="fa fa-star"></i></a>'
        },
        events: {
            'click .like': function (e, value, row) {
                ShowStopRegion(row.ID, row.RegionName);
            }
        }
    }
];

export const CleStopRegionData =
    [
        {
            Sno: 0,
            Time: '08:00',
            RegionName: 'OO警示區0'
        },
    ];

//警示區展示
function ShowStopRegion(SSRID, regionName) {
    window.fetch("/api/CarWatch/GetStopRegionLocation",
        {
            method: 'POST',
            headers: {
                _TokenHeader,
                'Content-Type': 'application/json'
            },
            body: SSRID
        })
        .then(response => response.json())
        .then(response => {
            if (response.length > 0) {
                var DrawPoints = [];
                if (response.length === 1) {
                    var obj = {
                        x: response[0].WGS_LAT,
                        y: response[0].WGS_LON
                    }
                    var point = new Point(obj, TGmap);
                    point.ShowMarker({
                        icon: '../../images/StopRegion/blue.png',
                        IsSetCenter: false,
                        IsSetZoom: false,
                        width: 30,
                        height: 30
                    });
                } else {
                    var RegionResult = response.groupBy("RegionCount");
                    $.each(RegionResult,
                        function (key, region) {
                            //個別組出一個一個Polygon
                            $.each(region,
                                function (key, item) {
                                    var point = {
                                        x: item.WGS_LAT,
                                        y: item.WGS_LON
                                    };
                                    DrawPoints.push(point);
                                });
                            var polygonOptions = {
                                fillColor: '#CCFF99',
                                fillOpacity: 0.3,
                                strokeWeight: 3,
                                strokeColor: '#008844',
                                strokeOpacity: 0.5
                            }
                            var polygon = new Polygon(DrawPoints, TGmap);
                            polygon.ShowPolygon(polygonOptions);
                            polygon.AddLabel(regionName, { opacity: 3 });
                        });
                }
            }
        });
}