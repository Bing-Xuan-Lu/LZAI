export const CleColumns = vThis => [
    {
        field: 'CleId',
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
        field: 'Time',
        title: '時間',
        visible: false
    },
    {
        field: 'CleNumber',
        title: '事業編號'
    }, 
    {
        field: 'Cle_Name',
        title: '事業名稱'
    },
    {
        field: 'Gate_Long',
        title: '經度',
        visible: false,
    },
    {
        field: 'Gate_Lat',
        title: '緯度',
        visible: false,
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
                vThis.CenterData(row);
                //alert(JSON.stringify(row))
            }
        }
    }
];

export const CleData = [
    {
        CleId: 1,
        Time: '08:00',
        CleNumber: '000001',
        Cle_Name: 'OO公司0'
    },
]