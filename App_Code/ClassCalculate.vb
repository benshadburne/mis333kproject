Option Strict On
'Description: Class for calculations, and in particular for calculating the discounts for each ticket price
'Author: Dennis Phelan
'Date Created: April 20, 2014
'Date Last Modified: April 20, 2014
Imports Microsoft.VisualBasic

Public Class ClassCalculate

    'Declare an object for getting the Base Fare
    Dim FlightDB As New DBFlightsClone

    'Should these be in the tblConstants??
    'Lay out the constants
    Const FIRST_CLASS_Constant As Decimal = 0.2D
    Const AGE_SeniorDiscount_Constant As Decimal = 0.1D
    Const AGE_ChildDiscount_Constant As Decimal = 0.15D
    Const TIME_TwoWeeksDiscount_Constant As Decimal = 0.1D
    Const INTERNET_PurchaseDiscount_Constant As Decimal = 0.05D
    Const SALES_TAX_Constant As Decimal = 0.0775D

    'Declare the base fare and end result of the price
    Dim decBaseFare As Decimal
    Dim decTentativeFinalPayBeforeTax As Decimal
    Dim decTentativeFinalPayAfterTax As Decimal
    Dim intAge As Integer
    Dim strFlightNumber As String
    Dim decAgeDiscount As Decimal
    Dim datReservation As Date
    Dim decTwoWeeksDiscount As Decimal
    Dim decInternetDiscount As Decimal
    Dim intFirstClass As Integer
    Dim decFirstClassPremium As Decimal
    Dim decTax As Decimal

    'Public Properties for the various inputs and outputs
    'Public Property for the CustomerAge
    Public Property CustomerAge As Integer
        Get
            Return intAge
        End Get
        Set(value As Integer)
            intAge = value
        End Set
    End Property

    'Public Property for the Date
    Public Property DateOfFlight As Date
        Get
            Return datReservation
        End Get
        Set(value As Date)
            datReservation = value
        End Set
    End Property

    'Public Property for the FlightNumber
    Public Property FlightNumber As String
        Get
            Return strFlightNumber
        End Get
        Set(value As String)
            strFlightNumber = value
        End Set
    End Property

    'Public Property for the First Class option
    Public Property FirstClass As Integer
        Get
            Return intFirstClass
        End Get
        Set(value As Integer)
            intFirstClass = value
        End Set
    End Property

    'Public ReadOnly Property for the tentativefinalpaybeforediscount
    Public ReadOnly Property Subtotal As Decimal
        Get
            Return decTentativeFinalPayBeforeTax
        End Get
    End Property

    'Public ReadOnly Property for the tentativefinalpaybeforediscount
    Public ReadOnly Property Total As Decimal
        Get
            Return decTentativeFinalPayAfterTax
        End Get
    End Property

    'Public Function for returning the Base Fare
    Public Function GetBaseFareFromFlightDB(strFlightNumber As String) As Decimal
        'Purpose: Get the base fare from the DBFlight Class that uses an SP to get it from the table
        'Author: Dennis Phelan
        'Inputs: The flight number that will be passed as a public property with the value form the form
        'Outputs: the Base fare that will be used on this class
        'Date Created: April 20, 2014
        'Date Last Modified: April 20, 2014

        'Use the function in the DBFlight
        decBaseFare = FlightDB.GetBaseFare(strFlightNumber)

        'Return the base fare to be used throughout this form for the discounts
        Return decBaseFare
    End Function

    'Public sub for asking for the age and then applying the discount

    Public Function ConvertToVBDate(strDate As String) As Date
        Dim strMonth As String
        Dim strDay As String
        Dim strYear As String
        Dim strVBDate As String
        Dim datDate As Date

        strMonth = strDate.Substring(5, 2)
        strDay = strDate.Substring(8, 2)
        strYear = strDate.Substring(0, 4)

        strVBDate = strMonth & "/" & strDay & "/" & strYear
        datDate = CDate(strVBDate)
        Return datDate
    End Function

    Public Function CalculateAgeDiscount(intAge As Integer, intBaseFare As Integer) As Decimal
        'Purpose: Apply the age discount if there is one to the base fare _
        '           NOTE: the age will be verified at the Gate check-in so this is the only tentative amount
        'Author: Dennis Phelan and Aaryaman Singhal
        'Inputs: 
        'Outputs: returns age discount
        'Date Created: April 20, 2014
        'Date Last Modified: April 21, 2014, Now April 23, 2014

        'Check to see if the age is a senior
        If intAge >= 65 Then
            decAgeDiscount = intBaseFare * AGE_SeniorDiscount_Constant

        Else
            If intAge <= 12 Then
                decAgeDiscount = intBaseFare * AGE_ChildDiscount_Constant
            Else
                decAgeDiscount = 0
            End If
            'Check to see if the person is 12 and under
            'Keep in mind: I need to add the part where you can multiply the whole decTotal by 0 if the kid is under 3 and has a lap to sit on

            'just apply the decAgeDiscount as the decBaseFare

        End If

        Return decAgeDiscount
    End Function

    'Calculate Date
   

    Public Function CalculateTimeBeforeFlight(datReservation As Date, datToday As Date) As Boolean
        'Purpose: Calculates whether or not the time of purchase is two weeks before the flight takes off
        'Author: Dennis Phelan
        'Inputs: datReservation that is the time 
        'Outputs: True or False if the reservation is 2 weeks out or not
        'Date Created: April 21, 2014
        'Date Last Modified: April 21, 2014

        ''''''''''''''''''''''''''''ALSO, Need to make sure the format of our date is used'''''''''''''''''''

        'Checks to see if the reservation is 14 days out or not
        If datReservation < datToday.AddDays(14) Then
            Return False
        End If

        'If it is, then return True
        Return True
        'Need the date stored as current time in DB

        ''''''''''''''''''''''''HOW SPECIFIC DOES THE 2 WEEKS NEED TO BE? IF IT IS 2 WEEKS FROM MONDAY, BUT THE FLIGHT IS AT 9:30 AM AND I RESERVE AT 12:30 PM, IS IT STILL 2 WEEKS?''''''''''''''''''''


        'need when the flight time is going to be
        ''Both of these will be from the tables, so I will need another object from flight table and the system date table
        ''Or, I can see if there is a current time function


    End Function

    'Calculate the discount related to the date
    Public Function CalculateDateDiscount(intBaseFare As Integer, datReservation As Date, datToday As Date) As Decimal
        'Purpose: Take the value from the CalculateTimeOfFlight to decide if it is 14 days, and then apply the discount if it is 14 days or more
        'Author: Dennis Phelan
        'Inputs: None
        'Outputs: the decTwoWeeksDiscount
        'Date Created: April 21, 2014
        'Date Last Modified: April 21, 2014


        'Check to see if there is a discount or not
        If CalculateTimeBeforeFlight(datReservation, datToday) = True Then
            decTwoWeeksDiscount = intBaseFare * TIME_TwoWeeksDiscount_Constant
        Else
            'If not 14 days or more out, the discount is 0
            decTwoWeeksDiscount = 0
        End If

        Return decTwoWeeksDiscount
    End Function

    'Calculate the discount from whether someone used the Customer site to purchase their ticket
    Public Function CalculateInternetPurchaseDiscount(intBaseFare As Integer) As Decimal
        'Purpose: Calculate a discount if the customer purchases their ticket online
        'Author: Dennis Phelan
        'Inputs: None
        'Outputs: decInternetDiscount
        'Date Created: April 21, 2014
        'Date Last Modified: April 21, 2014

        'Calculate the Internet discount
        decInternetDiscount = intBaseFare * INTERNET_PurchaseDiscount_Constant

        'Check to see if the Save button is clicked on the Customer site and see if it is true
        'If true, apply the discount to the BaseFare

        Return decInternetDiscount

    End Function

    Public Function CalculateFirstClass(intBaseFare As Integer) As Decimal
        'Purpose: Calculate the premium for flying first class
        'Author: Dennis Phelan
        'Inputs: None
        'Outputs: The first class premium
        'Date Created: April 21, 2014
        'Date Last Created: April 21, 2014


        'Checking to see if the first class option is selected

        decFirstClassPremium = intBaseFare * FIRST_CLASS_Constant


        Return decFirstClassPremium
    End Function


    'Public Sub for adding up all of the different values
    Public Function CalculateSubTotalDiscount(intBaseFare As Integer, decFirstClassPremium As Decimal, decAgeDiscount As Decimal, decTwoWeekDiscount As Decimal, decInternetDiscount As Decimal) As Decimal
        'Purpose: Use all of the above subs to get the numbers and get the ticket price with the discounts before Tax
        'Author: Dennis Phelan, Aaryaman
        'Inputs: base fair, first class premium, age discount, two week discount, intenet discount
        'Outputs: the FinalPayBeforeTax
        'Date Created: April 21, 2014
        'Date Last Modified: April 21, 2014

        'Get the first class premium

        'Add up all of the discounts
        decTentativeFinalPayBeforeTax = decFirstClassPremium - decAgeDiscount - decTwoWeeksDiscount - decInternetDiscount

        'Put all of the subs in here
        'Add up all of the outputs and have it equal decTentativeFinalPayBeforeTax

        Return decTentativeFinalPayBeforeTax

    End Function

    'Public Sub for calculating the tax
    Public Function CalculateTotal(decSubtotal As Decimal) As Decimal
        'Purpose: Calculate the tax for the final pay
        'Author: Dennis Phelan
        'Inputs: None
        'Outputs: the tax amount of the final pay
        'Date Created: April 21, 2014
        'Date Last Modified: April 21, 2014

        ''Calculate the subtotal with discounts
        'CalculateSubTotalDiscount()

        Dim decTotal As Decimal
        'Calculate the tax
        decTotal = decSubtotal * (1 + SALES_TAX_Constant)

        Return decTotal

    End Function

    Public Sub CalculateTotalDiscount()
        'Purpose: Calculate the total discount for the final pay
        'Author: Dennis Phelan
        'Inputs: None
        'Outputs: the tax amount of the final pay
        'Date Created: April 21, 2014
        'Date Last Modified: April 21, 2014

        ''Get the subtotal
        'CalculateSubTotalDiscount()

        'Get the tax
        CalculateTax()

        'Add them up to get the total discount
        decTentativeFinalPayAfterTax = decTentativeFinalPayBeforeTax + decTax

        ''''''''''''''''Check the age one more time; if an infant, they get on for free. But only one infant per older person'''''''''''''''''''''''''''''
        If intAge < 3 Then
            decTentativeFinalPayAfterTax = decTentativeFinalPayAfterTax * 0
        End If

        'Put the CalculateSubTotalDiscount() sub in here
        'Multiply that with the tax to get the total discount after the tax
    End Sub

    '''''''''''''''Use the value calculated here to then transfer it to the DBTickets to run a SP to update the price of the ticket paid and the base fare at purchase'''''''''''''

    Public Function CalculateArrivalTime(intDepartureTime As Integer, intDuration As Integer) As String
        'declare variables
        Dim intArrivalTime As Integer
        Dim strArrivalTime As String
        Dim intNumberofHours As Integer
        Dim intMinutesRemainder As Integer
        Dim intDepartureMinutes As Integer
        Dim intTotalMinutes As Integer
        Dim intDepartureHours As Integer

        'find number of hours (not considering minutes) of flight
        intNumberofHours = Math.DivRem(intDuration, 100, intMinutesRemainder)
        'find how many hours have passed in day by time flight departs
        intDepartureHours = Math.DivRem(intDepartureTime, 100, intDepartureMinutes)

        'find total number of minutes we will need to add (3:59 with flight duration of 1 hour, 30 minutes is 89 minutes to add)
        intTotalMinutes = intDepartureMinutes + intMinutesRemainder

        'if total number of minutes to add is greater than 60, add an hour to add
        If intTotalMinutes > 60 Then
            intTotalMinutes = intTotalMinutes - 60
            intNumberofHours += 1
        End If

        'the arrival time equals the number of hours in day at time of departure, plus number of hours in air...
        intArrivalTime = intDepartureHours * 100 + intNumberofHours * 100
        'plus number of minutes leftover
        intArrivalTime = intArrivalTime + intTotalMinutes

        'if this happens to put us in the next day, we need to take away 2400 minutes
        If intArrivalTime > 2400 Then
            intArrivalTime = intArrivalTime - 2400
        End If

        'return the calculated arrival time as a string
        strArrivalTime = intArrivalTime.ToString

        Return strArrivalTime
    End Function

    Private Sub CalculateTax()
        Throw New NotImplementedException
    End Sub

End Class
