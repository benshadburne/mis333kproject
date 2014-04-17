Option Strict On
Imports Microsoft.VisualBasic



Public Class ClassCheckLogin

    'PURPOSE OF THIS CLASS IS TO MAKING CHECKING EMPLOYEE/CUSTOMER LOGINS EASIER

    Dim DBCustomer As New DBCustomersClone
    Dim DBEmployee As New DBEmployee




    'these need some work 
    Public Function CheckEmployee(ByVal strID As String) As Boolean




        DBEmployee.GetALLEmployeeUsingSP()

        Return True

    End Function

    Public Function CheckCustomer(ByVal strID As String) As Boolean
        DBCustomer.GetAllCustomersCloneUsingSP()


        Return True
    End Function

End Class
