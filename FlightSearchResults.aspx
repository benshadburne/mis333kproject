<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="FlightSearchResults.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    

    <br />
    <div class="pull-left">
    <asp:CheckBoxList class="checkbox" ID="cblDays" runat="server" Width="107px">
        <asp:ListItem>Monday</asp:ListItem>
        <asp:ListItem>Tuesday</asp:ListItem>
        <asp:ListItem>Wednesday</asp:ListItem>
        <asp:ListItem>Thursday</asp:ListItem>
        <asp:ListItem>Friday</asp:ListItem>
        <asp:ListItem>Saturday</asp:ListItem>
        <asp:ListItem>Sunday</asp:ListItem>
    </asp:CheckBoxList>
        </div>

    <div class="pull-left">
        <asp:DropDownList class="dropdown" ID="DropDownList1" runat="server"></asp:DropDownList></div>

    <div class="pull-left">
    <asp:Button class="btn" ID="btnSearch" runat="server" Text="Search" />
    </div>

</asp:Content>

