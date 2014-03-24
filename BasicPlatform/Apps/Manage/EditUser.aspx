<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="BasicPlatform.Apps.Manage.EditUser" %>

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
            <uc:PlaceHolder runat="server" Actions="CreateUsersOfApps" App='<%#App.Id %>'>
                <div class="btn-group pull-right">
                    <a class="btn" href="CreateUser.aspx?id=<%=ContextHelper.ObjId %>"><i class="icon-plus-sign"></i>
                        Create a user</a> <a class="btn dropdown-toggle" data-toggle="dropdown"><span class="caret"></span></a>
                    <ul class="dropdown-menu">
                        <!-- dropdown menu links -->
                        <li><a href="Users.aspx?id=<%=ContextHelper.ObjId %>">View users</a></li>
                    </ul>
                </div>
            </uc:PlaceHolder>
            <uc:PlaceHolder runat="server" Actions="AppendUsersOfApps,ViewUsersOfApps,RemoveUsersOfApps"
                ExcludeActions="CreateUsersOfApps" App='<%#App.Id %>'>
                <a href="Users.aspx?id=<%=App.Id %>" class="btn pull-right"><i class="icon-circle-arrow-left"></i>
                    View users</a>
            </uc:PlaceHolder>
            <uc:MgtNav runat="server" ID="ucMgtNav" App='<%#App %>' NavId="1" />
            <form class="form form-horizontal" runat="server" id="grant">
                <fieldset>
                    <legend>Grant access of this app to <%=Username %></legend>
                    <div class="alert alert-success hide">
                        These users have been granted to successfully.
                    </div>
                    <div class="control-group">
                        <label class="control-label">Username</label>
                        <div class="controls">
                            <input type="text" class="input-medium" value="<%=Username %>" readonly="readonly" disabled="disabled" id="username" />
                        </div>
                    </div>
                    <asp:PlaceHolder runat="server" Visible="<%# !string.IsNullOrEmpty(App.ConnectUrl) %>">
                        <uc:PlaceHolder runat="server" Actions="ConnectUsersOfApps" App='<%#App.Id %>'>
                            <div class="control-group">
                                <label class="control-label" for="alias">Alias</label>
                                <div class="controls">
                                    <asp:TextBox runat="server" ID="alias" CssClass="input-medium" placeholder="enter connection alias"></asp:TextBox>
                                    <p class="help-inline"></p>
                                </div>
                            </div>
                            <div class="control-group hide">
                                <label class="control-label" for="group">Group</label>
                                <div class="controls">
                                    <select id="group" data-app-group="<%#App.Id %>" class="input-medium" data-url="<%#App.Url %>">
                                    </select>
                                    <p class="help-inline"></p>
                                </div>
                            </div>
                        </uc:PlaceHolder>
                    </asp:PlaceHolder>
                    <uc:PlaceHolder runat="server" Actions="ViewRolesOfApps,CreateRolesOfApps,AppendRolesOfApps,RemoveRolesOfApps" App='<%#App.Id %>'>
                        <div class="control-group">
                            <label class="control-label">
                                Roles</label>
                            <div class="controls">
                                <select id="roles" name="roles" class="span4" multiple="multiple">
                                    <asp:Repeater runat="server" ID="roles">
                                        <ItemTemplate>
                                            <option value="<%#Eval("Id") %>" data-text="<%#Eval("Label") %>" <%#InitRoles.Contains((ObjectId)Eval("Id"))?"selected":"" %>>
                                                <%#Eval("Name") %></option>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </select>
                                <uc:PlaceHolder runat="server" Actions="CreateRolesOfApps" App='<%#App.Id %>'>
                                    <a href="<%=App.Id %>" class="btn vertical-bottom" role="button" data-invoke="$ajax.modal"
                                        data-invoke-target="apps" data-invoke-action="addrole"><i class="icon-plus-sign"></i>
                                        Create a new role</a>
                                </uc:PlaceHolder>
                                <p class="help-inline">
                                </p>
                            </div>
                        </div>
                    </uc:PlaceHolder>
                    <div class="form-actions">
                        <uc:Button runat="server" OnServerClick="Grant" class="btn btn-primary">
                            Grant to
                        </uc:Button>
                        <a href="Users.aspx?id=<%=App.Id %>" class="btn">Cancel</a>
                    </div>
                </fieldset>
            </form>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script type="text/ecmascript">
        var con = '<%=App.SelfConnectable ? "y" : "n"%>'
    </script>
    <script type="text/javascript" src="edituser.js"></script>
</asp:Content>
