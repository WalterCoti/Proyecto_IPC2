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
        static int turno = 0;
        private static Celda cldaNueva;
        private static Object[,] tablero = new object[8,8];

        
            
        protected void Page_Load(object sender, EventArgs e)
        {
        CrearTablero();    
        }
        
        
        private void CrearTablero()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    cldaNueva = new Celda();
                    cldaNueva.Width = 50;
                    cldaNueva.Height = 50;
                    cldaNueva.Columna = j;
                    cldaNueva.Fila = i;
                    
                    cldaNueva.CssClass = "boton";
                    cldaNueva.Click += new EventHandler(this.Ficha_click);
                    Panel1.Controls.Add(cldaNueva);
                    tablero[i, j] = cldaNueva;
                }
            }
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
            }
            else
            {
                FichaSeleccionada.Enabled = false;
                FichaSeleccionada.BackColor = Color.Black;
                FichaSeleccionada.colorFicha = "negro";
                FichaSeleccionada.TieneFicha = true;
                turno = 0;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
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
                                if(node.ChildNodes[i].InnerText == "blanco")
                                {
                                    
                                    xml_color = "blanco";
                                    continue;
                                }
                                else
                                {
                                    xml_color = "negro";
                                    continue;
                                }
                            }else if (node.ChildNodes[i].Name == "columna")
                            {
                                foreach(KeyValuePair<string, int> tmp_Alpha in alpha_to_int)
                                {
                                    if (tmp_Alpha.Key == node.ChildNodes[i].InnerText)
                                    {
                                        xml_columna = tmp_Alpha.Value;
                                        break;
                                    }
                                }
                                continue;
                            }
                            else
                            {
                                xml_Fila = int.Parse(node.ChildNodes[i].InnerText);
                                xml_cambio(xml_color, xml_Fila, xml_columna);
                                continue;
                            }
                        }
                    }
                    if(node.Name == "siguienteTiro")
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
        }

        private void xml_cambio(string color, int Fila, int Columna )
        {
            Celda xmlCelda = (Celda)tablero[Fila-1, Columna-1];
            if(color == "blanco")
            {
                xmlCelda.Enabled = false;
                xmlCelda.BackColor = Color.White;
                xmlCelda.TieneFicha = true;
            }
            else
            {
                xmlCelda.Enabled = false;
                xmlCelda.BackColor = Color.Black;
                xmlCelda.TieneFicha = true;
            }
        }

        protected void Button2_Click1(object sender, EventArgs e)
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
                    Celda creaxmlCelda = (Celda)tablero[i,j];
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
                        XmlText txt_atrib_Fila = doc.CreateTextNode(creaxmlCelda.Fila.ToString());
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
            doc.Save(filepath+"Partida.xml");
        }


        Dictionary<int, string> int_to_alpha = new Dictionary<int, string>
            {
                {1,"A"},
                {2,"B"},
                {3,"C"},
                {4,"D"},
                {5,"E"},
                {6,"F"},
                {7,"G"},
                {8,"H" }
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

    }
}