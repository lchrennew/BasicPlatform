<%@ Page Title="Create a new role" Language="C#" MasterPageFile="~/Ajax/Modal.Master" AutoEventWireup="true" CodeBehind="AddRole.aspx.cs" Inherits="BasicPlatform.Ajax.Apps.AddRole" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <form runat="server" class="form-horizontal" id="roles_add">
    <fieldset>
        <div class="control-group">
            <label class="control-label">
                Name</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="rolename" CssClass="input-medium" placeholder="enter name of this role" /></div>
        </div>
        <div class="control-group">
            <label class="control-label">
                Label</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="rolelabel" CssClass="input-medium" placeholder="enter label of this role" />
            </div>
        </div>
    </fieldset>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <a href="<%=App.Id %>" class="btn btn-primary" data-invoke="$ajax.action" data-invoke-form="roles_add" data-invoke-target="apps" data-invoke-action="addrole">Create role</a>
    <a href="#" class="btn" data-dismiss="modal">Cancel</a>
</asp:Content>
