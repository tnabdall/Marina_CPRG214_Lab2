<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="Marina_CPRG214_Lab2.WebForm4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
    <%-- Holds all login/registration form controls --%>
    <div class="container">
        <%-- First name --%>
        <div class="form-group">
            <asp:Label ID="firstNameLabel" CssClass="text-primary" runat="server" Text="First Name" Visible="false"></asp:Label>
            <asp:TextBox ID="firstNameTextBox" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>
        </div>
        <%-- Last name --%>
        <div class="form-group">
            <asp:Label ID="lastNameLabel" CssClass="text-primary" runat="server" Text="Last Name" Visible="false"></asp:Label>
            <asp:TextBox ID="lastNameTextBox" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>
        </div>
        <%-- Phone # --%>
        <div class="form-group">
            <asp:Label ID="phoneLabel" CssClass="text-primary" runat="server" Text="Phone" Visible="false"></asp:Label>
            <asp:TextBox ID="phoneTextBox" CssClass="form-control" runat="server" TextMode="Phone" Visible="false"></asp:TextBox>
        </div>
        <%-- City --%>
        <div class="form-group">
            <asp:Label ID="cityLabel" CssClass="text-primary" runat="server" Text="City" Visible="false"></asp:Label>
            <asp:TextBox ID="cityTextBox" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>
        </div>
        <%-- Username --%>
        <div class="form-group">
            <asp:Label ID="usernameLabel" CssClass="text-primary" runat="server" Text="Username" Visible="true"></asp:Label>
            <asp:TextBox ID="usernameTextBox" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <%-- Password --%>
        <div class="form-group">
            <asp:Label ID="passwordLabel" CssClass="text-primary" runat="server" Text="Password" Visible="true"></asp:Label>
            <asp:TextBox ID="passwordTextBox" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
        </div>
        <%-- Submit, cancel, and register buttons --%>
        <asp:Button ID="submitButton" CssClass="btn btn-dark " runat="server" Text="Submit" OnClick="submitButton_Click" />
        <asp:Button ID="cancelButton" CssClass="btn btn-warning" runat="server" Text="Cancel" Visible="false" CausesValidation="False" OnClick="cancelButton_Click" />
        <asp:LinkButton ID="registerButton" runat="server" OnClick="registerButton_Click" CausesValidation="False">Register</asp:LinkButton>
         <%-- Holds all validators. Displays messages under form. --%>
        <div class="container mt-2">
             <%-- Required field validators --%>
            <asp:RequiredFieldValidator ID="firstNameRequiredValidator" CssClass="text-danger" runat="server" ErrorMessage="First Name required. " Enabled="False" Display="Dynamic" ControlToValidate="firstNameTextBox"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="lastNameRequiredValidator" CssClass="text-danger" runat="server" ErrorMessage="Last Name required. " Enabled="False" Display="Dynamic" ControlToValidate="lastNameTextBox"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="phoneRequiredValidator" CssClass="text-danger" runat="server" ErrorMessage="Phone required. " Enabled="False" Display="Dynamic" ControlToValidate="phoneTextBox"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="cityRequiredValidator" CssClass="text-danger" runat="server" ErrorMessage="City required. " Enabled="False" Display="Dynamic" ControlToValidate="cityTextBox"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="usernameRequiredValidator" CssClass="text-danger" runat="server" ErrorMessage="Username required. " Enabled="true" Display="Dynamic" ControlToValidate="usernameTextBox"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="passwordRequiredValidator" CssClass="text-danger" runat="server" ErrorMessage="Password required. " Enabled="true" Display="Dynamic" ControlToValidate="passwordTextBox"></asp:RequiredFieldValidator>
             <%-- Phone regex validator --%>
            <asp:RegularExpressionValidator ID="phoneRegularExpressionValidator" CssClass="text-danger" runat="server" ErrorMessage="Phone must follow format: 111-111-1111" ValidationExpression="^[0-9]{3}-[0-9]{3}-[0-9]{4}$" ControlToValidate="phoneTextBox"></asp:RegularExpressionValidator>
            <br />
            <%-- Warning label for when failed to login --%>
            <asp:Label ID="failedLabel" CssClass="text-danger" runat="server" Text="Unable to register customer. Username already exists." Visible="False"></asp:Label>
        </div>
    </div>
</asp:Content>
