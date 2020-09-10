using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Othello
{
    public partial class Login : System.Web.UI.Page
    {
        string connectionString = "Data Source = GUSTAVC; Initial Catalog = OTHELLO; Integrated Security = True";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistro_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registro.aspx");
        }

        protected void btnInicio_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM JUGADOR WHERE USERNAME='" + txtUsername.Text + "' AND PASSWRD = '" + txtPassword.Text+"'");
            cmd.Connection = con;

            int OBJ = (Int32)cmd.ExecuteScalar();
            if (OBJ > 0)
            {
                /*Session["name"] = txtUsername.Text;
                SiteMaster empl = new SiteMaster();
                empl.damenombre(txtUser.Text);*/
                Response.Redirect("home.aspx");
                
                txtUsername.Text = "";
                txtPassword.Text = "";
            }
            else
            {
                Label1.Text = "Datos Incorrectos";
                this.Label1.ForeColor = Color.Red;
            }
            con.Close();
        }
    
    }
}