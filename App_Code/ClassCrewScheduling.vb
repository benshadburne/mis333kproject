Option Strict On
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
        Dim intCounter As Integer
        'get all captains who are active employees
        GetCaptains()
        'this is the number of captains there are
        intCaptains = mdatasetCaptain.Tables("tblCaptain").Rows.Count - 1

        intCounter = 0

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
                    If CInt(mdatasetCaptainBusy.Tables("tblCaptain").Rows(j).Item("DepartureTime")) <= intDepartureTime And _
                        intDepartureTime <= CInt(mdatasetCaptainBusy.Tables("tblCaptain").Rows(j).Item("ArriveTime")) Then
                        'the captain is busy at this time don't add them to the array of available captains
                    Else
                        'check to see if inputted arrive time is between start and end of journey we are looping through
                        If CInt(mdatasetCaptainBusy.Tables("tblCaptain").Rows(j).Item("DepartureTime")) <= intArriveTime And _
                        intArriveTime <= CInt(mdatasetCaptainBusy.Tables("tblCaptain").Rows(j).Item("ArriveTime")) Then
                            'the captain is busy, don't add them to the arraylist
                        Else
                            'captain is not busy during arrival or departure, check to see if would take off before journey 
                            'and would land after journey we are looking at
                            If intDepartureTime <= CInt(mdatasetCaptainBusy.Tables("tblCaptain").Rows(j).Item("DepartureTime")) And _
                                intArriveTime >= CInt(mdatasetCaptainBusy.Tables("tblCaptain").Rows(j).Item("ArriveTime")) Then
                                'captain is airborne during another flight he/she is on
                            Else
                                'captain is free to be on this flight, add them to arraylist
                                intCounter += 1
                            End If

                        End If

                    End If

                Next
                ' check if intcounter is equal to the number of times we looped
                If intCounter = mdatasetCaptainBusy.Tables("tblCaptain").Rows.Count Then
                    'captain is free despite all other journeys they are one
                    aryCaptains.Add(mdatasetCaptain.Tables("tblCaptain").Rows(i).Item("EmpID").ToString)
                Else
                    'one of the captain's journeys conflicted with the times we needed
                End If
            Else
                'Captain is free all day
                aryCaptains.Add(mdatasetCaptain.Tables("tblCaptain").Rows(i).Item("EmpID").ToString)
            End If
            'reset the counter
            intCounter = 0
        Next

        Return aryCaptains
    End Function

    Public Function FindAvailableCoCaptains(strDate As String, intDepartureTime As Integer, intArriveTime As Integer) As ArrayList
        Dim intCoCaptains As Integer
        Dim aryCoCaptains As New ArrayList
        Dim intCounter As Integer
        'get all CoCaptains who are active employees
        GetCoCaptains()
        'this is the number of CoCaptains there are
        intCoCaptains = mdatasetCoCaptain.Tables("tblCoCaptain").Rows.Count - 1

        intCounter = 0

        'leep through all CoCaptains
        For i = 0 To intCoCaptains
            'check to see if CoCoCaptain is on any journeys on that date
            GetCoCaptainBusy(mdatasetCoCaptain.Tables("tblCoCaptain").Rows(i).Item("EmpID").ToString, strDate)
            'if there are no records returned for that date
            If mdatasetCoCaptainBusy.Tables("tblCoCaptain").Rows.Count <> 0 Then
                'CoCaptain has other flights that day 
                'run a loop to see if they are busy at the times we are looking for
                For j = 0 To (mdatasetCoCaptainBusy.Tables("tblCoCaptain").Rows.Count - 1)
                    'checks to see if inputted departure is between start and end of journey they are on
                    If CInt(mdatasetCoCaptainBusy.Tables("tblCoCaptain").Rows(j).Item("DepartureTime")) <= intDepartureTime And _
                        intDepartureTime <= CInt(mdatasetCoCaptainBusy.Tables("tblCoCaptain").Rows(j).Item("ArriveTime")) Then
                        'the CoCaptain is busy at this time don't add them to the array of available CoCaptains
                    Else
                        'check to see if inputted arrive time is between start and end of journey we are looping through
                        If CInt(mdatasetCoCaptainBusy.Tables("tblCoCaptain").Rows(j).Item("DepartureTime")) <= intArriveTime And _
                        intArriveTime <= CInt(mdatasetCoCaptainBusy.Tables("tblCoCaptain").Rows(j).Item("ArriveTime")) Then
                            'the CoCaptain is busy, don't add them to the arraylist
                        Else
                            'CoCaptain is not busy during arrival or departure, check to see if would take off before journey 
                            'and would land after journey we are looking at
                            If intDepartureTime <= CInt(mdatasetCoCaptainBusy.Tables("tblCoCaptain").Rows(j).Item("DepartureTime")) And _
                                intArriveTime >= CInt(mdatasetCoCaptainBusy.Tables("tblCoCaptain").Rows(j).Item("ArriveTime")) Then
                                'CoCaptain is airborne during another flight he/she is on
                            Else
                                'CoCaptain is free to be on this flight, add them to arraylist
                                intCounter += 1
                            End If

                        End If

                    End If

                Next
                ' check if intcounter is equal to the number of times we looped
                If intCounter = mdatasetCoCaptainBusy.Tables("tblCoCaptain").Rows.Count Then
                    'CoCaptain is free despite all other journeys they are one
                    aryCoCaptains.Add(mdatasetCoCaptain.Tables("tblCoCaptain").Rows(i).Item("EmpID").ToString)
                Else
                    'one of the CoCaptain's journeys conflicted with the times we needed
                End If
            Else
                'CoCaptain is free all day
                aryCoCaptains.Add(mdatasetCoCaptain.Tables("tblCoCaptain").Rows(i).Item("EmpID").ToString)
            End If
            'reset the counter
            intCounter = 0
        Next

        Return aryCoCaptains
    End Function

    Public Function FindAvailableCabin(strDate As String, intDepartureTime As Integer, intArriveTime As Integer) As ArrayList
        Dim intCabin As Integer
        Dim aryCabin As New ArrayList
        Dim intCounter As Integer
        'get all Cabin who are active employees
        GetCabin()
        'this is the number of Cabin there are
        intCabin = mdatasetCabin.Tables("tblCabin").Rows.Count - 1

        intCounter = 0

        'leep through all Cabin
        For i = 0 To intCabin
            'check to see if CoCabin is on any journeys on that date
            GetCabinBusy(mdatasetCabin.Tables("tblCabin").Rows(i).Item("EmpID").ToString, strDate)
            'if there are no records returned for that date
            If mdatasetCabinBusy.Tables("tblCabin").Rows.Count <> 0 Then
                'Cabin has other flights that day 
                'run a loop to see if they are busy at the times we are looking for
                For j = 0 To (mdatasetCabinBusy.Tables("tblCabin").Rows.Count - 1)
                    'checks to see if inputted departure is between start and end of journey they are on
                    If CInt(mdatasetCabinBusy.Tables("tblCabin").Rows(j).Item("DepartureTime")) <= intDepartureTime And _
                        intDepartureTime <= CInt(mdatasetCabinBusy.Tables("tblCabin").Rows(j).Item("ArriveTime")) Then
                        'the Cabin is busy at this time don't add them to the array of available Cabin
                    Else
                        'check to see if inputted arrive time is between start and end of journey we are looping through
                        If CInt(mdatasetCabinBusy.Tables("tblCabin").Rows(j).Item("DepartureTime")) <= intArriveTime And _
                        intArriveTime <= CInt(mdatasetCabinBusy.Tables("tblCabin").Rows(j).Item("ArriveTime")) Then
                            'the Cabin is busy, don't add them to the arraylist
                        Else
                            'Cabin is not busy during arrival or departure, check to see if would take off before journey 
                            'and would land after journey we are looking at
                            If intDepartureTime <= CInt(mdatasetCabinBusy.Tables("tblCabin").Rows(j).Item("DepartureTime")) And _
                                intArriveTime >= CInt(mdatasetCabinBusy.Tables("tblCabin").Rows(j).Item("ArriveTime")) Then
                                'Cabin is airborne during another flight he/she is on
                            Else
                                'Cabin is free to be on this flight, add them to arraylist
                                intCounter += 1
                            End If

                        End If

                    End If

                Next
                ' check if intcounter is equal to the number of times we looped
                If intCounter = mdatasetCabinBusy.Tables("tblCabin").Rows.Count Then
                    'Cabin is free despite all other journeys they are one
                    aryCabin.Add(mdatasetCabin.Tables("tblCabin").Rows(i).Item("EmpID").ToString)
                Else
                    'one of the Cabin's journeys conflicted with the times we needed
                End If
            Else
                'Cabin is free all day
                aryCabin.Add(mdatasetCabin.Tables("tblCabin").Rows(i).Item("EmpID").ToString)
            End If
            'reset the counter
            intCounter = 0
        Next

        Return aryCabin
    End Function

    Private Function EmpID() As Object
        Throw New NotImplementedException
    End Function

End Class
