'Author: Ben Shadburne
'Assignment: ASP4
'Date: 02/06/2014
'Description: handles database based queries and sets up the string and connection



Option Strict On
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Public Class DBjourneyclone
    'setting up db, dim connection, adapter, query, dataset
    Dim mMyView As New DataView
    Dim mMyViewSeats As New DataView
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size=4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False;initial catalog=mis333k_20142_Team06;user id=msbcf819;password=Databasepassword5"
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdatasetjourneyclone As New DataSet
    Dim mdatasetjourneycloneSeats As New DataSet
    Dim mQueryString As String

    Dim DBJourneySeats As New DBJourneySeats
    Dim DBFlightSearch As New DBFlightSearch

    Public Sub GetALLjourneycloneUsingSP()
        'Author: Ben Shadburne
        'Purpose: runs journeyclone procedure
        'Arguments: na
        'Return: na
        'Date: 03/18/2014

        RunProcedure("usp_journeyclone_get_all")
    End Sub

    'define a public read only property
    Public ReadOnly Property MyView() As DataView
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: journeyclone dataview
        'Date: 03/18/2014

        Get
            Return mMyView
        End Get
    End Property

    Public ReadOnly Property MyDataSet() As DataSet
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: journeyclone dataview
        'Date: 03/18/2014

        Get
            Return mdatasetjourneyclone
        End Get
    End Property

    Public Sub GetOneJourney(intJourneyID As Integer)
        RunSPwithOneParam("usp_JourneyClone_Get_One", "@JourneyID", intJourneyID.ToString)
    End Sub

    Public Sub RunProcedure(ByVal strName As String)
        'Author: Ben Shadburne
        'Purpose: runs procedure
        'Arguments: procedure name
        'Return: na
        'Date: na

        'create instances of the conneciton and command object
        Dim objConnection As New SqlConnection(mstrConnection)
        'tell sql server the name of the stored procedure you will be executing
        Dim mdbDataAdapter As New SqlDataAdapter(strName, objConnection)
        Try
            'sets the command type to "stored procedure"
            mdbDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            'clear dataset
            Me.mdatasetjourneyclone.Clear()
            'open conneciton and fill dataset
            mdbDataAdapter.Fill(mdatasetjourneyclone, "tblJourneyClone")
            'copy dataset to dataview
            mMyView.Table = mdatasetjourneyclone.Tables("tblJourneyClone")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strName.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub RunSPwithOneParam(ByVal strSPName As String, ByVal strParamName As String, ByVal strParamValue As String)
        ' purpose to run a stored procedure with one parameter
        ' inputs:  stored procedure name, parameter name, parameter value
        ' returns: dataset filled with correct records

        ' CREATES INSTANCES OF THE CONNECTION AND COMMAND OBJECT
        Dim objConnection As New SqlConnection(mstrConnection)
        ' Tell SQL server the name of the stored procedure you will be executing
        Dim mdbDataAdapter As New SqlDataAdapter(strSPName, objConnection)
        Try
            ' SETS THE COMMAND TYPE TO "STORED PROCEDURE"
            mdbDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure

            ' ADD PARAMETER(S) TO SPROC
            mdbDataAdapter.SelectCommand.Parameters.Add(New SqlParameter(strParamName, strParamValue))
            ' clear dataset
            mdatasetjourneyclone.Clear()

            ' OPEN CONNECTION AND FILL DATASET
            mdbDataAdapter.Fill(mdatasetjourneyclone, "tblJourneys")

            ' copy dataset to dataview
            mMyView.Table = mdatasetjourneyclone.Tables("tblJourneys")

        Catch ex As Exception
            Throw New Exception("params are " & strSPName.ToString & " " & strParamName.ToString & " " & strParamValue.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Protected Sub UseSPforInsertOrUpdateQuery(ByVal strUSPName As String, ByVal aryParamNames As ArrayList, ByVal aryParamValues As ArrayList)
        'Purpose: Sort the dataview by the argument (general sub)
        'Arguments: Stored procedure name, Arraylist of parameter names, and  arraylist of parameter values
        'Returns: Nothing
        'Author: Rick Byars
        'Date: 4/03/12

        'Creates instances of the connection and command object
        Dim objConnection As New SqlConnection(mstrConnection)
        'Tell SQL server the name of the stored procedure
        Dim objCommand As New SqlDataAdapter(strUSPName, objConnection)
        Try
            'Sets the command type to stored procedure
            objCommand.SelectCommand.CommandType = CommandType.StoredProcedure

            'Add parameters to stored procedure
            Dim index As Integer = 0
            For Each paramName As String In aryParamNames
                objCommand.SelectCommand.Parameters.Add(New SqlParameter(CStr(aryParamNames(index)), CStr(aryParamValues(index))))
                index = index + 1
            Next

            ' OPEN CONNECTION AND RUN INSERT/UPDATE QUERY
            objCommand.SelectCommand.Connection = objConnection
            objConnection.Open()
            objCommand.SelectCommand.ExecuteNonQuery()
            objConnection.Close()

            'Print out each element of our arraylists if error occured
        Catch ex As Exception
            Dim strError As String = ""
            Dim index As Integer = 0
            For Each paramName As String In aryParamNames
                strError = strError & "ParamName: " & CStr(aryParamNames(index))
                strError = strError & " ParamValue: " & CStr(aryParamValues(index))
                index = index + 1
            Next
            Throw New Exception(strError & " error message is " & ex.Message)
        End Try
    End Sub

    Protected Sub UseSPToRetrieveRecords(ByVal strUSPName As String, ByVal strDatasetName As DataSet, ByVal strViewName As DataView, ByVal strTableName As String, ByVal aryParamNames As ArrayList, ByVal aryParamValues As ArrayList)
        'Purpose: Run any stored procedure with any number of parameters
        'Arguments: Stored procedure name, tblName, dataset name, dataview name, arraylist of parameter names, and arraylist of parameter values
        'Returns: Nothing
        'Author: Rick Byars
        'Date: 4/16/10
        'Creates instances of the connection and command object
        Dim objConnection As New SqlConnection(mstrConnection)
        'Tell SQL server the name of the stored procedure
        Dim objCommand As New SqlDataAdapter(strUSPName, objConnection)
        Try
            'Sets the command type to stored procedure
            objCommand.SelectCommand.CommandType = CommandType.StoredProcedure

            'Add parameters to stored procedure
            Dim index As Integer = 0
            For Each paramName As String In aryParamNames
                objCommand.SelectCommand.Parameters.Add(New SqlParameter(CStr(aryParamNames(index)), CStr(aryParamValues(index))))
                index = index + 1
            Next

            'Clear dataset
            strDatasetName.Clear()

            'Open the connection and fill dataset
            objCommand.Fill(strDatasetName, strTableName)
            ' fill view
            strViewName.Table = strDatasetName.Tables(strTableName)

            'Print out each element of our arraylists if error occured
        Catch ex As Exception
            Dim strError As String = ""
            Dim index As Integer = 0
            For Each paramName As String In aryParamNames
                strError = strError & "ParamName: " & CStr(aryParamNames(index))
                strError = strError & " ParamValue: " & CStr(aryParamValues(index))
                index = index + 1
            Next
            Throw New Exception(strError & " error message is " & ex.Message)
        End Try
    End Sub


    Public Sub AddNewJourney(strUSPName As String, intFlightNumber As Integer, datSelectedDate As Date, intDepartureTime As Integer, intArrivalTime As Integer, strDay As String)
        'defines array to put parameter names into
        Dim aryParamNames As New ArrayList
        Dim aryParamValues As New ArrayList

        'add parameter names to names array list
        aryParamNames.Add("@FlightNumber")
        aryParamNames.Add("@FlightDate")
        aryParamNames.Add("@DepartureTime")
        aryParamNames.Add("@ArrivalTime")
        aryParamNames.Add("@NameOfDay")


        'add values to parameter values array list
        aryParamValues.Add(intFlightNumber)
        aryParamValues.Add(datSelectedDate)
        aryParamValues.Add(intDepartureTime)
        aryParamValues.Add(intArrivalTime)
        aryParamValues.Add(strDay)


        'run the sp to add a journey
        UseSPforInsertOrUpdateQuery(strUSPName, aryParamNames, aryParamValues)
    End Sub

    Public Function GetDateByJourney(strJourneyID As String, strTicketID As String) As String
        'defines array to put parameter names into
        Dim aryParamNames As New ArrayList
        Dim aryParamValues As New ArrayList

        'add parameter names to names array list
        aryParamNames.Add("@JourneyID")
        aryParamNames.Add("@TicketID")


        'add values to parameter values array list
        aryParamValues.Add(strJourneyID)
        aryParamValues.Add(strTicketID)

        UseSPToRetrieveRecords("usp_Journeys_Get_Date", mdatasetjourneyclone, mMyView, "tblDate", aryParamNames, aryParamValues)

        Return mdatasetjourneyclone.Tables("tblDate").Rows(0).Item("FlightDate").ToString
    End Function

    Public Sub GetJourneysByDate(ByVal strUSPName As String, ByVal strTableName As String, ByVal strDayOfWeek As String, ByVal datSelectedDate As Date)

        'defines array to put parameter names into
        Dim aryParamNames As New ArrayList
        Dim aryParamValues As New ArrayList

        'add parameter names to names array list
        aryParamNames.Add("@DayOfWeek")
        aryParamNames.Add("@DATE")


        'add values to parameter values array list
        aryParamValues.Add(strDayOfWeek)
        aryParamValues.Add(datSelectedDate)

        UseSPToRetrieveRecords(strUSPName, MyDataSet, MyView, strTableName, aryParamNames, aryParamValues)

    End Sub



    Public Sub CheckWhichJourneysToAdd(FlightsAdded As DataSet, FlightsNeeded As DataSet, datSelectedDate As Date, strDay As String)
        'define counter variables
        Dim i As Integer
        Dim j As Integer

        'define a boolean to see if we should add a journey
        Dim bolAddJourney As Boolean = True
        'define a variable to hold the flight number we are running through a loop
        Dim intFlightNumber As Integer

        'J stores the number of times we need to loop through the flights that must be added if they aren't in it already. 
        Try
            j = FlightsNeeded.Tables("tblFlightsClone").Rows.Count - 1
        Catch ex As Exception
            Exit Sub
        End Try

        'i stores the number of times we need to loop through the flights that are already added
        i = FlightsAdded.Tables("tblJourneyClone").Rows.Count - 1

        'l is the counter variable for this loop
        For k = 0 To j
            'Remember the flight number we are considering adding
            intFlightNumber = CInt(FlightsNeeded.Tables("tblFlightsClone").Rows(k).Item("FlightNumber"))

            'make the boolean true. If it stays true through the next loop, we will add a record
            bolAddJourney = True

            'this loop compares the flight we are trying to add to the flights already in the DB for that day. 
            For m = 0 To i
                If intFlightNumber <> CInt(FlightsAdded.Tables("tblJourneyClone").Rows(m).Item("FlightNumber")) Then
                    'that flight number is not in the database yet. we may want to add it to the database. Begin loop again to check next flight number
                Else
                    'this flight is already in the database. We will not want to add it to the database. 
                    bolAddJourney = False
                    Exit For
                End If
            Next

            'if the boolean isn't changed, add a new flight to the database
            If bolAddJourney = True Then
                AddNewJourney("usp_JourneyClone_Add_New", intFlightNumber, datSelectedDate, CInt(FlightsNeeded.Tables("tblFlightsClone").Rows(k).Item("DepartureTime")), CInt(FlightsNeeded.Tables("tblFlightsClone").Rows(k).Item("ArrivalTime")), strDay)

                'also add unfilled seats to the JourneySeat bridge table in that case
                'need the journeyID of the journey just added first

                DBJourneySeats.AddSeats(intFlightNumber.ToString, DBFlightSearch.AlterDate(datSelectedDate.ToShortDateString))
            End If

        Next

    End Sub

    Public Sub CheckJourneyDays(strFlightNumber As String, strDayofWeek As String)
        Dim aryParamNames As New ArrayList
        Dim aryParamValues As New ArrayList

        aryParamNames.Add("@flightnumber")
        aryParamNames.Add("@dayofweek")

        aryParamValues.Add(strFlightNumber)
        aryParamValues.Add(strDayofWeek)

        UseSPToRetrieveRecords("usp_JourneysClone_Find_By_Day", MyDataSet, mMyView, "tblJourneysClone", aryParamNames, aryParamValues)
    End Sub

    Public Sub InactivateJourneyWithDay(strDay As String, strFlightNumber As String)
        Dim aryParamNames As New ArrayList
        Dim aryParamValues As New ArrayList

        aryParamNames.Add("@dayofweek")
        aryParamNames.Add("@flightnumber")

        aryParamValues.Add(strDay)
        aryParamValues.Add(strFlightNumber)


        UseSPforInsertOrUpdateQuery("usp_JourneysClone_Modify_Journey", aryParamNames, aryParamValues)
    End Sub

    Public Sub InactivateJourneyWithoutDay(strFlightNumber As String)
        RunSPwithOneParam("usp_JourneysClone_Modify_Journey_Without_Day", "@flightnumber", strFlightNumber)
    End Sub

End Class
