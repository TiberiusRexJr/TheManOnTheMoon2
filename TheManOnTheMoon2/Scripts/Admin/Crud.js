//#region Functions

//#endregion
function AjaxGetALL(senderId)
{
    const CRUD_TYPE = "Get";

    const API_GET_ALL_PRODUCTS = " https://localhost:44383/api/Admin/GetAllProducts";
    const API_GET_ALL_CATEGORYS = " https://localhost:44383/api/Admin/GetAllCategories";
    const API_GET_ALL_BRANDS = " https://localhost:44383/api/Admin/GetAllBrands";
    const API_GET_ALL_USERS = " https://localhost:44383/";

    const API_GET_PRODUCT_BY_ID = "https://localhost:44383/api/Admin/GetProductById/";

    var sendToAdress = "";
    var tableType = " ";

    if (senderId == null) {
        ModalMessenger(null, false, CRUD_TYPE, "null parameters provided");
    }

    else {
        switch (senderId) {
            case "link_Inventory_Categories":
                sendToAdress = API_GET_ALL_CATEGORYS;
                tableType = "Category";
                break;
            case "link_Inventory_Brands":
                sendToAdress = API_GET_ALL_BRANDS;
                tableType = "Brand"
                break;
            case "link_Inventory_Products":
                sendToAdress = API_GET_ALL_PRODUCTS;
                tableType = "Product";
                break;
            case "link_Users_Users":
                sendToAdress = API_GET_ALL_USERS;
                tableType = "Users";
                break;
            default: ModalMessenger(senderId, false, CRUD_TYPE, "could not resolve api address to send data too");
        }
    }

    $.ajax({
        type: "GET",
        
        url: sendToAdress,
        success: function (response, jqXHR, data)
        {
            alert(data.Name);
            alert(data[0]);
            var data2 = $.parseJSON(data);
            alert(data2.Name);
            InventorySubTableAdjust($.parseJSON(data), tableType)
        },
        statusCode:
        {
            404: function (response, jqXHR) {
                ModalMessenger(senderId, false, CRUD_TYPE, "404-Not Found");
            },
            500: function (response, jqXHR) {
                var successStatus = false;
                ModalMessenger(senderId, false, CRUD_TYPE, "500-Internal Server Error");
            },
            200: function (response, jqXHR) {
                var successStatus = true;
                
                //XModalMessenger(senderId, successStatus, CRUD_TYPE, "200-SuccessFully Created");
            }
        },
        error: function (response, jqXHR, data) {
            ModalMessenger(senderId, false, CRUD_TYPE, "AjaxGET Errror");
        }
    })
}

function ModalMessenger(data, successStatus, crudType, serverMessage) {
    var successOrFailureMessage = " ";
    var successOrFailureMessageHeader = " ";



    if (data == null || successStatus === false) {
        $("#ModalAdminMessengerHeader").removeClass("bg-success");
        $("#ModalAdminMessengerHeader").addClass("bg-danger");

        $("#ModalAdminMessengerHeader").empty()
        $("#ModalAdminMessengerBody").empty()


        successOrFailureMessage = "UnSuccessfully";
        successOrFailureMessageHeader = "Failed" + "(" + serverMessage + ")";
    }
    else if (successStatus === true) {
        $("#ModalAdminMessengerHeader").removeClass("bg-danger");
        $("#ModalAdminMessengerHeader").addClass("bg-success");

        $("#ModalAdminMessengerHeader").empty()
        $("#ModalAdminMessengerBody").empty()

        successOrFailureMessage = "Successfully";
        successOrFailureMessageHeader = "Success!";

    }

    var htmlModalBodyElementMessage = '<p class="text-justify">' + data.Name + '" "' + crudType + '" "' + successOrFailureMessage + '</p>';

    var htmlModalBodyElementHeader = '<h4>' + successOrFailureMessageHeader + '</h4>';


    $("#ModalAdminMessengerBody").append(htmlModalBodyElementMessage);
    $("#ModalAdminMessengerHeader").append(htmlModalBodyElementHeader);

    $('#ModalAdminMessenger').modal('show');
    $('#ModalAdminMessenger').modal('hide');

}

function AjaxExistByName(dataObj, tableType) {
    const API_EXISTBYNAME_URL = "https://localhost:44310/api/Admin/ExistByName/";

    $.ajax({
        type: "GET",
        url: API_EXISTBYNAME_URL + dataObj.Name + "/" + tableType,
        statusCode:
        {
            404: function () { return false },

            400: function () { return false },

            302: function () { return true }

        },
        success: function (data, textStatus, jqXHR) {

        },
        error: function (jqxhr, textStatus, errorThrown) {

        }
    })
}

function InventorySubTableAdjust(data,tableType)
{
    var HTML_tableTitle = " ";


    if (data==null||tableType==null) {
        ModalMessenger("Update Table Failed", false, "GET", "NULL Data or null TAbleType");
    }
    else
    {
        switch (tableType)
        {
            case "Product":
                HTML_tableTitle = '<i class="fa-box fa-2x"></i>Products';
                    break;
            case "Category":
                HTML_tableTitle = '<i class="fas fa-columns fa-1x mr-1"></i>Categories';
                    break;
            case "Brand":
                HTML_tableTitle = '<i class="fas fa-tags fa-1x mr-1"></i>Brands';
                break;
            default: ModalMessenger("Update Table Failed", false, "GET", "NULL Data or null TAbleType");
        }
        
        $('#cardHeaderDataTableInventory').html(HTML_tableTitle);

        var keys = Object(data).keys;
        alert(keys[0]);
        $.each(keys, function (index, value) { alert(value) });
    }
}

