<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="Othello._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <html>
    <head>
        <title></title>
        <link rel="stylesheet" href="css/styleGame.css" />
        <meta http-equiv="Expires" content="0">
        <meta http-equiv="Last-Modified" content="0">
        <meta http-equiv="Cache-Control" content="no-cache, mustrevalidate">
        <meta http-equiv="Pragma" content="no-cache">
    </head>
    <body>
        
        <div class="contenedor">
            <div class="head_Marcador">
                <div class="">
                    <h1> Player 1</h1>
                   </div>
                <div> vs </div>
                <div class="float-right">
                    <h1>Player 2</h1>
                </div>

                </div>
            <div class="lateral-zone"></div>
            <div class="tablero_Completo">
                <div class="tablero_Centro"><asp:Panel ID="Panel1" runat="server" class="tablero_Centro"> </asp:Panel></div> 
                
            </div>
                <asp:Button ID="PartidaPVP" runat="server" Text="P1 vs P2" OnClick="Button1_Click" /><asp:Button ID="Partida_CPU" runat="server" Text="P1 vs CPU" />
                
        
        
            <div class="feet_Pagina">
                <asp:Panel ID="Panel2" runat="server" ></asp:Panel>
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <br />
                <asp:Button ID="LeerXML" runat="server" Text="Cargar Partida" OnClick="LeerXML_Click"/>
                <asp:Button ID="CrearXML" runat="server" Text="Guardar Partida" OnClick="CrearXML_Click"/>
            </div>
     </div>
    </body>
    </html>
</asp:Content>
