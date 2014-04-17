Imports Microsoft.VisualBasic

Public Class AddJourneyClass

    Dim DBFlights As New DBFlightsClone
    Dim DBJourney As New DBjourneyclone
    Public Sub AddJourney(ByVal strDay As String, ByVal strDate As String)

        'This fills the DBFlights.MyDataSet and DBFlights.MyView with the Flights the Journey table should have in it for the given date
        DBFlights.CheckFlightsNeededForSpecificDate("usp_FlightClone_Need_Journeys", "@DayOfWeek", strDay)

        'this fills the DB Journey Dataset and DB Journey Dataview with journeys that have already been created for the specific day
        DBJourney.GetJourneysByDate("usp_JourneyClone_Choose_Active_By_Day", "tblJourneyClone", strDay, strDate)

        'Run code to make sure we don't add duplicate journeys. Then add required journeys. 
        DBJourney.CheckWhichJourneysToAdd(DBJourney.MyDataSet, DBFlights.MyDataSet, strDate, strDay)

    End Sub
End Class
