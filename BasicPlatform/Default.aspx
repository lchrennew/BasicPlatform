<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="BasicPlatform.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="page-header">
        <h1>
            Dashboard <small>this is your workspace home page</small>
        </h1>
    </div>
    <div class="row">
        <div class="span9">
            <h2>
                My apps</h2>
            <ul class="thumbnails">
                <asp:Repeater ID="rpt" runat="server">
                    <ItemTemplate>
                        <li class="span9">
                            <div class="thumbnail">
                                <div class="caption">
                                    <div class="text-right pull-right" style="padding-top: 5px;">
                                        <uc:PlaceHolder runat="server" Actions="ViewRolesOfApps,CreateRolesOfApps,AppendRolesOfApps,RemoveRolesOfApps,ViewUsersOfApps,CreateUsersOfApps,AppendUsersOfApps,EditUsersOfApps,RemoveUsersOfApps,ViewActionsOfApps,CreateActionsOfApps,EditActionsOfApps,RemoveActionsOfApps" App='<%#Eval("Id") %>'>
                                            <a href="/Apps/Manage/?id=<%#Eval("Id") %>" class="btn">Manage</a>
                                        </uc:PlaceHolder>
                                        <uc:PlaceHolder runat="server" Actions="EditApps" App='<%#Eval("Id") %>'>
                                            <a href="/Apps/Edit.aspx?id=<%#Eval("Id") %>" class="btn">Edit</a>
                                        </uc:PlaceHolder>
                                        <a href="<%#Eval("Url") %>" class="btn btn-primary">Start <i class="icon-chevron-right icon-white">
                                        </i>
                                        </a>
                                    </div>
                                    <h4>
                                        <%#Eval("Name") %>
                                        <uc:PlaceHolder runat="server" Actions="EditApps" App='<%#Eval("Id") %>'>
                                                        <span class="label"><i class="icon-tag icon-white"></i>
                                                            <%#Eval("Label") %></span> <span class="label"><i class="icon-flag icon-white"></i>
                                                                <%#Eval("Id") %></span>
                                        </uc:PlaceHolder>
                                    </h4>
                                </div>
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <div class="span3">
            <h2>
                My roles</h2>
            <p>
                <span class="label"><i class="icon-user icon-white"></i> Staff</span>
                <asp:Repeater runat="server" ID="roles">
                    <ItemTemplate>
                        <span class="label"><i class="icon-user icon-white"></i>
                            <%#Eval("Name") %></span>
                    </ItemTemplate>
                </asp:Repeater>
            </p>
            <hr />
            <h2>
                World Time
            </h2>
            <p>
                <i class="icon-time"></i>
                UTC:
                <%=DateTime.UtcNow %>
            </p>
            <p>
                <i class="icon-time"></i>
                PEK:
                <%=DateTime.Now %>
            </p>
            <p>
                <i class="icon-time"></i>
                BKK:
                <%=DateTime.UtcNow.AddHours(7) %>
            </p>
            <p>
                <i class="icon-time"></i>
                NYC:
                <%=DateTime.UtcNow.AddHours(-4) %>
            </p>
            <p>
                <i class="icon-time"></i>
                SAN:
                <%=DateTime.UtcNow.AddHours(-7) %>
            </p>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
