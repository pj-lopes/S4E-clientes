Imports System.Security

Public Class Cliente
    Public Property ID As Integer
    Public Property Nome As String
    Public Property TipoCliente As TipoCliente
    Public Property Documento As String
    Public Property Email As String

    Public Sub New()
    End Sub

    Public Sub New(nome As String, tipoCliente As TipoCliente, documento As String, email As String)
        Me.Nome = nome
        Me.TipoCliente = tipoCliente
        Me.Documento = documento
        Me.Email = email
    End Sub

    Public Function Validar() As List(Of String)
        Dim erros As New List(Of String)()

        If String.IsNullOrWhiteSpace(Me.Nome) Then
            erros.Add("O nome é obrigatório.")
        End If


        If Not Validacoes.ValidarEmail(Me.Email) Then
            erros.Add("O e-mail informado é inválido.")
        End If


        If Not Validacoes.ValidarCpfCnpj(Me.Documento) Then
            erros.Add("O CPF ou CNPJ informado é inválido.")
        End If

        Return erros
    End Function
End Class
