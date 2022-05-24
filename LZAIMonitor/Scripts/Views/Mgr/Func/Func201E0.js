$(function () {
    //Tree
    $.ajax({
        type: "GET",
        url: NavList,
        data: { FuncId: $('#FuncId').val() },
        dataType: "json",
        success: function (nodes) {
            $('#tree').treeview({
                data: nodes,
                showCheckbox: true, //是否顯示覆選框
                highlightSelected: false, //是否高亮選中
                checkable: true,
                checkedIcon: 'far fa-check-square',
                uncheckedIcon: 'far fa-square',
                expandIcon: 'fas fa-plus',
                collapseIcon: 'fas fa-minus',
                emptyIcon: '', //沒有子節點的節點圖示
                multiSelect: false, //多選
                state: {
                    checked: true,
                    expanded: true,
                    selected: true
                },
                onNodeChecked: nodeChecked,
                onNodeUnchecked: nodeUnchecked
            });
            //預設先選中 測試中
            //$('#tree').treeview('toggleNodeChecked', [2, { silent: true }]);
            //$('#tree').treeview('toggleNodeChecked', [0, { silent: true }]);
        },
        error: function (xhr, ajaxOptions, thrownError) {
        }
    });
});

var nodeCheckedSilent = false;
function nodeChecked(event, node) {
    if (nodeCheckedSilent) {
        return;
    }
    nodeCheckedSilent = true;
    checkAllParent(node);
    checkAllSon(node);
    nodeCheckedSilent = false;
}

var nodeUncheckedSilent = false;
function nodeUnchecked(event, node) {
    if (nodeUncheckedSilent)
        return;
    nodeUncheckedSilent = true;
    uncheckAllParent(node);
    uncheckAllSon(node);
    nodeUncheckedSilent = false;
}

//選中全部父節點  
function checkAllParent(node) {
    $('#tree').treeview('checkNode', node.nodeId, { silent: true });
    var parentNode = $('#tree').treeview('getParent', node.nodeId);
    if (!("nodeId" in parentNode)) {
        return;
    } else {
        checkAllParent(parentNode);
    }
}
//取消全部父節點
function uncheckAllParent(node) {
    $('#tree').treeview('uncheckNode', node.nodeId, { silent: true });
    var siblings = $('#tree').treeview('getSiblings', node.nodeId);
    var parentNode = $('#tree').treeview('getParent', node.nodeId);
    if (!("nodeId" in parentNode)) {
        return;
    }
    var isAllUnchecked = true;  //是否全部沒選中  
    for (var i in siblings) {
        if (siblings[i].state.checked) {
            isAllUnchecked = false;
            break;
        }
    }
    if (isAllUnchecked) {
        uncheckAllParent(parentNode);
    }

}

//選中所有子節點
function checkAllSon(node) {
    $('#tree').treeview('checkNode', node.nodeId, { silent: true });
    if (node.nodes != null && node.nodes.length > 0) {
        for (var i in node.nodes) {
            checkAllSon(node.nodes[i]);
        }
    }
}
//取消所有子節點
function uncheckAllSon(node) {
    $('#tree').treeview('uncheckNode', node.nodeId, { silent: true });
    if (node.nodes != null && node.nodes.length > 0) {
        for (var i in node.nodes) {
            uncheckAllSon(node.nodes[i]);
        }
    }
}


//function Update() {
//    var message = "";
//    var checkedValue = $('#tree').treeview('getChecked');//.get().join(',');
//    var insValue = '';
//    for (var i = 0; i <= checkedValue.length - 1; i++) {
//        insValue += checkedValue[i].nodeid + ',';
//    }
//    $("#checkValue").val(insValue);
//    $.ajax({
//        type: "POST",
//        url: UrlUpdate,
//        dataType: "Json",
//        cache: false,
//        data: $('#UpdateDocForm').serialize(),
//        headers: {
//            'RequestVerificationToken': Token
//        },
//        success: function (data) {
//            if (data.Status) {

//                MessageShowDialog(data, "訊息");
//                $('#divEdit').dialog('close');
//            }
//            else {
//                MessageShowDialog(data.Message, "訊息");
//            }
//        }
//        , error: function (xhr, status, message) {
//            //alert(xhr);
//            if (xhr.status == "500") {
//                MessageShowDialog(xhr.responseText, "訊息");
//            }
//        }
//    });
//}


function Update() {
  
    var _form = $('#UpdateDocForm');
    var _validator = _form.validate();
    let iframeObj = window.parent.document.getElementById("frameid");


    if (_form.valid()) {
        $.ajax({
            type: "POST",
            url: UrlUpdate,
            dataType: "Json",
            cache: false,
            headers: {
                'RequestVerificationToken': Token
            },
            data: _form.serialize(),
            success: function (data) {
                if (data.Status) {
                    swal.fire({
                        title: '編輯成功!',
                      
                        icon: 'success',
                    }).then(function (result) {
                        console.log(result);

                        window.parent.swal.close();

                        window.parent.$('#table').bootstrapTable('refresh');
                    });

                }
                else {
                    if (data.ModelStateErrors !== null) {
                        _validator.showErrors(data.ModelStateErrors);
                    }

                    swal.fire({
                        title: t + '失敗!',
                        html: data.Message,
                        icon: 'error'
                    });
                }

            }
            , error: function (xhr, status, message) {
                if (xhr.status === 403) {

                    Swal.fire({
                        title: t + '失敗!',
                        //html: xhr.responseText,
                        text: '您無權限使用此功能',
                        icon: 'error'
                    });
                }
                if (xhr.status === 500) {

                    Swal.fire({
                        title: t + '失敗!',
                        //html: xhr.responseText,
                        text: '錯誤:500',
                        icon: 'error'
                    });
                }
            }
        });
    }
};