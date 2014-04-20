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
    Dim mMyViewAdvantageOthers As New DataView
    Dim mMyViewAdvantageUser As New DataView
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size=4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False;initial catalog=mis333k_20142_Team06;user id=msbcf819;password=Databasepassword5"
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdatasetSeats As New DataSet
    Dim mdatasetSeatsAdvantage As New DataSet
    Dim mdatasetSeatsAdvantageOthers As New DataSet
    Dim mdatasetSeatsAdvantageUser As New DataSet
    Dim mQueryString As String

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

    Public Sub GetALLSeatsAdvantageUsingSP()
        'Author: Ben Shadburne
        'Purpose: runs xxxxx procedure
        'Arguments: na
        'Return: na
        'Date: 03/18/2014

        RunProcedureAdvantage("usp_SeatAdvantage_Get_All")

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

    Public Sub RunProcedureAdvantage(ByVal strName As String)
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
            Me.mdatasetSeatsAdvantage.Clear()
            'open conneciton and fill dataset
            mdbDataAdapter.Fill(mdatasetSeatsAdvantage, "tblSeatsAdvantage")
            'copy dataset to dataview
            mMyViewAdvantage.Table = mdatasetSeatsAdvantage.Tables("tblSeatsAdvantage")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strName.ToString & " error is " & ex.Message)
        End Try
    End Sub

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








    'THIS IS WITH THE ADVANTAGE NUMBER OF USER INCLUDED

    Public Sub GetALLSeatsAdvantageUserUsingSP()
        'Author: Ben Shadburne
        'Purpose: runs xxxxx procedure
        'Arguments: na
        'Return: na
        'Date: 03/18/2014

        RunProcedureAdvantageUser("usp_SeatAdvantage_Get_All")

    End Sub

    'define a public read only property
    Public ReadOnly Property MyViewAdvantageUser() As DataView
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: xxxxx dataview
        'Date: 03/18/2014

        Get
            Return mMyViewAdvantageUser
        End Get
    End Property

    Public Sub RunProcedureAdvantageUser(ByVal strName As String)
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
            Me.mdatasetSeatsAdvantageUser.Clear()
            'open conneciton and fill dataset
            mdbDataAdapter.Fill(mdatasetSeatsAdvantageUser, "tblSeatsAdvantage")
            'copy dataset to dataview
            mMyViewAdvantageUser.Table = mdatasetSeatsAdvantageUser.Tables("tblSeatsAdvantage")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strName.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub FilterAdvantage(strNumber As String)
        MyViewAdvantageUser.RowFilter = "[AdvantageNumber] = '" & strNumber & "'"
    End Sub

    Public ReadOnly Property lblCountAdvantageUser() As Integer
        'Author: Ben Shadburne
        'Purpose: return lblcount
        'Arguments:  none
        'Return: count of xxxxx
        'Date: 03/07/2014

        Get
            'returns the count to the label
            Return MyViewAdvantageUser.Count
        End Get
    End Property



    'THIS IS WITH THE ADVANTAGE NUMBER FOR OTHERS INCLUDED

    Public Sub GetALLSeatsAdvantageOthersUsingSP()
        'Author: Ben Shadburne
        'Purpose: runs xxxxx procedure
        'Arguments: na
        'Return: na
        'Date: 03/18/2014

        RunProcedureAdvantageOthers("usp_SeatAdvantage_Get_All")

    End Sub

    'define a public read only property
    Public ReadOnly Property MyViewAdvantageOthers() As DataView
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: xxxxx dataview
        'Date: 03/18/2014

        Get
            Return mMyViewAdvantageOthers
        End Get
    End Property

    Public Sub RunProcedureAdvantageOthers(ByVal strName As String)
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
            Me.mdatasetSeatsAdvantageOthers.Clear()
            'open conneciton and fill dataset
            mdbDataAdapter.Fill(mdatasetSeatsAdvantageOthers, "tblSeatsAdvantageOthers")
            'copy dataset to dataview
            mMyViewAdvantageOthers.Table = mdatasetSeatsAdvantageOthers.Tables("tblSeatsAdvantageOthers")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strName.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub FilterAdvantageOthers(strNumber As String, strReservationID As String)
        MyViewAdvantageOthers.RowFilter = "[ReservationID] <> '" & strReservationID & "'"
    End Sub

    Public ReadOnly Property lblCountOthers() As Integer
        'Author: Ben Shadburne
        'Purpose: return lblcount
        'Arguments:  none
        'Return: count of xxxxx
        'Date: 03/07/2014

        Get
            'returns the count to the label
            Return MyViewAdvantageOthers.Count
        End Get
    End Property


    'Update Query

    Public Sub GreyPress(strSeat As String)
        'first gotta check if they have a seat selected already
        Dim aryParamName As New ArrayList
        Dim aryParamValue As New ArrayList
        aryParamName.Add("@Seat")
        aryParamValue.Add(MyViewAdvantage.Table().Rows(0).Item("Seat").ToString)
        aryParamName.Add("@Status")
        If lblCountAdvantage <> 0 Then
            'they have one selected, so we gotta change the status of their current seat to 0
            aryParamValue.Add(0)

            UseSPforInsertOrUpdateQuery("usp_Seats_Alter_User_Seat", aryParamName, aryParamValue)
            'remove so we can update 
            aryParamValue.Remove(0)
        End If
        'either way, update status of new seat to 1 in table
        aryParamValue.Add(1)

        'run update
        UseSPforInsertOrUpdateQuery("usp_Seats_Alter_User_Seat", aryParamName, aryParamValue)
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

End Class

