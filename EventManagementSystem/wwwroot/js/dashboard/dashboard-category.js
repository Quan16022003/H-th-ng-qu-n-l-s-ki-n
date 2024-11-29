import { postEntity, putEntity, deleteEntity } from "../service/service.js";
import { stylingDashboardPaginate } from "../service/datatables-utility.js";
import { assignCheckBoxInput, assignImageInputEvent } from "./form-utility.js"

const route = "/dashboard/category";
const action = {
    add: "handle-add",
    put: "handle-update",
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
    $(".add-form").on("submit", (e) => {
        e.preventDefault();
        addCategory();
    });

    $(".update-form").on("submit", (e) => {
        e.preventDefault();
        updateCategory();
    })

    assignCheckBoxInput();
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

function updateCategory() {
    let form = $(".dashboard-category-form")[0];
    let url = `${route}/${action.put}`

    let formData = new FormData(form);

    for (var item of formData.entries()) {
        console.log(`${item[0]}: ${item[1]}`);
    };

    $(".dashboard-submit-button").addClass("disabled");
    putEntity(url, formData);
}

function deleteCategory() {
    let id = $(this).data("id");
    let url = `${route}/${action.delete}/${id}`
    deleteEntity(url);
}

//#endregion