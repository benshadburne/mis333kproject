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
                     <asp:CheckBox ID="chkOnFlight" runat="server" />
                 </ItemTemplate>
             </asp:TemplateField>
         </Columns>
    </asp:GridView>

    <br />
    <br />
    <asp:Button ID="btnConfirm" runat="server" text="confirm"> </asp:Button>

    <asp:panel id="manifest" runat="server" Visible ="false">
        <asp:DropDownList ID="DropDownList2" runat="server" padding ="3px"></asp:DropDownList>
     <br />
     <asp:GridView ID="GridView1" runat="server">
         <Columns>
             <asp:CheckBoxField />
         </Columns>
    </asp:GridView>

    <br />
    <br />
    <asp:Button ID="btnDeparted" runat="server" text="Departed"> </asp:Button>
    </asp:panel>
</asp:Content>

