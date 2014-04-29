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
Public Class DBFlightsClone
    'setting up db, dim connection, adapter, query, dataset
    Dim mMyView As New DataView
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size=4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False;initial catalog=mis333k_20142_Team06;user id=msbcf819;password=Databasepassword5"
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdatasetFlightsClone As New DataSet
    Dim mQueryString As String

    Public Sub GetALLFlightsCloneUsingSP()
        'Author: Ben Shadburne
        'Purpose: runs FlightsClone procedure
        'Arguments: na
        'Return: na
        'Date: 03/18/2014

        RunProcedure("usp_FlightsClone_get_all")
    End Sub

    'define a public read only property
    Public ReadOnly Property MyView() As DataView
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: FlightsClone dataview
        'Date: 03/18/2014

        Get
            Return mMyView
        End Get
    End Property

    Public ReadOnly Property MyDataSet() As DataSet
        'Author: Ben Shadburne
        'Purpose: returns read only dataset
        'Arguments: na
        'Return: FlightsClone dataset
        'Date: 03/18/2014

        Get
            Return mdatasetFlightsClone

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
            Me.mdatasetFlightsClone.Clear()
            'open conneciton and fill dataset
            mdbDataAdapter.Fill(mdatasetFlightsClone, "tblFlightClone")
            'copy dataset to dataview
            mMyView.Table = mdatasetFlightsClone.Tables("tblFlightClone")
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
            Me.MyDataSet.Clear()

            ' OPEN CONNECTION AND FILL DATASET
            mdbDataAdapter.Fill(MyDataSet, "tblFlightsClone")

            ' copy dataset to dataview
            mMyView.Table = MyDataSet.Tables("tblFlightsClone")

        Catch ex As Exception
            Throw New Exception("params are " & strSPName.ToString & " " & strParamName.ToString & " " & strParamValue.ToString & " error is " & ex.Message)
        End Try
    End Sub

    'the sub checks which flights have been added as a journey for a specific day. 
    Public Sub CheckFlightsNeededForSpecificDate(strSPname As String, strParamName As String, strDay As String)

        RunSPwithOneParam(strSPname, strParamName, strDay)
    End Sub


    Public Sub GetOneFlight(strSPName As String, strParamName As String, strFlightNumber As String)

        RunSPwithOneParam(strSPName, strParamName, strFlightNumber)
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

    Public Sub AddFlight(ByVal aryParamNames As ArrayList, ByVal aryParamValues As ArrayList)
        'run the use SP sub to add flights using information passed from form
        UseSP("usp_FlightClone_Add_New", mdatasetFlightsClone, mMyView, "tblFlightClone", aryParamNames, aryParamValues)
    End Sub

    Public Function GetBaseFare(strFlightNumber As String) As Decimal
        'Purpose: Get the Base Fare from the Database to use in the Calculation Class for it to be discounted
        'Author: Dennis Phelan
        'Inputs: The Flight Number the customer/employee is looking at
        'Outputs: The Base Fare
        'Date Created: April 20, 2014
        'Date Last Modified: April 20, 2014

        'Declare the result
        Dim decResult As Decimal

        'Run the stored procedure to that will give you the one base fare
        RunSPwithOneParam("usp_FlightClone_Get_BaseFare", "@FlightNumber", strFlightNumber)

        'Set the result to the value that appears in the database
        decResult = Convert.ToDecimal(mMyView.Table.Rows(0).Item("BaseFare"))

        'Return the Base Fare
        Return decResult
    End Function

    'run stored procedure to modify an employee record
    Public Sub ModifyFlight(ByVal aryParamNames As ArrayList, ByVal aryParamValues As ArrayList)
        UseSPforInsertOrUpdateQuery("usp_FlightClone_Modify_Flight", aryParamNames, aryParamValues)
    End Sub

    'make a flight inactive given a flight number 
    Public Sub MakeFlightInactive(strFlightNumber As String)
        Dim aryParamNames As New ArrayList
        Dim aryParamValues As New ArrayList

        aryParamNames.Add("@flightnumber")
        aryParamValues.Add(strFlightNumber)
        UseSPforInsertOrUpdateQuery("usp_FlightClone_Invalidate_By_Flight#", aryParamNames, aryParamValues)
    End Sub
End Class
