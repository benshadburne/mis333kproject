﻿Imports System.Net.Mail
Partial Class Emp_GateCheckIn
    Inherits System.Web.UI.Page


    Dim DBJourney As New DBjourneyclone
    Dim DBCustomer As New DBCustomersClone
    Dim DBDate As New DBdate
    Dim DBMilage As New DBMilage
    Dim DBTickets As New DBTickets
    Dim DBCrew As New ClassCrewScheduling



    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJourneys.SelectedIndexChanged
        lblMessage.Text = ""

        LoadCustomerGridView(ddlJourneys.SelectedValue.ToString)

    End Sub

    Public Sub LoadCustomerGridView(strJourneyID As String)
        'run stored procedure to get all customers and their miles and stuff
        DBCustomer.GetActiveCustomersByJourney(strJourneyID)

        gvCustomers.DataSource = DBCustomer.MyDataset
        gvCustomers.DataBind()

        If gvCustomers.Rows.Count = 0 Then
            'throw them an error
            lblMessage.Text = "There are no customers on this flight. Please try another."
        Else
            gvCustomers.Visible = True
        End If

        gvCustomers.Visible = True
    End Sub

    Public Sub LoadCrewGridView(strJourneyID As String)
        btnCrewScheduling.Visible = False
        DBCrew.GetCrewByJourney(strJourneyID)

        gvCrew.DataSource = DBCrew.MyView
        gvCrew.DataBind()

        If gvCrew.Rows.Count <> 3 Then
            'throw them an error
            lblMessage.Text = "There are not crew members sheduled for this flight. Please schedule crew members"
            If Session("UserType").ToString = "Manager" Then
                'let them schedule crew
                btnCrewScheduling.Visible = True
            Else
                'they are a gate agent
                lblMessage.Text = "Crew members have not been scheduled for this flight. Have a manager schedule them."
            End If
        Else
            gvCrew.Visible = True
            btnDeparted.Visible = True
        End If

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Session("UserType") Is Nothing Then
            Response.Redirect("HomePage.aspx")
        ElseIf Session("UserType").ToString = "Crew" Or Session("UserType").ToString = "Customer" Then
            Response.Redirect("Emp_EmployeeDashboard.aspx")
        End If


        If IsPostBack = False Then
            LoadDDL()
            ddlJourneys.SelectedIndex = 0
            LoadCustomerGridView(ddlJourneys.SelectedValue.ToString)
        End If
    End Sub

    Public Sub LoadDDL()
        'bind ddl for journeys
        DBJourney.GetActiveJourneysForCrewByDate(DBDate.GetCurrentDate())
        ddlJourneys.DataSource = DBJourney.MyView
        ddlJourneys.DataTextField = "FlightNumber"
        ddlJourneys.DataValueField = "JourneyID"
        ddlJourneys.DataBind()
    End Sub

    Protected Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        lblMessage.Text = ""
        'check to see if any customers showed up for the flight
        Dim chkPassengers As CheckBox
        Dim bolOnFlight As Boolean = False

        'loop through each record of gridview
        For i = 0 To gvCustomers.Rows.Count - 1
            'select a row
            gvCustomers.SelectedIndex = i
            chkPassengers = gvCustomers.SelectedRow.FindControl("chkPassengers")
            'check to see if the customer is there
            If chkPassengers.Checked = True Then
                'this passenger is on the flight
                bolOnFlight = True
            End If
        Next

        'if no one is on the flight
        If bolOnFlight = False Then
            'throw them an error
            lblMessage.Text = "We don't want to fly a plane with no customers on it."
            'ADD A CANCEL FLIGHT BUTTON
            Exit Sub
        End If

        LoadCrewGridView(ddlJourneys.SelectedValue.ToString)

        'check to see that three crew members are scheduled for this flight
        If gvCrew.Rows.Count = 3 Then
            btnBack.Visible = True
            btnConfirm.Visible = False
            ddlJourneys.Enabled = False
            gvCustomers.Enabled = False
        Else
            'there aren't three crew members
            If Session("UserType").ToString = "Manager" Then
                'if this is a manager
                btnCrewScheduling.Visible = True
                lblMessage.Text = "You must schedule all the crew members before this flight can take off."
            Else
                'this flight doesn't have 3 crew members on it
                lblMessage.Text = "This flight doesn't have three crew members on it. Please have a manager schedule them."
                Exit Sub
            End If

        End If

    End Sub

    Public Sub LoadManifestGridView(strJourneyID As String)
        pnlManifest.Visible = True

        DBCrew.GetManifest(strJourneyID)

        gvManifest.DataSource = DBCrew.MyDataSetCaptain
        gvManifest.DataBind()

    End Sub

    Protected Sub btnDeparted_Click(sender As Object, e As EventArgs) Handles btnDeparted.Click
        lblMessage.Text = ""
        Dim chkPassengers As CheckBox
        Dim chkCrew As CheckBox
        Dim strMiles As String
        Dim intMiles As Integer
        Dim BolCrew As Boolean = True

        For i = 0 To gvCrew.Rows.Count - 1
            gvCrew.SelectedIndex = i
            chkCrew = gvCrew.SelectedRow.FindControl("chkCrew")

            'if any of the crew isn't present
            If chkCrew.Checked = False Then
                'make the boolean false
                BolCrew = False
            End If
        Next

        If BolCrew = False Then
            'journey can't take off
            lblMessage.Text = "This flight can't take off without all of its crew members."
            Exit Sub
        End If

        strMiles = DBMilage.GetMilesForFlight(ddlJourneys.SelectedItem.ToString)

        For i = 0 To gvCustomers.Rows.Count - 1
            gvCustomers.SelectedIndex = i
            chkPassengers = gvCustomers.SelectedRow.FindControl("chkPassengers")

            If chkPassengers.Checked = True Then
                'run if statement to see if they paid my miles or price
                If gvCustomers.SelectedRow.Cells(9).Text = "0" Then
                    'they have not paid any real money for their ticket
                    'they do not get miles

                Else
                    'give them miles
                    intMiles = CInt(gvCustomers.SelectedRow.Cells(5).Text)
                    intMiles += CInt(strMiles)
                    DBCustomer.UpdateMiles(intMiles.ToString, gvCustomers.SelectedRow.Cells(1).Text)
                    'run code to indicate they were on the flight
                End If
                DBTickets.MarkOnFlight(gvCustomers.SelectedRow.Cells(7).Text)
            Else
                ''they didn 't make it to the flight
                ''send them an email
                'Dim Msg As MailMessage = New MailMessage()
                'Dim MailObj As New SmtpClient("smtp.mccombs.utexas.edu")
                'Msg.From = New MailAddress("mis333kgroup6@gmail.com", "Jace Barton")
                'Msg.To.Add(New MailAddress(gvCustomers.SelectedRow.Cells(10).Text, gvCustomers.SelectedRow.Cells(2).Text + " " + gvCustomers.SelectedRow.Cells(3).Text))
                'Msg.IsBodyHtml = False
                'Msg.Body = "Hello " & gvCustomers.SelectedRow.Cells(2).Text & ", " & vbCrLf & vbCrLf & "Unfortunately, we needed to cancel your ticket on flight #" & ddlJourneys.SelectedItem.ToString & " and all other flights associated with that reservation (ReservationID:" & gvCustomers.SelectedRow.Cells(11).Text & "). We apologize for any inconvenience this may cause. Please visit our website to make a new reservation." & vbCrLf & vbCrLf & "Best," & vbCrLf & "The Penguin Air Team"
                'Msg.Subject = "Flight Cancellation"
                'MailObj.Send(Msg)
                'Msg.To.Clear()


                'mark all their tickets on that reservation for that customer inactive
                DBTickets.DeactivateOneCustomersTicketsOnReservation(gvCustomers.SelectedRow.Cells(11).Text, gvCustomers.SelectedRow.Cells(1).Text)
            End If

        Next

        'mark journey as departed
        'this is commented out since we haven't done crew scheduling yet
        DBJourney.MarkJourneyDeparted(ddlJourneys.SelectedValue.ToString)

        'show a flight manifest
        pnlManifest.Visible = True
        LoadManifestGridView(ddlJourneys.SelectedValue.ToString)

        btnBack.Visible = False
        btnDeparted.Visible = False

    End Sub

    Protected Sub btnReload_Click(sender As Object, e As EventArgs) Handles btnReload.Click
        ResetPage()
    End Sub

    Public Sub ResetPage()
        'load the ddl
        LoadDDL()
        LoadCustomerGridView(ddlJourneys.SelectedValue.ToString)
        'fix what the user can touch on the form
        btnConfirm.Visible = True
        btnDeparted.Visible = False
        ddlJourneys.Enabled = True
        gvCustomers.Enabled = True
        gvCrew.Visible = False
        pnlManifest.Visible = False
        btnBack.Visible = False
        btnCrewScheduling.Visible = False
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        ResetPage()
    End Sub


    Protected Sub btnCrewScheduling_Click(sender As Object, e As EventArgs) Handles btnCrewScheduling.Click
        Response.Redirect("Emp_CrewScheduling.aspx")
    End Sub

End Class
