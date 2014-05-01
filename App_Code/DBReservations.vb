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

    Public Sub GetALLReservationsUsingSP()
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

    Public ReadOnly Property MyDataSet() As DataSet
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: xxxxx dataview
        'Date: 03/18/2014

        Get
            Return mdatasetReservations
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
            mdbDataAdapter.Fill(mdatasetReservations, "tblReservationsClone")
            'copy dataset to dataview
            mMyView.Table = mdatasetReservations.Tables("tblReservationsClone")
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

    Public Sub AddFirstReservationJourney(strSPName As String, strJourneyNumber As String, intJourneyID As Integer, strDate As String)
        'defines array to put parameter names into
        Dim aryParamNames As New ArrayList
        Dim aryParamValues As New ArrayList

        'add parameter names to names array list
        aryParamNames.Add("@JourneyNumber")
        aryParamNames.Add("@StartDate")
        aryParamNames.Add("@JourneyID")


        'add values to parameter values array list
        aryParamValues.Add(strJourneyNumber)
        aryParamValues.Add(strDate)
        aryParamValues.Add(intJourneyID)

        UseSPforInsertOrUpdateQuery(strSPName, aryParamNames, aryParamValues)
    End Sub

    Public Sub AddLaterReservationJourneys(strSPName As String, strJourneyNumber As String, intJourneyID As Integer, intReservationID As Integer)
        'defines array to put parameter names into
        Dim aryParamNames As New ArrayList
        Dim aryParamValues As New ArrayList

        'add parameter names to names array list
        aryParamNames.Add("@ReservationID")
        aryParamNames.Add("@JourneyNumber")
        aryParamNames.Add("@JourneyID")


        'add values to parameter values array list
        aryParamValues.Add(intReservationID)
        aryParamValues.Add(strJourneyNumber)
        aryParamValues.Add(intJourneyID)

        UseSPforInsertOrUpdateQuery(strSPName, aryParamNames, aryParamValues)

    End Sub

    Public Function ConvertJourneyNumberToString(intJourneyNumber As Integer) As String
        Select Case intJourneyNumber
            Case 1
                Return "JourneyOne"
            Case 2
                Return "JourneyTwo"
            Case 3
                Return "JourneyThree"
            Case 4
                Return "JourneyFour"
            Case 5
                Return "JourneyFive"
            Case 6
                Return "JourneySix"
            Case 7
                Return "JourneySeven"
            Case 8
                Return "JourneyEight"
            Case 9
                Return "JourneyNine"
            Case Else
                Return "JourneyTen"
        End Select

    End Function

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
            mdatasetReservations.Clear()

            ' OPEN CONNECTION AND FILL DATASET
            mdbDataAdapter.Fill(mdatasetReservations, "tblReservationsClone")

            ' copy dataset to dataview
            mMyView.Table = mdatasetReservations.Tables("tblReservationsClone")

        Catch ex As Exception
            Throw New Exception("params are " & strSPName.ToString & " " & strParamName.ToString & " " & strParamValue.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Function GetNewestReservationID() As Integer
        Dim intReservationID As Integer
        RunProcedure("usp_ReservationsClone_Get_Newest")
        intReservationID = CInt(mdatasetReservations.Tables("tblReservationsClone").Rows(0).Item("ReservationID"))
        Return intReservationID
    End Function

    Public Sub GetJourneysInReservation(intReservationID As Integer)
        RunSPwithOneParam("usp_ReservationsClone_Get_Journeys", "@ReservationID", intReservationID.ToString)

    End Sub

    Public Sub PutOneJourneyInGridview(intIndex As Integer)

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

        MyView.RowFilter = "[AdvantageNumber] = '" & strIn & "'"
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

    Public Sub AddFee(strReservationID As String)

        Dim aryParamName As New ArrayList
        Dim aryParamValue As New ArrayList

        aryParamName.Add("@ReservationID")
        aryParamValue.Add(strReservationID)

        UseSPforInsertOrUpdateQuery("usp_Reservation_Add_Fee", aryParamName, aryParamValue)
    End Sub

End Class

