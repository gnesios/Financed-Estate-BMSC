﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WPEstatePresenterUserControl.ascx.cs" Inherits="BMSCFinancedEstate.WPEstatePresenter.WPEstatePresenterUserControl" %>

<link rel="stylesheet" type="text/css" href="/_catalogs/masterpage/bmsc/financedestate/styles.css" />

<div class="filter_options">
    <asp:DropDownList runat="server" ID="ddlCity" AppendDataBoundItems="true"
        AutoPostBack="true" OnSelectedIndexChanged="rbl_SelectedIndexChanged">
        <asp:ListItem Selected="True" Value="">(SELECCIONE LA CIUDAD DEL INMUEBLE)</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList runat="server" ID="ddlType" AppendDataBoundItems="true"
        AutoPostBack="true" OnSelectedIndexChanged="rbl_SelectedIndexChanged">
        <asp:ListItem Selected="True" Value="">(SELECCIONE EL TIPO DE INMUEBLE)</asp:ListItem>
    </asp:DropDownList>
    <%--<asp:RadioButtonList runat="server" ID="rblCity" AppendDataBoundItems="true"
        RepeatDirection="Horizontal" RepeatLayout="Flow"
        AutoPostBack="true" OnSelectedIndexChanged="rbl_SelectedIndexChanged"></asp:RadioButtonList>
    <asp:RadioButtonList runat="server" Id="rblType" AppendDataBoundItems="true"
        RepeatDirection="Horizontal" RepeatLayout="Flow"
        AutoPostBack="true" OnSelectedIndexChanged="rbl_SelectedIndexChanged"></asp:RadioButtonList>--%>
    <asp:RadioButtonList runat="server" ID="rblPrice" AppendDataBoundItems="true"
        RepeatDirection="Horizontal" RepeatLayout="Flow" Visible="false"
        AutoPostBack="true" OnSelectedIndexChanged="rbl_SelectedIndexChanged" ></asp:RadioButtonList>
</div>

<div class="contenedor-inmuebles" style="position: relative;">
    <asp:Label runat="server" ID="lblHeader" Text="<h3>Los m&aacute;s destacados<h3>"></asp:Label>
    <asp:Literal runat="server" ID="ltrEstate"></asp:Literal>
</div>

