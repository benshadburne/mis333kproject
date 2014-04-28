Option Strict On
Partial Class Emp_ViewReports
    Inherits System.Web.UI.Page

    'Declare an instance of Tickets DB
    Dim TicketsDB As New DBTickets

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Load up the grid view
        TicketsDB.GetGrossRevenueAndSeatCount()

        gvReports.DataSource = TicketsDB.MyView

        gvReports.DataBind()
    End Sub
End Class
