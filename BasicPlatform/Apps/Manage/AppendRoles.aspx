<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="AppendRoles.aspx.cs" Inherits="BasicPlatform.Apps.Manage.AppendRoles" %>
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
            <uc:PlaceHolder runat="server" Actions="CreateRolesOfApps" App='<%#App.Id %>'>
                <div class="btn-group pull-right">
                    <a class="btn" href="CreateRole.aspx?id=<%=ContextHelper.ObjId %>"><i class="icon-plus-sign">
                    </i>
                        Create a role</a><a class="btn dropdown-toggle" data-toggle="dropdown"><span class="caret">
                        </span></a>
                    <ul class="dropdown-menu">
                        <!-- dropdown menu links -->
                        <li><a href="Roles.aspx?id=<%=ContextHelper.ObjId %>">View roles</a></li>
                    </ul>
                </div>
            </uc:PlaceHolder>
            <uc:PlaceHolder runat="server" Actions="AppendRolesOfApps,ViewRolesOfApps,RemoveRolesOfApps"
                ExcludeActions="CreateRolesOfApps" App='<%#App.Id %>'>
                <a href="Roles.aspx?id=<%=App.Id %>" class="btn pull-right"><i class="icon-circle-arrow-left">
                </i>
                    View roles</a>
            </uc:PlaceHolder>
                  <uc:MgtNav runat="server" ID="ucMgtNav" App='<%#App %>' NavId="3" />

            <form runat="server" id="grant" class="form-horizontal">
            <fieldset>
                <legend>Grant access of this app to existing roles </legend>
                <p class="alert alert-success hide">
                    Success!
                </p>
                <div class="control-group">
                    <label class="control-label">
                        Roles</label>
                    <div class="controls">
                        <select id="roles" name="roles" class="span4" multiple="multiple">
                            <asp:Repeater runat="server" ID="roles">
                                <ItemTemplate>
                                    <option value="<%#Eval("Id") %>" data-text="<%#Eval("Label") %>">
                                        <%#Eval("Name") %></option>
                                </ItemTemplate>
                            </asp:Repeater>
                        </select>
                        <uc:PlaceHolder runat="server" Actions="CreateRolesOfApps" App='<%#App.Id %>'>
                            <a href="<%=App.Id %>" class="btn vertical-bottom" role="button" data-invoke="$ajax.modal"
                                data-invoke-target="apps" data-invoke-action="addrole"><i class="icon-plus-sign">
                                </i>
                                Create a new role</a>
                        </uc:PlaceHolder>
                        <p class="help-inline">
                        </p>
                    </div>
                </div>
                <div class="form-actions">
                    <uc:Button runat="server" OnServerClick="Grant" class="btn btn-primary">
                        Grant to</uc:Button>
                    <a href="Roles.aspx?id=<%=App.Id %>" class="btn">Cancel</a>
                </div>
            </fieldset>
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script src="appendroles.js" type="text/javascript"></script>
</asp:Content>
