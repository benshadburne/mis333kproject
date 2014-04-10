'Author: Ben Shadburne
'Assignment: ASP4
'Date: 02/06/2014
'Description: handles database based queries and sets up the string and connection





'decently blank DB class
'when making a new db class, just copy this one


Option Strict On
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Public Class DBjourneyclone
    'setting up db, dim connection, adapter, query, dataset
    Dim mMyView As New DataView
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size=4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False;initial catalog=mis333k_20142_Team06;user id=msbcf819;password=Databasepassword5"
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdatasetjourneyclone As New DataSet
    Dim mQueryString As String

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


    Public Sub AddNewJourney(strUSPName As String, intFlightNumber As Integer, datSelectedDate As Date, intDepartureTime As Integer)
        'defines array to put parameter names into
        Dim aryParamNames As New ArrayList
        Dim aryParamValues As New ArrayList

        'add parameter names to names array list
        aryParamNames.Add("@FlightNumber")
        aryParamNames.Add("@FlightDate")
        aryParamNames.Add("@DepartureTime")


        'add values to parameter values array list
        aryParamValues.Add(intFlightNumber)
        aryParamValues.Add(datSelectedDate)
        aryParamValues.Add(intDepartureTime)

        'run the sp to add a journey
        UseSPforInsertOrUpdateQuery(strUSPName, aryParamNames, aryParamValues)
    End Sub

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

    Public Sub CheckWhichJourneysToAdd(FlightsAdded As DataSet, FlightsNeeded As DataSet, intFlightNumber As Integer)
        'define a counter variable
        Dim i As Integer
        'define a boolean to see if we should add a journey
        Dim bolAddJourney As Boolean = True

        'For intFlightNumber = FlightsAdded.Tables("table

        i = FlightsNeeded.Tables("tblFlightsClone").Rows.Count - 1

        For i = 0 To i
            If intFlightNumber <> CInt(FlightsNeeded.Tables("tblFlightClone").Rows(i).Item("FlightNumber")) Then
                'that flight number is not in the database yet. we may want to add it to the database 
            Else
                bolAddJourney = False
            End If
        Next

        If bolAddJourney = True Then
            'AddNewJourney(intFlightNumber, )
        End If

    End Sub


End Class
