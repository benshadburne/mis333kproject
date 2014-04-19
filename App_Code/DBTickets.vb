'Author: Ben Shadburne
'Assignment: ASP4
'Date: 02/06/2014
'Description: handles database based queries and sets up the string and connection



Option Strict On
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Public Class DBTickets
    'setting up db, dim connection, adapter, query, dataset
    Dim mMyView As New DataView
    Dim mMyViewOthers As New DataView
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size=4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False;initial catalog=mis333k_20142_Team06;user id=msbcf819;password=Databasepassword5"
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdatasetTickets As New DataSet
    Dim mdatasetTicketsOthers As New DataSet
    Dim mQueryString As String

    Public Sub GetALLTicketsUsingSP()
        'Author: Ben Shadburne
        'Purpose: runs xxxxx procedure
        'Arguments: na
        'Return: na
        'Date: 03/18/2014


        RunProcedure("usp_Tickets_Get_All")

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
            Me.mdatasetTickets.Clear()
            'open conneciton and fill dataset
            mdbDataAdapter.Fill(mdatasetTickets, "tblTickets")
            'copy dataset to dataview
            mMyView.Table = mdatasetTickets.Tables("tblTickets")
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

        mMyView.Sort = "TicketID, "

        'sort using radio buttons
        If intIndex = 0 Then
            'sort by name
            mMyView.Sort = "lastname, firstname"
        Else
            mMyView.Sort = "username"
        End If
    End Sub

    Public Sub FilterYou(ByVal strRes As String, strAdvantage As String)
        'Author: Ben Shadburne
        'Purpose: search by state
        'Arguments: search text
        'Return: filtered dataview by state
        'Date: 03/18/2014

        MyView.RowFilter = "[ReservationID] = '" & strRes & "' AND [AdvantageNumber] = '" & strAdvantage & "'"
    End Sub

    Public Sub FilterOthers(ByVal strRes As String, strAdvantage As String)
        'Author: Ben Shadburne
        'Purpose: search by state
        'Arguments: search text
        'Return: filtered dataview by state
        'Date: 03/18/2014

        MyViewOthers.RowFilter = "[ReservationID] = '" & strRes & "' AND [AdvantageNumber] <> '" & strAdvantage & "'"
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






    'WILL RUN PROCEDURE TO CREATE DATASET FOR OTHERS' TICKETS





    Public Sub GetALLOthersTicketsUsingSP()
        'Author: Ben Shadburne
        'Purpose: runs xxxxx procedure
        'Arguments: na
        'Return: na
        'Date: 03/18/2014


        RunProcedureOthers("usp_Tickets_Get_All")

    End Sub

    'define a public read only property
    Public ReadOnly Property MyViewOthers() As DataView
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: xxxxx dataview
        'Date: 03/18/2014

        Get
            Return mMyViewOthers
        End Get
    End Property

    Public Sub RunProcedureOthers(ByVal strName As String)
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
            Me.mdatasetTicketsOthers.Clear()
            'open conneciton and fill dataset
            mdbDataAdapter.Fill(mdatasetTicketsOthers, "tblTicketsOthers")
            'copy dataset to dataview
            mMyViewOthers.Table = mdatasetTicketsOthers.Tables("tblTicketsOthers")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strName.ToString & " error is " & ex.Message)
        End Try
    End Sub


End Class

