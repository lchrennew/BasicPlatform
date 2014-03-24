<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Add.aspx.cs" Inherits="BasicPlatform.Apps.Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="row-fluid">
        <div class="span12">
            <div class="page-header">
                <h1>
                    Apps <small>Create a new app</small></h1>
            </div>
            <form runat="server" id="add" class="form-horizontal">
            <fieldset>
                <div class="alert alert-success hide">
                    Created successful.</div>
                <div class="control-group">
                    <label class="control-label" for="name">
                        Name</label>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="name" CssClass="input-medium" placeholder="enter username here" />
                        <p class="help-inline">
                        </p>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label" for="label">
                        Label</label>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="label" CssClass="input-medium" placeholder="enter full name here" />
                        <p class="help-inline">
                        </p>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label" for="secret">
                        Secret Key</label>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="secret" CssClass="input-medium" placeholder="enter secret key here" />
                        <p class="help-inline">
                        </p>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label" for="url">
                        Url</label>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="url" CssClass="input-xlarge" placeholder="enter url here" />
                        <p class="help-inline">
                        </p>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label" for="connectUrl">
                        Connect Url</label>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="connectUrl" CssClass="input-xlarge" placeholder="enter connect url here" />
                        <p class="help-inline">
                        </p>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label" for="selfConnectable">Self Connectable</label>
                    <div class="controls">
                        <asp:DropDownList runat="server" ID="selfConnectable" CssClass="input-mini">
                                <asp:ListItem Text="On" Value="1" />
                                <asp:ListItem Text="Off" Value="0" />
                            </asp:DropDownList>
                        <p class="help-inline">
                        </p>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label" for="accessable">Accessable</label>
                    <div class="controls">
                        <asp:DropDownList runat="server" ID="accessable" CssClass="input-mini">
                                <asp:ListItem Text="On" Value="1" />
                                <asp:ListItem Text="Off" Value="0" />
                            </asp:DropDownList>
                        <p class="help-inline">
                        </p>
                    </div>
                </div>
                <div class="form-actions">
                    <uc:Button runat="server" OnServerClick="Save" class="btn btn-primary btn-large"
                        LoadingText="Creating app...">
                        Create this app</uc:Button>
                    <a class="btn btn-large" href="./">Cancel</a>
                </div>
            </fieldset>
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
    <script type="text/javascript" src="add.js"></script>
</asp:Content>
