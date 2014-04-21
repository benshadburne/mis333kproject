Option Strict On
'Description: Class for calculations, and in particular for calculating the discounts for each ticket price
'Author: Dennis Phelan
'Date Created: April 20, 2014
'Date Last Modified: April 20, 2014
Imports Microsoft.VisualBasic

Public Class ClassCalculate

    'Declare an object for getting the Base Fare
    Dim FObject As New DBFlightsClone

    'Should these be in the tblConstants??
    'Lay out the constants
    Const FIRST_CLASS_Constant As Decimal = 1.2D
    Const AGE_SeniorDiscount_Constant As Decimal = 0.1D
    Const AGE_ChildDiscount_Constant As Decimal = 0.15D
    Const TIME_2WeeksDiscount_Constant As Decimal = 0.1D
    Const INTERNET_PurchaseDiscount_Constant As Decimal = 0.05D
    Const SALES_TAX_Constant As Decimal = 1.0775D

    'Declare the base fare and end result of the price
    Dim decBaseFare As Decimal
    Dim decTentativeFinalPayBeforeTax As Decimal
    Dim decTentativeFinalPayAfterTax As Decimal
    Dim intAge As Integer
    Dim strFlightNumber As String
    Dim decAgeDiscount As Decimal
    Dim decInternetDiscount As Decimal

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

    'Public Property for the FlightNumber
    Public Property FlightNumber As String
        Get
            Return strFlightNumber
        End Get
        Set(value As String)
            strFlightNumber = value
        End Set
    End Property

    'Public ReadOnly Property for the age discount
    Public ReadOnly Property AgeDiscount As Decimal
        Get
            Return decAgeDiscount
        End Get
    End Property

    'Public ReadOnly Property for the tentativefinalpaybeforediscount
    Public ReadOnly Property Subtotal As Decimal
        Get
            Return decTentativeFinalPayBeforeTax
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
        decBaseFare = FObject.GetBaseFare(strFlightNumber)

        'Return the base fare to be used throughout this form for the discounts
        Return decBaseFare
    End Function

    'Public sub for asking for the age and then applying the discount
    Public Sub CalculateAgeDiscount(intAge As Integer)
        'Purpose: Apply the age discount if there is one to the base fare _
        '           NOTE: the age will be verified at the Gate check-in so this is the only tentative amount
        'Author: Dennis Phelan
        'Inputs: No input
        'Outputs: Does not return anything; Calculates the age discount
        'Date Created: April 20, 2014
        'Date Last Modified: April 21, 2014

        GetBaseFareFromFlightDB(strFlightNumber)

        'Check to see if the age is a senior
        If intAge >= 65 Then
            decAgeDiscount = decBaseFare * AGE_SeniorDiscount_Constant

            'Check to see if the person is 12 and under
            'Keep in mind: I need to add the part where you can multiply the whole decTotal by 0 if the kid is under 3 and has a lap to sit on
        ElseIf intAge <= 12 Then
            decAgeDiscount = decBaseFare * AGE_ChildDiscount_Constant

            'just apply the decAgeDiscount as the decBaseFare
        Else
            decAgeDiscount = 0
        End If
    End Sub

    'Calculate Date
    Public Sub CalculateTimeBeforeFlight()

        'Need the date stored as current time in DB


        'need when the flight time is going to be
        ''Both of these will be from the tables, so I will need another object from flight table and the system date table
        ''Or, I can see if there is a current time function


    End Sub

    'Calculate the discount related to the date
    Public Sub CalculateDateDiscount()

        'Output from the CalculateTimeBeforeFlight()
        'If statement to see if it is 2 weeks out before

    End Sub

    'Calculate the discount from whether someone used the Customer site to purchase their ticket
    Public Sub CalculateInternetPurchaseDiscount()

        decInternetDiscount = decBaseFare * INTERNET_PurchaseDiscount_Constant

        'Check to see if the Save button is clicked on the Customer site and see if it is true
        'If true, apply the discount to the BaseFare

    End Sub



    'Public Sub for adding up all of the different values
    Public Sub CalculateSubTotalDiscount()

        CalculateAgeDiscount(intAge)

        decTentativeFinalPayBeforeTax = decBaseFare - decAgeDiscount

        'Put all of the subs in here
        'Add up all of the outputs and have it equal decTentativeFinalPayBeforeTax

    End Sub

    Public Sub CalculateTotalDiscount()

        'Put the CalculateSubTotalDiscount() sub in here
        'Multiply that with the tax to get the total discount after the tax

    End Sub

    'Use the value calculated here to then transfer it to the DBTickets to run a SP to update the price of the ticket

End Class
