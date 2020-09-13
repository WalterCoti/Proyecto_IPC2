<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Othello.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    
    <link rel="stylesheet" href="css/styleLogin.css" />
</head>
<body>
    <form id="form1" runat="server" class="fondo">
        <div >
            <asp:TextBox ID="txtUsername" runat="server" placeholder="Usuario" CssClass ="text_Form "></asp:TextBox>
            <br />
            <asp:TextBox ID="txtPassword" runat="server" placeholder="Contraseña" type="password"  CssClass ="text_Form "></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnInicio" runat="server" Text="Iniciar Sesion" OnClick="btnInicio_Click" class="boton" />
                <br />
            <br />
            <asp:Button ID="btnRegistro" runat="server" Text="Registrarse" OnClick="btnRegistro_Click" class="boton"/>
                <br />
            <br />
            <asp:Label ID="Label1" runat="server" ></asp:Label>
        </div>
    </form>
</body>
</html>
