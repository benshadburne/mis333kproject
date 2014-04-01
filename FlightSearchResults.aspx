<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="FlightSearchResults.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    

    <br />
    <div class="center-block" style="width: 79%">
    <div class="pull-left">
        <asp:Label class="label" ID="Label3" runat="server" Text="Day of Week" ForeColor="Black"></asp:Label>
    <asp:CheckBoxList class="checkbox" ID="cblDays" runat="server" Width="107px" ForeColor="Black">
        <asp:ListItem>Monday</asp:ListItem>
        <asp:ListItem>Tuesday</asp:ListItem>
        <asp:ListItem>Wednesday</asp:ListItem>
        <asp:ListItem>Thursday</asp:ListItem>
        <asp:ListItem>Friday</asp:ListItem>
        <asp:ListItem>Saturday</asp:ListItem>
        <asp:ListItem>Sunday</asp:ListItem>
    </asp:CheckBoxList>
        </div>
        <br />
    <div class="pull-left">
        <asp:Label class="label" ID="Label2" runat="server" Text="Time" ForeColor="Black" Font-Bold="False" Font-Size="Medium"></asp:Label>
            </div>
        <br />
    <br />
   

    <div class="pull-left">
        <asp:DropDownList class="dropdown" ID="ddlTimeOfDay" runat="server">
            <asp:ListItem>12</asp:ListItem>
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            <asp:ListItem>5</asp:ListItem>
            <asp:ListItem>6</asp:ListItem>
            <asp:ListItem>7</asp:ListItem>
            <asp:ListItem>8</asp:ListItem>
            <asp:ListItem>9</asp:ListItem>
            <asp:ListItem>10</asp:ListItem>
            <asp:ListItem>11</asp:ListItem>
            <asp:ListItem></asp:ListItem>
        </asp:DropDownList>


        <asp:Label class="label" ID="Label1" runat="server" Text="  :  " ForeColor="Black"></asp:Label>
          

 
        <asp:DropDownList class="dropdown" ID="ddlFifteenInterval" runat="server">
            <asp:ListItem>00</asp:ListItem>
             <asp:ListItem>15</asp:ListItem>
            <asp:ListItem>30</asp:ListItem>
            <asp:ListItem>45</asp:ListItem>

            <asp:ListItem></asp:ListItem>
        </asp:DropDownList>

           
    
        <asp:RadioButtonList class="radio" ID="RadioButtonList1" runat="server">
            <asp:ListItem Selected="True">AM</asp:ListItem>
            <asp:ListItem>PM</asp:ListItem>
        </asp:RadioButtonList>

   
            </div>
  </div>

    <br />
   
    <div class="center-block" style="width:79%">
    <asp:Button class="btn" ID="btnSearch" runat="server" Text="Search" />
    </div>

</asp:Content>

