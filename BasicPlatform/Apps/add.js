
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

    var f = new form('add')
    f.cg('name', function (cg) {
        var v = cg.j.val()
        if (v.length) return cg.ok()
        else return cg.err()
    }, 'required, name for displaying')

    f.cg('label', function (cg) {
        var v = cg.j.val()
        if (v.length) return cg.ok()
        else return cg.err()
    }, 'required, an unique label to identify this app')

    f.cg('url', function (cg) {
        var v = cg.j.val()
        if (v.length) return cg.ok()
        else return cg.err()
    }, 'required, an url for logging into this app')

    f.cg('secret', function (cg) {
        var v = cg.j.val()
        if (v.length) return cg.ok()
        else return cg.err()
    }, 'required, an secret key for logging into this app')

    f.cg('connectUrl', function (cg) {
        var v = cg.j.val()
        if (v.length) return cg.ok()
        else return cg.reset()
    }, 'an url for connect into this app')

    f.cg('selfConnectable', function (cg) {
        return cg.reset()
    }, 'select wether a user can connect into this app by themself')

    f.cg('accessabel', function (cg) {
        return cg.reset()
    }, 'select wether a user can connect into this app from internet')

})