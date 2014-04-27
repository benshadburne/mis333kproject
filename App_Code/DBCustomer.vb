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
Public Class DBCustomersClone
    'setting up db, dim connection, adapter, query, dataset
    Dim mMyView As New DataView
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size=4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False;initial catalog=mis333k_20142_Team06;user id=msbcf819;password=Databasepassword5"
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdatasetCustomersClone As New DataSet
    Dim mQueryString As String

    Public Sub GetAllCustomersCloneUsingSP()
        'Author: Ben Shadburne
        'Purpose: runs CustomersClone procedure
        'Arguments: na
        'Return: na
        'Date: 03/18/2014

        RunProcedure("usp_CustomersClone_get_all")
    End Sub

    Public Sub SearchCustomerClone(intSearchType As Integer, intSearchBy As Integer, strSearch As String)
        'Author: Aaryaman
        'Purpose: sorts data to be placed in a gridview
        'Arguments: integer -- preferrably from a radio button list
        'Return: none, but sorts data in the dataview
        'Date: 4/18/2014

        GetAllCustomersCloneUsingSP()

        'we are doing a partial search
        If intSearchBy = 0 Then
            If intSearchType = 0 Then
                'search by last name
                mMyView.RowFilter = "Lastname LIKe '" & strSearch & "%'"
            Else
                mMyView.RowFilter = "Lastname like '%" & strSearch & "%'"
            End If
        Else

            mMyView.RowFilter = "AdvantageNumber = " & strSearch

        End If

    End Sub

    'define a public read only property
    Public ReadOnly Property MyView() As DataView
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: CustomersClone dataview
        'Date: 03/18/2014

        Get
            Return mMyView
        End Get
    End Property

    'define a public read only property
    Public ReadOnly Property MyDataset() As DataSet
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: CustomersClone dataview
        'Date: 03/18/2014

        Get
            Return mdatasetCustomersClone
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
            Me.mdatasetCustomersClone.Clear()
            'open conneciton and fill dataset
            mdbDataAdapter.Fill(mdatasetCustomersClone, "tblCustomersClone")
            'copy dataset to dataview
            mMyView.Table = mdatasetCustomersClone.Tables("tblCustomersClone")
        Catch ex As Exception
            Throw New Exception("stored procedure is " & strName.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub DoSort(ByVal intIndex As Integer)
        'Author: Ben Shadburne
        'Purpose: sorts data
        'Arguments: index of rad button
        'Return: sorted dataview
        'Date: 03/18/2014

        'sort using radio buttons
        If intIndex = 0 Then
            'sort by name
            mMyView.Sort = "lastname, firstname"
        Else
            mMyView.Sort = "username"
        End If
    End Sub

    Public Sub SearchByState(ByVal strIn As String)
        'Author: Ben Shadburne
        'Purpose: search by state
        'Arguments: search text
        'Return: filtered dataview by state
        'Date: 03/18/2014

        MyView.RowFilter = "State = '" & strIn & "'"
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
        'Return: count of CustomersClone
        'Date: 03/07/2014

        Get
            'returns the count to the label
            Return MyView.Count
        End Get
    End Property

    'Add new customer using a stored procedure
    Public Sub AddNewCustomer(strLName As String, strFName As String, strMI As String, strUsername As String, strPassword As String, strAddress As String, strCity As String, strState As String, strZip As String, strEmail As String, strPhone As String)

        'Run the stored procedure here
        'RunProcedure("usp_Customers_Add_New")

    End Sub

    'End Sub

    Public Sub UseSPforInsertOrUpdateQuery(ByVal strUSPName As String, ByVal aryParamNames As ArrayList, ByVal aryParamValues As ArrayList)
        'Purpose: Sort the dataview by the argument (general sub)
        'Arguments: Stored procedure name, Arraylist of parameter names, and arraylist of parameter values
        'Returns: Nothing; Runs an Insert or Update Query
        'Author: Dennis Phelan
        'Date Created: April 11, 2014
        'Date Last Modified: April 11, 2014

        'Creates instances of the connection and command object
        Dim objConnection As New SqlConnection(mstrConnection)

        'Tell SQL Server the name of the stored procedure
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

            'Open connection and run insert/update query
            objCommand.SelectCommand.Connection = objConnection
            objConnection.Open()
            objCommand.SelectCommand.ExecuteNonQuery()
            objConnection.Close()


        Catch ex As Exception

            'Print out each element of our arraylists if error occured
            Dim strError As String = ""
            Dim index As Integer = 0
            For Each paramName As String In aryParamNames
                strError = strError & "ParamName: " & CStr(aryParamNames(index))
                strError = strError & "ParamValue: " & CStr(aryParamValues(index))
                index = index + 1
            Next

            Throw New Exception(strError & " error message is " & ex.Message)
        End Try
    End Sub

    Public Sub UpdateMiles(strMiles As String, strAdvantageNumber As String)
        'create arrays
        Dim aryParamNames As New ArrayList
        Dim aryParamValues As New ArrayList

        aryParamNames.Add("@Miles")
        aryParamNames.Add("@AdvantageNumber")

        aryParamValues.Add(strMiles)
        aryParamValues.Add(strAdvantageNumber)

        UseSPforInsertOrUpdateQuery("usp_Customers_UpdateMiles", aryParamNames, aryParamValues)

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
            mdatasetCustomersClone.Clear()

            ' OPEN CONNECTION AND FILL DATASET
            mdbDataAdapter.Fill(mdatasetCustomersClone, "tblCustomersClone")

            ' copy dataset to dataview
            mMyView.Table = mdatasetCustomersClone.Tables("tblCustomersClone")

        Catch ex As Exception
            Throw New Exception("params are " & strSPName.ToString & " " & strParamName.ToString & " " & strParamValue.ToString & " error is " & ex.Message)
        End Try
    End Sub

    Public Sub GetCustomersByJourney(strJourneyID As String)
        RunSPwithOneParam("usp_Customers_Get_By_Journey", "@JourneyID", strJourneyID)
    End Sub



    Public Sub FindCustomersForEmail(strFlightNumber As String)
        RunSPwithOneParam("usp_CustomersClone_Select_For_Email", "@flightnumber", strFlightNumber)
    End Sub
End Class

