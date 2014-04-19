Option Strict On
Partial Class _Default
    Inherits System.Web.UI.Page

    Dim DBSeats As New DBSeats
    Dim DBTickets As New DBTickets

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        'check customer login

        'Dim strCheck As String
        ''check session reservationID if it's empty
        'strCheck = Session("ReservationID").ToString
        'If strCheck = "" Then
        '    Response.Redirect("HomePage.aspx")
        'End If

        'next, need to load all tickets dataset
        DBTickets.GetALLTicketsUsingSP()
        DBTickets.GetALLOthersTicketsUsingSP()


        'check seats to initialize them


    End Sub

    Public Sub CheckSeats()

    End Sub

    Public Sub ChangeSeat(strSeat As String, strType As String)

    End Sub


    Public Sub SortandBind()
        'Author: Ben Shadburne
        'Purpose: sort the data and bind it 
        'Arguments:  na
        'Return: sorted and binded data
        'Date: 03/18/2014

        'sort 
        'DBTickets.DoSort()

        ''bind all data
        gvYourReservation.DataSource = DBTickets.MyView
        gvYourReservation.DataBind()
        gvOtherReservation.DataSource = DBTickets.MyViewOthers
        gvYourReservation.DataBind()

        '' show record count
        'lblCountDirect.Text = CStr(DBFlightSearch.lblCount)
        'lblCountIndirect.Text = CStr(DBFlightSearch.lblCountStart)
    End Sub

End Class
