﻿Option Strict On
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Public Class ClassCrewScheduling
    Dim mMyViewCaptain As New DataView
    Dim mMyViewCoCaptain As New DataView
    Dim mMyViewCabin As New DataView
    Dim mMyViewCaptainBusy As New DataView
    Dim mMyViewCoCaptainBusy As New DataView
    Dim mMyViewCabinBusy As New DataView
    Dim mMyView As New DataView
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size=4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False;initial catalog=mis333k_20142_Team06;user id=msbcf819;password=Databasepassword5"
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdatasetCaptain As New DataSet
    Dim mdatasetCoCaptain As New DataSet
    Dim mdatasetCabin As New DataSet
    Dim mdatasetCaptainBusy As New DataSet
    Dim mdatasetCoCaptainBusy As New DataSet
    Dim mdatasetCabinBusy As New DataSet
    Dim mQueryString As String

    Public Sub RunProcedureCaptain(ByVal strName As String)
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
            Me.mdatasetCaptain.Clear()
            'open conneciton and fill dataset
            mdbDataAdapter.Fill(mdatasetCaptain, "tblCaptain")
            'copy dataset to dataview
            mMyView.Table = mdatasetCaptain.Tables("tblCaptain")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strName.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub RunProcedureCoCaptain(ByVal strName As String)
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
            Me.mdatasetCoCaptain.Clear()
            'open conneciton and fill dataset
            mdbDataAdapter.Fill(mdatasetCoCaptain, "tblCoCaptain")
            'copy dataset to dataview
            mMyView.Table = mdatasetCoCaptain.Tables("tblCoCaptain")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strName.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub RunProcedureCabin(ByVal strName As String)
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
            Me.mdatasetCabin.Clear()
            'open conneciton and fill dataset
            mdbDataAdapter.Fill(mdatasetCabin, "tblCabin")
            'copy dataset to dataview
            mMyView.Table = mdatasetCabin.Tables("tblCabin")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strName.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub RunSPwithOneParamCaptain(ByVal strSPName As String, ByVal strParamName As String, ByVal strParamValue As String)
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
            Me.mdatasetCaptain.Clear()

            ' OPEN CONNECTION AND FILL DATASET
            mdbDataAdapter.Fill(mdatasetCaptain, "tblCaptain")

            ' copy dataset to dataview
            mMyView.Table = mdatasetCaptain.Tables("tblCaptain")

        Catch ex As Exception
            Throw New Exception("params are " & strSPName.ToString & " " & strParamName.ToString & " " & strParamValue.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public ReadOnly Property MyDataSetCoCaptain() As DataSet
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: airportclone dataview
        'Date: 03/18/2014

        Get
            Return mdatasetCoCaptain

        End Get
    End Property

    Public ReadOnly Property MyDataSetCaptain() As DataSet
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: airportclone dataview
        'Date: 03/18/2014

        Get
            Return mdatasetCaptain

        End Get
    End Property

    Public ReadOnly Property MyView() As DataView
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: airportclone dataview
        'Date: 03/18/2014

        Get
            Return mMyView

        End Get
    End Property

    Public Sub RunSPwithOneParamCoCaptain(ByVal strSPName As String, ByVal strParamName As String, ByVal strParamValue As String)
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
            Me.mdatasetCoCaptain.Clear()

            ' OPEN CONNECTION AND FILL DATASET
            mdbDataAdapter.Fill(mdatasetCoCaptain, "tblCoCaptain")

            ' copy dataset to dataview
            mMyView.Table = mdatasetCoCaptain.Tables("tblCoCaptain")

        Catch ex As Exception
            Throw New Exception("params are " & strSPName.ToString & " " & strParamName.ToString & " " & strParamValue.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub RunSPwithOneParamCabin(ByVal strSPName As String, ByVal strParamName As String, ByVal strParamValue As String)
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
            Me.mdatasetCabin.Clear()

            ' OPEN CONNECTION AND FILL DATASET
            mdbDataAdapter.Fill(mdatasetCabin, "tblCabin")

            ' copy dataset to dataview
            mMyView.Table = mdatasetCabin.Tables("tblCabin")

        Catch ex As Exception
            Throw New Exception("params are " & strSPName.ToString & " " & strParamName.ToString & " " & strParamValue.ToString & " error is " & ex.Message)
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

    Public Sub AddCrew(strCaptain As String, strCoCaptain As String, strCabin As String, strJourneyID As String)
        Dim aryNames As New ArrayList
        Dim aryValues As New ArrayList

        aryNames.Add("@Captain")
        aryNames.Add("@CoCaptain")
        aryNames.Add("@Cabin")
        aryNames.Add("@JourneyID")

        aryValues.Add(strCaptain)
        aryValues.Add(strCoCaptain)
        aryValues.Add(strCabin)
        aryValues.Add(strJourneyID)

        UseSPforInsertOrUpdateQuery("usp_Journey_Add_Crew", aryNames, aryValues)
    End Sub

    Public Sub GetCaptainBusy(strCaptain As String, strDate As String)
        Dim aryNames As New ArrayList
        Dim aryValues As New ArrayList

        aryNames.Add("@Captain")
        aryNames.Add("@FlightDate")

        aryValues.Add(strCaptain)
        aryValues.Add(strDate)

        UseSP("usp_Journey_Captain", mdatasetCaptainBusy, mMyViewCaptainBusy, "tblCaptain", aryNames, aryValues)

    End Sub

    Public Sub GetCoCaptainBusy(strCoCaptain As String, strDate As String)
        Dim aryNames As New ArrayList
        Dim aryValues As New ArrayList

        aryNames.Add("@CoCaptain")
        aryNames.Add("@FlightDate")

        aryValues.Add(strCoCaptain)
        aryValues.Add(strDate)

        UseSP("usp_Journey_CoCaptain", mdatasetCoCaptainBusy, mMyViewCoCaptainBusy, "tblCoCaptain", aryNames, aryValues)

    End Sub

    Public Sub GetCabinBusy(strCabin As String, strDate As String)
        Dim aryNames As New ArrayList
        Dim aryValues As New ArrayList

        aryNames.Add("@Cabin")
        aryNames.Add("@FlightDate")

        aryValues.Add(strCabin)
        aryValues.Add(strDate)

        UseSP("usp_Journey_Cabin", mdatasetCabinBusy, mMyViewCabinBusy, "tblCabin", aryNames, aryValues)

    End Sub

    Public Sub GetCaptains()
        RunProcedureCaptain("usp_Employees_Get_Captains")
    End Sub

    Public Sub GetCoCaptains()
        RunProcedureCoCaptain("usp_Employees_Get_CoCaptains")
    End Sub

    Public Sub GetCabin()
        RunProcedureCabin("usp_Employees_Get_Cabin")
    End Sub

    Public Function FindAvailableCaptains(strDate As String, intDepartureTime As Integer, intArriveTime As Integer) As ArrayList
        Dim intCaptains As Integer
        Dim aryCaptains As New ArrayList
        Dim bolAvailable As Boolean
        'get all captains who are active employees
        GetCaptains()
        'this is the number of captains there are
        intCaptains = mdatasetCaptain.Tables("tblCaptain").Rows.Count - 1

        bolAvailable = True

        'leep through all captains
        For i = 0 To intCaptains
            'check to see if captain is on any journeys on that date
            GetCaptainBusy(mdatasetCaptain.Tables("tblCaptain").Rows(i).Item("EmpID").ToString, strDate)
            'if there are no records returned for that date
            If mdatasetCaptainBusy.Tables("tblCaptain").Rows.Count <> 0 Then
                'captain has other flights that day 
                'run a loop to see if they are busy at the times we are looking for
                For j = 0 To (mdatasetCaptainBusy.Tables("tblCaptain").Rows.Count - 1)
                    'checks to see if inputted departure is between start and end of journey they are on
                    'if inputtedd departure is less than departure AND inputted arrival is less than departure
                    If intDepartureTime < CInt(mdatasetCaptainBusy.Tables("tblCaptain").Rows(j).Item("DepartureTime")) And _
                        intArriveTime <= CInt(mdatasetCaptainBusy.Tables("tblCaptain").Rows(j).Item("DepartureTime")) Then
                        'the captain is free at this time
                        'dont do anything

                        'check if inputted departure is after departure on other flight AND inputted departure
                        'check to see if inputted arrive time is between start and end of journey we are looping through
                    ElseIf intDepartureTime > CInt(mdatasetCaptainBusy.Tables("tblCaptain").Rows(j).Item("DepartureTime")) And _
                        intDepartureTime > CInt(mdatasetCaptainBusy.Tables("tblCaptain").Rows(j).Item("ArriveTime")) Then
                        
                        'the captain is free
                    Else
                        'captain is busy, don't add them. 
                        bolAvailable = False

                    End If

                Next
                ' check if bolAvailable is still true after going through all journeys
                If bolAvailable = True Then
                    'captain is free despite all other journeys they are one
                    aryCaptains.Add(mdatasetCaptain.Tables("tblCaptain").Rows(i).Item("EmpID").ToString)
                Else
                    'one of the captain's journeys conflicted with the times we needed
                    'dont add them
                End If
            Else
                'Captain is free all day
                aryCaptains.Add(mdatasetCaptain.Tables("tblCaptain").Rows(i).Item("EmpID").ToString)
            End If
            'reset the counter
            bolAvailable = True
        Next

        Return aryCaptains
    End Function

    Public Function FindAvailableCoCaptains(strDate As String, intDepartureTime As Integer, intArriveTime As Integer) As ArrayList
        Dim intCoCaptains As Integer
        Dim aryCoCaptains As New ArrayList
        Dim bolAvailable As Boolean
        'get all CoCaptains who are active employees
        GetCoCaptains()
        'this is the number of CoCaptains there are
        intCoCaptains = mdatasetCoCaptain.Tables("tblCoCaptain").Rows.Count - 1

        bolAvailable = True

        'leep through all CoCaptains
        For i = 0 To intCoCaptains
            'check to see if CoCaptain is on any journeys on that date
            GetCoCaptainBusy(mdatasetCoCaptain.Tables("tblCoCaptain").Rows(i).Item("EmpID").ToString, strDate)
            'if there are no records returned for that date
            If mdatasetCoCaptainBusy.Tables("tblCoCaptain").Rows.Count <> 0 Then
                'CoCaptain has other flights that day 
                'run a loop to see if they are busy at the times we are looking for
                For j = 0 To (mdatasetCoCaptainBusy.Tables("tblCoCaptain").Rows.Count - 1)
                    'checks to see if inputted departure is between start and end of journey they are on
                    'if inputtedd departure is less than departure AND inputted arrival is less than departure
                    If intDepartureTime < CInt(mdatasetCoCaptainBusy.Tables("tblCoCaptain").Rows(j).Item("DepartureTime")) And _
                        intArriveTime <= CInt(mdatasetCoCaptainBusy.Tables("tblCoCaptain").Rows(j).Item("DepartureTime")) Then
                        'the CoCaptain is free at this time
                        'dont do anything

                        'check if inputted departure is after departure on other flight AND inputted departure
                        'check to see if inputted arrive time is between start and end of journey we are looping through
                    ElseIf intDepartureTime > CInt(mdatasetCoCaptainBusy.Tables("tblCoCaptain").Rows(j).Item("DepartureTime")) And _
                        intDepartureTime > CInt(mdatasetCoCaptainBusy.Tables("tblCoCaptain").Rows(j).Item("ArriveTime")) Then

                        'the CoCaptain is free
                    Else
                        'CoCaptain is busy, don't add them. 
                        bolAvailable = False

                    End If

                Next
                ' check if bolAvailable is still true after going through all journeys
                If bolAvailable = True Then
                    'CoCaptain is free despite all other journeys they are one
                    aryCoCaptains.Add(mdatasetCoCaptain.Tables("tblCoCaptain").Rows(i).Item("EmpID").ToString)
                Else
                    'one of the CoCaptain's journeys conflicted with the times we needed
                    'dont add them
                End If
            Else
                'CoCaptain is free all day
                aryCoCaptains.Add(mdatasetCoCaptain.Tables("tblCoCaptain").Rows(i).Item("EmpID").ToString)
            End If
            'reset the counter
            bolAvailable = True
        Next

        Return aryCoCaptains
    End Function

    Public Function FindAvailableCabins(strDate As String, intDepartureTime As Integer, intArriveTime As Integer) As ArrayList
        Dim intCabins As Integer
        Dim aryCabins As New ArrayList
        Dim bolAvailable As Boolean
        'get all Cabins who are active employees
        GetCabin()
        'this is the number of Cabins there are
        intCabins = mdatasetCabin.Tables("tblCabin").Rows.Count - 1

        bolAvailable = True

        'leep through all Cabins
        For i = 0 To intCabins
            'check to see if Cabin is on any journeys on that date
            GetCabinBusy(mdatasetCabin.Tables("tblCabin").Rows(i).Item("EmpID").ToString, strDate)
            'if there are no records returned for that date
            If mdatasetCabinBusy.Tables("tblCabin").Rows.Count <> 0 Then
                'Cabin has other flights that day 
                'run a loop to see if they are busy at the times we are looking for
                For j = 0 To (mdatasetCabinBusy.Tables("tblCabin").Rows.Count - 1)
                    'checks to see if inputted departure is between start and end of journey they are on
                    'if inputtedd departure is less than departure AND inputted arrival is less than departure
                    If intDepartureTime < CInt(mdatasetCabinBusy.Tables("tblCabin").Rows(j).Item("DepartureTime")) And _
                        intArriveTime <= CInt(mdatasetCabinBusy.Tables("tblCabin").Rows(j).Item("DepartureTime")) Then
                        'the Cabin is free at this time
                        'dont do anything

                        'check if inputted departure is after departure on other flight AND inputted departure
                        'check to see if inputted arrive time is between start and end of journey we are looping through
                    ElseIf intDepartureTime > CInt(mdatasetCabinBusy.Tables("tblCabin").Rows(j).Item("DepartureTime")) And _
                        intDepartureTime > CInt(mdatasetCabinBusy.Tables("tblCabin").Rows(j).Item("ArriveTime")) Then

                        'the Cabin is free
                    Else
                        'Cabin is busy, don't add them. 
                        bolAvailable = False

                    End If

                Next
                ' check if bolAvailable is still true after going through all journeys
                If bolAvailable = True Then
                    'Cabin is free despite all other journeys they are one
                    aryCabins.Add(mdatasetCabin.Tables("tblCabin").Rows(i).Item("EmpID").ToString)
                Else
                    'one of the Cabin's journeys conflicted with the times we needed
                    'dont add them
                End If
            Else
                'Cabin is free all day
                aryCabins.Add(mdatasetCabin.Tables("tblCabin").Rows(i).Item("EmpID").ToString)
            End If
            'reset the counter
            bolAvailable = True
        Next

        Return aryCabins
    End Function

    Public Sub GetCrewByJourney(strJourneyID As String)
        RunSPwithOneParamCoCaptain("usp_Journey_Get_Crew", "@JourneyID", strJourneyID)
    End Sub

    Public Sub GetManifest(strJourneyID As String)
        RunSPwithOneParamCaptain("usp_Journey_Get_Manifest", "@JourneyID", strJourneyID)
    End Sub

    Public Sub GetSchedule(intEmpType As Integer, strEmpID As String, strSQLDate As String)

        If intEmpType = 103 Then
            'this is a captain

            GetCaptainSchedule(strEmpID, strSQLDate)
        Else
            If intEmpType = 104 Then
                'this is a cocaptain
                GetCoCaptainSchedule(strEmpID, strSQLDate)
            Else
                GetCabinSchedule(strEmpID, strSQLDate)
            End If
        End If


    End Sub

    Public Sub GetCaptainSchedule(strEmpID As String, strSQLDate As String)
        Dim aryNames As New ArrayList
        Dim aryValues As New ArrayList

        aryNames.Add("@EmpID")
        aryNames.Add("@Date")

        aryValues.Add(strEmpID)
        aryValues.Add(strSQLDate)

        UseSP("usp_Journey_Get_Captain_Schedule", mdatasetCaptain, mMyViewCaptain, "tblSchedule", aryNames, aryValues)
    End Sub

    Public Sub GetCoCaptainSchedule(strEmpID As String, strSQLDate As String)
        Dim aryNames As New ArrayList
        Dim aryValues As New ArrayList

        aryNames.Add("@EmpID")
        aryNames.Add("@Date")

        aryValues.Add(strEmpID)
        aryValues.Add(strSQLDate)

        UseSP("usp_Journey_Get_CoCaptain_Schedule", mdatasetCaptain, mMyViewCaptain, "tblSchedule", aryNames, aryValues)
    End Sub

    Public Sub GetCabinSchedule(strEmpID As String, strSQLDate As String)
        Dim aryNames As New ArrayList
        Dim aryValues As New ArrayList

        aryNames.Add("@EmpID")
        aryNames.Add("@Date")

        aryValues.Add(strEmpID)
        aryValues.Add(strSQLDate)

        UseSP("usp_Journey_Get_Cabin_Schedule", mdatasetCaptain, mMyViewCaptain, "tblSchedule", aryNames, aryValues)
    End Sub

    Public Sub GetCrewNames()
        RunProcedureCoCaptain("usp_Employees_Get_Active_Crew_With_Name")
    End Sub

    Private Function EmpID() As Object
        Throw New NotImplementedException
    End Function

End Class
