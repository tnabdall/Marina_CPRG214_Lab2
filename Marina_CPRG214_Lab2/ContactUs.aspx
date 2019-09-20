<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="Marina_CPRG214_Lab2.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
    <h1 class="text-center text-primary mt-2">Contact Information</h1>
    <%-- Contact info for Marina in definition list --%>
    <div class="container border border-primary rounded mt-2">
        <dl>
            <dt>Address</dt>
            <dd>Inland Lake Marina</dd>
            <dd>Box 123</dd>
            <dd>Inland Lake, Arizona</dd>
            <dd>86038</dd>

            <dt>Phone Contact</dt>
            <dd>(office ph) 928-450-2234</dd>
            <dd>(leasing ph) 928-450-2235</dd>
            <dd>(fax) 928-450-2236</dd>

            <dt>People</dt>
            <dd>Manager: Glenn Cooke</dd>
            <dd>Slip Manager: Kimberley Carson</dd>
            <dd>Contact email: <a href="mailto: info@inlandmarina.com">info@inlandmarina.com</a></dd>
        </dl>
    </div>
</asp:Content>
