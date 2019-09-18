<%@ Control Language="C#" CodeBehind="Default.ascx.cs" Inherits="Marina_CPRG214_Lab2.DefaultEntityTemplate" %>

<asp:EntityTemplate runat="server" ID="EntityTemplate1">
    <ItemTemplate>
         <tr class="td">
             <td class="DDLightHeader">
                 <asp:Label runat="server" OnInit="Label_Init" />
             </td>
             <td>
                 <asp:DynamicControl runat="server" OnInit="DynamicControl_Init" />
             </td>
         </tr>
     </ItemTemplate>
</asp:EntityTemplate>

