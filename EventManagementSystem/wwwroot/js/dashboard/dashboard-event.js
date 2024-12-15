import { postEntity, putEntity, deleteEntity, reRender } from "../service/service.js"
import { stylingDashboardPaginate } from "../service/datatables-utility.js";
import { assignImageInputEvent } from "./form-utility.js"
import { addSearch, loadMap, getLocationInformation, isSupportedLocation } from "../service/map-utility.js"
import { eventRoute, ticketRoute } from "../service/route.js";

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
    assignTicketFormButton();

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

    $(".dashboard-event-ticket-form.update-form").on("submit", (e) => {
        e.preventDefault();
        updateTicketEvent(e.target);
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

function assignTicketFormButton() {
    $(".add-ticket-button").on("click", (e) => {
        e.preventDefault();
        onAddTicket(e.target);
    })

    $(".edit-ticket-button").on("click", (e) => {
        onEditTicket(e.currentTarget);
    })

    $(".delete-ticket-button").on("click", (e) => {
        onDeleteTicket(e.currentTarget);
    })

    $(".dashboard-ticket-form-cancel").on("click", (e) => {
        e.preventDefault();
        onOutTicketForm(e.currentTarget);
    })
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

//#region Ticket

function openTicketForm() {
    if (!$(".dashboard-ticket-form")[0].classList.contains("d-flex")) {
        $(".dashboard-ticket-form")[0].classList.add("d-flex");
    }
    $(".dashboard-ticket-form")[0].classList.remove("d-none");
}

function closeTicketForm() {
    if (!$(".dashboard-ticket-form")[0].classList.contains("d-none")) {
        $(".dashboard-ticket-form")[0].classList.add("d-none");
    }

    $(".dashboard-ticket-form")[0].classList.remove("d-flex");
}

function onAddTicket(button) {
    openTicketForm();
}

function onEditTicket(button) {
    let container = $(".dashboard-event-ticket-form-container")[0];
    let id = button.dataset.ticketid;

    reRender(`/dashboard/ticket/update-form/${id}`, container);

    setTimeout(() => {
        $(".dashboard-ticket-form-cancel").on("click", (e) => {
            e.preventDefault();
            onOutTicketForm(e.target);
        });
        openTicketForm();
    }, 200);
}

function onDeleteTicket(button) {
    let container = $(".dashboard-event-ticket-list")[0];
    let id = button.dataset.ticketid;
    let eventId = $(".dashboard-event-ticket-form").find("#eventId")[0].value;

    deleteEntity(`${ticketRoute.delete}/${id}`, false);

    if (eventId !== "" && eventId !== "-1") {
        setTimeout(() => reRender(`/dashboard/ticket/event/${eventId}`, container), 500);
        setTimeout(() => assignTicketFormButton(), 700);
    }
}
function onOutTicketForm(button = undefined) {
    closeTicketForm();
    clearTicketForm();
}

function clearTicketForm() {
    var inputList = $(".dashboard-ticket-form").find("input, textarea");

    for (let item of inputList) {
        item.value = "";
    }
}

//#endregion

//#region REST

function addDetailEvent(form) {
    let formData = new FormData(form);

    //for (var item of formData.entries()) {
    //    console.log(`${item[0]}: ${item[1]}`);
    //};

    postEntity(eventRoute.addDetail, formData);
}

function updateDetailEvent(form) {
    let formData = new FormData(form);
    putEntity(eventRoute.updateDetail, formData);
}

function updateTimingEvent(form) {
    let formData = new FormData(form);
    putEntity(eventRoute.updateTiming, formData);
}

function updateVenueEvent(form) {
    let formData = new FormData(form);
    putEntity(eventRoute.updateVenue, formData);
}

function updateTicketEvent(form) {
    let formData = new FormData(form);
    let id = "";
    let eventId = "";

    var container = $(".dashboard-event-ticket-list")[0];

    for (var item of formData.entries()) {
        if (item[0] === "id") id = item[1];
        if (item[0] === "eventId") eventId = item[1];

    };

    if (id === "" || id === "-1") postEntity(ticketRoute.add, formData);
    else putEntity(ticketRoute.update, formData);

    if (eventId !== "" && eventId !== "-1") {
        setTimeout(() => reRender(`/dashboard/ticket/event/${eventId}`, container), 2000);
        setTimeout(() => assignTicketFormButton(), 2200);
    }
    onOutTicketForm();
}

function publishEvent(form) {
    let formData = new FormData(form);
    putEntity(eventRoute.publish, formData);
}

function deleteEvent() {
    var id = $(this).data("id");
    var url = `${eventRoute.delete}/${id}`
    deleteEntity(url);
}

//#endregion