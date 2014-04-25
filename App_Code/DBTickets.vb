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
    Dim mMyViewAdvantageNumbers As New DataView
    Dim mMyViewFlight As New DataView
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size=4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False;initial catalog=mis333k_20142_Team06;user id=msbcf819;password=Databasepassword5"
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdatasetTickets As New DataSet
    Dim mdatasetTicketsOthers As New DataSet
    Dim mdatasetAdvantageNumbers As New DataSet
    Dim mdatasetFlight As New DataSet
    Dim mQueryString As String
    Dim mMyViewOne As New DataView
    Dim mdatasetOne As New DataSet

    Public Sub GetALLTicketsUsingSP()
        'Author: Ben Shadburne
        'Purpose: runs xxxxx procedure
        'Arguments: na
        'Return: na
        'Date: 03/18/2014


        RunProcedure("usp_Tickets_Get_All")

    End Sub

    Public Sub GetAdvantageNumbersUsingSP(strJourneyID As String, strReservationID As String)
        'Author: Ben Shadburne
        'Purpose: runs advantagenumber procedure
        'Arguments: journeyid and reservationid
        'Return: na
        'Date: 04/23/2014

        Dim aryParamName As New ArrayList
        Dim aryParamValue As New ArrayList

        aryParamName.Add("@JourneyID")
        aryParamName.Add("@ReservationID")
        aryParamValue.Add(strJourneyID)
        aryParamValue.Add(strReservationID)

        UseSP("usp_Tickets_Get_Advantage", mdatasetAdvantageNumbers, mMyViewAdvantageNumbers, "tblAdvantageNumbers", aryParamName, aryParamValue)


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

    Public ReadOnly Property MyViewAdvantageNumbers() As DataView
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: xxxxx dataview
        'Date: 03/18/2014

        Get
            Return mMyViewAdvantageNumbers
        End Get
    End Property

    Public ReadOnly Property MyDataSet() As DataSet
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: xxxxx dataview
        'Date: 03/18/2014

        Get
            Return mdatasetTickets

        End Get
    End Property

    Public ReadOnly Property MyDataSetOne() As DataSet
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: xxxxx dataview
        'Date: 03/18/2014

        Get
            Return mdatasetOne

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
            mdatasetOne.Clear()

            ' OPEN CONNECTION AND FILL DATASET
            mdbDataAdapter.Fill(mdatasetOne, "tblTickets")

            ' copy dataset to dataview
            mMyView.Table = mdatasetOne.Tables("tblTickets")

        Catch ex As Exception
            Throw New Exception("params are " & strSPName.ToString & " " & strParamName.ToString & " " & strParamValue.ToString & " error is " & ex.Message)
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

    Protected Sub UseSP(ByVal strUSPName As String, ByVal strDatasetName As DataSet, ByVal strViewName As DataView, ByVal strTableName As String, ByVal aryParamNames As ArrayList, ByVal aryParamValues As ArrayList)
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

    Public Sub AddTicket(strReservationID As String, strAdvantageNumber As String, strJourneyID As String, strFlightNumber As String, strBaseFare As String)
        'defines array to put parameter names into
        Dim aryParamNames As New ArrayList
        Dim aryParamValues As New ArrayList

        'add parameter names to names array list
        aryParamNames.Add("@ReservationID")
        aryParamNames.Add("@AdvantageNumber")
        aryParamNames.Add("@JourneyID")
        aryParamNames.Add("@FlightNumber")
        aryParamNames.Add("@BaseFare")


        'add values to parameter values array list
        aryParamValues.Add(strReservationID)
        aryParamValues.Add(strAdvantageNumber)
        aryParamValues.Add(strJourneyID)
        aryParamValues.Add(strFlightNumber)
        aryParamValues.Add(strBaseFare)

        UseSPforInsertOrUpdateQuery("usp_Ticketclone_Add_New", aryParamNames, aryParamValues)
    End Sub

    Public Function CheckIfTicketIsUnique(strJourneyID As String, strAdvantageNumber As String) As Boolean
        'defines array to put parameter names into
        Dim aryParamNames As New ArrayList
        Dim aryParamValues As New ArrayList


        aryParamNames.Add("@JourneyID")
        aryParamNames.Add("@AdvantageNumber")

        aryParamValues.Add(strJourneyID)
        aryParamValues.Add(strAdvantageNumber)

        UseSP("usp_TicketsClone_Check_Unique", mdatasetTickets, mMyView, "tblTicketsClone", aryParamNames, aryParamValues)

        If MyDataSet.Tables("tblTicketsClone").Rows.Count >= 1 Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function GetAge(strAdvantageNumber As String) As Integer

        RunSPwithOneParam("usp_TicketsClone_Get_One", "@AdvantageNumber", strAdvantageNumber)

        Return CInt(mdatasetOne.Tables("tblTickets").Rows(0).Item("Age"))

    End Function

    Public Function GetMileage(strAdvantageNumber As String) As String

        RunSPwithOneParam("usp_CustomersClone_Get_Miles", "@AdvantageNumber", strAdvantageNumber)

        Return mdatasetOne.Tables("tblTickets").Rows(0).Item("Miles").ToString

    End Function

    Public Function GetBaseFare(strAdvantageNumber As String) As Integer

        RunSPwithOneParam("usp_TicketsClone_Get_One", "@AdvantageNumber", strAdvantageNumber)

        Return CInt(mdatasetOne.Tables("tblTickets").Rows(0).Item("BaseFareAtPurchase"))

    End Function

    Public Sub GetTicketsInReservation(strReservationID As String)
        'returns all tickets in a reservation
        RunSPwithOneParam("usp_Tickets_Get_By_Reservation", "@ReservationID", strReservationID)

    End Sub

    Public Sub AddTicketPrices(strSPName As String, intPricePaid As Integer, intMileagePaid As Integer, intTicketID As Integer)
        'defines array to put parameter names into
        Dim aryParamNames As New ArrayList
        Dim aryParamValues As New ArrayList

        'add parameter names to names array list
        aryParamNames.Add("@PricePaid")
        aryParamNames.Add("@MilesPaid")
        aryParamNames.Add("@TicketID")

        'add values to parameter values array list
        aryParamValues.Add(intPricePaid)
        aryParamValues.Add(intMileagePaid)
        aryParamValues.Add(intTicketID)

        UseSPforInsertOrUpdateQuery("usp_TicketClone_Add_Price", aryParamNames, aryParamValues)

    End Sub

    Public Sub DoSort(ByVal intIndex As Integer)
        'Author: Ben Shadburne
        'Purpose: sorts data
        'Arguments: index of rad button
        'Return: sorted dataview
        'Date: 03/18/2014

        mMyView.Sort = "TicketID, "

        
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

    Public ReadOnly Property lblCountAdvantage() As Integer
        'Author: Ben Shadburne
        'Purpose: return lblcount
        'Arguments:  none
        'Return: count of xxxxx
        'Date: 03/07/2014

        Get
            'returns the count to the label
            Return MyViewAdvantageNumbers.Count
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


    'THIS IS TO MODIFY EXISTING TICKETS WITH NEW JOURNEYID/REMOVE SEAT
    Public Sub ModifyTicketJourneyID(strNewJourneyID As String, strOldJourneyID As String, strReservationID As String)

        Dim aryParamName As New ArrayList
        Dim aryParamValue As New ArrayList

        aryParamName.Add("@NewJourneyID")
        aryParamName.Add("@OldJourneyID")
        aryParamName.Add("@ReservationID")
        aryParamValue.Add(strNewJourneyID)
        aryParamValue.Add(strOldJourneyID)
        aryParamValue.Add(strReservationID)

        UseSPforInsertOrUpdateQuery("usp_Tickets_Update_JourneyID", aryParamName, aryParamValue)

    End Sub

    Public Sub ModifyTicketSeat(strJourneyID As String, strReservationID As String)

        Dim aryParamName As New ArrayList
        Dim aryParamValue As New ArrayList

        aryParamName.Add("@JourneyID")
        aryParamName.Add("@ReservationID")
        aryParamValue.Add(strJourneyID)
        aryParamValue.Add(strReservationID)

        UseSPforInsertOrUpdateQuery("usp_Tickets_Update_Seat", aryParamName, aryParamValue)

    End Sub


    'this is to retrieve flightnumber
    Public Sub GetFlightNumber(strReservationID As String, strJourneyID As String)

        Dim aryParamName As New ArrayList
        Dim aryParamValue As New ArrayList

        aryParamName.Add("@ReservationID")
        aryParamName.Add("@JourneyID")

        aryParamValue.Add(strReservationID)
        aryParamValue.Add(strJourneyID)

        UseSP("usp_Tickets_Get_FlightNumber", mdatasetFlight, mMyViewFlight, "tblFlightNumber", aryParamName, aryParamValue)


    End Sub

    Public ReadOnly Property MyViewFlight() As DataView
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: xxxxx dataview
        'Date: 03/18/2014

        Get
            Return mMyViewFlight

        End Get
    End Property

End Class

