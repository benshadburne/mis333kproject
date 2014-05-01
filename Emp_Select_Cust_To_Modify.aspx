<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Emp_Select_Cust_To_Modify.aspx.vb" Inherits="Emp_Select_Cust_To_Modify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="header" style="text-align: center;">

        <h1>
            Select Customer to Modify Page
        </h1>

    </div>

    <br />
    <br />

    <div class ="center-block">
    <div class ="pull-left" style="width:20%;">
        <asp:Label ID="Label1" runat="server" padding="3px" Text="Select a Filter:"></asp:Label>

        <asp:RadioButtonList ID="rblSearchBy" runat="server" AutoPostBack="True">
            <asp:ListItem Selected="True" Value="0">Lastname</asp:ListItem>
            <asp:ListItem Value="1">Advantage Number (must be exact)</asp:ListItem>
        </asp:RadioButtonList>

        <br />
        <asp:RadioButtonList ID="rblSearchType" runat="server" >
            <asp:ListItem Value="0">Partial</asp:ListItem>
            <asp:ListItem Value="1" Selected="True">Keyword</asp:ListItem>
        </asp:RadioButtonList>
        <br />

        <br />
        

        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>

        <br />
        <br />

        <asp:Button ID="btnSearch" runat="server" Text="Search" />
           
    </div>
   
        <div class ="center-block" style="width: 70%;">
            <div id ="pull-right" style="width: 15%; float:left; text-align: right; line-height: 200%">
               
                
            </div>
            <div id ="pull-left" style="width: 70%; float: left; line-height: 160%">
                
                <asp:GridView ID="gvCustomers" runat="server">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
                </asp:GridView>
                
            </div>
            </div>
        </div>
</asp:Content>


