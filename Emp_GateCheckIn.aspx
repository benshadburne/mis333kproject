<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Emp_GateCheckIn.aspx.vb" Inherits="Emp_GateCheckIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:GridView ID="gvFlight" runat="server">
         <Columns>
             <asp:CheckBoxField />
         </Columns>
    </asp:GridView>

    <br />
    <br />
    <asp:Button ID="btnConfirm" runat="server" text="confirm"> </asp:Button>
</asp:Content>

