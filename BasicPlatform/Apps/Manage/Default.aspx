<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="BasicPlatform.Apps.Manage.Default" %>
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
            <uc:MgtNav runat="server" ID="ucMgtNav" App='<%#App %>' />
            <ul class="thumbnails">
                <uc:PlaceHolder runat="server" Actions="ViewUsersOfApps,CreateUsersOfApps,AppendUsersOfApps,RemoveUsersOfApps"
                    App='<%#App.Id %>'>
                    <li class="span4">
                        <h4>
                            Manage users</h4>
                        <hr />
                        <div class="caption">
                            <uc:PlaceHolder runat="server" Actions="ViewUsersOfApps,CreateUsersOfApps,AppendUsersOfApps,RemoveUsersOfApps" App='<%#App.Id %>'>
                                <p class="clearfix">
                                    <a href="Users.aspx?id=<%=App.Id %>" class="btn btn-large span12">View Users</a></p>
                            </uc:PlaceHolder>
                            <uc:PlaceHolder runat="server" Actions="CreateUsersOfApps" App='<%#App.Id %>'>
                                <p class="clearfix">
                                    <a href="CreateUser.aspx?id=<%=App.Id %>" class="btn btn-large span12">Create a new user</a></p>
                            </uc:PlaceHolder>
                            <uc:PlaceHolder runat="server" Actions="AppendUsersOfApps" App='<%#App.Id %>'>
                                <p class="clearfix">
                                    <a href="AppendUsers.aspx?id=<%=App.Id %>" class="btn btn-large span12">Add existing users</a></p>
                            </uc:PlaceHolder>
                        </div>
                    </li>
                </uc:PlaceHolder>
                <uc:PlaceHolder runat="server" Actions="ViewActionsOfApps,CreateActionsOfApps,EditActionsOfApps,RemoveActionsOfApps" App='<%#App.Id %>'>
                    <li class="span4">
                        <h4>
                            Manage actions</h4>
                        <hr />
                        <div class="caption">
                            <uc:PlaceHolder runat="server" Actions="ViewActionsOfApps,CreateActionsOfApps,EditActionsOfApps,RemoveActionsOfApps"
                                App='<%#App.Id %>'>
                                <p class="clearfix">
                                    <a href="Actions.aspx?id=<%=App.Id %>" class="btn btn-large span12">View Actions</a></p>
                            </uc:PlaceHolder>
                            <uc:PlaceHolder runat="server" Actions="CreateActionsOfApps" App='<%#App.Id %>'>
                                <p class="clearfix">
                                    <a href="CreateAction.aspx?id=<%=App.Id %>" class="btn btn-large span12">Create a new action</a></p>
                            </uc:PlaceHolder>
                        </div>
                    </li>
                </uc:PlaceHolder>
                <uc:PlaceHolder runat="server" Actions="ViewRolesOfApps,CreateRolesOfApps,AppendRolesOfApps,RemoveRolesOfApps"
                    App='<%#App.Id %>'>
                    <li class="span4">
                        <h4>
                            Manage roles</h4>
                        <hr />
                        <div class="caption">
                            <uc:PlaceHolder runat="server" Actions="ViewRolesOfApps,CreateRolesOfApps,AppendRolesOfApps,RemoveRolesOfApps" App='<%#App.Id %>'>
                                <p class="clearfix">
                                    <a href="Roles.aspx?id=<%=App.Id %>" class="btn btn-large span12">View Roles</a></p>
                            </uc:PlaceHolder>
                            <uc:PlaceHolder runat="server" Actions="CreateRolesOfApps" App='<%#App.Id %>'>
                                <p class="clearfix">
                                    <a href="CreateRole.aspx?id=<%=App.Id %>" class="btn btn-large span12">Create a new role</a></p>
                            </uc:PlaceHolder>
                            <uc:PlaceHolder runat="server" Actions="AppendRolesOfApps" App='<%#App.Id %>'>
                                <p class="clearfix">
                                    <a href="AppendRoles.aspx?id=<%=App.Id %>" class="btn btn-large span12">Add existing roles</a></p>
                            </uc:PlaceHolder>
                        </div>
                    </li>
                </uc:PlaceHolder>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
