'Author: Ben Shadburne
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
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size=4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False;initial catalog=mis333k_20142_Team06;user id=msbcf819;password=Databasepassword5"
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdatasetFlightSearch As New DataSet
    Dim mQueryString As String
    Dim mstrFilterStatement As String

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

    Public Sub DoSort(ByVal intIndex As Integer)
        'Author: Ben Shadburne
        'Purpose: sorts data
        'Arguments: index of rad button
        'Return: sorted dataview
        'Date: 03/18/2014

        'sort using radio buttons
        If intIndex = 0 Then
            'sort by name
            mMyView.Sort = "lastname, firstname"
        Else
            mMyView.Sort = "username"
        End If
    End Sub

    Public Sub SearchByAirports(ByVal strStart As String, ByVal strEnd As String)
        'Author: Aaryaman Singhal
        'Purpose: search by start/end airports
        'Arguments: start and end airport codes
        'Return: filtered dataview by start and end airport
        'Date: 04/16/2014

        mstrFilterStatement += "[Departure City] = '" & strStart & "' AND [End City] = '" & strEnd & "' AND "

    End Sub

    Public Sub SearchStartAirport(ByVal strStart As String)
        'Author: Aaryaman Singhal
        'Purpose: search by start/end airports
        'Arguments: start and end airport codes
        'Return: filtered dataview by start and end airport
        'Date: 04/16/2014

        MyView.RowFilter = "[Departure City] = '" & strStart & "'"
    End Sub

    Public Sub SearchEndAirport(ByVal strEnd As String)
        'Author: Aaryaman Singhal
        'Purpose: search by start/end airports
        'Arguments: start and end airport codes
        'Return: filtered dataview by start and end airport
        'Date: 04/16/2014

        MyView.RowFilter = "[End City] = '" & strEnd & "'"
    End Sub

    Public Sub SearchByDate(ByVal strIn As String)
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

        mstrFilterStatement += "[Flight Date] = '" & strIn.Substring(Len(strIn) - 4, 4) & "-" & strMonth & "-" & strDay & "'"

    End Sub

    Public Function FilterAll(strStart As String, strEnd As String, strDate As String) As String

        SearchByAirports(strStart, strEnd)
        SearchByDate(strDate)
        Return mstrFilterStatement
        mstrFilterStatement = ""
    End Function

    Public Sub SearchTime(ByVal strIn As String)
        'Author: Ben Shadburne
        'Purpose: search by partial city
        'Arguments:  search text
        'Return: filtered dataview by partial city
        'Date: 03/18/2014

        mstrFilterStatement += "[Departure Time] >= '" & strIn & "'"

    End Sub

    Public Sub SearchByPartialUserName(ByVal strIn As String)
        'Author: Ben Shadburne
        'Purpose: search by partial username
        'Arguments:  search text
        'Return: filtered dataview by partial username
        'Date: 03/18/2014

        MyView.RowFilter = "UserName like '" & strIn & "%'"
    End Sub

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

End Class

