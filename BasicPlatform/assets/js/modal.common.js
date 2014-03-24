/// <reference path="http://static.js.weather.com.cn/uc/js/jquery.js" />

$(function () {
    $('#modal a[data-invoke]').click(function () {
        var a = $(this);
        eval('var m = ' + a.attr('data-invoke'));
        if (a.attr('data-invoke-form')) {
            var f = $('#' + a.attr('data-invoke-form'))
            f.trigger('validate')
            if (f.find('.control-group.error').length) return false;
        }
        if (a.attr('data-invoke-action'))
            m(a.attr('href'), a.attr('data-invoke-target'), a.attr('data-invoke-action'), a.attr('data-invoke-form'));

        return false;
    });
});