<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Cust_ReservationDetails.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <br />
    <br />

    <div class="center-block">

        <asp:Label CssClass="h1" ID="Label1" runat="server" Text="Reservation ID: "></asp:Label>
        <asp:Label CssClass="h1" ID="lblReservationID" runat="server" Text=""></asp:Label>
       
        <br />
        <br />
        <br />
        <asp:Label CssClass="h3" ID="Label2" runat="server" Text="Your Tickets:"></asp:Label>
        <asp:GridView class="table" ID="gvYourReservation" runat="server"></asp:GridView>
        <br />
         <asp:Label CssClass="h3" ID="Label3" runat="server" Text="Other Tickets:"></asp:Label>
        <asp:GridView class="table" ID="gvOtherReservation" runat="server" ShowHeader="False"></asp:GridView>
   <br />
        <asp:Label CssClass="label" ID="Label5" runat="server" Text="Choose the JourneyID:"></asp:Label>
        <br />
        <asp:DropDownList CssClass="dropdown" ID="ddlJourneyID" runat="server"></asp:DropDownList>
        <br />
        <br />
        <div class="centerblock" id="JourneySeats">
            <button type="button" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon glyphicon-unchecked-you"></span>
</button>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<button type="button" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon glyphicon-unchecked-party"></span>
</button>
            &nbsp;&nbsp;&nbsp;
            <button type="button" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon glyphicon-unchecked-filled"></span>
</button>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <button type="button" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon glyphicon-unchecked"></span>
</button>
            <br />
            <button type="button" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon glyphicon-unchecked"></span>
</button>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <button type="button" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon glyphicon-unchecked"></span>
</button>
            &nbsp;&nbsp;&nbsp;
            <button type="button" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon glyphicon-unchecked"></span>
</button>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <button type="button" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon glyphicon-unchecked"></span>
</button>
            <br />
            <button type="button" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon glyphicon-unchecked"></span>
</button>
            <button type="button" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon glyphicon-unchecked"></span>
</button>
            <button type="button" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon glyphicon-unchecked"></span>
</button>
            &nbsp;&nbsp;
            <button type="button" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon glyphicon-unchecked"></span>
</button>
            <button type="button" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon glyphicon-unchecked"></span>
</button>
            <button type="button" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon glyphicon-unchecked"></span>
</button>
            <br />
            <button type="button" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon glyphicon-unchecked"></span>
</button>
            <button type="button" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon glyphicon-unchecked"></span>
</button>
            <button type="button" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon glyphicon-unchecked"></span>
</button>
            &nbsp;&nbsp;
            <button type="button" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon glyphicon-unchecked"></span>
</button>
            <button type="button" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon glyphicon-unchecked"></span>
</button>
            <button type="button" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon glyphicon-unchecked"></span>
</button>
        </div>
        <br />
        <br />
        <asp:Label CssClass="h5" ID="Label4" runat="server" Text="Need to make changes?"></asp:Label>
       <br />
        <ul>
         <li><a href="Cust_ModifyReservation.aspx">Modify Reservation</a></li>
</ul>
         </div>


</asp:Content>

