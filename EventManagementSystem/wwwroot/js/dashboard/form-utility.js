import { getImageUrl } from "../service/service.js"

/**
 * Assgin event for preview image change when choose image file, should be called when load (e.g: in $(document).ready)
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

/**
 * This make checkbox value to true false when submit form
 * 
 * Only work with bootstrap input checkbox and need an hidden input with class "form-check-value" inside "form-check" class to handle the value
 */
export const assignCheckBoxInput = () => {
    let containers = $(".form-check");

    for (var item of containers) {
        let checkBox = $(item).find(".form-check-input");
        let inputValue = $(item).find(".form-check-value")[0];

        inputValue.value = "true";

        checkBox.on('change', (e) => {
            if ($(e.target).is(':checked')) {
                inputValue.value = "true";
            }
            else inputValue.value = "false";

            console.log(inputValue.value);
        })
    }
}