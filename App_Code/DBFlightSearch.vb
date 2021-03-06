﻿'Author: Ben Shadburne
'Assignment: ASP4
'Date: 02/06/2014
'Description: handles database based queries and sets up the string and connection


Option Strict On
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Public Class DBFlightSearch
    'setting up db, dim connection, adapter, query, dataset
    Dim mMyView As New DataView
    Dim mMyViewStart As New DataView
    Dim mMyViewFinish As New DataView
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size=4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False;initial catalog=mis333k_20142_Team06;user id=msbcf819;password=Databasepassword5"
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdatasetFlightSearch As New DataSet
    Dim mdatasetIndirectStart As New DataSet
    Dim mdatasetIndirectFinish As New DataSet
    Dim mQueryString As String
    Dim mstrFilterStatement As String


    'IMPORTANT SORTING NOTE! TYPE 1 = REGULAR, TYPE 2 = START, TYPE 3 = FINISH



    Public Sub GetALLFlightSearchUsingSP()
        'Author: Ben Shadburne
        'Purpose: runs FlightSearch procedure
        'Arguments: na
        'Return: na
        'Date: 03/18/2014


        'this is in clone version for customers and general
        RunProcedure("usp_Flightsearchclone_Get_PresentFuture")


    End Sub

    Public Sub GetALLFlightSearchUsingSPEmployee()
        'Author: Ben Shadburne
        'Purpose: runs FlightSearch procedure
        'Arguments: na
        'Return: na
        'Date: 03/18/2014


        'this is in clone version for customers and general
        RunProcedure("usp_Flightsearchclonse_Get_All_Employee")


    End Sub

    Public Sub GetAllJourneysSeatsEmpty(intSeats As Integer)
        'this factors in number of available seats
        Dim aryParamName As New ArrayList
        Dim aryParamValue As New ArrayList

        aryParamName.Add("@Seats")
        aryParamValue.Add(intSeats)

        UseSPToRetrieveRecords("usp_FlightSearch_EmptySeats", mdatasetFlightSearch, mMyView, "tblJourneysEmptySeats", aryParamName, aryParamValue)
        UseSPToRetrieveRecords("usp_IndirectJourney_Get_PresentFuture", mdatasetIndirectStart, mMyViewStart, "tblJourneysEmptySeats", aryParamName, aryParamValue)
        UseSPToRetrieveRecords("usp_IndirectJourney_Get_PresentFuture", mdatasetIndirectFinish, mMyViewFinish, "tblJourneysEmptySeats", aryParamName, aryParamValue)

        MyViewStart.RowFilter = "Departed = 'N'"
        MyView.RowFilter = "Departed = 'N'"
        MyViewFinish.RowFilter = "Departed = 'N'"

        aryParamName = Nothing
        aryParamValue = Nothing
    End Sub


    'define a public read only property
    Public ReadOnly Property MyView() As DataView
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: FlightSearch dataview
        'Date: 03/18/2014

        Get
            Return mMyView
        End Get
    End Property

    Public ReadOnly Property MyDataSet() As DataSet
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: FlightSearch dataview
        'Date: 03/18/2014

        Get
            Return mdatasetFlightSearch
        End Get
    End Property

    Public Sub DoSort()
        'Author: Aaryaman Singhal
        'Purpose: sorts data by departure time
        'Arguments: none
        'Return: sorted dataview
        'Date: 04/17/2014

        'this is for regular
        MyView.Sort = "[Flight Date], [Departure Time]"
        MyViewStart.Sort = "[Flight Date], [Departure Time]"
        MyViewFinish.Sort = "[Flight Date], [Departure Time]"
    End Sub

    Public Sub SearchDirect(strDeparture As String, strEnd As String, strDate As String)
        'everything we'll need for this
        MyView.RowFilter = "[Departure City] = '" & strDeparture & "' AND [End City] = '" & strEnd & "' AND [Flight Date] = '" & strDate & "' AND Departed = 'N'"
        'for departure time ( AND [Departure Time] >= '" & intTime & "')
    End Sub

    Public Sub SearchIndirectStart(strDeparture As String, strEnd As String, strDate As String)
        'everything we'll need for this
        MyViewStart.RowFilter = "[Departure City] = '" & strDeparture & "' AND [End City] <> '" & strEnd & "' AND [Flight Date] = '" & strDate & "' AND Departed = 'N'"
    End Sub

    Public Sub SearchIndirectFinish(strDeparture As String, strEnd As String, strDate As String, strTime As String)
        'indirect flights have to happen all in one day, or not at all
        MyViewFinish.RowFilter = "[Departure City] = '" & strDeparture & "' AND [End City] = '" & strEnd & "' AND [Flight Date] = '" _
         & strDate & "' AND [Departure Time] > '" & strTime & "' And Departed = 'N'"

    End Sub

    Public Function SearchTime(ByVal strIn As String) As String
        'Author: Ben Shadburne
        'Purpose: search by partial city
        'Arguments:  search text
        'Return: filtered dataview by partial city
        'Date: 03/18/2014

        Return "AND 'Departure Time' >= '" & strIn & "'"

    End Function

    Public Function AlterDate(ByVal strIn As String) As String
        'Author: Ben Shadburne
        'Purpose: search by partial lastname
        'Arguments:  search text
        'Return: filtered dataview by partial lastname
        'Date: 03/18/2014

        'need to decypher if days and months are len 2
        Dim strMonth As String
        Dim strDay As String

        'makes strday and strmonth into len2 strings of their respective values
        If strIn.Substring(1, 1) = "/" Then
            strMonth = "0" & strIn.Substring(0, 1)
            If strIn.Substring(3, 1) = "/" Then
                strDay = "0" & strIn.Substring(2, 1)
            Else
                strDay = strIn.Substring(2, 2)
            End If
        Else
            strMonth = strIn.Substring(0, 2)
            If strIn.Substring(4, 1) = "/" Then
                strDay = "0" & strIn.Substring(3, 1)
            Else
                strDay = strIn.Substring(3, 2)
            End If
        End If

        Return strIn.Substring(Len(strIn) - 4, 4) & "-" & strMonth & "-" & strDay

    End Function

    Public ReadOnly Property lblCount() As Integer
        'Author: Ben Shadburne
        'Purpose: return lblcount
        'Arguments:  none
        'Return: count of FlightSearch
        'Date: 03/07/2014

        Get
            'returns the count to the label
            Return MyView.Count
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
            Me.mdatasetFlightSearch.Clear()
            'open conneciton and fill dataset
            mdbDataAdapter.Fill(mdatasetFlightSearch, "tblFlightSearch")
            'copy dataset to dataview
            mMyView.Table = mdatasetFlightSearch.Tables("tblFlightSearch")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strName.ToString & " error is " & ex.Message)
        End Try
    End Sub

    'INDIRECT FLIGHT STUFF

    Public Sub GetALLIndirectStartUsingSP()
        'Author: Ben Shadburne
        'Purpose: runs FlightSearch procedure
        'Arguments: na
        'Return: na
        'Date: 03/18/2014


        'this is in clone version for customers and general
        RunProcedureStart("usp_IndirectJourney_Get_PresentFuture")


    End Sub

    Public Sub GetALLIndirectFinishUsingSP()
        'Author: Ben Shadburne
        'Purpose: runs FlightSearch procedure
        'Arguments: na
        'Return: na
        'Date: 03/18/2014


        'this is in clone version for customers and general
        RunProcedureFinish("usp_IndirectJourney_Get_PresentFuture")


    End Sub

    Public Sub RunProcedureStart(ByVal strName As String)
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
            Me.mdatasetIndirectStart.Clear()
            'open conneciton and fill dataset
            mdbDataAdapter.Fill(mdatasetIndirectStart, "tblIndirectStart")
            'copy dataset to dataview
            mMyViewStart.Table = mdatasetIndirectStart.Tables("tblIndirectStart")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strName.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub RunProcedureFinish(ByVal strName As String)
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
            Me.mdatasetIndirectFinish.Clear()
            'open conneciton and fill dataset
            mdbDataAdapter.Fill(mdatasetIndirectFinish, "tblIndirectFinish")
            'copy dataset to dataview
            mMyViewFinish.Table = mdatasetIndirectFinish.Tables("tblIndirectFinish")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strName.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public ReadOnly Property MyViewStart() As DataView
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: FlightSearch dataview
        'Date: 03/18/2014

        Get
            Return mMyViewStart
        End Get
    End Property

    Public ReadOnly Property MyViewFinish() As DataView
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: FlightSearch dataview
        'Date: 03/18/2014

        Get
            Return mMyViewFinish
        End Get
    End Property

    Public ReadOnly Property lblCountStart() As Integer
        'Author: Ben Shadburne
        'Purpose: return lblcount
        'Arguments:  none
        'Return: count of FlightSearch
        'Date: 03/07/2014

        Get
            'returns the count to the label
            Return MyViewStart.Count
        End Get
    End Property

    Public ReadOnly Property lblCountFinish() As Integer
        'Author: Ben Shadburne
        'Purpose: return lblcount
        'Arguments:  none
        'Return: count of FlightSearch
        'Date: 03/07/2014

        Get
            'returns the count to the label
            Return MyViewFinish.Count
        End Get
    End Property


    'this is for individual flights on dates

    'Public Sub 

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

End Class

