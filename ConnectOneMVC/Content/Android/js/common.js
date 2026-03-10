function loadThemeCss(_Theme = "blue")
{   
    $("#LinkTheme").remove();
    var head = document.getElementsByTagName('head')[0]
    if (_Theme != "blue")
    {
        var style = document.createElement('link')
        style.id = "LinkTheme";
        style.href = '../../Content/Android/css/' + _Theme + ".css"
        style.type = 'text/css'
        style.rel = 'stylesheet'
        head.append(style);
    }
}
function applyTheme(_androidID)
{
    var checkedTheme = getSelectedTheme();
    if (checkedTheme == null || checkedTheme.length == 0)
    {
        DevExpress.ui.dialog.alert("Please Choose A Theme", "Information");
    }
    else
    {
        $.ajax({
            type: "GET",
            url: "/Help/Android/UpdateTheme",
            data:
            {
                theme: checkedTheme,
                androidID: _androidID
            },
            cache: false,
            success: function (data)
            {
                var alertclick = DevExpress.ui.dialog.alert(data.jsonParam.message, data.jsonParam.title);
                alertclick.done(function (alertResult)
                {
                    
                    if (data.jsonParam.result == true)
                    {
                        loadThemeCss(checkedTheme);
                    }
                });
            }
        });        
    }
}
function getSelectedTheme()
{
    var checkedTheme = $("#ChooseThemeDiv").find("input:checked");
    if (checkedTheme == null || checkedTheme.length == 0)
    {
        return "";
    }
    return checkedTheme[0].value;
}
function setAppliedTheme(_Theme = "blue")
{
    $("#th_" + _Theme).prop("checked", true);
    loadThemeCss(_Theme)
}