<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Edit.aspx.cs" Inherits="BasicPlatform.Actions.Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="row-fluid">
        <div class="span12">
            <div class="page-header">
                <h1>
                    Actions <small>Edit an existing action</small></h1>
            </div>
            <form runat="server" id="edit" class="form-horizontal">
            <fieldset>
                <div class="alert alert-success hide">
                    Saved successful.</div>
                <div class="control-group">
                    <label class="control-label" for="name">
                        Name</label>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="name" CssClass="input-medium" placeholder="enter name of this app here" />
                        <p class="help-inline">
                        </p>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label" for="label">
                        Label</label>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="label" ReadOnly='<%# InitApp==default(ObjectId) %>' CssClass="input-medium" placeholder="enter an unique label here" />
                        <p class="help-inline">
                        </p>
                    </div>
                </div>
                <uc:PlaceHolder runat="server" Actions="ViewRoles,EditRoles,DeleteRoles,AddRoles" Visible='<%# !Action.Individual %>'>
                    <div class="control-group">
                        <label class="control-label" for="roles">
                            Roles</label>
                        <div class="controls">
                            <select id="roles" name="roles" role="menu" class="span4" multiple="multiple">
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
                                    Create a new role</a></uc:PlaceHolder>
                            <p class="help-inline">
                            </p>
                        </div>
                    </div>
                </uc:PlaceHolder>
                <div class="form-actions">
                    <uc:Button runat="server" OnServerClick="Save" class="btn btn-primary btn-large"
                        LoadingText="Saving...">
                        Save changes</uc:Button>
                    <a class="btn btn-large" href="./">Cancel</a>
                </div>
            </fieldset>
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script type="text/javascript" src="edit.js"></script>
</asp:Content>
