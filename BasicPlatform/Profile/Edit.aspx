<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Edit.aspx.cs" Inherits="BasicPlatform.Profile.Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="row-fluid">
        <div class="span12">
            <div class="page-header">
                <h1>Profile <small>Edit my profile</small></h1>
            </div>
            <form runat="server" id="edit" class="form-horizontal">
                <fieldset>
                    <div class="alert alert-success hide">
                        Saved successful.
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            Username</label>
                        <div class="controls">
                            <asp:TextBox runat="server" ID="username" CssClass="input-medium" placeholder="enter username here"
                                ReadOnly="true" />
                            <p class="help-inline">
                            </p>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="label">
                            Full Name</label>
                        <div class="controls">
                            <asp:TextBox runat="server" ID="label" CssClass="input-medium" placeholder="enter full name here" />
                            <p class="help-inline">
                            </p>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="email">
                            E-mail</label>
                        <div class="controls">
                            <asp:TextBox runat="server" ID="email" CssClass="input-medium" placeholder="enter e-mail here" />
                            <p class="help-inline">
                            </p>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="password">
                            Password</label>
                        <div class="controls">
                            <asp:TextBox runat="server" ID="password" CssClass="input-medium" placeholder="enter password here"
                                TextMode="Password" />
                            <p class="help-inline">
                            </p>
                        </div>
                    </div>
                    <div class="form-actions">
                        <uc:Button runat="server" OnServerClick="Save" class="btn btn-primary btn-large"
                            LoadingText="Saving...">
                            Save changes
                        </uc:Button>
                        <a class="btn btn-large" href="/">Cancel</a>
                    </div>
                </fieldset>
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script src="/assets/js/security.js"></script>
    <script type="text/javascript" src="edit.js"></script>

    <script>
        $(function () {
            var lvs = [
            { bar: 'progress progress-danger', text: 'Dangerous', tc: 'text-error' },
            { bar: 'progress progress-warning', text: 'Tolerable', tc: 'text-warning' },
            { bar: 'progress progress-success', text: 'Good', tc: 'text-success' },
            { bar: 'progress progress-success', text: 'Perfect', tc: 'text-success' }
            ]
            function getClass(s) {
                if (s < 60) return lvs[0]
                else if (s < 80) return lvs[1]
                else if (s < 90) return lvs[2]
                else return lvs[3]
            }
            $('#password')
                .popover({
                    html: true,
                    placement: 'right',
                    content: function () {
                        var c = $('<div style="width:200px;"><p>Strength: <span id="pwdlv">0</span></p><p>Scrore: <span id="pwdscr">0</span></p><div class="progress"><div class="bar"></div></div></div>'),
                            s = $(this).val().getPwdLv(),
                            lv = getClass(s)
                        c.find('#pwdlv').html(lv.text).attr('class',lv.tc)
                        c.find('#pwdscr').html(s).attr('class', lv.tc)
                        c.find('.progress').addClass(lv.bar).find('.bar').css('width', s + '%')
                        return c
                    },
                    trigger: 'focus'
                })
            .on('keypress change keydown, keyup', function (event) {
                var s = $(this).val().getPwdLv(),
                            lv = getClass(s)
                $('#pwdlv').html(lv.text).attr('class', lv.tc).parent().next().find('#pwdscr').html(s).attr('class', lv.tc).parent().next().attr('class', lv.bar).find('.bar').css('width', s + '%')
            })
        })
    </script>
</asp:Content>
