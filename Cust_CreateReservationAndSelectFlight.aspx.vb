﻿
Partial Class Cust_CreateReservationAndSelectFlight
    Inherits System.Web.UI.Page
    Dim DBAirport As New DBairportclone

    'CREATING SOME NOTES SO THAT I CAN WRAP MY HEAD AROUND WHAT THE USER WILL NEED TO BE ABLE TO DO ON THIS PAGE

    'the way our DB works, we need to add all reservation information (every journey) before we add specific customers to the reservation
    'So... first add all the journeys, then add all the customers, then add all the seats for all the customers. 

    'SELECTING ALL THE JOURNEYS
    'I like the way you laid out the page, I HAVE HOOKED UP ALL THE DDLS TO AUTO-POPULATE WITH CITY NAMES
    'maybe after they choose each leg, we can have the user choose the flights they want. 
    'to let them choose a flight, we will need the number of customers (must know babies and non-babies to know what flights have enough seats available for users to select)
    'keep the datetime of that flight in a session variable. Keep the end airport in a stored procedure. 

    'user now clicks back to reservation page and number of customers cannot be changed. 
    'the DDL should show the airport at which the user's last flight ended. 

    'The user selects their next destination and is redirected to flight search page where they choose their next flight. 
    'we validate to make sure the flights they choose are after the initial flight.

    'we bring them back to the reservation - add leg page.

    'keep doing this until their journey is complete.

    'At all stages, I think they should be able to cancel their reservation and start over OR finish their reservation and modify it.

    'then we can have them choose the people that will be flying. Once they make this choice --> add tickets for all customers on all journeys

    'then have customers choose seats --> redirect them to that page with appropriate session variables

    'then we can bring them back to this page and have them do it again. 

    'user will select first leg of journey. A to B. -- should the user have to create their flight at this point? 
    'how will we know which advantage numbers to add to the flight. 

    Protected Sub btnAddJourney_Click(sender As Object, e As EventArgs) Handles btnAddJourney.Click

        'run all the validations
        'check to make sure children + adults is 16 or less
        If ddlAdult.SelectedValue + ddlChildren.SelectedValue > 16 Then
            lblMessage.Text = "You can have a maximum of 16 people over age 2 in your reservation."
        End If

        'check to make sure adults > babies
        If ddlBabies.SelectedValue > ddlAdult.SelectedValue Then
            lblMessage.Text = "You cannot have more babies than adults in your reservation"
        End If

        Session.Add("Adults", ddlAdult.SelectedValue)
        Session.Add("Children", ddlChildren.SelectedValue)
        Session.Add("Babies", ddlBabies.SelectedValue)

        'if this is the first add journey then do these things
        If Session("JourneyNumber") Is Nothing Then
            Session.Add("JourneyNumber", CInt(0))
            Session.Add("TripType", rblTrip.SelectedValue.ToString)
        End If

        CreateSessionVariablesAndRedirect()

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If IsPostBack = False Then
            LoadDDL()
        End If

        If Session("StartAirport") Is Nothing Then
            'don't do anything
        Else
            'set that as the start airport
            ddlDepartureCity.Items.FindByValue(Session("StartAirport")).Selected = True
            ddlDepartureCity.Enabled = False
        End If

        If Session("TripType") Is Nothing Then
            'dont do anything
        Else
            'hide the radio button list and drop downs for number of customers
            rblTrip.Visible = False
            lblAdult.Visible = False
            lblBabies.Visible = False
            lblChildren.Visible = False
            ddlAdult.Visible = False
            ddlChildren.Visible = False
            ddlBabies.Visible = False

            'make the add as final leg button visisble
            btnFinalLeg.Visible = True

        End If



    End Sub

    Private Sub LoadDDL()

        DBAirport.GetTwoairportcloneUsingSP()

        ddlDepartureCity.DataSource = DBAirport.MyDataSet
        ddlDepartureCity.DataTextField = "CityName"
        ddlDepartureCity.DataValueField = "AirportCode"
        ddlDepartureCity.DataBind()

        ddlArrivalCity.DataSource = DBAirport.MyDataSetArrival
        ddlArrivalCity.DataTextField = "CityName"
        ddlArrivalCity.DataValueField = "AirportCode"
        ddlArrivalCity.DataBind()

    End Sub

    Protected Sub btnFinalLeg_Click(sender As Object, e As EventArgs) Handles btnFinalLeg.Click

        'create a session variable to let the cust flight search page know that this is the final journey to add
        Session.Add("IsFinal", "yes")

        'get start and end airport and redirect
        CreateSessionVariablesAndRedirect()

    End Sub

    Private Sub CreateSessionVariablesAndRedirect()
        Session.Add("StartAirport", ddlDepartureCity.SelectedValue.ToString)
        Session.Add("EndAirport", ddlArrivalCity.SelectedValue.ToString)

        Response.Redirect("Cust_FlightSearch.aspx")
    End Sub
End Class
