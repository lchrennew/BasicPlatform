/// <reference path="http://static.js.weather.com.cn/uc/js/jquery.js" />

$ajax = {};
$ajax.modal = function (search, tgt, act) {
    if (!$ajax.c) {
        $(document.body).append('<div id="modal"></div>');
        $ajax.c = $('#modal');
    }
    $('.modal').modal('hide');
    try {
        search = JSON.parse(search)
    } catch (e) {
        search = { '$': search }
    }

    $ajax.c.load('/ajax/' + tgt + '/' + act + '.aspx', search, $ajax._modal);
};

$ajax._modal = function () {
    $ajax.c.find('.modal').modal('show');
    $('a[title]').tooltip();
    $ajax.c.find('.pager a[href]').click($ajax._refresh);
};

$ajax._refresh = function (event) {
    $('.modal').modal('hide');
    $ajax.c.load(event.target.href, $ajax._modal);
    return false;
};

$ajax.action = function (search, tgt, act, form) {
    var d = ''
    if (form && !form.indexOf('?'))
        d = form + '=' + location.search.deparam()[form.replace('?', '')] + ('&$=' + encodeURIComponent(search));
    else {
        form = $('#' + form).serialize()
        d = ('?$=' + encodeURIComponent(search))
    }
    $.post('/ajax/' + tgt + '/' + act + '.aspx' + d, form, function (data) {
        if (data.ok) {
            $(window).trigger($.extend($.Event([tgt, act, 'ok'].join('.')), data.data))
            $('.modal').modal('hide')
            if (data.reload) location.replace(location.href);
        }
    }, 'json');
}