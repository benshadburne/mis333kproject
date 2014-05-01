﻿
Partial Class Emp_SearchCustomer
    Inherits System.Web.UI.Page

    Dim CustomerDB As New DBCustomersClone

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        'Check to see if anyone is logged in
        If Session("UserID") Is Nothing Then
            Response.Redirect("HomePage.aspx")
        End If

        'Check to see if person logged in is an Employee; if customer, redirect to home page
        If Session("UserType").ToString = "Customer" Then
            Response.Redirect("HomePage.aspx")
        End If

        If IsPostBack = False Then
            CustomerDB.GetAllCustomersCloneUsingSP()

            SortAndBind()
        End If

        ''Author: Aaryaman Singhal
        'Date: 02/06/2014
        'Description: A sub that gets all customers, sorts them, and then puts them on the form 


    End Sub

    Private Sub SortAndBind()
        gvCustomers.DataSource = CustomerDB.MyView
        gvCustomers.DataBind()

    End Sub


    Protected Sub gvCustomers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvCustomers.SelectedIndexChanged
        'add a session variable to remember the selected customer
        Session.Add("SelectedCustomer", gvCustomers.SelectedRow.Cells(1).Text)

        ''send the user to another page
        'Response.Redirect()
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        CustomerDB.SearchCustomerClone(rblSearchType.SelectedIndex, rblSearchBy.SelectedIndex, txtSearch.Text)
        SortAndBind()
    End Sub

    Protected Sub rblSearchBy_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblSearchBy.SelectedIndexChanged
        If rblSearchBy.SelectedIndex = 1 Then
            'make partial/keyword invisible
            rblSearchType.Visible = False
        End If
    End Sub
End Class
