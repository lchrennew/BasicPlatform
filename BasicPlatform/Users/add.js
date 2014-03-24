
$(function () {
    var s2opt = {
        formatResult: function (state) { return $(state.element).text() }
            , sortResults: function (results, container, query) {
                return results.sort(function (a, b) {
                    if ($(a.element).attr('data-text') > $(b.element).attr('data-text')) return 1
                    else return -1
                });
            }
            , matcher: function (term, text, opt) {
                return text.toUpperCase().indexOf(term.toUpperCase()) >= 0 || opt.attr('data-text').toUpperCase().indexOf(term.toUpperCase()) >= 0;
            }
    }

    $('#roles').select2($.extend(s2opt, { placeholder: 'choose roles for this account' }))

    function rowChange(event) {
        var tr = $(this).parents('tr')
        if (this.checked) {

            tr
                .find('select,input:text')
                .prop({ disabled: false })
            tr.find('[data-control="roles"]').select2({
                multiple: true,
                ajax: {
                    url: '/ajax/apps/getroles.aspx?id=' + $(this).val(),
                    dataType: 'json',
                    results: function (data) {
                        return { results: data }
                    }
                }
            })
            $(window).trigger($.extend($.Event('apps.checked'), {
                url: tr.attr('data-url') + '?getgroups=1',
                id: $(this).val()
            }))
        } else {
            tr.find('[data-control="roles"]').select2('destroy').val('')
            tr.find(':text,select').prop({ disabled: true }).find('option').remove()
        }
    }

    $('[name="apps"]').change(rowChange)


    var f = new form('add', function (f, data) {
        var apps = $('[name="apps"]:checked'), username = $('#username').val(), i = 0
        if (apps.length) {
            var app = apps[i]
            function start(app) {
                i++
                var id = app.value
                var tr = $(app).parents('tr'), a = tr.find('[data-control="alias"]')
                if (a.length) {
                    var alias = a.val() || username
                    $.getScript(tr.attr('data-url') + '?setalias=1&u=' + encodeURIComponent(username) + '&a=' + encodeURIComponent(alias)) // 触发bind.ok，传回event.id
                } else {
                    $(window).trigger('apps.setgroup.ok')
                }
            }
            $(window).on('apps.setgroup.ok', function (event) {
                $(window).on('js.setgroup.signout', function () {
                    // 开始下一条勾选的处理
                    if (i < apps.length) {
                        app = apps[i]
                        start(app)
                    }
                    else location.replace(data.url)
                })
                // 注销
                if (event.id) {
                    var tr = $('[data-app="' + event.id + '"]')
                    $.getScript(tr.attr('data-url') + '?logout=js.setgroup.signout')
                } else
                    $(window).trigger('js.setgroup.signout')
            })
            start(app)
        }
        else location.replace(data.url)

    })
    f.cg('username', function (cg) {
        var v = cg.j.val()
        if (v.length) return cg.ok()
        else return cg.err()
    }, 'required, 6 to 10 alphanumeric charactors')

    f.cg('label', function (cg) {
        var v = cg.j.val()
        if (v.length) return cg.ok()
        else return cg.err()
    }, 'required, full name for display')

    f.cg('email', function (cg) {
        var v = cg.j.val()
        if (v.length) return cg.ok()
        else return cg.err()
    }, 'required, E-mail for communicating with this user')

    f.cg('password', function (cg) {
        var v = cg.j.val()
        if (v.length) return cg.ok()
        else return cg.err()
    }, 'required, signin password')

    f.cg('roles', function (cg) {
        return true
    }, 'optional, roles this account belong to')

    f.cg('apps', function (cg) {
        return true
    }, 'optional, apps this user can enter into')


    $(window).on('roles.add.ok', function (event) {
        $('#roles')
            .append($('<option></option>').text(event.n).attr({ value: event.id, 'data-text': event.l }).prop('selected', true)).change()
    }).on('apps.add.ok', function (event) { // 应用创建完成后触发此事件，更新界面的应用列表
        var tr = $('<tr><td><label class="checkbox inline"><input type="checkbox" name="apps" value="' + event.id + '" /></label></td><td></td><td></td><td></td></tr>')
        .attr({
            'data-app': event.id,
            'data-url': event.url
        })

        tr.find('label.checkbox').append(event.n)
        $('#apps tbody').append(tr)
        if (event.url) {
            tr.find('td:nth-child(2)').append($('<select class="input-medium"></select>').attr({
                'data-control': 'group',
                disabled: true
            }))
            tr.find('td:nth-child(3)').append($('<input type="text" class="input-medium" />').attr({
                'data-control': 'alias',
                disabled: true
            }))
            tr.find('td:nth-child(4)').append($('<input type="text" class="span12" />').attr({
                'data-control': 'roles',
                disabled: true
            }))
        }
        tr.find(':checkbox').change(rowChange).prop('checked', true).change()
    }).on('apps.checked', function (event) { // 勾选应用后触发此事件，调用远端获取用户组数据
        $.getScript(event.url) // 触发apps.group.loaded事件，传入id & groups
    }).on('apps.groups.loaded', function (event) { // 用户组列表数据加载完成，绑定下拉列表
        var tr = $('[data-app="' + event.id + '"]'), s = tr.find('[data-control="group"]').html('')
        $(event.groups).each(function () {
            s.append($('<option></option>').val(this.k).text(this.v))
        })
        if (tr.attr('data-url'))
            $.getScript(tr.attr('data-url') + '?logout=silent')
    }).on('bind.ok', function (event) { // 设置完成用户名触发此事件，开始设置用户组
        var tr = $('[data-app="' + event.id + '"]'), s = tr.find('select')
        $.getScript(tr.attr('data-url') + '?setgroups=1&g=' + encodeURIComponent(s.val()) + '&a=' + encodeURIComponent(event.alias)) // 触发apps.setgroup.ok，传回id
    })

})