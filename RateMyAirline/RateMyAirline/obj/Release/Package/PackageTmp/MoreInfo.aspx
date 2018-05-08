<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MoreInfo.aspx.cs" Inherits="RateMyAirline.MoreInfo" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<h1>More Info</h1>
    <div style="text-align: center">
       <asp:Label ID="lblAirport" runat="server" Text=" "></asp:Label>

       <table style="margin: 0px auto;">
 
       <tr><td>Customer Service:</td>
       <td><asp:DropDownList ID ="cboCustomerService" runat="server"></asp:DropDownList></td></tr>
       <tr><td> Wait Time:</td>
       <td><asp:DropDownList ID ="cboWaitTime" runat="server"></asp:DropDownList></td></tr>
       <tr><td>Cleanliness:</td>
       <td><asp:DropDownList ID ="cboCleanliness" runat="server"></asp:DropDownList></td></tr>
       <tr><td>Amenities:</td>
       <td><asp:DropDownList ID ="cboAmenities" runat="server"></asp:DropDownList></td></tr>
       <tr><td>Parking:</td>
       <td><asp:DropDownList ID ="cboParking" runat="server"></asp:DropDownList></td></tr>
       <tr><td>Comments</td></tr>
       <tr><td><asp:TextBox ID="txtComments" runat="server"></asp:TextBox></td>
        
       </table>
    </div>
    
    <div style="text-align:center">
        <asp:Button ID="cmdSubmit" runat="server"   Text="Submit Review" OnClick="cmdSubmitReview_Click" />
    </div>
 
</asp:Content>
