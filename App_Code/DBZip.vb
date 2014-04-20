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
Public Class DBZip
    'setting up db, dim connection, adapter, query, dataset
    Dim mMyView As New DataView
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size=4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False;initial catalog=mis333k_20142_Team06;user id=msbcf819;password=Databasepassword5"
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdatasetzip As New DataSet
    Dim mQueryString As String

    Public Sub GetALLZipUsingSP()
        'Author: Ben Shadburne
        'Purpose: runs zip procedure
        'Arguments: na
        'Return: na
        'Date: 03/18/2014

        RunProcedure("usp_ZipsCloneget_all")
    End Sub

    'define a public read only property
    Public ReadOnly Property MyView() As DataView
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: Zip dataview
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
            Me.mdatasetzip.Clear()
            'open conneciton and fill dataset
            mdbDataAdapter.Fill(mdatasetzip, "tblZipClone")
            'copy dataset to dataview
            mMyView.Table = mdatasetzip.Tables("tblZipClone")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strName.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public ReadOnly Property lblCount() As Integer
        'Author: Ben Shadburne
        'Purpose: return lblcount
        'Arguments:  none
        'Return: count of zip
        'Date: 03/07/2014

        Get
            'returns the count to the label
            Return MyView.Count
        End Get
    End Property


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
            mdatasetzip.Clear()

            ' OPEN CONNECTION AND FILL DATASET
            mdbDataAdapter.Fill(mdatasetzip, "tblZipClone")

            ' copy dataset to dataview
            mMyView.Table = mdatasetzip.Tables("tblZipClone")

        Catch ex As Exception
            Throw New Exception("params are " & strSPName.ToString & " " & strParamName.ToString & " " & strParamValue.ToString & " error is " & ex.Message)
        End Try
    End Sub


    'author: Jace Barton
    'description: runs SP to see if a matching zip code was found
    'returns: False if no matching zip code found, true otherwise
    'inputs: the zip code you want to search for as a string
    Public Function FindZip(strZipCode As String) As Boolean
        RunSPwithOneParam("usp_ZipsClone_Find_Zip", "Zip", strZipCode)

        If mMyView.Count = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    'author: Jace Barton
    'description: runs SP to see if a matching zip code was found
    'returns: False if no matching zip code found, true otherwise
    'inputs: the zip code you want to search for as a string
    Public Function FindState(strState As String) As Boolean
        RunSPwithOneParam("usp_ZipsClone_Find_State", "State", strState)

        If mMyView.Count = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    'Function for returning the state that matches with the zip code
    Public Function MatchZipToState(strZip As String) As String
        'Purpose: Run a stored procedure that matches the state with the zip code, and then set a value equal to the state that is outputted
        'Author: Dennis Phelan
        'Input: the Zip code the user enters
        'Outputs: the State that goes with the zip code
        'Date Created: April 20, 2014
        'Date Last Modified: April 20, 2014

        'Declare the result
        Dim strResult As String

        'Run the SP with one param that matches the zip to the state
        RunSPwithOneParam("usp_ZipsClone_Match_Zip_to_State", "@zip", strZip)

        'Set the result to the State that is outputted
        strResult = mMyView.Table.Rows(0).Item("State").ToString

        'Return the result
        Return strResult
    End Function
End Class

