<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditRole.aspx.cs" Inherits="BasicPlatform.Apps.Manage.EditRole" %>

<%@ Register Src="~/UserControls/MgtNav.ascx" TagPrefix="uc" TagName="MgtNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="row-fluid">
        <div class="span12">
            <div class="page-header">
                <h1>Manage App <small>Manage users, roles and actions of
                        <%=App.Name %></small></h1>
            </div>
            <uc:PlaceHolder runat="server" Actions="AppendRolesOfApps" App='<%#App.Id %>'>
                <div class="btn-group pull-right">
                    <a class="btn" href="AppendRoles.aspx?id=<%=ContextHelper.ObjId %>"><i class="icon-plus-sign"></i>
                        Add roles</a> <a class="btn dropdown-toggle" data-toggle="dropdown"><span class="caret"></span></a>
                    <ul class="dropdown-menu">
                        <!-- dropdown menu links -->
                        <li><a href="Roles.aspx?id=<%=ContextHelper.ObjId %>">View roles</a></li>
                        <uc:PlaceHolder runat="server" Actions="CreateRolesOfApps" App='<%#App.Id %>'>
                            <li><a href="CreateRole.aspx?id=<%=ContextHelper.ObjId %>">Create a new role</a></li>
                        </uc:PlaceHolder>
                    </ul>
                </div>
            </uc:PlaceHolder>
            <uc:PlaceHolder runat="server" Actions="EditRolesOfApps,RemoveRolesOfApps"
                ExcludeActions="AppendRolesOfApps,CreateRolesOfApps" App='<%#App.Id %>'>
                <a href="Roles.aspx?id=<%=App.Id %>" class="btn pull-right"><i class="icon-circle-arrow-left"></i>
                    View roles</a>
                <uc:PlaceHolder runat="server" Actions="CreateRolesOfApps" App='<%#App.Id %>'>
                    <ul class="dropdown-menu">
                        <!-- dropdown menu links -->
                        <li><a href="CreateRole.aspx?id=<%=ContextHelper.ObjId %>">Create a new role</a></li>
                    </ul>
                </uc:PlaceHolder>
            </uc:PlaceHolder>
            <uc:PlaceHolder runat="server" Actions="CreateRolesOfApps" ExcludeActions="AppendRolesOfApps" App='<%#App.Id %>'>
                <div class="btn-group pull-right">
                    <a href="Roles.aspx?id=<%=App.Id %>" class="btn pull-right"><i class="icon-circle-arrow-left"></i>
                        View roles</a>
                    <uc:PlaceHolder runat="server" Actions="CreateRolesOfApps" App='<%#App.Id %>'>
                        <ul class="dropdown-menu">
                            <!-- dropdown menu links -->
                            <li><a href="Roles.aspx?id=<%=ContextHelper.ObjId %>">View roles</a></li>
                            <li><a href="CreateRole.aspx?id=<%=ContextHelper.ObjId %>">Create a new role</a></li>
                        </ul>
                    </uc:PlaceHolder>

                </div>
            </uc:PlaceHolder>
            <uc:MgtNav runat="server" App='<%#App %>' NavId="3" />
            <form runat="server" id="edit" class="form-horizontal">
                <fieldset>
                    <legend>Edit role which will be able to access this app</legend>
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
                            <asp:TextBox runat="server" ID="label" placeholder="enter an unique label" ReadOnly="true" />
                        </div>
                    </div>
                    <div class="form-actions">
                        <uc:Button ID="Button1" runat="server" OnServerClick="Save" class="btn btn-primary">
                            Save
                        </uc:Button>
                        <a href="Roles.aspx?id=<%=App.Id %>" class="btn">Cancel</a>
                    </div>
                </fieldset>
            </form>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script src="editrole.js" type="text/javascript"></script>
</asp:Content>
