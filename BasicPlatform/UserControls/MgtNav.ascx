<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MgtNav.ascx.cs" Inherits="BasicPlatform.UserControls.MgtNav" %>
<ul class="nav nav-tabs">
<%--    <li <%=NavId==0?"class=\"active\"":"" %>><a href="./?id=<%=App.Id %>">Summary</a></li>--%>
    <!--下拉 Users-->
    <uc:PlaceHolder runat="server" Actions="CreateUsersOfApps,AppendUsersOfApps" App='<%#App.Id %>'>
        <li <%=NavId==1?"class=\"active dropdown\"":"class=\"dropdown\"" %>><a class="dropdown-toggle" href="Users.aspx?id=<%=App.Id %>"
            data-target="muser" data-toggle="dropdown" id="duser">Users <b class="caret"></b>
        </a>
            <ul class="dropdown-menu" role="menu" aria-labelledby="duser" id="muser">
                <li><a href="Users.aspx?id=<%=App.Id %>">View users</a></li>
                <li class="divider"></li>
                <uc:PlaceHolder runat="server" Actions="AppendUsersOfApps" App='<%#App.Id %>'>
                    <li><a href="AppendUsers.aspx?id=<%=App.Id %>">Add users</a></li>
                </uc:PlaceHolder>
                <uc:PlaceHolder runat="server" Actions="CreateUsersOfApps" App='<%#App.Id %>'>
                    <li><a href="CreateUser.aspx?id=<%=App.Id %>">Create new user</a></li>
                </uc:PlaceHolder>
            </ul>
        </li>
    </uc:PlaceHolder>
    <!--单页 Users-->
    <uc:PlaceHolder runat="server" Actions="RemoveUsersOfApps" App='<%#App.Id %>'
        ExcludeActions="CreateUsersOfApps,AppendUsersOfApps">
        <li <%=NavId==1?"class=\"active\"":"" %>><a href="Users.aspx?id=<%=App.Id %>">Users</a></li>
    </uc:PlaceHolder>
    <!--下拉 Actions-->
    <uc:PlaceHolder runat="server" Actions="CreateActionsOfApps" App='<%#App.Id %>'>
        <li <%=NavId==2?"class=\"active dropdown\"":"class=\"dropdown\"" %>><a class="dropdown-toggle" href="Actions.aspx?id=<%=App.Id %>"
            data-toggle="dropdown">Actions <b class="caret"></b></a>
            <ul class="dropdown-menu" role="menu">
                <li><a href="Actions.aspx?id=<%=App.Id %>">View actions</a></li>
                <li class="divider"></li>
                <li><a href="CreateAction.aspx?id=<%=App.Id %>">Create an action</a></li>
            </ul>
        </li>
    </uc:PlaceHolder>
    <!--单页 Actions-->
    <uc:PlaceHolder runat="server" Actions="EditActionsOfApps,RemoveActionsOfApps"
        ExcludeActions="CreateActionsOfApps" App='<%#App.Id %>'>
        <li <%=NavId==2?"class=\"active\"":"" %>><a href="Actions.aspx?id=<%=App.Id %>">Actions</a></li>
    </uc:PlaceHolder>
    <!--下拉 Roles-->
    <uc:PlaceHolder runat="server" Actions="CreateRolesOfApps,AppendRolesOfApps" App='<%#App.Id %>'>
        <li <%=NavId==3?"class=\"active dropdown\"":"class=\"dropdown\"" %>><a href="Roles.aspx?id=<%=App.Id %>" class="dropdown-toggle"
            data-toggle="dropdown">Roles <b class="caret"></b></a>
            <ul class="dropdown-menu" role="menu">
                <li><a href="Roles.aspx?id=<%=App.Id %>">View roles</a></li>
                <li class="divider"></li>
                <uc:PlaceHolder runat="server" Actions="CreateRolesOfApps" App='<%#App.Id %>'>
                    <li><a href="CreateRole.aspx?id=<%=App.Id %>">Create a role</a></li>
                </uc:PlaceHolder>
                <uc:PlaceHolder runat="server" Actions="AppendRolesOfApps" App='<%#App.Id %>'>
                    <li><a href="AppendRoles.aspx?id=<%=App.Id %>">Add roles</a></li>
                </uc:PlaceHolder>
            </ul>
        </li>
    </uc:PlaceHolder>
    <!--单页 Roles-->
    <uc:PlaceHolder runat="server" Actions="RemoveRolesOfApps" ExcludeActions="CreateRolesOfApps,AppendRolesOfApps"
        App='<%#App.Id %>'>
        <li <%=NavId==3?"class=\"active\"":"" %>><a href="Roles.aspx?id=<%=App.Id %>">Roles</a></li>
    </uc:PlaceHolder>
</ul>
