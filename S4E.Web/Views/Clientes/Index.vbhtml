@Code
    ViewData("Title") = "Cadastro de Clientes"
End Code

@Section stylesheet
    <style>
        .card-header-custom {
            background-color: #fff;
            border-bottom: 1px solid #e3e6f0;
            padding: 15px 20px;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }
    </style>
End Section

@Section modal
    <div class="modal fade" id="modalCliente" tabindex="-1" aria-hidden="true" data-bs-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title text-primary-s4e"><i class="fas fa-user-edit me-2"></i> <span id="modalTitle">Cadastrar Cliente</span></h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">

                    <form id="formCliente">
                        <input type="hidden" id="Id" name="Id" value="0">

                        <div class="mb-3">
                            <label class="form-label fw-bold">Tipo de Cliente <span class="text-danger">*</span></label>
                            <select class="form-select" id="TipoCliente" name="TipoCliente" style="width: 100%;">
                                <option value="">Selecione...</option>
                                <option value="1">Pessoa Física</option>
                                <option value="2">Pessoa Jurídica</option>
                            </select>
                        </div>

                        <div class="mb-3">
                            <label class="form-label fw-bold">Nome / Razão Social <span class="text-danger">*</span></label>
                            <input type="text" class="form-control" name="Nome" id="Nome" placeholder="Digite o nome completo">
                        </div>

                        <div class="mb-3">
                            <label class="form-label fw-bold">Documento (CPF/CNPJ) <span class="text-danger">*</span></label>
                            <input type="text" class="form-control" name="Documento" id="Documento" placeholder="Insira seu documento">
                        </div>

                        <div class="mb-3">
                            <label class="form-label fw-bold">E-mail</label>
                            <input type="email" class="form-control" name="Email" id="Email" placeholder="exemplo@email.com">
                        </div>

                    </form>

                </div>
                <div class="modal-footer bg-light">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <button type="button" class="btn btn-s4e px-4" id="btnSalvar">
                        <i class="fa fa-save me-1"></i> Salvar
                    </button>
                </div>
            </div>
        </div>
    </div>
End Section


<main class="container-fluid mt-4">
    <div class="card shadow mb-4">
        <div class="card-header-custom">
            <div>
                <h5 class="m-0 font-weight-bold text-dark">Clientes</h5>
                <small class="text-muted">Gerenciamento de clientes</small>
            </div>
            <button class="btn btn-s4e btn-sm shadow-sm" id="btnNovoCliente">
                <i class="fa fa-plus-circle fa-sm text-white me-1"></i> Cadastrar Cliente
            </button>
        </div>

        <div class="card-body">
            <div class="row mb-3">
                <div class="col-md-5">
                    <div class="input-group">
                        <span class="input-group-text bg-white border-end-0"><i class="fa fa-search text-gray-400"></i></span>
                        <input type="text" id="searchInput" class="form-control border-start-0" placeholder="Pesquisar por nome ou documento...">
                        <button class="btn btn-light border" id="btnClearSearch">Limpar</button>
                    </div>
                </div>
            </div>

            <div id="tabela-clientes"></div>
        </div>
    </div>
</main>

@Section scripts
    <script src="~/Scripts/site/site.js"></script>
End Section

