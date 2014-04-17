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
Public Class DBJourneySeats
    'setting up db, dim connection, adapter, query, dataset
    Dim mMyViewSeats As New DataView
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size=4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False;initial catalog=mis333k_20142_Team06;user id=msbcf819;password=Databasepassword5"
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdatasetJourneySeats As New DataSet
    Dim mQueryString As String
    Private _session As Integer


    Public Sub AddSeats(ByVal strFlight As String, ByVal strDate As String)
        Dim aryParamNames As New ArrayList
        Dim aryParamValues As New ArrayList
        Dim arySeatValues As New ArrayList
        Dim intJourneyID As Integer

        aryParamNames.Add("@FlightNumber")
        aryParamNames.Add("@FlightDate")
        aryParamValues.Add(strFlight)
        aryParamValues.Add(strDate)

        'gets the journeyid that was just created, places it in view, should just be one
        UseSPToRetrieveRecords("usp_JourneySeats_Get_ID", mdatasetJourneySeats, mMyViewSeats, "tblJourneySeats", aryParamNames, aryParamValues)
        intJourneyID = CInt(mdatasetJourneySeats.Tables("tblJourneySeats").Rows(0).Item("JourneyID"))
        'loop to add seats, first reassigning values to paramnames array list
        aryParamNames.Remove("@FlightNumber")
        aryParamNames.Remove("@FlightDate")
        aryParamNames.Add("@JourneyID")
        aryParamNames.Add("@Seat")
        aryParamNames.Add("@Status")
        arySeatValues.Add(intJourneyID)
        Dim arlSeats As New ArrayList
        Dim j As Integer

        For j = 1 To 5

            arlSeats.Add(j & "A")
            arlSeats.Add(j & "B")
            If j > 2 Then
                arlSeats.Add(j & "C")
                arlSeats.Add(j & "D")
            End If

        Next


        For i = 0 To 15
            'adds these values to the array list

            arySeatValues.Add(arlSeats(i))
            arySeatValues.Add("N")


            UseSPforInsertOrUpdateQuery("usp_JourneySeats_Insert_Seats", aryParamNames, arySeatValues)
            'removes these values from the array list then loops to next seat

            arySeatValues.Remove("N")
            arySeatValues.Remove(arlSeats(i))
        Next

    End Sub



    Public Sub GetALLJourneySeatsUsingSP()
        'Author: Ben Shadburne
        'Purpose: runs xxxxx procedure
        'Arguments: na
        'Return: na
        'Date: 03/18/2014

        RunProcedure("usp_journeyclone_get_all")
    End Sub

    'define a public read only property
    Public ReadOnly Property MyViewSeats() As DataView
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: xxxxx dataview
        'Date: 03/18/2014

        Get
            Return mMyViewSeats
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
            Me.mdatasetJourneySeats.Clear()
            'open conneciton and fill dataset
            mdbDataAdapter.Fill(mdatasetJourneySeats, "tblJourneySeats")
            'copy dataset to dataview
            mMyViewSeats.Table = mdatasetJourneySeats.Tables("tblJourneySeats")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strName.ToString & " error is " & ex.Message)
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

    'Public Sub DoSort(ByVal intIndex As Integer)
    '    'Author: Ben Shadburne
    '    'Purpose: sorts data
    '    'Arguments: index of rad button
    '    'Return: sorted dataview
    '    'Date: 03/18/2014

    '    'sort using radio buttons
    '    If intIndex = 0 Then
    '        'sort by name
    '        mMyView.Sort = "lastname, firstname"
    '    Else
    '        mMyView.Sort = "username"
    '    End If
    'End Sub

    'Public Sub SearchByState(ByVal strIn As String)
    '    'Author: Ben Shadburne
    '    'Purpose: search by state
    '    'Arguments: search text
    '    'Return: filtered dataview by state
    '    'Date: 03/18/2014

    '    MyView.RowFilter = "State = '" & strIn & "'"
    'End Sub

    'Public Sub SearchByPartialLastname(ByVal strIn As String)
    '    'Author: Ben Shadburne
    '    'Purpose: search by partial lastname
    '    'Arguments:  search text
    '    'Return: filtered dataview by partial lastname
    '    'Date: 03/18/2014

    '    MyView.RowFilter = "Lastname like '" & strIn & "%'"
    'End Sub

    'Public Sub SearchbyPartialCity(ByVal strIn As String)
    '    'Author: Ben Shadburne
    '    'Purpose: search by partial city
    '    'Arguments:  search text
    '    'Return: filtered dataview by partial city
    '    'Date: 03/18/2014

    '    MyView.RowFilter = "City like '" & strIn & "%'"
    'End Sub

    'Public Sub SearchByPartialUserName(ByVal strIn As String)
    '    'Author: Ben Shadburne
    '    'Purpose: search by partial username
    '    'Arguments:  search text
    '    'Return: filtered dataview by partial username
    '    'Date: 03/18/2014

    '    MyView.RowFilter = "UserName like '" & strIn & "%'"
    'End Sub

    'Public ReadOnly Property lblCount() As Integer
    '    'Author: Ben Shadburne
    '    'Purpose: return lblcount
    '    'Arguments:  none
    '    'Return: count of xxxxx
    '    'Date: 03/07/2014

    '    Get
    '        'returns the count to the label
    '        Return MyViewSeats.Count
    '    End Get
    'End Property

End Class

