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

            <div class="head_Marcador ">
                <div class="row">
                    <div class="col-4">
                        <h1><asp:Label ID="lblJugador1" runat="server" Text="perez"></asp:Label>    <asp:Label ID="PunteoP1" runat="server" Text="13"></asp:Label></h1>
                    </div>
                    <div class="col">vs </div>
                    <div class="col-4">
                        <h1> <asp:Label ID="PunteoP2" runat="server" Text="25"></asp:Label> <asp:Label ID="lblJugador2" runat="server" Text="simon"></asp:Label></h1>
                    </div>
                </div>
                <div class="headTurno">
                   <h1> Turno Actual es:  <asp:Label ID="turnPlayer" runat="server" Text=""></asp:Label> color: <asp:Button ID="btnTurno" runat="server" class="ficha" Width = 50 Height = 50/></h1>
                    
                </div>
            </div>
            <asp:Button ID="Btn_Extream" runat="server" Text="Partida Xtream" OnClick="Btn_Extream_Click" class="btn btn-primary"/>
            <asp:Button ID="Btn_normal" runat="server" Text="Partida Normal" class="btn btn-primary"/>


            <div class="tablero_Completo">
                <div>
                    <asp:Panel ID="Panel1" runat="server"></asp:Panel>
                </div>
            </div>

            
            <div>
                <div>
                    <asp:Panel ID="PanelFormulario" runat="server" Visible="False">
                        <div id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="exampleModalLabel">Detalles de Partida</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">

                                        <div class="form-inline" >
                                            <div>
                                                <h4>Tablero</h4>
                                            </div>
                                            <div class="form-group row">
                                                <label for="recipient-name" class="col-form-label">Alto:</label>
                                                <asp:TextBox ID="txtAltura" runat="server" class="form-control" placeholder="6 - 20"></asp:TextBox>
                                                <label class="col-form-label">Ancho:</label>
                                                <asp:TextBox ID="txtAncho" runat="server" class="form-control" placeholder="6 - 20"> </asp:TextBox>
                                            </div>
                                        </div>
                                        <hr />
                                         <div>
                                             <div>
                                                <h4>Jugador 2</h4>
                                                 </div>
                                        <div>
                                             <asp:TextBox ID="Usuario_dos" runat="server" class="form-control" placeholder="Usuario"> </asp:TextBox>
                                        </div>
                                            </div>
                                        <hr />
                                        <%--Fin primera parte--%>
                                        <div >
                                            <h4>
                                                <label class="col-form-label">Colores</label>
                                            </h4>
                                            <div class="form-group row ">
                                                <div class="col-4" align="left">    
                                                <h4><asp:Label ID="Label1" runat="server" Text="Jugador 1"></asp:Label></h4>
                                                <asp:CheckBoxList ID="CheckBoxList1" runat="server" OnSelectedIndexChanged="selectcolor" class="align-self-lg-start">
                                                    <asp:ListItem value="rojo">Rojo</asp:ListItem>
                                                    <asp:ListItem value="amarillo">Amarillo</asp:ListItem>
                                                    <asp:ListItem value="anaranjado">Anaranjado</asp:ListItem>
                                                    <asp:ListItem value="azul">Azul</asp:ListItem>
                                                    <asp:ListItem value="verde">Verde</asp:ListItem>
                                                    <asp:ListItem value="violeta">Violeta</asp:ListItem>
                                                    <asp:ListItem value="blanco">Blanco</asp:ListItem>
                                                    <asp:ListItem value="negro">Negro</asp:ListItem>
                                                    <asp:ListItem value="celeste">Celeste</asp:ListItem>
                                                    <asp:ListItem value="gris">Gris</asp:ListItem>
                                                </asp:CheckBoxList>
                                                     <asp:Button ID="buttonmania" runat="server" Text="Confirm Colores" OnClick="buttonmania_Click" />

                                            </div>
                                            <div  class="col-4" align="left">
                                                <h4><asp:Label ID="Label2" runat="server" Text="Jugador 2"></asp:Label></h4>
                                                <asp:CheckBoxList ID="CheckBoxList2" runat="server">
                                                    <asp:ListItem value="rojo">Rojo</asp:ListItem>
                                                    <asp:ListItem value="amarillo">Amarillo</asp:ListItem>
                                                    <asp:ListItem value="anaranjado">Anaranjado</asp:ListItem>
                                                    <asp:ListItem value="azul">Azul</asp:ListItem>
                                                    <asp:ListItem value="verde">Verde</asp:ListItem>
                                                    <asp:ListItem value="violeta">Violeta</asp:ListItem>
                                                    <asp:ListItem value="blanco">Blanco</asp:ListItem>
                                                    <asp:ListItem value="negro">Negro</asp:ListItem>
                                                    <asp:ListItem value="celeste">Celeste</asp:ListItem>
                                                    <asp:ListItem value="gris">Gris</asp:ListItem>
                                                </asp:CheckBoxList>
                                               
                                               <asp:Button ID="button1" runat="server" Text="Confirm Colores" OnClick="buttonmania_Click" />
                                            </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="form-inline">
                                            <h4><label class="col-form-label">Cargar XML</label></h4>
                                            <asp:FileUpload ID="FileUpload1" runat="server" />

                                        </div>
                                        <hr />
                                        <div class="form-group row">
                                            <h4>Apertura</h4>
                                              <div  class="col-4" align="left">
                                                <asp:CheckBoxList ID="CheckApertura" runat="server">
                                                    <asp:ListItem value="libre">Libre</asp:ListItem>
                                                    <asp:ListItem value="automatica">Automatica</asp:ListItem>
                                              </asp:CheckBoxList>
                                                    </div>
                                        </div>
                                         <hr />
                                        <div class="form-group row">
                                            <h4>Modo de Juego</h4>
                                            <div  class="col-4" align="left">
                                                <asp:CheckBoxList ID="ModoGAme" runat="server">
                                                    <asp:ListItem value="normal">Normal</asp:ListItem>
                                                    <asp:ListItem value="inverso">Inverso</asp:ListItem>
                                              </asp:CheckBoxList>
                                                    </div>
                                            </div>

                                    </div>
                                </div>
                            </div>


                            <div class="modal-footer">
                                
                                <asp:Button ID="CerrarPanelForm" runat="server" Text="Close"  class="btn btn-secondary" OnClick="CerrarPanelForm_Click" />
                                <asp:Button ID="PartidaPVP" runat="server" Text="Iniciar Partida" OnClick="Iniciar_Extream" class="btn btn-primary" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>



            <div class="feet_Pagina">

                <asp:Panel ID="Panel2" runat="server"></asp:Panel>
                <asp:Button ID="CrearXML" runat="server" Text="Guardar Partida" OnClick="CrearXML_Click" Visible="false" class="btn btn-primary"/>
                <asp:Button ID="Btn_Reset" runat="server" Text="Nueva Partida" OnClick="Restart" Visible="false" class="btn btn-primary" />
            </div>
        </div>



    </body>
    </html>

</asp:Content>
