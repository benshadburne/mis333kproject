<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Cust_SelectSeats.aspx.vb" Inherits="Cust_SelectSeats" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="CustomersStyleSheet.css" rel="stylesheet" />
    <div id="seats">
        <asp:Button ID="btnSeat1" runat="server" Text="Seat1" />
        <asp:Button ID="Button1" runat="server" Text="Seat2" />
        <asp:Button ID="Button2" runat="server" Text="Seat3" />
        <asp:Button ID="Button3" runat="server" Text="Seat4" />
        <asp:Button ID="Button4" runat="server" Text="Seat5" />
        <asp:Button ID="Button5" runat="server" Text="Seat6" />
        <asp:Button ID="Button6" runat="server" Text="Seat7" />
        <asp:Button ID="Button7" runat="server" Text="Seat8" />
    </div>
</asp:Content>

