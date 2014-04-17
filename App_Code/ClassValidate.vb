'Author: Ben Shadburne
'Assignment: VB1 Review
'Date: 01/16/2014
'Description: Calculates customer subtotals and totals and allows mike to change the price
Option Strict On
Imports Microsoft.VisualBasic



Public Class ClassValidate

    'create instance of Zip DB
    Dim ZObject As New DBZip


    Dim mstrPhone As String

    Public Function CheckIntegerWithSubstring(ByVal strIn As String) As Boolean
        'Author: Ben Shadburne
        'Purpose: checks input to see if integer
        'Arguments:  strIn
        'Return: true/false
        'Date: 02/06/2014

        ' check to see that each character is 0-9
        Dim i As Integer
        Dim strOne As String

        For i = 0 To Len(strIn) - 1
            'get one character from the string
            strOne = strIn.Substring(i, 1)
            Select Case strOne
                'if the character is 0-9, then keep going
                Case "0" To "9"
                    ' if the character is anything else, stop looking the return false
                Case Else
                    'if bad data, return false
                    Return False
            End Select
        Next

        'if we made it through the loop, return true
        Return True
    End Function

    Public Function CheckPhone(ByVal strIn As String) As Boolean
        'Author: Ben Shadburne
        'Purpose: validates phone
        'Arguments:  strIn
        'Return: true/false
        'Date: 02/06/2014

        'check if string's length is 10
        If Len(strIn) <> 10 Then
            Return False
        End If

        'check digits
        Return CheckIntegerWithSubstring(strIn)


    End Function

    Public Function CheckZip(ByVal strIn As String) As Boolean
        'Author: Ben Shadburne, modified by Jace
        'Purpose: validates zip
        'Arguments:  strIn
        'Return: true/false
        'Date: 02/06/2014

        If ZObject.FindZip(strIn) = False Then
            'zip code not found
            Return False
        Else ' zip code found
            Return True
        End If

    End Function
    Public Function CheckDigitForLetterNumber(ByVal strIn As String) As Boolean
        'Author: Ben Shadburne
        'Purpose: checks input to see if letter or number
        'Arguments:  strIn
        'Return: true/false
        'Date: 02/06/2014

        ' check to see that each character is 0-9
        Dim i As Integer
        Dim strOne As String

        For i = 0 To Len(strIn) - 1
            'get one character from the string
            strOne = strIn.Substring(i, 1)
            Select Case strOne.ToUpper
                'if the character is 0-9, then keep going
                Case "0" To "9"
                    'if not 0 to 9 then check if letter
                Case "A" To "Z"
                Case Else
                    Return False
            End Select
        Next

        'if we made it through the loop, return true
        Return True
    End Function


    Public Function CheckLetterWithSubstring(ByVal strIn As String) As Boolean
        'Author: Ben Shadburne
        'Purpose: checks input to see if letter
        'Arguments:  strIn
        'Return: true/false
        'Date: 02/06/2014

        ' check to see that each character is A-Z
        Dim i As Integer
        Dim strOne As String

        For i = 0 To Len(strIn) - 1
            'get one character from the string
            strOne = strIn.Substring(i, 1)
            Select Case strOne.ToUpper
                'if the character is 0-9, then keep going
                Case "A" To "Z"
                    ' if the character is anything else, stop looking the return false
                Case Else
                    'if bad data, return false
                    Return False
            End Select
        Next

        'if we made it through the loop, return true
        Return True
    End Function

    Public Function CheckCity(ByVal strIn As String) As Boolean
        'Author: Aaryaman Singhal
        'Purpose: checks input to see if city
        'Arguments:  strIn
        'Return: true/false
        'Date: 04/13/2014

        'check to see if length is 2 while seeing if it contains only letters
        Select Case strIn.Substring(0, 1)
            Case "A" To "Z"
                Return CheckLetterWithSubstring(strIn)
            Case Else
                'failed first letter capital test
                Return False
        End Select

    End Function

    Public Function CheckState(ByVal strIn As String) As Boolean
        'Author: Ben Shadburne, modified by Jace
        'Purpose: checks input to see if state is 2 letters AND in the database
        'Arguments:  strIn
        'Return: true/false
        'Date: 02/06/2014

        'check to see if length is 2 while seeing if it contains only letters
        If Len(strIn) = 2 Then
            'length is true
            Return CheckLetterWithSubstring(strIn) 'will return true if all are letters, false if not

        End If

        'failed length requirement
        Return False

    End Function

    Public Function CheckAirportCode(ByVal strIn As String) As Boolean
        'Author: Aaryaman Singhal edited Ben Shadburne's check state Function
        'Purpose: checks input to see if input is valid airport code
        'Arguments:  strIn
        'Return: true/false
        'Date: 02/06/2014

        'check to see if length is 2 while seeing if it contains only letters
        If Len(strIn) = 3 Then
            'length is true
            Return CheckLetterWithSubstring(strIn) 'will return true if all are letters, false if not

        End If

        'failed length test
        Return False

    End Function

    Public Function CheckMI(ByVal strIn As String) As Boolean
        'Author: Ben Shadburne
        'Purpose: checks input to see if middle initial
        'Arguments:  strIn
        'Return: true/false
        'Date: 02/06/2014

        'check to see if length is 2 while seeing if it contains only letters
        If Len(strIn) = 1 Then
            'length is true
            Return CheckLetterWithSubstring(strIn) 'will return true if all are letters, false if not

        End If

        'failed length test
        Return False

    End Function

    Public Function CheckPassword(ByVal strIn As String) As Boolean
        'Author: Ben Shadburne
        'Purpose: checks input to see if password is in correct format/length/meets requirements
        'Arguments:  strIn
        'Return: true/false
        'Date: 02/06/2014

        'first check if correct length
        If Len(strIn) > 5 And Len(strIn) < 11 Then

            'check if starts with a letter
            If CheckLetterWithSubstring(strIn.Substring(0, 1)) = False Then
                Return False
            End If

            'check if strIn contains number
            Dim intNumberCount As Integer = 0
            Dim i As Integer = 0

            For i = 0 To Len(strIn) - 1
                If CheckIntegerWithSubstring(strIn.Substring(i, 1)) = True Then
                    intNumberCount += 1
                End If
            Next

            If intNumberCount > 0 Then
                Return True
            End If

        End If

        'either doesn't contain both letter and number or not the correct lenth
        Return False


    End Function

    Public Function RetrievePhone(ByVal strIn As String) As String
        'Author: Ben Shadburne
        'Purpose: returns phone in correct format, retreives from db
        'Arguments:  strIn
        'Return: string of formatted phone number
        'Date: 02/06/2014

        'returns phone# in correct format
        Return CDbl(strIn).ToString("(000) 000-0000")

    End Function

    Public Function FormatName(ByVal strIn As String) As String
        'Author: Ben Shadburne
        'Purpose: capitalize first letter of input
        'Arguments:  strIn
        'Return: original string with capitalized first letter
        'Date: 02/06/2014

        'formatting names and initial to have first letter capitalized
        Return strIn.Substring(0, 1).ToUpper() & strIn.Substring(1, Len(strIn) - 1)

    End Function

    Public Function FormatZIP(ByVal strIn As String) As String
        'Author: Ben Shadburne
        'Purpose: inserts - if zip has 9 length
        'Arguments:  strIn
        'Return: zip with - if 9 length
        'Date: 02/06/2014

        'formats zip if length 9
        If Len(strIn) = 9 Then
            'make new string, set it's value to have a - in the 5th position
            Dim strConcat As String
            strConcat = strIn.Substring(0, 5) & "-" & strIn.Substring(4, 4)
            Return strConcat
        End If
        Return strIn

    End Function

    Public Function CheckInteger(strInput As String) As Integer
        'Purpose: checks if str is integer
        'Arguments: strInput
        'Returns: -1 or integer
        'Author: Ben Shadburne
        'Date: 01/16/2014

        'declarations
        Dim intResult As Integer

        'check for numeric, whole number
        Try
            intResult = CInt(strInput)
        Catch ex As Exception
            Return -1 'there's a problem
        End Try

        'check for correct range
        If intResult < 0 Then 'negative numbers not okay but 0 is fine
            Return -1
        End If

        'output
        Return intResult
    End Function

    Public Function CheckDecimal(strInput As String) As Decimal
        'Purpose: checks if str is decimal
        'Arguments: strInput
        'Returns: -1 or decimal
        'Author: Ben Shadburne
        'Date: 01/16/2014

        'declarations
        Dim decResult As Decimal

        'check for numeric, whole number
        Try
            decResult = Convert.ToDecimal(strInput)
        Catch ex As Exception
            Return -1 'there's a problem
        End Try

        'check for correct range
        If decResult <= 0 Then 'negative numbers not okay
            Return -1
        End If

        'output
        Return decResult
    End Function

    Public Function CheckLength(strInput As String, intLength As Integer) As Boolean
        'check the length of an input
        'Jace Barton
        'returns false if input does not equal passed in length, true otherwise
        If strInput.Length <> intLength Then
            Return False
        Else
            Return True
        End If
    End Function

End Class