import { postEntity, putEntity, deleteEntity } from "../service/service.js";
import { stylingDashboardPaginate } from "../service/datatables-utility.js";
import { assignCheckBoxInput, assignImageInputEvent } from "./form-utility.js";
import { categoryRoute } from "../service/route.js";

$(document).ready(() => {
    $('.dashboard-table-container').find(".table").DataTable({
        search: false,
        searching: false,
        lengthChange: false,
        autoWidth: false,
        columnDefs: [
            { targets: -1, orderable: false }, // last col
            { targets: 5, orderable: false },
            { target: 3, type: "date-eu" },
            { target: 4, type: "date-eu" },
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
    $(".dashboard-category-form.add-form").on("submit", (e) => {
        e.preventDefault();
        addCategory();
    });

    $(".dashboard-category-form.update-form").on("submit", (e) => {
        e.preventDefault();
        updateCategory();
    })

    assignImageInputEvent();
}

//#endregion

//#region REST

function addCategory() {
    let form = $(".dashboard-category-form")[0];
    let submitButton = $(".dashboard-submit-button");

    let formData = new FormData(form);

    submitButton.addClass("disabled");
    postEntity(categoryRoute.add, formData, submitButton);
}

function updateCategory() {
    let form = $(".dashboard-category-form")[0];
    let submitButton = $(".dashboard-submit-button");

    let formData = new FormData(form);

    //for (var item of formData.entries()) {
    //    console.log(`${item[0]}: ${item[1]}`);
    //};

    submitButton.addClass("disabled");
    putEntity(categoryRoute.put, formData, submitButton);
}

function deleteCategory() {
    let id = $(this).data("id");
    let url = `${categoryRoute.delete}/${id}`
    deleteEntity(url);
}

//#endregion