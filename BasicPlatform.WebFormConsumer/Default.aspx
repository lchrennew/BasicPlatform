<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BasicPlatform.WebFormConsumer.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div><%=BasicPlatform.Client.PermissionHelper.Check("A") %>
        <%=User.Identity.Name %>
        <asp:LoginView runat="server">
            <RoleGroups>
                <asp:RoleGroup Roles="SysAdmin">
                    <ContentTemplate>
                        <p>
                            SysAdmin Only</p>
                    </ContentTemplate>
                </asp:RoleGroup>
            </RoleGroups>
        </asp:LoginView>
        <asp:LoginView runat="server">
            <RoleGroups>
                <asp:RoleGroup Roles="Editor">
                    <ContentTemplate>
                        <p>
                            Editor Only</p>
                    </ContentTemplate>
                </asp:RoleGroup>
            </RoleGroups>
        </asp:LoginView>
        <asp:LoginView runat="server">
            <RoleGroups>
                <asp:RoleGroup Roles="SysAdmin, Editor">
                    <ContentTemplate>
                        <p>
                            Both SysAdmin &amp; Editor</p>
                    </ContentTemplate>
                </asp:RoleGroup>
            </RoleGroups>
        </asp:LoginView>
        <p>My roles: <%= string.Join(",", Roles.GetRolesForUser()) %>
        </p>
        <a href="/bpo.aspx?logout=1">退出</a> <a href="/bpo.aspx?logout=all">全部退出</a>
    </div>
    </form>
</body>
</html>
