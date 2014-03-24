<%@ Page Title="" Language="C#" MasterPageFile="~/Filter.Master" AutoEventWireup="true"
    CodeBehind="DefaultFilter.aspx.cs" Inherits="BasicPlatform.Users.DefaultFilter" %>

<asp:Content ID="Content2" ContentPlaceHolderID="filter" runat="server">
    <div class="control-group">
        <label class="control-label" for="f.l">
            Full Name
        </label>
        <div class="controls">
            <input type="text" id="f.l" class="input-medium" data-field="l" data-type="s" data-op="regex"
                placeholder="Full Name" /></div>
    </div>
    <div class="control-group">
        <label class="control-label" for="f.n">
            Username
        </label>
        <div class="controls">
            <input type="text" id="f.n" class="input-medium" data-field="n" data-type="s" data-op="regex"
                placeholder="Username" /></div>
    </div>
    <div class="control-group">
        <label class="control-label" for="f.e">
            E-mail
        </label>
        <div class="controls">
            <input type="text" id="f.e" class="input-medium" data-field="e" data-type="s" data-op="regex"
                placeholder="E-mail" /></div>
    </div>
    <uc:PlaceHolder runat="server" Actions="ViewApps,AddApps,EditApps,DeleteApps">
        <div class="control-group">
            <label class="control-label" for="f.a">
                Apps
            </label>
            <div class="controls">
                <select id="f.a" data-field="a" data-type="_" data-op="in" class="input-xxlarge"
                    multiple="multiple">
                    <option value=""></option>
                    <asp:Repeater runat="server" ID="apps">
                        <ItemTemplate>
                            <option data-text="<%#Eval("Label") %>" value="<%#Eval("Id") %>">
                                <%#Eval("Name") %></option>
                        </ItemTemplate>
                    </asp:Repeater>
                </select></div>
        </div>
    </uc:PlaceHolder>
    <uc:PlaceHolder runat="server" Actions="ViewRoles,AddRoles,EditRoles,DeleteRoles">
        <div class="control-group">
            <label class="control-label" for="f.r">
                Roles
            </label>
            <div class="controls">
                <select id="f.r" data-field="r" data-type="_" data-op="in" class="input-xxlarge"
                    multiple="multiple">
                    <option value=""></option>
                    <asp:Repeater runat="server" ID="roles">
                        <ItemTemplate>
                            <option data-text="<%#Eval("Label") %>" value="<%#Eval("Id") %>">
                                <%#Eval("Name") %></option>
                        </ItemTemplate>
                    </asp:Repeater>
                </select></div>
        </div>
    </uc:PlaceHolder>
    <div class="form-actions">
        <button class="btn btn-primary" type="button" id="filter-btn">
            <i class="icon-search icon-white"></i>
            Search</button>
        <a class="btn" href="./">Reset Filter</a></div>
    <script type="text/javascript">
        $(window).on('filter.loaded', function () {
            var s2opt = {
                maximumSelectionSize: 3
                , formatResult: function (state) { return $(state.element).text() }
                , sortResults: function (results, container, query) {
                    return results.sort(function (a, b) {
                        if ($(a.element).attr('data-text') > $(b.element).attr('data-text')) return 1
                        else return -1
                    })
                }
                , matcher: function (term, text, opt) {
                    return text.toUpperCase().indexOf(term.toUpperCase()) >= 0 || opt.attr('data-text').toUpperCase().indexOf(term.toUpperCase()) >= 0;
                }
            }
            $('[id="f.a"]').select2($.extend(s2opt, { placeholder: 'select apps' })).change()
            $('[id="f.r"]').select2($.extend(s2opt, { placeholder: 'select roles' })).change()
        })
    </script>
</asp:Content>
