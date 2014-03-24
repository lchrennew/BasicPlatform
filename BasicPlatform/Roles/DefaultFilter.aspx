<%@ Page Title="" Language="C#" MasterPageFile="~/Filter.Master" AutoEventWireup="true"
    CodeBehind="DefaultFilter.aspx.cs" Inherits="BasicPlatform.Roles.DefaultFilter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="filter" runat="server">
    <div class="control-group">
        <label class="control-label" for="f.n">
            Name
        </label>
        <div class="controls">
            <input type="text" id="f.n" class="input-medium" data-field="n" data-type="s" data-op="regex"
                placeholder="Name" /></div>
    </div>
    <div class="control-group">
        <label class="control-label" for="f.l">
            Label
        </label>
        <div class="controls">
            <input type="text" id="f.l" class="input-medium" data-field="l" data-type="s" data-op="regex"
                placeholder="Label" /></div>
    </div>
    <div class="form-actions">
        <button class="btn btn-primary" type="button" id="filter-btn">
            <i class="icon-search icon-white"></i>  Search</button>
        <a class="btn" href="./">Reset Filter</a></div>
</asp:Content>
