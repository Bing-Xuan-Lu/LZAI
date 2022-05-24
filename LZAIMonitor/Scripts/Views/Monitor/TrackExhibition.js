export function TrackExhibition(TGmap) {
    const simulateAsyncOperation = fn => {
        setTimeout(fn, 500);
    }
    var ExhibitionPoint = null || [];
    return new Vue({
        el: "#Exhibition",
        components: {
            'treeselect': VueTreeselect.Treeselect
        },
        data: () => ({
            value: [],
            options: null,
        }),
        methods: {
            async loadOptions({ action, parentNode, callback }) {
                // Typically, do the AJAX stuff here.
                // Once the server has responded,
                // assign children options to the parent node & call the callback.

                if (action === VueTreeselect.LOAD_ROOT_OPTIONS) {
                    simulateAsyncOperation(() => {
                        window.fetch("/api/GPSData/GetCleInfoTree",
                                {
                                    method: 'GET',
                                    headers: {
                                        _TokenHeader,
                                        'Content-Type': 'application/json'
                                    }
                                })
                            .then(response => response.json())
                            .then(json => {
                                this.options = json;
                            });
                        callback();
                    });
                }
            }
            , ShowCleInfoExhibition() {
                var params = this.value.map(item => ({ id: item.id, IsCity: item.IsCity }));
                var multiPointIndex = TGmap.container.MultiPoint.findIndex(e => e.note === '主題展點');
                if (multiPointIndex > 0)
                    TGmap.ClearMultiPoint(multiPointIndex);
                if (params.length > 0) {
                    window.fetch("/api/GPSData/ShowCleInfoExhibition",
                            {
                                method: 'POST',
                                headers: {
                                    _TokenHeader,
                                    'Content-Type': 'application/json'
                                },
                                body: JSON.stringify(params)
                            })
                        .then(response => response.json())
                        .then(json => {
                            ExhibitionPoint = [];
                            if (json.length > 0) {
                                json.forEach((item, key) => {
                                    ExhibitionPoint.push(
                                        {
                                            ...item,
                                            icon: '/images/factory.png',
                                            x: item.Gate_Long,
                                            y: item.Gate_Lat
                                        });
                                });

                                var multiPoint = new MultiPoint(ExhibitionPoint, TGmap);
                                multiPoint.note = "主題展點";
                                multiPoint.ShowMultiPoint({
                                        width: 30,
                                        height: 30,
                                        PointWidth: 7,
                                        PointHeight: 7,
                                        IsDifferentIcon: true
                                    },
                                    [
                                        {
                                            event: 'click',
                                            eventCallback: function(item, key) {
                                                multiPoint.SetInfoWindow(infoWindowStrContent(item));
                                                multiPoint.OpenInfoWindow();
                                            }
                                        }
                                    ]);
                            }
                        });
                }
            }
        }
        , mounted: function () { console.log('Exhibition ok'); },
        updated: function () { }
    });
}

function infoWindowStrContent(item) {
    //資訊視窗
    var StrContent = '';
    StrContent += '<table width="400" style="text-align:center;font-size: 17px;margin-left: 9px;height: 250px;">';
    StrContent += '<tr class="InfoWindowTrTitle" ><td colspan="2" height="35px" style="padding: 5px;"><a href="#" target="_blank" style="color: white;">' + item.Cle_No + '</a></td></tr>';
    StrContent += '<tr class="InfoWindowTrContent"><td style="padding: 5px;">事業代碼:</td><td style="padding: 5px 10px;">' + item.CleNumber + '</td></tr>';
    StrContent += '<tr class="InfoWindowTrContent"><td style="padding: 5px;">事業名稱:</td><td style="padding: 5px 10px;">' + item.Cle_Name + '</td></tr>';
    StrContent += '<tr class="InfoWindowTrContent"><td style="padding: 5px;">地址:</td><td style="padding: 5px 10px;">' + item.Addr + '</td></tr>';
    StrContent += '<tr class="InfoWindowTrContent"><td style="padding: 5px;">經緯度:</td><td style="padding: 5px 10px;"><a href="https://www.google.com/maps?q=loc:' + item.Gate_Lat + ',' + item.Gate_Long + '&z=15" target="_blank">' + item.Gate_Lat + "&nbsp; &nbsp;" + item.Gate_Long + '</a></td></tr>';
    StrContent += '</table>';
    return StrContent;
}

