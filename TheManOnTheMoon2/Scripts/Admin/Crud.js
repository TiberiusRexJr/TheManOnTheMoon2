//#region Constants
const TableType = { "BRAND": "Brand", "PRODUCT": "Product", "CATEGORY": "Category" };
Object.freeze(TableType);

const ApiPut = { "BRAND": "https://localhost:44383/api/Admin/PutBrand/formData", "PRODUCT": "https://localhost:44383/api/Admin/PutProduct/formData", "CATEGORY": "https://localhost:44383/api/Admin/PutCategory/formData" };
Object.freeze(ApiPut);

const CRUDType = { "POST": "Post", "PUT": "Put", "GET": "Get", "DELETE": "Delete" };
Object.freeze(CRUDType);

const ApiGet = {
    "PRODUCTS": "https://localhost:44383/api/Admin/GetAllProducts", "CATEGORIES": " https://localhost:44383/api/Admin/GetAllCategories", "BRANDS":"https://localhost:44383/api/Admin/GetAllBrands"}
//#endregion

//#region Classes
class Brand
{
    constructor(Name)
    {
        this.Name = Name;
    }
}


class Category {
    constructor(Name) {
        this.Name = Name;
    }
}

class Product {
    constructor(Name_NotNull, Description, Upc_NotNull, Brand, Length, Weight, Height, Cost, Retail_Price, Sale_Price, Stock_QuantityNotNull, Category, Sub_Category, On_Sale_Category, Width, Type, Sub_Type, Image_Main, Image_Alt_1, Image_Alt_2) {
        this.Name = Name_NotNull, this.Description = Description, this.Upc = Upc_NotNull, this.Brand = Brand, this.Length = Length, this.Weight = Weight, this.Height = Height, this.Cost = Cost, this.Retail_Price = Retail_Price, this.Sale_Price = Sale_Price, this.Stock_Quantity = Stock_QuantityNotNull,
            this.Category = Category, this.Sub_Category = Sub_Category, this.On_Sale_Category = On_Sale_Category, this.Width = Width, this.Type = Type, this.Sub_Type = Sub_Type, this.Image_Main = Image_Main, this.Image_Alt_1 = Image_Alt_1, this.Image_Alt_2 = Image_Alt_2
    };
    }

//#endregion

//#region DocumentOnREady
$(document).ready(function () {
    AjaxGetALL("link_Inventory_Brands");

    DynamicInsertion();
    //todo Pending
    $("buttoneditbrandsubmit").on('click', function ()
    {

    });

    

})
//#endregion

//#region Add "new" Buttons
    $("#ButtonAddBrand").on('click', function () { $("#Modal_Add_Brand").modal("show") });
    $("#ButtonAddProduct").on('click', function () {$("#Modal_Add_Product").modal("show") });
    $("#ButtonAddCategory").on('click', function () { $("#Modal_Add_Category").modal("show") });
    //#endregion

//#region Reset *modal forms,reset *imageSelect
    $(".btn.btn-secondary.closeClearForm").on("click", function (event)
    {
        alert("hi asshole");
        $('.form_module').trigger("reset");



        var boxZone = $(event.currentTarget).parent().parent().find('.modal-body').find('.preview-zone').find('.box-body');
      
        var previewZone = $(event.currentTarget).parent().parent().find('.modal-body').find('.preview-zone');
        
        var dropZone = $(event.currentTarget).parent().parent().find('.modal-body').find('.form_image_upload').find('.form-group').find('.dropzone');
      
        boxZone.empty();
        previewZone.addClass('hidden');
        reset(dropZone);

        



            //var boxZone = $(this).parents('.preview-zone').find('.box-body');
            //var previewZone = $(this).parents('.preview-zone');
            //var dropzone = $(this).parents('.form-group').find('.dropzone');
            //boxZone.empty();
            //previewZone.addClass('hidden');
            //reset(dropzone);
        
    });
    //#endregion

//#region Dynamic Insertion Stuff

function AjaxGetSelectData(tableType) {
    var sendToAdress = "";
    let result;

    switch (tableType) {
        case "Product": sendToAdress = ApiGet.PRODUCTS;
            break;
        case "Brand": sendToAdress = ApiGet.BRANDS;
            break;
        case "Category": sendToAdress = ApiGet.CATEGORIES;
            break;
        default:
            ModalMessenger("null", false, CRUDType.GET, "Failed to set a Send To Adress");
    }

    try {
        return $.ajax(

            {
                type: CRUDType.GET,
                url: sendToAdress,

            })
    }
    catch (error) {
        ModalMessenger("fucked up", false, CRUDType.GET, "Failed to get data for dynmnaic <Select>");
    }
}

function InsertDynamicSelectData(data, tableType) {
    var target;
    var target_2;

    if (data == null) { ModalMessenger("null", false, "Dynamic Inserttion", "data sent was null") };

    switch (tableType) {
        case "Brand": target = "#product_brand_Select"; target_2 = "#product_brand_Select_Edit";
            break;
        case "Category": target = "#product_category_select"; target_2 = "#product_category_select_Edit";
            break;
        default: ModalMessenger("null", false, "im in her mouth like toothpase", "fuck nigga");
    }

    $.each(data, function (index, obj) {
        $(target).append('<option value="' + obj.Name + '">' + obj.Name + '</option>');
        $(target_2).append('<option value="' + obj.Name + '">' + obj.Name + '</option>');
    });



}

async function DynamicInsertion() {
    await AjaxGetSelectData(TableType.BRAND).then((data) => { InsertDynamicSelectData(data,TableType.BRAND) });
    

    await AjaxGetSelectData(TableType.CATEGORY).then((data) => { InsertDynamicSelectData(data, TableType.CATEGORY) });

}
//#endregion

//#region AjaxCrud 
function AjaxGetALL(senderId) {
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
        success: function (response, jqXHR, data) {
            
            IntializeDatatable(data.responseJSON, tableType);
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

function AjaxDeleteRecords()
{

    //todo API_ADRESSES
    //#region ApiAdress
    const API_DELETE_PRODUCTS = " https://localhost:44383/api/Admin/DeleteProducts/products";
    const API_DELETE_CATEGORYS = " https://localhost:44383/api/Admin/DeleteCategorys/categories";
    const API_DELETE_BRANDS = " https://localhost:44383/api/Admin/DeleteBrands/brands";
    const API_DELETE_USERS = " https://localhost:44383/api/Admin/DelteUser/users"; 
    //#endregion

    var table = null;
    var rowsSelected = null;
    var tableTypeSelected = null;
    var idsToDelete = null;
    var sendToAdress = null;
    var tableTypeForApi = null;

    table = $("#dataTableInventory").DataTable();

    tableTypeSelected = $("#cardHeaderDataTableInventory").text();

    if (tableTypeSelected == null)
    {
        ModalMessenger("Delete Failed!", false, "Delete", "Failed to get TableType");

    }
    

    if (table == null) {
        ModalMessenger("Get DataTable to Get Selected Rows", false, "GET", "Unable to retrieve Datatable instance");

    }
    else {
        rowsSelected = table.rows('.selected').data();
        
        if (rowsSelected === undefined || rowsSelected.length==0) {
            ModalMessenger("Nothing to Delete!", false, "Delete", "Please Select a row or Rows");

        }
        else
        {

            idsToDelete = new Array(rowsSelected.length);

            $.each(rowsSelected, function (index, record) {
                idsToDelete[index] = record;
            });
            
           
        }

        

    }
    switch (tableTypeSelected) {
        case "Categories":
            sendToAdress = API_DELETE_CATEGORYS;
            break;
        case "Brands":
            sendToAdress = API_DELETE_BRANDS;
            break;
        case "Products":
            sendToAdress = API_DELETE_PRODUCTS;
            break;
        case "Users":
            sendToAdress = API_DELETE_USERS;
            break;
        default: ModalMessenger(tableTypeSelected, false, "Delete", "could not resolve api address to send data too");
    }

    $.ajax({
        type: "DELETE",
        data: {'': idsToDelete },
        url: sendToAdress,
        success: function (response, jqXHR, data) {

            ModalMessenger(idsToDelete, true, "Delete", "Deleted Successfully");
            var table = $("#dataTableInventory").DataTable();
            table.rows('.selected').remove().draw();

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
            ModalMessenger(senderId, false, CRUD_TYPE, "Ajax Delete Errror");
        }
    })

}

function AjaxEditRecord(event, Id) {

    var table = $('#dataTableInventory').DataTable();

    $('.dataTableInventoryRow').removeClass('selected');

    $("#" + event.currentTarget.id).toggleClass('selected');

    console.log(event.currentTarget.id);
    console.log(Id);
    var RowData = table.row('#' + Id).data();
   

    switch ($('#cardHeaderDataTableInventory').text())
    {
        case 'Products': SetEditProductModal(RowData);
            break;
        case 'Brands': SetEditBrandModal(RowData);
            break;
        case 'Categories': SetEditCategoryModal(RowData);
            break;
        default: ModalMessenger("Failure!", false, "Initiate Record Editing", "Unable to Distinguish Category Type!");
    }

    $("#form_brand_name").find("#Id").val(RowData.Id);
    $("#form_brand_name").find("#Name").val(RowData.Name);



}



function AjaxPost(senderId, FormToSend)
{
    const CRUD_TYPE = "Posted";
  

    const API_POST_PRODUCT = " https://localhost:44383/api/Admin/PostProduct/objData";
    const API_POST_CATEGORY = " https://localhost:44383/api/Admin/PostCategory/objData";
    const API_POST_BRAND = " https://localhost:44383/api/Admin/PostBrand/objData";

    var sendToAdress = "";
    var tableType = " ";
    console.log(FormToSend);
    if (FormToSend == null || senderId == null) {
        ModalMessenger(null, false, CRUD_TYPE, "null parameters provided");
    }

    else {
        switch (senderId) {
            case "Product":
                sendToAdress = API_POST_PRODUCT;
                tableType = "Product";
                break;
            case "Category":
                sendToAdress = API_POST_CATEGORY;
                tableType = "Category"
                break;
            case "Brand":
                sendToAdress = API_POST_BRAND;
                tableType = "Brand";
                break;
            case "ButonPostProductImageUrls":
                sendToAdress = API_POST_PRODUCT_IMAGES;
                tableType = "ProductImagesUrl";
                break;
            default: ModalMessenger(FormToSend, false, CRUD_TYPE, "could not resolve api address to send data too");
        }
    }
    //if (AjaxExistByName(data.Name, tableType)) {
    //    ModalMessenger(data, false, CRUD_TYPE, data.Name + " Already Exist!");
    //}

    $.ajax({
        type: "POST",
        data: FormToSend,
        url: sendToAdress,
        contentType: false,
        processData: false,
        success: function (response, jqXHR, data)
        {
            console.log(response.responseJSON);
        },
        statusCode:
        {
            400: function (response, jqXHR) {
                ModalMessenger("400", false, CRUD_TYPE, "400-BadRequest");
            },
            500: function (response, jqXHR) {
                var successStatus = false;
                ModalMessenger("500", false, CRUD_TYPE, "500-Internal Server Error");
            },
            201: function (response, jqXHR) {
                var successStatus = true;
                $('.form_add').trigger("reset");
                $(".modal fade add").modal('hide');
                ModalMessenger("201", successStatus, CRUD_TYPE, "201-SuccessFully Created");
            },
            415: function (response, jqXHR) {
                var successStatus = false;
                ModalMessenger("Invalid Media Type", successStatus, CRUD_TYPE, "415-Invalid medai type");

            },
            302: function (response, jqXHR) {
                var successStatus = false;
                ModalMessenger("Already Exist!", successStatus, CRUD_TYPE, "302 Resource Found");
            }
        },
        error: function (response, jqXHR, data) {
            ModalMessenger("error", false, CRUD_TYPE, "AjaxPost Errror");
            }
    })
}

function AjaxSubmitEdit(TableType, FormData)
{
    var sendToAdress = "";

    if (TableType == null || FormData == null)
    {
        ModalMessenger("TableType or Formdata", false, "Submit Record Changes", "null data provided");
    }

    switch (TableType)
    {
        case 'Brand': sendToAdress = ApiPut.BRAND;
            break;
        case 'Product': sendToAdress = ApiPut.PRODUCT;
            break;
        case 'Category': sendToAdress = ApiPut.CATEGORY;
            break;
        default: ModalMessenger("Can't Settle a Api Address", false, CRUDType.PUT, "Could not settle upon a apiAdress to Send Updated data too");
    }

    $.ajax({
        url: sendToAdress,
        type: CRUDType.PUT,
        data: FormData,
        contentType: false,
        processData: false,
        success: function (response, jqXHR, data) {

        },
        statusCode:
        {
            500: ModalMessenger(FormData, false, CRUDType.PUT, "Failed to Update"),
            200: ModalMessenger(FormData, true, CRUDType.PUT, "Successfully Updated!")
        },
        error: ModalMessenger(FormData, false, CRUDType.PUT, "Something Fucked up with AJax")
    });
}

//#endregion

//#region AjaxUtilities

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
//#endregion

//#region Modals
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

function SetEditProductModal(RowData) {
    if (RowData == null) {
        ModalMessenger("null data!", false, "SetEditProductModal", "null data provided");
    }
    else {


    }
}

function SetEditCategoryModal(RowData) {
    if (RowData == null) {
        ModalMessenger("null data!", false, "SetEditProductModal", "null data provided");
    }
    else {
        $("#CategoryIdEdit").val(RowData.Id);
        $("#CategoryNameEdit").val(RowData.Name);
        $("#Modal_Edit_Category").modal('show');
    }

}

function SetEditBrandModal(RowData) {
    if (RowData == null) {
        ModalMessenger("null data!", false, "SetEditProductModal", "null data provided");
    }
    else {
        $("#BrandIdEdit").val(RowData.Id);
        $("#BrandNameEdit").val(RowData.Name);

        var boxzone = $("#BrandNameEdit").parent().parent().parent().find('.form_image_upload').find('.preview-zone').find('.box').find('.box-body');
        console.log(boxZone);
        $("#Modal_Edit_Brand").modal("show");




    }

}
//#endregion

//#region Datables

function IntializeDatatable(data, tableType)
{
    var HTML_tableTitle = " ";
    if (data == null || tableType==null) {
        ModalMessenger("InitialzizeDatable", false, "GET", "data passed to IntialzieDatable was NULL");
    }
    else {
        switch (tableType) {
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



    }
  


    if (data.length > 0)
    {
        if ($.fn.DataTable.isDataTable('#dataTableInventory'))
        {
            
            $('#dataTableInventory').DataTable().destroy();
        }
        
            

            //todo Individual Objects
            var indiviaulObj = data[0];

            //todo Keys
            var keys = Object.keys(indiviaulObj);

            //todo columnArrays
            var columnsArray = new Array(keys.length);

            $("#dataTableInventoryThead_Tr").empty();
            $.each(keys, function (index, key) {
                columnsArray[index] = {
                    data: key, title: key
                };
               
                $("#dataTableInventoryThead_Tr").append('<th> </th>');
            })



            var table = $('#dataTableInventory').DataTable(
                {
                    responsive: true,
                    columns: columnsArray,
                    columnDefs: [
                        {
                            'targets': 0,
                            "render": function (Id, type, row) {
                                return '<a class="btn btn-primary bg-warning" href="#" id="' + tableType + '" role="button"  onclick="AjaxEditRecord(event,' + Id + ')"><i class="fas fa-edit"></i></a>' + Id;
                            },
                        },
                        {
                            'targets': 2,
                            'render': function (Image_Main,) {
                                return '<img src="../../Images/' + Image_Main + '" class="img-fluid" alt="Responsive image">'
                            }
                        }
                    ],
                    rowId: function (data) { return data.Id },
                    createdRow: function (row, data, dataIndex) {
                        $(row).addClass('dataTableInventoryRow');

                    },
                    select: {
                        'style': 'multi'
                    },
                    retrieve: true,
                    data: data,
                    select: "multi",
                    autoWidth: true,
                    pageLength: data.length,

                });

            $('.dataTableInventoryRow').on('click', this, function () {
                $("#" + this.id).toggleClass('selected');
            });
        
        
    }
    else
    {
        var table = $('#dataTableInventory').DataTable();
        table.clear();
        table.destroy();
        $('#dataTableInventoryThead_Tr').empty();
        $("#dataTableInventoryThead_Tr").append('<th> </th>');

        var table2 = $('#dataTableInventory').DataTable();
        
        console.log("hi from empty");
    }
 

};

function GetSelectedRows(table) {
    

    
}
//#endregion

//#region PrepareFormSubmission Area
function CreateSendCategoryFormData() {


    var imageElement = null;

    var ImageDataBase64 = null;
    var ImageMimeType = null;

    var FormToSend = new FormData();

    if ((!$("#previewZoneAddCategory").hasClass("hidden"))) {
        imageElement = $("#boxBodyAddCategory").children()[0];
        ImageDataBase64 = $(imageElement).attr('src');

        ImageMimeType = $("#boxBodyAddCategory").children()[2].innerText;


        var baseformated = getBase64Image(imageElement);
        

        var categoryImage = base64ToBlob(baseformated, ImageMimeType);
    
       
        FormToSend.append("ImageData", categoryImage);



    }
    let c = new Category($("#CategoryName").val());

    console.log(c.Name);

    FormToSend.append("ObjectData", JSON.stringify(c));

        AjaxPost(TableType.CATEGORY, FormToSend);

}

function CreateSendBrandFormData() {


    var imageElement = null;

    var ImageDataBase64 = null;
    var ImageMimeType = null;
    var brandImage = null;
    var FormToSend = new FormData();

    if ((!$("previewZoneAddBrand").hasClass("hidden"))) {
        imageElement = $("#boxBodyAddBrand").children()[0];
        ImageDataBase64 = $(imageElement).attr('src');

        ImageMimeType = $("#boxBodyAddBrand").children()[2].innerText;


        var baseformated = getBase64Image(imageElement);


        brandImage = base64ToBlob(baseformated, ImageMimeType);

       

        FormToSend.append("ImageData", brandImage);

    }

    let b = new Brand($("#BrandName").val());

    console.log(b.Name);
    FormToSend.append("ObjectData", JSON.stringify(b));

    AjaxPost("Brand", FormToSend);




}

function CreateSendProductFormData() {

    var ProductObj = new Product($("#product_name").val(), $("#product_description").val(), $("#product_upc").val(), $("#product_brand_Select").val(), $("#product_length").val(), $("#product_width").val(), $("#product_height").val(), $("#product_purchase_cost").val(), $("#product_retail_price").val(), $("#product_sale_price").val(), $("#product_stock_quantity").val(), $("#product_category_select").val(), $("#product_sub_category").val(), $("#product_onSale_Status").val(), $("#product_width").val(), $("#product_type_Select").val(), $("#product_sub_type_select").val(), "", "", "");
    console.log(ProductObj);

    var imageElement = null;
    
    var ImageDataBase64 = null;
    var ImageMimeType = null;


    var FormToSend = new FormData();

    if ((!$("#ProductImageMain_Add").find("#previewZoneAddProductImageMain").hasClass("hidden")))
    {
        imageElement = $("#boxBodyAddProductImageMain").children()[0];
        ImageDataBase64 = $(imageElement).attr('src');

        ImageMimeType = $("#boxBodyAddProductImageMain").children()[2].innerText;


        var baseformated = getBase64Image(imageElement);


        var productImage = base64ToBlob(baseformated, ImageMimeType);

        

        FormToSend.append("ProductImageMain", productImage);
    }

    if ((!$("#ProductImageAlt1_Add").find("#previewZoneAddProductImageAlt1").hasClass("hidden"))) {
        imageElement = $("#boxBodyAddProductImageAlt1").children()[0];
        ImageDataBase64 = $(imageElement).attr('src');

        ImageMimeType = $("#boxBodyAddProductImageAlt1").children()[2].innerText;


        var baseformated = getBase64Image(imageElement);


        var productImage = base64ToBlob(baseformated, ImageMimeType);



        FormToSend.append("ProductImageAlt1", productImage);
    }

    if ((!$("#ProductImageAlt2_Add").find("#previewZoneAddProductImageAlt2").hasClass("hidden"))) {
        imageElement = $("#boxBodyAddProductImageAlt2").children()[0];
        ImageDataBase64 = $(imageElement).attr('src');

        ImageMimeType = $("#boxBodyAddProductImageAlt2").children()[2].innerText;


        var baseformated = getBase64Image(imageElement);


        var productImage = base64ToBlob(baseformated, ImageMimeType);



        FormToSend.append("ProductImageAlt2", productImage);
    }


    console.log(FormToSend);


    FormToSend.append("ObjectData", JSON.stringify(ProductObj));

    AjaxPost(TableType.PRODUCT, FormToSend);
    
}
//#endregion

//#region ImageManipulation


function getBase64Image(img)
{
    var canvas = document.createElement("canvas");
    canvas.width = img.width;
    canvas.height = img.height;
    var ctx = canvas.getContext("2d");
    ctx.drawImage(img, 0, 0);
    var dataURL = canvas.toDataURL("image/png");
    return dataURL.replace(/^data:image\/(png|jpg);base64,/, "");
}


function base64ToBlob(base64, mime) {
    mime = mime || '';
    var sliceSize = 1024;
    var byteChars = window.atob(base64);
    var byteArrays = [];

    for (var offset = 0, len = byteChars.length; offset < len; offset += sliceSize) {
        var slice = byteChars.slice(offset, offset + sliceSize);

        var byteNumbers = new Array(slice.length);
        for (var i = 0; i < slice.length; i++) {
            byteNumbers[i] = slice.charCodeAt(i);
        }

        var byteArray = new Uint8Array(byteNumbers);

        byteArrays.push(byteArray);
    }

    return new Blob(byteArrays, { type: mime });
}
//#endregion






