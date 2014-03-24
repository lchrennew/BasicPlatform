<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Roles.aspx.cs" Inherits="BasicPlatform.Apps.Manage.Roles" %>

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
                        Add roles</a>
                    <uc:PlaceHolder runat="server" Actions="CreateRolesOfApps" App='<%#App.Id %>'>
                        <a class="btn dropdown-toggle" data-toggle="dropdown"><span class="caret"></span>
                        </a>
                        <ul class="dropdown-menu">
                            <!-- dropdown menu links -->
                            <li><a href="CreateRole.aspx?id=<%=ContextHelper.ObjId %>">Create a new role</a></li>
                        </ul>
                    </uc:PlaceHolder>
                </div>
            </uc:PlaceHolder>
            <uc:PlaceHolder runat="server" Actions="CreateRolesOfApps" ExcludeActions="AppendRolesOfApps"
                App='<%#App.Id %>'>
                <a class="btn pull-right" href="AppendRoles.aspx?id=<%=ContextHelper.ObjId %>"><i
                class="icon-plus-sign"></i>
                    Create a new role</a>
            </uc:PlaceHolder>
            <uc:MgtNav runat="server" ID="ucMgtNav" App='<%#App %>' NavId="3" />
            <form>
            <fieldset>
                <legend>View roles</legend>
                <table class="table table-bordered table-hover table-striped">
                    <colgroup>
                        <col width="150px" />
                        <col width="150px" />
                        <col width="500px" />
                        <col />
                    </colgroup>
                    <thead>
                        <tr>
                            <th>
                                Name
                            </th>
                            <th>
                                Label
                            </th>
                            <th>
                                Actions
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater runat="server" ID="rpt" ItemType="BasicPlatform.Web.Models.Role">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# Item.Name%>
                                    </td>
                                    <td>
                                        <%# Item.Label%>
                                    </td>
                                    <td>
                                        <uc:PlaceHolder runat="server" Actions="EditRolesOfApps" App="<%#App.Id %>">
                                            <a href="EditRole.aspx?id=<%#App.Id %>&$=<%#Item.Id %>">EDIT</a>
                                        </uc:PlaceHolder>
                                        <uc:PlaceHolder runat="server" Actions="RemoveRolesOfApps" App='<%#App.Id %>'>
                                            <a href="<%#Item.Id %>" data-invoke="$ajax.action" data-invoke-target="apps" data-invoke-action="removerole"
                                                data-invoke-form="?id">REMOVE</a></uc:PlaceHolder>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </fieldset>
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
