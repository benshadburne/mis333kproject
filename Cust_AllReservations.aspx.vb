
Option Strict On
Partial Class _Default
    Inherits System.Web.UI.Page

    Dim DBReservations As New DBReservations

    

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim strCheck As String
        'check if login session is empty
        strCheck = Session("Login").ToString
        'advantage number is 4 digits, so checks for that
        If Len(strCheck) <> 4 Then
            Response.Redirect("HomePage.aspx")
        End If

        'load gv based on advantage number (strCheck should have advantage number
        DBReservations.GetALLReservationsUsingSP()
        DBReservations.SearchByAdvantageNumber(Session("Login").ToString)

    End Sub

    Public Sub SortandBind()
        'Author: Ben Shadburne
        'Purpose: sort the data and bind it 
        'Arguments:  na
        'Return: sorted and binded data
        'Date: 03/18/2014

        'sorts data then binds it
        DBReservations.DoSort()
        gvAllReservations.DataSource = DBReservations.MyView
        gvAllReservations.DataBind()


        ' show record count
        lblCountReservations.Text = "Count: " & CStr(DBReservations.lblCount)
    End Sub

    Protected Sub gvAllReservations_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvAllReservations.SelectedIndexChanged

        Session("ReservationID") = gvAllReservations.SelectedIndex.ToString

        Response.Redirect("Cust_ReservationDetails.aspx")

    End Sub
End Class
