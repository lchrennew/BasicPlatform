<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="BasicPlatform.Roles.Add" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="page-header">
        <h1>
            Roles <small>Create a new role</small></h1>
    </div>
    <div class="row-fluid">
        <form runat="server" id="add" class="form-horizontal">
        <fieldset>
            <div class="alert alert-success hide">
                Created successful.</div>
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
                    LoadingText="Creating role...">
                    Create this role</uc:Button>
                <a class="btn btn-large" href="./">Cancel</a>
            </div>
        </fieldset>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script type="text/javascript" src="add.js"></script>
</asp:Content>