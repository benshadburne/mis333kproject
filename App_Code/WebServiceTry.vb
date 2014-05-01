Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services
Imports System.Collections.Generic

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
 Public Class WebServiceTry
    Inherits System.Web.Services.WebService

    'create instances of DB classes
    Dim FObject As New DBFlightsClone
    Dim RObject As New DBReservations

    Public Structure FlightData
        Public FlightNumber As Integer
        Public DepartureCity As String
        Public ArrivalCity As String
        Public DepartureTime As Integer
        Public ArrivalTime As Integer
        Public BaseFare As Integer
        Public Monday As String
        Public Tuesday As String
        Public Wednesday As String
        Public Thursday As String
        Public Friday As String
        Public Saturday As String
        Public Sunday As String
    End Structure

    Public Structure ReservationData
        Public ReservationID As Integer
        Public Active As String
        Public FlightNumber As Integer
        Public TicketID As Integer
        Public DepartureCity As String
        Public EndCity As String
        Public FlightDate As String
        Public DepartureTime As Integer
        Public ArrivalTime As Integer
        Public AdvantageNumber As Integer
        Public FirstName As String
        Public LastName As String
        Public Phone As String


    End Structure

    <WebMethod()> _
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <WebMethod()> _
    Public Function getFlights() As List(Of FlightData)
        Dim data As New List(Of FlightData)
        FObject.RunProcedure("usp_FlightClone_Get_City_Names")
        Try
            For i = 0 To FObject.MyDataSet.Tables("tblFlightsClone").Rows.Count - 1
                Dim item As New FlightData
                item.FlightNumber = FObject.MyDataSet.Tables("tblFlightsClone").Rows(i).Item("FlightNumber")
                item.DepartureCity = FObject.MyDataSet.Tables("tblFlightsClone").Rows(i).Item("DepartureCity")
                item.ArrivalCity = FObject.MyDataSet.Tables("tblFlightsClone").Rows(i).Item("ArrivalCity")
                item.DepartureTime = FObject.MyDataSet.Tables("tblFlightsClone").Rows(i).Item("DepartureTime")
                item.ArrivalTime = FObject.MyDataSet.Tables("tblFlightsClone").Rows(i).Item("ArrivalTime")
                item.BaseFare = FObject.MyDataSet.Tables("tblFlightsClone").Rows(i).Item("BaseFare")
                item.Monday = FObject.MyDataSet.Tables("tblFlightsClone").Rows(i).Item("Monday")
                item.Tuesday = FObject.MyDataSet.Tables("tblFlightsClone").Rows(i).Item("Tuesday")
                item.Wednesday = FObject.MyDataSet.Tables("tblFlightsClone").Rows(i).Item("Wednesday")
                item.Thursday = FObject.MyDataSet.Tables("tblFlightsClone").Rows(i).Item("Thursday")
                item.Friday = FObject.MyDataSet.Tables("tblFlightsClone").Rows(i).Item("Friday")
                item.Saturday = FObject.MyDataSet.Tables("tblFlightsClone").Rows(i).Item("Saturday")
                item.Sunday = FObject.MyDataSet.Tables("tblFlightsClone").Rows(i).Item("Sunday")
                data.Add(item)
            Next
        Catch ex As Exception
            Throw New Exception("Error is" & ex.Message.ToString)
        End Try

        Return data
    End Function

    <WebMethod()> _
    Public Function SearchFlights(ByVal departurecity As String) As List(Of FlightData)
        Dim data As New List(Of FlightData)
        FObject.RunSPwithOneParam("usp_FlightClone_Search_By_City", "@departurecity", departurecity)
        Try
            For i = 0 To FObject.MyDataSet.Tables("tblFlightsClone").Rows.Count - 1
                Dim item As New FlightData
                item.FlightNumber = FObject.MyDataSet.Tables("tblFlightsClone").Rows(i).Item("FlightNumber")
                item.DepartureCity = FObject.MyDataSet.Tables("tblFlightsClone").Rows(i).Item("DepartureCity")
                item.ArrivalCity = FObject.MyDataSet.Tables("tblFlightsClone").Rows(i).Item("ArrivalCity")
                item.DepartureTime = FObject.MyDataSet.Tables("tblFlightsClone").Rows(i).Item("DepartureTime")
                item.ArrivalTime = FObject.MyDataSet.Tables("tblFlightsClone").Rows(i).Item("ArrivalTime")
                item.BaseFare = FObject.MyDataSet.Tables("tblFlightsClone").Rows(i).Item("BaseFare")
                item.Monday = FObject.MyDataSet.Tables("tblFlightsClone").Rows(i).Item("Monday")
                item.Tuesday = FObject.MyDataSet.Tables("tblFlightsClone").Rows(i).Item("Tuesday")
                item.Wednesday = FObject.MyDataSet.Tables("tblFlightsClone").Rows(i).Item("Wednesday")
                item.Thursday = FObject.MyDataSet.Tables("tblFlightsClone").Rows(i).Item("Thursday")
                item.Friday = FObject.MyDataSet.Tables("tblFlightsClone").Rows(i).Item("Friday")
                item.Saturday = FObject.MyDataSet.Tables("tblFlightsClone").Rows(i).Item("Saturday")
                item.Sunday = FObject.MyDataSet.Tables("tblFlightsClone").Rows(i).Item("Sunday")
                data.Add(item)
            Next
        Catch ex As Exception
            Throw New Exception("Error is" & ex.Message.ToString)
        End Try

        Return data
    End Function

    <WebMethod()> _
    Public Function SearchReservations(ByVal reservationid As String) As List(Of ReservationData)
        Dim data As New List(Of ReservationData)
        RObject.RunSPwithOneParam("usp_ReservationsClone_Find_By_ReservationID", "@reservationid", reservationid)
        Try
            For i = 0 To RObject.MyDataSet.Tables("tblReservationsClone").Rows.Count - 1
                Dim item As New ReservationData
                item.ReservationID = RObject.MyDataSet.Tables("tblReservationsClone").Rows(i).Item("ReservationID")
                item.Active = RObject.MyDataSet.Tables("tblReservationsClone").Rows(i).Item("Active")
                item.FlightNumber = RObject.MyDataSet.Tables("tblReservationsClone").Rows(i).Item("FlightNumber")
                item.TicketID = RObject.MyDataSet.Tables("tblReservationsClone").Rows(i).Item("TicketID")
                item.DepartureCity = RObject.MyDataSet.Tables("tblReservationsClone").Rows(i).Item("DepartureCity")
                item.EndCity = RObject.MyDataSet.Tables("tblReservationsClone").Rows(i).Item("EndCity")
                item.FlightDate = RObject.MyDataSet.Tables("tblReservationsClone").Rows(i).Item("FlightDate")
                item.DepartureTime = RObject.MyDataSet.Tables("tblReservationsClone").Rows(i).Item("DepartureTime")
                item.ArrivalTime = RObject.MyDataSet.Tables("tblReservationsClone").Rows(i).Item("ArriveTime")
                item.AdvantageNumber = RObject.MyDataSet.Tables("tblReservationsClone").Rows(i).Item("AdvantageNumber")
                item.FirstName = RObject.MyDataSet.Tables("tblReservationsClone").Rows(i).Item("FirstName")
                item.LastName = RObject.MyDataSet.Tables("tblReservationsClone").Rows(i).Item("LastName")
                item.Phone = RObject.MyDataSet.Tables("tblReservationsClone").Rows(i).Item("Phone")
                data.Add(item)
            Next
        Catch ex As Exception
            Throw New Exception("Error is" & ex.Message.ToString)
        End Try

        Return data
    End Function
End Class