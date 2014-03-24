<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Actions.aspx.cs" Inherits="BasicPlatform.Apps.Manage.Actions" %>

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
            <uc:PlaceHolder runat="server" Actions="CreateActionsOfApps" App='<%#App.Id %>'>
                <a class="btn pull-right" href="CreateAction.aspx?id=<%=ContextHelper.ObjId %>"><i
                class="icon-plus-sign"></i>
                    Create an action</a></uc:PlaceHolder>
            <uc:MgtNav runat="server" ID="ucMgtNav" App='<%#App %>' NavId="2" />
            <form>
            <fieldset>
                <legend>View actions</legend>
                <table class="table table-bordered table-hover table-striped">
                    <colgroup>
                        <col width="200px" />
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
                                Roles
                            </th>
                            <th>
                                Actions
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater runat="server" ID="rpt">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# Eval("Name")%>
                                    </td>
                                    <td>
                                        <%# Eval("Label")%>
                                    </td>
                                    <td>
                                        <asp:Repeater runat="server" DataSource='<%#GetRoles((ObjectId)Eval("Id"))%>'>
                                            <ItemTemplate>
                                                <span class="label"><i class="icon-user icon-white"></i>
                                                    <%#Eval("Name") %></span>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                    <td>
                                        <uc:PlaceHolder runat="server" Actions="EditActionsOfApps" App='<%#App.Id %>' Visible='<%#Eval("Individual") %>'>
                                            <uc:PlaceHolder runat="server" Actions="ViewRolesOfApps" App='<%#App.Id %>'>
                                                <a href="EditAppAction.aspx?id=<%#App.Id %>&amp;$=<%#Eval("Id") %>">EDIT</a></uc:PlaceHolder>
                                        </uc:PlaceHolder>
                                        <uc:PlaceHolder runat="server" Actions="EditActionsOfApps" App='<%#App.Id %>' Visible='<%#!(bool)Eval("Individual") %>'>
                                            <a href="EditAction.aspx?id=<%#App.Id %>&amp;$=<%#Eval("Id") %>">EDIT</a></uc:PlaceHolder>
                                        <uc:PlaceHolder runat="server" Actions="RemoveActionsOfApps" App='<%#App.Id %>' Visible='<%#!(bool)Eval("Individual") %>'>
                                            <a href="<%#Eval("Id") %>" data-invoke="$ajax.action" data-invoke-target="apps" data-invoke-action="removeaction"
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
