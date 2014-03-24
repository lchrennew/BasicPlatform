<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Connections.aspx.cs" Inherits="BasicPlatform.Profile.Connections" %>

<%@ Register Src="~/UserControls/NumericPagination.ascx" TagPrefix="uc" TagName="Pagination" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="row-fluid">
        <div class="span12">
            <div class="page-header">
                <h1>
                    Connections <small>View links connecting my account to accounts of apps</small></h1>
            </div>
            <div class="row-fluid">
            <div class="span12">
                <p class="alert alert-block hide" id="disconnect">
                    <span class="label label-warning">Warning!</span> Do you really want to disconnect
                    this connection? <a href="#" class="btn btn-danger" data-confirm="yes">Yes</a> <a
                        href="#" class="btn" data-confirm="no">No</a>
                </p>
            </div></div>
            <table class="table table-striped table-bordered table-hover">
                <thead>
                    <tr>
                        <th>
                            App Name
                        </th>
                        <th>
                            Account
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
                                    <%# GetAppName((ObjectId)Eval("App"))%>
                                </td>
                                <td>
                                    <%#Eval("Alias") %>
                                </td>
                                <td>
                                    <a href="<%#Eval("Id") %>" data-invoke="$ajax.action" data-invoke-target="connections"
                                        data-invoke-action="disconnect" data-invoke-confirm="#disconnect">DISCONNECT</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <uc:Pagination runat="server" ID="pager" PageSize="20" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
