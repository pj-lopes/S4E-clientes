const clienteControle = {
  table: null,
  modal: null,
  configurarComponentes: function () {
    var modalEl = document.getElementById('modalCliente');
    if (modalEl) {
      this.modal = new bootstrap.Modal(modalEl, { backdrop: 'static' });
    }

    $('#TipoCliente').select2({
      theme: 'bootstrap-5',
      dropdownParent: $('#modalCliente'),
      minimumResultsForSearch: Infinity,
      placeholder: "Selecione..."
    });

    $('#TipoCliente').on('select2:close', function (e) {
      $(this).valid();
    });

    var behavior = function (val) {
      return val.replace(/\D/g, '').length === 11 ? '000.000.000-009' : '00.000.000/0000-00';
    };
    var options = {
      onKeyPress: function (val, e, field, options) {
        field.mask(behavior.apply({}, arguments), options);
      }
    };

    $('#Documento').mask(behavior, options);
  },
  initTabela: function () {
    this.table = new Tabulator("#tabela-clientes", {
      layout: "fitColumns",
      pagination: "local",
      paginationSize: 10,
      placeholder: "<i class='fas fa-folder-open me-2'></i> Nenhum cliente encontrado",
      locale: "pt-br",
      langs: {
        "pt-br": {
          "pagination": {
            "first": "Primeira",
            "first_title": "Primeira Página",
            "last": "Última",
            "last_title": "Última Página",
            "prev": "Anterior",
            "prev_title": "Página Anterior",
            "next": "Próxima",
            "next_title": "Próxima Página",
            "page_title": "Página",
          }
        }
      },
      columns: [
        { title: "#", field: "Id", width: 60, hozAlign: "center", visible: false },
        { title: "NOME", field: "Nome" },
        {
          title: "TIPO",
          field: "TipoCliente",
          width: 130,
          formatter: function (cell) {
            var valor = cell.getValue();
            if (valor == 1) return '<span class="badge bg-primary-s4e text-white">Pessoa Física</span>';
            if (valor == 2) return '<span class="badge bg-secondary">Pessoa Jurídica</span>';
            return valor;
          }
        },
        {
          title: "DOCUMENTO",
          field: "Documento",
          formatter: function (cell) {
            var valor = cell.getValue();
            var data = cell.getRow().getData();

            if (!valor) return "";

            valor = valor.replace(/\D/g, "");

            if (data.TipoCliente == 1) {
              return valor.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4");
            } else {
              return valor.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, "$1.$2.$3/$4-$5");
            }
          }
        },
        { title: "EMAIL", field: "Email" },
      ]
    });
  },
  initValidacoes: function () {
    $("#formCliente").validate({
      ignore: '.select2-input, .select2-focusser',
      onfocusout: function (element) {
        this.element(element);
      },
      rules: {
        Nome: { required: true, minlength: 3 },
        Documento: { required: true, minlength: 14 },
        TipoCliente: "required",
        Email: { email: true }
      },
      messages: {
        Nome: "Informe o nome completo",
        Documento: "Informe um CPF ou CNPJ válido",
        TipoCliente: "Selecione o tipo",
        Email: "Informe um e-mail válido"
      },
      errorElement: "div",
      errorClass: "invalid-feedback",
      highlight: function (element) {
        $(element).addClass("is-invalid").removeClass("is-valid");

        if ($(element).hasClass("select2-hidden-accessible")) {
          $(element).next(".select2-container").find(".select2-selection").addClass("border-danger");
        }
      },
      unhighlight: function (element) {
        $(element).removeClass("is-invalid");

        if ($(element).hasClass("select2-hidden-accessible")) {
          $(element).next(".select2-container").find(".select2-selection").removeClass("border-danger");
        }
      },
      errorPlacement: function (error, element) {
        if (element.hasClass('select2-hidden-accessible')) {
          error.insertAfter(element.next('.select2'));
        } else {
          error.insertAfter(element);
        }
      }
    });

    $('#TipoCliente').on('change', function () {
      $(this).valid();
    });
  },
  bindEventos: function () {
    var $this = this;

    $("#btnNovoCliente").on("click", function () {
      $this.abrirModal();
    });

    $("#btnSalvar").on("click", function () {
      $this.salvarCliente();
    });

    $("#searchInput").on("keyup", function () {
      var term = $(this).val();
      
      $this.table.setFilter([
        [
          { field: "Nome", type: "like", value: term },
          { field: "Documento", type: "like", value: term }
        ]
      ]);
    });;

    $("#btnClearSearch").on("click", function () {
      $("#searchInput").val("");
      $this.table.clearFilter();
    });

  },
  listarClientes: function () {
    var $this = this;

    $.ajax({
      url: "/Clientes/ObterClientes",
      type: 'GET',
      dataType: 'json',
      success: function (response) {
        if (response.success) {
          $this.table.setData(response.dados);
        } else {
          toastr.error(response.message || "Erro ao carregar dados.");
        }
      },
      error: function () {
        toastr.error("Falha na comunicação com o servidor.");
      }
    });
  },
  salvarCliente: function () {
    var $this = this;
    var $form = $("#formCliente");

    if (!$form.valid()) {
      toastr.warning('Verifique os campos obrigatórios.');
      return;
    }

    var $btn = $("#btnSalvar");
    var textoOriginal = $btn.html();
    $btn.prop('disabled', true).html('<i class="fas fa-spinner fa-spin"></i> Processando...');

    var formData = $form.serialize();

    $.ajax({
      url: "/Clientes/SalvarCliente",
      type: 'POST',
      data: formData,
      success: function (response) {
        if (response.success) {
          toastr.success(response.message);
          $this.modal.hide();
          $this.listarClientes();
        } else {
          toastr.error(response.message);

          if (response.errors && response.errors.length > 0) {
            var htmlErro = "<ul>" + response.errors.map(e => `<li>${e}</li>`).join('') + "</ul>";
            toastr.warning(htmlErro, "Atenção aos detalhes:");
          }
        }
      },
      error: function (xhr) {
        toastr.error("Erro interno no servidor: " + xhr.statusText);
      },
      complete: function () {
        $btn.prop('disabled', false).html(textoOriginal);
      }
    });
  },
  abrirModal: function () {
    $("#modalTitle").text("Cadastrar Cliente");
    $("#Id").val(0);

    var form = $("#formCliente");
    form[0].reset();

    $('#TipoCliente').val(null).trigger('change');

    var validator = form.validate();
    validator.resetForm();

    form.find(".is-invalid").removeClass("is-invalid");
    form.find(".is-valid").removeClass("is-valid");
    $('.select2-selection').removeClass('border-danger')

    this.modal.show();
  },
  init() {
    this.configurarComponentes();
    this.initTabela();
    this.initValidacoes();
    this.bindEventos();
    this.listarClientes();
  }
}

$(document).ready(function () {
  clienteControle.init();
});