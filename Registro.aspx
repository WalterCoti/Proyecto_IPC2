<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Othello.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" href="css/styleReg.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="fondo">
            <asp:TextBox ID="txtUser" runat="server" placeholder="Nombre de Usuario" CssClass="text_Form"></asp:TextBox>
             <br />
            <asp:TextBox ID="txtName" runat="server" placeholder="Nombres" CssClass="text_Form"></asp:TextBox>
            <br />
            <asp:TextBox ID="txtApellido" runat="server" placeholder="Apellidos" CssClass="text_Form"></asp:TextBox>
             <br />
            <asp:TextBox ID="txtPass" runat="server" placeholder="Contraseña"  type="password" CssClass="text_Form"> </asp:TextBox>
            <br />
            <asp:TextBox ID="txtFecha" runat="server" type="Date" CssClass="text_Form"></asp:TextBox>
            <br />
            <asp:TextBox ID="txtPais" runat="server" placeholder="Pais" CssClass="text_Form"></asp:TextBox>
            <br />
            <asp:TextBox ID="txtCorreo" runat="server" type="email" placeholder="Correo Electronico" CssClass="text_Form"></asp:TextBox>
           <br />
            <br />
            <asp:Button ID="btnRegistrarse" runat="server" Text="Registrarse" OnClick="btnRegistrarse_Click" class="boton"/>
            <br />
            <br />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" class="boton"/>
            <br />
            <asp:Label ID="Label1" runat="server" ></asp:Label>

        </div>
    </form>
</body>
</html>
