'Author: Ben Shadburne
'Assignment: ASP4
'Date: 02/06/2014
'Description: handles database based queries and sets up the string and connection


Option Strict On
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Public Class DBChangeDateTime

    'setting up db, dim connection, adapter, query, dataset
    Dim mMyViewJourneys As New DataView
    Dim mMyViewTickets As New DataView
    Dim mdbConn As New SqlConnection
    Dim mstrConnection As String = "workstation id=COMPUTER;packet size=4096;data source=MISSQL.mccombs.utexas.edu;integrated security=False;initial catalog=mis333k_20142_Team06;user id=msbcf819;password=Databasepassword5"
    Dim mdbDataAdapter As New SqlDataAdapter
    Dim mdatasetJourneys As New DataSet
    Dim mdatasetTickets As New DataSet
    Dim mQueryString As String

    Dim DBFlightSearch As New DBFlightSearch
    Dim DBMilage As New DBMilage
    Dim DBCustomer As New DBCustomersClone
    Dim DBTickets As New DBTickets

    Public Sub ChangDateTime(strDate As String, strTime As String)

        'first we get all journeys where departed is no and active is yes
        GetAllJourneysJourneysBeforeDate(strDate)

        'now we have all the journeys with starting dates and times in all of them, need to loop through and make the journey departed and
        'give all customers on the journey miles from the journey
        Dim aryParamName As New ArrayList
        Dim aryParamValue As New ArrayList
        Dim aryAdvantageName As New ArrayList
        Dim aryAdvantageValue As New ArrayList
        Dim aryJourneyID As New ArrayList
        Dim strMilage As String

        aryParamName.Add("@JourneyID")

        aryJourneyID.Add("@JourneyID")

        aryAdvantageName.Add("@Milage")
        aryAdvantageName.Add("@AdvantageNumber")
        'loop for each journeyid that is updated
        For i = 0 To lblCountJourneyID - 1
            'set mileage of given journeyid for customer update
            strMilage = DBMilage.GetMilesForFlight(MyViewJourney.Table().Rows(i).Item("JourneyID").ToString)
            aryAdvantageValue.Add(strMilage)
            'setup array lists
            aryParamValue.Add(CInt(MyViewJourney.Table().Rows(i).Item("JourneyID")))
            aryJourneyID.Add(CInt(MyViewJourney.Table().Rows(i).Item("JourneyID")))

            'query Tickets Table with given journeyID to retrieve
            UseSPToRetrieveRecords("usp_Tickets_Get_By_JourneyID", mdatasetTickets, mMyViewTickets, "tblTickets", aryParamName, aryParamValue)

            'loop through the tickets
            For j = 0 To lblCountTickets - 1

                'first check to see if they paid by miles, if they have then no need to update miles
                If CInt(MyViewTickets.Table().Rows(j).Item("MilagePaid")) <> 0 Then
                    'this means they didn't pay by miles

                    'update miles on customer profiles with given advantage number
                    DBCustomer.UpdateMiles(strMilage, MyViewTickets.Table().Rows(j).Item("AdvantageNumber").ToString)
                End If
                'mark that customer was on flight
                DBTickets.MarkOnFlight(MyViewTickets.Table().Rows(j).Item("TicketID").ToString)
            Next




            'set departed to Y


            aryAdvantageValue.Remove(strMilage)
            'remove value just used for next loop
            aryParamValue.Remove(CInt(MyViewJourney.Table().Rows(i).Item("JourneyID")))
        Next


    End Sub

    Protected Sub GetAllJourneysJourneysBeforeDate(strDate As String)

        Dim aryParamName As New ArrayList
        Dim aryParamValue As New ArrayList

        aryParamName.Add("@Date")
        aryParamValue.Add(strDate)

        UseSPToRetrieveRecords("usp_FlightSearch_DateTime", mdatasetJourneys, mMyViewJourneys, "tblJourneys", aryParamName, aryParamValue)
        aryParamName = Nothing
        aryParamValue = Nothing
    End Sub

    'define a public read only property
    Public ReadOnly Property MyViewJourney() As DataView
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: xxxxx dataview
        'Date: 03/18/2014

        Get
            Return mMyViewJourneys
        End Get
    End Property

    Public ReadOnly Property MyViewTickets() As DataView
        'Author: Ben Shadburne
        'Purpose: returns read only dataview
        'Arguments: na
        'Return: xxxxx dataview
        'Date: 03/18/2014

        Get
            Return mMyViewTickets
        End Get
    End Property

    Public ReadOnly Property lblCountJourneyID() As Integer
        'Author: Ben Shadburne
        'Purpose: return lblcount
        'Arguments:  none
        'Return: count of xxxxx
        'Date: 03/07/2014

        Get
            'returns the count to the label
            Return MyViewJourney.Count
        End Get
    End Property

    Public ReadOnly Property lblCountTickets() As Integer
        'Author: Ben Shadburne
        'Purpose: return lblcount
        'Arguments:  none
        'Return: count of xxxxx
        'Date: 03/07/2014

        Get
            'returns the count to the label
            Return MyViewTickets.Count
        End Get
    End Property



    Protected Sub UseSPToRetrieveRecords(ByVal strUSPName As String, ByVal strDatasetName As DataSet, ByVal strViewName As DataView, ByVal strTableName As String, ByVal aryParamNames As ArrayList, ByVal aryParamValues As ArrayList)
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

End Class

