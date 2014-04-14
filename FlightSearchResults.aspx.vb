
Partial Class _Default
    Inherits System.Web.UI.Page


    Dim DB As New DBFlightSearch

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        lblMessage.Text = calFlightSearch.SelectedDate.ToString

        'Dim strLogin As String
        'strLogin = Session("Login").ToString
        ''checks login session variable 
        ''if empty, neither select nor edit show up
        ''if it's a customer id then select shows up
        'If strLogin = "???????????" Then
        '    gvDirectFlights.AutoGenerateSelectButton = True
        '    gvIndirectFlights.AutoGenerateSelectButton = True
        'End If
        ''if it's an employee id then select and edit show up
        'If strLogin = "??????Employee" Then
        '    gvDirectFlights.AutoGenerateSelectButton = True
        '    gvDirectFlights.AutoGenerateEditButton = True
        '    gvIndirectFlights.AutoGenerateSelectButton = True
        '    gvIndirectFlights.AutoGenerateEditButton = True
        'End If


        DB.GetALLFlightSearchUsingSP()

        SortandBind()
    End Sub

    Public Sub SortandBind()
        'Author: Ben Shadburne
        'Purpose: sort the data and bind it 
        'Arguments:  na
        'Return: sorted and binded data
        'Date: 03/18/2014

        'sort using radio
        'DB.DoSort(radSort.SelectedIndex)

        gvDirectFlights.DataSource = DB.MyView
        gvDirectFlights.DataBind()

        ' show record count
        lblCountDirect.Text = lblCountDirect.Text & CStr(DB.lblCount)
    End Sub


    Protected Sub gvDirectFlights_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDirectFlights.SelectedIndexChanged

        'Session("FlightChoice") = gvDirectFlights




    End Sub
End Class
