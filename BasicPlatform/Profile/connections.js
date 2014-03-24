
$(function () {

    var f = new form('edit')

    f.cg('label', function (cg) {
        var v = cg.j.val()
        if (v.length) return cg.ok()
        else return cg.err()
    }, 'required, your full name for display')

    f.cg('email', function (cg) {
        var v = cg.j.val()
        if (v.length) return cg.ok()
        else return cg.err()
    }, 'required, your E-mail')

    f.cg('password', function (cg) {
        var v = cg.j.val()
        if (v.length) return cg.ok()
        else return cg.reset()
    }, 'optional, password for signing in, leave empty if you don\'t want to change it')
})  