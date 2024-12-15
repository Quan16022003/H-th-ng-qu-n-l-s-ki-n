const leaflet = window.leaflet;
const GeoSearchControl = window.GeoSearchControl;
const OpenStreetMapProvider = window.OpenStreetMapProvider;

/**
 * Load and get map from leaflet
 * Should be called in window onload
 * @param {any} config
 * @returns Leaflet object map
 */
export const loadMap = (config = undefined) => {
    if (!$("#map")[0]) return undefined;

    var map = leaflet.map('map', config ? config : {}).locate({ setView: true });

    leaflet.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://osm.org/copyright">OpenStreetMap</a> contributors',
    }).addTo(map);

    setTimeout(() => map.setZoom(20), 1000)
    return map;
}

/**
 * Add search utility to Map
 * @param {any} map
 */
export const addSearch = (map, onResultCallBack) => {
    const icon = leaflet.icon({
        iconUrl: "../../../images/marker.png",
        iconSize: [45, 65]
    })

    let searchControl = new GeoSearchControl({
        provider: new OpenStreetMapProvider(),
        style: 'bar',
        searchLabel: 'Nhập địa chỉ tại đây',
        marker: {
            icon: icon
        }
    }).addTo(map);

    map.addControl(searchControl);

    map.on("geosearch/showlocation", (result) => {
        setTimeout(() => map.setZoom(20), 500);
        onResultCallBack(result);
    })
}

export const disableInteractions = (map) => {
    map.dragging.disable();
    map.touchZoom.disable();
    map.scrollWheelZoom.disable();
    map.doubleClickZoom.disable();
    map.boxZoom.disable();
    map.keyboard.disable();
    if (map.tap) map.tap.disable(); // For touch devices
}

export const enableInteractions = (map) => {
    map.dragging.enable();
    map.touchZoom.enable();
    map.scrollWheelZoom.enable();
    map.doubleClickZoom.enable();
    map.boxZoom.enable();
    map.keyboard.enable();
    if (map.tap) map.tap.enable(); // For touch devices
}

/**
 * Not supported location with province or outside VietNam
 * @param {any} location
 * @returns
 */
export const isSupportedLocation = (location) => {
    for (var item in location) {
        if (!location[item] || location[item] === "") return false;
    }

    return true;
}

function gatherInformation(location, informations) {
    let isCityNext = false;
    let isPostalCodeNext = false;
    let count = 0;
    let tempStreet = "";

    for (let information of informations) {
        information = information.trim();
        if (count == 2) {
            tempStreet = information;
        }

        if (isCityNext) {
            location.city = information;
            isCityNext = false;
            isPostalCodeNext = true;
        }

        else if (isPostalCodeNext) {
            location.postalCode = information;
            isPostalCodeNext = false;
        }

        else if (information.includes("Đường")
            || information.includes("Street") || information.includes("Road")) {
            location.street = information;
        }

        else if (information.includes("Phường") || information.includes("Ward")) {
            location.ward = information;
        }

        else if (information.includes("Quận") || information.includes("District")) {
            location.district = information;
            isCityNext = true;
        }

        count++;
    }

    if (location.street === "") location.street = tempStreet;
}

/**
 * Get location information from result of geosearch
 * @param {any} result
 * @returns
 */
export const getLocationInformation = (result) => {
    let informations = result.location.raw.display_name.split(',');
    let location = {
        venueName: result.location.raw.name,
        street: "",
        city: "",
        district: "",
        ward: "",
        postalCode: "",
        latitude: result.location.raw.lat,
        longitude: result.location.raw.lon,
        address: result.location.raw.display_name
    }

    gatherInformation(location, informations);
    return location;
}