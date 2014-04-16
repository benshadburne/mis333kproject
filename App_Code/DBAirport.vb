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
Public Class DBairportclone
    'setting up db, dim connection, adapter, query, dataset
    Dim mMyView As New DataView
    Dim mMyViewArrival As New DataView
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size=4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False;initial catalog=mis333k_20142_Team06;user id=msbcf819;password=Databasepassword5"
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdatasetairportclone As New DataSet
    Dim mdatasetairportcloneArrival As New DataSet
    Dim mQueryString As String

    Public Sub GetALLairportcloneUsingSP()
        'Author: Ben Shadburne
        'Purpose: runs airportclone procedure
        'Arguments: na
        'Return: na
        'Date: 03/18/2014

        RunProcedureDeparture("usp_AirportClone_get_all")

    End Sub

    Public Sub GetTwoairportcloneUsingSP()
        'Author: Aaryaman Singhal
        'Purpose: runs airportclone get all twice -- one for each DDL
        'Arguments: na
        'Return: na
        'Date: 04/162014

        RunProcedureDeparture("usp_AirportClone_get_all")
        RunProcedureArrival("usp_AirportClone_get_all")

    End Sub

    'define a public read only property
    Public ReadOnly Property MyView() As DataView
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: airportclone dataview
        'Date: 03/18/2014

        Get
            Return mMyView
        End Get
    End Property

    Public ReadOnly Property MyDataSet() As DataSet
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: airportclone dataview
        'Date: 03/18/2014

        Get
            Return mdatasetairportclone
        End Get
    End Property

    Public ReadOnly Property MyViewarrival() As DataView
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: airportclone dataview
        'Date: 03/18/2014

        Get
            Return mMyViewArrival
        End Get
    End Property

    Public ReadOnly Property MyDataSetArrival() As DataSet
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: airportclone dataview
        'Date: 03/18/2014

        Get
            Return mdatasetairportcloneArrival
        End Get
    End Property


    Public Sub RunProcedureDeparture(ByVal strName As String)
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
            Me.mdatasetairportclone.Clear()
            'open conneciton and fill dataset
            mdbDataAdapter.Fill(mdatasetairportclone, "tblAirportClone")
            'copy dataset to dataview
            mMyView.Table = mdatasetairportclone.Tables("tblAirportClone")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strName.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub RunProcedureArrival(ByVal strName As String)
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
            Me.mdatasetairportcloneArrival.Clear()
            'open conneciton and fill dataset
            mdbDataAdapter.Fill(mdatasetairportcloneArrival, "tblAirportClone")
            'copy dataset to dataview
            mMyView.Table = mdatasetairportcloneArrival.Tables("tblAirportClone")
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


    Public Sub AddAirport(strSPName As String, strAirportCode As String, strCityName As String)
        'defines array to put parameter names into
        Dim aryParamNames As New ArrayList
        Dim aryParamValues As New ArrayList

        'add parameter names to names array list
        aryParamNames.Add("@AirportCode")
        aryParamNames.Add("@CityName")


        'add values to parameter values array list
        aryParamValues.Add(strAirportCode)
        aryParamValues.Add(strCityName)

        UseSPforInsertOrUpdateQuery(strSPName, aryParamNames, aryParamValues)
    End Sub

    Public Sub AddMileageAndFlightTime(strSPName As String, strStartAirport As String, strEndAirport As String, intMileage As Integer, intFlightTime As Integer)
        'defines array to put parameter names into
        Dim aryParamNames As New ArrayList
        Dim aryParamValues As New ArrayList

        'add parameter names to array list
        aryParamNames.Add("@StartAirport")
        aryParamNames.Add("@EndAirport")
        aryParamNames.Add("@Mileage")
        aryParamNames.Add("FlightTime")

        'add values to array list
        aryParamValues.Add(strStartAirport)
        aryParamValues.Add(strEndAirport)
        aryParamValues.Add(intMileage)
        aryParamValues.Add(intFlightTime)

        'add records
        UseSPforInsertOrUpdateQuery(strSPName, aryParamNames, aryParamValues)
    End Sub

End Class
