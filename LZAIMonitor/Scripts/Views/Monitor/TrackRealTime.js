import { NowTrajectory } from './GPSMapFunc/TGOS/GPSRealtime.js'
export function TrackRealTime(TGmap) {
    return new Vue({
        el: "#RealTime",
        components: {
            'BootstrapTable': BootstrapTable
        },
        data: {
            plate_no: ''
            , RealTimeCarColumns: [
                {
                    field: 'Sno',
                    title: '編號',
                    visible: false
                },
                {
                    field: 'CleHandPlate_no',
                    title: '前車牌/後車牌'
                },
                {
                    field: 'WGS_LON',
                    title: '經度'
                    , visible: false
                },
                {
                    field: 'WGS_LAT',
                    title: '緯度'
                    , visible: false
                },
                {
                    field: 'DateTime',
                    title: '時間'
                    , visible: false
                },
                {
                    field: 'Speed',
                    title: '速度'
                    , visible: false
                },
                {
                    field: 'Sat',
                    title: '衛星數'
                    , visible: false
                },
                {
                    field: 'Heading',
                    title: '方向'
                    , visible: false
                },
                {
                    field: 'IO1',
                    title: '發動情形'
                    , visible: false
                }, 
                {
                    field: 'action',
                    title: '定位',
                    align: 'center',
                    formatter: function () {
                        return '<button class="position btn btn-primary">定位</button>';
                    },
                    events: {
                        'click .position': function (e, value, row) {
                            NowTrajectory(row,TGmap);
                        }
                    }
                }
            ]
            , BSTableOptions: {
                search: true,
                sortable: true
            }
            , RealTimeCarData: []
        },
        methods: {
            GetRealTimeData() {

                return window.fetch("/api/GPSData/GpsRealTimeData?plateNo=" + this.plate_no,
                    {
                        method: 'POST',
                        headers: {
                            _TokenHeader,
                            'Content-Type': 'application/json'
                        }
                    })
                    .then(response => response.json())
                    .then(json => {
                        this.$set(this, 'RealTimeCarData', json);
                    });
            }
        }
        , mounted: function () { console.log('RealTime ok'); },
        updated: function () {}
    });
}