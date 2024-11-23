import { getImageUrl, postEntity, deleteEntity } from "../service/service.js"

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
        drawCallback: stylingAdminPaginate
    });

    stylingAdminPaginate();
    assignEvent();
});

//#region assign event

function assignEvent() {
    $(".delete-button").on("click", deleteCategory);
    $(".dashboard-category-form").on("submit", (e) => {
        e.preventDefault();
        addCategory();
    });
    $(".choose-img-button").find("input[type=file]").on("change", (e) => {
        setImage(e.target);
    });
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

function setImage(input) {
    let imgContainer = $(".dashboard-category-img")[0];
    let imgElement = $(imgContainer).find("img")[0];

    getImageUrl(input.files[0])
        .then((res) => {
            imgElement.src = res;
            imgContainer.style.backgroundImage = `url(${res})`;
        })
        .catch((err) => {
            window.toastr.error("Cant read Image");
        });
}