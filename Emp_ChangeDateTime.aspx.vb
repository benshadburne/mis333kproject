Option Strict On
Partial Class Default2
    Inherits System.Web.UI.Page

    Dim DBFlightSearch As New DBFlightSearch
    Dim DBChangeDateTime As New DBChangeDateTime
    'to add days to a date
    'datTwoWeeks = datNow.AddDays(14) <--- this adds 14 days to the current date

    'time and date should only be moved forward 


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Session("UserType") Is Nothing Then
            Response.Redirect("Homepage.aspx")
        Else
            If Session("UserType").ToString = "Customer" Then
                Response.Redirect("Cust_CustomerDashboard.aspx")
            ElseIf Session("UserType").ToString <> "Manager" Then
                Response.Redirect("Emp_EmployeeDashboard.aspx")
            End If
        End If

        If IsPostBack = False Then
            'initializes the calendar to select tomorrow, only when page loads, it just doens't show it
            calDate.SelectedDate = Now().AddDays(1)
        End If

    End Sub

    Protected Sub btnChange_Click(sender As Object, e As EventArgs) Handles btnChange.Click

        'initialize the date and time as strings
        Dim strDate As String
        Dim strTime As String

        strDate = DBFlightSearch.AlterDate(calDate.SelectedDate.ToShortDateString)
        strTime = ddlTimeOfDay.SelectedValue & ddl10Minutes.SelectedValue & ddlMinutes.SelectedValue

        'run code to change departed to yes, and change the miles of customers who were on that journey
        DBChangeDateTime.ChangDateTime(strDate, strTime)

        lblMessage.Text = "You have successfully moved the date and time forward."



    End Sub
End Class
