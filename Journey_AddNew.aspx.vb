
Partial Class Journey_AddNew

    Inherits System.Web.UI.Page
    Dim DBFlights As New DBFlightsClone
    Dim DBJourney As New DBjourneyclone

    Private Sub LoadDDL()
        DBFlights.GetALLFlightsCloneUsingSP()

        ddlFlights.DataSource = DBFlights.MyDataSet

        ddlFlights.DataTextField = "FlightNumber"
        ddlFlights.DataBind()

    End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        'define variables
        Dim intRow As Integer
        Dim strDay As String

        'intRow keeps track of selected value of ddl
        intRow = ddlFlights.SelectedIndex

        'put the name of the day of the week into strDay
        strDay = WeekdayName(Weekday(calDate.SelectedDate))
        'the textbox shows the day in strDay -- I just did this to double check that it was working properly
        TextBox1.Text = strDay

        'This fills the DBFlights.MyDataSet and DBFlights.MyView with the Flights the Journey table should have in it for the given date
        DBFlights.CheckFlightsNeededForSpecificDate("usp_FlightClone_Need_Journeys", "@DayOfWeek", strDay)

        'this fills the DB Journey Dataset and DB Journey Dataview with journeys that have already been created for the specific day
        DBJourney.GetJourneysByDate("usp_JourneyClone_Choose_Active_By_Day", "tblJourneyClone", strDay, calDate.SelectedDate)

        ''NEED TO RUN SOME CODE HERE TO COMPARE THE TWO DATASET/DATAVIEWS to make sure we don't add duplicate journeys. SHOULDN't BE TOO HARD
        DBJourney.CheckWhichJourneysToAdd(DBJourney.MyDataSet, DBFlights.MyDataSet, calDate.SelectedDate)

        'DBJourney.AddNewJourney("usp_JourneyClone_Add_New", CInt(DBFlights.MyDataSet.Tables("tblFlightClone").Rows(intRow).Item("FlightNumber")), calDate.SelectedDate, CInt(DBFlights.MyDataSet.Tables("tblFlightClone").Rows(intRow).Item("DepartureTime")))




    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsPostBack = False Then
            LoadDDL()
        End If
    End Sub

   
End Class
