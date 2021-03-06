﻿'Author: Ben Shadburne
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

    'Declare views for ddls
    Dim DepartureCityDataView As New DataView
    Dim EndCityDataView As New DataView

    'Declare an instance of the classes
    Dim valid As New ClassValidate

    Public Sub GetALLTicketsUsingSP()
        'Author: Ben Shadburne
        'Purpose: runs xxxxx procedure
        'Arguments: na
        'Return: na
        'Date: 03/18/2014


        RunProcedure("usp_Tickets_Get_All")

    End Sub

    Public Sub GetALLTicketsForResUsingSP()
        'Author: Ben Shadburne
        'Purpose: runs xxxxx procedure
        'Arguments: na
        'Return: na
        'Date: 03/18/2014


        RunProcedure("usp_Tickets_Get_All_For_Reservations")

    End Sub

    Public Sub GetALLOthersTicketsForResUsingSP()
        'Author: Ben Shadburne
        'Purpose: runs xxxxx procedure
        'Arguments: na
        'Return: na
        'Date: 03/18/2014

        RunProcedureOthers("usp_Tickets_Get_All_For_Reservations")

    End Sub

    Public Sub GetALLTicketsWithDateUsingSP()
        'Author: Ben Shadburne
        'Purpose: runs xxxxx procedure
        'Arguments: na
        'Return: na
        'Date: 03/18/2014


        RunProcedure("usp_Tickets_Get_All_With_Date")

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

    Public Sub GetActiveAdvantageNumbersUsingSP(strJourneyID As String, strReservationID As String)
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

        UseSP("usp_Tickets_Get_Active_Advantage", mdatasetAdvantageNumbers, mMyViewAdvantageNumbers, "tblAdvantageNumbers", aryParamName, aryParamValue)


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

    'define a public read only property
    Public ReadOnly Property DepartureCity() As DataView
        'Author: Dennis Phelan
        'Purpose: returns read only dataview for Departure City ddl
        'Arguments: na
        'Return: xxxxx dataview
        'Date: April 29, 2014

        Get
            Return DepartureCityDataView
        End Get
    End Property

    'define a public read only property
    Public ReadOnly Property EndCity() As DataView
        'Author: Dennis Phelan
        'Purpose: returns read only dataview for Departure City ddl
        'Arguments: na
        'Return: xxxxx dataview
        'Date: April 29, 2014

        Get
            Return EndCityDataView
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

    Public ReadOnly Property MyViewOne() As DataView
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: xxxxx dataview
        'Date: 03/18/2014

        Get
            Return mMyViewOne

        End Get
    End Property


    Public ReadOnly Property MyDataSetOthers() As DataSet
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: xxxxx dataview
        'Date: 03/18/2014

        Get
            Return mdatasetTicketsOthers

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
            mMyViewOne.Table = mdatasetOne.Tables("tblTickets")

        Catch ex As Exception
            Throw New Exception("params are " & strSPName.ToString & " " & strParamName.ToString & " " & strParamValue.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub RunSPwithOneParamReservation(ByVal strSPName As String, ByVal strParamName As String, ByVal strParamValue As String)
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
            mdatasetTickets.Clear()

            ' OPEN CONNECTION AND FILL DATASET
            mdbDataAdapter.Fill(mdatasetTickets, "tblTickets")

            ' copy dataset to dataview
            mMyViewOne.Table = mdatasetTickets.Tables("tblTickets")

        Catch ex As Exception
            Throw New Exception("params are " & strSPName.ToString & " " & strParamName.ToString & " " & strParamValue.ToString & " error is " & ex.Message)
        End Try
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
            mdatasetOne.Clear()

            ' OPEN CONNECTION AND FILL DATASET
            mdbDataAdapter.Fill(mdatasetTicketsOthers, "tblTickets")

            ' copy dataset to dataview
            mMyView.Table = mdatasetTicketsOthers.Tables("tblTickets")

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

    Public Sub AddTicket(strReservationID As String, strAdvantageNumber As String, strJourneyID As String, strFlightNumber As String, strBaseFare As String, strAge As String)
        'defines array to put parameter names into
        Dim aryParamNames As New ArrayList
        Dim aryParamValues As New ArrayList

        'add parameter names to names array list
        aryParamNames.Add("@ReservationID")
        aryParamNames.Add("@AdvantageNumber")
        aryParamNames.Add("@JourneyID")
        aryParamNames.Add("@FlightNumber")
        aryParamNames.Add("@BaseFare")
        aryParamNames.Add("@Age")


        'add values to parameter values array list
        aryParamValues.Add(strReservationID)
        aryParamValues.Add(strAdvantageNumber)
        aryParamValues.Add(strJourneyID)
        aryParamValues.Add(strFlightNumber)
        aryParamValues.Add(strBaseFare)
        aryParamValues.Add(strAge)

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

    Public Sub MarkOnFlight(strTicketID As String)
        Dim aryParamNames As New ArrayList
        Dim aryParamValues As New ArrayList

        aryParamNames.Add("@TicketID")

        aryParamValues.Add(strTicketID)

        UseSPforInsertOrUpdateQuery("usp_Tickets_Mark_On_Flight", aryParamNames, aryParamValues)
    End Sub

    Public Function GetAge(strAdvantageNumber As String) As Integer

        RunSPwithOneParam("usp_TicketsClone_Get_One", "@AdvantageNumber", strAdvantageNumber)

        Return CInt(mdatasetOne.Tables("tblTickets").Rows(0).Item("Age"))

    End Function

    Public Sub DeactivateOneCustomersTicketsOnReservation(strReservationID As String, strAdvantageNumber As String)
        Dim aryNames As New ArrayList
        Dim aryValues As New ArrayList

        aryNames.Add("@ReservationID")
        aryNames.Add("@AdvantageNumber")

        aryValues.Add(strReservationID)
        aryValues.Add(strAdvantageNumber)

        UseSPforInsertOrUpdateQuery("usp_Tickets_Deactivate_Missed_Journey", aryNames, aryValues)
    End Sub

    Public Function GetMileage(strAdvantageNumber As String) As String

        RunSPwithOneParam("usp_CustomersClone_Get_Miles", "@AdvantageNumber", strAdvantageNumber)

        Return mdatasetOne.Tables("tblTickets").Rows(0).Item("Miles").ToString

    End Function

    Public Function GetBaseFare(strAdvantageNumber As String) As Integer

        RunSPwithOneParam("usp_TicketsClone_Get_One", "@AdvantageNumber", strAdvantageNumber)

        Return CInt(mdatasetOne.Tables("tblTickets").Rows(0).Item("BaseFareAtPurchase"))

    End Function

    Public Sub GetTicketsInReservationForPricing(strReservationID As String)
        RunSPwithOneParam("usp_Tickets_Get_By_Reservation_For_Pricing", "@ReservationID", strReservationID)

    End Sub

    Public Sub GetTicketsInReservation(strReservationID As String)
        'returns all tickets in a reservation
        RunSPwithOneParam("usp_Tickets_Get_By_Reservation", "@ReservationID", strReservationID)

    End Sub

    Public Sub FilterReservationByPaid()
        mMyViewOne.RowFilter = "PricePaid = 0 AND MilagePaid = 0"
    End Sub

    Public Sub GetTicketsInReservationOthers(strReservationID As String)
        'returns all tickets in a reservation
        RunSPwithOneParamOthers("usp_Tickets_Get_By_Reservation", "@ReservationID", strReservationID)

    End Sub



    Public Sub AddTicketPricesAndMiles(strPricePaid As String, strMileagePaid As String, strTicketID As String)
        'defines array to put parameter names into
        Dim aryParamNames As New ArrayList
        Dim aryParamValues As New ArrayList

        'add parameter names to names array list
        aryParamNames.Add("@TicketID")
        aryParamNames.Add("@MilagePaid")
        aryParamNames.Add("@PricePaid")

        'add values to parameter values array list
        aryParamValues.Add(strTicketID)

        aryParamValues.Add(strMileagePaid)
        aryParamValues.Add(strPricePaid)

        UseSPforInsertOrUpdateQuery("usp_TicketClone_Add_Price", aryParamNames, aryParamValues)

    End Sub

    Public Sub AddTicketPrices(strPricePaid As String, strTicketID As String)
        'defines array to put parameter names into
        Dim aryParamNames As New ArrayList
        Dim aryParamValues As New ArrayList

        'add parameter names to names array list
        aryParamNames.Add("@TicketID")
        aryParamNames.Add("@PricePaid")


        'add values to parameter values array list
        aryParamValues.Add(strTicketID)
        aryParamValues.Add(strPricePaid)


        UseSPforInsertOrUpdateQuery("usp_TicketsClone_Update_Price", aryParamNames, aryParamValues)

    End Sub

    Public Sub AddTicketMiles(strMilagePaid As String, strTicketID As String)
        'defines array to put parameter names into
        Dim aryParamNames As New ArrayList
        Dim aryParamValues As New ArrayList

        'add parameter names to names array list
        aryParamNames.Add("@TicketID")
        aryParamNames.Add("@MilagePaid")


        'add values to parameter values array list
        aryParamValues.Add(strTicketID)
        aryParamValues.Add(strMilagePaid)


        UseSPforInsertOrUpdateQuery("usp_TicketsClone_Update_Mileage", aryParamNames, aryParamValues)

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
        'Return: filtered dataview 
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

    Public Sub FilterByReservationID(strRes As String)
        MyView.RowFilter = "[ReservationID] = '" & strRes & "'"
    End Sub

    Public Sub FilterToGetUniqueTicket(strJourneyID As String, strAdvantageNumber As String)
        MyView.RowFilter = "[JourneyID] = '" & strJourneyID & "' AND [AdvantageNumber] = '" & strAdvantageNumber & "'"
    End Sub

    Public Sub FilterToGetOtherTickets(strJourneyID As String, strAdvantageNumber As String)
        MyViewOthers.RowFilter = "[JourneyID] NOT IN ( '" & strJourneyID & "') OR [AdvantageNumber] NOT IN ('" & strAdvantageNumber & "')"
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

    Public Sub GetALLOthersTicketsWithDateUsingSP()
        'Author: Ben Shadburne
        'Purpose: runs xxxxx procedure
        'Arguments: na
        'Return: na
        'Date: 03/18/2014


        RunProcedureOthers("usp_Tickets_Get_All_With_Date")

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

        UseSP("usp_Tickets_Get_FlightNumber_Active", mdatasetFlight, mMyViewFlight, "tblFlightNumber", aryParamName, aryParamValue)


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

    'Get the Gross Revenue
    Public Sub GetGrossRevenueAndSeatCount()
        'Purpose: Run the stored procedure to get the gross revenue for each flight number by journey
        'Author: Dennis Phelan
        'Inputs: None
        'Outputs: the gross revenue query output
        'Date Created: April 27, 2014
        'Date Last Modified: April 27, 2014

        'Run the procedure
        RunProcedure("usp_ShowPricePaidAndSeatNumberClone_Get_GrossRevenue_And_SeatCount")

    End Sub

    'Filter the Gross Revenue and Seat Count by dates
    Public Function RevenueSeatFilterByDate(datLowerDate As Date, datUpperDate As Date) As String
        'Purpose: Check to see what dates the employee wants to look for
        'Author: Dennis Phelan
        'Input: strLowerDate and strUpperDate
        'Output: the gridview will meet the employee's requirements
        'Date Created: April 27, 2014
        'Date Last Modified: April 28, 2014

        'Declare strDateFilterStatement
        Dim strDateFilterStatement As String

        'Check to make sure if there is a lower date; if not, just filter for the upper date
        If datLowerDate = Nothing And datUpperDate <> Nothing Then
            strDateFilterStatement = "FlightDate = '" & datUpperDate & "'"

            'If lower date is there, check to make sure there is an upper date; if not, just filter for the upper date
        ElseIf datUpperDate = Nothing And datLowerDate <> Nothing Then
            strDateFilterStatement = "FlightDate = '" & datLowerDate & "'"

            'If lower and upper date are there, filter for both
        Else
            strDateFilterStatement = "FlightDate >= '" & datLowerDate & "' AND FlightDate <= '" & datUpperDate & "'"
        End If

        Return strDateFilterStatement
    End Function

    Public Sub GetRevenueOrSeatCountOrBoth(ByVal intIndex As Integer)
        'Purpose: Check to see if the employee wants revenue or seat count or both
        'Author: Dennis Phelan
        'Input: intIndex, the index of the radio button list
        'Output: the gridview will meet the employee's requirements
        'Date Created: April 28, 2014
        'Date Last Modified: April 28, 2014

        'If 0, then that is just Seat Count
        If intIndex = 0 Then
            RunProcedure("usp_ShowPricePaidAndSeatNumberClone_Get_SeatCount")

            'If it is 1 then it is just Revenue
        ElseIf intIndex = 1 Then
            RunProcedure("usp_ShowPricePaidAndSeatNumberClone_Get_GrossRevenue")

            'If neither 0 or 1, then must be 2, and most be both
        Else
            RunProcedure("usp_ShowPricePaidAndSeatNumberClone_Get_GrossRevenue_And_SeatCount")
        End If
    End Sub

    Public Sub FilterClass(ByVal intIndexClass As Integer, ByVal intIndexSearch As Integer)
        'Purpose: Check to see if the employee wants revenue or seat count or both
        'Author: Dennis Phelan
        'Input: intIndex, the index of the radio button list
        'Output: the gridview will meet the employee's requirements
        'Date Created: April 28, 2014
        'Date Last Modified: April 28, 2014

        'If nothing is selected, Carry on trying to figure out seat, revenue, or both
        If intIndexClass = -1 Then
            'If 0, then that is just Seat Count
            If intIndexSearch = 0 Then
                RunProcedure("usp_ShowPricePaidAndSeatNumberClone_Get_SeatCount")

                'If it is 1 then it is just Revenue
            ElseIf intIndexSearch = 1 Then
                RunProcedure("usp_ShowPricePaidAndSeatNumberClone_Get_GrossRevenue")

                'If neither 0 or 1, then must be 2, and most be both
            Else
                RunProcedure("usp_ShowPricePaidAndSeatNumberClone_Get_GrossRevenue_And_SeatCount")
            End If

            'If Economy is selected, look up the economies for them all
        ElseIf intIndexClass = 0 Then
            'If 0, then that is just Seat Count
            If intIndexSearch = 0 Then
                RunProcedure("usp_ShowPricePaidAndSeatNumberClone_Get_SeatCount_Economy")

                'If it is 1 then it is just Revenue
            ElseIf intIndexSearch = 1 Then
                RunProcedure("usp_ShowPricePaidAndSeatNumberClone_Get_GrossRevenue_Economy")

                'If neither 0 or 1, then must be 2, and most be both
            Else
                RunProcedure("usp_ShowPricePaidAndSeatNumberClone_Get_GrossRevenue_And_SeatCount_Economy")
            End If

            'If First Class is selected, look up the first class for them all
        ElseIf intIndexClass = 1 Then
            'If 0, then that is just Seat Count
            If intIndexSearch = 0 Then
                RunProcedure("usp_ShowPricePaidAndSeatNumberClone_Get_SeatCount_FirstClass")

                'If it is 1 then it is just Revenue
            ElseIf intIndexSearch = 1 Then
                RunProcedure("usp_ShowPricePaidAndSeatNumberClone_Get_GrossRevenue_FirstClass")

                'If neither 0 or 1, then must be 2, and most be both
            Else
                RunProcedure("usp_ShowPricePaidAndSeatNumberClone_Get_GrossRevenue_And_SeatCount_FirstClass")
            End If
        End If
    End Sub

    'Get all of the cities
    Public Sub GetAllCities()
        'Purpose: Get all of the cities
        'Author: Dennis Phelan
        'Input: None
        'Output: Get the values ready to load the ddl with
        'Date Created: April 29, 2014
        'Date Last Modified: April 29, 2014

        'Run the procedure to get all of the cities
        RunProcedure("usp_AirportClone_Get_AirportCode")

    End Sub


    'Filter by city
    Public Function FilterDepartureCity(strCity As String) As String
        'Purpose: Check to see what cities the employee wants to look for
        'Author: Dennis Phelan
        'Input: strCity
        'Output: the gridview will meet the employee's requirements
        'Date Created: April 29, 2014
        'Date Last Modified: April 29, 2014

        'Declare the filter statement
        Dim strFilterStatement As String

        'Check to make sure that All is not selected. If so, don't filter
        If strCity = "ALL" Then
            strFilterStatement = Nothing

            'If ALL is not selected, filter
        Else
            strFilterStatement = "DepartureCity = '" & strCity & "'"
        End If

        Return strFilterStatement
    End Function

    'Filter by city
    Public Function FilterEndCity(strCity As String) As String
        'Purpose: Check to see what cities the employee wants to look for
        'Author: Dennis Phelan
        'Input: strCity
        'Output: the gridview will meet the employee's requirements
        'Date Created: April 29, 2014
        'Date Last Modified: April 29, 2014

        'Declare the filter statement
        Dim strFilterStatement As String

        'Check to make sure that All is not selected. If so, don't filter
        If strCity = "ALL" Then
            strFilterStatement = ""

            'If ALL is not selected, filter
        Else
            strFilterStatement = "EndCity = '" & strCity & "'"
        End If

        'Return the filterstatement
        Return strFilterStatement
    End Function

    Public Sub RowFilter(ByVal strFilter As String)
        'Purpose: Take a filter statement and run it through the row filter
        'Author: Dennis Phelan
        'Input: strFilter
        'Output: the gridview will meet the employee's requirements
        'Date Created: April 29, 2014
        'Date Last Modified: April 29, 2014

        'Check to make sure that there is a string filter
        If strFilter = "" Then
            'Just set the row filter = to nothing
            mMyView.RowFilter = Nothing
        Else
            'Filter the row
            mMyView.RowFilter = strFilter
        End If
    End Sub

    Public Sub FindTicketsByAdvantageNumber(strAdvantageNumber As String)
        RunSPwithOneParam("usp_TicketsClone_Find_By_AdvantageNumber", "@advantagenumber", strAdvantageNumber)
    End Sub

    Public Sub FindTicketsByReservationID(strReservationID As String)
        RunSPwithOneParamReservation("usp_TicketsClone_Select_Distinct_Customers", "@reservationid", strReservationID)
    End Sub

    Public Sub InactivateTicketsByAdvantageNumber(strAdvantageNumber As String)
        Dim aryTicketNames As New ArrayList
        Dim aryTicketValues As New ArrayList

        aryTicketNames.Add("@advantagenumber")
        aryTicketValues.Add(strAdvantageNumber)

        UseSPforInsertOrUpdateQuery("usp_Inactivate_By_AdvantageNumber", aryTicketNames, aryTicketValues)
    End Sub



End Class

