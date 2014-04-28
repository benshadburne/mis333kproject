<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Res_CreateTicketAndPay.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="header">
        Pay for your tickets
    </div>

    <div id="gridview">
        

    </div>
    
    <br />
    <br />

    <br />
    
    <asp:Label ID="lblSubTotal" runat="server" Text="Label"></asp:Label>
    
    <br />
    <br />
    <asp:Button ID="btnCalculateTotal" CssClass="btn" runat="server" Text="Calculate Total" />
    <br />
    <asp:Label ID="lblTotal" runat="server" Text="Label"></asp:Label>



    <br />
    <br />
    <asp:Button ID="btnPay" runat="server" CssClass="btn" Text="Pay" />



</asp:Content>

