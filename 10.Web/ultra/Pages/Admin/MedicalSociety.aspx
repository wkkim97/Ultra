<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Ultra_Admin.master" AutoEventWireup="true" CodeFile="MedicalSociety.aspx.cs" Inherits="Pages_Admin_MedicalSociety" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpHolderContent" Runat="Server">
    <div class="panel panel-topinfo">
        <div class="panel-heading">
            <div class="row row-sm">
                <table class="table-default">
                    <colgroup>
                        <col />
                        <col style="width: 65px" />
                    </colgroup>
                    <tr>
                        <th class="text-left">Society Name</th>
                        <th class="text-left">Status</th>
                    </tr>
                    <tr>
                        <td><input type="text" id="txtSocietyName" class="form-control" /></td>
                        <td>
                            <select id="selSocietysStatus" class="form-control">
                                <option>Y</option>
                                <option>N</option>
                            </select>
                        </td>
                    </tr>
                </table>
                <input type="hidden" id="hhdSocietyID" />
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
                <h3 class="panel-title">Medical Society</h3>
            </div>
            <div class="panel-body">
                <div class="box-panel">
                    <div id="jsGridMedicalSociety"></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpHolderBottom" Runat="Server">
    <script type="text/javascript" src="/ultra/Scripts/Pages/Admin/MedicalSociety.js"></script>
</asp:Content>

