<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="BasicPlatform.Roles.Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="page-header">
        <h1>
            Roles <small>Edit an existing role</small></h1>
    </div>
    <div class="row-fluid">
        <form runat="server" id="edit" class="form-horizontal">
        <fieldset>
            <div class="alert alert-success hide">
                Saved successful.</div>
            <div class="control-group">
                <label class="control-label" for="name">
                    Name</label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="name" CssClass="input-medium" placeholder="enter name here" />
                    <p class="help-inline">
                    </p>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label" for="label">
                    Label</label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="label" CssClass="input-medium" placeholder="enter label here" />
                    <p class="help-inline">
                    </p>
                </div>
            </div>
            <div class="form-actions">
                <uc:Button runat="server" OnServerClick="Save" class="btn btn-primary btn-large"
                    LoadingText="Saving...">
                    Save changes</uc:Button>
                <a class="btn btn-large" href="./">Cancel</a>
            </div>
        </fieldset>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script type="text/javascript" src="edit.js"></script>
</asp:Content>