$(function () {
    InlinePartialViewSubmit("UpdateDocForm", "btnMgrGrpFuncUpdate","divEdit");
    $('#FuncId').trigger('change');
    $('#FuncId').attr('disabled', 'disabled');
});
