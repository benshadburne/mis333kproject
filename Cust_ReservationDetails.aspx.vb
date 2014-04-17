
Partial Class _Default
    Inherits System.Web.UI.Page

    Dim DBSeats As New DBSeats


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim strCheck As String
        'check session reservationID if it's empty
        strCheck = Session("ReservationID").ToString
        If strCheck = "" Then
            Response.Redirect("HomePage.aspx")
        End If

        'next, need to load all tickets dataset


    End Sub
End Class
