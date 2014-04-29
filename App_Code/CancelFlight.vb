Option Strict On
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Mail
Public Class CancelFlight


    'setting up db, dim connection, adapter, query, dataset
    Dim mMyView As New DataView
    Dim mMyViewOne As New DataView
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size=4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False;initial catalog=mis333k_20142_Team06;user id=msbcf819;password=Databasepassword5"
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdatasetCancel As New DataSet
    Dim mQueryString As String
    Dim mDatasetOne As New DataSet
    Dim mDatasettwo As New DataSet
    Dim mDatasetthree As New DataSet

    Dim DBJourneys As New DBjourneyclone
    Dim DBCustomer As New DBCustomersClone
    Dim DBTickets As New DBTickets
    Dim DBFlights As New DBFlightsClone
    Dim DBReservations As New DBReservations
    Dim DBCancelReservation As New CancelReservation




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

    Public Sub RunSPwithOneParamReservations(ByVal strSPName As String, ByVal strParamName As String, ByVal strParamValue As String)
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
            mDatasetOne.Clear()

            ' OPEN CONNECTION AND FILL DATASET
            mdbDataAdapter.Fill(mDatasetOne, "tblTickets")

            ' copy dataset to dataview
            mMyViewOne.Table = mDatasetOne.Tables("tblTickets")

        Catch ex As Exception
            Throw New Exception("params are " & strSPName.ToString & " " & strParamName.ToString & " " & strParamValue.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub InactivateFlightAllDays(strFlightNumber As String)
        'get all journeys
        DBJourneys.GetJourneysByFlightNumber(strFlightNumber)
        'loop through each journey
        For i = 0 To DBJourneys.MyDataSet.Tables("tblJourneysClone").Rows.Count - 1
            'check if flight number of journey matches flight number we're looking for
            'If DBJourneys.MyDataSet.Tables("tblJourneyClone").Rows(i).Item("FlightNumber").ToString = strFlightNumber Then

            'if it does, inactivate the journey
            'create variables for things
            Dim strJourneyID As String
            Dim aryJourneyNames As New ArrayList
            Dim aryJourneyValues As New ArrayList

            'set journey ID to the current journey ID
            strJourneyID = DBJourneys.MyDataSet.Tables("tblJourneysClone").Rows(i).Item("JourneyID").ToString
            aryJourneyNames.Add("@flightnumber")
            aryJourneyValues.Add(strFlightNumber)

            'run SP to make current journey ID inactive
            DBJourneys.InactivateJourneysByFlightNumber(strFlightNumber)

            'run SP to search reservations by current journey ID
            DBReservations.RunSPwithOneParam("usp_ReservationsClone_Find_By_Journey_ID", "@journeyid", strJourneyID)

            'loop through found reservations
            For j = 0 To DBReservations.MyDataSet.Tables("tblReservationsClone").Rows.Count - 1
                'update found reservations to inactive
                'create variables for things
                Dim strReservationID As String
                Dim aryReservationNames As New ArrayList
                Dim aryReservationValues As New ArrayList

                'set reservation id to the current reservation id
                strReservationID = DBReservations.MyDataSet.Tables("tblReservationsClone").Rows(j).Item("ReservationID").ToString
                aryReservationNames.Add("@reservationid")
                aryReservationValues.Add(strReservationID)
                'run stored procedure to update reservation to inactive
                UseSPforInsertOrUpdateQuery("usp_ReservationsClone_Inactivate_By_ReservationID", aryReservationNames, aryReservationValues)

                'find all journeys affected 
                DBJourneys.RunSPwithOneParam("usp_ReservationsClone_Get_Journeys", "@ReservationID", strReservationID)

                'loop thorugh affected journeys
                For k = 0 To DBJourneys.MyDataSet.Tables("tblJourneys").Rows.Count - 1
                    'deactive ticket, return mileage and money for entire reservation
                    DBCancelReservation.ReturnMilesAndDeactivateTicket(strReservationID)
                    'make all seats on reservation 
                    DBCancelReservation.ChangeSeatStatus(strReservationID)
                    'find the customers on each reservation
                    DBTickets.RunSPwithOneParam("usp_TicketsClone_Find_Customers_By_ReservationID", "@reservationid", strReservationID)
                    'For l = 0 To DBTickets.MyDataSetOne.Tables("tblTickets").Rows.Count - 1
                    '    Dim Msg As MailMessage = New MailMessage()
                    '    Dim MailObj As New SmtpClient("smtp.mccombs.utexas.edu")
                    '    Msg.From = New MailAddress("mis333kgroup6@gmail.com", "Jace Barton")
                    '    Msg.To.Add(New MailAddress(DBTickets.MyDataSetOne.Tables("tblTickets").Rows(l).Item("Email").ToString, DBTickets.MyDataSetOne.Tables("tblTickets").Rows(l).Item("FirstName").ToString + " " + DBTickets.MyDataSetOne.Tables("tblTickets").Rows(l).Item("LastName").ToString))
                    '    Msg.IsBodyHtml = False
                    '    Msg.Body = "Hello " & DBTickets.MyDataSetOne.Tables("tblTickets").Rows(l).Item("FirstName").ToString & ", " & vbCrLf & vbCrLf & "Unfortunately, we needed to cancel your reservation on journey #" & DBTickets.MyDataSetOne.Tables("tblTickets").Rows(l).Item("JourneyID").ToString & ". We apologize for any inconvenience this may cause. Please visit our website to make a new reservation." & vbCrLf & vbCrLf & "Best," & vbCrLf & "The Penguin Air Team"
                    '    Msg.Subject = "Flight Cancellation"
                    '    MailObj.Send(Msg)
                    '    Msg.To.Clear()
                    'Next

                Next
            Next


            'End If
        Next

    End Sub

    Public Sub InactivateFlightWithDay(strFlightNumber As String, strDay As String)
        'get all journeys
        DBJourneys.GetJourneysByFlightNumberAndDay(strFlightNumber, strDay)
        'loop through each journey
        For i = 0 To DBJourneys.MyDataSet.Tables("tblJourneysClone").Rows.Count - 1
            'check if flight number of journey matches flight number we're looking for
            'If DBJourneys.MyDataSet.Tables("tblJourneysClone").Rows(i).Item("FlightNumber").ToString = strFlightNumber Then

            'if it does, inactivate the journey
            'create variables for things
            Dim strJourneyID As String
            Dim aryJourneyNames As New ArrayList
            Dim aryJourneyValues As New ArrayList

            'set journey ID to the current journey ID
            strJourneyID = DBJourneys.MyDataSet.Tables("tblJourneysClone").Rows(i).Item("JourneyID").ToString
            aryJourneyNames.Add("@flightnumber")
            aryJourneyValues.Add(strFlightNumber)

            'run SP to make current journey ID inactive
            DBJourneys.InactivateJourneysByFlightNumberAndDay(strFlightNumber, strDay)

            'run SP to search reservations by current journey ID
            DBReservations.RunSPwithOneParam("usp_ReservationsClone_Find_By_Journey_ID", "@journeyid", strJourneyID)

            'loop through found reservations
            For j = 0 To DBReservations.MyDataSet.Tables("tblReservationsClone").Rows.Count - 1
                'update found reservations to inactive
                'create variables for things
                Dim strReservationID As String
                Dim aryReservationNames As New ArrayList
                Dim aryReservationValues As New ArrayList

                'set reservation id to the current reservation id
                strReservationID = DBReservations.MyDataSet.Tables("tblReservationsClone").Rows(j).Item("ReservationID").ToString
                aryReservationNames.Add("@reservationid")
                aryReservationValues.Add(strReservationID)
                'run stored procedure to update reservation to inactive
                UseSPforInsertOrUpdateQuery("usp_ReservationsClone_Inactivate_By_ReservationID", aryReservationNames, aryReservationValues)

                'find all journeys affected 
                DBJourneys.RunSPwithOneParam("usp_ReservationsClone_Get_Journeys", "@ReservationID", strReservationID)

                'loop thorugh affected journeys
                For k = 0 To DBJourneys.MyDataSet.Tables("tblJourneys").Rows.Count - 1
                    'deactive ticket, return mileage and money for entire reservation
                    DBCancelReservation.ReturnMilesAndDeactivateTicket(strReservationID)
                    'make all seats on reservation 
                    DBCancelReservation.ChangeSeatStatus(strReservationID)
                    'find the customers on each reservation
                    DBTickets.RunSPwithOneParam("usp_TicketsClone_Find_Customers_By_ReservationID", "@reservationid", strReservationID)
                    'For l = 0 To DBTickets.MyDataSetOne.Tables("tblTickets").Rows.Count - 1
                    '    Dim Msg As MailMessage = New MailMessage()
                    '    Dim MailObj As New SmtpClient("smtp.mccombs.utexas.edu")
                    '    Msg.From = New MailAddress("mis333kgroup6@gmail.com", "Jace Barton")
                    '    Msg.To.Add(New MailAddress(DBTickets.MyDataSetOne.Tables("tblTickets").Rows(l).Item("Email").ToString, DBTickets.MyDataSetOne.Tables("tblTickets").Rows(l).Item("FirstName").ToString + " " + DBTickets.MyDataSetOne.Tables("tblTickets").Rows(l).Item("LastName").ToString))
                    '    Msg.IsBodyHtml = False
                    '    Msg.Body = "Hello " & DBTickets.MyDataSetOne.Tables("tblTickets").Rows(l).Item("FirstName").ToString & ", " & vbCrLf & vbCrLf & "Unfortunately, we needed to cancel your reservation on journey #" & DBTickets.MyDataSetOne.Tables("tblTickets").Rows(l).Item("JourneyID").ToString & ". We apologize for any inconvenience this may cause. Please visit our website to make a new reservation." & vbCrLf & vbCrLf & "Best," & vbCrLf & "The Penguin Air Team"
                    '    Msg.Subject = "Flight Cancellation"
                    '    MailObj.Send(Msg)
                    '    Msg.To.Clear()
                    'Next

                Next
            Next


            'End If
        Next

    End Sub

    Public Sub InactivateSpecificJourney(strJourneyID As String)



        'run SP to make current journey ID inactive
        DBJourneys.InactivateJourneyByJourneyID(strJourneyID)

        'run SP to search reservations by current journey ID
        DBReservations.RunSPwithOneParam("usp_ReservationsClone_Find_By_Journey_ID", "@journeyid", strJourneyID)

        'loop through found reservations
        For j = 0 To DBReservations.MyDataSet.Tables("tblReservationsClone").Rows.Count - 1
            'update found reservations to inactive
            'create variables for things
            Dim strReservationID As String
            Dim aryReservationNames As New ArrayList
            Dim aryReservationValues As New ArrayList

            'set reservation id to the current reservation id
            strReservationID = DBReservations.MyDataSet.Tables("tblReservationsClone").Rows(j).Item("ReservationID").ToString
            aryReservationNames.Add("@reservationid")
            aryReservationValues.Add(strReservationID)
            'run stored procedure to update reservation to inactive
            UseSPforInsertOrUpdateQuery("usp_ReservationsClone_Inactivate_By_ReservationID", aryReservationNames, aryReservationValues)

            'find all journeys affected 
            DBJourneys.RunSPwithOneParam("usp_ReservationsClone_Get_Journeys", "@ReservationID", strReservationID)

            'loop thorugh affected journeys
            For k = 0 To DBJourneys.MyDataSet.Tables("tblJourneys").Rows.Count - 1
                'deactive ticket, return mileage and money for entire reservation
                DBCancelReservation.ReturnMilesAndDeactivateTicket(strReservationID)
                'make all seats on reservation 
                DBCancelReservation.ChangeSeatStatus(strReservationID)
                'find the customers on each reservation
                DBTickets.RunSPwithOneParam("usp_TicketsClone_Find_Customers_By_ReservationID", "@reservationid", strReservationID)
                'For l = 0 To DBTickets.MyDataSetOne.Tables("tblTickets").Rows.Count - 1
                '    Dim Msg As MailMessage = New MailMessage()
                '    Dim MailObj As New SmtpClient("smtp.mccombs.utexas.edu")
                '    Msg.From = New MailAddress("mis333kgroup6@gmail.com", "Jace Barton")
                '    Msg.To.Add(New MailAddress(DBTickets.MyDataSetOne.Tables("tblTickets").Rows(l).Item("Email").ToString, DBTickets.MyDataSetOne.Tables("tblTickets").Rows(l).Item("FirstName").ToString + " " + DBTickets.MyDataSetOne.Tables("tblTickets").Rows(l).Item("LastName").ToString))
                '    Msg.IsBodyHtml = False
                '    Msg.Body = "Hello " & DBTickets.MyDataSetOne.Tables("tblTickets").Rows(l).Item("FirstName").ToString & ", " & vbCrLf & vbCrLf & "Unfortunately, we needed to cancel your reservation on journey #" & DBTickets.MyDataSetOne.Tables("tblTickets").Rows(l).Item("JourneyID").ToString & ". We apologize for any inconvenience this may cause. Please visit our website to make a new reservation." & vbCrLf & vbCrLf & "Best," & vbCrLf & "The Penguin Air Team"
                '    Msg.Subject = "Flight Cancellation"
                '    MailObj.Send(Msg)
                '    Msg.To.Clear()
                'Next

            Next
        Next

    End Sub

End Class

