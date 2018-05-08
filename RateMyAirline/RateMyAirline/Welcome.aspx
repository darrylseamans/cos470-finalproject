<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Welcome.aspx.cs" Inherits="RateMyAirline.Welcome" %>
 
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

         <table style="margin: 0px auto;">
         <tr><td>Latitude:</td><td><asp:TextBox ID="txtLat" runat="server" Text="43.6591"></asp:TextBox></td></tr>
         <tr><td>Longitude:</td><td><asp:TextBox ID="txtLong" runat="server" Text="-70.2568"></asp:TextBox></td></tr>
         <tr><td>Degrees close by:</td><td><asp:TextBox ID="txtCloseBy" runat="server" Text="2.0"></asp:TextBox></td></tr>
         </table>

    <div style="text-align:center">
         <asp:Button ID="cmdFindAirports" runat="server" OnClick="cmdFindAirports_Click" Text="Find" />
         <p />
         <asp:Label ID="lblResults" runat="server"></asp:Label>
         </div>
 
</asp:Content>
