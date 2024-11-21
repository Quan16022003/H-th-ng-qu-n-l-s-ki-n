import { addEntity, deleteEntity } from "../service/service.js"

const route = "/dashboard/category";
const action = {
    add: "handle-add",
    delete: "handle-delete"
}

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

    $(".delete-button").on("click", deleteCategory);
    $(".dashboard-category-form").on("submit", (e) => {
        e.preventDefault();
        addCategory();
    });
});

function addCategory() {
    let form = $(".dashboard-category-form");
    var url = `${route}/${action.add}`

    $(".dashboard-submit-button").addClass("disabled");
    addEntity(url, form);
}

function deleteCategory() {
    var id = $(this).data("id");
    var url = `${route}/${action.delete}/${id}`
    deleteEntity(url);
}