
let searchCondition = {};

//queryTableObj 傳入 以Table id 命名的物件
//tableIdConfig = "_"+tableId + "Config"，如：_queryTableConfig <- 它是後端C#產生的javascript object，若不提供，則預設為_quertyTableCofnig
//call ex : tableDraw(queryTable,'QueryForm',_quertyTableCofnig)
function tableDraw(queryTableObj, queryFormId, tableIdConfig) {
    if (queryFormId !== '' && queryFormId !== 'undefined')
        setQueryCondition(queryFormId);
    let tableConfig = {};
    if (typeof (tableIdConfig) !== "object") {
        tableIdConfig = _queryTableConfig;
    }
    if (typeof (queryTableObj) === "string") {
        tableConfig.iDeferLoading = 1;
        queryTableObj = $('#' + queryTableObj).DataTable(tableConfig);
    }
    else
        queryTableObj.draw();
}

function setQueryCondition(formId) {
    searchCondition = getFormInputToJson(formId);
}

function ajaxParam(dataTablesParameter) {

    $.extend(true, dataTablesParameter, searchCondition);//合併datatables 欄位json及查詢條件json
    return dataTablesParameter;
}

function ajaxSuccess(data) {
    console.log(data);
    let obj = {
        data: data.data,
        draw: data.draw,
        recordsFiltered: data.recordsFiltered,
        recordsTotal: data.recordsTotal
    }
    console.log(obj);
    return obj;

}

function ajaxError(jqXHR, textStatus, errorThrown) {
    console.log(jqXHR);
    console.log(textStatus);
    console.log(errorThrown);
}


//function ajaxParam(pMethod, pUrl, pQueryParam) {

//    let searchCondition = {};
//    if (pQueryParam === "function")
//        searchCondition = function () { return pQueryParam; };
//    else
//        searchCondition = pQueryParam;

//    return {
//        method: pMethod,
//        url: pUrl,
//        //dataType: "json",
//        data: function (dataTablesParameter) {

//            $.extend(true, dataTablesParameter, searchCondition);//合併datatables 欄位json及查詢條件json
//            return dataTablesParameter;
//        }

//    }
//}

function getFormInputToJson(pFormId) {
    return KeyPairsArrayToJson($("#" + pFormId + " :input").serializeArray());
}

function dateTypeToyyyyMMdd(data, type, row, meta) {

    if (data !== "" && data !== null)
        return moment(data).format("YYYY-MM-DD");
    else
        return "";
}

function dateTypeToyyyyMMddHHmm(data, type, row, meta) {

    if (data !== "" && data !== null)
        return moment(data).format("YYYY-MM-DD HH:mm");
    else
        return "";
}





