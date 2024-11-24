import { getImageUrl } from "../service/service.js"

/**
 * Assgin event for preview image change when choose image file, 
 * should be called when load (e.g: in $(document).ready)
 * 
 * This only applied for partial view _ImageInput in dashboard
 */
export const assignImageInputEvent = () => {
    $(".choose-img-button").find("input[type=file]").on("change", (e) => {
        setImage(e.target);
    });
}

function setImage(input) {
    let imgContainer = $(".dashboard-img-input")[0];
    let imgElement = $(imgContainer).find("img")[0];

    getImageUrl(input.files[0])
        .then((res) => {
            imgElement.src = res;
            imgContainer.style.backgroundImage = `url(${res})`;
        })
        .catch((err) => {
            window.toastr.error("Cant read Image");
            $(".choose-img-button").find("input[type=file]").val('');
        });
}