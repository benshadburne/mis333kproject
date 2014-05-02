
Option Strict On
Partial Class _Default
    Inherits System.Web.UI.Page

    Dim DBReservations As New DBReservations

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        'check if login session is empty
        If Session("UserID") Is Nothing Or Session("UserType") Is Nothing Then
            Response.Redirect("HomePage.aspx")
        End If

        If Session("UserType").ToString <> "Customer" Then
            Response.Redirect("HomePage.aspx")
        End If

        If Not (Session("NoJourneys") Is Nothing) Then
            Session.Remove("NoJourneys")
            lblMessage.Text = "No Journeys found on this reservation"
        End If
        'SHOULD MANAGERS BE ABLE TO SELECT ALL RESERVATIONS TO VIEW THEM???
        'trys to lead database and do a search on the datafield, if there's an error  (they don't have reservations) it sends them back to customer dash
        Try
            DBReservations.GetALLReservationsUsingSP()
            DBReservations.SearchByAdvantageNumber(Session("UserID").ToString)
        Catch ex As Exception
            Response.Redirect("Cust_CustomerDashboard.aspx")
        End Try

        SortandBind()

        If DBReservations.lblCount = 0 Then
            lblMessage.Text = "No Results Found"
        End If
        'lblMessage.Text = Session("UserID").ToString
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

        Session.Add("ReservationID", gvAllReservations.Rows(gvAllReservations.SelectedIndex).Cells(1).Text)
        Session.Add("FromReservation", "Yes")
        Response.Redirect("Cust_ReservationDetails.aspx")

    End Sub

    
    Protected Sub btnCustomerDash_Click(sender As Object, e As EventArgs) Handles btnCustomerDash.Click
        Response.Redirect("Cust_CustomerDashboard.aspx")
    End Sub
End Class
