﻿Option Strict On
'Jace Barton
Partial Class Emp_Select_Cust_To_Modify
    Inherits System.Web.UI.Page

    Dim CObject As New DBCustomersClone

    Protected Sub gvCustomers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvCustomers.SelectedIndexChanged
        Session("AdvantageNumber_Selected_By_Manager") = gvCustomers.SelectedRow.Cells(1).Text
        Response.Redirect("Cust_ModifyProfile.aspx")
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'check for session variables
        If Session("UserType") Is Nothing Then
            Response.Redirect("HomePage.aspx")
        ElseIf Session("UserType").ToString = "Manager" Or Session("UserType").ToString = "Agent" Or Session("UserType").ToString = "Crew" Then
            'everything's good, they're allowed to see this page
        Else 'logged in as something other than customer
            Response.Redirect("HomePage.aspx")
        End If


        CObject.GetAllCustomersCloneUsingSP()
        If IsPostBack = False Then
            gvCustomers.DataSource = CObject.MyDataset.Tables("tblCustomersClone")
            gvCustomers.DataBind()
        End If
    End Sub
End Class
