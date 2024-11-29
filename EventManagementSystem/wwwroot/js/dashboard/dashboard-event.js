import { deleteEntity } from "../service/service.js"
import { stylingDashboardPaginate } from "../service/datatables-utility.js";
import { assignImageInputEvent } from "./form-utility.js"

const route = "/dashboard/event";
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
        drawCallback: stylingDashboardPaginate
    });

    stylingDashboardPaginate();
    assignElementEvent();
});

function assignElementEvent() {
    $(".dashboard-event-form").on("submit", (e) => {
        e.preventDefault();
        addEvent(e.target);
    })
    $(".delete-button").on("click", deleteEvent);
    assignTabClick();
    assignImageInputEvent();
}

function assignTabClick() {
    var tabs = $(".dashboard-event-tab").find(".nav-link");
    for (var item of tabs) {
        item.addEventListener("click", (e) => {
            tabClick(e.target);
        });
    }
}

function changeTab(tab) {
    let tabs = $(".dashboard-event-tab").find(".nav-link");
    for (var item of tabs) {
        let container = item.dataset.classhandle;
        $(`.${container}`)[0].classList.add("d-none");

    }

    let name = tab.dataset.classhandle;
    $(`.${name}`)[0].classList.remove("d-none");
}

function tabClick(tab){
    let tabs = $(".dashboard-event-tab").find(".nav-link");
    for (var item of tabs) {
        item.classList.remove("active");
        
    }

    tab.classList.add("active");
    changeTab(tab);
}

//#region REST

function addEvent(form) {
    let url = `${route}/${action.add}`
    let formData = new FormData(form);

    //for (var item of formData.entries()) {
    //    console.log(`${item[0]}: ${item[1]}`);
    //};

    $(".dashboard-submit-button").addClass("disabled");
    postEntity(url, formData);
}

function deleteEvent() {
    var id = $(this).data("id");
    var url = `${route}/${action.delete}/${id}`
    deleteEntity(url);
}

//#endregion