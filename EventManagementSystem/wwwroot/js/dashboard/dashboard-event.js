import { postEntity, putEntity, deleteEntity } from "../service/service.js"
import { stylingDashboardPaginate } from "../service/datatables-utility.js";
import { assignImageInputEvent } from "./form-utility.js"
import { addSearch, loadMap, getLocationInformation, isSupportedLocation } from "../service/map-utility.js"

const route = "/dashboard/event";
const action = {
    addDetail: "add/detail",
    updateDetail: "update/detail",
    updateTiming: "update/timing",
    updateVenue: "update/venue",
    publish: "publish",
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
    assignFormEvent();
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

function assignFormEvent(){
    $(".dashboard-event-basic-infor-form.add-form").on("submit", (e) => {
        e.preventDefault();
        addDetailEvent(e.target);
    });

    $(".dashboard-event-basic-infor-form.update-form").on("submit", (e) => {
        e.preventDefault();
        updateDetailEvent(e.target);
    });

    $(".dashboard-event-time-form.update-form").on("submit", (e) => {
        e.preventDefault();
        updateTimingEvent(e.target);
    })

    $(".dashboard-event-venue-form.update-form").on("submit", (e) => {
        e.preventDefault();
        updateVenueEvent(e.target);
    })

    $(".dashboard-event-publish-form.update-form").on("submit", (e) => {
        e.preventDefault();
        publishEvent(e.target);
    })
}

function assignTabClick() {
    var tabs = $(".dashboard-event-tab").find(".nav-link.available");
    for (var item of tabs) {
        item.addEventListener("click", (e) => {
            tabClick(e.target);
        });
    }
}

function changeTab(tab) {
    let tabs = $(".dashboard-event-tab").find(".nav-link");
    for (var item of tabs) {
        let containerClass = item.dataset.classhandle;
        let container = $(`.${containerClass}`)[0];

        if (!container) continue;
        container.classList.add("d-none");
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

function clearInput(location) {
    for (let information in location) {
        var id = information;
        $(`#${id}`).val("");
    }
}

function onMapSearch(result) {
    let location = getLocationInformation(result);
    console.log(location);

    if (!isSupportedLocation(location)) {
        window.toastr.error("Không hỗ trợ địa điểm này");
        clearInput(location);
        return;
    }

    for (let information in location) {
        var id = information;
        $(`input[name="${id}"`).val(location[id]);
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
    let url = `${route}/${action.updateDetail}`;
    let formData = new FormData(form);

    putEntity(url, formData);
}

function updateTimingEvent(form) {
    let url = `${route}/${action.updateTiming}`;
    let formData = new FormData(form);

    putEntity(url, formData);
}

function updateVenueEvent(form) {
    let url = `${route}/${action.updateVenue}`;
    let formData = new FormData(form);

    putEntity(url, formData);
}

function publishEvent(form) {
    let url = `${route}/${action.publish}`;
    let formData = new FormData(form);

    putEntity(url, formData);
}

function deleteEvent() {
    var id = $(this).data("id");
    var url = `${route}/${action.delete}/${id}`
    deleteEntity(url);
}

//#endregion