
Partial Class _Default
    Inherits System.Web.UI.Page


    Dim DB As New asdfasdf

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load


        DB.GetALLCustomersUsingSP()

        SortandBind()
    End Sub

    Public Sub SortandBind()
        'Author: Ben Shadburne
        'Purpose: sort the data and bind it 
        'Arguments:  na
        'Return: sorted and binded data
        'Date: 03/18/2014

        'sort using radio
        DB.DoSort(radSort.SelectedIndex)

        gvCustomers.DataSource = DB.MyView
        gvCustomers.DataBind()

        ' show record count
        lblCount.Text = CStr(DB.lblCount)
    End Sub
End Class
