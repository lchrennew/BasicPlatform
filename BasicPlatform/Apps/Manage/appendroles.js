
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

    var f = new form('grant')

    f.cg('roles', function (cg) {
        return true
    }, 'choose roles to add')
})