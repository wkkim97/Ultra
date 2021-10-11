<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Ultra_Admin.master" AutoEventWireup="true" CodeFile="RAD_Micro_Marketing.aspx.cs" Inherits="Pages_Admin_RAD_Micro_Marketing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .jsgrid-clicked-row{
            background-color : #ECECEC;
        }
        .jsgrid-clicked-row td{
            background-color : #ECECEC !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpHolderContent" Runat="Server">
    <div class="panel panel-topinfo">
        <div class="panel-heading">
            <div class="row row-sm">
                <table class="table-default">
                    <colgroup>
                        <col style="width: 50%" />
                        <col style="width: 50%" />
                    </colgroup>
                    <tr>
                        <td >
                            <div class="form_group">
                                <label>Manufacture</label>
                                <input class="form-control" type="text" id="txtManufacture" />
                            </div>

                        </td>
                        <td >
                            <div class="form_group">
                                <label>Segment</label>
                                <input class="form-control" type="text" id="txtSegment" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <div class="form_group">
                                <label>Product Family</label>
                                <input class="form-control" type="text" id="txtProductFamily" />
                            </div>

                        </td>
                        <td >
                            <div class="form_group">
                                <label>Product</label>
                                <input class="form-control" type="text" id="txtProduct" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <div class="form_group">
                                <label>Price</label>
                                <input class="form-control" type="text" id="txtPrice" oninput="this.value=numberWithCommas(this.value)"/>
                            </div>

                        </td>
                    </tr>
                    
                </table>                
                <div class="btnset">
                    <button type="button" class="btn btn-sm btn-gray fl" id="btnDelete"><i class="fa fa-trash-o"></i>Delete</button>
                    <button type="button" class="btn btn-sm btn-navy fl" style="margin-left: 3px;" id="btnNew"><i class="fa fa-close"></i>New</button>
                    <button type="button" class="btn btn-sm btn-red fr" id="btnSave"><i class="fa fa-floppy-o"></i>Save</button>
                </div>
            </div>
        </div>
    </div>
            
    <div class="panel panel-topinfo">
        <div class="panel-body">
            <div class="panel-heading">
                <h3 class="panel-title">RAD Micro Marketing</h3>
            </div>
            <div class="panel-body">
                <div class="box-panel">
                    <div id="jsGridMicroMarketing"></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpHolderBottom" Runat="Server">
    <script type="text/javascript" src="/ultra/Scripts/Pages/Admin/RAD_Micro_Marketing.js"></script>
</asp:Content>

