<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="BasicPlatform.Apps.Default" %>

<%@ Register Src="~/UserControls/NumericPagination.ascx" TagPrefix="uc" TagName="Pagination" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="row-fluid">
        <div class="span12">
            <div class="page-header">
                <uc:PlaceHolder runat="server" Actions="AddApps">
                    <a class="btn btn-large pull-right" href="Add.aspx"><i class="icon-plus-sign"></i>
                        Create a new app</a>
                </uc:PlaceHolder>
                <h1>Apps <small>Manage all sub/3rd-party systems</small></h1>
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
        </div>
        <div class="row-fluid">
            <h4>Results</h4>
            <table class="table table-striped table-bordered table-hover">
                <colgroup>
                    <col width="200px" />
                    <uc:PlaceHolder runat="server" Actions="EditApps,AddApps">
                        <col width="200px" />
                        <col width="200px" />
                    </uc:PlaceHolder>
                    <col />
                </colgroup>
                <thead>
                    <tr>
                        <th>Name
                        </th>
                        <uc:PlaceHolder runat="server" Actions="EditApps,AddApps">
                            <th>Label
                            </th>
                            <th>Key
                            </th>
                        </uc:PlaceHolder>
                        <th>Actions
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rpt">
                        <ItemTemplate>
                            <uc:PlaceHolder runat="server" Actions="EditApps,ViewUsersOfApps,DeleteApps,EditActions"
                                App='<%#Eval("Id") %>'>
                                <tr>
                                    <td>
                                        <%#Eval("Name")%>
                                    </td>
                                    <uc:PlaceHolder runat="server" Actions="EditApps,AddApps">
                                        <td>
                                            <%#Eval("Label")%>
                                        </td>
                                        <td>
                                            <%#Eval("Id") %>
                                        </td>
                                    </uc:PlaceHolder>
                                    <td>
                                        <uc:PlaceHolder runat="server" Actions="EditApps" App='<%#Eval("Id") %>'>
                                            <a href="edit.aspx?id=<%#Eval("Id") %>">EDIT</a>
                                        </uc:PlaceHolder>
                                        <uc:PlaceHolder runat="server" Actions="ViewUsersOfApps" App='<%#Eval("Id") %>'>
                                            <a href="Manage/?id=<%#Eval("Id") %>">MANAGE</a>
                                        </uc:PlaceHolder>
                                        <uc:PlaceHolder runat="server" Actions="DeleteApps" App='<%#Eval("Id") %>'>
                                            <a href="<%#Eval("Id") %>" data-invoke="$ajax.action" data-invoke-target="apps" data-invoke-action="delete"
                                                data-invoke-confirm="#delete<%#Eval("Id") %>">DELETE</a>
                                        </uc:PlaceHolder>
                                    </td>
                                </tr>
                                <uc:PlaceHolder runat="server" Actions="DeleteApps" App='<%#Eval("Id") %>'>
                                    <tr class="hide">
                                    </tr>
                                    <tr class="hide warning alert alert-block" id="delete<%#Eval("Id") %>">
                                        <td colspan="4">
                                            <h4>Warning!</h4>
                                            <p>
                                                Do you really want to delete this app? <a href="#" class="btn btn-danger" data-confirm="yes">
                                                    <i class="icon-trash icon-white"></i>
                                                    Yes</a> <a href="#" class="btn" data-confirm="no">No</a>
                                            </p>
                                        </td>
                                    </tr>
                                </uc:PlaceHolder>
                            </uc:PlaceHolder>
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
