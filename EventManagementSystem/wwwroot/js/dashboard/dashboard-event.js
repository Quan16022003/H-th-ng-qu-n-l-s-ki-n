import { postEntity, deleteEntity } from "../service/service.js"
import { stylingDashboardPaginate } from "../service/datatables-utility.js";
import { assignImageInputEvent } from "./form-utility.js"
import { addSearch, loadMap } from "../service/map-utility.js"

const route = "/dashboard/event";
const action = {
    addDetail: "add/detail",
    updateDetail: "update/detail",
    delete: "handle-delete"
}

let map;

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
    $(".dashboard-event-basic-infor-form.add-form").on("submit", (e) => {
        e.preventDefault();
        addDetailEvent(e.target);
    });
    $(".dashboard-event-basic-infor-form.update-form").on("submit", (e) => {
        e.preventDefault();
        updateDetailEvent(e.target);
    });
    $(".delete-button").on("click", deleteEvent);

    assignTabClick();
    assignImageInputEvent();

    map = loadMap({
        minZoom: 7,
        doubleClickZoom: false
    });

    if (!map) return;

    addSearch(map, onMapSearch);

    $(".dashboard-event-venue-header").on("click", () => {
        map.invalidateSize(true);
    })
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

function tabClick(tab) {
    let tabs = $(".dashboard-event-tab").find(".nav-link");
    for (var item of tabs) {
        item.classList.remove("active");

    }

    tab.classList.add("active");
    changeTab(tab);
}

//#region Map

function onMapSearch(result) {
    console.log(result)
    let element = {
        venueName: "",
        city: "",
        district: "",
        ward: "",
        latitude: "",
        longitude: ""
    }
}

function gatherMapInformation(element, result) {
    let informations = result.raw.display_name.split(',');

    for (var item of informations) {
        //let road = 
    }
}

//#endregion

//#region REST

function addDetailEvent(form) {
    let url = `${route}/${action.addDetail}`
    let formData = new FormData(form);

    //for (var item of formData.entries()) {
    //    console.log(`${item[0]}: ${item[1]}`);
    //};

    postEntity(url, formData);
}

function updateDetailEvent(form) {
    let url = `${route}/${action.updateDetail}`
    let formData = new FormData(form);

    putEntity(url, formData);
}

function deleteEvent() {
    var id = $(this).data("id");
    var url = `${route}/${action.delete}/${id}`
    deleteEntity(url);
}

//#endregion