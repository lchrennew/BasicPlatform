<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="BasicPlatform.Roles.Default" %>

<%@ Register Src="~/UserControls/NumericPagination.ascx" TagPrefix="uc" TagName="Pagination" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="row-fluid">
        <div class="span12">
            <div class="page-header">
                <uc:PlaceHolder runat="server" Actions="AddRoles">
                    <a class="btn btn-large pull-right" href="Add.aspx"><i class="icon-plus-sign">
                    </i>
                        Create a role</a></uc:PlaceHolder>
                <h1>
                    Roles <small>Manage and grouping users into roles</small></h1>
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
                <thead>
                    <tr>
                        <th>
                            Name
                        </th>
                        <th>
                            Label
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
                                    <%#Eval("Name") %>
                                </td>
                                <td>
                                    <%#Eval("Label")%>
                                </td>
                                <td>
                                    <uc:PlaceHolder runat="server" Actions="EditRoles">
                                        <a href="edit.aspx?id=<%#Eval("Id") %>">EDIT</a></uc:PlaceHolder>
                                    <uc:PlaceHolder runat="server" Actions="ViewUsers">
                                        <a href="../users/?$=%7B&quot;r!_!in&quot;%3A&quot;<%#Eval("Id") %>&quot;%7D">VIEW USERS</a></uc:PlaceHolder>
                                    <uc:PlaceHolder runat="server" Actions="DeleteRoles">
                                        <a href="<%#Eval("Id") %>" data-invoke="$ajax.action" data-invoke-target="roles"
                                            data-invoke-action="delete" data-invoke-confirm="#delete<%#Eval("Id") %>">DELETE</a></uc:PlaceHolder>
                                </td>
                            </tr>
                            <uc:PlaceHolder runat="server" Actions="DeleteRoles">
                                <tr class="hide">
                                </tr>
                                <tr class="hide warning alert alert-block" id="delete<%#Eval("Id") %>">
                                    <td colspan="3">
                                        <h4>
                                            Warning!</h4>
                                        <p>
                                            Do you really want to delete this role? <a href="#" class="btn btn-danger" data-confirm="yes">
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
