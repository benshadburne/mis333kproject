<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Res_CreateTicketAndPay.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="header">
        Create Ticket and Pay
    </div>

    <div id="gridview">
        

        <asp:GridView ID="gvJourney" runat="server">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
        </asp:GridView>

        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>

    </div>
    
    <br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Flight Number"></asp:Label>
    <asp:TextBox ID="txtFlightNumber" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Age"></asp:Label>
    <asp:TextBox ID="txtAge" runat="server"></asp:TextBox>
    
    <br />
    <asp:Button ID="btnGetBaseFare" CssClass="btn" runat="server" Text="Get Base Fare" />
    <br />
    <asp:Label ID="lblResult" runat="server" Text="Label"></asp:Label>



</asp:Content>

