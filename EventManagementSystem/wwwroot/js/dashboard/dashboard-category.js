import { postEntity, deleteEntity } from "../service/service.js";
import { stylingDashboardPaginate } from "../service/datatables-utility.js";
import { assignImageInputEvent } from "./form-utility.js"

const route = "/dashboard/category";
const action = {
    add: "handle-add",
    delete: "handle-delete"
}

$(document).ready(() => {
    $('.dashboard-table-container').find(".table").DataTable({
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
        drawCallback: stylingDashboardPaginate
    });

    stylingDashboardPaginate();
    assignEvent();
});

//#region assign event

function assignEvent() {
    $(".delete-button").on("click", deleteCategory);
    $(".dashboard-category-form").on("submit", (e) => {
        e.preventDefault();
        addCategory();
    });
    assignImageInputEvent();
}

//#endregion

//#region REST

function addCategory() {
    let form = $(".dashboard-category-form")[0];
    let url = `${route}/${action.add}`

    let formData = new FormData(form);

    $(".dashboard-submit-button").addClass("disabled");
    postEntity(url, formData);
}

function deleteCategory() {
    let id = $(this).data("id");
    let url = `${route}/${action.delete}/${id}`
    deleteEntity(url);
}

//#endregion