import { deleteEntity } from "../service/service.js"

const route = "/dashboard/event";
const deleteAction = "handle-delete"

$(document).ready(() => {
    $('.dashboard-table-container').find(".table").DataTable({
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

    $(".delete-button").on("click", deleteEvent);
});

function deleteEvent() {
    var id = $(this).data("id");
    var url = `${route}/${deleteAction}/${id}`
    deleteEntity(url);
}