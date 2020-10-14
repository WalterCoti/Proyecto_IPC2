using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.Ajax.Utilities;
using System.Runtime.Remoting.Messaging;
using System.IO;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
namespace Othello
{


    public partial class _Default : Page
    {
        static int turno = 1;
        private static Celda cldaNueva;
        private static Celda[,] tablero = new Celda[8, 8];


        protected void Page_Load(object sender, EventArgs e)
        {
            Panel1.Controls.Clear();
            imprimir_Tab();
        }


        public void CrearTablero()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    cldaNueva = new Celda();
                    cldaNueva.Width = 65;
                    cldaNueva.Height = 65;
                    cldaNueva.Columna = j;
                    // cldaNueva.BackColor = Color.Red;
                    cldaNueva.Fila = i;
                    cldaNueva.CssClass = "ficha";
                    tablero[i, j] = cldaNueva;
                }
            }
            imprimir_Tab();
        }

        class Celda : System.Web.UI.WebControls.Button
        {
            public int Columna { get; set; }
            public int Fila { get; set; }
            public Boolean TieneFicha { get; set; }
            public string colorFicha { get; set; }

        }


        private void Ficha_click(Object sender, EventArgs e)
        {
            Celda FichaSeleccionada = (Celda)sender;

            if (turno == 0)
            {
                FichaSeleccionada.Enabled = false;
                FichaSeleccionada.BackColor = Color.White;
                FichaSeleccionada.colorFicha = "blanco";
                FichaSeleccionada.TieneFicha = true;
                turno = 1;
                posValida("blanco", "negro");
               // imprimir_Tab();
            }
            else if (turno == 1)
            {
                FichaSeleccionada.Enabled = false;
                FichaSeleccionada.BackColor = Color.Black;
                FichaSeleccionada.colorFicha = "negro";
                FichaSeleccionada.TieneFicha = true;
                turno = 0;
                posValida("negro", "blanco");
               // imprimir_Tab();
            }
        }

        private void imprimir_Tab()
        {
            //Panel1.Controls.Clear();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (tablero[i, j] == null)
                    {
                        break;
                    }
                    else
                    {

                        tablero[i, j].Click += new EventHandler(this.Ficha_click);
                        Panel1.Controls.Add(tablero[i, j]);

                    }
                }
            }
            if (turno == 1)
            {
                posValida("negro", "blanco");

            }
            else if (turno == 0)
            {
                posValida("blanco", "negro");
            }
        }

        protected void LeerXML_Click(object sender, EventArgs e)
        {
            if (tablero[0, 0] == null) {
                MessageBox.Show("Debe iniciar una aprtida Primero");

            }
            else
            {
                if (FileUpload1.HasFile)
                {
                    string fileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);// obtiene el nombre del archivo
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Importado/" + fileName));//lo guarda en la carpeta uploads
                    string filepath = Server.MapPath("~/Importado/" + FileUpload1.FileName);//busca el archivo en el servidor

                    XmlDocument xml_doc = new XmlDocument();
                    xml_doc.Load(filepath);

                    foreach (XmlNode node in xml_doc.DocumentElement.ChildNodes)
                    {
                        if (node.Name == "ficha")
                        {
                            string xml_color = "";
                            int xml_columna = 0;
                            int xml_Fila = 0;
                            for (int i = 0; i < node.ChildNodes.Count; i++)
                            {
                                if (node.ChildNodes[i].Name == "color")
                                {
                                    if (node.ChildNodes[i].InnerText == "blanco")
                                    {

                                        xml_color = "blanco";
                                        continue;
                                    }
                                    else
                                    {
                                        xml_color = "negro";
                                        continue;
                                    }
                                }
                                else if (node.ChildNodes[i].Name == "columna")
                                {
                                    try
                                    {
                                        foreach (KeyValuePair<string, int> tmp_Alpha in alpha_to_int)
                                        {
                                            if (tmp_Alpha.Key == node.ChildNodes[i].InnerText)
                                            {
                                                xml_columna = tmp_Alpha.Value;
                                                break;
                                            }
                                        }
                                        continue;
                                    } catch (Exception exept)
                                    {
                                        MessageBox.Show("Esta ficha no puede colocarse fuera del tablero");
                                    }
                                }
                                else
                                {
                                    xml_Fila = int.Parse(node.ChildNodes[i].InnerText);

                                    xml_cambio(xml_color, xml_Fila, xml_columna);
                                    continue;
                                }
                            }
                        }
                        if (node.Name == "siguienteTiro")
                        {
                            for (int i = 0; i < node.ChildNodes.Count; i++)
                            {
                                if (node.ChildNodes[i].InnerText == "blanco")
                                {
                                    turno = 0;
                                }
                                else
                                {
                                    turno = 1;
                                }
                            }
                        }
                    }
                }
                imprimir_Tab();
            }
        }

        private void xml_cambio(string color, int Fila, int Columna)
        {
            string txt_Columna = "";
            try
            {
                Celda xmlCelda = tablero[Fila - 1, Columna - 1];
                if (Fila > 8 || Columna > 8)
                {
                    MessageBox.Show("Esta ficha no puede colocarse fuera del tablero");
                }
                else if (xmlCelda.TieneFicha)
                {

                }
                else
                {
                    if (color == "blanco")
                    {
                        xmlCelda.Enabled = false;
                        xmlCelda.BackColor = Color.White;
                        xmlCelda.colorFicha = "blanco";
                        xmlCelda.TieneFicha = true;
                    }
                    else
                    {
                        xmlCelda.Enabled = false;
                        xmlCelda.BackColor = Color.Black;
                        xmlCelda.colorFicha = "negro";
                        xmlCelda.TieneFicha = true;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Esta ficha no puede colocarse en la posicion " + Fila.ToString() + " " + txt_Columna);
            }


        }

        protected void CrearXML_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();

            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            XmlElement element1 = doc.CreateElement(string.Empty, "tablero", string.Empty);
            doc.AppendChild(element1);

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Celda creaxmlCelda = tablero[i, j];
                    Boolean numa = creaxmlCelda.TieneFicha;
                    if (creaxmlCelda.TieneFicha)
                    {
                        XmlElement nodoFicha = doc.CreateElement(string.Empty, "ficha", string.Empty);
                        element1.AppendChild(nodoFicha);

                        XmlElement atributo_Color = doc.CreateElement(string.Empty, "color", string.Empty);
                        XmlText txt_atributo_Color = doc.CreateTextNode(creaxmlCelda.colorFicha);
                        atributo_Color.AppendChild(txt_atributo_Color);
                        nodoFicha.AppendChild(atributo_Color);

                        XmlElement atrib_columna = doc.CreateElement(string.Empty, "columna", string.Empty);

                        foreach (KeyValuePair<int, string> tmp_Alpha in int_to_alpha)
                        {
                            if (tmp_Alpha.Key == creaxmlCelda.Columna)
                            {
                                XmlText txt_atrib_columna = doc.CreateTextNode(tmp_Alpha.Value);
                                atrib_columna.AppendChild(txt_atrib_columna);
                                nodoFicha.AppendChild(atrib_columna);
                                break;
                            }
                        }


                        XmlElement atrib_Fila = doc.CreateElement(string.Empty, "fila", string.Empty);
                        XmlText txt_atrib_Fila = doc.CreateTextNode((creaxmlCelda.Fila + 1).ToString());
                        atrib_Fila.AppendChild(txt_atrib_Fila);
                        nodoFicha.AppendChild(atrib_Fila);
                    }
                    continue;
                }
                continue;
            }
            XmlElement nodoTurno = doc.CreateElement(string.Empty, "siguienteTiro", string.Empty);
            element1.AppendChild(nodoTurno);

            XmlElement turnoColor = doc.CreateElement(string.Empty, "color", string.Empty);
            if (turno == 0)
            {
                XmlText txt_turno_color = doc.CreateTextNode("blanco");
                nodoTurno.AppendChild(turnoColor);
                turnoColor.AppendChild(txt_turno_color);
            }
            else
            {
                XmlText txt_turno_color = doc.CreateTextNode("negro");
                nodoTurno.AppendChild(turnoColor);
                turnoColor.AppendChild(txt_turno_color);
            }
            string filepath = Server.MapPath("~/Exportado/");
            doc.Save(filepath + "Partida.xml");
        }


        Dictionary<int, string> int_to_alpha = new Dictionary<int, string>
            {
                {0,"A"},
                {1,"B"},
                {2,"C"},
                {3,"D"},
                {4,"E"},
                {5,"F"},
                {6,"G"},
                {7,"H" }
            };

        Dictionary<string, int> alpha_to_int = new Dictionary<string, int>
            {
                {"A",1},
                {"B",2},
                {"C",3},
                {"D",4},
                {"E",5},
                {"F",6},
                {"G",7},
                {"H",8}
            };

        protected void Button1_Click(object sender, EventArgs e)
        {
            CrearTablero();
            iniciarPartida_PVP();
            imprimir_Tab();
            PartidaPVP.Visible = false;
            Partida_CPU.Visible = false;


        }
        private void iniciarPartida_PVP()
        {
            tablero[3, 3].TieneFicha = true;
            tablero[3, 3].BackColor = Color.White;
            tablero[3, 3].Enabled = false;
            tablero[3, 3].colorFicha = "blanco";

            tablero[3, 4].TieneFicha = true;
            tablero[3, 4].BackColor = Color.Black;
            tablero[3, 4].Enabled = false;
            tablero[3, 4].colorFicha = "negro";

            tablero[4, 3].TieneFicha = true;
            tablero[4, 3].BackColor = Color.Black;
            tablero[4, 3].Enabled = false;
            tablero[4, 3].colorFicha = "negro";

            tablero[4, 4].TieneFicha = true;
            tablero[4, 4].BackColor = Color.White;
            tablero[4, 4].Enabled = false;
            tablero[4, 4].colorFicha = "blanco";


        }


        public void posValida(string Color_Act, string Color_rival)
        {
            try
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (tablero[i, j] == null)
                        {
                        }
                        else
                        {
                            if (tablero[i, j].TieneFicha == true)//Busco los espacios vacios, para saber si es marcable o no
                            {

                                continue;
                            }
                            else
                            {
                                tablero[i, j].BackColor = Color.Green;
                                //tablero[i, j].Enabled = false;
                                Value_UP(i, j, Color_Act, Color_rival);
                                Value_Up_Left(i, j, Color_Act, Color_rival);
                                Value_Left(i, j, Color_Act, Color_rival);
                                Value_Down_Left(i, j, Color_Act, Color_rival);
                                Value_Down(i, j, Color_Act, Color_rival);

                                Value_Rigth(i, j, Color_Act, Color_rival);

                            }
                        }
                    }
                }
            } catch (NullReferenceException nle)
            {

            }
        }

        public void Value_UP(int fila, int columna, string ColorA, string ColorB)
        {
            try
            {
                int distancia = 0;
                for (int i = fila; i >= 0; i--)
                {
                    if (tablero[i - 1, columna].TieneFicha)
                    {
                        if (tablero[i - 1, columna].colorFicha == ColorB)
                        {
                            distancia++;
                            continue;
                        }
                        else if (tablero[i - 1, columna].colorFicha == ColorA)
                        {
                            if (distancia > 0)
                            {
                                tablero[fila, columna].BackColor = Color.Gray;
                                //tablero[fila, columna].BorderColor = Color.Red;
                                tablero[fila, columna].Enabled = true;
                                break;
                            }
                        }
                    }
                    break;
                }
            } catch (Exception exeption)
            {

            }
        }

        public void Value_Up_Left(int fila, int columna, string ColorA, string ColorB)
        {
            try
            {
                int distancia = 0;
                for (int i = fila; i >= 0; i--)
                {
                    for (int j = columna; j >= 0; j--)
                    {
                        if (tablero[i - 1, j - 1].TieneFicha)
                        {
                            if (tablero[fila, j - 1].colorFicha == ColorB)
                            {
                                distancia++;
                                continue;
                            }
                            else if (tablero[i - 1, j - 1].colorFicha == ColorA)
                            {
                                if (distancia > 0)
                                {
                                    tablero[fila, columna].BackColor = Color.Gray;
                                    tablero[fila, columna].Enabled = true;
                                    break;
                                }
                            }
                        }
                        break;
                    }
                    break;
                }
            }
            catch (Exception exeption)
            {

            }
        }

        public void Value_Left(int fila, int columna, string ColorA, string ColorB)
        {
            try
            {
                int distancia = 0;
                for (int j = columna; j >= 0; j--)
                {
                    if (tablero[fila, j - 1].TieneFicha)
                    {
                        if (tablero[fila, j - 1].colorFicha == ColorB)
                        {
                            distancia++;
                            continue;
                        }
                        else if (tablero[fila, j - 1].colorFicha == ColorA)
                        {
                            if (distancia > 0)
                            {
                                tablero[fila, columna].BackColor = Color.Gray;
                                tablero[fila, columna].Enabled = true;
                                break;
                            }
                        }
                    }
                    break;
                }
            }
            catch (Exception exeption)
            {

            }
        }

        public void Value_Down_Left(int fila, int columna, string ColorA, string ColorB)
        {
            int distancia = 0;
            for (int i = fila; i <= 5; i++)
            {
                for (int j = columna; j > 0; j--)
                {
                    try
                    {
                        if (tablero[i + 1, j - 1].TieneFicha)
                        {
                            if (tablero[i + 1, j - 1].colorFicha == ColorB)
                            {
                                distancia++;
                                break;
                            }
                            else if (tablero[i + 1, j - 1].colorFicha == ColorA)
                            {
                                if (distancia > 0)
                                {
                                    tablero[fila, columna].BackColor = Color.Red;
                                    tablero[fila, columna].Enabled = true;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch (Exception exeption)
                    {

                    }

                }
            }

        }
    

        public void Value_Down(int fila, int columna, string ColorA, string ColorB)
        {
            try
            {
                int distancia = 0;
                for (int i = fila; i <= 8; i++)
                {
                    if (tablero[i+1, columna].TieneFicha)
                    {
                        
                        if (tablero[i + 1, columna].colorFicha == ColorB)
                        {
                            distancia++;
                            continue;
                        }
                        else if (tablero[i + 1, columna].colorFicha == ColorA)
                        {
                            if (distancia > 0)
                            {
                                tablero[fila, columna].BackColor = Color.Gray;
                                tablero[fila, columna].Enabled = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                 
                }
            }
            catch (Exception exeption)
            {

            }
        }

        public void Value_Rigth(int fila, int columna, string ColorA, string ColorB)
        {
            try
            {
                int distancia = 0;
                for (int j = columna; j <= 8; j++)
                {
                    if (tablero[fila, j+1].TieneFicha)
                    {

                        if (tablero[fila, j + 1].colorFicha == ColorB)
                        {
                            distancia++;
                            continue;
                        }
                        else if (tablero[fila, j + 1].colorFicha == ColorA)
                        {
                            if (distancia > 0)
                            {
                                tablero[fila, columna].BackColor = Color.Gray;
                                tablero[fila, columna].Enabled = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }

                }
            }
            catch (Exception exeption)
            {

            }
        }

        public void marcarPosible(int fila, int columna)
        {
            tablero[fila, columna].BackColor = Color.Gray;
        }



    }
}