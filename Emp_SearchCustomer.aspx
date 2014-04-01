<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Emp_SearchCustomer.aspx.vb" Inherits="Emp_SearchCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="header">
       Search Customer
   </div>
    <asp:Label ID="Label1" runat="server" padding="3px" Text="Select a Filter"></asp:Label>
    <asp:DropDownList ID="DropDownList1" runat="server" padding ="3px"></asp:DropDownList>
    <asp:TextBox ID="txtFilter" runat="server"></asp:TextBox>
    <br />
    <asp:GridView ID="gvCustomers" runat="server">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
    </asp:GridView>


</asp:Content>

