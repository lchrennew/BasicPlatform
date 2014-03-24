<%@ Page Title="Create a new app" Language="C#" MasterPageFile="~/Ajax/Modal.Master"
    AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="BasicPlatform.Ajax.Apps.Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <form runat="server" class="form-horizontal" id="apps_add">
    <fieldset>
        <div class="control-group">
            <label class="control-label">
                Name</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="appname" CssClass="input-medium" placeholder="enter name of this app" /></div>
        </div>
        <div class="control-group">
            <label class="control-label">
                Label</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="applabel" CssClass="input-medium" placeholder="enter label of this app" />
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="appsecret">Secret Key</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="appsecret" CssClass="input-xlarge" placeholder="enter secret key here" />
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="url">
                Url</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="appurl" CssClass="input-xlarge" placeholder="enter url here" />
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="connectUrl">
                Connect Url</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="connectUrl" CssClass="input-xlarge" placeholder="enter connect url here" />
                <p class="help-inline">
                </p>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="selfConnectable"></label>
            <div class="controls">
                <label class="checkbox">
                    <asp:CheckBox runat="server" ID="selfConnectable" /> Self Connectable
                </label>
            </div>
        </div>
    </fieldset>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <a href="#" class="btn btn-primary" data-invoke="$ajax.action" data-invoke-form="apps_add"
        data-invoke-target="apps" data-invoke-action="add">Create App</a> <a href="#" class="btn"
            data-dismiss="modal">Cancel</a>
</asp:Content>
