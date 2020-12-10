<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/Admin/MasterPageAdmin.Master" AutoEventWireup="true" CodeBehind="Page_CrudMaster.aspx.cs" Inherits="TheManOnTheMoon2.WebForms.Admin.Page_CrudMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Content/Admin/StylesAdminCustom.css" rel="stylesheet"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

         <h1 class="mt-4">C.R.U.D MasterPro</h1>
               
    
   <div class="accordion" id="accordionExample">
  <div class="card">
    <div class="card-header" id="card-header-Iventory">
      <h2 class="mb-0">
        <button class="btn btn-link" type="button" data-toggle="collapse" data-target="#collpaseInventory" aria-expanded="true" aria-controls="collapseOne">
          <h4 class="card-title"><i class="fas fa-warehouse fa-3x mr-1"></i>Inventory</h4>
        </button>
      </h2>
    </div>

    <div id="collpaseInventory" class="collapse show" aria-labelledby="headingOne" data-parent="#accordionExample">
      <div class="card-body">
                <div class="row">
                    <div class="col-xl-3 col-md-6 col-sm-3 col-6">
                        <a href="#" id="link_Inventory_Products" onclick="AjaxGetALL(this.id)" class="custom-card"> <div class="card bg-primary text-white mb-4 ">
                                <h5 class="card-title"><i class="fas fa-box fa-1x mr-1"></i>Products</h5>
                                    
                                    
                        </div></a>
                    </div>
                    <div class="col-xl-3 col-md-6 col-sm-3 col-6">
                        <a href="#" id="link_Inventory_Brands" onclick="AjaxGetALL(this.id)" class="custom-card"> <div class="card bg-primary text-white mb-4 ">
                                <h5 class="card-title"><i class="fas fa-tags fa-1x mr-1"></i>Brands</h5>
                                    
                                    
                        </div></a>
                    </div>
                    <div class="col-xl-3 col-md-6 col-sm-3 col-6">
                        <a href="#" id="link_Inventory_Categories" onclick="AjaxGetALL(this.id)" class="custom-card"> <div class="card bg-primary text-white mb-4 ">
                                <h5 class="card-title"><i class="fas fa-columns fa-1x mr-1"></i>Categories</h5>
                                    
                                    
                        </div></a>
                    </div>
                            
                </div>
          <div class="card mb-4">
                            <div class="card-header"  id="cardHeaderDataTableInventory">
                                <i class="fas fa-table mr-1"></i>
                                DataTable Example
                            </div>
                            <div class="card-body">
                                <div class="table-responsive">
                                    <button type="button" class="btn btn-primary btn-sm bg-success"><i class="fas fa-plus-circle"></i>Add</button>
<button type="button" class="btn btn-secondary btn-sm bg-danger" onclick="AjaxDeleteRecords()"><i class="fas fa-skull-crossbones"></i>Mass Delete</button>
                                    <table id="dataTableInventory" class="display" style="width:100%">
        <thead>
            <tr>
                <th></th>
                <th></th>
                <th></th>
                
               

                
            </tr>
        </thead>
                                        <tbody>

                                        </tbody>
        <tfoot>
            <tr>
                <th></th>
                
            </tr>
        </tfoot>
    </table>
                                    
                                </div>
                            </div>
                        </div>
        
      </div>
    </div>
  </div>
  <div class="card">
    <div class="card-header" id="headingTwo">
      <h2 class="mb-0">
        <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
          <h4 class="card-title"><i class="fas fa-user-cog fa-3x mr-1"></i>Administration</h4>
        </button>
      </h2>
    </div>
    <div id="collapseTwo" class="collapse" aria-labelledby="headingTwo" data-parent="#accordionExample">
      <div class="card-body">
        <div class="row">
                <div class="col-xl-3 col-md-6 col-sm-3 col-6">
                               <a href="#" id="link_Users_Users" onclick="AjaxGetALL(this.id)" class="custom-card"> <div class="card bg-primary text-white mb-4 ">
                                      <h5 class="card-title"><i class="fas fa-users fa-1x mr-1"></i>Users</h5>

                                </div></a>
                            </div>
            </div>
      </div>
    </div>
  </div>
  
</div>

<div class="modal fade" id="ModalAdminMessenger" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
<div class="modal-dialog" role="document">
<div class="modal-content">
    <div class="modal-header" id="ModalAdminMessengerHeader">
    <h5 class="modal-title" id="exampleModalLabel">Success</h5>
    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
        <span aria-hidden="true">&times;</span>
    </button>
    </div>
    <div class="modal-body" id="ModalAdminMessengerBody">
       
    </div>
    <div class="modal-footer">
    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
    <button type="button" class="btn btn-primary">Save changes</button>
    </div>
</div>
</div>
</div>
    <!-- Button trigger modal -->
<button type="button" class="btn btn-primary" data-toggle="modal" data-target="#Modal_Edit_Brand">
  Launch demo modal
</button>
   <!--Modal_Edit_Products-->
  <div class="modal fade" id="Modal_Edit_Brand" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered" role="document">
        
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalLongTitle">Edit Brand</h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
         <div class="container" style="background-color: orange; content:'f02b'; font-family:'FontAwesome'">
             <h2><i class="fas fa-tag fa-2x"></i>Brand</h2>
          <div class="row justify-content-center" style="background-color:purple;">
                
              <div class="col-lg-6 col-md-6 bg-info">
                 
                  <form>
                 
                 </form>
                  <form id="form_brand_name">
                                           <div class="form-group">
                     <i class="fas fa-hashtag"></i>
                    <label class="small mb-1" for="Id ">Id</label>
                    <input class="form-control py-4" readonly id="Id" name="Id" value="" type="number" />
                 </div>
                 <div class="form-group">
                     <i class="fas fa-tag"></i>
                    <label class="small mb-1" for="Name">Name</label>
                    <input class="form-control py-4" id="Name" name="Name" value="" type="text" />
                 </div>
                                            
                                        </form>
                  
              </div>
              

        </div>

         </div>
            
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
            <a class="btn btn-primary" id="ButtonPostBrand" onclick="TestData($('#form_brand_name').serializeToJSON())">Save</a>
          
        </div>
      </div>
          
    </div>
  </div>
    

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FooterResources" runat="server">
        <script src="../../Scripts/Admin/datatables-Crud.js"></script>
     <script src="../../Scripts/Admin/Crud.js"></script>
   <%-- <link rel="stylesheet" href="../../Content/mdb/mdb.min.css" />
    <script src="../../Scripts/mdb/mdb.min.js"></script>--%>
</asp:Content>