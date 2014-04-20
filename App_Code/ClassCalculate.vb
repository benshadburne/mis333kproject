Option Strict On
'Description: Class for calculations, and in particular for calculating the discounts for each ticket price
'Author: Dennis Phelan
'Date Created: April 20, 2014
'Date Last Modified: April 20, 2014
Imports Microsoft.VisualBasic

Public Class ClassCalculate

    'Should these be in the tblConstants??
    'Lay out the constants
    Const FIRST_CLASS_Constant As Decimal = 1.2D
    Const AGE_SeniorDiscount_Constant As Decimal = 0.9D
    Const AGE_ChildDiscount_Constant As Decimal = 0.85D
    Const TIME_2WeeksDiscount_Constant As Decimal = 0.9D
    Const INTERNET_PurchaseDiscount_Constant As Decimal = 0.95D
    Const SALES_TAX_Constant As Decimal = 1.0775D

    'Declare the base fare and end result of the price
    Dim decBaseFare As Decimal
    Dim decTentativeFinalPay As Decimal
    Dim intAge As Integer
    Dim decAgeDiscount As Decimal

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

    'Public ReadOnly Property for the age discount
    Public ReadOnly Property AgeDiscount As Decimal
        Get
            Return decAgeDiscount
        End Get
    End Property

    'Public Property for the Base Fare
    ''I think I need to declare an object from the Flight DB so I can grab the base fare from that flight
    Public Property BaseFare As Decimal
        Get
            Return decBaseFare
        End Get
        Set(value As Decimal)
            decBaseFare = value
        End Set
    End Property


    'Public sub for asking for the age and then applying the discount
    Public Sub CalculateAgeDiscount()
        'Purpose: Apply the age discount if there is one to the base fare _
        '           NOTE: the age will be verified at the Gate check-in so this is the only tentative amount
        'Author: Dennis Phelan
        'Inputs: No input
        'Outputs: Does not return anything; Calculates the age discount
        'Date Created: April 20, 2014
        'Date Last Modified: April 21, 2014

        'Check to see if the age is a senior
        If intAge >= 65 Then
            decAgeDiscount = decBaseFare * AGE_SeniorDiscount_Constant

            'Check to see if the person is 12 and under
            'Keep in mind: I need to add the part where you can multiply the whole decTotal by 0 if the kid is under 3 and has a lap to sit on
        ElseIf intAge <= 12 Then
            decAgeDiscount = decBaseFare * AGE_ChildDiscount_Constant

            'just apply the decAgeDiscount as the decBaseFare
        Else
            decAgeDiscount = decBaseFare
        End If

    End Sub

    'Calculate Date
    Public Sub CalculateTimeBeforeFlight()

        'Need the current system date time
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

        'Check to see if the Save button is clicked on the Customer site and see if it is true
        'If true, apply the discount to the BaseFare

    End Sub



    'Public Sub for adding up all of the different values
    Public Sub CalculateSubTotalDiscount()

        'Put all of the subs in here
        'Add up all of the outputs and have it equal decTentativeFinalPayBeforeTax

    End Sub

    Public Sub CalculateTotalDiscount()

        'Put the CalculateSubTotalDiscount() sub in here
        'Multiply that with the tax to get the total discount after the tax

    End Sub


End Class
