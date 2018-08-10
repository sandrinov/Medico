function LoadPartialView(controllerName, Action, callback) {
    //metroLoading.show();
    //ga('send', 'pageview', '/' + controllerName + '/' + Action);

    $.ajax({
        url: '/' + controllerName + '/' + Action,
        type: 'GET',
        success: function (response) {
            $('#' + Action.toLowerCase() + controllerName.toLowerCase()).remove();
            var row = '<ul class="nav metismenu" id="side-menu">';
            $.each(response.MenuVoices, function (i, item) {
                row = row + '<li ><a href=/' + item.Controller + '/' + item.Action + "><span class='label h-bg-navy-blue'><i class='fa fa-"+ item.IconName +"'></i></span> " + item.Text + "</a></li>";
            });
            row = row + "</ul>";
            $('.renderPartialView').append(row);
            if (callback) callback();
        },
    });
}


/*************************************   Old Menu ***********************

 $.ajax({
        url: '/' + controllerName + '/' + Action,
        type: 'GET',
        success: function (response) {
            $('#' + Action.toLowerCase() + controllerName.toLowerCase()).remove();
            var row = '<ul class="nav metismenu" id="side-menu">';
            $.each(response.MenuVoices, function (i, item) {
             row = row + '<li ><a href=/' + item.Controller + '/' + item.Action + "><span class='nav-label'>" + item.Text + "</span></a></li>";
            });
            row = row + "</ul>";
            $('.renderPartialView').append(row);
            if (callback) callback();
        },
    });
*************************************************************************/

//<span class="label h-bg-navy-blue"><i class="fa fa-users"></i></span>