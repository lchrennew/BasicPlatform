
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

    $('#roles').select2($.extend(s2opt, { placeholder: 'choose roles for this action' }))
    $('#app').select2($.extend(s2opt, { placeholder: 'choose a app for this action', allowClear: true }))

    var f = new form('edit')
    f.cg('name', function (cg) {
        var v = cg.j.val()
        if (v.length) return cg.ok()
        else return cg.err()
    }, 'required, provide the name for displaying this action')

    f.cg('label', function (cg) {
        var v = cg.j.val()
        if (v.length) return cg.ok()
        else return cg.err()
    }, 'required, provide an unique label to identify this action')

    f.cg('app', function (cg) {
        var v = cg.j.val()
        if (!cg.j.length) return true;
        else if (v.length) return cg.ok()
        else return cg.err()
    }, 'required, choose the app for users to perform this action')

    f.cg('roles', function (cg) {
        return true
    }, 'optional, choose the roles should be able to perform this action')
})