
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

    Protected Sub btnCreateReservation_Click(sender As Object, e As EventArgs) Handles btnCreateReservation.Click
        'send the user to a page where they can choose the customers on the reservation and add tickets
    End Sub

    Protected Sub btnAddJourney_Click(sender As Object, e As EventArgs) Handles btnAddJourney.Click
        'redirect the user to the flight search page with session variables for start and end airport.


        Session.Add("StartAiport", ddlDepartureCity.SelectedValue)
        Session.Add("EndAirport", ddlArrivalCity.SelectedValue)

        Label4.Text = Session("StartAirport")
        Label5.Text = Session("EndAirport")

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If IsPostBack = False Then
            LoadDDL()
        End If
    End Sub

    Private Sub LoadDDL()

        DBAirport.GetALLairportcloneUsingSP()

        ddlDepartureCity.DataSource = DBAirport.MyDataSet1
        ddlDepartureCity.DataTextField = "CityName"
        ddlDepartureCity.DataValueField = "AirportCode"
        ddlDepartureCity.DataBind()

        ddlArrivalCity.DataSource = DBAirport.MyDataSet2
        ddlArrivalCity.DataTextField = "CityName"
        ddlArrivalCity.DataValueField = "AirportCode"
        ddlArrivalCity.DataBind()



    End Sub

End Class
