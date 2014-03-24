<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="CreateRole.aspx.cs" Inherits="BasicPlatform.Apps.Manage.CreateRole" %>

<%@ Register Src="~/UserControls/MgtNav.ascx" TagPrefix="uc" TagName="MgtNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="row-fluid">
        <div class="span12">
            <div class="page-header">
                <h1>
                    Manage App <small>Manage users, roles and actions of
                        <%=App.Name %></small></h1>
            </div>
            <uc:PlaceHolder runat="server" Actions="AppendRolesOfApps" App='<%#App.Id %>'>
                <div class="btn-group pull-right">
                    <a class="btn" href="AppendRoles.aspx?id=<%=ContextHelper.ObjId %>"><i class="icon-plus-sign">
                    </i>
                        Add roles</a> <a class="btn dropdown-toggle" data-toggle="dropdown"><span class="caret">
                        </span></a>
                    <ul class="dropdown-menu">
                        <!-- dropdown menu links -->
                        <li><a href="Roles.aspx?id=<%=ContextHelper.ObjId %>">View roles</a></li>
                    </ul>
                </div>
            </uc:PlaceHolder>
            <uc:PlaceHolder runat="server" Actions="CreateRolesOfApps,ViewRolesOfApps,RemoveRolesOfApps"
                ExcludeActions="AppendRolesOfApps" App='<%#App.Id %>'>
                <a href="Roles.aspx?id=<%=App.Id %>" class="btn pull-right"><i class="icon-circle-arrow-left">
                </i>
                    View roles</a>
            </uc:PlaceHolder>
            <uc:MgtNav runat="server" ID="ucMgtNav" App='<%#App %>' NavId="3" />
            <form runat="server" id="grant" class="form-horizontal">
            <fieldset>
                <legend>Create a role which will be able to access this app </legend>
                <div class="control-group">
                    <label class="control-label">
                        Name
                    </label>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="name" placeholder="enter name here" />
                        <p class="help-inline">
                        </p>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">
                        Label</label>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="label" placeholder="enter an unique label" />
                        <p class="help-inline">
                        </p>
                    </div>
                </div>
                <div class="form-actions">
                    <uc:Button runat="server" OnServerClick="Create" class="btn btn-primary">
                        Create</uc:Button>
                    <a href="Roles.aspx?id=<%=App.Id %>" class="btn">Cancel</a>
                </div>
            </fieldset>
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script src="createrole.js" type="text/javascript"></script>
</asp:Content>
