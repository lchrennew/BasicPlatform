<%@ Page Title="Connect Account To App" Language="C#" MasterPageFile="~/Ajax/Modal.Master" AutoEventWireup="true" CodeBehind="Connect.aspx.cs" Inherits="BasicPlatform.Ajax.Apps.Connect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <form runat="server" class="form-horizontal" id="apps_connect">
        <fieldset>
            <div class="alert alert-success hide">
                Connected successful.</div>
            <div class="control-group">
                <label class="control-label">User</label>
                <div class="controls">
                    <input type="text" readonly="readonly" disabled="disabled" value="<%=Username %>" class="input-large" id="apps_connect_username" />
                </div>
            </div>
            <div class="control-group">
                <label class="control-label">Alias</label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="apps_connect_alias" CssClass="input-large" placeholder="enter connection alias here"></asp:TextBox>
                    <p class="help-inline"></p>
                </div>
            </div>
        </fieldset>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <a href="<%=AppUrl %>" class="btn btn-primary" id="connectbtn">Connect</a> <a href="#" class="btn"
        data-dismiss="modal">Cancel</a>
    <script>
        var f = new form('apps_connect')
        f.cg('apps_connect_alias', function (cg) {
            var v = cg.j.val()
            if (v.length) return cg.ok()
            else return cg.err()
        }, 'required, the account name of app')
        $('#connectbtn').click(function (event) {
            event.preventDefault()
            if ($('#apps_connect').trigger('validate').find('.error').length) return;
            $.getScript(this.href + '?setalias=1&u=' + encodeURIComponent($('#apps_connect_username').val()) + '&a=' + encodeURIComponent($('#apps_connect_alias').val()))
        })
        $(window).on('bind.ok', function () {
            $('#apps_connect .alert-success').show(300, function () {
                setTimeout(function () { location.reload(true) }, 1000);
            });
        })
    </script>
</asp:Content>
