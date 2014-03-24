<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="EditAppAction.aspx.cs" Inherits="BasicPlatform.Apps.Manage.EditAppAction" %>

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
                <a class="btn pull-right" href="Actions.aspx?id=<%=ContextHelper.ObjId %>"><i class="icon-circle-arrow-left">
                </i>
                    View actions</a></uc:PlaceHolder>
            <uc:MgtNav runat="server" ID="ucMgtNav" App='<%#App %>' NavId="2" />
            <form runat="server" id="grant" class="form-horizontal">
            <fieldset>
                <legend>Edit action <small>
                    <%=name.Text%></small></legend>
                <p class="alert alert-success hide">
                    Success!
                </p>
                <div class="control-group">
                    <label class="control-label">
                        Name</label>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="name" placeholder="enter name here" ReadOnly="true" />
                        <p class="help-inline">
                        </p>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">
                        Label</label>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="label" placeholder="enter an unique label" ReadOnly="true" />
                        <p class="help-inline">
                        </p>
                    </div>
                </div>
                <uc:PlaceHolder runat="server" Actions="ViewRolesOfApps,CreateRolesOfApps,AppendRolesOfApps,RemoveRolesOfApps" App='<%#App.Id %>'>
                    <div class="control-group">
                        <label class="control-label">
                            Roles</label>
                        <div class="controls">
                            <select id="roles" name="roles" class="span4" multiple="multiple">
                                <asp:Repeater runat="server" ID="roles">
                                    <ItemTemplate>
                                        <option value="<%#Eval("Id") %>" data-text="<%#Eval("Label") %>" <%#InitRoles.Contains((ObjectId)Eval("Id"))?" selected":"" %>>
                                            <%#Eval("Name") %></option>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </select>
                            <uc:PlaceHolder runat="server" Actions="CreateRolesOfApps" App='<%#App.Id %>'>
                                <a href="<%=App.Id %>" class="btn vertical-bottom" role="button" data-invoke="$ajax.modal"
                                    data-invoke-target="apps" data-invoke-action="addrole"><i class="icon-plus-sign">
                                    </i>
                                    Create a new role</a></uc:PlaceHolder>
                            <p class="help-inline">
                            </p>
                        </div>
                    </div>
                </uc:PlaceHolder>
                <div class="form-actions">
                    <uc:Button runat="server" class="btn btn-primary" OnServerClick="Save">
                        Save</uc:Button>
                    <a href="Actions.aspx?id=<%=App.Id %>" class="btn">Cancel</a>
                </div>
            </fieldset>
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script src="editaction.js" type="text/javascript"></script>
</asp:Content>
