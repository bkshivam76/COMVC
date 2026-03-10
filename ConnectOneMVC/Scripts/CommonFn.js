var LastKeyDown;
var Is_DD_Refresh = false;
var DropDown_DataGrid;
var MomentDateFormat = "DD/MM/YYYY HH:mm:ss.SSS";
var DropDown_KeyPress_text = "";
var GridViewPendingCallbacks = {};
var AllKeysDownStatus = {};
var sessionclearpath = "";
var dontaskpopupcloseconfirmation = true;
var PopupCloseCallFromPayment = false;
var PopupCloseCallFromAttachment = false;
var AttachmentWindowRecurringCount = 0;
var ClosingScreenParentDivClass = "";

function OnFABInit(s, e)
{
    $('.dx-fab-text').hide();
    $('.dx-fab-action').hover(function () {$(this).find('.dx-fab-text').show(); }, function () {$(this).find('.dx-fab-text').hide(); });
    dragElement(document.getElementById(s.uniqueID));
}
window.addEventListener("error", function (e) {
    console.log(e);
    if (e.type == "error") {
        var alertText = 'JavaScript Error:\n ' + e.message + "\n Note:Do Not Proceed. Please Report The Error\n" + e.error.stack;
        window.alert(alertText);
    }
});
window.addEventListener("unhandledrejection", function (promiseRejectionEvent) {
    console.log(promiseRejectionEvent);
    console.log("Promise Reject Reason: " + promiseRejectionEvent.reason)
});
$(document).on("mouseenter", "input[type='text']", function () {
    if ($(this)[0].hasAttribute("aria-invalid") == true) {
        if (this.id != null && this.id.length > 0) {
            $("input[id=" + this.id + "]").focus()
        }
        else if ($(this).find('input').length > 0) {
            $(this).find('input').focus();
        }
        else {
            $(this).focus();
        }
    }
})
$(document).delegate(".dxpc-closeBtn", "click", function () {
    $(this).closest(".tab-pane.active").find("div.column-chooser").find("i").removeClass("dx-icon-close").addClass("dx-icon-add");
});
$(document).on('keydown', '.dx-numberbox input', function (e) {
    if ([69, 187, 188, 189, 190, 109, 107].includes(e.keyCode)) {
        e.preventDefault();
    }
});
document.onkeyup = function (event)
{
    if (event.key === "Escape")
    {
        var allWidgets = $(document).find(".dx-dropdownbox input[aria-expanded='true']");
        allWidgets.each(function () {
            var ele = $("#" + this.id);
            ele.dxDropDownBox("close");
            ele.dxDropDownBox("focus");
            ele = null;
        })
        allWidgets = null;
        Event_PreventDefaultAndStopPropagation(event)
    }
};
$(document.body).on('keydown', 'input, select, div.dx-checkbox, div.dx-radiogroup', function (e)
{
    if (e.key === "Enter") {
        var self = $(this), form = self.parents('form:eq(0)'), focusable, next;
        focusable = form.find('input:enabled,textarea:enabled,a,select,button,.dx-button.FormActionButton[tabindex="0"],div.dx-checkbox[tabindex="0"],div.dx-radiogroup[tabindex="0"]').filter(':visible:not(input[aria-readonly="true"],textarea[aria-readonly="true"],div.dx-radiogroup[aria-readonly="true"],div.dx-checkbox[aria-readonly="true"])');
        next = focusable.eq(focusable.index(this) + 1);
        if (next.length) {
            next.focus();
        }
        else {
            focusable.eq(0).focus();
        }
        next = null;
        focusable = null;
        form = null;
        self = null;
        Event_PreventDefaultAndStopPropagation(e)
    }
});
$("#loadPanel,#loadPanel_ready,#loadPanel_gridView").dxLoadPanel({
    message: 'Loading...',
    closeOnOutsideClick: false,
    showPane: false,
    shading: true,
    height: "100%",
    width: "100%",
    shadingColor: "",
    deferRendering: false,
    visible: false
});
$("#loadPanel_ajax").dxLoadPanel({
    message: 'Please Wait...',
    closeOnOutsideClick: false,
    showPane: false,
    shading: true,
    height: "100%",
    width: "100%",
    shadingColor: "",
    deferRendering: false,
    visible: false
});
function Event_PreventDefaultAndStopPropagation(evt, PD = true, SP = true, SIP = true) {
    if (evt.preventDefault && PD) {
        evt.preventDefault()
    }
    else if (PD) {
        evt.returnValue = false;
    }
    if (SP) {
        evt.stopPropagation();
    } if (SIP) {
        evt.stopImmediatePropagation();
    }
}
function getParameterByName(name, url)
{
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}
function AjaxBeginFormPost(jqXHR, settings) {
    document.activeElement.blur();
    settings.url = updateQueryStringParameter(settings.url, 'SessionGUID', getParameterByName('SessionGUID', window.location.href));
}
function updateQueryStringParameter(uri, key, value) {
    var re = new RegExp("([?&])" + key + "=.*?(&|$)", "i");
    var separator = uri.indexOf('?') !== -1 ? "&" : "?";
    if (uri.match(re)) {
        return uri.replace(re, '$1' + key + "=" + value + '$2');
    }
    else {
        return uri + separator + key + "=" + value;
    }
}
function ShowLoader_ajax() {
    if (typeof ($("#loadPanel_ajax").data("dxLoadPanel")) === "undefined") {
        $("#loadPanel_ajax").dxLoadPanel({
            message: 'Please Wait...',
            closeOnOutsideClick: false,
            showPane: false,
            shading: true,
            height: "100%",
            width: "100%",
            shadingColor: "",
            deferRendering: false,
            visible: false
        });
    }
    if (typeof ($("#loadPanel").data("dxLoadPanel")) != "undefined" && $("#loadPanel").dxLoadPanel("instance").option("visible")) {
        $("#loadPanel_ajax").dxLoadPanel("instance").option("message", $("#loadPanel").dxLoadPanel("instance").option("message"));
    }
    else if (typeof ($("#loadPanel_ready").data("dxLoadPanel")) != "undefined" && $("#loadPanel_ready").dxLoadPanel("instance").option("visible")) {
        $("#loadPanel_ajax").dxLoadPanel("instance").option("message", $("#loadPanel_ready").dxLoadPanel("instance").option("message"));
    }
    else if (typeof ($("#loadPanel_gridView").data("dxLoadPanel")) != "undefined" && $("#loadPanel_gridView").dxLoadPanel("instance").option("visible")) {
        $("#loadPanel_ajax").dxLoadPanel("instance").option("message", $("#loadPanel_gridView").dxLoadPanel("instance").option("message"));
    }
    else {
        $("#loadPanel_ajax").dxLoadPanel("instance").option("message", "Please Wait...");
    }

    if ($("#loadPanel_ajax").dxLoadPanel("instance").option("visible")) {
        $("#loadPanel_ajax").dxLoadPanel("instance").option("visible", false);
    }
    $("#loadPanel_ajax").dxLoadPanel("instance").option("visible", true);
}
function HideLoader_ajax() {
    $("#loadPanel_ajax").dxLoadPanel("instance").option("visible", false);
}
function TransformText(text) {
    if (text == null) {
        return text
    }
    text = text.replace(/&quot;|&amp;|&lt;|&gt;|&#172;/gi, function (x) {
        return TransformCharacters(x);
    });
    return text;
}
function TransformCharacters(charStr) {
    if (charStr == "&quot;") {
        return "\"";
    }
    else if (charStr == "&amp;") {
        return "&";
    }
    else if (charStr == "&lt;") {
        return "<";
    }
    else if (charStr == "&gt;") {
        return ">";
    }
    else if (charStr == "&#172;") {
        return "¬";
    }
}
function BracketKeyPressEvent(e) {
    if (e.key == "[") {
        $(this).val($(this).val() + "(");
        e.preventDefault();
    }
    if (e.key == "]") {
        $(this).val($(this).val() + ")");
        e.preventDefault();
    }
    if (e.key == "'") {
        $(this).val($(this).val() + "`");
        e.preventDefault();
    }
}
function TransformTextForDB(formID = "") {
    var GetTextBoxes = formID.length > 0 ? document.getElementById(formID).getElementsByClassName("CleanTextForDB") : document.getElementsByClassName("CleanTextForDB");
    if (GetTextBoxes && GetTextBoxes.length > 0) {
        for (var i = 0; i < GetTextBoxes.length; i++) {
            if (GetTextBoxes[i].id) {
                GetTextBoxes[i].oninput = function (evt) {
                    CleanTextForDB(evt);
                };
            }
            //if (GetTextBoxes[i].id)
            //{
            //    $("#" + GetTextBoxes[i].id).bind('paste', function (ev)
            //    {
            //        setTimeout(function ()
            //        {
            //            CleanTextForDB(ev)
            //        });
            //    });
            //}
        }
    }
}
function transformTypedChar(charStr) {
    if (charStr == "'") {
        charStr = "`";
    }
    else if (charStr == "[") {
        charStr = "(";
    }
    else if (charStr == "]") {
        charStr = ")";
    }
    else if (charStr == "!") {
        charStr = "|";
    }
    else if (charStr == "&") {
        charStr = "&";
    }
    //else if (charStr == "<") {
    //    charStr = "(";
    //}
    //else if (charStr == ">") {
    //    charStr = ")";
    //}
    return charStr;
}
function CleanTextForDB(evt) {
    evt.target.value = evt.target.value.replace(/'|<|>|&|!|\[|\]/gi, function (x) {
        return transformTypedChar(x);
    });
    if ((evt.target.className).includes("TextBoxUpperCase")) {
        TransfromTextToUpperCase(evt);
    }
    if ((evt.target.className).includes("OnlyDigit")) {
        AllowOnlyDigits(evt);
    }
}
function AllowOnlyDigits(evt) {
    evt.target.value = evt.target.value.replace(/[^0-9]/g, '');
}
function TransfromTextToUpperCase(evt) {
    evt.target.value = evt.target.value.toUpperCase();
}
function TextBox_OnContentReady(e) {
    var data = e.component.option("value");
    if (data != null && data.length > 0) {
        if (data.search(/'|<|>|&|!|\[|\]/gi) >= 0) {
            data = data.replace(/'|<|>|&|!|\[|\]/gi, function (x) {
                return transformTypedChar(x);
            });
            e.component.option("value", data);
        }
    }
}
function RestrictNumberBoxLength(MaxLength, ClassName = null, ElementID = null) {
    if (ElementID == null) {
        $("." + ClassName).each(function () {
            $(this).dxNumberBox({
                onInput: function (e) {
                    var inputElement = e.event.target;
                    if (inputElement.value.length > MaxLength)
                        inputElement.value = inputElement.value.slice(0, MaxLength);
                }
            });
        });
    }
    else {
        $("#" + ElementID).dxNumberBox({
            onInput: function (e) {
                var inputElement = e.event.target;
                if (inputElement.value.length > MaxLength)
                    inputElement.value = inputElement.value.slice(0, MaxLength);
            }
        });
    }
}
function btnCloseActiveTab() {
    $(".TabPanalMenu-tabs .active span").click()
}
function ShowLoader(message = "", showLoadingWithMessage = true) {
    if ($("#loadPanel").dxLoadPanel("instance").option("visible")) {
        $("#loadPanel").dxLoadPanel("instance").option("visible", false);
    }
    if (showLoadingWithMessage) {
        $("#loadPanel").dxLoadPanel("instance").option("message", message + " Loading...");
    }
    else {
        $("#loadPanel").dxLoadPanel("instance").option("message", message + "...");
    }
    $("#loadPanel").dxLoadPanel("instance").option("visible", true);
}
function HideLoader() {
    $("#loadPanel").dxLoadPanel("instance").resetOption("message");
    $("#loadPanel").dxLoadPanel("instance").option("visible", false);
}
function ShowLoader_Ready(message = "") {
    if ($("#loadPanel_ready").dxLoadPanel("instance").option("visible")) {
        $("#loadPanel_ready").dxLoadPanel("instance").option("visible", false);
    }
    $("#loadPanel_ready").dxLoadPanel("instance").option("message", message + " Loading...");
    $("#loadPanel_ready").dxLoadPanel("instance").option("visible", true);
}
function HideLoader_Ready() {
    $("#loadPanel_ready").dxLoadPanel("instance").option("visible", false);
}
function ShowLoader_GridView(message = "") {
    if ($("#loadPanel_gridView").dxLoadPanel("instance").option("visible")) {
        $("#loadPanel_gridView").dxLoadPanel("instance").option("visible", false);
    }
    $("#loadPanel_gridView").dxLoadPanel("instance").option("message", message + "Grid Loading...");
    $("#loadPanel_gridView").dxLoadPanel("instance").option("visible", true);
}
function HideLoader_GridView() {
    $("#loadPanel_gridView").dxLoadPanel("instance").option("visible", false);
}
function ShowLoader_clearReference(message = "") {
    if ($("#loadPanel_clearReference").dxLoadPanel("instance").option("visible")) {
        $("#loadPanel_clearReference").dxLoadPanel("instance").option("visible", false);
    }
    $("#loadPanel_clearReference").dxLoadPanel("instance").option("message", message + "Grid Loading...");
    $("#loadPanel_clearReference").dxLoadPanel("instance").option("visible", true);
}
function HideLoader_clearReference() {
    $("#loadPanel_clearReference").dxLoadPanel("instance").option("visible", false);
}
function ConvertTimeStringToJsDate(timeStr)
{
    if (timeStr)
    {
        let dateStr = new Date().toISOString();
        dateStr = dateStr.slice(0, dateStr.indexOf("T"));
        const time = timeStr.indexOf(":") === 1 ? "0" + timeStr : timeStr;
        return new Date(`${dateStr}T${time}`);
    }
    return null
}
function DateStringToJsDate(DateStr, _format = "")
{
    if (DateStr)
    {
        var momentDate = StringToMomentDate(DateStr, _format);
        if (momentDate == null)
        {
            return null
        }
        return new Date(momentDate);
    }
    return null
}
function ConvertJsonDateToString(inputdate)
{
    var newDate = new Date(parseInt(inputdate.substr(6)));
    var datemonth = newDate.getMonth() + 1;
    newDate = datemonth + "-" + newDate.getDate() + "-" + newDate.getFullYear();
    return newDate;
}
function FormatDateToddMMyyyy(date) {
    if (isValidDate(date)) {
        var month = date.getMonth() + 1;
        var year = date.getFullYear();
        var day = date.getDate();
        return day + "/" + month + "/" + year;
    }
    else {
        return "";
    }
}
function JS_Json_Date_To_ddMMyyyy(inputdate, _format = "")
{
    var format = _format.length == 0 ? MomentDateFormat : _format;
    var _date = new Date(inputdate);
    if (isValidDate(_date) && inputdate!=null) {
        return (moment(_date, format).format(format))
    }
    return null;
}
function JS_Json_DateToMomentDate(inputdate, _format = "")
{
    var format = _format.length == 0 ? MomentDateFormat : _format;
    var _date = new Date(inputdate);
    if (isValidDate(_date))
    {
        return (moment(_date, format))
    }
    return null;
}
function StringToMomentDate(inputdateInString, _format = "")
{
    var format = _format.length == 0 ? MomentDateFormat : _format;
    var MomentDate = (moment(inputdateInString, format));
    if (MomentDate.isValid())
    {
        return MomentDate;
    }
    return null;
}
function JS_Date_toIsoString(date,withTime=true)
{
    if (isValidDate(date)) {
        var pad = function (num) {
            return (num < 10 ? '0' : '') + num;
        },
            milpad = function (num) {
                if (num < 10) return '00' + num
                if (num < 100) return '0' + num
                if (num < 1000) return '' + num
            };
        if (withTime) {
            return date.getFullYear() +
                '-' + pad(date.getMonth() + 1) +
                '-' + pad(date.getDate()) +
                'T' + pad(date.getHours()) +
                ':' + pad(date.getMinutes()) +
                ':' + pad(date.getSeconds()) +
                '.' + milpad(date.getMilliseconds())
        }
        else
        {
            return date.getFullYear() +
                '-' + pad(date.getMonth() + 1) +
                '-' + pad(date.getDate()) +
                'T00:00:00'
        }
    }
    return null;
}
function isValidDate(value)
{
    if (value == void 0 || value == null || value=="")
    {
        return false
    }
    return value && !(value.getTime && value.getTime() !== value.getTime());
}
function getDifferenceInDays(Fromdate, Todate) {
    const diffInMs = Todate - Fromdate;
    return diffInMs / (1000 * 60 * 60 * 24);
}
function getDiffinHrMin(FrTime, ToTime) {
    var hour = "";
    var minutes = "";
    var timeDiff = ToTime.getTime() - FrTime.getTime();
    hour = Math.floor(timeDiff / 1000 / 60 / 60);
    hour = ('' + hour).slice(-2);
    timeDiff -= hour * 1000 * 60 * 60;
    minutes = Math.floor(timeDiff / 1000 / 60);
    minutes = ('' + minutes).slice(-2);
    return [hour, minutes];
}
function createGuid() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}
function IsUndefinedOrNull(data) {
    if (typeof (data) === "undefined" || data === null) {
        return true;
    }
    return false;
}
String.prototype.toProperCase = function () {
    return this.replace(/\w\S*/g, function (txt) { return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase(); });
};
String.prototype.ReplaceSpecialwith_ = function () {
    return this.replace(/[^\w]/gi, "_");
};
function IsNullOrWhitespace(controlid, controltype = "dxTextBox", value = null) {
    if (controlid != null && controlid.length > 0) {
        value = ($("#" + controlid))[controltype]("instance").option("value");
    }
    if (typeof value == 'undefined') { return true }
    if (value == null) { return true }
    if ((String(value).trim()).length == 0) { return true }
    return false
}
function TextEditor_OnFocusIn(e) {
    if (!isMobile) {
        e.element.find(".dx-texteditor-input").select();
    }
    $(this).on('touchstart touchmove touchend', function (e) {
        e.stopPropagation();
    });
}
async function DataGrid_keyDown(e, dropdown_component) {
    var dataGrid = e.component;
    var dataGrid_ID = e.element[0].id;
    if (e.event.key == "Enter") {
        Event_PreventDefaultAndStopPropagation(e.event)
        var focusedElement = $("#" + dataGrid_ID + " .dx-focused").parent()[0];
        if ($(focusedElement).attr('aria-selected') == "true") {
            dropdown_component.focus();
            dropdown_component.close();
        }
        else {
            var rowCount = dataGrid.totalCount();
            for (i = 0; i < rowCount; i++) {
                if (focusedElement === dataGrid.getRowElement(i)[0]) {
                    dropdown_component.focus();
                    dropdown_component.close();
                    await dataGrid.selectRows([dataGrid.getKeyByRowIndex(i)], false);
                    break;
                }
            }
        }
    }
}
function DataGrid_OnEditorPreparing(e, dropdown_component) {
    if (e.parentType !== "searchPanel" && e.parentType !== "filterRow") {
        return;
    }
    var dataGrid = e.component;
    if (e.parentType == "searchPanel") {
        dataGrid.clearFilter("search");
        e.updateValueTimeout = 10;
        e.editorOptions.onKeyDown = e => {
            if (e.event.keyCode === 40)
            {
                Event_PreventDefaultAndStopPropagation(e.event)
                dataGrid.focus(dataGrid.getCellElement(0, 0));
            }
            if (e.event.key == "Enter") {
                Event_PreventDefaultAndStopPropagation(e.event)
                dropdown_component.focus();
                dropdown_component.close();
                if (dataGrid.getVisibleRows().length > 0) {
                    dataGrid.selectRows([dataGrid.getKeyByRowIndex(0)], false);
                }
            }
        }
    }
    else if (e.parentType == "filterRow") {
        e.editorOptions.onKeyDown = e => {
            if (e.event.keyCode === 40)
            {
                Event_PreventDefaultAndStopPropagation(e.event)
                dataGrid.focus(dataGrid.getCellElement(0, 0));
            }
        }
    }
}
function DataGrid_OnRowClick(e, dropdown_component) {
    if (e.isSelected) {
        dropdown_component.focus();
        dropdown_component.close();
    }
}
function DropDown_Close(e) {
    if (LastKeyDown != "Tab") {
        if (!($(".dx-texteditor").find("input,textarea").is(":focus"))) {
            e.component.focus();
        }
    }
    var dropdown_DatagridID = Get_DropDown_DataGrid_ID(null, e.component);
    if (dropdown_DatagridID.length > 0) {
        if ($("#" + dropdown_DatagridID).dxDataGrid("instance")) {
            $("#" + dropdown_DatagridID).dxDataGrid("instance").clearFilter("search");
        }
    }
    DropDown_KeyPress_text = "";
}
function DropDown_Click(ele)
{
    var dg_id = ""
    if (ele) {
        dg_id= Get_DropDown_DataGrid_ID(ele.id)
    }
    setTimeout(function ()
    {
        if (dg_id == null || dg_id.length == 0)
        {
            $(".dx-datagrid-search-panel").find("input").focus();
        }
        else
        {
            $("#" + dg_id+" .dx-datagrid-search-panel").find("input").focus();
        }
        $(".dx-datagrid-search-panel").find("input").val(null);
    }, 10);
    DropDown_KeyPress_text = "";
}
function DropDown_KeyPress(e) {
    if (e.event.which != 13 && e.event.which != 9 && e.event.which != 32 && e.event.which != 16) //enter tab space shift
    {
        if (e.component.option("readOnly") == false && e.component.option("disabled") == false) {
            e.component.open();
            var DD_Grid = $(e.component.content().find(".DropDownDataGrid"));
            if (DD_Grid.length > 0) {
                DD_Grid.dxDataGrid("instance").clearFilter("search");
            }
            else {
                $(".dx-datagrid-search-panel").find("input").val("");
            }
            if (e.event.which >= 48 && e.event.which <= 105 && e.event.which != 91 && e.event.which != 92 && e.event.which != 93) {
                DropDown_KeyPress_text += e.event.key;
                if (DD_Grid.length > 0) {
                    DD_Grid.dxDataGrid("instance").searchByText(DropDown_KeyPress_text);
                }
                else {
                    $(".dx-datagrid-search-panel").find("input").val(DropDown_KeyPress_text);
                }
            }
            DD_Grid = null;
        }
    }
}
function DDPopup_ResizeEnd(e) {
    var dataGrid = $(e.component.content()).find('div.DropDownDataGrid')
    if (dataGrid.length > 0) {
        var PopupHeight = e.component.option("height")
        var PopupMaxHeight = parseInt(getComputedStyle($(e.component.content().parent())[0]).maxHeight, 10)
        var PopupWidth = e.component.option("width")
        $(dataGrid[0]).dxDataGrid("option", "height", PopupHeight > PopupMaxHeight ? (PopupMaxHeight * 97) / 100 : (PopupHeight * 97) / 100);
        $(dataGrid[0]).dxDataGrid("updateDimensions");
    }
}
function GetKeyFieldOfDataSource(ControlID, componentType)
{
    var inputid = $("div[id='" + ControlID + "']");
    return $(inputid)[componentType]("getDataSource").store().key();
}
function GetRowIndexAsPerDataSource(ControlID, componentType, keyVal)
{
    if (keyVal == null || keyVal.length == 0)
    {
        return -1;
    }
    if (Array.isArray(keyVal))
    {
        keyVal = keyVal[0];
    }
    var DsItems = GetDataSourceItems(ControlID, componentType)
    if (DsItems != null && DsItems.length > 0)
    {
        return DsItems.findIndex(x => { return x[GetKeyFieldOfDataSource(ControlID, componentType)] == keyVal })
    }
    return -1;
}
function GetDataSourceItems(ControlID, componentType)
{
    var inputid = $("div[id='" + ControlID + "']");
    var DS = $(inputid)[componentType]("getDataSource");
    if (DS._store._array) {
        return DS._store._array;
    }
    if (DS._store.__rawData)
    {
        return DS._store.__rawData;
    }
}
function Focus_FirstEditable_Field(formId, actionMethod = null)
{
    var focusable = $("#" + formId).find('input:enabled,.dx-button.FormActionButton[tabindex="0"],div.dx-checkbox[tabindex="0"],div.dx-radiogroup[tabindex="0"]').filter(':visible:not(input[aria-readonly="true"],div.dx-radiogroup[aria-readonly="true"],div.dx-checkbox[aria-readonly="true"])');  
    if (focusable.length > 0)
    {
        for (var i = 0; i < focusable.length; i++)
        {
            var widget = $(focusable[i]).closest('.dx-widget');
            if (widget.length > 0)
            {
                focusable.eq(i).focus();
                break;
            }
        }       
    }
    focusable = null;
}
function Set_Form_View_Delete_Mode(colourcode = null, formId = null, ActionMethod = null, DisableControls = true) {
    if (ActionMethod != null) {
        if (ActionMethod.includes("New") || ActionMethod.includes("Edit")) {
            return;
        }
    }
    if (colourcode == null) {
        if (ActionMethod.includes("View")) {
            colourcode = "#000080";
        }
        if (ActionMethod.includes("Delete")) {
            colourcode = "#000000";
        }
    }
    var element = $("#" + formId).find('label,input:not(".dx-button-submit-input"),div.dx-checkbox,div.dx-radiogroup,div.dx-textarea').filter(':visible');
    if (element.length > 0) {
        var labels = element.filter('label');
        if (labels.length > 0) {
            (labels).addClass("disabletxt").removeClass("text-red");
        }
        var dxelement = element.filter(':not(label)');
        var dxelement_length = dxelement.length;
        if (dxelement_length > 0) {
            dxelement.css('color', colourcode);
            if (DisableControls == true) {
                dxelement.each(function () {
                    var widget = $(this).closest('.dx-widget');
                    widget.removeClass("input-read-only");
                    var data = widget.data();
                    var dxComponentName = data.dxComponents[0];
                    if (dxComponentName.indexOf("dxPrivateComponent") < 0) {
                        widget[dxComponentName]("instance").option("readOnly", true);
                        widget[dxComponentName]("instance").option("focusStateEnabled", false);
                    }
                    widget = null;
                    data = null;
                    dxComponentName = null;
                });
            }
        }
        element = null;
        labels = null;
        dxelement = null;
        dxelement_length = null;
    }
}
function DisableLabel(formId) {
    var list = $("#" + formId).find(".control-label");
    if (list.length > 0) {
        (list).addClass("disabletxt").removeClass("text-red");
    }
    list = null;
}
function AssignIDtoLabel(formId, prefix) {
    var index = 1;
    var list = $("#" + formId).find("label");
    if (list.length > 0) {
        list.each(function () {
            if ($(this).prop("id").length == 0) {
                $(this).prop("id", "Label_" + prefix + "_" + index);
                index++;
            }
        })
    }
    list = null;
}
function Fieldset_SetDisable(FieldsetID, disable) {
    var list = $("#" + FieldsetID).find("label");
    $("#" + FieldsetID).prop("disabled", disable);
    if (disable) {
        list.addClass("disabletxt").removeClass("text-red");
    }
    else {
        list.removeClass("disabletxt").removeClass("text-red");
    }
}
function Control_SetReadOnly(ControlID, componentType, Readonly, colorCode = null, controlClass = "") {
    var inputid = "";
    if (controlClass.length > 0)
    {
        inputid = "." + controlClass;
    }
    else
    {
        inputid = "#" + ControlID;
    }
    if (Readonly == false)
    {
        if (colorCode == null)
        {
            colorCode = "#000080";
        }
        $(inputid).removeClass("input-read-only");
    }
    else { $(inputid).addClass("input-read-only"); }
    if (colorCode != null) {
        $(inputid).find('input').css('color', colorCode);
    }
    ($(inputid))[componentType]("instance").option("readOnly", Readonly);
    ($(inputid))[componentType]("instance").option("focusStateEnabled", !Readonly);
}
function Control_SetDisable(ControlID, componentType, Disable, colorCode = null) {
    var inputid = $("#" + ControlID);
    if (Disable == false) {
        if (colorCode == null) {
            colorCode = "#000080";
        }
    }
    if (colorCode != null) {
        inputid.find('input').css('color', colorCode);
    }
    ($(inputid))[componentType]("instance").option("disabled", Disable);
}
function Control_SetColor(ControlID, colorCode) {
    var inputid = $("#" + ControlID);
    if (colorCode.toUpperCase == "DIMGRAY") {
        colorCode = "#696969";
    }
    if (colorCode.toUpperCase == "NAVY") {
        colorCode = "#000080";
    }
    if (colorCode.toUpperCase == "GRAY") {
        colorCode = "#808080";
    }
    inputid.find('input').css('color', colorCode);
}
function FocusOnFirstClientSideBrokenRule(ValidationResult) {
    var brokenID = ValidationResult.brokenRules[0].validator._$element[0].id;
    if (brokenID != null && brokenID.length > 0) {
        $("#" + brokenID).find('input').focus();
    }
}
function ValidateForm(e, ValidationGroup = "") {
    var result;
    if (e) {
        if (e.validationGroup) {
            result = e.validationGroup.validate()
        }
        else {
            return true;
        }
    }
    else {
        result = DevExpress.validationEngine.validateGroup(ValidationGroup);
    }
    //var result = e ? e.validationGroup.validate() : DevExpress.validationEngine.validateGroup(ValidationGroup);
    if (result.isValid == false) {
        var brokenID = result.brokenRules[0].validator._$element[0].id;
        if (brokenID != null && brokenID.length > 0) {
            $("#" + brokenID).find('input').focus();
        }
    }
    return result.isValid;
}
function FormInitialSettings(Form, Tag, Timeout = true)
{
    setTimeout(function () {
        Set_Form_View_Delete_Mode(null, Form, Tag);
        Focus_FirstEditable_Field(Form);
        HideLoader_Ready();
        console.log("Voucher Ready")
    }, Timeout?Timeout500:0);
}
function GetDayNumberWithSuffix(DayofMonth) {
    switch (DayofMonth) {
        case "1":
        case "21":
        case "31":
        case "41":
        case "51":
        case "61":
        case "71":
        case "81":
        case "91":
        case "101":
        case "121":
            return DayofMonth + "st";
            break;
        case "2":
        case "22":
        case "32":
        case "42":
        case "52":
        case "62":
        case "72":
        case "82":
        case "92":
        case "102":
        case "122":
            return DayofMonth + "nd";
            break;
        case "3":
        case "23":
        case "33":
        case "43":
        case "53":
        case "63":
        case "73":
        case "83":
        case "93":
        case "103":
        case "123":
            return DayofMonth + "rd";
            break;
        default:
            return DayofMonth + "th";
            break;
    }
}
function Get_DropDown_DataGrid_ID(DropdownId = null, Component = null) {
    if (Component) {
        var id = "#" + (((Component.content())[0]).id);
        var DGid = ((Component.content())[0]).firstElementChild.id;
    }
    else if (DropdownId) {
        var id = "#" + ((($("#" + DropdownId).dxDropDownBox("instance").content())[0]).id);
        var DGid = (($("#" + DropdownId).dxDropDownBox("instance").content())[0]).firstElementChild.id;
    }
    return DGid;
}
function DeleteCookie(cookieName) {
    var expires = "expires=" + new Date() - 1;
    document.cookie = cookieName + "=;" + expires + ";path=/";
}
function dragElement(elmnt) {
    var pos1 = 0, pos2 = 0, pos3 = 0, pos4 = 0;
    elmnt.onmousedown = dragMouseDown;
    function dragMouseDown(e) {
        e = e || window.event;
        e.preventDefault();
        pos3 = e.clientX;
        pos4 = e.clientY;
        document.onmouseup = closeDragElement;
        document.onmousemove = elementDrag;
    }
    function elementDrag(e) {
        e = e || window.event;
        e.preventDefault();
        pos1 = pos3 - e.clientX;
        pos2 = pos4 - e.clientY;
        pos3 = e.clientX;
        pos4 = e.clientY;
        elmnt.style.top = (elmnt.offsetTop - pos2) + "px";
        elmnt.style.left = (elmnt.offsetLeft - pos1) + "px";
    }
    function closeDragElement() {
        document.onmouseup = null;
        document.onmousemove = null;
    }
}
function MultiUserPrevention(PopupID, Message, title, GridName)
{
    var showAlert = DevExpress.ui.dialog.alert(Message, title);
    showAlert.done(function ()
    {
        if (GridName) {
            RefreshGridView(GridName);
        }
    });
    setTimeout(function ()
    {
        $("#" + PopupID).dxPopup('hide');
    })
}
function NoRightsPrevention(Closetab = false, PopupID = "", Message = 'Not Allowed', title = 'No Rights') {
    var showAlert = DevExpress.ui.dialog.alert(Message, title);
    showAlert.done(function () {
        if (Closetab == true) {
            btnCloseActiveTab();
        }
        else if (!IsNullOrWhitespace(null, "", PopupID)) {
            $("#" + PopupID).dxPopup('hide');
        }
    });
}
function isMobile()
{
    var check = false;
    (function (a) {
        if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino/i.test(a) || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(a.substr(0, 4)))
            check = true;
    })(navigator.userAgent || navigator.vendor || window.opera);
    return check;
};
var isMobile = isMobile();
function isDesktop()
{
    var check = true;
    if (/iPhone|iPad|iPod|Android/i.test(navigator.userAgent)) {
        check = false;
    }
    return check;
}
var isDesktop = isDesktop();
function isIos() {
    return [
        'iPad Simulator',
        'iPhone Simulator',
        'iPod Simulator',
        'iPad',
        'iPhone',
        'iPod'
    ].includes(navigator.platform)
        // iPad on iOS 13 detection
        || (navigator.userAgent.includes("Mac") && "ontouchend" in document)
}
var isIos = isIos();
function RefreshCashBookGrid()
{
    var GridInstance = $("div[id = 'CashBookListGrid']").data("dxDataGrid")
    if (GridInstance !== void 0)
    {
        Grid_Display_CB();
    }
    else if (typeof CashBookListGrid !== 'undefined') {
        Grid_Display();
    }
}
function RefreshGridView(GridName, CallBackfn = null)
{
    if (GridName == "CashBookListGrid")
    {
        RefreshCashBookGrid();
    }
    else
    {
        var GridInstance = $("div[id = '" + GridName + "']").data("dxDataGrid")
        if (GridInstance !== void 0)
        {           
            GridInstance.refresh().done(function (data)
            {
                var selectedRowKeys = GridInstance.getSelectedRowKeys()
                if (CallBackfn != null) { CallBackfn(); }
                else
                {                    
                    if (selectedRowKeys != null && selectedRowKeys.length > 0)
                    {
                        GridInstance.clearSelection()
                        GridInstance.selectRows(selectedRowKeys)
                    }
                }
            });
        }
        else
        {
            if (typeof ASPxClientControl.GetControlCollection().GetByName(GridName) !== 'undefined' && ASPxClientControl.GetControlCollection().GetByName(GridName) != null)
            {
                GridViewDoCallback(ASPxClientControl.GetControlCollection().GetByName(GridName), function () { ASPxClientControl.GetControlCollection().GetByName(GridName).Refresh(); });
            }
        }
    }
}
function PerformCallbackGridView(GridName) {
    if (typeof ASPxClientControl.GetControlCollection().GetByName(GridName) !== 'undefined' && ASPxClientControl.GetControlCollection().GetByName(GridName) != null) {
        GridViewDoCallback(ASPxClientControl.GetControlCollection().GetByName(GridName), function () { ASPxClientControl.GetControlCollection().GetByName(GridName).PerformCallback(); });
    }
}
function GridViewDoCallback(sender, callback) {
    if (sender.InCallback()) {
        GridViewPendingCallbacks[sender.name] = callback;
        sender.EndCallback.RemoveHandler(GridViewDoEndCallback);
        sender.EndCallback.AddHandler(GridViewDoEndCallback);
    }
    else {
        callback();
    }
}
function GridViewDoEndCallback(s, e) {
    var pendingCallback = GridViewPendingCallbacks[s.name];
    if (pendingCallback) {
        pendingCallback();
        delete GridViewPendingCallbacks[s.name];
    }
}
async function ResetDropdownWithSameValue(ddID, DG_Instance = null)
{
    var ddvalue = $("#" + ddID).dxDropDownBox("option", "value");
    if (ddvalue)
    {
        if (Array.isArray(ddvalue))
        {
            ddvalue = ddvalue[0];
        }
        $("#" + ddID).dxDropDownBox("reset");
        if (ddvalue != null)
        {
            var DS_items = GetDataSourceItems(ddID, "dxDropDownBox")
            var keyfield = $("#" + ddID).dxDropDownBox("getDataSource").store().key();
            if (DS_items.findIndex(x => x[keyfield] == ddvalue) > -1)
            {
                if (DG_Instance == null)
                {
                    $("#" + ddID).dxDropDownBox("option", "value", ddvalue);
                }
                else
                {
                    await DG_Instance.selectRows(ddvalue);
                }
            }
        }
    }
}
function Check_keydownEventRepeat(evt)
{
    var key = convertUpperCaseToLowerForKeysPressed(evt)
    if (AllKeysDownStatus[key] == void 0 || AllKeysDownStatus[key] == false)
    {
        return false
    }
    return true;
}
function convertUpperCaseToLowerForKeysPressed(evt)
{
    var key = evt.key;
    if (evt.keyCode > 64 && evt.keyCode < 91) {
        key = String.fromCharCode(evt.keyCode).toLowerCase();
    }
    return key;
}
function formatBytes(a, b = 2, k = 1024) { with (Math) { let d = floor(log(a) / log(k)); return 0 == a ? "0 Bytes" : parseFloat((a / pow(k, d)).toFixed(max(0, b))) + " " + ["Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB"][d] } }
function ChangeTextOfLoader(message) {
    $("#loadPanel").dxLoadPanel("instance").option("message", message);
    if (typeof ($("#loadPanel_ajax").data("dxLoadPanel")) != "undefined") {
        if ($("#loadPanel_ajax").dxLoadPanel("instance").option("visible")) {
            $("#loadPanel_ajax").dxLoadPanel("instance").option("message", message);
        }
    }
}
function SendCustomArgs_FilterControl(s, e) {
    if (e.command == "SHOWFILTERCONTROL") {
        var filterControl = s.GetFilterControl();
        var oldGetCallbackParams = filterControl.GetCallbackParams;
        filterControl.GetCallbackParams = function () {
            var params = oldGetCallbackParams.apply(filterControl);

            if (!s.BeginCallback.IsEmpty()) {
                var args = new MVCxClientBeginCallbackEventArgs();
                s.BeginCallback.FireEvent(s, args);
                for (var key in args.customArgs) {
                    params[key] = args.customArgs[key];
                }
            }
            return params;
        }
    }
}
Array.prototype.move = function (from, to) {
    this.splice(to, 0, this.splice(from, 1)[0]);
};
DevExpress.ui.dxTabPanel.defaultOptions({
    device: { deviceType: "phone" },
    options: {
        swipeEnabled: false,
        scrollByContent: false,
        showNavButtons: true
    }
});
DevExpress.ui.dxTabPanel.defaultOptions({
    device: { deviceType: "tablet" },
    options: {
        swipeEnabled: false,
        scrollByContent: false,
        showNavButtons: true
    }
});
DevExpress.ui.dxScrollView.defaultOptions({
    device: { ios: true },
    options: {
        useNative:true
    }
});
//Code by pritam bhai commented because it's making mobile popup height less...
DevExpress.ui.dxPopup.defaultOptions({
    options: {
        animation: "none",
        onInitialized: function (e) {
            e.component.registerKeyHandler("escape", null); //prevent esc close shortcut
        }
    }
});
function OpenDynamicPopup(PopupTitle, LoadURL, maxHeight = "auto", maxWidth = "100%", viaAjax = false, AjaxSuccessData = null, wrapperClass = "") {
    ShowLoader(PopupTitle);
    if (!$("#Dynamic_Content_popup").dxPopup("instance"))
    {
        $("#Global_popup").append("<div id='Dynamic_Content_popup'>");
        $("#Dynamic_Content_popup").dxPopup({
            maxHeight:"100%"
        });
    }
    else
    {
        $("#Dynamic_Content_popup").dxPopup("instance").resetOption("height");
        $("#Dynamic_Content_popup").dxPopup("instance").resetOption("width");
        $("#Dynamic_Content_popup").dxPopup("instance").resetOption("position");
    }
    $("#Dynamic_Content_popup").dxPopup("instance").option("title", PopupTitle)
    $("#Dynamic_Content_popup").dxPopup("instance").option("height", maxHeight)
    $("#Dynamic_Content_popup").dxPopup("instance").option("width", maxWidth)
    $("#Dynamic_Content_popup").dxPopup("instance").option("showCloseButton", true)
    $("#Dynamic_Content_popup").dxPopup("instance").option("position", { my: "top", at: "top",of:window}) 
    $("#Dynamic_Content_popup").dxPopup("instance").option("fullScreen", false);
    $("#Dynamic_Content_popup").dxPopup({ onHiding: function (e) { Dynamic_Content_popup_hiding(e) } });
    $("#Dynamic_Content_popup").dxPopup({ onHidden: function (e) { PopupOnHidden(e) } });
    $("#Dynamic_Content_popup").dxPopup({ onShown: function (e) { PopupOnShown(e) } });
    $("#Dynamic_Content_popup").dxPopup({ onShowing: function (e) { PopupOnShowing(e, wrapperClass) } });
    loadDataintoPopup("Dynamic_Content_popup", viaAjax, LoadURL, AjaxSuccessData, PopupTitle, wrapperClass);
}
function OpenPopup(PopupID, PopupTitle = "", LoadURL = null, maxHeight = "auto", maxWidth = "100%", viaAjax = false, AjaxSuccessData = null, wrapperClass = "") {
    ShowLoader(PopupTitle);
    var Popupid = "#" + PopupID;
    if (!$(Popupid).dxPopup("instance")) {
        $("#Global_popup").append("<div id='" + PopupID + "'>");
        $(Popupid).dxPopup({maxHeight: "100%"});
    }
    $(Popupid).dxPopup("instance").resetOption("height");
    $(Popupid).dxPopup("instance").resetOption("width");
    $(Popupid).dxPopup("instance").option("title", PopupTitle)
    $(Popupid).dxPopup("instance").option("height", maxHeight)
    $(Popupid).dxPopup("instance").option("width", maxWidth)
    $(Popupid).dxPopup("instance").option("showCloseButton", true)
    $(Popupid).dxPopup("instance").option("position", { my: "top", at: "top",of:window})
    $(Popupid).dxPopup("instance").option("fullScreen", false);
    $(Popupid).dxPopup({ onShowing: function (e) { PopupOnShowing(e, wrapperClass) } });
    $(Popupid).dxPopup({ onHidden: function (e) { PopupOnHidden(e) } });
    $(Popupid).dxPopup({ onShown: function (e) { PopupOnShown(e) } });
    if (Popupid == "#Dynamic_Content_popup") {
        $(Popupid).dxPopup({ onHiding: function (e) { Dynamic_Content_popup_hiding(e) } });
    }
    loadDataintoPopup(PopupID, viaAjax, LoadURL, AjaxSuccessData, PopupTitle,wrapperClass);
}
function loadDataintoPopup(Popupid, viaAjax, LoadURL, AjaxSuccessData, PopupTitle, wrapperClass = "") {
    ShowLoader(PopupTitle);
    var content = $("<div id='result_" + Popupid + "' class='PopupContentDiv'></div>");
    content.html("");
    var scrollView = $("<div id='scrollView_" + Popupid + "' class='ScrollViewDiv'></div>");
    scrollView.append(content);
    if (viaAjax == false) {
        content.load(LoadURL, function ()
        {
            CreatePopupContentTemplate(Popupid, scrollView);
            HideLoader();
            $("#" + Popupid).dxPopup("instance").show();
        });
    }
    else
    {
        content.html(AjaxSuccessData);
        CreatePopupContentTemplate(Popupid, scrollView);
        HideLoader();
        $("#" + Popupid).dxPopup("instance").show();
    }
}
function HidePopup(PopupID) {
    var Popupid = "#" + PopupID;
    $(Popupid).dxPopup("hide");
}
function Dynamic_Content_popup_hiding(e) {
    //HideLoader();
    if (dontaskpopupcloseconfirmation == true) {
        if (sessionclearpath != "") {
            $.ajax
                ({
                    type: 'GET',
                    url: sessionclearpath,
                    cache: false,
                    success: function (data) { sessionclearpath = ""; }
                });
        }
        if (PopupCloseCallFromPayment) {
            //$("#Payment_ParentDiv").remove();
            PopupCloseCallFromPayment = false;
        }
        if (PopupCloseCallFromAttachment) {
            if (AttachmentWindowRecurringCount > 0) {
                if (typeof AttachmentListGrid !== "undefined" && ASPxClientUtils.IsExists(AttachmentListGrid) && AttachmentListGrid.GetMainElement()) {
                    AttachmentListGrid.Refresh();
                }
            }
            AttachmentWindowRecurringCount = 0;
            PopupCloseCallFromAttachment = false;
        }
        if (IsRegisteredForAudit == 1)//Registration Completed
        {
            RegisterForAuditSuccess();
            IsRegisteredForAudit = 0;
        }
    }
    else if (dontaskpopupcloseconfirmation == false) {
        e.cancel = true;
        var result = DevExpress.ui.dialog.confirm('Are You Sure You want To Cancel...?', "Confirm");
        result.done(function (dialogResult) {
            dontaskpopupcloseconfirmation = dialogResult;
            if (dialogResult) {
                $("#Dynamic_Content_popup").dxPopup("hide");
            }
            else {
                FocusButtonOnPopupCloseConfirmationNo($("#Dynamic_Content_popup"));
            }
        });
    }
}
function PopupOnHidden(e) {
    var popupid = e.element[0].id;
    var PopupContentDiv = $("#result_" + popupid);
    if (PopupContentDiv.length > 0) {
        ClosingScreenParentDivClass = PopupContentDiv[0].firstElementChild == null ? "" : PopupContentDiv[0].firstElementChild.className;
        PopupContentDiv = null;
    }
    else {
        ClosingScreenParentDivClass = "";
    }
    if (ClosingScreenParentDivClass.toLowerCase().includes("_clearreference")) {
        ClearPopupClosingScreenReferences();
    }
    if (popupid == "Dynamic_Content_popup") {
        e.component.content()[0].innerHTML = "";
        e.component.dispose();
        $("#Dynamic_Content_popup").remove();
    }
}
function ClearPopupClosingScreenReferences() {    
    switch (ClosingScreenParentDivClass) {
        case "Notify_ChartResponsesFacility_ClearReferences":            
            mouse_is_down_notificationSet = null;
            left_notificationSet = null;
            bar_notificationSet = null;
            break;
        case "RoomsViz_Facility_ClearReferences":
            tooltip = null;
            RoomsFilleddata = null;
            AllRoomsdata = null;
            svg = null;
            break;
        case "AccomShort_Facility_ClearReferences":
            selectedEvent_AccomShort = null;
            selectedForm_AccomShort = null;
            selectedRegistrationNumbers = null;
            selectedPhoneNumbers = null;
            selectedInstanceId_AccomShort = null;
            reg_numbers_AccomShort = null;
            ph_numbers_AccomShort = null;

            result_regNumbs = null;
            result_chartids = null;
            result_respids = null;
            result_names = null;
            result_gender = null;
            result_names_genders_str = null;
            break;
        case "CDW_ClearReferences":
            CDW_saveClick = null;
            Item_CDW_DG = null;
            Bank_CDW_DG = null;
            Tag_CDW = null;
            break;
        case "B2B_ClearReferences":

            B2B_CloseConfirmation = null;
            Bank2ShortName_B2B = null;
            Bank1_B2B_DG = null;
            Bank2_B2B_DG = null;
            Item_B2B_DG = null;
            B2B_CloseConfirmation = null;
            break;
        case "CBox_ClearReferences":

            Cmd_Mode_CBox = null;
            RefBank_CBox_DG = null;
            Bank_CBox_DG = null;
            Item_CBox_DG = null;
            Party1_CBox_DG = null;
            Party2_CBox_DG = null;
            Cnt_BankAccount = null;
            ActionMethod_CBox = null;
            break;
        case "SaleAsset_ClearReferences":

            Item_Name_SaleA_DG = null;
            PartyList1_SaleA_DG = null;
            AssetList_SaleA_DG = null;
            RefBankList_SaleA_DG = null;
            PurposeList_SaleA_DG = null;
            BankList_SaleA_DG = null;
            ItemDDRefresh_SaleA = null;
            PartyDDRefresh_SaleA = null;
            AssetListDDRefresh_SaleA = null;
            RefBankDDRefresh_SaleA = null;
            BankDDRefresh_SaleA = null;
            SaleA_PopupCloseConfirmation = null;
            break;
        case "Receipt_ClearReferences":

            AdjustmentDDRefresh_Rpt = null;
            Cnt_BankAccount_Rpt = null;
            IsDeposit_Rpt = null;
            PartyID_Rpt = null;
            Rpt_DepBank_DataGrid = null;
            Rpt_DepBank_DDrefresh = null;
            Rpt_DepBank_Enable = null;
            Rpt_DepBank_ReadOnly = null;
            Rpt_Item_DataGrid = null;
            Rpt_Item_DDrefresh = null;
            Rpt_Item_Enable = null;
            Rpt_Item_ReadOnly = null;
            Rpt_Party_DataGrid = null;
            Rpt_Party_DDrefresh = null;
            Rpt_Payment_Datagrid = null;
            Rpt_Payment_DDrefresh = null;
            Rpt_Payment_Enable = null;
            Rpt_Payment_ReadOnly = null;
            Rpt_ReceiptType_Enable = null;
            Rpt_ReceiptType_ReadOnly = null;
            Rpt_RefBank_DataGrid = null;
            Rpt_RefBank_DDrefresh = null;
            Rpt_RefBank_Enable = null;
            Rpt_RefBank_ReadOnly = null;
            Rpt_SlipCount_Enable = null;
            Rpt_SlipCount_ReadOnly = null;
            Rpt_SlipNo_Enable = null;
            Rpt_SlipNo_ReadOnly = null;
            break;
        case "DonationRegular_ClearReferences":

            Cnt_BankAccount = null;
            ItemDD_Donation_ReadOnly = null;
            ItemDD_Donation_Enabled = null;
            RefBankDD_Donation_ReadOnly = null;
            RefBankDD_Donation_Enabled = null;
            BankDD_Donation_ReadOnly = null;
            BankDD_Donation_Enabled = null;
            BankDD_Donation_ReadOnly = null;
            SlipTxn_Donation_Enabled = null;
            SlipTxn_Donation_ReadOnly = null;
            Banklist_Dropdown_event = null;
            Item_DG_Donation = null;
            Party_DG_Donation = null;
            Bank_DG_Donation = null;
            RefBank_DG_Donation = null;
            BankID_Donation = null;
            DonRTag = null;
            if (dataSource_Party_Donation != null) {
                dataSource_Party_Donation.dispose();
            }
            dataSource_Party_Donation = null;
            break;
        case "DonationForeign_ClearReferences":

            GLookUp_ItemList_DG_D_F = null;
            GLookUp_PartyList_DG_D_F = null;
            GLookUp_CatList_DG_D_F = null;
            GLookUp_BankList_DG_D_F = null;
            GLookUp_CurList_DG_D_F = null;
            GLookUp_RefBankList_DG_D_F = null;
            GLookUp_PurList_DG_D_F = null;
            ItemList_DDRefresh_D_F = null;
            PartyList_DDRefresh_D_F = null;
            CatList_DDRefresh_D_F = null;
            BankList_DDRefresh_D_F = null;
            CurList_DDRefresh_D_F = null;
            RefBankList_DDRefresh_D_F = null;
            PurList_DDRefresh_D_F = null;
            Tag_Don_F = null;
            ItemList_F_Enable = null;
            ItemList_F_ReadOnly = null;
            PartyList_F_Enable = null;
            PartyList_F_ReadOnly = null;
            CategoryList_F_Enable = null;
            CategoryList_F_ReadOnly = null;
            Cnt_BankAccount_F = null;
            BankList_F_Enable = null;
            BankList_F_ReadOnly = null;
            CurrencyList_F_Enable = null;
            CurrencyList_F_ReadOnly = null;
            RefBankList_F_Enable = null;
            RefBankList_F_ReadOnly = null;
            PurposeList_F_Enable = null;
            PurposeList_F_ReadOnly = null;
            break;
        case "CheckDonationType_VoucherInfo_ClearReferences":

            // No Global Variable in Check Donation Type Scree:
            break;
        case "CheckDonationType_Voucher_ClearReferences":

            // No Global Variable in Check Donation Type Scree:
            break;
        case "DocumentLibrary_ClearReferences":

            ID = null;
            Title = null;
            Category = null;
            FileName = null;
            FileLocationPath = null;
            break;
        case "DonationRegister_ChangePeriod_ClearReferences":

            // No Global Variable in Donation Register Change Period Scree:
            break;
        case "InternalTransferVoucher_ClearReferences":

            TrfBank_ITV_Enable = null;
            TrfBank_ITV_ReadOnly = null;
            ToCen_ITV_Enable = null;
            ToCen_ITV_ReadOnly = null;
            FrCen_ITV_Enable = null;
            FrCen_ITV_ReadOnly = null;
            PopupCloseConfirmation = null;
            OpenInsName_ITV = null;
            BAnkID_ITR = null;
            ItemDDRefresh_ITV = null;
            FrCenterDDRefresh_ITV = null;
            ToCenterDDRefresh_ITV = null;
            BankDDRefresh_ITV = null;
            TrfBankDDRefresh_ITV = null;
            BankCount_ITR = null;
            ITR_iTrans_Type = null;
            ITR_iVoucher_Type = null;
            ITV_Cmd_Mode = null;
            iFR_CEN_ID = null;
            FrCen_ListID = null;
            iTO_CEN_ID = null;
            ToCenListID = null;
            TrfbankId = null;
            Item_DataGrid_ITV = null;
            FrCentre_DataGrid_ITV = null;
            ToCentre_DataGrid_ITV = null;
            Bank_DataGrid_ITV = null;
            TrfBank_DataGrid_ITV = null;
            Tag_ITV = null;
            TO_CEN_ID = null;
            ToCenListID = null;
            To_CEN_Name = null;
            _Status = null;
            _ID = null;
            _Centre_Name = null;
            _Centre_UID = null;
            _No = null;
            _Zone = null;
            _Sub_Zone = null;
            _CEN_ID = null;
            _P_Date = null;
            _BI_ACC_NO = null;
            _Incharge = null;
            _Contact_No = null;
            _Purpose = null;
            _Mode = null;
            _Amount = null;
            _BI_ID = null;
            _Bank_Name = null;
            _Branch_Name = null;
            _Ref_No = null;
            _Ref_Date = null;
            _M_ID = null;
            _PUR_ID = null;
            _Ref_Bank_AccNo = null;
            _Description = null;
            _REF_BI_ID = null;
            _Ref_Branch = null;
            _Ref_Others = null;
            _Add_Date = null;
            _Add_By = null;
            _Edit_By = null;
            _Edit_Date = null;
            _Action_Status = null;
            _Action_By = null;
            _Action_Date = null;
            _Modes = null;
            _Ref_Branches = null;

            break;
        case "AssetTransferVoucher_ClearReferences":

            GLookUp_ItemList_DataGrid_AsetTrans = null;
            GLookUp_FrCen_List_DataGrid_AsetTrans = null;
            GLookUp_ToCen_List_DataGrid_AsetTrans = null;
            GLookUp_PurList_DataGrid_AsetTrans = null;
            GLookUp_AssetList_DataGrid_AsetTrans = null;
            Look_OwnList_DataGrid_AsetTrans = null;
            Look_LocList_DataGrid_AsetTrans = null;
            Tag_AsetTrans = null;
            ItemListDDRefresh_AsetTrans = null;
            FrCen_ListDDRefresh_AsetTrans = null;
            ToCen_ListDDRefresh_AsetTrans = null;
            PurListDDRefresh_AsetTrans = null;
            AssetList_ListDDRefresh_AsetTrans = null;
            OwnerListDDRefresh_AsetTrans = null;
            LocList_ListDDRefresh_AsetTrans = null;
            Cmb_Asset_Type_SelectedValue = null;
            ItemList_ATV_Enable = null;
            ItemList_ATV_ReadOnly = null;
            FrCen_ATV_Enable = null;
            FrCen_ATV_ReadOnly = null;
            ToCen_ATV_Enable = null;
            ToCen_ATV_ReadOnly = null;
            PurList_ATV_Enable = null;
            PurList_ATV_ReadOnly = null;
            AssetList_ATV_Enable = null;
            AssetList_ATV_ReadOnly = null;
            OwnList_ATV_Enable = null;
            OwnList_ATV_ReadOnly = null;
            Cmd_PUse_ATV_Enable = null;
            Cmd_PUse_ATV_ReadOnly = null;
            Txt_CurQty_ATV_Enable = null;
            Txt_CurQty_ATV_ReadOnly = null;
            Txt_Qty_ATV_Enable = null;
            Txt_Qty_ATV_ReadOnly = null;
            Look_LocList_Enable = null;
            Look_LocList_ReadOnly = null;
            Txt_V_Date_Enable = null;
            Txt_V_Date_ReadOnly = null;
            Cmb_Asset_Type_Enable = null;
            Cmb_Asset_Type_ReadOnly = null;
            Txt_SaleAmt_Enable = null;
            Txt_SaleAmt_ReadOnly = null;
            Txt_Desc_Enable = null;
            Txt_Desc_ReadOnly = null;
            Open_Institute_Name = null;
            _Date_AsetTrans = null;
            _ITEM_ID_AsetTrans = null;
            _CEN_ID_AsetTrans = null;
            _ASSET_TYPE_AsetTrans = null;
            _ASSET_REF_ID_AsetTrans = null;
            _PUR_ID_AsetTrans = null;
            _ID_AsetTrans = null;
            _M_ID_AsetTrans = null;
            _EDIT_DATE_AsetTrans = null;
            _ASSET_QTY_AsetTrans = null;
            _ASSET_SALE_AMT_AsetTrans = null;
            _Amount_AsetTrans = null;
            _Description_AsetTrans = null;
            _Centre_Name_AsetTrans = null;
            _Incharge_AsetTrans = null;
            _Contact_No_AsetTrans = null;
            _Centre_UID_AsetTrans = null;
            _No_AsetTrans = null;
            _ASSET_ITEM_ID_AsetTrans = null;
            _Asset_AsetTrans = null;
            _Purpose_AsetTrans = null;
            _Narration_AsetTrans = null;
            TO_CEN_ID_AsetTrans = null;
            dataGrid = null;
            break;
        case "WIPVoucher_ClearReferences":

            Txt_Reference_Enable = null;
            Txt_Reference_ReadOnly = null;
            Txt_Amount_Enable = null;
            Txt_Amount_ReadOnly = null;
            Txt_Finalized_Amount_Enable = null;
            Txt_Finalized_Amount_ReadOnly = null;
            Tag_ItemDetail_WIPF = null;
            Asset_RecID_WIPFinal = null;
            Rec_Edit_On_WIPFinal = null;
            GLookUp_WIPLedgerList_DG_WIPF = null;
            GLookUp_FinalizedAssetList_DG_WIPF = null;
            Tag_WIPF = null;
            WIPLedgerList_Enable = null;
            WIPLedgerList_ReadOnly = null;
            FinalizedAssetList_Enable = null;
            FinalizedAssetList_ReadOnly = null;
            WIPLedgerList_DDRefresh_WIPF = null;
            FinalizedAssetList_DDRefresh_WIPF = null;
            LED_ID_WIPF = null;
            LEDGER_WIPF = null;
            WIPLedgerList_DD_ValueChange_FirstCall = null;
            break;
        case "AuditAction_InfoScreen_ClearReferences":

            IsSucessPopup = null;
            IsViewPopup = null;
            IsActionItemNewPopup = null;
            ActionMethod = null;
            ActionItem_ShowHorizontalBar = null;
            AuditAction_IsVouchingMode = null;
            ActionItemsInfo_Auto_Vouching_Mode = null;
            UserType_ActionItemsPrf = null;
            ActionItems_ViewMode = null;
            ActionItemsID = null;
            AI_Type = null;
            AI_Status = null;
            AI_Date = null;
            AI_Auditor = null;
            AI_Title = null;
            AI_Description = null;
            AI_Due_On = null;
            AI_Centre_Remarks = null;
            AI_Close_Remarks = null;
            AI_Closed_On = null;
            AI_Closed_By = null;
            AI_CrossedTimeLimit = null;
            AI_Add_Date = null;
            AI_Add_By = null;
            AI_Edit_By = null;
            AI_Edit_Date = null;
            AI_Action_Status = null;
            AI_Action_By = null;
            AI_Action_Date = null;
            AI_RefScree = null;
            AI_RefTabl = null;
            AI_RefRecID = null;
            AI_RefStatus = null;
            RefScreen = null;
            _Ref_Table = null;
            RefRecID = null;
            RefTable = null;
            Info_LastEditedOn = null;
            ActionItemsInfo_IsDetailRowExpanded = null;
            ActionItemsInfo_NestedRecord_FocusedRowIndex = null;
            ActionItemsInfo_NestedGrid_AccessID = null;
            ActionItemsInfo_AtachmentID = null;
            ActionItemsInfo_FileName = null;
            ActionItemsInfo_ParamMandatory = null;
            ActionItemsInfo_FromDateLabel = null;
            ActionItemsInfo_ToDateLabel = null;
            ActionItemsInfo_DescriptionLabel = null;
            ActionItemsInfo_Doc_ID = null;
            ActionItemsInfo_Doc_Category = null;
            ActionItemsInfo_MasterRowRecID = null;
            ActionItemsInfo_MasterRowMID = null;
            ActionItemsInfo_NestedRowMapID = null;
            ActionItemsInfo_reason = null;
            ActionItemsInfo_Attachment_RecStatus = null;
            ActionItemsInfo_Nested_Doc_Status = null;
            AI_Select_xid = null;
            AIFocusedRowIndexBeforeRefresh = null;
            ActionItemsGridLayout = null;
            AuditAction_ColumnToBeShownIndex = null;
            AuditAction_ColumnToBeHidddenIndex = null;
            break;
        case "AuditAction_WindowScreen_ClearReferences":

            AI_PopupCloseTakeConfirmation = null;
            ActionMethod_AI = null;
            break;
        case "AuditAction_CentreRemarksWindowScreen_ClearReferences":

            // No Global Variable in Audit Action Centre Remarks Window Scree:
            break;
        case "SubmissionHistory_ClearReferences":

            // No Global Variabl:
            break;
        case "SubmitAccounts_ClearReferences":

            // No Global Variabl:
            break;
        case "ChangeFY_ClearReferences":

            CFY_SetDefaultPressed = null;
            CFY_IsDefault = null;
            COD_YEAR_ID = null;
            ACC_TYPE = null;
            Lock = null;
            To = null;
            From = null;
            Financial_Year = null;
            break;
        case "SubmitReport_ClearReferences":

            // No Global Variable in Submit Report Scree:
            break;
        case "VouchingAudit_Filter_ClearReferences":

            CB_Zone = null;
            CB_Subzone = null;
            CB_Stateid = null;
            CB_Head = null;
            ZoneFirstValueChange = null;
            ZonevalueChange = null;
            SubZoneFirstValueChange = null;
            SubZoneValuechange = null;
            StateFirstValueChange = null;
            StateValueChange = null;
            HeadDDvalueChangeDone = null;
            break;
        case "DocumentChecking_RejectReason_ClearReferences":

            Reason_DC = null;
            break;
        case "VouchingPreference_ClearReferences":

            SelectedVouchingCode = null;
            break;
        case "DataRestrictions_Window_ClearReferences":

            Party_DG_Lockdata = null;
            Bank_DG_Lockdata = null;
            PartyID_Lockdata = null;
            BankID_Lockdata = null;
            LedgerID_Lockdata = null;
            ActionMethod_LockData = null;
            break;
        case "ChangeInstitute_ClearReferences":

            INS_ID = null;
            INS_NAME = null;
            INS_SHORT = null;
            break;
        case "LandBuilding_Window_ClearReferences":

            City_LBW_DG = null;
            State_LBW_DG = null;
            District_LBW_DG = null;
            OwnerList_LBW_DG = null;
            Refresh_State_LBW_DD = null;
            Refresh_District_LBW_DD = null;
            Refresh_City_LBW_DD = null;
            OwnerListRefresh_LBW_DD = null;
            GLookUp_StateName_LBW = null;
            resStateCode_LBW = null;
            StateDDvalueChangeExecution_LBW_FirstTime = null;
            StateDDvalueChangeFinishExecution_LBW = null;
            GLookUp_DistrictName_LBW = null;
            GLookUp_CityName_LBW = null;
            break;
        case "LandBuilding_ExtensionWindow_ClearReferences":

            LB_Ins_LBW_DG = null;
            LB_Ins_LBW_DD_Refresh = null;
            break;
        case "CashBookEditCommon_ClearReferences":

            PurListDDRefresh_VoucherUpdate = null;
            PurListDG_VoucherUpdate = null;
            break;
        case "CashBookPrintVoucher_ClearReferences":


            break;
        case "CashBookNew_ClearReferences":

            ItemSelectedID = null;
            IsDonationNewPopup = null;
            break;
        case "CB_NoAttachmentReason_ClearReferences":


            break;
        case "FDType_ClearReferences":

            SelectedActivityID = null;
            FDActivity = null;
            break;
        case "FDVoucher_ClearReferences":

            FD_Item_DataGrid = null;
            FD_DepBank_DataGrid = null;
            FD_FDList_DataGrid = null;
            FD_item_id = null;
            FD_PopupCloseConfirmation = null;
            //FD_Item_valueChangeFinishExecution = null;
            FD_CMD_Mode = null;
            FD_DepBankDD_Refresh = null;
            FD_FDListDD_Refresh = null;
            FD_ItemDD_Refresh = null;
            //SelectedActivityID = null;
            //FDActivity = null;
            //ClearAllReferences("FDType_ClearReferences"):
            FDVoucher_GridToRefresh = null;
            break;
        case "PaymentVoucher_ClearReferences":

            paymentvoucher_saveClick = null;
            Party_DG_Pay = null;
            ActionMethod_Payment = null;
            Payment_Grid_1_RowNumber = null;
            Payment_Selected_ItemID = null;
            Payment_ispecificallow_item = null;
            Payment_vDate_item = null;
            Payment_partyID_item = null;
            Grid1_Item_TotalCount = null;
            Actionmethod_pay_item = null;
            Payment_ItemGrid_Data = null;
            BankPayment_Sr = null;
            Payment_BankGrid_Data = null;
            Grid_3_RowNumber = null;
            Payment_Grid3_PageData = null;
            PAGridRow = null;
            Payment_Grid4_PageData = null;
            if (dataSource_Party_Payment != null) {
                dataSource_Party_Payment.dispose();
            }
            dataSource_Party_Payment = null;
            Payment_v_date_checkPassed = null;
            Payment_vDatevalueChangeInprogress = null;
            break;
        case "PaymentItem_ClearReferences":

            Paymentitem_ActionMethod = null;
            Payment_Party_Pan = null;
            Payment_Item_Party_Name = null;
            Item_DG_Pay = null;
            Party_DG_itemPay = null;
            if (dataSource_Party_Payment_Item != null) {
                dataSource_Party_Payment_Item.dispose();
            }
            dataSource_Party_Payment_Item = null;
            break;
        case "PaymentBank_ClearReferences":

            lbl_Trf_ANo_Tag = null;
            Bank_DG_Pay = null;
            RefBank_DG_Pay = null;
            Cnt_BankAccount = null;
            break;
        case "PaymentAdvance_ClearReference":


            break;
        case "PaymentLiability_ClearReference":


            break;
        case "DNKVoucher_ClearReferences":

            GLookUp_PartyList1_DG_DNK = null;
            PartyList1_DDRefresh_DNK = null;
            Tag_DNK = null;
            PartyList_DNK_Enable = null;
            PartyList_DNK_ReadOnly = null;
            break;
        case "DNKItem_ClearReferences":

            ItemList_DDRefresh_Itm_Dtel = null;
            PurList_DDRefresh_Itm_Dtel = null;
            DG_ItemList_Itm_Dtel = null;
            DG_PurList_Itm_Dtel = null;
            Tag_Itm_Dtel = null;
            PurList_Itm_Enable = null;
            PurList_Itm_ReadOnly = null;
            ItemList_Itm_Enable = null;
            ItemList_Itm_ReadOnly = null;
            Txt_Qty_Itm_Enable = null;
            Txt_Qty_Itm_ReadOnly = null;
            Cmd_UOM_Itm_Enable = null;
            Cmd_UOM_Itm_ReadOnly = null;
            Txt_Rate_Itm_Enable = null;
            Txt_Rate_Itm_ReadOnly = null;
            break;
        case "VoucherAssetProfile_ClearReferences":
            LocList_Asset_DG = null;
            if (dataSource_AssetLocation_PrfPay != null) {
                dataSource_AssetLocation_PrfPay.dispose();
            }
            dataSource_AssetLocation_PrfPay = null;
            break;
        case "VoucherGSProfile_ClearReferences":

            MiscList_DG = null;
            LocList_GS_DG = null;
            if (dataSource_GSLocation_PrfPay != null) {
                dataSource_GSLocation_PrfPay.dispose();
            }
            dataSource_GSLocation_PrfPay = null;
            break;
        case "VoucherLiveStockProfile_ClearReferences":

            LocList_LS_DG = null;
            if (dataSource_LSLocation_PrfPay != null) {
                dataSource_LSLocation_PrfPay.dispose();
            }
            dataSource_LSLocation_PrfPay = null;
            break;
        case "VoucherAddLocation_ClearReferences":


            break;
        case "VoucherMapLocation_ClearReferences":


            break;
        case "VoucherPropertySelect_ClearReferences":

            Property_LB_REC_ID = null;
            Property_Rec_Edit_on = null;
            Property_ClosingValue = null;
            Property_Name = null;
            break;
        case "VoucherPropertyProfile_ClearReferences":

            resStateCode = null;
            GLookUp_StateName = null;
            GLookUp_DistrictName = null;
            GLookUp_CityName = null;
            DistrictID_OnMainCenter_Selection = null;
            CityID_OnMainCenter_Selection = null;
            StateDDvalueChangeFinishExecution = null;
            LB_City_DG:
            LB_State_DG:
            LB_District_DG:
            LB_Owner_DG:
            LB_Refresh_StateDD = null;
            LB_Refresh_DistrictDD = null;
            LB_Refresh_CityDD = null;
            LB_Refresh_OwnerDD = null;
            StateDDvalueChangeExecution_FirstTime = null;
            ExtendedProperty_Sr = null;
            ClosePropertyAddressPopup_Voucher = null;
            if (dataSource_PropOwner_PrfPay != null) {
                dataSource_PropOwner_PrfPay.dispose();
            }
            dataSource_PropOwner_PrfPay = null;
            if (dataSource_PropDistrct_PrfPay != null) {
                dataSource_PropDistrct_PrfPay.dispose();
            }
            dataSource_PropDistrct_PrfPay = null;
            if (dataSource_PropCity_PrfPay != null) {
                dataSource_PropCity_PrfPay.dispose();
            }
            dataSource_PropCity_PrfPay = null;
            break;
        case "Voucher_ExtendedProperty_ClearReferences":

            LB_Ins_DG = null;
            break;
        case "VoucherTelephone_ClearReferences":

            paymentvoucher_telephone_planType = null;
            TeleList_DG = null;
            break;
        case "VoucherVehicleProfile_ClearReferences":

            Vehiclies_Cmd_Make = null;
            LocList_vehicle_DG = null;
            Owner_DG = null;
            if (dataSource_VehLocation_PrfPay != null) {
                dataSource_VehLocation_PrfPay.dispose();
            }
            dataSource_VehLocation_PrfPay = null;
            if (dataSource_VehOwner_PrfPay != null) {
                dataSource_VehOwner_PrfPay.dispose();
            }
            dataSource_VehOwner_PrfPay = null;
            if (dataSource_VehModel_PrfPay != null) {
                dataSource_VehModel_PrfPay.dispose();
            }
            dataSource_VehModel_PrfPay = null;
            break;
        case "VoucherWIPRefType_ClearReferences":

            RefType_WIP = null;
            break;
        case "VoucherWIPNew_ClearReferences":

            WipLedger_DG = null;
            break;
        case "VoucherWIPExisiting_ClearReferences":

            reference_recid = null;
            reference_name = null;
            reference_opening = null;
            reference_closing = null;
            reference_NextyearclosingValue = null;
            break;
        case "BankProfile_ClearReferences":

            BankInfo_IsDetailRowExpanded = null;
            BankInfo_NestedRecord_FocusedRowIndex = null;
            BankInfo_NestedGrid_AccessID = null;
            BankInfo_AtachmentID = null;
            BankInfo_FileName = null;
            BankInfo_ParamMandatory = null;
            BankInfo_FromDateLabel = null;
            BankInfo_ToDateLabel = null;
            BankInfo_DescriptionLabel = null;
            BankInfo_Doc_ID = null;
            BankInfo_Doc_Category = null;
            BankInfo_MasterRowRecID = null;
            BankInfo_MasterRowMID = null;
            BankInfo_NestedRowMapID = null;
            BankInfo_NestedRowUniqueID = null;
            BankInfo_reason = null;
            BankInfo_Attachment_RecStatus = null;
            BankInfo_SetVouchingModeOn = null;
            BankInfo_NestedRowReasonID = null;

            Bank_ShowHorizontalBar = null;
            BankGridLayout = null;
            BankProfileLastSelectedRowKeyBeforeRefresh = null;
            BankProfileSelectedKeysBeforeRefresh = null;
            BankProfileFocusedRowIndexBeforeRefresh = null;
            BankID_BankProfile = null;
            BA_CLOSE_DATE = null;
            YearID = null;
            Bank_ActionStatus = null;
            Bank_EditDate = null;
            OpenActions = null;
            BA_ACCOUNT_TYPE = null;
            BA_ACCOUNT_NO = null;
            Branch = null;
            Name = null;
            BA_Customer_Number = null;
            Bank_AddBy = null;
            Bank_AddDate = null;
            Bank_EditBy = null;
            Bank_ActionDate = null;
            Bank_ActionBy = null;
            _actionName = null;
            _IsContextMenu = null;
            Bank_SelectedVisibleIndex = null;
            Bank_SelectedBankID = null;
            BankLastSelectedindex = null;
            BankLastSelectedID = null;
            Bank_IsVouchingMode = null;
            Bank_ViewMode = null;
            Select_xid = null;
            UserType_BankPrf = null;
            Bank_ColumnToBeShownIndex = null;
            Bank_ColumnToBeHidddenIndex = null;




            break;
        case "BankProfileWindow_ClearReferences":

            Bank_DG_BankPrf = null;
            Branch_DG_BankPrf = null;
            SignList1_DG_BankPrf = null;
            SignList2_DG_BankPrf = null;
            SignList3_DG_BankPrf = null;
            ActionMethod_Bank = null;
            BankWindow_TakeCloseConfirmation = null;
            bankData = null;
            Bank_DD_ValueChange_Complete = null;
            break;
        case "BankProfileClose_ClearReferences":

            Bank_TakeCloseConfirmation = null;
            break;
        case "AddressBook_ClearReferences":

            AddressBookInfo_IsDetailRowExpanded = null;
            AddressBookInfo_NestedRecord_FocusedRowIndex = null;
            AddressBookInfo_NestedGrid_AccessID = null;
            AddressBookInfo_AtachmentID = null;
            AddressBookInfo_FileName = null;
            AddressBookInfo_ParamMandatory = null;
            AddressBookInfo_FromDateLabel = null;
            AddressBookInfo_ToDateLabel = null;
            AddressBookInfo_DescriptionLabel = null;
            AddressBookInfo_Doc_ID = null;
            AddressBookInfo_Doc_Category = null;
            AddressBookInfo_MasterRowRecID = null;
            AddressBookInfo_MasterRowMID = null;
            AddressBookInfo_NestedRowMapID = null;
            AddressBookInfo_reason = null;
            AddressBookInfo_Attachment_RecStatus = null;
            AddressBookInfo_Nested_Doc_Status = null;
            AddressBookInfo_NestedRowReasonID = null;
            AddressBookInfo_NestedRowKeyValue = null;
            AB_ColumnToBeShownIndex = null;
            AB_ColumnToBeHidddenIndex = null;
            AddressBookGridLayou = null;
            AB_Select_xid = null;
            ABFocusedRowIndexBeforeRefresh = null;
            IsAddressbookSucessPopup = null;
            IsAddressbookViewPopup = null;
            IsAddressbookNewPopup = null;
            AB_ShowHorizontalBar = null;
            Addressbook_I = null;
            tP_NOField = null;
            telMiscIdField = null;
            categoryField = null;
            Address_typeField = null;
            Addressbook_Edit_Date = null;
            Addressbook_Add_Date = null;
            Addressbook_Add_By = null;
            Addressbook_Edit_By = null;
            Addressbook_Action_Status = null;
            Addressbook_Action_By = null;
            Addressbook_Action_Date = null;
            Addressbook_AddRight = null;
            Addressbook_EditRight = null;
            Addressbook_DeleteRight = null;
            Addressbook_ExportRight = null;
            Addressbook_ListRight = null;
            UserType_AddressPrf = null;
            _actionName = null;
            _IsContextMenu = null;
            AddressBook_IsVouchingMode = null;
            AddressBook_SetVouchingModeOn = null;
            AdresBook_ViewMode = null;
            UserType_AddressPrf = null;
            break;
        case "AddressBookWindow_ClearReferences":

            AB_TakepopUpcloseConfirmation = null;
            AB_FirstLoad = null;
            Country_DG_AB = null;
            State_DG_AB = null;
            District_DG_AB = null;
            City_DG_AB = null;
            Country_O_DG_AB = null;
            State_O_DG_AB = null;
            District_O_DG_AB = null;
            City_O_DG_AB = null;
            ActionMethod_AB = null;
            SelectedCountryId_AB = null;
            Country_AccessibleDescription_AB = null;
            State_AccessibleDescription_AB = null;
            CountryDDValueChangeFinishExecution_AB = null;
            StateDDValueChangeFinishExecution_AB = null;
            SelectedCountryId_O_AB = null;
            Country_AccessibleDescription_O_AB = null;
            State_AccessibleDescription_O_AB = null;
            CountryDDValueChangeFinishExecution_O_AB = null;
            StateDDValueChangeFinishExecution_O_AB = null;
            selectedIndexCenterCategory = null;
            CenList_AB_DG = null;
            break;
        case "AddressBookSmall_ClearReferences":

            Country_DG_ABSmall = null;
            State_DG_ABSmall = null;
            District_DG_ABSmall = null;
            City_DG_ABSmall = null;
            ActionMethod_ABSmall = null;
            SelectedCountryId_ABSmall = null;
            Country_AccessibleDescription_ABSmall = null;
            State_AccessibleDescription_ABSmall = null;
            CountryDDValueChangeFinishExecution = null;
            StateDDValueChangeFinishExecution = null;
            ABSmall_TakePopupCloseConfirmation = null;

            break;
        case "Attachment_ClearReferences":

            CashbookPeriod_Attachment = null;
            CashBookFromDate_Attachment = null;
            CashBookToDate_Attachment = null;
            HelpAttachment_ShowHorizontalBar = null;
            helpnewright = null;
            helpeditright = null;
            helpDeleteright = null;
            helpViewright = null;
            helpExportright = null;
            helpSuperuser_auditor = null;
            Help_Rec_ID = null;
            Help_Ref_ID = null;
            Help_FileName = null;
            Help_Checked = null;
            Help_AddBy = null;
            Help_AddOn = null;
            Help_EditBy = null;
            Help_Editon = null;
            Help_Attach_ActionStatus = null;
            Help_CheckingStatus = null;
            AttachmentGrid_layout = null;
            AttachmentFocusRowIndexBeforeRefresh = null;
            Attachment_IsVouchingMod = null;
            break;
        case "AttachmentLinkbySrno_ClearReferences":

            Referece_Screen = null;
            Referece_ID = null;
            MainGridName = null;
            NestedGridName = null;
            FocusedRowIndex = null;
            LinkBySerialNo = null;
            break;
        case "AttachmentRejectReason_ClearReferences":

            Reason_Attachment = null;
            break;
        case "SelectAttachment_ClearReferences":

            LinkAttachment_ID = null;
            LinkAttachmentFileName = null;
            AllAttachmentIDsToLink = null;
            break;
        case "AttachmentWindow_ClearReferences":

            form = null;
            AttachmentWindowNameRefresh = null;
            helpreturnCategory = null;
            DocumentwindowFirstLoad = null;
            Help_uploadErrorDuetoFilesize = null;
            Help_uploadErrorDuetoDots = null;
            Help_uploadErrorDuetoFileCount = null;
            Help_MultipleAttachmentCount = null;
            Help_MultipleAttachmentSucessCount = null;
            Help_uploadErrorDuetoHtml = null;
            Help_uploadErrorDuetoFilename = null;
            Help_Document_Name_DG = null;
            Help_Document_Category_DG = null;
            DocumentName_Category = null;
            AttachmentCategoryFirstValueChange = null;

            break;
        case "DailyBalanceoption_ClearReferences":

            DB_Bank_DataGrid = null;
            Daily_Balances_Showclick = null;
            break;
        case "CoreWindow_ClearReferences":

            Party_DDGrid_Coreprofile = null;
            Party_DD_Coreprofile_Refresh = null;
            CallingScreen = null;
            break;
        case "UserPreferencesOptions_ClearReferences":
            ChosenScreenDataSource_UserPreference = null;
            AvailScreenDataSource_UserPreference = null;
            ScreenIds_UserPreferences = null;
            break;
        case "ChangePasswordOptions_ClearReferences":
            form = null;
            break;
        case "Dialog_TelephoneStatements_ClearReferences":
            _OK = null;
            TelephoneSt_fromDate = null;
            TelephoneSt_toDate = null;
            TelephoneSt_TelephoneID = null;
            break;
        case "UserRegister_WinOptions_ClearReferences":
            Groupslistselectkey = null;
            SelectedItem = null;
            form = null;
            break;
        case "GroupReg_MngPrivOptions_ClearReferences":
            ugFLAG = null;
            Manag_Privileg_GroupID = null;
            Manag_Privileg_UserID = null;
            ManPrivilegUserName = null;
            ManPrivilegGroupID = null;
            ManPriUserName = null;
            ManPriGroupName = null;
            ManPriEntityName = null;
            ManPriPrivilegesGiven = null;
            ManPriPrivilegeID = null;
            ManPriAddedBy = null;
            ManPriAddedOn = null;
            ManPriEditedBy = null;
            ManPriEditedOn = null;
            ManPriScreenID = null;
            ManPriGrpID = null;
            ManPriUserID = null;
            ManPri_PopupID = null;
            ManPri_PopupDivID = null;
            ManpriAlluser = null;
            break;
        case "GroupReg_WinOptions_ClearReferences":
            form = null;
            break;
        case "GroupReg_MngPriv_AddUserOptions_ClearReferences":
            Grouplistselectkey = null;
            ManPri_AdUsrPri_Popid = null;
            form = null;
            AdUserPrivileges_EntityScreen_DG = null;
            ManagePrivileges_UserName = null;
            break;
        case "ServicePlaces_WindowProfile_ClearReferences":
            ActionMethod_ServicePlace = null;
            Look_PlaceOwner_DataGrid = null;
            Look_ResponsiblePerson_DataGrid = null;
            Look_PlaceOwner_DDrefresh = null;
            Look_ResponsiblePerson_DDrefresh = null;
            break;
        case "Vehicle_WindowProfile_ClearReferences":
            ActionMethod_VPW = null;
            ItemName_Datagrid_VPW = null;
            LocationList_Datagrid_VPW = null;
            MakeCompanyList_Datagrid_VPW = null;
            VehicleModelList_Datagrid_VPW = null;
            OwnerList_Datagrid_VPW = null;
            InsuranceCompanyList_Datagrid_VPW = null;
            ItemNameRefresh_VPW = null;
            LocationListRefresh_VPW = null;
            MakeCompanyListRefresh_VPW = null;
            VehicleModelListRefresh_VPW = null;
            OwnerListRefresh_VPW = null;
            InsuranceCompanyListRefresh_VPW = null;
            Cmd_Make_VPW = null;
            xSPID_VPW = null;
            break;
        case "Telephone_WindowProfile_ClearReferences":
            Telephone_IsSucessPopup = null;
            break;
        case "WIP_WindowProfile_ClearReferences":
            model = null;
            Txt_Amount_WIP_ReadOnly = null;
            Txt_Amount_WIP_Enabled = null;
            GLookUp_WIP_ReadOnly = null;
            GLookUp_WIP_Enabled = null;
            LED_ID = null;
            WIP_ID = null;
            WIP_Ledger = null;
            ActionMethod_WIP = null;
            IsWIPSucessPopup = null;
            IsWIPViewPopup = null;
            WIP_CloseConfirmation = null;
            WipLedger_DG_DD = null;
            break;
        case "WIP_Txn_RprtProfile_ClearReferences":
            WIP_ID = null;
            Opening = null;
            LedgerID = null;
            LedgerName = null;
            Reference = null;
            OpeningDate = null;
            break;
        case "Notes_WindowFacility_ClearReferences":
            ActionMode_Notes = null;
            break;
        case "JournalVoucherAccount_ClearReferences":
            Tag_JV = null;
            break;
        case "JournalVoucher_ItemAccount_ClearReferences":
            ItemList_DDRefresh_Itm = null;
            Jv_Party_DDrefresh_Itm = null;
            PurList_DDRefresh_JvItm = null;
            Tag_Itm_Jv = null;
            ItemList_JvItm_Enable = null;
            ItemList_JvItm_ReadOnly = null;
            PurList_JvItm_Enable = null;
            PurList_JvItm_ReadOnly = null;
            PartyList_JvItm_Enable = null;
            PartyList_JvItm_ReadOnly = null;
            JV_ItemList_Itm = null;
            JV_PartyList_Itm = null;
            JV_PurList_Itm = null;
            $JV_PurList_Itm = null;
            $JV_ItemList_Itm = null;

            break;
        case "JV_Reference_ClearReferences":
            RefList_DDRefresh_JV_Ref = null;
            RefList_Jv_Ref = null;
            Jv_RefList_Enable = null;
            Jv_RefList_ReadOnly = null;
            Tag_JV_Ref = null;

            break;
        case "ServiceReportWindowFacility_ClearReferences":
            Tag_GSR = null;
            GSR_Attached_FileName = null;
            GSR_Attach_RefRecId = null;
            GSR_Attach_RefScreen = null;
            GSR_Attach_DocName_ID = null;
            GSR_Attach_uploadErrorDuetohtml = null;
            GSR_uploadErrorDuetoFileCount = null;
            GSR_DocumentDescription = null;
            GSR_Is_Document_Attached = null;
            GSR_FilesSelectedCount = null;
            GSR_FileUploadSuccessCount = null;
            GSR_FileUploadStart = null;
            break;
        case "MembershipVoucher_ClearReferences":
            Item_MRR_DG = null;
            SubsList_MRR_DG = null;
            MembershipNamesList_MRR_DG = null;
            WingList_MRR_DG = null;
            cmb_SelectedIndex_MRR = null;
            sublist_Tag_MRR = null;
            SubList_MRR = null;
            Subs_Catg_MRR = null;
            Subs_Strt_Mth_MRR = null;
            Subs_Ttl_Mth_MRR = null;
            Strt_Date_MRR = null;
            Tag_MRR = null;
            WingDDEnabled_MRR = null;
            WingDDReadOnly_MRR = null;
            if (dataSource_Member_MRR != null) {
                dataSource_Member_MRR.dispose();
            }
            dataSource_Member_MRR = null;
            if (dataSource_Period_MRR != null) {
                dataSource_Period_MRR.dispose();
            }
            dataSource_Period_MRR = null;
            Bank_DG_MRR = null;
            RefBank_DG_MRR = null;
            Cnt_BankAccount_MRR = null;
            Tag_Bank_MRR = null;
            lbl_Trf_ANo_Tag_MRR = null;
            Member_C_ID_MRR = null;
            break;
        default:
            break;
    }
    if (ClosingScreenParentDivClass.length > 0) {
        ClearAllReferences(ClosingScreenParentDivClass);
        ClosingScreenParentDivClass = "";
    }
}
function PopupOnShowing(e, wrapperClass="") { 
    ShowLoader();
    var Content = e.component.content();
    var Overlay = Content.closest(".dx-overlay-wrapper");
    if (Overlay.length > 0) {
        if (!(Overlay.hasClass('popupresponsive'))) {
            Overlay.addClass('popupresponsive');
        }
        Overlay.addClass(wrapperClass);
    }
    if (Content.length > 0) {
        SetScrollView(Content, e.element[0].id);
    }
    HideLoader();
}
function PopupOnShown(e)
{
    var Content = e.component.content();
    if (Content.length > 0) {
        SetScrollView(Content, e.element[0].id);
    }
    var scrollview = $("#scrollView_" + e.element[0].id); 
    if (isDesktop)
    {
        ShowLoader();
        var form = $(Content).find('form:first');
        if (form.length > 0 && form[0].id.length > 0)
        {
            Focus_FirstEditable_Field(form[0].id);
        }
        form = null;     
        resizeScrollview(e, scrollview, Content);
        HideLoader();
    }
    else
    {
        setTimeout(function ()
        {
            var WindowHeight = document.documentElement.clientHeight;
            var PopupHeight = Content.height();
            var PopupWidth = Content.width();
            if (PopupHeight > WindowHeight) {
                e.component.option("width", "97%");
                e.component.option("height", "97%");
            }
            else
            {
                // e.component.option("height", PopupHeight + 40);  // animation disabled
                //e.component.option("width", PopupWidth + 15);

                //$(window).on('orientationchange', function () {
                //        e.component.option("width", "97%");
                //        e.component.option("height", "97%");
                //});
            }
            resizeScrollview(e, scrollview, Content);
        })
    }
}
function CreatePopupContentTemplate(Popupid, scrollView)
{
    $("#" + Popupid).dxPopup({
        contentTemplate: function (contentElement) {
            InitilaizeScrollView(scrollView);
            contentElement.append(scrollView);
            return contentElement;
        }
    });
}
function SetScrollView(PopupContentElement, Popupid) {
    if ((!(PopupContentElement.hasClass("dx-scrollable"))) && PopupContentElement.children(".dx-scrollable").length == 0) {
        var content = PopupContentElement.children();
        var scrollView = $("<div id='scrollView_" + Popupid + "' class='ScrollViewDiv'></div>");
        scrollView.append(content);
        InitilaizeScrollView(scrollView);
        PopupContentElement.html("");
        PopupContentElement.append(scrollView);
    }
}
function InitilaizeScrollView(scrollView)
{
    scrollView.dxScrollView({
        height: "100%",
        width: '100%',
        useNative: false,
        showScrollbar: 'always',
        scrollByThumb: true,
        scrollByContent: true,
        bounceEnabled: false
    });
}
function resizeScrollview(e, scrollview = null, Content = null)
{
    if (scrollview == null)
    {
        scrollview = $("#scrollView_" + e.element[0].id);
    }
    if (Content == null)
    {
        Content = e.component.content();
    }    
    scrollview.dxScrollView("update").done(function (data) {      
        var PopUpHeight = parseInt(getComputedStyle($($(Content).parent())[0]).height, 10)
        var WindowHeight = document.documentElement.clientHeight;
        var scrollHeight = scrollview.dxScrollView("scrollHeight");
        var clientHeight = scrollview.dxScrollView("clientHeight");
     //   if (PopUpHeight <= WindowHeight)
       // {
            if (scrollHeight <= clientHeight)
            {
                $(Content).on('dxmousewheel', function (evt) { evt.stopPropagation(); });
            }
       // }
        //else
       // {
           // if (scrollHeight <= clientHeight)
           // {
             //   $(Content).on('dxmousewheel', function (evt) { evt.stopPropagation(); });
            //}
        //}
        scrollview = null;
        Content = null;
   });
}
function StandardizeCallURL(URL) {
    var urlParams = new URLSearchParams(window.location.search);
    if (URL.includes("?")) { return URL + "&SessionGUID=" + urlParams.get('SessionGUID'); }
    else { return URL + "?SessionGUID=" + urlParams.get('SessionGUID'); }
}
function CreateCustomConfirmDialog(Title = "", Message = "", ForDelete = false) {
    Message = Message.length == 0 ? (ForDelete == true ? "Sure You Want To Delete This Entry...?" : "Sure You Want To Cancel This Entry...?") : Message;
    var CustomConfirmDialog = DevExpress.ui.dialog.custom({
        title: Title,
        messageHtml: Message,
        buttons: [
            {
                text: 'Yes',
                elementAttr:
                {
                    id: "CustomConfirmDialog_YesButton"
                },
                onClick: function () {
                    return true
                }
            },
            {
                text: 'No',
                elementAttr:
                {
                    id: "CustomConfirmDialog_NoButton"
                },
                onClick: function () {
                    return false
                }
            }
        ],
        popupOptions:
        {
            elementAttr:
            {
                id: "CustomConfirmDialog_Popup"
            },
            onShown: function (e) {
                if (ForDelete) {
                    $(".dx-dialog-button:last").focus();
                }
                else {
                    $(".dx-dialog-button:first").focus();
                }
            },
            onHidden: function (e) {
                e.component.dispose();
                $("#CustomConfirmDialog_Popup").remove();
            }
        }
    });
    return CustomConfirmDialog;
}
function AutoHideAlertDialog(Message = "", Title = "") {
    var AutoHideDialog = DevExpress.ui.dialog.custom({
        title: Title,
        messageHtml: Message,
        buttons: [{
            text: 'OK',
            elementAttr:
            {
                id: "AutoHideAlertDialog_OKButton"
            },
            onClick: function () {
                return true
            }
        }],
        popupOptions:
        {
            elementAttr:
            {
                id: "AutoHideAlertDialog_Popup"
            },
            onShowing: function (e) {
                //$("#AutoHideAlertDialog_OKButton").hide();
            },
            onShown: function (e) {
                //setTimeout(function ()
                //{
                //    if ($("#AutoHideAlertDialog_OKButton").length > 0)
                //    {
                //        $("#AutoHideAlertDialog_OKButton").click();
                //    }
                //},800)
            },
            onHidden: function (e) {
                e.component.dispose();
                $("#AutoHideAlertDialog_Popup").remove();
            }
        }
    });
    return AutoHideDialog.show();
}
function ClearAllReferences(MainDivClass) {
    ShowLoader_clearReference();
    var ID = "";
    var allWidgets = $("." + MainDivClass + " .DevexpressComponents");    //removing devexpress compnenets
    allWidgets.each(function () {
        ID = this.id;
        if (ID.length > 0) {
            ASPxClientControl.GetControlCollection().Remove(ASPxClientControl.GetControlCollection().GetByName(ID));
            delete window[ID];
        }
    });
    allWidgets = $("." + MainDivClass + " .DropDownDataGrid"); //removing dropdown data grid
    allWidgets.each(function () {
        $(this).dxDataGrid("dispose");
        $(this).html("");
        $(this).remove();
    });
    allWidgets = $("." + MainDivClass + " .DataGridListing"); //removing datagrid listing
    allWidgets.each(function ()
    {
        var GridInstance = $(this).data("dxDataGrid")
        if (GridInstance !== void 0)
        {
            $(this).dxDataGrid("dispose");
        }
        $(this).html("");
        $(this).remove();
        GridInstance = null;
    });
    //allWidgets = $("." + MainDivClass).find(".dx-widget");  //removing devextreme components
    //allWidgets.each(function ()
    //{
    //    if (this.id.length > 0)
    //    {
    //        var data = $(this).data();
    //        if (data)
    //        {
    //            if (data.dxComponents)
    //            {
    //                var dxComponentName = data.dxComponents[0];
    //                if (dxComponentName.indexOf("dxPrivateComponent") < 0)
    //                {
    //                    $(this)[dxComponentName]("dispose");
    //                    $(this).remove();
    //                }
    //            }
    //        }
    //    }
    //    else
    //    {
    //        $(this).html("");
    //        $(this).remove();
    //    }
    //})
    allWidgets = $("." + MainDivClass).find(".dx-validator"); //removing devextreme components with validation
    allWidgets.each(function () {
        var data = $(this).data();
        if (data) {
            if (data.dxComponents) {
                var dxComponentName = data.dxComponents[0];
                if (dxComponentName.indexOf("dxPrivateComponent") < 0) {
                    $(this)[dxComponentName]("dispose");
                    $(this).remove();
                }
            }
        }
    })
    allWidgets = $("." + MainDivClass + " *"); // removing child dom nodes
    var len = allWidgets.length, i = 0;
    for (i; i < len; i++) {
        allWidgets[i].innerHTML = "";
        allWidgets[i].remove();
    }
    allWidgets = null;
    ID = null;
    if (MainDivClass != "LeftPane_Div_ClearReference") {
        $("." + MainDivClass).html(""); // removing parent dom node
        $("." + MainDivClass).remove();
    }
    if (MainDivClass == "NoteBook_ClearReferences" && $(".TabPanalMenu-tabs .active").attr("id") == "contact_Frm_NoteBook_Info") {
        $(".TabPanalMenu-tabs li.active").prev().addClass("active");
        $(".TabPanalMenu-tabs li.active").prev().find("a").trigger("click");
        removeElement(document.getElementById("Frm_NoteBook_Info"));
        removeElement(document.getElementById("contact_Frm_NoteBook_Info"));
        if (AllOpenControlTabs.indexOf("Frm_NoteBook_Info") > -1) {
            AllOpenControlTabs.splice($.inArray("Frm_NoteBook_Info", AllOpenControlTabs), 1);
        }
    }
    else if (MainDivClass == "DailyBalance_ClearReferences" && $(".TabPanalMenu-tabs .active").attr("id") == "contact_DailyBalances") {
        $(".TabPanalMenu-tabs li.active").prev().addClass("active");
        $(".TabPanalMenu-tabs li.active").prev().find("a").trigger("click");
        removeElement(document.getElementById("DailyBalances"));
        removeElement(document.getElementById("contact_DailyBalances"));
        if (AllOpenControlTabs.indexOf("DailyBalances") > -1) {
            AllOpenControlTabs.splice($.inArray("DailyBalances", AllOpenControlTabs), 1);
        }
    }
    HideLoader_clearReference();
}
function FocusButtonOnPopupCloseConfirmationNo(popupSelector) {
    var PopupContent = $((popupSelector.dxPopup("content"))[0]);
    var formButtons = PopupContent.find('form:first .FormActionButton:last');
    if (formButtons.length > 0) {
        formButtons.focus();
    }
    formButtons = null;
    PopupContent = null;
}
function DeleteAfterConfirmation(Title = "", Message = "", formSelector) {
    CreateCustomConfirmDialog(Title, Message, true).show().done(function (dialogResult) {
        if (dialogResult) {
            formSelector.submit();
        }
        else {
            var formButtons = formSelector.find('.FormActionButton:first');
            if (formButtons.length > 0) {
                formButtons.focus();
            }
            formButtons = null;
            formSelector = null;
        }
    });
}
function OpenFormPopup(_chartInstanceID = "", Connectoneuser = true, FormUrl = "", PopupID = 'ViewFormUserSidePopup', PopUpTitle = 'Fill Form', ActionMethod = "_New", height = "100%", width = "100%")
{
    ShowLoader();
    $.ajax({
        type: 'GET',
        url: "/Options/CreateForm/ViewFormUserSide",
        cache: false,
        data:
        {
            ChartInstanceId: _chartInstanceID,
            For_C_One_user: Connectoneuser,
            FormUrl: FormUrl,
            PopupID: PopupID,
            ActionMethod: ActionMethod
        },
        success: function (data)
        {
            HideLoader();
            OpenPopup(PopupID, PopUpTitle, null, height, width, true, data);
        },
        error: function (jqXHR, textStatus, errorThrown)
        {
            HideLoader();
            console.log(jqXHR);
            console.log(textStatus);
            console.log(errorThrown);
            DevExpress.ui.dialog.alert(jqXHR.responseText, errorThrown);
        }
    });
}
function OpenListingTab(_chartInstanceID = "", menuID = "", menuLinkName = "", Connectoneuser = true, Filter = "", showApproval = "" )
{
    if (menuID != null && menuID.includes("_dynamic"))
    {
        if ($("#Chart_Responses_ParentDiv").length > 0)
        {
            DevExpress.ui.dialog.alert("Responses For A Form Is Already Opened In Tabs<br>Please Close That Tab To View The Selected Form Responses", "Information..");
            return;
        }
    }
    ShowLoader();
    var url = '/Facility/Chart/Frm_Chart_Responses_Info_dx?chartInstanceID=' + _chartInstanceID + "&QuestionFilter=" + Filter;
    if (showApproval.length > 0) { url = url + '&showApproval=' + showApproval; }
    OpenTabControl(menuID, menuLinkName, url);
    HideLoader();
}

function PreventFileUpload(file) {
    var FileType = file.type
    if (FileType.includes("html")) {
        return "<b>Html files are not allowed</b><br>" + file.name + "<br>Did Not Upload<br>Kindly Check The File";
    }
    if (FileType.includes("x-msdownload") || FileType.includes("x-ms-installer")) {
        return "<b>Executable files are not allowed</b><br>" + file.name + "<br>Did Not Upload<br>Kindly Check The File";
    }
    if (FileType.includes("xspf+xml")) {
        return "<b>xspf files are not allowed</b><br>" + file.name + "<br>Did Not Upload<br>Kindly Check The File";
    }
    if (FileType.includes("tiff")) {
        return "<b>tiff files are not allowed</b><br>" + file.name + "<br>Did Not Upload<br>Kindly Check The File";
    }
    if (FileType.includes("sct") || FileType.includes("scriplet")) {
        return "<b>sct files are not allowed</b><br>" + file.name + "<br>Did Not Upload<br>Kindly Check The File";
    }
    return "";
}
function isElementInView(element, container) {
    const elementRect = element.getBoundingClientRect();
    const containerRect = container.getBoundingClientRect();

    // Check if element is within container's boundaries
    const isVisible = !(
        elementRect.top > containerRect.bottom ||
        elementRect.bottom < containerRect.top ||
        elementRect.left > containerRect.right ||
        elementRect.right < containerRect.left
    );

    return isVisible;
}
// function for smooth scroll in mobile
function InitializeScrollView_GridView(s, e) {
    if (screen.width <= 768) {
        var GridName = s.uniqueID;
        GetScrollableElement_GridView(s, e).dxScrollView({
            showScrollbar: 'always',
            direction: 'both',
            useNative: false,
            scrollByThumb: true,
            scrollByContent: true,
            bounceEnabled: false,
            height: '100%',
            width: '97vw',
            onScroll: function (e) {
                $('#' + GridName + ' .dxgvHSDC .dxgvTable_MetropolisBlue').css('transform', 'translateX(' + (-e.scrollOffset.left) + 'px)');
                $('#' + GridName + ' .dxgvFSDC .dxgvTable_MetropolisBlue').css('transform', 'translateX(' + (-e.scrollOffset.left) + 'px)');
            }
        });
        AdjustHeaderElement_GridView(s, e);
        AdjustFooterElement_GridView(s, e);
    }
}
function GetScrollableElement_GridView(s, e) {
    return $('.dxgvCSD', s.GetMainElement());
}
function AdjustHeaderElement_GridView(s, e)
{
    var GridName = s.uniqueID;
    if ($('#' + GridName + ' .dxgvHSDC').length > 0)
    {
        $('#' + GridName + ' .dxgvHSDC')[0].style.paddingRight = '';
        var headerScrollElement = $('#' + GridName + ' .dxgvHSDC > div')[0];
        headerScrollElement.style.width = (s.GetMainElement().offsetWidth - 2) + 'px';
    }
}
function AdjustFooterElement_GridView(s, e)
{
    var GridName = s.uniqueID;
    if ($('#' + GridName + ' .dxgvFSDC').length > 0)
    {
        $('#' + GridName + ' .dxgvFSDC')[0].style.paddingRight = '';
        var FooterScrollElement = $('#' + GridName + ' .dxgvFSDC > div')[0];
        FooterScrollElement.style.width = (s.GetMainElement().offsetWidth - 2) + 'px';
    }
}
function ScrollToFocusedRow_GridView(s, e) {
    if (screen.width <= 768) {
        var GridName = s.uniqueID;
        GetScrollableElement(s, e).dxScrollView("instance").scrollToElement($("#" + GridName + "_DXDataRow" + s.GetFocusedRowIndex()))
    }
}
class ObserverHelper {
    constructor() {
        this.__resizeCallbacks = new Map();
        this.__resizeObserver = new ResizeObserver((entries) => {
            entries.forEach((entry) => {
                const listeners = this.__resizeCallbacks.get(entry.target);
                listeners &&
                    listeners.forEach((resizeCallback) => resizeCallback(entry));
            });
        });
    }
    isSizeChanged = (value1, value2, delta) => {
        return !value1 || Math.abs(value2 - value1) > delta;
    };
    debounce = (func, timeout) => {
        let timer;
        return (...args) => {
            console.log("start_timer", timer);
            clearTimeout(timer);

            timer = setTimeout(() => {
                func.apply(this, args);
            }, timeout);
        };
    };
    resizeCallback = (component, delta = 1, resizeAction, ListingBtnsDiv, PopupID = "") =>
        (container) => {
            debugger
            let { width, height } = container.contentRect;
            if (
                this.isSizeChanged(component.__observableWidth, width, delta) ||
                this.isSizeChanged(component.__observableHeight, height, delta)
            ) {
                component.__observableHeight = height;
                component.__observableWidth = width;
                if (width === 0 || height === 0) return;
                component.option("height", this.GetHeight(ListingBtnsDiv, PopupID))
                resizeAction && resizeAction.apply(component);
            }
        };
    subscribe(component, element, resizeAction, delta, delay, ListingBtnsDiv, PopupID = "") {
        if (!resizeAction) {
            console.error('Subscription failed. No reisze callback passed')
            return
        }
        let listeners = this.__resizeCallbacks.get(element);
        if (!listeners) {
            this.__resizeObserver.observe(element);
            listeners = new Map();
        }
        const newResizeCallback = this.resizeCallback(component, delta, resizeAction, ListingBtnsDiv, PopupID = "");
        listeners.set(
            component.element().get(0),
            delay ? this.debounce(newResizeCallback, delay) : newResizeCallback
        );
        this.__resizeCallbacks.set(element, listeners);
        component.on("disposing", ({ component }) => {
            this.unsubscribe(element, component.element().get(0));
        });
    }
    unsubscribe(key1, key2) {
        let listeners = this.__resizeCallbacks.get(key1);
        listeners.delete(key2);
        if (listeners.size === 0) {
            this.__resizeCallbacks.delete(key1);
            this.__resizeObserver.unobserve(key1);
        } else {
            this.__resizeCallbacks.set(key1, listeners);
        }
    }
    disconnect() {
        this.__resizeCallbacks.clear();
        this.__resizeObserver.disconnect();
    }
    GetHeight(ListingBtnsDiv, PopupID = "") {
        let height;
        if (isDesktop == true) {
            if (PopupID.length > 0) {
                var listingBtnDivHeight = 0;
                if (ListingBtnsDiv.length > 0) {
                    listingBtnDivHeight = parseInt(getComputedStyle(ListingBtnsDiv[0]).height, 10);
                }
                height = document.documentElement.clientHeight - listingBtnDivHeight - 75
            }
            else {
                height = (document.documentElement.clientHeight * 90) / 100;
            }
        }
        else {
            var listingBtnDivHeight = 0;
            if (ListingBtnsDiv.length > 0) {
                listingBtnDivHeight = parseInt(getComputedStyle(ListingBtnsDiv[0]).height, 10);
            }
            height = document.documentElement.clientHeight - listingBtnDivHeight - 150;
        }
        return height;
    }
}
const Observer_Helper = new ObserverHelper();
// function to adjust popup width on mobile rotation
//$(window).on("resize", function () {
//    if (screen.width <= 768) {
//        $('.dx-popup-normal').each(function () {
//            debugger
//            if ($(this).width > 300 && $(this).is(':visible')) {
//                $(this).css('width','inherit !important')
//            }
//        })
//    }
//}, false);
