
Partial Class Emp_SearchCustomer
    Inherits System.Web.UI.Page

    Dim CustomerDB As New DBCustomersClone

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If IsPostBack = False Then
            SortAndBind()
        End If

        ''Author: Aaryaman Singhal
        'Date: 02/06/2014
        'Description: A sub that gets all customers, sorts them, and then puts them on the form 


    End Sub

    Private Sub SortAndBind()
        CustomerDB.GetAllCustomersCloneUsingSP()
        CustomerDB.SortCustomersClone(radSort.SelectedValue)
        gvCustomers.DataSource = CustomerDB.MyView
        gvCustomers.DataBind()


    End Sub




    Protected Sub RadioButtonList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles radSort.SelectedIndexChanged
        SortAndBind()
    End Sub

    Protected Sub gvCustomers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvCustomers.SelectedIndexChanged
        'add a session variable to remember the selected customer
        Session.Add("SelectedCustomer", gvCustomers.SelectedRow.Cells(1).Text)

        ''send the user to another page
        'Response.Redirect()
    End Sub
End Class
