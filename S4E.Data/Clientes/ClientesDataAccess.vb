Imports System.Data.SqlClient
Imports System.Linq
Imports System.Configuration
Imports System.Security
Imports Dapper
Imports S4E.Domain

Public Class ClientesDataAccess
    Private ReadOnly _connectionString As String

    Public Sub New(connectionString As String)
        _connectionString = connectionString
    End Sub


    Public Async Function SalvarCliente(cliente As Cliente) As Task(Of Integer)

        Dim sQuery As String = "INSERT INTO Clientes (Nome, TipoCliente, Documento, Email) VALUES (@Nome, @TipoCliente, @Documento, @Email); SELECT CAST(SCOPE_IDENTITY() AS INT);"

        Using conn As New SqlConnection(_connectionString)
            Await conn.OpenAsync()

            Dim parametros = New With {
                Key .Nome = cliente.Nome,
                Key .TipoCliente = cliente.TipoCliente,
                Key .Documento = cliente.Documento,
                Key .Email = cliente.Email
            }

            Dim id As Integer = Await conn.ExecuteScalarAsync(Of Integer)(sQuery, parametros)

            Return id
        End Using
    End Function

    Public Function ListarTodos() As List(Of Cliente)
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Return conn.Query(Of Cliente)("SELECT ID, Nome, TipoCliente, Documento, Email FROM Clientes").ToList()
        End Using
    End Function

    Public Function ObterPorDocumento(documento As String) As Cliente
        Using conn As New SqlConnection(_connectionString)
            conn.Open()

            Dim parametros = New With {
                Key .Documento = documento
            }

            Return conn.Query(Of Cliente)("SELECT ID, Nome, TipoCliente, Documento, Email FROM Clientes", parametros).FirstOrDefault()
        End Using
    End Function

End Class
