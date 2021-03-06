﻿Option Strict On
'Jace Barton
Partial Class Emp_Select_Cust_To_Modify
    Inherits System.Web.UI.Page

    Dim CObject As New DBCustomersClone
    Dim valid As New ClassValidate

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


        If IsPostBack = False Then
            CObject.GetAllCustomersCloneUsingSP()
            LoadGridView()
        End If
    End Sub
    Public Sub LoadGridView()
        CObject.SearchCustomerClone(rblSearchType.SelectedIndex, rblSearchBy.SelectedIndex, txtSearch.Text)
        gvCustomers.DataSource = CObject.MyView
        gvCustomers.DataBind()

        If gvCustomers.Rows.Count = 0 Then
            lblMessage.Text = "Your search returned no records. Please try again."
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim intAdvantageNumber As Integer
        lblMessage.Text = ""
        'check to make sure a search type is selected
        If txtSearch.Text = "" Then
            lblMessage.Text = "Please enter a search term."
            Exit Sub
        End If

        If rblSearchBy.SelectedIndex = 1 And valid.CheckIntegerWithSubstring(txtSearch.Text) = False Then
            lblMessage.Text = "Please enter an integer for the advantage number."
            Exit Sub
        End If

        
        If rblSearchType.SelectedIndex = -1 Then
            lblMessage.Text = "Please select a search type."
            Exit Sub
        End If

        LoadGridView()
    End Sub

    Protected Sub rblSearchBy_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblSearchBy.SelectedIndexChanged
        lblMessage.Text = ""

        If rblSearchBy.SelectedIndex = 1 Then
            'make partial/keyword invisible
            rblSearchType.Visible = False
        End If
        If rblSearchBy.SelectedIndex = 0 Then
            'make partial/keyword visible
            rblSearchType.Visible = True
        End If
    End Sub
End Class
