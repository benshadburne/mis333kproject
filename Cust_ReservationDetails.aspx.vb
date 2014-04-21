Option Strict On
Partial Class _Default
    Inherits System.Web.UI.Page

    Dim DBSeats As New DBSeats
    Dim DBTickets As New DBTickets

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim strReservationID As String
        Dim strAdvantageNum As String
        strReservationID = CStr(10004)
        strAdvantageNum = CStr(5000)


        Session("Login") = strAdvantageNum
        Session("ReservationID") = strReservationID
        lblReservationID.Text = strReservationID
        'check customer login

        ''check session reservationID if it's empty
        'strReservationID = Session("ReservationID").ToString
        'If strReservationID = "" Then
        '    Response.Redirect("HomePage.aspx")
        'End If

        ''check session login if it's empty
        'strAdvantageNum = Session("Login").ToString
        'If strAdvantageNum = "" then
        '   Response.Redirect("HomePage.aspx")
        'End If

        'next, need to load all tickets dataset
        LoadTickets()

        SortandBind()


        'bind ddl for journeys
        ddlJourneyID.DataSource = DBTickets.MyView
        ddlJourneyID.DataValueField = "JourneyID"
        ddlJourneyID.DataBind()

        'bind seats and seats w/advantage numbers
        BindSeats()
        'check seats to initialize them
        CheckSeats()

    End Sub

    Public Sub LoadTickets()
        DBTickets.GetALLTicketsUsingSP()
        DBTickets.GetALLOthersTicketsUsingSP()
        DBTickets.FilterYou(Session("ReservationID").ToString, Session("Login").ToString)
        DBTickets.FilterOthers(Session("ReservationID").ToString, Session("Login").ToString)

    End Sub

    Public Sub BindSeats()
        DBSeats.GetALLSeatsUsingSP()
        DBSeats.FilterJourneyID(ddlJourneyID.SelectedValue)
        DBSeats.GetALLSeatsAdvantageUsingSP(ddlJourneyID.SelectedValue)
        DBSeats.GetALLSeatsAdvantageUserUsingSP(ddlJourneyID.SelectedValue)
        DBSeats.FilterAdvantage(Session("Login").ToString)
    End Sub

    Public Sub CheckSeats()

        'button variable makes it so that I can give the work 'button' methods like .backcolor for each button in the arlSeats array
        Dim button As Button
        Dim arlSeats As New ArrayList

        'adding all buttons to the arraylist
        arlSeats.Add(btn1A)
        arlSeats.Add(btn1B)
        arlSeats.Add(btn2A)
        arlSeats.Add(btn2B)
        arlSeats.Add(btn3A)
        arlSeats.Add(btn3B)
        arlSeats.Add(btn3C)
        arlSeats.Add(btn3D)
        arlSeats.Add(btn4A)
        arlSeats.Add(btn4B)
        arlSeats.Add(btn4C)
        arlSeats.Add(btn4D)
        arlSeats.Add(btn5A)
        arlSeats.Add(btn5B)
        arlSeats.Add(btn5C)
        arlSeats.Add(btn5D)


        'basically it does a test in each step
        For i = 0 To 15
            button = CType(arlSeats(i), Button)
            'first test is if the seat is empty, if yes then it makes the color lightgray
            If CInt(DBSeats.MyView.Table().Rows(i).Item("Status")) = 0 Then
                button.BackColor = Drawing.Color.LightGray
                'second test checks if the advantage dataset advantage number (should be user's advantage number)
            ElseIf ConvertInteger(DBSeats.MyViewAdvantage.Table().Rows(i).Item("AdvantageNumber").ToString) = CInt(Session("Login")) Then
                button.BackColor = Drawing.Color.Green
                Session("UserSeat") = DBSeats.MyViewAdvantage.Table().Rows(i).Item("Seat").ToString
                'third test is whether the 'Others' dataset is empty (basically if there are other nonreservation people on the plane
            ElseIf ConvertInteger(DBSeats.MyViewAdvantage.Table().Rows(i).Item("ReservationID").ToString) <> CInt(Session("ReservationID")) Then
                'it has to loop through all the people in the dataset and check whether their advantage number is the one in the seat we are checking (i)
                button.BackColor = Drawing.Color.Coral
            Else
                'if this runs, the seat is filled by someone who is on the reservation but not the user
                button.BackColor = Drawing.Color.Blue
            End If
        Next

    End Sub

    Public Sub SortandBind()
        'Author: Ben Shadburne
        'Purpose: sort the data and bind it 
        'Arguments:  na
        'Return: sorted and binded data
        'Date: 03/18/2014

        'sort 
        'DBTickets.DoSort()

        ''bind all data
        gvYourReservation.DataSource = DBTickets.MyView
        gvYourReservation.DataBind()
        gvOtherReservation.DataSource = DBTickets.MyViewOthers
        gvOtherReservation.DataBind()

    End Sub

    Protected Sub ddlJourneyID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJourneyID.SelectedIndexChanged
        CheckSeats()
    End Sub

    'every time a button is clicked this happens 
    Protected Sub btn1A_Click(sender As Object, e As EventArgs) Handles btn1A.Click, btn1B.Click, _
        btn2A.Click, btn2B.Click, btn3A.Click, btn3B.Click, btn3C.Click, btn3D.Click, btn4A.Click, btn4B.Click, _
        btn4C.Click, btn4D.Click, btn5A.Click, btn5B.Click, btn5C.Click, btn5D.Click

        Dim button As Button
        button = CType(sender, Button)

        'basically, they can't choose the seat if it's filled by anyone
        If button.BackColor = Drawing.Color.Coral Or button.BackColor = Drawing.Color.Blue Or button.BackColor = Drawing.Color.Green Then
            'seat already taken
            Exit Sub
        End If

        'if seat is empty then stuff happens
        If button.BackColor = Drawing.Color.LightGray Then
            'this runs code to make sure database is updated
            Dim strNewSeat As String
            strNewSeat = button.ID.Substring(3, 2)
            DBSeats.GreyPress(Session("UserSeat").ToString, strNewSeat, Session("Login").ToString, ddlJourneyID.SelectedValue)
        End If

        'alter the colors of the seats if needed

        BindSeats()
        CheckSeats()
        LoadTickets()
        SortandBind()
    End Sub

    Public Function ConvertInteger(strIn As String) As Integer
        Dim intFill As Integer
        Try
            intFill = CInt(strIn)
        Catch ex As Exception
            Return -1
        End Try
        Return CInt(strIn)
    End Function

End Class
