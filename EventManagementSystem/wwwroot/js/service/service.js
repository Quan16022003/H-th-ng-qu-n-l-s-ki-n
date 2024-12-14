export const getImageUrl = (file) => new Promise((resolve, rejects) => {
    let reader = new FileReader();
    if (!file.type.startsWith('image/')) {
        reject(new Error("Selected file is not an image"));
        return;
    }

    reader.onload = (e) => {
        resolve(e.target.result);
    }
    reader.onerror = (e) => {
        rejects(new Error("Cant read file"));
    }

    reader.readAsDataURL(file);
})

function redirect(url) {
    if (url === undefined) return;
    window.location.href = url;
}

// for re-render but not reload the page
export const reRender = (url, container) => {
    $.ajax({
        url: url,
        type: 'GET',
        success: (response) => {
            container.innerHTML = response;
        }
    })
}

/**
 * Add new Entity
 * @param {any} url Url of add action in controller
 * @param {any} data Json data of DTO
 */
export const postEntity = (url, data, submitButton = undefined) => {
    var token = $("input[name='__RequestVerificationToken']").val();
    data.delete("__RequestVerificationToken");

    $.ajax({
        url: url,
        type: 'POST',
        headers: {
            RequestVerificationToken: token,
        },
        processData: false,
        contentType: false,
        data: data,
        success: (response) => {
            window.toastr.success(response.message)
            setTimeout(() => redirect(response.redirectUrl), 2000)
        },
        error: (xhr, status, error) => {
            if (submitButton) submitButton.removeClass("disabled");
            let response = JSON.parse(xhr.responseText);
            if (xhr.status === 400) {
                if (Array.isArray(response.message)) {
                    response.message.forEach((message, index) => {
                        setTimeout(() => {
                            window.toastr.error(message);
                        }, index * 100);
                    })
                } else {
                    window.toastr.error(response.message)
                }
                
            }
        }
    })
}

/**
 * Update entity
 * @param {any} url Url of update action in controller
 * @param {any} data Json data of DTO
 */
export const putEntity = (url, data, submitButton = undefined) => {
    var token = $("input[name='__RequestVerificationToken']").val();
    data.delete("__RequestVerificationToken");

    $.ajax({
        url: url,
        type: 'PUT',
        headers: {
            RequestVerificationToken: token
        },
        processData: false,
        contentType: false,
        data: data,
        success: (response) => {
            window.toastr.success(response.message)
            setTimeout(() => redirect(response.redirectUrl), 2000)
        },
        error: (xhr, status, error) => {
            if (submitButton) submitButton.removeClass("disabled");
            let response = JSON.parse(xhr.responseText);
            if (xhr.status === 400) {
                window.toastr.error(response.message)
            }
        }
    })
}


/**
 * Delete entity by Id
 * @param {any} url Url of delete action in controller
 */
export const deleteEntity = (url, isReload = true) => {
    var token = $("input[name='__RequestVerificationToken']").val();
    $.ajax({
        url: url,
        type: 'DELETE',
        headers: {
            RequestVerificationToken: token,
        },
        success: (response) => {
            window.toastr.success(response.message)
            if (isReload) setTimeout(() => window.location.reload(), 2000)
        },
        error: (xhr, status, error) => {
            let response = JSON.parse(xhr.responseText);
            if (xhr.status === 400) {
                window.toastr.error(response.message)
            }
        }
    })
}