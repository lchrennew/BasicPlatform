
$(function () {
    var uopt = {
        ajax: {
            url: '/ajax/users/getusers.aspx',
            data: function (term) {
                return { $: term, id: location.search.deparam().id }
            },
            results: function (data) {
                return { results: data }
            }
        }
        , formatResult: function (user) {
            return '<span class="muted">' + user.l +' - '+ user.n + '</span> ' + ' <span class="muted pull-right">' + user.e + '</span>'
        }
        , formatSelection: function (user) { return user.l; }
        , placeholder: 'choose users to be granted to'
        , tags: []
    }
    $('#users').select2(uopt)


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

    var f = new form('grant')
    f.cg('users', function (cg) {
        var v = cg.j.val()
        if (v.length) return cg.ok()
        else return cg.err()
    }, 'choose users to be granted to')

    f.cg('roles', function (cg) {
        return true
    }, 'choose these users\' roles of app')
})