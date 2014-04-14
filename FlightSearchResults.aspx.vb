
Partial Class _Default
    Inherits System.Web.UI.Page


    Dim DB As New DBFlightSearch

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        'checks login session variable 
        'if empty, neither select nor edit show up
        'if it's a customer id then select shows up

        'if it's an employee id then select and edit show up



        ' DB.GetALLCustomersUsingSP()

        SortandBind()
    End Sub

    Public Sub SortandBind()
        'Author: Ben Shadburne
        'Purpose: sort the data and bind it 
        'Arguments:  na
        'Return: sorted and binded data
        'Date: 03/18/2014

        'sort using radio
        'DB.DoSort(radSort.SelectedIndex)

        'gvCustomers.DataSource = DB.MyView
        'gvCustomers.DataBind()

        ' show record count
        'lblCount.Text = CStr(DB.lblCount)
    End Sub

    Protected Sub Calendar1_SelectionChanged(sender As Object, e As EventArgs) Handles Calendar1.SelectionChanged

    End Sub

    Protected Sub gvDirectFlights_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDirectFlights.SelectedIndexChanged

        'Session("FlightChoice") = gvDirectFlights




    End Sub
End Class
