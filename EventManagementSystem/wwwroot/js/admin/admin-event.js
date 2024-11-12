$(document).ready(() => {
    $('.table').DataTable({
        search: false,
        searching: false,
        lengthChange: false,
        autoWidth: false,
        columnDefs: [
            { targets: -1, orderable: false }, // last col
            { targets: 8, orderable: false },
            { targets: 9, orderable: false },
            { target: 4, type: "date-eu" },
            { target: 5, type: "date-eu" },
            { target: 6, type: "date-eu" },
            { target: 7, type: "date-eu" },
        ],
        pagingType: "simple_numbers",
        drawCallback: stylingPaginate
    });

    stylingPaginate();
});

function stylingPaginate() { 
    $('.pagination').find('.previous').addClass("bg-light");
    $('.pagination').find('.next').addClass("bg-light");
}