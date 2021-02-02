function newProduct(num) {
    let newCard = $("#productCard").html();
    newCard = newCard.replaceAll("Content[0]", "Content[" + num + "]");
    let element = $(newCard);
    element.attr("id", "prod_card_" + num);
    element.appendTo("#productsList");
    
    element.find("#delete_btn").attr("onclick", "deleteCard(" + num + ")");
    element.find("#productId").attr("data-number", num);
    
    $("#newProductBtn").attr("onclick", "newProduct(" + (num+1) + ")");
}

function deleteCard(num) {
    let element = $("#prod_card_" + num);
    element.remove();
    $("#newProductBtn").attr("onclick", "newProduct(" + (num) + ")");
}

$("#client").change(function () {
    let selId = $(this).children("option:selected").val();
    if (parseInt(selId) === 0) {
        $("#nameDiv").css("display", "block")
    } else {
        $("#nameDiv").css("display", "none")
    }
})

$("#productId").change(function () {
    let selId = $(this).children("option:selected").getAttribute("data-price");
    let number = this.getAttribute("data-number");

    
});
