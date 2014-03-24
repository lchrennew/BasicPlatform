
$(function () {
    var ropt = {
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
            , placeholder: 'choose roles for this account'
    }
    $('#roles').select2(ropt)

    $(window).on('apps.addrole.ok', function (event) {
        $('#roles')
            .append($('<option></option>').text(event.n).attr({ value: event.id, 'data-text': event.l }).prop('selected', true)).change()
    })

    var f = new form('grant', function (f, data) {
        var group = $('#group'), alias = $('#alias'), username = $('#username')
        $(window)
            .on('bind.ok', function (event) { // 设置完成用户名触发此事件，开始设置用户组
                if (group.length) // 如果有用户组，开始设置用户组
                    $.getScript(group.attr('data-url') + '?setgroups=1&g=' + encodeURIComponent(group.val()) + '&a=' + encodeURIComponent(event.alias)) // 触发apps.setgroup.ok，传回id
                else
                    location.replace(data.url)
            })
            .on('apps.setgroup.ok', function () {
                $(window).on('js.setgroup.signout', function () {
                    location.replace(data.url)
                })
                $.getScript($('#group').attr('data-url') + '?logout=js.setgroup.signout')
            })

        if (alias.length) // 如果有alias，则开始设置用户名，否则跳转回列表
        {
            if (!alias.val()) {
                if ((con == 'y' && group.length) || con != 'y')
                    $.getScript(data.data + '?setalias=1&u=' + encodeURIComponent(username.val()) + '&a=' + encodeURIComponent(username.val()))
                else
                    location.replace(data.url)
            }
            else
                $.getScript(data.data + '?setalias=1&u=' + encodeURIComponent(username.val()) + '&a=' + encodeURIComponent(alias.val()))
        }
        else
            location.replace(data.url)
    })

    f.cg('roles', function (cg) {
        return true
    }, 'choose these users\' roles of app')

    f.cg('alias', function (cg) {
        if (cg.j) {
            var v = cg.j.val()
            if (v && v.length) return cg.ok()
            else return cg.reset()
        } else return cg.reset()
    }, 'enter the app account to connect')

    f.cg('group', function (cg) {
        if (!cg.j) return cg.reset()
        else {
            var v = cg.j.val()
            if (v && v.length) return cg.ok()
            else return cg.reset()
        }
    }, 'select a group for this user')

    $(window)
        .on('apps.groups.loaded', function (event) { // 用户组列表数据加载完成，绑定下拉列表
            if (event.groups && event.groups.length) {
                var s = $('#group')
                s.parents('.control-group').removeClass('hide')
                $(event.groups).each(function () {
                    s.append($('<option></option>').val(this.k).text(this.v))
                })
                if ($('#alias').val())
                    $.getScript($('#group').attr('data-url') + '?getgroups=1&a=' + encodeURIComponent($('#alias').val()))
            }
            else {
                $.getScript($('#group').attr('data-url') + '?logout=silent')
                $('#group').parents('.control-group').remove()
            }
        })
    .on('apps.usergroups.loaded', function (event) {
        if (event.groups && event.groups.length)
            $('#group option').each(function () {
                if (this.value == event.groups[0])
                    this.selected = true
            })
        $.getScript($('#group').attr('data-url') + '?logout=silent')
    })
    $.getScript($('#group').attr('data-url') + '?getgroups=1')
})