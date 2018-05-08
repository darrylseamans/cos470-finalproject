<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ratings.aspx.cs" Inherits="RateMyAirline.Ratings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div style="text-align: center">
        <asp:Label ID="lblAirport" runat="server" Text=" "></asp:Label>
        <br />
        <asp:label ID="lblRatings" runat="server" Text=""></asp:label>
        <p />

         <asp:Button ID="cmdFindAirports" runat="server" OnClick="cmdFindAirports_Click" Text="Back" />
         <p />
    </div>
</asp:Content>
