import { CleColumns, CleData } from './CleInfo.js';
import { CleStopRegionColumns, CleStopRegionData } from './CleStopRegion.js';
import { HistoryTrajectory, ClearHistory, TimeLine } from './GPSMapFunc/TGOS/GPSHistory.js'

let TGmap = null;
let CarWatch = null;
$(function () {
    let carWatchHub = $.connection.CarWatchHub;
    CarWatch = new Vue({
        el: '#CarWatch',
        data() {
            var vThis = this;
            return {
                carInfo: {
                    HeadNo: '',
                    Plate_No: '',
                    HeadImageUrl: 'images/NoImage.png',
                    Status: 0,
                    StatusClass: function () {
                        var classResult = '';
                        switch (this.Status) {
                            case 0:
                                classResult = 'bg-secondary';
                                break;
                            case 1:
                                classResult = 'bg-success';
                                break;
                            default:
                                classResult = 'bg-danger';
                        }
                        return classResult;
                    }
                },
                tmpEvent: {},//最後一次的車牌辨識紀錄
                HisTroyEventId: vThis.GetHisTroyEventId(),//車牌辨識記錄重演事件使用
                HisTroyEventDate: '',
                HisPlate_noList: [],
                LaneInfo: {
                    CarLaneNum: 1
                },
                tables: {
                    CleInfoColumns: CleColumns(vThis),
                    CleInfoData: CleData,
                    CleInfoMessage: '',
                    CleStopRegionColumns: CleStopRegionColumns,
                    CleStopRegionData: CleStopRegionData,
                    CleStopRegionMessage: '',
                    BSTableOptions: {
                        search: true,
                        sortable: true,
                        pagination: true,
                        pageList: "[5, 10, 15, 20, all]"
                    }
                }
                , HandEventInfo: {
                    DateTime: (new Date()).toLocaleString(),
                    IsImage: true,
                    CarImg: '#',
                    NoImgUrl: '',
                    Head_No: '',
                    Plate_No: '',
                    Plate_noTxt: '',
                    WatchId: 0,
                    Lane: 1,
                },
            }
        },
        components: {
            'BootstrapTable': BootstrapTable
        },
        methods: {
            GetCarResult() {
                let apiRoute = this.HisTroyEventId === 0
                    ? "/api/CarWatch/CarWatchEvent?Lane=" + this.LaneInfo.CarLaneNum
                    : "/api/CarWatch/CarWatchHisTroyEvent?eventId=" + this.HisTroyEventId;
                window.fetch(apiRoute,
                    {
                        method: 'GET',
                        headers: _TokenHeader
                    })
                    .then(response => response.json())
                    .then(json => {
                        this.RefreshCarInfo(json.CarWatchEvent);
                        this.$set(this.tables, 'CleInfoData', json.CarWatchCleGrantInfos);
                        this.$set(this.tables, 'CleStopRegionData', json.CarWatchStopRegionFiles);
                        this.$set(this.carInfo, 'Status', json.Status);
                        if (json.CarWatchEvent.IsSetGPS) {
                            this.GetCarHistory(json.CarWatchEvent).then(response => {
                                this.ShowCleInfo(json.CarWatchCleGrantInfos);
                            });
                        }
                    });
            }
            , GetCarHistory(data) {

                return window.fetch("/api/CarWatch/GpsHistoryData",
                    {
                        method: 'POST',
                        headers: {
                            _TokenHeader,
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(data)
                    })
                    .then(response => response.json())
                    .then(json => {
                        if (json.HistoryJsonData !== null && json.HistoryJsonData.length > 0) {
                            HistoryTrajectory(json.HistoryJsonData,
                                (json.WasteCarInfo.Head_No || '無') + '/' + (json.WasteCarInfo.Plate_No || '無'),
                                TGmap);
                            $('#timePlay button').on('click', function () { TimeLine(); });
                        } else {
                            ClearHistory(TGmap);
                        }
                    })
                    .catch(error => console.error('Error:', error));
            }
            , ChangeLane(laneNo) {
                this.$set(this.LaneInfo, 'CarLaneNum', laneNo);
                this.ClearCarInfo();
                this.GetCarResult();
            }
            , RefreshCarInfo(data) {
                this.$set(this.carInfo, 'HeadNo', data.Head_No);
                this.$set(this.carInfo, 'Plate_No', data.Plate_No);
                this.$set(this.carInfo, 'HeadImageUrl', data.CarImg);
                this.$set(this.carInfo, 'Status', 0);

                this.$set(this.tables, 'CleStopRegionData', []);
                this.$set(this.tables, 'CleInfoData', []);
                this.$set(this, 'tmpEvent', data);
                this.$set(this, 'HisTroyEventDate', data.InsTime);
                if (data.IsSetGPS) {
                    ClearHistory(TGmap);
                }
            }
            , ClearCarInfo() {
                this.$set(this.carInfo, 'HeadNo', '');
                this.$set(this.carInfo, 'Plate_No', '');
                this.$set(this.carInfo, 'HeadImageUrl', '');
                this.$set(this.carInfo, 'Status', 0);
                this.$set(this.tables, 'CleStopRegionData', []);
                this.$set(this.tables, 'CleInfoData', []);
                ClearHistory(TGmap);
            }
            , ShowCleInfo(cleInfo) {
                var option = {
                    color: '#00DD00',
                }
                cleInfo.forEach(x => {
                    var point = new Point({
                        x: x.Gate_Long,
                        y: x.Gate_Lat,
                    }, TGmap);
                    var cc = new Circle(point, 100, TGmap);
                    cc.ShowBuffer(option);
                    cc.AddLabel(x.Cle_Name, { fontSize: 12 });
                });
            }
            , CenterData(obj) {
                //經緯度置中
                TGmap.SetCenter({
                    x: obj.Gate_Long,
                    y: obj.Gate_Lat,
                });
                TGmap.SetZoom(14);
            }
            , GetWasteCar() {
                //車輛選單
                window.fetch("/api/GPSData/GetCleWasteCar",
                    {
                        headers: _TokenHeader
                    })
                    .then(response => response.json())
                    .then(json => {
                        this.$set(this, 'HisPlate_noList', json);
                        $("#ddlHisPlate_no").select2({
                            allowClear: true,
                            minimumInputLength: 0,
                            width: 250,
                            placeholder: "請選擇車輛號碼"
                        });
                    })
                    .catch(error => console.error('Error:', error));
            }
            , ManualEvent(el) {
                if (SweetAlertCheck(el.target, '新增', '點選確認後，將會手動新增一筆車牌辨識', '!')) {
                    if (!this.HandEventInfo.IsImage) {
                        this.$set(this.HandEventInfo, 'CarImg', '');
                    }
                    //手動查詢輸入事件
                    window.fetch("/api/CarWatch/ManualEvent",
                        {
                            method: 'POST',
                            headers: {
                                _TokenHeader,
                                'Content-Type': 'application/json'
                            },
                            body: JSON.stringify(this.HandEventInfo)
                        })
                        .then(response => response.json())
                        .then(json => {

                            var elem = this.$refs.closeModal;
                            elem.click();
                        })
                        .catch(error => console.error('Error:', error));
                }

            }
            , GetHisTroyEventId() {
                const params = new Proxy(new URLSearchParams(window.location.search),
                    {
                        get: (searchParams, prop) => searchParams.get(prop),
                    });
                let value = params.EventId; // "some_value"
                value = value !== null ? window.decodeURIComponent(window.atob(value)) : 0; //解密
                return value;
            }
        },
        beforeCreate: function () {

        },
        created: function () {
            var vThis = this;
            vThis.GetCarResult();
            //歷史事件重演不開即時刷新功能
            if (vThis.HisTroyEventId === 0) {
                vThis.GetWasteCar();
                //如果結果出來與選取車道不同，辨識後就不再做全頻廣播了
                let isSameLane = false;

                carWatchHub.client.hello = () => {
                    console.log("Server Hello");
                };
                // Create a function that the hub can call to broadcast messages.
                carWatchHub.client.updateMessages = function (carWatchEvent) {
                    if (carWatchEvent.Lane === vThis.LaneInfo.CarLaneNum &&
                        vThis.tmpEvent.EventId !== carWatchEvent.EventId) {
                        isSameLane = true;
                        vThis.RefreshCarInfo(carWatchEvent, true);
                        vThis.$set(vThis, 'tmpEvent', carWatchEvent); //防重複廣播機制
                        console.log(carWatchEvent);
                    }
                };
                //歷史軌跡Hub回覆
                carWatchHub.client.GetGpsData = function (gpsData) {
                    if (isSameLane && gpsData.HistoryJsonData !== null && gpsData.HistoryJsonData.length > 0) {
                        HistoryTrajectory(gpsData.HistoryJsonData,
                            (gpsData.WasteCarInfo.Head_No || '無') + '/' + (gpsData.WasteCarInfo.Plate_No || '無'),
                            TGmap);
                        $('#timePlay button').on('click', function () { TimeLine(); });
                    } else {
                        ClearHistory(TGmap);
                    }
                }

                //事業點Hub回覆
                carWatchHub.client.GetCleGrantInfo = function (cleData) {
                    if (isSameLane) {
                        vThis.$set(vThis.tables, 'CleInfoData', cleData);
                        vThis.ShowCleInfo(cleData);
                        vThis.$set(vThis.carInfo, 'Message', '事業點分析完成');
                    }
                }

                //警示區Hub回覆
                carWatchHub.client.GetStopRegion = function (regionData) {
                    if (isSameLane) {
                        vThis.$set(vThis.tables, 'CleStopRegionData', regionData);
                        vThis.$set(vThis.carInfo, 'Message', '警示區分析完成');
                    }
                }

                //計算後狀態Hub回覆
                carWatchHub.client.UpdateCarStatus = function (status) {
                    if (isSameLane) {
                        vThis.$set(vThis.carInfo, 'Status', status);
                        vThis.$set(vThis.carInfo, 'Message', '計算車牌辨識分析結果完成');
                    }
                }
                // Start the connection. client Js要先建置
                $.connection.hub.logging = true;
                $.connection.hub.start().done(function () {
                    console.log("connection started");
                    carWatchHub.server.CarWatchListenIng().done(function () {
                        console.log("Hello 監聽車牌事件");
                        console.log($.connection.hub.id);
                    }).fail(function (e) {
                        console.log(e);
                    });

                }).fail(function (e) {
                    console.log(e);
                });
            }
        },
        mounted: function () {
            var vThis = this;
            this.$nextTick(() => {
                document.getElementById('handModal').addEventListener('shown.bs.modal',
                    () => {
                        //取得最後一張辨識的照片
                        window.fetch("/api/CarWatch/GetLastCarImg",
                            {
                                headers: _TokenHeader
                            })
                            .then(response => response.json())
                            .then(json => {
                                vThis.$set(vThis.HandEventInfo, 'CarImg', json.image);
                                vThis.$set(vThis.HandEventInfo, 'WatchId', json.sid);
                                vThis.$set(vThis.HandEventInfo, 'Lane', json.channel_id);
                            })
                            .catch(error => console.error('Error:', error));
                        vThis.$set(vThis.HandEventInfo, 'Plate_noTxt', $('#ddlHisPlate_no option:selected').text());
                        var car_arr = $('#ddlHisPlate_no option:selected').text().split('/');
                        vThis.$set(vThis.HandEventInfo, 'Head_No', car_arr[0] === '無' ? '' : car_arr[0]);
                        vThis.$set(vThis.HandEventInfo, 'Plate_no', car_arr[1] === '無' ? '' : car_arr[1]);
                    });
            });
        },
        updated: function () {
            console.log('updated');
        }
    });
    TGmap = new Maps(option);
    console.log('main JS');
    MapIconFunc.Init();
    //滑鼠座標匯入
    pointEvent(TGmap);
});
export { TGmap, CarWatch }
