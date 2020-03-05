<%@ Assembly Name="BMSCFinancedEstate, Version=1.0.0.0, Culture=neutral, PublicKeyToken=fc3674c5b8650aff" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WPPropertyDetailsUserControl.ascx.cs" Inherits="BMSCFinancedEstate.WPPropertyDetails.WPPropertyDetailsUserControl" %>

<link rel="stylesheet" type="text/css" href="/_catalogs/masterpage/bmsc/financedestate/styles.css" />
<script src="https://maps.googleapis.com/maps/api/js?v=3.exp&signed_in=false&callback=initialize"></script>

<div class="contenedor-inmuebles">
    <div class="col2-1">
        <asp:Literal runat="server" ID="ltrImage"></asp:Literal>
    </div>
    <div class="col2-2">
        <asp:Literal runat="server" ID="ltrDetail"></asp:Literal>
    </div>
</div>
