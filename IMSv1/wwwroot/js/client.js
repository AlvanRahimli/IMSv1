$("#newClientSelect").change(function () {
    let selId = $(this).children("option:selected").val();
    if (selId === "new") {
        $("#newClient").css("display", "block")
        $("#existingClient").css("display", "none")
    } else if (selId === "existing") {
        $("#existingClient").css("display", "block")
        $("#newClient").css("display", "none")
    }
})