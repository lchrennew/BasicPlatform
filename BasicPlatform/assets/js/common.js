

$(function () {
    String.prototype.deparam = function () {
        var pairs = this.substring(1).split('&'), obj = {}, pair, i;
        for (i in pairs) {
            if (pairs[i] === '') continue;
            pair = pairs[i].split('=');
            obj[decodeURIComponent(pair[0].toLowerCase())] = decodeURIComponent(pair[1]);
        }
        return obj;
    };
    try {
        window.$filter = JSON.parse(location.search.deparam()['$'])
    } catch (e) {
        window.$filter = {}
    }

    $('a[data-invoke]').click(function (event) {
        event.preventDefault()
        var a = $(this);
        eval('var m = ' + a.attr('data-invoke'));
        if (a.attr('data-invoke-action')) {
            var c = $(a.attr('data-invoke-confirm'))
            if (c.length) {
                event.preventDefault()
                a.fadeOut()
                c.fadeIn().find('[data-confirm="yes"]').click(function (e) {
                    e.preventDefault()
                    m(a.attr('href'), a.attr('data-invoke-target'), a.attr('data-invoke-action'), a.attr('data-invoke-form'));

                })
                c.find('[data-confirm="no"]').unbind('click').click(function (e) {
                    e.preventDefault()
                    c.find('[data-confirm="yes"]').unbind('click')
                    c.fadeOut()
                    a.fadeIn()
                })
            }
            else
                m(a.attr('href'), a.attr('data-invoke-target'), a.attr('data-invoke-action'), a.attr('data-invoke-form'));
        }
        return false;
    });
    $('a[title][rel="tooltip"]').tooltip();

    var filter = $('#filter')
    filter
    .load(filter.attr('data-page'), function () {
        $(window).trigger('filter.loading')
        // init view data
        var viewState = {}
        var search = location.search.deparam()
        try { $.extend(viewState, JSON.parse(search['$'])) } catch (e) { } // on JSON syntax error
        for (var key in viewState) {
            var p = key.split('!'), f = p[0], t = p[1], o = p[2], s = '[data-field="' + f + '"][data-type="' + t + '"]', v = viewState[key]
            if (o) s += ('[data-op="' + o + '"]')
            else s += ':not([data-op])'

            if (!$.isArray(v)) {
                var sel = []
                sel.push(s + ':text')
                sel.push('textarea' + s)
                sel.push('input[type="hidden"]' + s)
                filter.find(sel.join(', ')).val(v)
                filter.find(s + ':radio').each(function () { if (this.value == v) this.checked = true })
                filter.find('select' + s + ' option').each(function () { if ($(this).val() == v) this.selected = true })
                filter.find(s + ':checkbox').each(function () { if ($(this).val() == v) this.checked = true })
            }
            else {
                filter.find('select' + s + ' option').each(function () { this.selected = (v.indexOf($(this).val()) >= 0) })
                filter.find(s + ':checkbox').each(function () { this.checked = (v.indexOf($(this).val()) >= 0) })
            }
        }

        filter.find('[data-parameter]:text, input[type="hidden"][data-parameter], textarea[data-parameter]').each(function () {
            $(this).val(search[$(this).attr('data-parameter')])
        })
        filter.find('[data-parameter]:radio,[data-parameter]:checkbox').each(function () {
            var s = search[$(this).attr('data-parameter')]
            if (s) {
                s = s.split(',')
                if (s.indexOf($(this).val())) this.checked = true
            }
        })
        filter.find('select[data-parameter] option').each(function () {
            if ($(this).val()) {
                var s = search[$(this).parent().attr('data-parameter')]
                if (s) {
                    s = s.split(',')
                    if (s.indexOf($(this).val())) this.selected = true
                }
            }
        })

        // init submit event & prevent default event
        filter.find('.btn-primary').click(function (event) {
            $(window).trigger('filter.submit')
            var q = {}
            // 服务器端使用BsonDocument.Elements来遍历，'created':{$gt:date}这种方式无法正确遍历到, 对应的正常情况是'created':date，所以需要把运算符放到key里面进行传递
            // 缺点是只能做两层的bson，不能做多层的and & or逻辑筛选
            // key格式：字段名称:类型:运算符(不加运算符表示“eq”)
            // 分隔符可以使用：~、!、*、-、_
            filter.find('[data-field]:text, input[type="hidden"][data-field], textarea[data-field], [data-field]:radio:checked').each(function () {
                var i = $(this)
                var op = i.attr('data-op')
                var k = [i.attr('data-field'), i.attr('data-type')]
                op && k.push(op)
                k[0] && k[1] && i.val() && (q[k.join('!')] = i.val())
            })
            var m = {}
            filter.find('[data-field]:checkbox:checked').each(function () {
                var i = $(this)
                var op = i.attr('data-op')
                var k = [i.attr('data-field'), i.attr('data-type')]
                op && k.push(op)
                k = k.join('!')
                if (i.val())
                    if (!m[k]) m[k] = i.val()
                    else if ($.isArray(m[k])) m[k].push(i.val())
                    else m[k] = [m[k], i.val()]
            })
            filter.find('select[data-field]').each(function () {
                var i = $(this)
                var op = i.attr('data-op')
                var k = [i.attr('data-field'), i.attr('data-type')]
                op && k.push(op)
                k = k.join('!')
                var opts = i.find('option:selected')
                if (opts.length)
                    opts.each(function () {
                        if (this.value)
                            if (!m[k]) m[k] = this.value
                            else if ($.isArray(m[k])) m[k].push(this.value)
                            else m[k] = [m[k], this.value]
                    })
            })
            q = $.extend(q, m)
            var searchParameters = filter.attr('data-parameters')
            var search = location.search.deparam()
            if (searchParameters)
                searchParameters = searchParameters.split(',')
            else
                searchParameters = []

            var u = location.pathname + '?$=' + encodeURIComponent(JSON.stringify(q))
            for (var i = 0; i < searchParameters.length; i++) {
                var pn = searchParameters[i]
                if (pn && search[pn]) u += '&' + pn + '=' + encodeURIComponent(search[pn])
            }

            filter.find('[data-parameter]').each(function () {
                var p = $(this).attr('data-parameter'), v = $(this).val()
                if (v)
                    u += '&' + p + '=' + encodeURIComponent(v)
            })

            location.href = u
        })


        $(window).trigger('filter.loaded')
    })
    .keypress(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault()
            $(this).find('#filter-btn').click()
        }
    })

});