
Partial Class _Default
    Inherits System.Web.UI.Page

    Dim DBSeats As New DBSeats
    Dim DBTickets As New DBTickets

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        'check customer login

        Dim strCheck As String
        'check session reservationID if it's empty
        strCheck = Session("ReservationID").ToString
        If strCheck = "" Then
            Response.Redirect("HomePage.aspx")
        End If

        'next, need to load all tickets dataset


    End Sub
End Class
