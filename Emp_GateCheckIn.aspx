<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Emp_GateCheckIn.aspx.vb" Inherits="Emp_GateCheckIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:DropDownList ID="ddlJourneys" runat="server" padding ="3px" AutoPostBack="True"></asp:DropDownList>
     <br />
     <asp:GridView ID="gvCustomers" runat="server">
         <Columns>
             <asp:TemplateField>
                 <ItemTemplate>
                     <asp:CheckBox ID="chkPassengers" runat="server" />
                 </ItemTemplate>
             </asp:TemplateField>
         </Columns>
    </asp:GridView>

    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>

    <br />
    <br />
    <asp:Button ID="btnConfirm" runat="server" text="confirm"> </asp:Button>

    <asp:panel id="manifest" runat="server" >
       
     <br />
     <asp:GridView ID="gvCrew" runat="server" visible="False">
         <Columns>
             <asp:TemplateField>
                 <ItemTemplate>
                     <asp:CheckBox ID="chkCrew" runat="server" />
                 </ItemTemplate>
             </asp:TemplateField>
         </Columns>
    </asp:GridView>

    <br />
    <br />
    <asp:Button ID="btnDeparted" runat="server" text="Departed" Visible ="false"> </asp:Button>
    </asp:panel>
</asp:Content>

