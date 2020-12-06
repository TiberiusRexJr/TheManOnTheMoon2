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
                        <a href="#" id="link_Inventory_Products" onclick="InventoryDispalyDataCrud(this.id)" class="custom-card"> <div class="card bg-primary text-white mb-4 ">
                                <h5 class="card-title"><i class="fas fa-box fa-1x mr-1"></i>Products</h5>
                                    
                                    
                        </div></a>
                    </div>
                    <div class="col-xl-3 col-md-6 col-sm-3 col-6">
                        <a href="#" id="link_Inventory_Brands" onclick="InventoryDispalyDataCrud(this.id)" class="custom-card"> <div class="card bg-primary text-white mb-4 ">
                                <h5 class="card-title"><i class="fas fa-tags fa-1x mr-1"></i>Brands</h5>
                                    
                                    
                        </div></a>
                    </div>
                    <div class="col-xl-3 col-md-6 col-sm-3 col-6">
                        <a href="#" id="link_Inventory_Categories" onclick="InventoryDispalyDataCrud(this.id)" class="custom-card"> <div class="card bg-primary text-white mb-4 ">
                                <h5 class="card-title"><i class="fas fa-columns fa-1x mr-1"></i>Categories</h5>
                                    
                                    
                        </div></a>
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
                               <a href="#" id="link_Users_Users" onclick="InventoryDispalyDataCrud(this.id)" class="custom-card"> <div class="card bg-primary text-white mb-4 ">
                                      <h5 class="card-title"><i class="fas fa-users fa-1x mr-1"></i>Users</h5>

                                </div></a>
                            </div>
            </div>
      </div>
    </div>
  </div>
  
</div>

    
  <script src="../../Scripts/Admin/Crud.js"></script>       
</asp:Content>
