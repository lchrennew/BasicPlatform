<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="BasicPlatform.Users.Default" %>

<%@ Register Src="~/UserControls/NumericPagination.ascx" TagPrefix="uc" TagName="Pagination" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="page-header">
        <uc:PlaceHolder runat="server" Actions="AddUsers">
            <a class="btn btn-large pull-right" href="Add.aspx"><i class="icon-plus-sign">
            </i>
                Create an account</a></uc:PlaceHolder>
        <h1>
            Users <small>Manage accounts of this platform</small></h1>
    </div>
    <div class="row-fluid">
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
                <col width="200px" />
                <col />
            </colgroup>
            <thead>
                <tr>
                    <th>
                        Full Name
                    </th>
                    <th>
                        Username
                    </th>
                    <th>
                        E-mail
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
                                <%#Eval("Label")%>
                            </td>
                            <td>
                                <%#Eval("Username") %>
                            </td>
                            <td>
                                <%#Eval("Email") %>
                            </td>
                            <td>
                                <uc:PlaceHolder runat="server" Actions="EditUsers">
                                    <a href="edit.aspx?id=<%#Eval("Id") %>">EDIT</a></uc:PlaceHolder>
                                <uc:PlaceHolder runat="server" Actions="DeleteUsers" Visible='<%#Eval("Username") as string!="admin" %>'>
                                    <a href="<%#Eval("Id") %>" data-invoke="$ajax.action" data-invoke-target="users"
                                        data-invoke-action="delete" data-invoke-confirm="#delete<%#Eval("Id") %>">DELETE</a></uc:PlaceHolder>
                            </td>
                        </tr>
                        <uc:PlaceHolder runat="server" Actions="DeleteUsers" Visible='<%#Eval("Username") as string!="admin" %>'>
                            <tr class="hide">
                            </tr>
                            <tr class="hide warning alert alert-block" id="delete<%#Eval("Id") %>">
                                <td colspan="4">
                                    <h4>
                                        Warning!</h4>
                                    <p>
                                        Do you really want to delete this user? <a href="#" class="btn btn-danger" data-confirm="yes">
                                            <i class="icon-trash icon-white"></i>
                                            Yes</a> <a href="#" class="btn" data-confirm="no">No</a></p>
                                </td>
                            </tr>
                        </uc:PlaceHolder>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
        <uc:Pagination runat="server" ID="pager" PageSize="20" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
