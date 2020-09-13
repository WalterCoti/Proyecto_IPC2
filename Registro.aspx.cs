using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace Othello
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string connectionString = "Data Source = GUSTAVC; Initial Catalog = OTHELLO; Integrated Security = True";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            string tmpUser = txtUser.Text;
            string tmpName = txtName.Text;
            string tmpApe = txtApellido.Text;
            string tmpPass = txtPass.Text;
            string tmpFecha = txtFecha.Text;
            string tmpPais = txtPais.Text;
            string tmpCorreo = txtCorreo.Text;

            if (txtUser.Text.Equals(""))
            {

            }

            setJugador(tmpUser, tmpName, tmpApe, tmpPass,  tmpFecha, tmpCorreo, tmpPais);

        }

        private void setJugador(string Username, string nombres, string apellidos, string password,  string fechaNac, string correo, string pais  )
        {
            SqlConnection sqlCon = new SqlConnection(connectionString);
            sqlCon.Open();
            String consulta = "SELECT USERNAME FROM JUGADOR WHERE USERNAME = '" + Username + "'";
            SqlCommand comando = new SqlCommand(consulta);
            comando.Connection = sqlCon;
                try
                {
                    SqlDataReader reader= comando.ExecuteReader();
                    reader.Read();

                if (Username.Equals(reader.GetValue(0).ToString()))
                {
                    Label1.Text = "El nombre de Usuario ya esta en uso";
                    txtUser.Text = "";
                    this.Label1.ForeColor = Color.Red;
                }
                else
                {
                   
                }
         
            }
                catch (Exception)
                {
                sqlCon.Close();
                SqlDataAdapter sqlDa = new SqlDataAdapter("INSERT INTO JUGADOR VALUES ('" + Username + "','" + nombres + "','" + apellidos + "','" + password + "','" + fechaNac + "','" + correo + "','" + pais + "')", sqlCon);
                DataTable dtbl = new DataTable();

                sqlDa.Fill(dtbl);
                sqlCon.Close();
                Label1.Text = "Usuario creado con exito";
                this.Label1.ForeColor = Color.Green;
                limpiarFormulario();
            }

                
                
            }
        

        private void limpiarFormulario()
        {
            txtUser.Text = "";
            txtName.Text = "";
            txtApellido.Text = "";
            txtPass.Text = "";
            txtFecha.Text = "";
            txtPais.Text = "";
            txtCorreo.Text = "";
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

     
    }
}
