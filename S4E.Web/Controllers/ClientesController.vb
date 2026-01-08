Imports System.Threading.Tasks
Imports System.Web.Mvc
Imports S4E.Application
Imports S4E.Domain

Namespace Controllers
    Public Class ClientesController
        Inherits Controller

        Private ReadOnly Property connString As String
            Get
                Return ConfigurationManager.ConnectionStrings("ConnectionSQLServer").ConnectionString
            End Get
        End Property

        Private ReadOnly _clienteService As ClientesServices

        Public Sub New()
            _clienteService = New ClientesServices(connString)
        End Sub

        Function Index() As ActionResult
            Dim lista = _clienteService.ListarTodosClientes()

            Return View(lista)
        End Function

        <HttpPost>
        Public Async Function SalvarCliente(cliente As ClienteDTO) As Task(Of ActionResult)
            If ModelState.IsValid Then
                Try
                    Dim novoId As Integer = Await _clienteService.SalvarCliente(cliente)

                    Return Json(New With {.success = True, .id = novoId, .message = "Cliente cadastrado com sucesso!"})
                Catch ex As Exception
                    ModelState.AddModelError("", ex.Message)
                    Return Json(New With {.success = False, .message = "Erro ao processar: " & ex.Message})
                End Try
            End If

            Dim erros = ModelState.Values.SelectMany(Function(v) v.Errors).Select(Function(e) e.ErrorMessage).ToList()

            Return Json(New With {.success = False, .message = "Dados inválidos.", .errors = erros})
        End Function

        Public Function ObterClientes() As ActionResult
            Try
                Dim lista As List(Of Cliente) = _clienteService.ListarTodosClientes()

                Return Json(New With {.success = True, .dados = lista}, JsonRequestBehavior.AllowGet)
            Catch ex As Exception
                ModelState.AddModelError("", ex.Message)
                Return Json(New With {.success = False, .message = "Erro ao processar: " & ex.Message}, JsonRequestBehavior.AllowGet)
            End Try
        End Function

    End Class
End Namespace