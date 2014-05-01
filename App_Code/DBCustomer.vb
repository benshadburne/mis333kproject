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

    Public Sub GetAllActiveCustomersCloneUsingSP()
        'Author: Ben Shadburne
        'Purpose: runs CustomersClone procedure
        'Arguments: na
        'Return: na
        'Date: 03/18/2014

        RunProcedure("usp_CustomersClone_get_all_active")
    End Sub

    Public Sub GetNewestCustomer()
        RunProcedure("usp_Customers_Get_Newest")
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
                mMyView.RowFilter = "Lastname LIKE '" & strSearch & "%'"
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

    Public Sub GetCustomerByAdvantageNumber(strAdvantageNumber As String)
        RunSPwithOneParam("usp_CustomersClone_Get_By_Advantage_Number", "@AdvantageNumber", strAdvantageNumber)
    End Sub

    Public Function CheckCustomerExists(strAdvantageNumber As String) As Boolean
        'returns true if that advantage number exists
        GetCustomerByAdvantageNumber(strAdvantageNumber)

        If mdatasetCustomersClone.Tables("tblCustomersClone").Rows.Count = 0 Then
            'no customer exists
            Return False
        Else
            Return True
        End If
    End Function

    Public Function CheckPassword(strAdvantageNumber As String, strPassword As String) As Boolean
        'Returns TRUE if the password and username combo match
        GetCustomerByAdvantageNumber(strAdvantageNumber)

        If mdatasetCustomersClone.Tables("tblCustomersClone").Rows(0).Item("Password").ToString = strPassword Then
            'test passes
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub GetCustomersByJourney(strJourneyID As String)
        RunSPwithOneParam("usp_Customers_Get_By_Journey", "@JourneyID", strJourneyID)
    End Sub

    Public Sub GetActiveCustomersByJourney(strJourneyID As String)
        RunSPwithOneParam("usp_Customers_Get_Active_By_Journey", "@JourneyID", strJourneyID)
    End Sub


    'Public Function for seeing if a Username typed into the text box is actually in the Customer Table
    Public Function GetFirstName(strFirstName As String) As Boolean
        'Purpose: See if a username typed into the text box is actually in the Customer Table
        'Inputs: strUsername
        'Returns: True if there, False if nonexistent
        'Author: Dennis Phelan
        'Date Created: April 27, 2014

        ''NEW WAY
        RunSPwithOneParam("usp_CustomersClone_Get_FirstName", "@Firstname", strFirstName)

        'Check number of rows in Data set
        ' If zero, return False
        ' Else return True
        If mMyView.Table.Rows.Count = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    'Public Function for seeing if a Username typed into the text box is actually in the Customer Table
    Public Function GetLastName(strLastName As String) As Boolean
        'Purpose: See if a username typed into the text box is actually in the Customer Table
        'Inputs: strUsername
        'Returns: True if there, False if nonexistent
        'Author: Dennis Phelan
        'Date Created: April 27, 2014

        ''NEW WAY
        RunSPwithOneParam("usp_CustomersClone_Get_LastName", "@LastName", strLastName)

        'Check number of rows in Data set
        ' If zero, return False
        ' Else return True
        If mMyView.Table.Rows.Count = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    'Public Function for seeing if a Username typed into the text box is actually in the Customer Table
    Public Function GetPhoneNumber(strPhone As String) As Boolean
        'Purpose: See if a username typed into the text box is actually in the Customer Table
        'Inputs: strUsername
        'Returns: True if there, False if nonexistent
        'Author: Dennis Phelan
        'Date Created: April 27, 2014

        ''NEW WAY
        RunSPwithOneParam("usp_CustomersClone_Get_Phone", "@Phone", strPhone)

        'Check number of rows in Data set
        ' If zero, return False
        ' Else return True
        If mMyView.Table.Rows.Count = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Sub ModifyEmployeeRecord(strAdvantageNumber As String, strPassword As String, strLastName As String, strFirstName As String, strMI As String, strAddress As String, strZip As String, strPhone As String, strEmail As String, strMiles As String, strActive As String)
        Dim aryEmployeeNames As New ArrayList
        Dim aryEmployeeValues As New ArrayList

        aryEmployeeNames.Add("@advantagenumber")
        aryEmployeeNames.Add("@Password")
        aryEmployeeNames.Add("@LastName")
        aryEmployeeNames.Add("@FirstName")
        aryEmployeeNames.Add("@MI")
        aryEmployeeNames.Add("@Address")
        aryEmployeeNames.Add("@Zip")
        aryEmployeeNames.Add("@Phone")
        aryEmployeeNames.Add("@Email")
        aryEmployeeNames.Add("@Miles")
        aryEmployeeNames.Add("@active")

        aryEmployeeValues.Add(strAdvantageNumber)
        aryEmployeeValues.Add(strPassword)
        aryEmployeeValues.Add(strLastName)
        aryEmployeeValues.Add(strFirstName)
        aryEmployeeValues.Add(strMI)
        aryEmployeeValues.Add(strAddress)
        aryEmployeeValues.Add(strZip)
        aryEmployeeValues.Add(strPhone)
        aryEmployeeValues.Add(strEmail)
        aryEmployeeValues.Add(strMiles)
        aryEmployeeValues.Add(strActive)


        UseSPforInsertOrUpdateQuery("usp_CustomersClone_Modify_By_Advantage_Number", aryEmployeeNames, aryEmployeeValues)
    End Sub


    Public Sub FindCustomersForEmail(strFlightNumber As String)
        RunSPwithOneParam("usp_CustomersClone_Select_For_Email", "@flightnumber", strFlightNumber)
    End Sub

    Public Sub FindCustomersByAdvantageNumber(intAdvantageNumber As Integer)
        RunSPwithOneParam("usp_CustomersClone_Get_By_Advantage_Number", "@advantagenumber", intAdvantageNumber.ToString)
    End Sub

    Public Sub FindIfCustomerExists(strFName As String, strLName As String, strPhone As String)
        Dim aryCustomerNames As New ArrayList
        Dim aryCustomerValues As New ArrayList

        aryCustomerNames.Add("@Fname")
        aryCustomerNames.Add("@Lname")
        aryCustomerNames.Add("@Phone")

        aryCustomerValues.Add(strFName)
        aryCustomerValues.Add(strLName)
        aryCustomerValues.Add(strPhone)

        UseSP("usp_CustomersClone_Find_If_Exists", mdatasetCustomersClone, mMyView, "tblCustomersClone", aryCustomerNames, aryCustomerValues)
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
End Class

