﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/Admin/MasterPageAdmin.Master" AutoEventWireup="true" CodeBehind="Page_CrudMaster.aspx.cs" Inherits="TheManOnTheMoon2.WebForms.Admin.Page_CrudMaster" %>
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

    <div id="collpaseInventory" class="collapse " aria-labelledby="headingOne" data-parent="#accordionExample">
      <div class="card-body">
                <div class="row">
                    <div class="col-xl-3 col-md-6 col-sm-3 col-6">
                        <a href="#"  id="link_Inventory_Products" onclick="AjaxGetALL(this.id)" class="custom-card"> <div class="card bg-primary text-white mb-4 ">
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
                                    <div class="dropdown">
  <button class="btn btn-primary btn-sm dropdown-toggle" style="display:inline-block;" type="button" id="dropdownMenu2" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
  <i class="fas fa-plus-circle"></i>Add
  </button>
  <div class="dropdown-menu" aria-labelledby="dropdownMenu2" >
    <button class="dropdown-item" id="ButtonAddProduct" type="button"><i class="fas fa-box"></i>Product</button>
    <button class="dropdown-item" id="ButtonAddBrand" type="button"><i class="fas fa-tag"></i>Brand</button>
    <button class="dropdown-item" id="ButtonAddCategory" type="button"><i class="fas fa-list"></i>Category</button>
  </div>
</div>
<button type="button" style="display:inline-block;" class="btn btn-secondary btn-sm bg-danger" onclick="AjaxDeleteRecords()"><i class="fas fa-skull-crossbones"></i>Mass Delete</button>
                                    <table id="dataTableInventory" class="display" style="width:100%">
        <thead>
            <tr id="dataTableInventoryThead_Tr">
                
                
               

                
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
<button type="button" class="btn btn-primary" data-toggle="modal" data-target="#Modal_Add_Product">
  Launch demo modal
</button>
   <!--Modal_Edit_Brand-->
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
            <a class="btn btn-primary" id="ButtonEditBrandSubmit" onclick="TestData($('#form_brand_name').serializeToJSON())">Save</a>
          
        </div>
      </div>
          
    </div>
  </div>
    <!--Modal_Edit_Brand-->
    
    <!--Modal_Edit_Cateogry-->
  <div class="modal fade" id="Modal_Edit_Category" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered" role="document">
        
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="modalTitleEditCategory">Edit a Current Brand</h5>
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
            <a class="btn btn-primary" id="ButtonEditBrandSubmit" onclick="TestData($('#form_brand_name').serializeToJSON())">Save</a>
          
        </div>
      </div>
          
    </div>
  </div>

    <!--Modal_Edit_Cateogry-->




<!--Modal_Add_Products-->
    
    <div class="modal fade" id="Modal_Add_Product" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered" role="document">
        
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="modalTitleAddProduct"><i class="fas fa-box mr-1"></i>New Product</h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
         <div class="container">
            
                 
              <form id="form_product_add">

    <div class="accordion" id="accordionAddProduct">
  
     <div class="card">
 
       <div class="card-header" id="cardHeaderDescription">
           <a class="btn btn-block collapsed" href="#" type="button" id="alinkButtonProductDescription" data-toggle="collapse" data-target="#collapseProductDescription">
         <h2 class="mb-0">
          <i class="fas fa-list-alt"></i> Description
         </h2>
               </a>
       </div>
 
       
       <div id="collapseProductDescription" class="collapse " aria-labelledby="headingOne" data-parent="#accordionAddProduct">
         <div class="card-body">
                 <div class="col-lg-6 col-md-6 ">
                 <div class="form-group d-none">
                     <i class="fas fa-user"></i>
                    <label class="small mb-1" for="product_id ">Product_Id</label>
                    <input class="form-control py-4" name="Id" value="" id="product_id " type="number" />
                 </div>
                <div class="form-group">
                     <i class="fas fa-user"></i>
                    <label class="small mb-1" for="product_name ">Name</label>
                    <input class="form-control py-4" value="" name="Name" id="product_name " type="text" />
                 </div>
                 <div class="form-group ">
                     <i class="fas fa-user"></i>
                    <label class="small mb-1" for="product_description ">Description</label>
                    <input class="form-control py-4" id="product_description " name="Description" value="" type="text" />
                 </div>
                <div class="form-group">
                     <i class="fas fa-user"></i>
                    <label class="small mb-1" for="product_upc ">UPC</label>
                    <input class="form-control py-4" name="Upc" value="" id="product_upc " type="text" />
                 </div>
                 <div class="form-group">
                     <i class="fas fa-user"></i>
                    <label class="small mb-1" for="product_brand ">Brand</label>
                    <select name="Brand" class="form-control selectpicker">
                  <option value="">Select your Department/Office</option>
                  <option value="">Department of Engineering</option>
                  <option value="">Department of Agriculture</option>
                  <option value="" >Tourism Office</option>
                    </select>
                 </div>
            </div>
         </div>
       </div>
     
     </div>
     <div class="card">
     
           <div class="card-header" id="cardHeadingCategory">
               <a class="btn btn-block collapsed" href="#" type="button" id="alinkButtonProductCategory" data-toggle="collapse" data-target="#collapseProductCategory">
             <h2 class="mb-0">
              <i class="fas fa-list-alt"></i> Categories
             </h2>
 </a>
           </div>
     
           
           <div id="collapseProductCategory" class="collapse " aria-labelledby="headingOne" data-parent="#accordionAddProduct">
             <div class="card-body">
                <div class="col-lg-6 col-md-6 ">
               <div class="form-group">
                     <i class="fas fa-user"></i>
                    <label class="small mb-1" for="product_category_select ">Category</label>
                          <select name="Category" class="form-control selectpicker">
                  <option value="">Select your Depatment/Office</option>
                  <option value="">Department of Engineering</option>
                  <option value="">Department of Agriculture</option>
                  <option value="" >Tourism Office</option>
                    </select>
                 </div>
                  <div class="form-group">
                     <i class="fas fa-user"></i>
                    <label class="small mb-1" for="product_sub_category ">Sub-Category</label>
                      <select name="Sub_Category" class="form-control selectpicker">
                  <option value="">Select your Department/Office</option>
                  <option value="">Department of Engineering</option>
                  <option value="">Department of Agriculture</option>
                  <option value="" >Tourism Office</option>
                    </select>
                 </div>
                  <div class="form-group">
                     <i class="fas fa-user"></i>
                    <label class="small mb-1" for="product_type ">Type</label>
                      <select name="Type" class="form-control selectpicker">
                  <option value="">Select your Department/Office</option>
                  <option value="">Department of Engineering</option>
                  <option value="">Department of Agriculture</option>
                  <option value="" >Tourism Office</option>
                    </select>
                 </div>
                  <div class="form-group">
                     <i class="fas fa-user"></i>
                    <label class="small mb-1" for="product_sub_type ">Sub-Type</label>
                      <select name="Sub_Type" class="form-control selectpicker">
                  <option value="">Select your Department/Office</option>
                  <option value="">Department of Engineering</option>
                  <option value="">Department of Agriculture</option>
                  <option value="" >Tourism Office</option>
                    </select>
                 </div>
           
        </div>
             </div>
           </div>
         </div>
   
       <div class="card">
     
         <div class="card-header" id="cardHeadingFinancial">
             <a class="btn btn-block collapsed" type="button" id="alinkButtonProductFinancial" data-toggle="collapse" data-target="#collapseProductFinancial">
           <h2 class="mb-0">
             <i class="fas fa-hand-holding-usd"></i>Financial
           </h2>
             </a>
         </div>
   
         
         <div id="collapseProductFinancial" class="collapse " aria-labelledby="headingOne" data-parent="#accordionAddProduct">
           <div class="card-body">
                <div class="col-lg-6 col-md-6 bg-light">
                 
                 <div class="form-group">
                     <i class="fas fa-user"></i>
                    <label class="small mb-1" for="product_purchase_cost ">Purchase Cost</label>
                    <input class="form-control py-4" id="product_purchase_cost" name="Cost" value="" type="number" />
                 </div>
                  <div class="form-group">
                     <i class="fas fa-user"></i>
                    <label class="small mb-1" for="product_retail ">Retail</label>
                    <input class="form-control py-4" id="product_retail " name="Retail_Price" value="" type="number" />
                 </div>
                  <div class="form-group">
                     <i class="fas fa-user"></i>
                    <label class="small mb-1" for="product_sale_price ">On Sale Price</label>
                    <input class="form-control py-4" id="product_sale_price " name="Sale_Price" value="" type="number" />
                 </div>
                  <div class="form-group">
                     <i class="fas fa-user"></i>
                    <label class="small mb-1" for="product_sale_status ">On Sale Status</label>
                    <label class="radio-inline d-block"><input type="radio" name="On_Sale_Status" value="True" checked>Yes</label>
<label class="radio-inline"><input type="radio" name="On_Sale_Status" value="False">No</label>
                 </div>
                  <div class="form-group">
                     <i class="fas fa-user"></i>
                    <label class="small mb-1" for="product_stock_quantity ">Stock Quantity</label>
                    <input class="form-control py-4" id="product_stock_quantity " name="Stock_Quantity" value="" type="number" />
                 </div>
              </div>
           </div>
         </div>
       </div>
      
       
         <div class="card">
     
           <div class="card-header" id="cardHeadingPictures">
               <a class="btn btn-block collapsed" href="#" type="button" id="alinkButtonProductImages" data-toggle="collapse" data-target="#collapseProductImages">
             <h2 class="mb-0">
              <i class="fas fa-images"></i> Pictures
             </h2>
 </a>
           </div>
     
           
           <div id="collapseProductImages" class="collapse " aria-labelledby="headingOne" data-parent="#accordionAddProduct">
             <div class="card-body">
                <div class="col-lg-6 col-md-6 bg-info">
               
            <!--Image_Upload_1-->
            <form>
                <div class="row">
                    <div class="col-lg-12 col-md-12">
                      <div class="form-group">
                        <label class="control-label">Upload File</label>
                        <div class="preview-zone hidden" id="preview-zone">
                          <div class="box box-solid">
                            <div class="box-header with-border">
                              <div><b>Image </b></div>
                              <div class="box-tools pull-right">
                                <button type="button" class="btn btn-danger btn-xs remove-preview">
                                 <i class="fas fa-sync-alt"></i> Reset 
                                </button>
                              </div>
                            </div>
                            <div class="box-body"></div>
                          </div>
                        </div>
                        <div class="dropzone-wrapper">
                          <div class="dropzone-desc">
                            <i class="fas fa-sync-alt"></i>
                            <p>Choose an image file or drag it here.</p>
                          </div>
                          <input type="file" name="img_logo" class="dropzone">
                        </div>
                      </div>
                </div>
             </div>
            </form>
            <!--Image_Upload_1--End-->
          <!--Image_Upload_2-->
            <form>
                <div class="row">
                    <div class="col-lg-12 col-md-12">
                      <div class="form-group">
                        <div class="preview-zone hidden">
                          <div class="box box-solid">
                            <div class="box-header with-border">
                              <div><b>Alternative Image 1</b></div>
                              <div class="box-tools pull-right">
                                <button type="button" class="btn btn-danger btn-xs remove-preview">
                                 <i class="fas fa-sync-alt"></i> Reset 
                                </button>
                              </div>
                            </div>
                            <div class="box-body"></div>
                          </div>
                        </div>
                        <div class="dropzone-wrapper">
                          <div class="dropzone-desc">
                            <i class="fas fa-sync-alt"></i>
                            <p>Choose an image file or drag it here.</p>
                          </div>
                          <input type="file" name="img_logo" class="dropzone">
                        </div>
                      </div>
                </div>
             </div>
            </form>
            <!--Image_Upload_2--End-->
             <!--Image_Upload_3-->
            <form>
                <div class="row">
                    <div class="col-lg-12 col-md-12">
                      <div class="form-group">
                       
                        <div class="preview-zone hidden">
                          <div class="box box-solid">
                            <div class="box-header with-border">
                              <div><b>Alternative Image 2</b></div>
                              <div class="box-tools pull-right">
                                <button type="button" class="btn btn-danger btn-xs remove-preview">
                                 <i class="fas fa-sync-alt"></i> Reset 
                                </button>
                              </div>
                            </div>
                            <div class="box-body"></div>
                          </div>
                        </div>
                        <div class="dropzone-wrapper">
                          <div class="dropzone-desc">
                            <i class="fas fa-sync-alt"></i>
                            <p>Choose an image file or drag it here.</p>
                          </div>
                          <input type="file" name="img_logo" class="dropzone">
                        </div>
                      </div>
                </div>
             </div>
            </form>
            <!--Image_Upload_3--End-->
        </div>
             </div>
           </div>
         </div>
       
       
           <div class="card">
                
             <div class="card-header" id="cardHeadingDimensions">
                   <a class="btn btn-block collapsed" href="#" type="button" id="alinkButtonProductDimensions" data-toggle="collapse" data-target="#collapseProductDimensions">
               <h2 class="mb-0">
               <i class="fas fa-ruler-combined"></i>  Dimensions
               </h2>
                       </a>
             </div>
       
             
             <div id="collapseProductDimensions" class="collapse " aria-labelledby="headingOne" data-parent="#accordionAddProduct">
               <div class="card-body">
                  <div class="col-lg-6 col-md-6 ">
                 
                 <div class="form-group">
                     <i class="fas fa-user"></i>
                    <label class="small mb-1" for="product_length ">Length</label>
                    <input class="form-control py-4" id="product_length " name="Length" value="" type="number" />
                 </div>
                  <div class="form-group">
                     <i class="fas fa-user"></i>
                    <label class="small mb-1" for="product_width ">Width</label>
                    <input class="form-control py-4" id="product_width " name="Width" value="" type="number" />
                 </div>
                  <div class="form-group">
                     <i class="fas fa-user"></i>
                    <label class="small mb-1" for="product_height ">Height</label>
                    <input class="form-control py-4" id="product_height " name="Height" value="" type="number" />
                 </div>
                  <div class="form-group">
                     <i class="fas fa-user"></i>
                    <label class="small mb-1" for="product_weight ">Weight</label>
                    <input class="form-control py-4" id="product_weight " name="Weight" value="" type="number" />
                 </div>
              </div>
               </div>
             </div>
           </div>
          
 </div>
    </form>
                  
             
              

        

         </div>
            
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
            <a class="btn btn-primary" id="ButtonPostProduct" onclick="TestData($('#form_product_add').serializeToJSON())">SubmitProduct</a>
          
        </div>
      </div>
          
    </div>
  </div>
            
<!--Modal_Add_Products-->

<!--Modal_Add_Brand--> 
  <div class="modal fade" id="Modal_Add_Brand" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered" role="document">
        
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="ModalTitleBrand"> <i class="fas fa-tag fa-2x"></i>Add Brand</h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
         
           
          
              <div class="col-lg-6 col-md-6 ">
                 
                  <form>
                 
                 </form>
                  <form id="formAddBrand">
                                           
                 <div class="form-group">
                     
                    <label class="col-form-label mb-1" for="Name">Brand Name</label>
                    <input class="form-control py-4" id="Name" name="Name" value="" type="text" />
                 </div>
                <div class="form-group">
                       <!--Image_Upload_1-->
            <form>
                <div class="row">
                    <div class="col-lg-12 col-md-12">
                      <div class="form-group">
                        <label class="control-label">Upload File</label>
                        <div class="preview-zone hidden">
                          <div class="box box-solid">
                            <div class="box-header with-border">
                              <div><b>Image </b></div>
                              <div class="box-tools pull-right">
                                <button type="button" class="btn btn-danger btn-xs remove-preview">
                                 <i class="fas fa-sync-alt"></i> Reset 
                                </button>
                              </div>
                            </div>
                            <div class="box-body"></div>
                          </div>
                        </div>
                        <div class="dropzone-wrapper">
                          <div class="dropzone-desc">
                            <i class="fas fa-sync-alt"></i>
                            <p>Choose an image file or drag it here.</p>
                          </div>
                          <input type="file" name="img_logo" class="dropzone">
                        </div>
                      </div>
                </div>
             </div>
            </form>
            <!--Image_Upload_1--End-->
                    </div>
                                            
                                        </form>
                  
              </div>
              

        </div>

         
            
        
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
            <a class="btn btn-primary" id="ButtonPostBrand" onclick="TestData(this.id,$('#formAddBrand').serializeToJSON())">Save</a>
          
        </div>
      </div>
          
    </div>
 </div>
<!--Modal_Add_Brand-->

<!--Modal_Add_Category-->
    <div class="modal fade" id="Modal_Add_Category" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered" role="document">
        
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="ModalTitleCategory"><i class="fas fa-list mr-1"></i>Add A Product Category</h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
         <div class="container">
             
          
              <div class="col-lg-6 col-md-6">
                 
                  <form>
                 
                 </form>
                  <form id="formAddCategory">
                                           
                 <div class="form-group">
                     <i class="fas fa-tag"></i>
                    <label class="col-form-label mb-1" for="Name">Name</label>
                    <input class="form-control py-4" id="Name" name="Name" value="" type="text" />
                 </div>
                <div class="form-group">
                       <!--Image_Upload_1-->
                        <form>
                            <div class="row">
                                <div class="col-lg-12 col-md-12">
                                  <div class="form-group">
                                    <label class="control-label">Upload File</label>
                                    <div class="preview-zone hidden">
                                      <div class="box box-solid">
                                        <div class="box-header with-border">
                                          <div><b>Image </b></div>
                                          <div class="box-tools pull-right">
                                            <button type="button" class="btn btn-danger btn-xs remove-preview">
                                             <i class="fas fa-sync-alt"></i> Reset 
                                            </button>
                                          </div>
                                        </div>
                                        <div class="box-body"></div>
                                      </div>
                                    </div>
                                    <div class="dropzone-wrapper">
                                      <div class="dropzone-desc">
                                        <i class="fas fa-sync-alt"></i>
                                        <p>Choose an image file or drag it here.</p>
                                      </div>
                                      <input type="file" name="img_logo" class="dropzone">
                                    </div>
                                  </div>
                            </div>
                         </div>
                        </form>
                        <!--Image_Upload_1--End-->
                </div>
                                            
                                        </form>
                
                  
              </div>
              

        </div>

         
            
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
            <a class="btn btn-primary" id="ButtonPostCategory" onclick="TestData($('#formAddCategory').serializeToJSON())">Save</a>
          
        </div>
      </div>
          
    </div>
  </div>
<!--Modal_Add_Category-->






</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FooterResources" runat="server">
        <script src="../../Scripts/Admin/datatables-Crud.js"></script>
     <script src="../../Scripts/Admin/Crud.js"></script>
    <link href="../../Content/Admin/Drag&Drop.css" rel="stylesheet" />
    <script src="../../Scripts/Admin/Drag&Drop.js"></script>
   <%-- <link rel="stylesheet" href="../../Content/mdb/mdb.min.css" />
    <script src="../../Scripts/mdb/mdb.min.js"></script>--%>
</asp:Content>