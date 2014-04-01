<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Emp_ViewReports.aspx.vb" Inherits="Emp_ViewReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <div id="header">
       View Reports
   </div>
    <div id="middle"> 
       <asp:Label ID="Label3" runat="server" Text="Search By:" CssClass="label"></asp:Label>
       <asp:RadioButtonList ID="RadioButtonList1" runat="server" CssClass="radio">
           <asp:ListItem Selected="True" Value="0">Seats</asp:ListItem>
           <asp:ListItem Value="1">Revenue</asp:ListItem>
           <asp:ListItem Value="2">Both</asp:ListItem>
       </asp:RadioButtonList>
       <br />

       <asp:Label ID="Label4" runat="server" Text="Filter by:" CssClass="label"></asp:Label>
       <asp:RadioButtonList ID="RadioButtonList2" runat="server" CssClass="radio">
           <asp:ListItem Value="0">Economy</asp:ListItem>
           <asp:ListItem Value="1">1st Class</asp:ListItem>
       </asp:RadioButtonList>
       <br />

       <asp:DropDownList class="dropdown" ID="ddlCityOrRoute" runat="server">
            <asp:ListItem>Cities</asp:ListItem>
           
        </asp:DropDownList>
       <br />

       
       <asp:Label ID="Label1" runat="server" Text="Start date:" CssClass="label"></asp:Label>
    <asp:Calendar ID="calStart" runat="server" ></asp:Calendar>
    <br />
    <asp:Label ID="Label2" runat="server" Text="End date:" CssClass="label"></asp:Label>

    <asp:Calendar ID="calEnd" runat="server"></asp:Calendar>

       </div>

    <div id ="right"> 

        <asp:GridView ID="gvReports" runat="server"></asp:GridView>
    </div>
</asp:Content>

