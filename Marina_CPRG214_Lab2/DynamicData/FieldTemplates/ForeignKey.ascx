<%@ Control Language="C#" CodeBehind="ForeignKey.ascx.cs" Inherits="Marina_CPRG214_Lab2.ForeignKeyField" %>

<asp:HyperLink ID="HyperLink1" runat="server"
    Text="<%# GetDisplayString() %>"
    NavigateUrl="<%# GetNavigateUrl() %>"  />

