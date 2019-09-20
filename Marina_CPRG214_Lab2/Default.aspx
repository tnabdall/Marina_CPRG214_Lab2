<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Marina_CPRG214_Lab2.WebForm1" %>

<%@ Register Assembly="Microsoft.AspNet.EntityDataSource" Namespace="Microsoft.AspNet.EntityDataSource" TagPrefix="ef" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
    <h1 class="text-center text-primary mt-2">About us</h1>
    <div class="container-fluid align-content-center text-center d-inline-block">
        <div class="col-xs-5 d-inline-block">
        <asp:Image ID="fullMarinaImage" CssClass="img-fluid rounded d-inline-block" runat="server" ImageUrl="~/Images/fullMarina.jpg" />
            </div>
        <div id="homeParagraph" class="container border border-primary rounded mt-2 d-inline-block col-xs-7">

            <p class="text-dark">
                Welcome to Inland Marina located on the south shore Inland Lake, just a small commute from major centers in the south west.
            </p>
            <p class="text-dark">
                Inland Marina was established in the 1967 shortly after Inland Lake was created as a result of the South West damn. From its humble beginnings, it has grown to be the largest marina on Inland Lake. Due to the warm climate that extends year-round, Inland Lake has become a popular tourist destination in the south west. Boat owners from California, Arizona, Nevada, and Utah are attracted to the area. Inland Marina has 90 slips ranging in size from 16 to 32 feet in length. Dock services include electrical and fresh water.
            </p>
        </div>
    </div>
</asp:Content>
