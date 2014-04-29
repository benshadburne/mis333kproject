<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Emp_ViewReports.aspx.vb" Inherits="Emp_ViewReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <div class ="header" style="text-align: center;">
       <h1>View Reports</h1>
   </div>
    <div class="center-block" style="float:left; width: 20%">
        <asp:Button class="btn" ID="btnSearch" runat="server" Text="Search" />
        <br />
        <br />
       <asp:Label ID="Label3" runat="server" Text="Search By:" CssClass="label"></asp:Label>
       <asp:RadioButtonList ID="radRevenueSeatCount" runat="server" CssClass="radio">
           <asp:ListItem Value="0">Seats</asp:ListItem>
           <asp:ListItem Value="1">Revenue</asp:ListItem>
           <asp:ListItem Value="2" Selected="True">Both</asp:ListItem>
       </asp:RadioButtonList>
       <br />

       <asp:Label ID="Label4" runat="server" Text="Filter by:" CssClass="label"></asp:Label>
       <asp:RadioButtonList ID="radClass" runat="server" CssClass="radio">
           <asp:ListItem Value="0">Economy</asp:ListItem>
           <asp:ListItem Value="1">1st Class</asp:ListItem>
       </asp:RadioButtonList>
       <br />

       <asp:DropDownList class="dropdown" ID="ddlCityOrRoute" runat="server">
            <asp:ListItem>Cities</asp:ListItem>
           
        </asp:DropDownList>
       <br />

       
       <asp:Label ID="Label1" runat="server" Text="Lower Date:" CssClass="label"></asp:Label>
    <asp:Calendar ID="calLowerDate" runat="server" ></asp:Calendar>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Upper date:" CssClass="label"></asp:Label>

    <asp:Calendar ID="calUpperDate" runat="server"></asp:Calendar>

       </div>

    <div class ="center-block" style="float: left; width: 70%;"> 

        <asp:GridView ID="gvReports" runat="server"></asp:GridView>
    </div>
</asp:Content>

