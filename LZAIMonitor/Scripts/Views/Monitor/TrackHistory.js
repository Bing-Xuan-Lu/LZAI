import { HistoryTrajectory, ClearHistory, TimeLine  } from './GPSMapFunc/TGOS/GPSHistory.js'
export function TrackHistory(TGmap) {
    return new Vue({
        el: "#History",
        data: {
            queryFilter: {
                HisPlate_noList: [],
                HisDate: '',
                HisTimeStart: '',
                HisTimeEnd: ''
            },
            historyResult: {
                HeadNo: '',
                Plate_No: '',
                Fac_No: '',
                Fac_Name: ''
            }
        },
        methods: {
            GetCarHistory() {
                if (this.queryFilter.HisDate === '' || this.queryFilter.HisTimeStart === '' || this.queryFilter.HisTimeEnd === '') {
                    alert('請輸入查詢日期時間');
                    return false;
                }
                return window.fetch("/api/GPSData/GpsHistoryData",
                    {
                        method: 'POST',
                        headers: {
                            _TokenHeader,
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(
                            {
                                PlateNo: $('#ddlHisPlate_no').val(),
                                CleDate: this.queryFilter.HisDate,
                                StartTime: this.queryFilter.HisTimeStart,
                                EndTime: this.queryFilter.HisTimeEnd
                            })
                    })
                    .then(response => response.json())
                    .then(json => {
                        this.$set(this.historyResult, 'HeadNo', json.WasteCarInfo.Head_No);
                        this.$set(this.historyResult, 'Plate_No', json.WasteCarInfo.Plate_No);
                        this.$set(this.historyResult, 'Fac_No', json.WasteCarInfo.Fac_No);
                        this.$set(this.historyResult, 'Fac_Name', json.WasteCarInfo.Fac_Name);
                        HistoryTrajectory(json.HistoryJsonData
                            , (json.WasteCarInfo.Head_No || '無') + '/' +(json.WasteCarInfo.Plate_No || '無')
                            , TGmap);
                        $('#timePlay button').on('click', function () { TimeLine(); });
                    })
                    .catch(error => console.error('Error:', error));
            },
        },
        created: function () {
            //車輛選單
            window.fetch("/api/GPSData/GetCleWasteCar",
                {
                    headers: _TokenHeader
                })
                .then(response => response.json())
                .then(json => {
                    this.$set(this.queryFilter, 'HisPlate_noList', json);
                    $("#ddlHisPlate_no").select2({
                        allowClear: true,
                        minimumInputLength: 0,
                        width: 250,
                        placeholder: "請選擇車輛號碼"
                    });
                })
                .catch(error => console.error('Error:', error));
        }
        , mounted: function () { console.log('HisTroy ok'); },
        updated: function () { }
    });
}