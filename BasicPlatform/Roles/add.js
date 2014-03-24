
$(function () {

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
    }, 'required, an unique label to identify this role')
})  