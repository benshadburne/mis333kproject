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
Public Class DBSeats
    'setting up db, dim connection, adapter, query, dataset
    Dim mMyView As New DataView
    Dim mMyViewAdvantage As New DataView
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size=4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False;initial catalog=mis333k_20142_Team06;user id=msbcf819;password=Databasepassword5"
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdatasetSeats As New DataSet
    Dim mdatasetSeatsAdvantage As New DataSet

    Public Sub GetALLSeatsUsingSP()
        'Author: Ben Shadburne
        'Purpose: runs xxxxx procedure
        'Arguments: na
        'Return: na
        'Date: 03/18/2014

        RunProcedure("usp_Seats_Get_All")

    End Sub

    'define a public read only property
    Public ReadOnly Property MyView() As DataView
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: xxxxx dataview
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
            Me.mdatasetSeats.Clear()
            'open conneciton and fill dataset
            mdbDataAdapter.Fill(mdatasetSeats, "tblSeats")
            'copy dataset to dataview
            mMyView.Table = mdatasetSeats.Tables("tblSeats")
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

    Public Sub FilterJourneyID(ByVal strIn As String)
        'Author: Ben Shadburne
        'Purpose: search by state
        'Arguments: search text
        'Return: filtered dataview by state
        'Date: 03/18/2014

        MyView.RowFilter = "[JourneyID] = '" & strIn & "'"
    End Sub

    Public ReadOnly Property lblCount() As Integer
        'Author: Ben Shadburne
        'Purpose: return lblcount
        'Arguments:  none
        'Return: count of xxxxx
        'Date: 03/07/2014

        Get
            'returns the count to the label
            Return MyView.Count
        End Get
    End Property









    'THIS IS WITH THE ADVANTAGE NUMBER INCLUDED 
    '(past here will only have records if tickets have been produced for this journey)

    Public Sub GetALLSeatsAdvantageUsingSP(strIn As String)
        'Author: Ben Shadburne
        'Purpose: runs xxxxx procedure
        'Arguments: na
        'Return: na
        'Date: 03/18/2014

        Dim aryParamName As New ArrayList
        Dim aryParamValue As New ArrayList

        aryParamName.Add("@JourneyID")
        aryParamValue.Add(strIn)

        UseSPToRetrieveRecords("usp_SeatAdvantage_Get_All", mdatasetSeatsAdvantage, mMyViewAdvantage, "tblSeatsAdvantage", aryParamName, aryParamValue)

    End Sub

    'define a public read only property
    Public ReadOnly Property MyViewAdvantage() As DataView
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: xxxxx dataview
        'Date: 03/18/2014

        Get
            Return mMyViewAdvantage
        End Get
    End Property

    Public ReadOnly Property lblCountAdvantage() As Integer
        'Author: Ben Shadburne
        'Purpose: return lblcount
        'Arguments:  none
        'Return: count of xxxxx
        'Date: 03/07/2014

        Get
            'returns the count to the label
            Return MyViewAdvantage.Count
        End Get
    End Property


    'Extra Subs
    Public Sub UpdateTicket(strNewSeat As String, strAdvantageNumber As String, strJourneyID As String)

        'these are for updating the ticket table
        Dim aryTicketName As New ArrayList
        Dim aryTicketValue As New ArrayList
        aryTicketName.Add("@Seat")
        aryTicketName.Add("@JourneyID")
        aryTicketName.Add("@AdvantageNumber")
        aryTicketValue.Add(strNewSeat)
        aryTicketValue.Add(strJourneyID)
        aryTicketValue.Add(strAdvantageNumber)

        'run update for ticket as well
        UseSPforInsertOrUpdateQuery("usp_Ticket_Seat_Set", aryTicketName, aryTicketValue)

    End Sub

    Public Sub UpdateJourneySeatBridge(strSeat As String, intStatus As Integer, strJourneyID As String)
        'runs update on journeyseatbridge
        'makes array lists
        Dim aryParamName As New ArrayList
        Dim aryParamValue As New ArrayList
        aryParamName.Add("@JourneyID")
        aryParamName.Add("@Seat")
        aryParamName.Add("@Status")
        aryParamValue.Add(strJourneyID)
        aryParamValue.Add(strSeat)
        aryParamValue.Add(intStatus)

        'runs the update
        UseSPforInsertOrUpdateQuery("usp_Seats_Alter_User_Seat", aryParamName, aryParamValue)
    End Sub

    Public Sub GreyPress(strOldSeat As String, strNewSeat As String, strAdvantageNumber As String, strJourneyID As String)

        UpdateTicket(strNewSeat, strAdvantageNumber, strJourneyID)

        'if they have a seat selected already
        If strOldSeat <> "" Then
            'run update to remove existing values
            UpdateJourneySeatBridge(strOldSeat, 0, strJourneyID)
        End If
        
        'either way, run update in journeyseatbridge for new values
        UpdateJourneySeatBridge(strNewSeat, 1, strJourneyID)

    End Sub

    Public Sub BluePress(strOldSeat As String, strNewSeat As String, strAdvantageNumber As String, strJourneyID As String)

        UpdateTicket(strNewSeat, strAdvantageNumber, strJourneyID)

        If strOldSeat <> "" Then
            UpdateJourneySeatBridge(strOldSeat, 1, strJourneyID)
        End If
        'either way, update status of new seat to 2 in table
        UpdateJourneySeatBridge(strNewSeat, 2, strJourneyID)

    End Sub

    Public Sub GreyPressBaby(strOldSeat As String, strNewSeat As String, strAdvantageNumber As String, strJourneyID As String, strInfant As String)
        'update parent ticket
        UpdateTicket(strNewSeat, strAdvantageNumber, strJourneyID)
        'update infant ticket
        UpdateTicket(strNewSeat, strInfant, strJourneyID)

        'if they have a seat selected already
        If strOldSeat <> "" Then
            UpdateJourneySeatBridge(strOldSeat, 0, strJourneyID)
        End If
        'either way, update status of new seat to 1 in table
        UpdateJourneySeatBridge(strNewSeat, 2, strJourneyID)

    End Sub

    Public Sub UseSPforInsertOrUpdateQuery(ByVal strUSPName As String, ByVal aryParamNames As ArrayList, ByVal aryParamValues As ArrayList)
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


End Class

