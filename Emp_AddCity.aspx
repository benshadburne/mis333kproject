<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Emp_AddCity.aspx.vb" Inherits="Emp_AddCity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="header">
       Add Airport
   </div>
    <asp:Panel runat="server" ID="pnlAddAirport"> 
   <div class ="label-default" id="middle">

        <asp:Label ID="Label1" runat="server" Text="City name:"></asp:Label>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Airport Code:"></asp:Label>
        <br />

<br />
        <br />

        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
        <br />


        </div>

    <div class ="input-group-lg"id="right">
        <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvCity" runat="server" ErrorMessage="The city name is a required field." Text="*" ControlToValidate="txtCity"></asp:RequiredFieldValidator>
        <br /> 
        <asp:TextBox ID="txtAirport" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvAirport" runat="server" ErrorMessage="The airport code must be 3 letters." Text="*" ControlToValidate="txtAirport"></asp:RequiredFieldValidator>
        <br /> 
        <br /> 
        <br />
        <br />
        <asp:Button ID="btnAdd" runat="server" Text="Add" />
        <br /> 
        </div>
    </asp:Panel>

    <asp:Panel runat="server" ID="pnlAddInfo" Visible ="false"> 
   <div class ="label-default" id="Div1">
       <br />

        <asp:Label ID="Label3" runat="server" Text="Mileage"></asp:Label>
        <br />
        <asp:Label ID="Label4" runat="server" Text="Flight Time:"></asp:Label>
        <br />

<br />
        <br />

        <asp:Label ID="Label5" runat="server" Text="Add the mileage and flight time to " ForeColor="Red"></asp:Label>
        <br />


        </div>

    <div class ="input-group-lg"id="Div2">
        <br />
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="The city name is a required field." Text="*" ControlToValidate="txtCity"></asp:RequiredFieldValidator>
        <br /> 
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="The airport code must be 3 letters." Text="*" ControlToValidate="txtAirport"></asp:RequiredFieldValidator>
        <br /> 
        <br /> 
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Add" />
        <br /> 
        </div>
    </asp:Panel>




    
</asp:Content>

