$(function () {

    var f = new form('edit')
    f.cg('name', function (cg) {
        var v = cg.j.val()
        if (v.length) return cg.ok()
        else return cg.err()
    }, 'required, name of the role')
})