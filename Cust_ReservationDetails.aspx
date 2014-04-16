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
        <div style="width: 250px; text-align: center">
            <asp:Label CssClass="label" ID="Label6" runat="server" Text="Front of Plane" ></asp:Label>
        </div>
       <div class ="center-block" width: 100%">
            <div class="pull-left" id="left isle" style="width:125px">
                <button type="button" id="btn1A" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon glyphicon-unchecked-you"></span>
</button>
                <br />
                 <button type="button" id="btn2A" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon-unchecked"></span>
</button>
           <br />
                <button type="button" id="btn3A" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon-unchecked"></span>
</button>
            <button type="button" id="btn3B" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon-unchecked"></span>
</button>
                <br />
                <button type="button" id="btn4A" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon-unchecked"></span>
</button>
            <button type="button" id="btn4B" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon-unchecked"></span>
</button>
                <br />
                <button type="button" id="btn5A" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon-unchecked"></span>
</button>
            <button type="button" id="btn5B" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon-unchecked"></span>
</button>
            </div>


            <div class="pull-left" id="right isle" style="width:125px">
                <button type="button" id="btn1B" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon glyphicon-unchecked-you"></span>
</button>
                <br />
                 <button type="button" id="btn2B" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon-unchecked"></span>
</button>
           <br />
                <button type="button" id="btn3C" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon-unchecked"></span>
</button>
            <button type="button" id="btn3D" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon-unchecked"></span>
</button>
                <br />
                <button type="button" id="btn4C" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon-unchecked"></span>
</button>
            <button type="button" id="btn4D" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon-unchecked"></span>
</button>
                <br />
                <button type="button" id="btn5C" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon-unchecked"></span>
</button>
            <button type="button" id="btn5D" class="btn btn-default btn-lg">
  <span class="glyphicon glyphicon-unchecked"></span>
</button>
            </div>
        </div>
       
        <div style="width: 250px; text-align: center" class="pull-left">
            <asp:Label CssClass="label" ID="Label7" runat="server" Text="Rear of Plane" ></asp:Label>
        </div>
        <br />
        <br />
            <div class="center-block" style="width:100%">
        <asp:Label CssClass="h5" ID="Label4" runat="server" Text="Need to make changes?"></asp:Label>
       </div>
        <ul>
         <li><a href="Cust_ModifyReservation.aspx">Modify Reservation</a></li>
</ul>
        </div>
       
       
         


</asp:Content>

