/**
 * Delete entity by Id
 * @param {any} url Url of delete action in controller
 */
export const deleteEntity = (url) => {
    var token = $("input[name='__RequestVerificationToken']").val();
    $.ajax({
        url: url,
        type: 'DELETE',
        data: {
            __RequestVerificationToken: token,
        },
        success: (result) => {
            window.toastr.success("Delete event success")
            setTimeout(() => window.location.reload(), 2000)
        },
        error: (err) => {
            window.toastr.error("Failed to delete event")
        }
    })
}