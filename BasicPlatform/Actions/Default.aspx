<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="BasicPlatform.Actions.Default" %>

<%@ Register Src="~/UserControls/NumericPagination.ascx" TagPrefix="uc" TagName="Pagination" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="row-fluid">
        <div class="span12">
            <div class="page-header">
                <%--                <uc:PlaceHolder runat="server" Actions="AddActions">
                    <a class="btn btn-large pull-right" href="Add.aspx"><i class="icon-plus-sign"></i>
                        Create an action</a></uc:PlaceHolder>--%>
                <h1>
                    Actions <small>Manage accesses of using OPS</small></h1>
            </div>
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
                            <fieldset data-page="defaultfilter.aspx" id="filter">
                            </fieldset>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
            <hr>
            <h4>
                Results</h4>
            <table class="table table-striped table-bordered table-hover">
                <colgroup>
                    <col width="200px" />
                    <col width="200px" />
                    <uc:PlaceHolder runat="server" Actions="ViewRoles,AddRoles,EditRoles">
                        <col width="400px" />
                    </uc:PlaceHolder>
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
                        <uc:PlaceHolder runat="server" Actions="ViewRoles,AddRoles,EditRoles">
                            <th>
                                Roles
                            </th>
                        </uc:PlaceHolder>
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
                                    <%#Eval("Name") %>
                                </td>
                                <td>
                                    <%#Eval("Label")%>
                                </td>
                                <uc:PlaceHolder runat="server" Actions="ViewRoles,AddRoles,EditRoles">
                                    <td>
                                        <asp:Repeater runat="server" DataSource='<%#GetRoles((ObjectId)Eval("Id")) %>'>
                                            <ItemTemplate>
                                                <span class="label"><i class="icon-user icon-white"></i>
                                                    <%#Eval("Name") %></span>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                </uc:PlaceHolder>
                                <td>
                                    <uc:PlaceHolder runat="server" Actions="EditActions">
                                        <a href="edit.aspx?id=<%#Eval("Id") %>">EDIT</a></uc:PlaceHolder>
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
