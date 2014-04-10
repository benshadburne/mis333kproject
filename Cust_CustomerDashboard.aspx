<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Cust_CustomerDashboard.aspx.vb" Inherits="_Default" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <br />
    <br />

    <div class="center-block">

       
        
        <asp:Panel CssClass="panel" ID="Panel1" runat="server" Width="30%">
            <asp:Label CssClass="h3" ID="Label1" runat="server" Text="Reservation Links"></asp:Label>

            <ul class="list-group">
               <li> <a href="Cust_AllReservations.aspx">All Reservations </a> </li> 
               <li> <a href="Cust_CreateReservationAndSelectFlight.aspx">Create Reservation </a></li>               
                 <li> <a href="Cust_ReservationDetails.aspx">Reservation Details </a></li>
            
            </ul>

        </asp:Panel>
       
        <asp:Panel CssClass="panel" ID="Panel2" runat="server" Width="30%">

            <asp:Label CssClass="h3" ID="Label3" runat="server" Text="Profile Links"></asp:Label>

            <ul class="list-group">
                <li><a href="Cust_ModifyProfile.aspx">Modify Profile</a></li>
                <li><a href="Cust_AddNewCustomer.aspx">Add New Profile </a></li>

            </ul>
        </asp:Panel>

        


    </div>



</asp:Content>

