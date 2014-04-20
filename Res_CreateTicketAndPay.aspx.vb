
Partial Class _Default
    Inherits System.Web.UI.Page
    Dim DBReservations As New DBReservations
    Dim DBJourney As New DBjourneyclone
    Dim DBFlight As New DBFlightsClone
    Dim DBTicket As New DBTickets
    'define a counter variables

    Dim intCurrentRecord As Integer

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Session("ReservationID") = 10025

        DBReservations.GetJourneysInReservation(CInt(Session("ReservationID")))

        If IsPostBack = False Then
            LoadGridview()
            Session("CurrentRecord") = 0
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

        DBJourney.GetOneJourney(CInt(DBReservations.MyDataSet.Tables("tblReservationsClone").Rows(Session("CurrentRecord")).Item("JourneyOne")))

        strJourneyID = DBJourney.MyDataSet.Tables("tblJourneys").Rows(Session("CurrentRecord")).Item("JourneyID")
        strFlightNumber = DBJourney.MyDataSet.Tables("tblJourneys").Rows(Session("CurrentRecord")).Item("FlightNumber")

        DBFlight.GetOneFlight("usp_FlightClone_Get_One", "@FlightNumber", strFlightNumber)

        strBaseFare = DBFlight.MyDataSet.Tables("tblFlightsClone").Rows(0).Item("BaseFare")

        DBTicket.AddTicket(Session("ReservationID").ToString, Session("SelectedCustomer").ToString, strJourneyID, strFlightNumber, strBaseFare)

        TextBox1.Text = strJourneyID & "," & strFlightNumber & "," & strBaseFare


    End Sub
End Class
