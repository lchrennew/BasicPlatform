<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Users.aspx.cs" Inherits="BasicPlatform.Apps.Manage.Users" %>

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
            <uc:PlaceHolder runat="server" Actions="AppendUsersOfApps" App="<%#App.Id %>">
                <div class="btn-group pull-right">
                    <a class="btn" href="AppendUsers.aspx?id=<%=ContextHelper.ObjId %>"><i class="icon-plus-sign"></i>
                        Add users</a>
                    <uc:PlaceHolder runat="server" Actions="CreateUsersOfApps" App="<%#App.Id %>">
                        <a class="btn dropdown-toggle" data-toggle="dropdown"><span class="caret"></span>
                        </a>
                        <ul class="dropdown-menu">
                            <!-- dropdown menu links -->
                            <li><a href="CreateUser.aspx?id=<%=ContextHelper.ObjId %>">Create a new user</a></li>
                        </ul>
                    </uc:PlaceHolder>
                </div>
            </uc:PlaceHolder>
            <uc:PlaceHolder runat="server" Actions="CreateUsersOfApps" ExcludeActions="AppendUsersOfApps"
                App="<%#App.Id %>">
                <a class="btn pull-right" href="AppendUsers.aspx?id=<%=ContextHelper.ObjId %>"><i
                    class="icon-plus-sign"></i>
                    Create a new user</a>
            </uc:PlaceHolder>
            <uc:MgtNav runat="server" ID="ucMgtNav" App="<%#App %>" NavId="1" />
            <form>
                <fieldset>
                    <legend>View users</legend>
                </fieldset>
            </form>
            <div class="accordion" id="accordionFilter">
                <div class="accordion-group">
                    <div class="accordion-heading">
                        <h5 class="accordion-toggle muted" data-toggle="collapse" data-parent="#accordionFilter"
                            href="#collapseFilter">
                            <i class="icon-search"></i>
                            Filter</h5>
                    </div>
                    <div id="collapseFilter" class="accordion-body collapse in">
                        <div class="accordion-inner">
                            <form class="form form-horizontal">
                                <fieldset data-page="usersfilter.aspx?id=<%#App.Id %>" id="filter" data-parameters="id">
                                </fieldset>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
            <h4>Results</h4>
            <table class="table table-bordered table-hover table-striped">
                <colgroup>
                    <col width="150px" />
                    <col width="150px" />
                    <asp:PlaceHolder runat="server" Visible="<%#!string.IsNullOrEmpty(App.ConnectUrl) %>">
                        <col width="150px" />
                    </asp:PlaceHolder>
                    <col width="350px" />
                    <col />
                </colgroup>
                <thead>
                    <tr>
                        <th>Username
                        </th>
                        <th>Full Name
                        </th>
                        <asp:PlaceHolder runat="server" Visible="<%#!string.IsNullOrEmpty(App.ConnectUrl) %>">
                            <th>Alias
                            </th>
                        </asp:PlaceHolder>
                        <th>Roles
                        </th>
                        <th>Actions
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rpt" ItemType="BasicPlatform.Web.Models.User">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%# Item.Username%>
                                </td>
                                <td>
                                    <%# Item.Label%>
                                </td>
                                <asp:PlaceHolder runat="server" Visible="<%#!string.IsNullOrEmpty(App.ConnectUrl) %>">
                                    <td>
                                        <%# GetAlias(Item.Id) %>
                                    </td>
                                </asp:PlaceHolder>
                                <td>
                                    <asp:Repeater runat="server" DataSource='<%#GetRoles(Item.Id)%>' ItemType="BasicPlatform.Web.Models.Role">
                                        <ItemTemplate>
                                            <span class="label<%#Item.Label=="AppAdmin"?" label-info":"" %>"><i
                                                class="icon-user icon-white"></i>
                                                <%#Item.Name %></span>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </td>
                                <td>
                                    <asp:PlaceHolder runat="server" Visible="<%# ContextHelper.User.IsSysAdmin() || ContextHelper.User.IsAppAdmin(App) || !GetRoles(Item.Id).Contains(Role.AppAdmin) %>">
                                        <uc:PlaceHolder runat="server" Actions="ViewRolesOfApps" App="<%#App.Id %>">
                                            <uc:PlaceHolder runat="server" Actions="EditUsersOfApps" App="<%#App.Id %>">
                                                <a href="EditUser.aspx?id=<%=App.Id %>&$=<%#Item.Id %>">EDIT</a>
                                            </uc:PlaceHolder>
                                        </uc:PlaceHolder>
                                        <uc:PlaceHolder runat="server" Actions="RemoveUsersOfApps" App="<%#App.Id %>">
                                            <a href="<%#Item.Id %>" data-invoke="$ajax.action" data-invoke-target="apps" data-invoke-action="removeuser"
                                                data-invoke-form="?id">REMOVE</a>
                                        </uc:PlaceHolder>
                                    </asp:PlaceHolder>
                                    <asp:PlaceHolder runat="server" Visible="<%#!string.IsNullOrEmpty(App.ConnectUrl) %>">
                                        <uc:PlaceHolder runat="server" Actions="ConnectUsersOfApps" App="<%#App.Id %>">
                                            <a href="{&quot;$&quot;:&quot;<%#Item.Id %>&quot;,&quot;id&quot;:&quot;<%#App.Id %>&quot;}" data-invoke="$ajax.modal" data-invoke-target="apps" data-invoke-action="connect">CONNECT</a>
                                        </uc:PlaceHolder>
                                    </asp:PlaceHolder>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
