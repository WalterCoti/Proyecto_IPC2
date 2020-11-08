<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="Othello.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Datos personales</h2>

    <table class="table">
  <thead class="thead-dark">
    <tr>
      <th scope="col"></th>
      <th scope="col"></th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th scope="row">Nombre de Usuario</th>
      <td> </td>
    </tr>
    <tr>
      <th scope="row"> Nombre </th>
      <td></td>
    </tr>
    <tr>
      <th scope="row">Apellido</th>
      <td></td>
    </tr>
       <tr>
      <th scope="row">Fecha Nac</th>
      <td></td>
    </tr> <tr>
      <th scope="row">Pais</th>
      <td></td>
    </tr> <tr>
      <th scope="row">Correo</th>
      <td></td>
    </tr>

  </tbody>
</table>





    <div>
        <h2>Datos de Partidas</h2>
    <table class="table">
  <thead>
    <tr>
      <th scope="col"></th>
      <th scope="col">Contador</th> 
    </tr>
  </thead>
  <tbody>
    <tr>
      <th scope="row">Partidas Ganadas</th>
      <td>0</td>

    </tr>
    <tr>
      <th scope="row">Partidas Empatadas</th>
      <td>0</td>

    </tr>
      <tr>
      <th scope="row">Partidas Perdidas</th>
      <td>0</td>
    </tr>
    <tr>
      <th scope="row">Torneos Participados</th>
      <td>0</td>
    </tr>
       
       <tr>
      <th scope="row">Torneos Ganados</th>
      <td>0</td>
    </tr>
       <tr>
      <th scope="row">Torneos Perdidos</th>
      <td>0</td>
    </tr>
      
  </tbody>
</table>



        </div>
</asp:Content>
