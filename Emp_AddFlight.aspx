﻿<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Emp_AddFlight.aspx.vb" Inherits="Emp_AddFlight" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div id ="left">
        <div class ="label-primary">
            
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" /><br /><br />
        </div><asp:Label ID="lblMessage" runat="server" Text="Welcome to the Add Flight Page!"></asp:Label>
        <div class ="btn">
            <br /><br />
            <br /><br />
        </div><asp:Button ID="btnAdd" runat="server" Text="Add Flight" />

    </div>

    <div id ="middle">

<asp:Label ID="Label2" runat="server" Text="Flight Number"></asp:Label><br />
            <asp:Label ID="Label3" runat="server" Text="Departure City"></asp:Label><br />
            <asp:Label ID="Label4" runat="server" Text="Arrival City"></asp:Label><br />
            <asp:Label ID="Label5" runat="server" Text="Departure Time"></asp:Label><br />
            <asp:Label ID="Label6" runat="server" Text="Arrival Time"></asp:Label><br />
            <asp:Label ID="Label7" runat="server" Text="Base Fare"></asp:Label><br />
        <asp:Label ID="Label9" runat="server" Text="Days Flown"></asp:Label><br />

        
        </div>
        <div id ="right">
            <div class ="input-group-lg">
                <asp:TextBox ID="txtFlightNumber" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvFlightNumber" runat="server" ErrorMessage="Please enter a flight number!" ControlToValidate="txtFlightNumber" Text="*"></asp:RequiredFieldValidator><br />
                <asp:DropDownList ID="ddlDepartureCity" runat="server"></asp:DropDownList><br />
                <asp:DropDownList ID="ddlArrivalCity" runat="server"></asp:DropDownList><br />
                <asp:DropDownList ID="ddlDepartureTimeHour" runat="server">
                    <asp:ListItem>00</asp:ListItem>
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
                    <asp:ListItem>12</asp:ListItem>
                    <asp:ListItem>13</asp:ListItem>
                    <asp:ListItem>14</asp:ListItem>
                    <asp:ListItem>15</asp:ListItem>
                    <asp:ListItem>16</asp:ListItem>
                    <asp:ListItem>17</asp:ListItem>
                    <asp:ListItem>18</asp:ListItem>
                    <asp:ListItem>19</asp:ListItem>
                    <asp:ListItem>20</asp:ListItem>
                    <asp:ListItem>21</asp:ListItem>
                    <asp:ListItem>22</asp:ListItem>
                    <asp:ListItem>23</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="Label1" runat="server" Text=":"></asp:Label>
                <asp:DropDownList ID="ddlDepartureTimeMinutes" runat="server">
                    <asp:ListItem>00</asp:ListItem>
                    <asp:ListItem>15</asp:ListItem>
                    <asp:ListItem>30</asp:ListItem>
                    <asp:ListItem>45</asp:ListItem>
                </asp:DropDownList><br />
                <asp:Label ID="lblArrivalTime" runat="server" Text="Arrival Time"></asp:Label><asp:Button ID="btnCalculateArrivalTime" runat="server" Text="Calculate Arrival Time" /><br />
                <asp:TextBox ID="txtBaseFare" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvBaseFare" runat="server" ErrorMessage="Please enter a base fare!" ControlToValidate="txtBaseFare" Text="*"></asp:RequiredFieldValidator><br />
                <asp:CheckBoxList ID="cblDaysToFly" runat="server">
                    <asp:ListItem>Monday</asp:ListItem>
                    <asp:ListItem>Tuesday</asp:ListItem>
                    <asp:ListItem>Wednesday</asp:ListItem>
                    <asp:ListItem>Thursday</asp:ListItem>
                    <asp:ListItem>Friday</asp:ListItem>
                    <asp:ListItem>Saturday</asp:ListItem>
                    <asp:ListItem>Sunday</asp:ListItem>
                </asp:CheckBoxList>
        </div>
            </div>
</asp:Content>

