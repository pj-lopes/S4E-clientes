Imports System.Security
Imports S4E.Data
Imports S4E.Domain

Public Class ClientesServices
    Private ReadOnly _clientesDataAccess As ClientesDataAccess

    Public Sub New(connectionString As String)
        _clientesDataAccess = New ClientesDataAccess(connectionString)
    End Sub

    Public Async Function SalvarCliente(cliente As ClienteDTO) As Task(Of Integer)

        Dim clienteExistente = ObterPorDocumento(Validacoes.LimparCpfCnpj(cliente.Documento))

        If clienteExistente IsNot Nothing Then
            Throw New Exception("Já existe um cliente cadastrado com esse documento.")
        End If

        If String.IsNullOrEmpty(cliente.Nome) Then
            Throw New Exception("O nome do cliente é obrigatório.")
        End If


        If Not Validacoes.ValidarCpfCnpj(cliente.Documento) Then
            Throw New Exception("O documento informado é inválido.")
        End If


        If Not String.IsNullOrEmpty(cliente.Email) AndAlso Not Validacoes.ValidarEmail(cliente.Email) Then
            Throw New Exception("O e-mail informado não tem um formato válido.")
        End If

        Dim documentoFormatdo = Validacoes.LimparCpfCnpj(cliente.Documento)
        Dim clienteInsert = New Cliente(cliente.Nome, cliente.TipoCliente, documentoFormatdo, cliente.Email)

        Dim id = Await _clientesDataAccess.SalvarCliente(clienteInsert)
        Return id
    End Function

    Public Function ListarTodosClientes() As List(Of Cliente)
        Return _clientesDataAccess.ListarTodos()
    End Function

    Public Function ObterPorDocumento(documento As String) As Cliente
        Return _clientesDataAccess.ObterPorDocumento(documento)
    End Function

End Class
