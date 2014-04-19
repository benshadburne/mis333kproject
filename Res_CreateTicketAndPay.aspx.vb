
Partial Class _Default
    Inherits System.Web.UI.Page
    Dim DBReservations As New DBReservations
    Dim DBJourney As New DBjourneyclone
    'define a counter variables

    Dim intCurrentRecord As Integer

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        DBReservations.GetJourneysInReservation(CInt(Session("ReservationID")))

        If IsPostBack = False Then
            LoadGridview()
            intCurrentRecord = 1
        End If
    End Sub

    Private Sub LoadGridview()

        DBJourney.GetOneJourney(CInt(DBReservations.MyDataSet.Tables("tblReservationsClone").Rows(intCurrentRecord).Item("JourneyOne")))

        gvJourney.DataSource = DBJourney.MyDataSet
        gvJourney.DataBind()
        intCurrentRecord += 1
    End Sub
End Class
