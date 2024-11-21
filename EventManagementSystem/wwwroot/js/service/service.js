function getJsonDataFromForm(form) {
    let arr = form.serializeArray();
    arr = arr.filter((e, i) => {
        if (e['name'] === "__RequestVerificationToken") return false;
        return true;
    })

    var index_arr = {};

    $.map(arr, function (obj, i) {
        index_arr[obj['name']] = obj['value'];
    });

    var json = JSON.stringify(index_arr);
    return JSON.parse(json);
}

function redirect(url) {
    if (url === undefined) return;
    window.location.href = url;
}

/**
 * Add new Entity
 * @param {any} url Url of add action in controller
 * @param {any} data Json data of DTO
 */
export const addEntity = (url, form) => {
    var token = $("input[name='__RequestVerificationToken']").val();
    var json = getJsonDataFromForm(form);

    $.ajax({
        url: url,
        type: 'POST',
        headers: {
            RequestVerificationToken: token,
        },
        data: json,
        success: (response) => {
            window.toastr.success(response.message)
            setTimeout(() => redirect(response.redirectUrl), 2000)
        },
        error: (xhr, status, error) => {
            let response = JSON.parse(xhr.responseText);
            if (xhr.status === 400) {
                window.toastr.error(response.message)
            }

            $(".dashboard-submit-button").removeClass("disabled");
        }
    })
}


/**
 * Delete entity by Id
 * @param {any} url Url of delete action in controller
 */
export const deleteEntity = (url) => {
    var token = $("input[name='__RequestVerificationToken']").val();
    $.ajax({
        url: url,
        type: 'DELETE',
        headers: {
            RequestVerificationToken: token,
        },
        success: (response) => {
            window.toastr.success(response.message)
            setTimeout(() => window.location.reload(), 2000)
        },
        error: (xhr, status, error) => {
            let response = JSON.parse(xhr.responseText);
            if (xhr.status === 400) {
                window.toastr.error(response.message)
            }
        }
    })
}