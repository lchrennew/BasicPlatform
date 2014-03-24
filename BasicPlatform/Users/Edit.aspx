<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Edit.aspx.cs" Inherits="BasicPlatform.Users.Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="page-header">
        <h1>Users <small>Edit an existing account</small></h1>
    </div>
    <div class="row-fluid">
        <form runat="server" id="edit" class="form-horizontal">
            <fieldset>
                <div class="alert alert-success hide">
                    Saved successful.
                </div>
                <div class="control-group">
                    <label class="control-label">
                        Username</label>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="username" CssClass="input-medium" placeholder="enter username here"
                            ReadOnly="true" />
                        <p class="help-inline">
                        </p>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label" for="label">
                        Full Name</label>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="label" CssClass="input-medium" placeholder="enter full name here" />
                        <p class="help-inline">
                        </p>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label" for="email">
                        E-mail</label>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="email" CssClass="input-medium" placeholder="enter e-mail here" />
                        <p class="help-inline">
                        </p>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label" for="password">
                        Password</label>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="password" CssClass="input-medium" placeholder="enter password here"
                            TextMode="Password" />
                        <p class="help-inline">
                        </p>
                    </div>
                </div>
                <uc:PlaceHolder runat="server" Actions="ViewRoles,AddRoles,EditRoles,DeleteRoles">
                    <div class="control-group">
                        <label class="control-label" for="roles">
                            Roles</label>
                        <div class="controls">
                            <select multiple="multiple" name="roles" id="roles" class="input-xlarge" role="listbox"
                                size="4">
                                <asp:Repeater runat="server" ID="roles">
                                    <ItemTemplate>
                                        <option value="<%#Eval("Id") %>" data-text="<%#Eval("Label") %>" <%#InitRoles.Contains((ObjectId)Eval("Id"))?" selected":"" %>>
                                            <%#Eval("Name") %></option>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </select>
                            <uc:PlaceHolder runat="server" Actions="AddRoles">
                                <a href="#" class="btn vertical-bottom" data-invoke="$ajax.modal" data-invoke-target="roles"
                                    data-invoke-action="add"><i class="icon-plus-sign"></i>
                                    Create a new role</a>
                            </uc:PlaceHolder>
                            <p class="help-inline">
                            </p>
                        </div>
                    </div>
                </uc:PlaceHolder>
                <uc:PlaceHolder runat="server" Actions="ViewApps,AddApps,EditApps,DeleteApps">
                    <div class="control-group">
                        <label class="control-label" for="apps">
                            Apps</label>
                        <div class="controls">
                            <div class="span12">
                                <table class="table table-striped table-hover table-condensed table-bordered" id="apps">
                                    <colgroup>
                                        <col style="width: 200px;" />
                                        <col style="width: 160px;" />
                                        <col style="width: 175px;" />
                                        <col />
                                    </colgroup>
                                    <thead>
                                        <tr>
                                            <th>App</th>
                                            <th>Group</th>
                                            <th>Alias</th>
                                            <th>Permissions</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater runat="server" ID="apps" ItemType="BasicPlatform.Web.Models.App">
                                            <ItemTemplate>
                                                <tr data-app="<%#Item.Id%>" data-url="<%#Item.Url %>" data-alias="<%#GetAlias(Item.Id) %>">
                                                    <td>
                                                        <label class="checkbox inline">
                                                            <input type="checkbox" name="<%=apps.ID %>" value="<%#Item.Id%>" <%#InitApps.Contains(Item.Id)?"checked":"" %> /><%#HttpUtility.HtmlEncode(Item.Name)%></label></td>
                                                    <td>
                                                        <asp:PlaceHolder runat="server" Visible="<%# !string.IsNullOrEmpty(Item.ConnectUrl) %>">
                                                            <select data-control="group" class="input-medium" disabled="disabled">
                                                            </select></asp:PlaceHolder>
                                                    </td>
                                                    <td>
                                                        <asp:PlaceHolder runat="server" Visible="<%# !string.IsNullOrEmpty(Item.ConnectUrl) %>">
                                                            <input type="text" name="alias" data-control="alias" class="input-medium" disabled="disabled" value="<%#HttpUtility.HtmlAttributeEncode(GetAlias(Item.Id)) %>" /></asp:PlaceHolder>
                                                    </td>
                                                    <td>
                                                        <input type="text" name="roles<%#Item.Id%>" class="span12" disabled="disabled" data-control="roles" value="<%#GetAppRoleIds(Item.Id) %>" data-names="<%#GetAppRoleNames(Item.Id) %>" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                    <uc:PlaceHolder runat="server" Actions="AddApps">
                                        <tfoot>
                                            <tr>
                                                <td colspan="4">

                                                    <a href="#" class="btn vertical-bottom pull-right" data-invoke="$ajax.modal" data-invoke-target="apps" data-invoke-action="add">
                                                        <i class="icon-plus-sign"></i>
                                                        Create a new app</a>

                                                </td>
                                            </tr>
                                        </tfoot>
                                    </uc:PlaceHolder>
                                </table>
                            </div>
                            <p class="help-block">
                            </p>
                        </div>
                    </div>
                </uc:PlaceHolder>
                <div class="form-actions">
                    <uc:Button runat="server" OnServerClick="Save" class="btn btn-primary btn-large"
                        LoadingText="Saving...">
                        Save changes
                    </uc:Button>
                    <a class="btn btn-large" href="./">Cancel</a>
                </div>
            </fieldset>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script type="text/javascript" src="edit.js"></script>
</asp:Content>
