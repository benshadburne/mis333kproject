Option Strict On
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Public Class CancelReservation


    'setting up db, dim connection, adapter, query, dataset
    Dim mMyView As New DataView
    Dim mMyViewOne As New DataView
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size=4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False;initial catalog=mis333k_20142_Team06;user id=msbcf819;password=Databasepassword5"
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdatasetCancel As New DataSet
    Dim mQueryString As String
    Dim mDatasetOne As New DataSet

    Dim DBJourneys As New DBjourneyclone
    Dim DBCustomer As New DBCustomersClone
    Dim DBTickets As New DBTickets



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

    Public Sub CancelReservation(strReservationID As String)
        Dim aryParamNames As New ArrayList
        Dim aryParamValues As New ArrayList

        aryParamNames.Add("@ReservationID")

        aryParamValues.Add(strReservationID)

        UseSPforInsertOrUpdateQuery("usp_Reservation_Cancel", aryParamNames, aryParamValues)
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
            mdatasetCancel.Clear()

            ' OPEN CONNECTION AND FILL DATASET
            mdbDataAdapter.Fill(mdatasetCancel, "tblTickets")

            ' copy dataset to dataview
            mMyView.Table = mdatasetCancel.Tables("tblTickets")

        Catch ex As Exception
            Throw New Exception("params are " & strSPName.ToString & " " & strParamName.ToString & " " & strParamValue.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub GetJourneysInReservation(strReservationID As String)
        'returns all journeys in a reservation
        'goes with mdatasetcancel
        RunSPwithOneParam("usp_ReservationsClone_Get_Journeys", "@ReservationID", strReservationID)

    End Sub

    Public Sub GetTicketsInReservationOthers(strReservationID As String)
        'returns all tickets in a reservation
        'goes with mdatasetone
        RunSPwithOneParamOthers("usp_Tickets_Get_By_Reservation", "@ReservationID", strReservationID)

    End Sub

    Public Sub ReturnMilesAndDeactivateTicket(strReservationID As String)
        'this will return miles to customers and send that information to the database
        Dim strMiles As String
        Dim intMiles As Integer
        Dim strAdvantageNumber As String

        GetTicketsInReservationOthers(strReservationID)

        For i = 0 To (mDatasetOne.Tables("tblTickets").Rows.Count - 1)
            strMiles = mDatasetOne.Tables("tblTickets").Rows(i).Item("MilagePaid").ToString
            intMiles = CInt(strMiles)
            If intMiles > 0 Then
                'get the customer's current mileage
                strAdvantageNumber = mDatasetOne.Tables("tblTickets").Rows(i).Item("AdvantageNumber").ToString
                RunSPwithOneParam("usp_CustomersClone_Get_Miles", "@AdvantageNumber", strAdvantageNumber)
                'add that mileage to the mileage from their ticket
                intMiles += CInt(mdatasetCancel.Tables("tblTickets").Rows(0).Item("Miles").ToString)
                'update their miles in the customer tables
                DBCustomer.UpdateMiles(intMiles.ToString, strAdvantageNumber)
            End If
            'update their ticket so that pricepaid and milage paid = 0
            DBTickets.AddTicketPricesAndMiles("0", "0", mDatasetOne.Tables("tblTickets").Rows(i).Item("TicketID").ToString)

            'deactivate their ticket
            UpdateTicketActive(mDatasetOne.Tables("tblTickets").Rows(i).Item("TicketID").ToString)

        Next

    End Sub

    Public Sub DeactivateTickets(strReservationID As String)
        GetTicketsInReservationOthers(strReservationID)
        For i = 0 To mDatasetOne.Tables("tblTickets").Rows.Count - 1
            'deactivate their ticket
            UpdateTicketActive(mDatasetOne.Tables("tblTickets").Rows(i).Item("TicketID").ToString)
        Next

    End Sub

    Public Sub ChangeSeatStatus(strReservationID As String)
        'this will return miles to customers and send that information to the database
        Dim strSeat As String
        Dim strJourneyID As String
        Dim aryParamNames As New ArrayList
        Dim aryParamValues As New ArrayList

        GetJourneysInReservation(strReservationID) ' this is mdatasetcancel


        'loop through each journey
        For i = 0 To mdatasetCancel.Tables("tblTickets").Rows.Count - 1
            strJourneyID = mdatasetCancel.Tables("tblTickets").Rows(i).Item("JourneyOne").ToString

            aryParamNames.Add("@ReservationID")
            aryParamNames.Add("@JourneyID")

            aryParamValues.Add(strReservationID)
            aryParamValues.Add(strJourneyID)

            UseSP("usp_Tickets_Get_By_ReservationANDJourneyID", mDatasetOne, mMyViewOne, "tblTickets", aryParamNames, aryParamValues)


            'loop thourgh each ticket with that journey in that reservation 
            For j = 0 To (mDatasetOne.Tables("tblTickets").Rows.Count - 1)
                strSeat = mDatasetOne.Tables("tblTickets").Rows(j).Item("Seat").ToString
                If strSeat = "" Then
                    'dont do anything
                Else
                    UpdateSeatStatus(strJourneyID, strSeat)
                End If

            Next
            'clear arrays
            aryParamNames.Clear()
            aryParamValues.Clear()

        Next

    End Sub

    Public Sub UpdateTicketActive(strTicketID As String)
        Dim aryParamNames As New ArrayList
        Dim aryParamValues As New ArrayList

        aryParamNames.Add("@TicketID")

        aryParamValues.Add(strTicketID)

        UseSPforInsertOrUpdateQuery("usp_TicketsClone_Update_Active", aryParamNames, aryParamValues)


    End Sub

    Public Sub UpdateSeatStatus(strJourneyID As String, strSeat As String)
        Dim aryParamNames As New ArrayList
        Dim aryParamValues As New ArrayList

        aryParamNames.Add("@JourneyID")
        aryParamNames.Add("@Seat")


        aryParamValues.Add(strJourneyID)
        aryParamValues.Add(strSeat)

        UseSPforInsertOrUpdateQuery("usp_JourneySeatBridge_OpenSeat", aryParamNames, aryParamValues)
    End Sub


    Public Sub RunSPwithOneParamOthers(ByVal strSPName As String, ByVal strParamName As String, ByVal strParamValue As String)
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
            mdatasetone.Clear()

            ' OPEN CONNECTION AND FILL DATASET
            mdbDataAdapter.Fill(mdatasetone, "tblTickets")

            ' copy dataset to dataview
            mMyViewOne.Table = mdatasetone.Tables("tblTickets")

        Catch ex As Exception
            Throw New Exception("params are " & strSPName.ToString & " " & strParamName.ToString & " " & strParamValue.ToString & " error is " & ex.Message)
        End Try
    End Sub

End Class
