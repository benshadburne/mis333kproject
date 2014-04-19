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
        lblReservationID.Text += strReservationID
        'check customer login

        ''check session reservationID if it's empty
        'strCheck = Session("ReservationID").ToString
        'If strCheck = "" Then
        '    Response.Redirect("HomePage.aspx")
        'End If

        'next, need to load all tickets dataset
        DBTickets.GetALLTicketsUsingSP()
        DBTickets.GetALLOthersTicketsUsingSP()


        'filter for the given reservationID for tickets 
        DBTickets.FilterYou(strReservationID, strAdvantageNum)
        DBTickets.FilterOthers(strReservationID, strAdvantageNum)


        SortandBind()


        'bind ddl for journeys
        ddlJourneyID.DataSource = DBTickets.MyView
        ddlJourneyID.DataValueField = "JourneyID"
        ddlJourneyID.DataBind()

        'bind seats
        DBSeats.GetALLSeatsUsingSP()
        DBSeats.FilterJourneyID(ddlJourneyID.SelectedValue)

        'check seats to initialize them
        CheckSeats()

    End Sub

    Public Sub CheckSeats()

        Dim button As Button
        Dim arlSeats As ArrayList

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

        For i = 0 To 15
            button = CType(arlSeats(i), Button)
            If CInt(DBSeats.MyView.Table().Rows(i).Item("Status")) = 0 Then
                button.BackColor = Drawing.Color.LightGray
                'ElseIf CInt(DBSeats.MyView.Table().Rows(i).Item("JourneyID")) = CInt(Session("Login")) Then
                button.BackColor = Drawing.Color.Green
                'Else If 
            End If
        Next






    End Sub

    Public Sub ChangeSeat(strSeat As String, strType As String)

        


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

    Protected Sub btn1A_Click(sender As Object, e As EventArgs) Handles btn1A.Click, btn1B.Click, _
        btn2A.Click, btn2B.Click, btn3A.Click, btn3B.Click, btn3C.Click, btn3D.Click, btn4A.Click, btn4B.Click, _
        btn4C.Click, btn4D.Click, btn5A.Click, btn5B.Click, btn5C.Click, btn5D.Click

        Dim button As Button
        button = CType(sender, Button)

        If button.BackColor = Drawing.Color.Coral Then
            Exit Sub
        End If

        If button.BackColor = Drawing.Color.Gray Then
            button.BackColor = Drawing.Color.Green
        ElseIf button.BackColor = Drawing.Color.Green Then
            button.BackColor = Drawing.Color.Gray
        End If

    End Sub
End Class
