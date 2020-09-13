<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="Othello._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <head>

    
    <link rel="stylesheet" href="css/styleGame.css" />
    <style>
        
    </style>
    </head>
    <div id="Div_Head" style="height: 800px; width: 990px" class="fondo">
        <div style="height: 110px; width: 987px">
            <asp:Panel ID="Panel2" runat="server" OnClick="btnInicio_Click"></asp:Panel>
            <asp:FileUpload ID="FileUpload1" runat="server"  class="botonaccion"/>
            <br />
            <asp:Button ID="Button1" runat="server" Text="Cargar Partida" OnClick="Button1_Click" class="botonaccion"/>
            <asp:Button ID="Button2" runat="server" Text="Guardar Partida" OnClick="Button2_Click1" class="botonaccion" />
        </div>
         <div id="Div_Tablero" style="height: 400px; width: 400px;">
             <asp:Panel ID="Panel1" runat="server" OnClick="btnInicio_Click"></asp:Panel>
         </div>
        <div>
            
        </div>
    </div>


</asp:Content>
