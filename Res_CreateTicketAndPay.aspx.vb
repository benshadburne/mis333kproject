Option Strict On

Partial Class _Default
    Inherits System.Web.UI.Page

    'Declare instances of classes
    Dim calc As New ClassCalculate

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

        DBJourney.GetOneJourney(CInt(DBReservations.MyDataSet.Tables("tblReservationsClone").Rows(CInt(Session("CurrentRecord"))).Item("JourneyOne")))

        gvJourney.DataSource = DBJourney.MyDataSet
        gvJourney.DataBind()
        intCurrentRecord += 1
    End Sub

    Protected Sub gvJourney_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvJourney.SelectedIndexChanged
        Dim strJourneyID As String
        Dim strFlightNumber As String
        Dim strBaseFare As String

        DBJourney.GetOneJourney(CInt(DBReservations.MyDataSet.Tables("tblReservationsClone").Rows(CInt(Session("CurrentRecord"))).Item("JourneyOne")))

        strJourneyID = DBJourney.MyDataSet.Tables("tblJourneys").Rows(CInt(Session("CurrentRecord"))).Item("JourneyID").ToString
        strFlightNumber = DBJourney.MyDataSet.Tables("tblJourneys").Rows(CInt(Session("CurrentRecord"))).Item("FlightNumber").ToString

        DBFlight.GetOneFlight("usp_FlightClone_Get_One", "@FlightNumber", strFlightNumber)

        strBaseFare = DBFlight.MyDataSet.Tables("tblFlightsClone").Rows(0).Item("BaseFare").ToString

        DBTicket.AddTicket(Session("ReservationID").ToString, Session("SelectedCustomer").ToString, strJourneyID, strFlightNumber, strBaseFare)

        TextBox1.Text = strJourneyID & "," & strFlightNumber & "," & strBaseFare


    End Sub

    Protected Sub btnGetBaseFare_Click(sender As Object, e As EventArgs) Handles btnGetBaseFare.Click


        'Populate the inputs that are public properties in the Calculate class
        calc.FlightNumber = txtFlightNumber.Text
        calc.CustomerAge = CInt(txtAge.Text)

        'Run the subtotal calculation
        calc.CalculateSubTotalDiscount()

        'Output the subtotal calculation
        lblResult.Text = calc.Subtotal.ToString

    End Sub
End Class
