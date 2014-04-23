
Partial Class Res_SelectCustomer
    Inherits System.Web.UI.Page

    Dim DBReservations As New DBReservations
    Dim DBJourney As New DBjourneyclone
    Dim DBFlight As New DBFlightsClone
    Dim DBTicket As New DBTickets
    Dim CustomerDB As New DBCustomersClone
    Dim Validation As New ClassValidate


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If IsPostBack = False Then
            CustomerDB.GetAllCustomersCloneUsingSP()

            SortAndBind()
        End If

        ''Author: Aaryaman Singhal
        'Date: 02/06/2014
        'Description: A sub that gets all customers, sorts them, and then puts them on the form 

        Label2.Text = Session("Adults").ToString
        Label3.Text = Session("Children").ToString
        Label4.Text = Session("Babies").ToString

    End Sub

    Private Sub SortAndBind()
        gvCustomers.DataSource = CustomerDB.MyView
        gvCustomers.DataBind()


    End Sub


    Protected Sub gvCustomers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvCustomers.SelectedIndexChanged
        pnlDoSearch.Visible = False
        pnlAddAge.Visible = True

        lblAge.Text = "Please enter " & gvCustomers.Rows(gvCustomers.SelectedIndex).Cells(4).Text & " " & _
        gvCustomers.Rows(gvCustomers.SelectedIndex).Cells(3).Text & "'s age."

        gvCustomers.Visible = False

    End Sub

    Protected Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        lblAgeMessage.Text = ""
        lblMessage.Text = ""
        'run the validation before doing anything else

        If Validation.CheckIntegerWithSubstring(txtAge.Text) = False Then
            'bad data
            lblAgeMessage.Text = "Please enter a positive integer age."
            Exit Sub
        End If

        Dim intAge As Integer

        intAge = CInt(txtAge.Text)

        'this code makes sure the age entered is moderately realistic
        If intAge > 125 Then
            lblAgeMessage.Text = "We're sorry, Penguin united does not allow people 125 years old to fly due to health risks"
            Exit Sub
        End If

        'this code reduces the session variables for the number of people on the reservation who still need tickets
        If intAge > 12 Then
            If Session("Adults") = 0 Then
                'WHAT IF THE SAME CUSTOMER HAS A DIFFERENT AGE ON DIFFERENT FLIGHTS????
                lblAgeMessage.Text = "Your reservation has no more adults on it. Please enter an age that matches with your" & _
                    " selected number of tickets for children and babies from earlier"
                Exit Sub
            Else
                Session("Adults") = Session("Adults") - 1
            End If
        Else
            If intAge > 2 Then
                If Session("Children") = 0 Then
                    'WHAT IF THE SAME CUSTOMER HAS A DIFFERENT AGE ON DIFFERENT FLIGHTS????
                    lblAgeMessage.Text = "Your reservation has no more children on it. Please enter an age that matches with your" & _
                        " selected number of tickets for adults and babies from earlier"
                    Exit Sub
                Else
                    Session("Children") = Session("Children") - 1
                End If
            Else
                If Session("Babies") = 0 Then
                    'WHAT IF THE SAME CUSTOMER HAS A DIFFERENT AGE ON DIFFERENT FLIGHTS????
                    lblAgeMessage.Text = "Your reservation has no more babies on it. Please enter an age that matches with your" & _
                        " selected number of tickets for adults and children from earlier"
                    Exit Sub
                Else
                    Session("Babies") = Session("Babies") - 1
                End If
            End If
        End If

        'define variables for adding tickets
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

            'put the flight number for that journeyID in a string
            strFlightNumber = DBJourney.MyDataSet.Tables("tblJourneys").Rows(0).Item("FlightNumber")

            'get the flight information for the journey using its flight number
            DBFlight.GetOneFlight("usp_FlightClone_Get_One", "@FlightNumber", strFlightNumber)

            'put the base fare for the flight in a string
            strBaseFare = DBFlight.MyDataSet.Tables("tblFlightsClone").Rows(0).Item("BaseFare")

            'add the ticket
            DBTicket.AddTicket(Session("ReservationID").ToString, strAdvantageNumber, strJourneyID, strFlightNumber, strBaseFare)

        Next

        'make the gv visible again
        gvCustomers.Visible = True

        'change which panels are visible
        pnlDoSearch.Visible = True
        pnlAddAge.Visible = False

        If Session("Adults") = Session("Children") And Session("Adults") = Session("Babies") And Session("Adults") = 0 Then
            'remove session variables and redirect
            Session.Remove("Adults")
            Session.Remove("Children")
            Session.Remove("Babies")
            Response.Redirect("Res_SeatSelection.aspx")
        End If

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
