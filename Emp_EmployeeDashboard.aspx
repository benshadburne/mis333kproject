<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Emp_EmployeeDashboard.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      
    <br />
    <br />

    <div class="center-block">

       
        
        <asp:Panel CssClass="panel" ID="Panel1" runat="server" Width="30%">
            <asp:Label CssClass="h3" ID="Label1" runat="server" Text="Employee Links"></asp:Label>

            <ul class="list-group">
               <li><a href="Emp_ModifyEmployee.aspx">Modify Employee</a>  </li> 
                <li><a href="Emp_ViewSchedule.aspx">View Crew Schedule</a></li>
            </ul>

        </asp:Panel>

         <asp:Panel CssClass="panel" ID="Panel3" runat="server" Width="30%">
            <asp:Label CssClass="h3" ID="Label2" runat="server" Text="Agent Links"></asp:Label>

            <ul class="list-group">
               <li> <a href="Emp_GateCheckIn.aspx">Gate Check In</a> </li> 
                <li><a href="Emp_SearchCustomer.aspx">Search Customer</a> </li>               
                 <li><a href="Emp_Select_Cust_To_Modify.aspx">Modify Customer</a> </li>
                <li><a href="Cust_AddNewCustomer.aspx">Create Customer Profile</a></li>
                <li><a href ="Cust_CreateReservationAndSelectFlight.aspx">Create Customer Reservation</a></li>
          </ul>

        </asp:Panel>
       
        <asp:Panel CssClass="panel" ID="Panel2" runat="server" Width="30%">

            <asp:Label CssClass="h3" ID="Label3" runat="server" Text="Management Links"></asp:Label>

            <ul class="list-group">
                <li><a href="Emp_AddCity.aspx">Add City</a> </li>
                <li><a href="Emp_AddFlight.aspx">Add Flight</a> </li>
                <li><a href="Emp_AddNewEmployee.aspx">Add Employee</a> </li>
                <li><a href="Emp_Cancel_Journey.aspx">Cancel Journey</a> </li>
                <li><a href="Emp_CancelFlight.aspx">Cancel Flight</a></li>
                <li><a href="Emp_ChangeDateTime.aspx">Change Date/Time</a> </li>
                <li><a href="Emp_CrewScheduling.aspx">Crew Scheduling</a> </li>
                <li><a href="Emp_ModifyFlight.aspx">Modify Flight</a> </li>
                <li><a href="Emp_SelectEmployeeToModify.aspx">Select Employee to Modify</a> </li>
                <li> <a href="Emp_ViewReports.aspx">View Reports</a></li>
                <li><a href ="Emp_ReactivateJourney.aspx">Activate Cancelled Journey</a></li>
                <li><a href="Emp_ReactivateFlight.aspx">Activate Cancelled Flight</a></li>
            </ul>
        </asp:Panel>

        


    </div>


</asp:Content>

