$(document).ready(function() {

    var grid = $("#gridProdutos").dxDataGrid({
        dataSource: [],
        filterRow: {
            visible: true,
            applyFilter: 'auto'
        },
        columnAutoWidth: true,
        loadPanel: {
            enabled: true,
        },
        height: 640,
        width: "100%",
        paging: {
            pageSize: 10
        },
        columnChooser: {
            enabled: true,
            mode: "select"
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [15, 25, 50],
            showInfo: true
        },
        allowColumnReordering: true,
        rowAlternationEnabled: true,
        allowColumnResizing: true,
        showBorders: true,
        scrolling: {
            columnRenderingMode: "virtual"
        },
        onToolbarPreparing: function(e) {
            e.toolbarOptions.items.unshift(
                {
                    location: "after",
                    template: function() {
                        return $("<div/>").append(
                            $(
                                '<input class="btn btn-flat btn-primary mr-2" id="limparFiltros" type="button" value="Limpar Filtros" onclick=""></input>'
                            )
                        );
                    },
                },
                {
                    location: "before",
                    template: function() {
                        return $("<div/>").append(
                            $(
                                '<input class="btn btn-flat btn-primary mr-2" id="pesquisarProdutos" type="button" value="Pesquisar"></input>'
                            )
                        );
                    },
                }
            );
        },
        columns: [
            {
                dataField: "id",
                caption: "Código Produto",
                dataType: "string",
            },
            {
                dataField: "name",
                caption: "Nome Produto",
                dataType: "string",
            },
            {
                dataField: "description",
                caption: "Descrição",
                dataType: "string",
            },
            {
                dataField: "price",
                caption: "Preço Produto",
                dataType: "number",
            },
            {
                dataField: "quantity",
                caption: "Quantidade Produto",
                dataType: "number",
            },
        ]
    }).dxDataGrid("instance");

    $("#limparFiltros").click(() => { grid.clearFilter() });

    $("#pesquisarProdutos").click(() => {
        $.ajax({
            url: "http://localhost:5000/api/Product/Listar",
            type: "GET",
            dataType: "json",
            success: function (request) {
                if (request.success && request.data) {
                    grid.option("dataSource", request.data);
                }
            },
            error: function(request){
                $.notify("Não foi possível carregar os dados.", "error");
            }
        });
    });
});