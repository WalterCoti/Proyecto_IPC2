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
     
                <div><asp:Label ID="lb_cronom" runat="server" Text="Cronometro"></asp:Label>

                    
                </div>
                
            </div>
            <div class="lateral-zone"></div>
            <div class="tablero_Completo">
                <div ><asp:Panel ID="Panel1" runat="server" > </asp:Panel></div> 
            </div>
            
           <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal" data-whatever="@mdo">Iniciar Partida P1 vs P2</button>
            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal" data-whatever="@mdo">Partida P1 vs CPU</button>
          
                <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                  <div class="modal-dialog" role="document">
                    <div class="modal-content">
                      <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Detalles de Partida</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                          <span aria-hidden="true">&times;</span>
                        </button>
                      </div>
                      <div class="modal-body">
                      
                            <div class="form-inline">
                            <h4><label class="col-form-label">Tablero</label> </h4>
                          <div class="form-group row">
                            <label for="recipient-name" class="col-form-label">Alto:</label>
                              <asp:TextBox ID="txtAltura" runat="server" class="form-control" placeholder="6 - 20"></asp:TextBox>
                            <label class="col-form-label">Ancho:</label>
                              <asp:TextBox ID="txtAncho" runat="server" class="form-control" placeholder="6 - 20"> </asp:TextBox>
                           </div>
                          </div>
                            <hr />  <%--Fin primera parte--%>
                            <div class="form-inline">
                                <h4><label class="col-form-label">Colores</label> </h4>
                                <div>

                                </div>
                                <div>

                                </div>

                            </div>
                            <hr />  
                            <div class="form-inline">
                                <h4><label class="col-form-label">Tipo de Partida</label></h4>
                            </div>
                             <hr />  
                            <div class="form-inline">
                                <h4><label class="col-form-label">Apertura</label></h4>
                            </div>
                          
                       
                      </div>

                      <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <asp:Button ID="PartidaPVP" runat="server" Text="Iniciar Partida" OnClick="Button1_Click" class="btn btn-primary" />
                      </div>
                    </div>
                  </div>
                </div>
               
        
        
            <div class="feet_Pagina" >
                 
            <asp:Button ID="Partida_CPU" runat="server" Text="P1 vs CPU" OnClick="Partida_CPU_Click" />
               
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
