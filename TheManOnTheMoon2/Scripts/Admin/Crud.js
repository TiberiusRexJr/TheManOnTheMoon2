//#region DocumentOnREady
$(document).ready(function ()
{
    AjaxGetALL("link_Inventory_Brands");

  
    
});
//#endregion

//#region AjaxCRUD 
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
    console.log(tableTypeSelected);

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
            console.log(idsToDelete);
           
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
        data: idsToDelete,
        url: sendToAdress,
        success: function (response, jqXHR, data) {

            ModalMessenger(idsToDelete, true, Delete, "Deleted Successfully");
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
    console.log(RowData.Id);

    $("#form_brand_name").find("#Id").val(RowData.Id);
    $("#form_brand_name").find("#Name").val(RowData.Name);
    $("#Modal_Edit_Brand").modal("show");



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
    //todo Datasets

    var dataSet = [
        ["Tiger Nixon", "System Architect", "Edinburgh", "5421", "2011/04/25", "$320,800"],
        ["Garrett Winters", "Accountant", "Tokyo", "8422", "2011/07/25", "$170,750"],
        ["Ashton Cox", "Junior Technical Author", "San Francisco", "1562", "2009/01/12", "$86,000"],
        ["Cedric Kelly", "Senior Javascript Developer", "Edinburgh", "6224", "2012/03/29", "$433,060"],
        ["Airi Satou", "Accountant", "Tokyo", "5407", "2008/11/28", "$162,700"],
        ["Brielle Williamson", "Integration Specialist", "New York", "4804", "2012/12/02", "$372,000"],
        ["Herrod Chandler", "Sales Assistant", "San Francisco", "9608", "2012/08/06", "$137,500"],
        ["Rhona Davidson", "Integration Specialist", "Tokyo", "6200", "2010/10/14", "$327,900"],
        ["Colleen Hurst", "Javascript Developer", "San Francisco", "2360", "2009/09/15", "$205,500"],
        ["Sonya Frost", "Software Engineer", "Edinburgh", "1667", "2008/12/13", "$103,600"],
        ["Jena Gaines", "Office Manager", "London", "3814", "2008/12/19", "$90,560"],
        ["Quinn Flynn", "Support Lead", "Edinburgh", "9497", "2013/03/03", "$342,000"],
        ["Charde Marshall", "Regional Director", "San Francisco", "6741", "2008/10/16", "$470,600"],
        ["Haley Kennedy", "Senior Marketing Designer", "London", "3597", "2012/12/18", "$313,500"],
        ["Tatyana Fitzpatrick", "Regional Director", "London", "1965", "2010/03/17", "$385,750"],
        ["Michael Silva", "Marketing Designer", "London", "1581", "2012/11/27", "$198,500"],
        ["Paul Byrd", "Chief Financial Officer (CFO)", "New York", "3059", "2010/06/09", "$725,000"],
        ["Gloria Little", "Systems Administrator", "New York", "1721", "2009/04/10", "$237,500"],
        ["Bradley Greer", "Software Engineer", "London", "2558", "2012/10/13", "$132,000"],
        ["Dai Rios", "Personnel Lead", "Edinburgh", "2290", "2012/09/26", "$217,500"],
        ["Jenette Caldwell", "Development Lead", "New York", "1937", "2011/09/03", "$345,000"],
        ["Yuri Berry", "Chief Marketing Officer (CMO)", "New York", "6154", "2009/06/25", "$675,000"],
        ["Caesar Vance", "Pre-Sales Support", "New York", "8330", "2011/12/12", "$106,450"],
        ["Doris Wilder", "Sales Assistant", "Sydney", "3023", "2010/09/20", "$85,600"],
        ["Angelica Ramos", "Chief Executive Officer (CEO)", "London", "5797", "2009/10/09", "$1,200,000"],
        ["Gavin Joyce", "Developer", "Edinburgh", "8822", "2010/12/22", "$92,575"],
        ["Jennifer Chang", "Regional Director", "Singapore", "9239", "2010/11/14", "$357,650"],
        ["Brenden Wagner", "Software Engineer", "San Francisco", "1314", "2011/06/07", "$206,850"],
        ["Fiona Green", "Chief Operating Officer (COO)", "San Francisco", "2947", "2010/03/11", "$850,000"],
        ["Shou Itou", "Regional Marketing", "Tokyo", "8899", "2011/08/14", "$163,000"],
        ["Michelle House", "Integration Specialist", "Sydney", "2769", "2011/06/02", "$95,400"],
        ["Suki Burks", "Developer", "London", "6832", "2009/10/22", "$114,500"],
        ["Prescott Bartlett", "Technical Author", "London", "3606", "2011/05/07", "$145,000"],
        ["Gavin Cortez", "Team Leader", "San Francisco", "2860", "2008/10/26", "$235,500"],
        ["Martena Mccray", "Post-Sales support", "Edinburgh", "8240", "2011/03/09", "$324,050"],
        ["Unity Butler", "Marketing Designer", "San Francisco", "5384", "2009/12/09", "$85,675"]
    ];
    var aDemoItems = [
        {
            "patientId": 1,
            "otherId": "LanTest101",
            "firstName": "x1",
            "lastName": "yLanTest101",
            "gender": "M",
            "dob": "10/16/1941",
            "race": "Caucasian/White"
        },

        {
            "patientId": 2,
            "otherId": "LanTest102",
            "firstName": "x2",
            "lastName": "yLanTest102",
            "gender": "M",
            "dob": "08/10/2005",
            "race": "Caucasian/White"
        },

        {
            "patientId": 3,
            "otherId": "Test1111",
            "firstName": "x3",
            "lastName": "yTest1111",
            "gender": "M",
            "dob": "08/13/2015",
            "race": "Native Hawaian/Pacific Islander"
        },
    ];

    //todo Individual Objects
    var indiviaulObj = data[0];
   
    //todo Keys
    var keys = Object.keys(indiviaulObj);
   
    //todo columnArrays
    var columnsArray = new Array(keys.length);


    $.each(keys, function (index, key) {
        columnsArray[index] = { data: key, title: key };
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

};

function GetSelectedRows(table) {
    

    
}
//#endregion

//#region Test & Expirimentals
function TestData(data)
{
    console.log(data);
    console.log(data.Name);
   
}
//#endregion






