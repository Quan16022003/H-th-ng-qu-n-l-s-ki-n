$(document).ready(() => {
    $('.dashboard-category-table').find(".table").DataTable({
        search: false,
        searching: false,
        lengthChange: false,
        autoWidth: false,
        columnDefs: [
            { targets: -1, orderable: false }, // last col
            { targets: 6, orderable: false },
            { target: 4, type: "date-eu" },
            { target: 5, type: "date-eu" },
        ],
        language: {
            paginate: {
                next: '',
                previous: ''
            }
        },
        pagingType: "simple_numbers",
        drawCallback: stylingAdminPaginate
    });

    stylingAdminPaginate();
});