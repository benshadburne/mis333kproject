
Partial Class _Default
    Inherits System.Web.UI.Page
    Dim DBReservations As New DBReservations
    Dim DBJourney As New DBjourneyclone
    Dim DBFlight As New DBFlightsClone
    'define a counter variables

    Dim intCurrentRecord As Integer

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Session("ReservationID") = 10025

        DBReservations.GetJourneysInReservation(CInt(Session("ReservationID")))

        If IsPostBack = False Then
            LoadGridview()
            Session("CurrentRecord") = 1
        End If
    End Sub

    Private Sub LoadGridview()

        DBJourney.GetOneJourney(CInt(DBReservations.MyDataSet.Tables("tblReservationsClone").Rows(Session("CurrentRecord")).Item("JourneyOne")))

        gvJourney.DataSource = DBJourney.MyDataSet
        gvJourney.DataBind()
        intCurrentRecord += 1
    End Sub

    Protected Sub gvJourney_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvJourney.SelectedIndexChanged
        Dim strJourneyID As String
        Dim strFlightNumber As String
        Dim strBaseFare As String

        strJourneyID = DBJourney.MyDataSet.Tables("tblJourneys").Rows(Session("CurrentRecord")).Item("JourneyID")
        strFlightNumber = DBJourney.MyDataSet.Tables("tblJourneys").Rows(Session("CurrentRecord")).Item("FlightNumber")

        DBFlight.GetOneFlight("usp_FlightClone _Get_One", "@FlightNumber", strFlightNumber)

        strBaseFare = DBFlight.MyDataSet.Tables("tblFlightsClone").Rows(0).Item("BaseFare")






    End Sub
End Class
