<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Othello.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtUser" runat="server" placeholder="Nombre de Usuario"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUser" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>
            <br />
            <asp:TextBox ID="txtName" runat="server" placeholder="Nombres"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtName" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>
            <br />
            <asp:TextBox ID="txtApellido" runat="server" placeholder="Apellidos"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtApellido" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>
            <br />
            <asp:TextBox ID="txtPass" runat="server" placeholder="Contraseña"  type="password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPass" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>
            <br />
            <asp:TextBox ID="txtFecha" runat="server" type="Date"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFecha" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>
            <br />
            <asp:TextBox ID="txtPais" runat="server" placeholder="Pais"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPais" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>
            <br />
            <asp:TextBox ID="txtCorreo" runat="server" type="email" placeholder="Correo Electronico"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCorreo" ErrorMessage="Campo Obligatorio"></asp:RequiredFieldValidator>

            <br />

            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
            <asp:Button ID="btnRegistrarse" runat="server" Text="Registrarse" OnClick="btnRegistrarse_Click"/>
            <br />
            <asp:Label ID="Label1" runat="server" ></asp:Label>

        </div>
    </form>
</body>
</html>
