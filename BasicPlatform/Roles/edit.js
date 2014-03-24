
$(function () {

    var f = new form('edit')

    f.cg('label', function (cg) {
        var v = cg.j.val()
        if (v.length) return cg.ok()
        else return cg.err()
    }, 'required, an unique label to identify this role')

    f.cg('name', function (cg) {
        var v = cg.j.val()
        if (v.length) return cg.ok()
        else return cg.err()
    }, 'required, name for display')
})  