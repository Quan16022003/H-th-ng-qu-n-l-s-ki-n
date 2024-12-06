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
    if (!$("#map")) return undefined;

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