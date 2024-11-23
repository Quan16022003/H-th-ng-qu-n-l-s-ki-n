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

/**
 * Add new Entity
 * @param {any} url Url of add action in controller
 * @param {any} data Json data of DTO
 */
export const postEntity = (url, data) => {
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