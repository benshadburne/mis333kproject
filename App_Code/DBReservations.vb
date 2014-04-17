'Author: Ben Shadburne
'Assignment: ASP4
'Date: 02/06/2014
'Description: handles database based queries and sets up the string and connection


Option Strict On
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Public Class DBReservations
    'setting up db, dim connection, adapter, query, dataset
    Dim mMyView As New DataView
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size=4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False;initial catalog=mis333k_20142_Team06;user id=msbcf819;password=Databasepassword5"
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdatasetReservations As New DataSet
    Dim mQueryString As String

    Public Sub GetALLReservationsUsingSP(ByVal strAdvantage As String)
        'Author: Ben Shadburne
        'Purpose: runs xxxxx procedure
        'Arguments: na
        'Return: na
        'Date: 03/18/2014

        'this only gets ones for the customer who's advantage number is sent
        RunProcedure("usp_Reservations_Get_All")

        'sort for advantage number here

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
            Me.mdatasetReservations.Clear()
            'open conneciton and fill dataset
            mdbDataAdapter.Fill(mdatasetReservations, "tblReservations")
            'copy dataset to dataview
            mMyView.Table = mdatasetReservations.Tables("tblReservations")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strName.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub DoSort()
        'Author: Ben Shadburne
        'Purpose: sorts data
        'Arguments: index of rad button
        'Return: sorted dataview
        'Date: 03/18/2014

        'only sort should be by reservationID
        mMyView.Sort = "[ReservationID]"

    End Sub

    Public Sub SearchByAdvantageNumber(ByVal strIn As String)
        'Author: Ben Shadburne
        'Purpose: search by state
        'Arguments: search text
        'Return: filtered dataview by state
        'Date: 03/18/2014

        MyView.RowFilter = "[Advantage Numbers] = '&" & strIn & "&'"
    End Sub

    Public Sub SearchByPartialLastname(ByVal strIn As String)
        'Author: Ben Shadburne
        'Purpose: search by partial lastname
        'Arguments:  search text
        'Return: filtered dataview by partial lastname
        'Date: 03/18/2014

        MyView.RowFilter = "Lastname like '" & strIn & "%'"
    End Sub

    Public Sub SearchbyPartialCity(ByVal strIn As String)
        'Author: Ben Shadburne
        'Purpose: search by partial city
        'Arguments:  search text
        'Return: filtered dataview by partial city
        'Date: 03/18/2014

        MyView.RowFilter = "City like '" & strIn & "%'"
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
        'Return: count of xxxxx
        'Date: 03/07/2014

        Get
            'returns the count to the label
            Return MyView.Count
        End Get
    End Property

End Class

