/// <reference path="http://static.js.weather.com.cn/uc/js/jquery.js" />
/* File Created: 五月 18, 2012 */
if (!String.prototype.trim) String.prototype.trim = function () {
    return this.replace(/(^\s*)|(\s*$)/g, '');
};
__doPostBack = function (eventTarget, eventArgument) {
    if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
        theForm.__EVENTTARGET.value = eventTarget;
        theForm.__EVENTARGUMENT.value = eventArgument;
    }
}
function form(f, ok, err, action) {
    f = $('form#' + f);
    this.f = f;
    f.errtip = f.find('.alert-error');
    f.suctip = f.find('.alert-success');
    f.ctrlGroups = {};
    f.ok = ok;
    f.err = err;
    f.find('input[type="text"],input[type="password"]').keypress(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault()
            if (f.attr('data-keyboard') == 'on')
                f.btn.click()
        }
    });

    f.btn = f.find('[type="submit"]');
    if (!f.btn.length) {
        f.btn = f.parents('.modal').find('[type="submit"]');
        f.btn.click(function () {
            var btn = $(this);
            f.find('#__EVENTTARGET').val(btn.attr('name'));
            btn.button('loading');
            f.submit();
            return false;
        });
    } else
        f.btn.click(function () {
            var btn = $(this);
            f.find('#__EVENTTARGET').val(btn.attr('name'));
            btn.button('loading');
        });
    f.btn.removeAttr('onclick');

    f.on('validate', function (event) {
        var r = true;
        for (var i in f.ctrlGroups) {
            var cg = f.ctrlGroups[i];
            if (cg.v && !cg.v()) r = false;
        }
        return r
    })

    f.submit(function (event) {
        event.preventDefault();
        f.suctip.addClass('hide')
        f.errtip.addClass('hide')
        //        var cgl = f.ctrlGroups.length;
        var r = true;
        for (var i in f.ctrlGroups) {
            var cg = f.ctrlGroups[i];
            if (cg.v && !cg.v()) r = false;
        }

        if (r) {
            var url = f.attr('action') || location.href;
            var pdata = f.serialize();
            $.post(url, ['fx=1', pdata].join('&'), function (data) {
                if (data.ok) {
                    data.resetbtn && setTimeout(function () { f.btn.button('reset'); }, 1000);
                    if (data.showtip && f.suctip.length)
                        f.suctip.hide().removeClass('hide').show(300, function () {
                            setTimeout(function () {
                                if (f.ok) f.ok(f, data);
                                else if (data.reload) location.replace(data.url);
                                else setTimeout(function () { f.suctip.hide(); }, data.tipspeed || 1000);
                            }, 1000);
                        });
                    else if (f.ok) f.ok(f, data);
                    else if (data.reload) location.replace(data.url);
                } else {
                    data.resetbtn && f.btn.button('reset');
                    for (var i in data.errors) {
                        f.ctrlGroups[i].err(data.errors[i]);
                    }
                    if (f.err) f.err(f, data);
                    else data.showtip && f.errtip.hide().removeClass('hide').show(100)
                }
            }, 'json');
        } else {
            f.btn.button('reset');
        }
        return false;
    });

    this.cg = function (j, v, help) {
        if (f.length > 0) f.ctrlGroups[j] = new ctrlGroup(j, v, help);
    };
    this.getCg = function (j) {
        return f.ctrlGroups[j];
    }

    $('select').each(function (idx, s) {
        var d = $(s).attr('data-init');
        if (d) {
            d = d.split(',');
            var dict = {};
            for (var i in d) {
                dict[d[i]] = true;
            }
            var l = s.options.length;
            for (var i = 0; i < l; i++) {
                var o = s.options[i];
                o.selected = dict[o.value];
            }
        }
    });
}

function ctrlGroup(j, v, help) {
    j = $('#' + j);
    this.j = j;
    this.v0 = j.val();
    this.jcg = j.parents('.control-group');
    this.jtip = this.jcg.find('.help-block, .help-inline') || j.next();
    this.help = help;
    this.jtip.html(help);
    this.err = function (msg) {
        this.jcg.removeClass('success');
        this.jcg.addClass('error');
        this.jtip.html(msg || this.help);
        return false;
    };

    this.ok = function (msg) {
        this.jcg.removeClass('error');
        this.jcg.addClass('success');
        this.jtip.html(msg || this.help);
        return true;
    }

    this.reset = function () {
        this.jcg.removeClass('error');
        this.jcg.removeClass('success');
        this.jtip.html(this.help);
        return true;
    }

    this.resetVal = function () {
        this.j.val(this.v0);
    };

    if (v) {
        var cg = this;
        j.change(function (event) {
            v(cg);
        });
        this.v = function () {
            return v(cg);
        };
    }
}
