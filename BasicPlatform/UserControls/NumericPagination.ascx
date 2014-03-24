<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NumericPagination.ascx.cs" Inherits="BasicPlatform.UserControls.NumericPagination" %>
<span class="label pull-right">第<%=CurrentPage %>页，共<%=TotalPages %>页（共<%=TotalRecords %>条数据）</span>
<div class="pagination pagination-left">
    <ul>
        <asp:Repeater runat="server" ID="rpt">
            <ItemTemplate>
                <asp:PlaceHolder ID="PlaceHolder1" runat="server" Visible='<%#Eval("Active") %>'>
                    <li class="active"><a href="#" rel="tooltip" title="<%#Eval("Tip") %>">
                        <%#Eval("Text") %></a></li></asp:PlaceHolder>
                <asp:PlaceHolder ID="PlaceHolder2" runat="server" Visible='<%#Eval("Disabled") %>'>
                    <li class="disabled"><a href="#" rel="tooltip" title="<%#Eval("Tip") %>">
                        <%#Eval("Text") %></a></li></asp:PlaceHolder>
                <asp:PlaceHolder ID="PlaceHolder3" runat="server" Visible='<%#!((bool)Eval("Disabled") || (bool)Eval("Active")) %>'>
                    <li><a href="<%#Eval("Link") %>" rel="tooltip" title="<%#Eval("Tip") %>">
                        <%#Eval("Text") %></a></li></asp:PlaceHolder>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
