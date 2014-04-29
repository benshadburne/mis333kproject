Option Strict On
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Public Class DBdate
    'setting up db, dim connection, adapter, query, dataset
    Dim mMyView As New DataView
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size=4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False;initial catalog=mis333k_20142_Team06;user id=msbcf819;password=Databasepassword5"
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdatasetdate As New DataSet
    Dim mQueryString As String

    Public Sub GetALLdateUsingSP()
        'Author: Ben Shadburne
        'Purpose: runs date procedure
        'Arguments: na
        'Return: na
        'Date: 03/18/2014

        RunProcedure("usp_date_Get_All")
    End Sub

    'define a public read only property
    Public ReadOnly Property MyView() As DataView
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: date dataview
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
            Me.mdatasetdate.Clear()
            'open conneciton and fill dataset
            mdbDataAdapter.Fill(mdatasetdate, "tblDate")
            'copy dataset to dataview
            mMyView.Table = mdatasetdate.Tables("tblDate")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strName.ToString & " error is " & ex.Message)
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

    Public Function GetCurrentDate() As String
        Dim strDate As String
        RunProcedure("usp_Constants_Get_Date")

        strDate = mdatasetdate.Tables("tblDate").Rows(0).Item("DateTime").ToString

        Return strDate
    End Function

    Public Function GetCurrentDateReturnDate() As Date
        Dim datDate As Date
        RunProcedure("usp_Constants_Get_Date")

        datDate = CDate(mdatasetdate.Tables("tblDate").Rows(0).Item("DateTime"))

        Return datDate
    End Function

    Public Function ConvertToVBDate(strDate As String) As Date
        Dim strMonth As String
        Dim strDay As String
        Dim strYear As String
        Dim strVBDate As String
        Dim datDate As Date

        strMonth = strDate.Substring(5, 2)
        strDay = strDate.Substring(8, 2)
        strYear = strDate.Substring(0, 4)

        strVBDate = strMonth & "/" & strDay & "/" & strYear
        datDate = CDate(strVBDate)
        Return datDate
    End Function

End Class

