
Partial Class Res_SelectCustomer
    Inherits System.Web.UI.Page

    Dim DBReservations As New DBReservations
    Dim DBJourney As New DBjourneyclone
    Dim DBFlight As New DBFlightsClone
    Dim DBTicket As New DBTickets
    Dim CustomerDB As New DBCustomersClone

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Session.Add("ReservationID", 10025)


        If IsPostBack = False Then
            CustomerDB.GetAllCustomersCloneUsingSP()

            SortAndBind()
        End If

        ''Author: Aaryaman Singhal
        'Date: 02/06/2014
        'Description: A sub that gets all customers, sorts them, and then puts them on the form 


    End Sub

    Private Sub SortAndBind()
        gvCustomers.DataSource = CustomerDB.MyView
        gvCustomers.DataBind()


    End Sub


    Protected Sub gvCustomers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvCustomers.SelectedIndexChanged
        Dim strJourneyID As String
        Dim strFlightNumber As String
        Dim strBaseFare As String
        Dim intTotalJourneys As Integer
        Dim strAdvantageNumber As String

        'add a session variable to remember the selected customer
        strAdvantageNumber = gvCustomers.SelectedRow.Cells(1).Text

        'this populates a list of all the journeys in a reservation
        DBReservations.GetJourneysInReservation(CInt(Session("ReservationID")))

        'this counts the total number of journeys in a reservation
        intTotalJourneys = DBReservations.MyDataSet.Tables("tblReservationsClone").Rows.Count - 1

        'this loop runs from 0 through all the journeys in a reservation


        For i = 0 To intTotalJourneys

            'select the first journeyID from the list of journeyIDs
            DBJourney.GetOneJourney(CInt(DBReservations.MyDataSet.Tables("tblReservationsClone").Rows(i).Item("JourneyOne")))

            'put that journey id in a string
            strJourneyID = DBJourney.MyDataSet.Tables("tblJourneys").Rows(0).Item("JourneyID")

            If DBTicket.CheckIfTicketIsUnique(strJourneyID, strAdvantageNumber) = True Then
                'ticket is unique -- add tickets
            Else
                'lblMessage.Text = this customer is already on a flight in this reservation. Give these tickets to a different customer
                Exit Sub
            End If

            'put the flight number for that journeyID in a string
            strFlightNumber = DBJourney.MyDataSet.Tables("tblJourneys").Rows(0).Item("FlightNumber")

            'get the flight information for the journey using its flight number
            DBFlight.GetOneFlight("usp_FlightClone_Get_One", "@FlightNumber", strFlightNumber)

            'put the base fare for the flight in a string
            strBaseFare = DBFlight.MyDataSet.Tables("tblFlightsClone").Rows(0).Item("BaseFare")

            DBTicket.AddTicket(Session("ReservationID").ToString, strAdvantageNumber, strJourneyID, strFlightNumber, strBaseFare)

        Next

        'Session("Adults") -= 1 <--- this could also be a child or baby or whatever
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        CustomerDB.SearchCustomerClone(rblSearchType.SelectedIndex, rblSearchBy.SelectedIndex, txtSearch.Text)
        SortAndBind()
    End Sub

    Protected Sub rblSearchBy_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblSearchBy.SelectedIndexChanged
        If rblSearchBy.SelectedIndex = 1 Then
            'make partial/keyword invisible
            rblSearchType.Visible = False
        End If
    End Sub
End Class
