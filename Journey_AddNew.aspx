<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Journey_AddNew.aspx.vb" Inherits="Journey_AddNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:DropDownList ID="ddlFlights" runat="server"></asp:DropDownList>
    <asp:Calendar ID="calDate" runat="server"></asp:Calendar>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:Button ID="btnAdd" runat="server" Text="Add Journey" />
</asp:Content>

