
Partial Class Res_SelectCustomer
    Inherits System.Web.UI.Page

    Dim DBReservations As New DBReservations
    Dim DBJourney As New DBjourneyclone
    Dim DBFlight As New DBFlightsClone
    Dim DBTicket As New DBTickets
    Dim CustomerDB As New DBCustomersClone
    Dim Validation As New ClassValidate
    Dim DBCancel As New CancelReservation


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("ReservationID") Is Nothing Then
            Response.Redirect("Homepage.aspx")
        End If

        If Session("UserType") Is Nothing Then
            Response.Redirect("HomePage.aspx")
        ElseIf Session("UserType").ToString = "Crew" Then
            Response.Redirect("Emp_EmployeeDashboard.aspx")
        ElseIf Session("UserType").ToString = "Customer" Then
            If Session("FirstTicket") Is Nothing Then
                'this is the first time the user is accessing this page
                CustomerDB.GetAllActiveCustomersCloneUsingSP()
                SortAndBind()

                'loop through the records in the DB 
                For i = 0 To gvCustomers.Rows.Count - 1
                    'set selected row
                    gvCustomers.SelectedIndex = i

                    'Check if userID is equal to customer logged in
                    If Session("UserID").ToString = gvCustomers.SelectedRow.Cells(1).Text Then
                        'this is the user we are looking for

                        'change what is visible
                        pnlDoSearch.Visible = False
                        pnlAddAge.Visible = True

                        'fill the label
                        lblAge.Text = "Please enter your age to add your ticket. You will add other customers next."

                        gvCustomers.Visible = False

                        Session.Add("FirstTicket", "Yes")

                        Exit Sub


                    End If

                Next
            End If

        End If

        If IsPostBack = False Then
            CustomerDB.GetAllActiveCustomersCloneUsingSP()

            SortAndBind()
        End If

        If Session("UserType").ToString = "Customer" Then
            'this customer must be added to the reservatoin

        End If

        If Session("NewCustomer") Is Nothing Then
            'do nothing

        Else

            'get the new customer's information
            CustomerDB.GetNewestCustomer()

            'change what is visible
            pnlDoSearch.Visible = False
            pnlAddAge.Visible = True

            'fill the label
            lblAge.Text = "Please enter " & CustomerDB.MyDataset.Tables("tblCustomersClone").Rows(0).Item("FirstName") & " " & _
            CustomerDB.MyDataset.Tables("tblCustomersClone").Rows(0).Item("LastName") & "'s age."

            gvCustomers.Visible = False

            gvCustomers.SelectedIndex = gvCustomers.Rows.Count - 1

            Session.Remove("NewCustomer")

        End If

        ''Author: Aaryaman Singhal
        'Date: 02/06/2014
        'Description: A sub that gets all customers, sorts them, and then puts them on the form 


    End Sub

    Private Sub SortAndBind()
        gvCustomers.DataSource = CustomerDB.MyView
        gvCustomers.DataBind()

        If gvCustomers.Rows.Count = 0 Then
            'show all records and throw them an error
            CustomerDB.GetAllCustomersCloneUsingSP()

            SortAndBind()

            lblMessage.Text = "Your search returned no records. Please try a different search."
        End If

        HideGridViewColumn(2)
        HideGridViewColumn(6)
        HideGridViewColumn(7)
        HideGridViewColumn(9)
        HideGridViewColumn(10)
        HideGridViewColumn(11)

    End Sub

    Public Sub HideGridViewColumn(intColumn As Integer)
        gvCustomers.HeaderRow.Cells(intColumn).Visible = False
        For Each gvr As GridViewRow In gvCustomers.Rows
            gvr.Cells(intColumn).Visible = False
        Next
    End Sub


    Protected Sub gvCustomers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvCustomers.SelectedIndexChanged
        ClearMessages()
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
            DBTicket.AddTicket(Session("ReservationID").ToString, strAdvantageNumber, strJourneyID, strFlightNumber, strBaseFare, intAge.ToString)

        Next

        'make the gv visible again
        gvCustomers.Visible = True

        'change which panels are visible
        pnlDoSearch.Visible = True
        pnlAddAge.Visible = False

        'clear text box
        txtAge.Text = ""

        If Session("Adults") = Session("Children") And Session("Adults") = Session("Babies") And Session("Adults") = 0 Then
            'remove session variables and redirect
            Session.Remove("Adults")
            Session.Remove("Children")
            Session.Remove("Babies")
            Session.Remove("FirstTicket")
            If Session("UserType") = "Customer" Then
                Session.Add("Login", Session("UserID").ToString)
            Else
                Session.Add("Login", strAdvantageNumber)
            End If

            If Session("Zip") Is Nothing Then
                'dont do anything 
            Else
                Session.Remove("Address")
                Session.Remove("City")
                Session.Remove("State")
                Session.Remove("Zip")
                Session.Remove("LastName")
                Session.Remove("Phone")
            End If
            Response.Redirect("Res_SeatSelection.aspx")
        End If

    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        'check to make sure a search type is selected
        If txtSearch.Text = "" Then
            lblMessage.Text = "Please enter a search term."
            Exit Sub
        End If

        If rblSearchType.SelectedIndex = -1 Then
            lblMessage.Text = "Please select a search type."
            Exit Sub
        End If
        CustomerDB.SearchCustomerClone(rblSearchType.SelectedIndex, rblSearchBy.SelectedIndex, txtSearch.Text)
    End Sub

    Protected Sub rblSearchBy_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblSearchBy.SelectedIndexChanged
        ClearMessages()
        If rblSearchBy.SelectedIndex = 1 Then
            'make partial/keyword invisible
            rblSearchType.Visible = False
        End If
        If rblSearchBy.SelectedIndex = 0 Then
            'make partial/keyword visible
            rblSearchType.Visible = True
        End If
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        DBCancel.CancelReservation(Session("ReservationID").ToString)
        DBCancel.DeactivateTickets(Session("ReservationID").ToString)

        Session.Remove("Adults")
        Session.Remove("Children")
        Session.Remove("Babies")
        Session.Remove("ReservationID")
        If Session("NewCustomer") Is Nothing Then
            'dont do anything
        Else
            Session.Remove("NewCustomer")
        End If
        Response.Redirect("Cust_CreateReservationAndSelectFlight.aspx")
    End Sub

    Protected Sub btnAddCustomer_Click(sender As Object, e As EventArgs) Handles btnAddCustomer.Click
        Response.Redirect("Res_CreateCustProfile.aspx")
    End Sub

    Public Sub ClearMessages()
        lblMessage.Text = ""
        lblAgeMessage.Text = ""
        lblAge.Text = ""
        txtSearch.Text = ""
        txtAge.Text = ""
    End Sub
End Class
