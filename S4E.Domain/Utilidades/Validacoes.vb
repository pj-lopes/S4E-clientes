Imports System.Text.RegularExpressions
Imports System.Linq
Public Class Validacoes
    Private Shared ReadOnly RegexApenasNumeros As New Regex("[^\d]", RegexOptions.Compiled)
    Private Shared ReadOnly RegexEmailCheck As New Regex("\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase Or RegexOptions.Compiled)

    Public Shared Function LimparCpfCnpj(cpfCnpj As String) As String
        If String.IsNullOrEmpty(cpfCnpj) Then
            Return String.Empty
        End If

        ' Substituição otimizada
        Return RegexApenasNumeros.Replace(cpfCnpj, String.Empty)
    End Function

    Public Shared Function ValidarCpfCnpj(cpfCnpj As String, Optional aceitaVazio As Boolean = False) As Boolean
        If String.IsNullOrEmpty(cpfCnpj) Then
            Return aceitaVazio
        End If

        Dim soNumero As String = LimparCpfCnpj(cpfCnpj)

        If soNumero.Length = 0 Then Return False

        If soNumero.Distinct().Count() = 1 Then
            Return False
        End If

        If soNumero.Length = 11 Then
            ' CPF: 11 dígitos
            Return CalcularDigitos(soNumero, New Integer() {10, 9, 8, 7, 6, 5, 4, 3, 2},
                                             New Integer() {11, 10, 9, 8, 7, 6, 5, 4, 3, 2})

        ElseIf soNumero.Length = 14 Then
            ' CNPJ: 14 dígitos
            Return CalcularDigitos(soNumero, New Integer() {5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2},
                                             New Integer() {6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2})
        End If

        Return False
    End Function

    Private Shared Function CalcularDigitos(numero As String, peso1 As Integer(), peso2 As Integer()) As Boolean
        Dim soma As Integer
        Dim resto As Integer
        Dim digito As String
        Dim tempDoc As String

        tempDoc = numero.Substring(0, numero.Length - 2)

        soma = 0
        For i As Integer = 0 To peso1.Length - 1
            soma += Integer.Parse(tempDoc(i).ToString()) * peso1(i)
        Next

        resto = soma Mod 11
        If resto < 2 Then
            resto = 0
        Else
            resto = 11 - resto
        End If

        digito = resto.ToString()
        tempDoc = tempDoc & digito

        soma = 0
        For i As Integer = 0 To peso2.Length - 1
            soma += Integer.Parse(tempDoc(i).ToString()) * peso2(i)
        Next

        resto = soma Mod 11
        If resto < 2 Then
            resto = 0
        Else
            resto = 11 - resto
        End If

        digito = digito & resto.ToString()

        Return numero.EndsWith(digito)
    End Function

    Public Shared Function ValidarEmail(email As String) As Boolean
        If String.IsNullOrWhiteSpace(email) Then
            Return False
        End If

        email = email.Replace(" ", "")

        Return RegexEmailCheck.IsMatch(email)
    End Function

End Class
