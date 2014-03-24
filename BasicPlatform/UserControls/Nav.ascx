<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Nav.ascx.cs" Inherits="BasicPlatform.UserControls.Nav" %>
<div class="navbar navbar-fixed-top">
    <div class="navbar-inner">
        <div class="container-fluid">
            <button type="button" class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse">
                <span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar">
                </span>
            </button>
            <a class="brand" href="/">Ops Gateway Platform</a>
            <div class="nav-collapse collapse">
                <ul class="nav" role="navigation">
                    <li <%=ConfigurationManager.AppSettings["nav"]==null?" class=\"active\"":"" %>><a
                        href="/">Dashboard</a></li>
                    <uc:PlaceHolder runat="server" Actions="ViewUsers,ViewRoles,AddUsers,EditUsers,AddRoles,EditRoles">
                        <li class="dropdown<%=ConfigurationManager.AppSettings["nav"]=="1"?" active":"" %>">
                            <a href="/users/" class="dropdown-toggle" data-toggle="dropdown" id="staffmenu" role="button">
                                Staffs <b class="caret"></b></a>
                            <ul class="dropdown-menu" role="menu" aria-labelledby="staffmenu">
                                <uc:PlaceHolder runat="server" Actions="ViewUsers,AddUsers,EditUsers">
                                    <li role="presentation"><a href="/users/" role="menuitem">Users</a></li></uc:PlaceHolder>
                                <uc:PlaceHolder runat="server" Actions="ViewRoles,AddRoles,EditRoles">
                                    <li role="presentation"><a href="/roles/" role="menuitem">Roles</a> </li>
                                </uc:PlaceHolder>
                            </ul>
                        </li>
                    </uc:PlaceHolder>
                    <uc:PlaceHolder runat="server" Actions="ViewApps,ViewActions,AddApps,EditApps,EditActions">
                        <li class="dropdown<%=ConfigurationManager.AppSettings["nav"]=="2"?" active":"" %>">
                            <a href="/roles/" class="dropdown-toggle" data-toggle="dropdown" id="appmenu" role="button">
                                Facilities <b class="caret"></b></a>
                            <ul class="dropdown-menu" role="menu" aria-labelledby="appmenu">
                                <uc:PlaceHolder runat="server" Actions="ViewApps,AddApps,EditApps">
                                    <li role="presentation"><a href="/apps/" role="menuitem">Apps</a></li></uc:PlaceHolder>
                                <uc:PlaceHolder runat="server" Actions="ViewActions,EditActions">
                                    <li role="presentation"><a href="/actions/" role="menuitem">Actions</a></li></uc:PlaceHolder>
                            </ul>
                        </li>
                    </uc:PlaceHolder>
                </ul>
                <ul class="nav pull-right">
                    <li class="dropdown<%=ConfigurationManager.AppSettings["nav"]=="0"?" active":"" %>">
                        <a id="usermenu" href="#" class="dropdown-toggle" data-toggle="dropdown" role="button">
                            <i class="icon-user"></i>
                            <%=ContextHelper.User.Label %>
                            <b class="caret"></b></a>
                        <ul class="dropdown-menu" role="menu" aria-labelledby="usermenu">
                            <li class="dropdown-submenu hide" role="presentation"><a href="#">Languages</a>
                                <ul class="dropdown-menu">
                                    <li role="presentation"><a href="#"><i class="icon-ok"></i>
                                        English United State</a></li>
                                    <li role="presentation"><a href="#"><i class="icon-no"></i>
                                        Chinese Simplified</a></li>
                                </ul>
                            </li>
                            <li class="divider hide"></li>
                            <li role="presentation"><a href="/profile/edit.aspx">Profile</a></li>
                            <li role="presentation"><a href="/profile/connections.aspx">Connections</a></li>
                            <li class="divider"></li>
                            <li role="presentation"><a href="/logout.aspx">Sign Out</a></li>
                        </ul>
                    </li>
                    <li></li>
                </ul>
            </div>
            <!--/.nav-collapse -->
        </div>
    </div>
</div>
