using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;
using System.Linq;
using System.Diagnostics;


namespace Othello
{
    public partial class _Default : Page
    {
        static int tab_Alto = 8;
        static int tab_Ancho = 8;
        static int con_Fichas = 0;
        private static Celda cldaNueva;
        private static Celda[,] tablero;
        private static string Jugador_uno = "";
        private static string Jugador_dos = "";
        private static List<string> Jug1 = new List<string>();
        private static List<string> Jug2 = new List<string>();



        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {

            }
            else
            {
                ViewState["turno"] = 0;
            }

            Panel1.Controls.Clear();
            imprimir_Tab();

        }

        public void CrearTablero()
        {
            
            tablero = new Celda[tab_Alto, tab_Ancho];
            for (int i = 0; i < tab_Alto; i++)
            {
                for (int j = 0; j < tab_Ancho; j++)
                {
                    cldaNueva = new Celda();
                    cldaNueva.Width = 50;
                    cldaNueva.Height = 50;
                    cldaNueva.Columna = j;
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
            if (ViewState["turno"].Equals(0))
            {
                FichaSeleccionada.Enabled = false;
                FichaSeleccionada.BackColor = Color.White;
                FichaSeleccionada.colorFicha = "blanco";
                FichaSeleccionada.TieneFicha = true;
                ViewState["turno"] = 1;
                // cambiarColor(FichaSeleccionada.Fila, FichaSeleccionada.Columna, "blanco", "negro");
                // posValida("blanco", "negro");
                //imprimir_Tab();
            }
            else if (ViewState["turno"].Equals(1))
            {
                FichaSeleccionada.Enabled = false;
                FichaSeleccionada.BackColor = Color.Black;
                FichaSeleccionada.colorFicha = "negro";
                FichaSeleccionada.TieneFicha = true;
                ViewState["turno"] = 0;
                // cambiarColor(FichaSeleccionada.Fila, FichaSeleccionada.Columna, "negro", "blanco");
                //  posValida("negro", "blanco");
                //imprimir_Tab();
            }
        }

        private void imprimir_Tab()
        {
            //Panel1.Controls.Clear();
            for (int i = 0; i < tab_Alto; i++)
            {
                for (int j = 0; j < tab_Ancho; j++)
                {
                    if (tablero == null)
                    {
                        break;
                    }
                    else
                    {
                        Panel1.Width = 50 * tab_Ancho;
                        Panel1.Height = (56 * tab_Alto);
                        tablero[i, j].Click += new EventHandler(this.Ficha_click);
                        BordeTablero();
                        Panel1.Controls.Add(tablero[i, j]);
                    }
                }
            }
            if (ViewState["turno"].Equals(1))
            {
                posValida("negro", "blanco");

            }
            else if (ViewState["turno"].Equals(0))
            {
                posValida("blanco", "negro");
            }
        }

        private void BordeTablero()
        {

            for (int i = 1; i < tab_Alto - 1; i++)
            {
                //-------------------------FILA SE AGREGA EL NUMERO-----------------------
                tablero[i, 0].CssClass = "borde_tablero";
                tablero[i, 0].Text = i.ToString();
                tablero[i, 0].Enabled = false;
                tablero[i, tab_Ancho - 1].CssClass = "borde_tablero";
                tablero[i, tab_Ancho - 1].Text = i.ToString();
                tablero[i, tab_Ancho - 1].Enabled = false;
            }
            for (int j = 1; j < tab_Ancho - 1; j++)
            {
                //------------------------- COLUMNA SE AGREGA EL TEXTO---------------
                tablero[0, j].CssClass = "borde_tablero";
                tablero[0, j].Enabled = false;
                tablero[tab_Alto - 1, j].CssClass = "borde_tablero";
                tablero[tab_Alto - 1, j].Enabled = false;
                foreach (KeyValuePair<int, string> tmp_Alpha in int_to_alpha)
                {
                    if (tmp_Alpha.Key == j)
                    {
                        tablero[0, j].Text = tmp_Alpha.Value;
                        tablero[tab_Alto - 1, j].Text = tmp_Alpha.Value;
                    }
                }
            }
            tablero[0, 0].CssClass = "borde_tablero";
            tablero[0, 0].Enabled = false;
            tablero[0, tab_Ancho - 1].CssClass = "borde_tablero";
            tablero[0, tab_Ancho - 1].Enabled = false;
            tablero[tab_Alto - 1, 0].CssClass = "borde_tablero";
            tablero[tab_Alto - 1, 0].Enabled = false;
            tablero[tab_Alto - 1, tab_Ancho - 1].CssClass = "borde_tablero";
            tablero[tab_Alto - 1, tab_Ancho - 1].Enabled = false;
        }

        protected void LeerXML_Click()
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
                            ViewState["turno"] = 0;
                        }
                        else
                        {
                            ViewState["turno"] = 1;
                        }
                    }
                }
            }
            imprimir_Tab();

        }

        private void xml_cambio(string color, int Fila, int Columna)
        {
            string txt_Columna = "";
            try
            {
                Celda xmlCelda = tablero[Fila - 1, Columna - 1];
                if (Fila > tab_Alto - 1 || Columna > tab_Ancho - 1)
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

            for (int i = 1; i < tab_Alto - 1; i++)
            {
                for (int j = 1; j < tab_Ancho - 1; j++)
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
            if (ViewState["turno"].Equals(0))
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
                {1,"A"},
                {2,"B"},
                {3,"C"},
                {4,"D"},
                {5,"E"},
                {6,"F"},
                {7,"G"},
                {8,"H"},
                {9,"I"},
                {10,"J"},
                {11,"K"},
                {12,"L"},
                {13,"M"},
                {14,"N"},
                {15,"O"},
                {16,"P"},
                {17,"Q"},
                {18,"R"},
                {19,"S"},
                {20,"T"}
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
                {"H",8},
                {"I",9},
                {"J",10},
                {"K",11},
                {"L",12},
                {"M",13},
                {"N",14},
                {"O",15},
                {"P",16},
                {"Q",17},
                {"R",18},
                {"S",19},
                {"T",20}

            };


        private void iniciarPartida_PVP()
        {
            int x = (int)((tab_Alto - 2) / 2);
            int y = (int)((tab_Ancho - 2) / 2);

            tablero[x, y].TieneFicha = true;
            tablero[x, y].BackColor = Color.White;
            tablero[x, y].Enabled = false;
            tablero[x, y].colorFicha = "blanco";

            tablero[x, y + 1].TieneFicha = true;
            tablero[x, y + 1].BackColor = Color.Black;
            tablero[x, y + 1].Enabled = false;
            tablero[x, y + 1].colorFicha = "negro";

            tablero[x + 1, y].TieneFicha = true;
            tablero[x + 1, y].BackColor = Color.Black;
            tablero[x + 1, y].Enabled = false;
            tablero[x + 1, y].colorFicha = "negro";

            tablero[x + 1, y + 1].TieneFicha = true;
            tablero[x + 1, y + 1].BackColor = Color.White;
            tablero[x + 1, y + 1].Enabled = false;
            tablero[x + 1, y + 1].colorFicha = "blanco";
        }


        public void posValida(string Color_Act, string Color_rival)
        {
            try
            {
                for (int i = 0; i < tab_Alto - 1; i++)
                {
                    for (int j = 0; j < tab_Ancho - 1; j++)
                    {
                        if (tablero == null)
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
                                tablero[i, j].Enabled = false;

                                if (Value_UP(i, j, Color_Act, Color_rival)) //analiza hacia arriba
                                {
                                    tablero[i, j].Click += new EventHandler(this.Ficha_click);
                                    tablero[i, j].BackColor = Color.Gray;
                                    tablero[i, j].Enabled = true;

                                }

                                if (Value_Up_Left(i, j, Color_Act, Color_rival))//analiza hacia arriba y a la izquierda
                                {
                                    tablero[i, j].Click += new EventHandler(this.Ficha_click);
                                    tablero[i, j].BackColor = Color.Gray;
                                    tablero[i, j].Enabled = true;
                                }
                                if (Value_Left(i, j, Color_Act, Color_rival))// analiza hacia la izquierda
                                {
                                    tablero[i, j].Click += new EventHandler(this.Ficha_click);
                                    tablero[i, j].BackColor = Color.Gray;
                                    tablero[i, j].Enabled = true;
                                }
                                if (Value_Down_Left(i, j, Color_Act, Color_rival)) // analiza hacia abajo y a la izquierda
                                {
                                    tablero[i, j].Click += new EventHandler(this.Ficha_click);
                                    tablero[i, j].BackColor = Color.Gray;
                                    tablero[i, j].Enabled = true;
                                }
                                if (Value_Down(i, j, Color_Act, Color_rival))// analiza hacia abajo
                                {
                                    tablero[i, j].Click += new EventHandler(this.Ficha_click);
                                    tablero[i, j].BackColor = Color.Gray;
                                    tablero[i, j].Enabled = true;
                                }
                                if (Value_Down_Rigth(i, j, Color_Act, Color_rival))//analiza hacia abajo y derecha
                                {
                                    tablero[i, j].Click += new EventHandler(this.Ficha_click);
                                    tablero[i, j].BackColor = Color.Gray;
                                    tablero[i, j].Enabled = true;
                                }
                                if (Value_Rigth(i, j, Color_Act, Color_rival))//analiza hacia la derecha
                                {
                                    tablero[i, j].Click += new EventHandler(this.Ficha_click);
                                    tablero[i, j].BackColor = Color.Gray;
                                    tablero[i, j].Enabled = true;
                                }
                                if (Value_UP_Rigth(i, j, Color_Act, Color_rival))//analiza hacia arriba y derecha
                                {
                                    tablero[i, j].Click += new EventHandler(this.Ficha_click);
                                    tablero[i, j].BackColor = Color.Gray;
                                    tablero[i, j].Enabled = true;
                                }


                            }
                        }
                    }
                }
            } catch (NullReferenceException nle)
            {

            }
        }

        public Boolean Value_UP(int fila, int columna, string ColorA, string ColorB)
        {
            try
            {
                con_Fichas = 0;
                for (int i = 1; i <= tab_Alto - 1; i++)
                {
                    if (tablero[fila - i, columna].TieneFicha)
                    {
                        if (tablero[fila - i, columna].colorFicha == ColorB)
                        {
                            con_Fichas++;
                            continue;
                        }
                        else if (tablero[fila - i, columna].colorFicha == ColorA)
                        {
                            if (con_Fichas > 0)
                            {
                                return true;

                            }

                        }

                    }
                    else
                    {
                        return false;
                    }

                }
            } catch (Exception exeption)
            {
                return false;
            }
            return false;
        }
        public Boolean Value_Up_Left(int fila, int columna, string ColorA, string ColorB)
        {
            try
            {
                con_Fichas = 0;
                for (int i = 1; i <= tab_Alto - 1; i++)
                {
                    if (tablero[fila - i, columna - i].TieneFicha)
                    {
                        if (tablero[fila - i, columna - i].colorFicha == ColorB)
                        {
                            con_Fichas++;
                            continue;
                        }
                        else if (tablero[fila - i, columna - i].colorFicha == ColorA)
                        {
                            if (con_Fichas > 0)
                            {
                                return true;
                            }
                        }
                    }

                }
            }
            catch (Exception exeption)
            {
                return false;
            }
            return false;
        }
        public Boolean Value_Left(int fila, int columna, string ColorA, string ColorB)
        {
            try
            {
                con_Fichas = 0;
                for (int i = 1; i <= tab_Alto - 1; i++)
                {
                    if (tablero[fila, columna - i].TieneFicha)
                    {
                        if (tablero[fila, columna - i].colorFicha == ColorB)
                        {
                            con_Fichas++;
                            continue;
                        }
                        else if (tablero[fila, columna - i].colorFicha == ColorA)
                        {
                            if (con_Fichas > 0)
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception exeption)
            {
                return false;
            }
            return false;
        }
        public Boolean Value_Down_Left(int fila, int columna, string ColorA, string ColorB)
        {
            con_Fichas = 0;
            for (int i = 1; i <= tab_Alto - 1; i++)
            {
                try
                {
                    if (tablero[fila + i, columna - i].TieneFicha)
                    {
                        if (tablero[fila + i, columna - i].colorFicha == ColorB)
                        {
                            con_Fichas++;
                            break;
                        }
                        else if (tablero[fila + i, columna - i].colorFicha == ColorA)
                        {
                            if (con_Fichas > 0)
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception exeption)
                {
                    return false;
                }

            }
            return false;
        }
        public Boolean Value_Down(int fila, int columna, string ColorA, string ColorB)
        {
            try
            {
                con_Fichas = 0;
                for (int i = 1; i <= tab_Alto - 1; i++)
                {
                    if (tablero[fila + i, columna].TieneFicha)
                    {

                        if (tablero[fila + i, columna].colorFicha == ColorB)
                        {
                            con_Fichas++;
                            continue;
                        }
                        else if (tablero[fila + i, columna].colorFicha == ColorA)
                        {
                            if (con_Fichas > 0)
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception exeption)
            {
                return false;
            }
            return false;
        }
        public Boolean Value_Down_Rigth(int fila, int columna, string ColorA, string ColorB)
        {
            con_Fichas = 0;
            for (int i = fila; i <= tab_Alto - 1; i++)
            {
                try
                {
                    if (tablero[fila + i, columna + i].TieneFicha)
                    {
                        if (tablero[fila + i, columna + i].colorFicha == ColorB)
                        {
                            con_Fichas++;
                            break;
                        }
                        else if (tablero[fila + i, columna + i].colorFicha == ColorA)
                        {
                            if (con_Fichas > 0)
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception exeption)
                {
                    return false;
                }
            }
            return false;
        }
        public Boolean Value_Rigth(int fila, int columna, string ColorA, string ColorB)
        {
            try
            {
                con_Fichas = 0;
                for (int i = 1; i <= tab_Alto - 1; i++)
                {
                    if (tablero[fila, columna + i].TieneFicha)
                    {

                        if (tablero[fila, columna + i].colorFicha == ColorB)
                        {
                            con_Fichas++;
                            continue;
                        }
                        else if (tablero[fila, columna + i].colorFicha == ColorA)
                        {
                            if (con_Fichas > 0)
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception exeption)
            {
                return false;
            }
            return false;
        }
        public Boolean Value_UP_Rigth(int fila, int columna, string ColorA, string ColorB)
        {
            con_Fichas = 0;
            for (int i = 1; i <= tab_Alto - 1; i++)
            {
                try
                {
                    if (tablero[fila - 1, columna + 1].TieneFicha)
                    {
                        if (tablero[fila - 1, columna + 1].colorFicha == ColorB)
                        {
                            con_Fichas++;
                            break;
                        }
                        else if (tablero[fila - 1, columna + 1].colorFicha == ColorA)
                        {
                            if (con_Fichas > 0)
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception exeption)
                {
                    return false;
                }

            }
            return false;

        }

        protected void Partida_CPU_Click(object sender, EventArgs e)
        {

        }

        private void voltearFicha(int fila, int columna, string colorTurno)
        {
            if (colorTurno == "negro")
            {
                tablero[fila, columna].colorFicha = colorTurno;
                tablero[fila, columna].BackColor = Color.Black;
            }
            else if (colorTurno == "blanco")
            {
                tablero[fila, columna].colorFicha = colorTurno;
                tablero[fila, columna].BackColor = Color.White;
            }
        }
        private void cambiarColor(int Fila_ini, int Columna_ini, string color_Turno, string color_Rival)
        {

            if (Value_UP(Fila_ini, Columna_ini, color_Turno, color_Rival))
            {
                for (int i = 1; i <= con_Fichas; i++)// el click en el boton me da la ficha actual
                {
                    voltearFicha(Fila_ini - i, Columna_ini, color_Turno);

                }
            }
            if (Value_Up_Left(Fila_ini, Columna_ini, color_Turno, color_Rival))//analiza hacia arriba y a la izquierda
            {
                for (int i = 1; i <= con_Fichas; i++)// el click en el boton me da la ficha actual
                {
                    voltearFicha(Fila_ini - i, Columna_ini - i, color_Turno);
                }
            }
            if (Value_Left(Fila_ini, Columna_ini, color_Turno, color_Rival))// analiza hacia la izquierda
            {
                for (int i = 1; i <= con_Fichas; i++)// el click en el boton me da la ficha actual
                {
                    voltearFicha(Fila_ini, Columna_ini - i, color_Turno);

                }
            }
            if (Value_Down_Left(Fila_ini, Columna_ini, color_Turno, color_Rival)) // analiza hacia abajo y a la izquierda
            {
                for (int i = 1; i <= con_Fichas; i++)// el click en el boton me da la ficha actual
                {
                    voltearFicha(Fila_ini + i, Columna_ini - i, color_Turno);
                }
            }
            if (Value_Down(Fila_ini, Columna_ini, color_Turno, color_Rival))// analiza hacia abajo
            {
                for (int i = 1; i <= con_Fichas; i++)// el click en el boton me da la ficha actual
                {
                    voltearFicha(Fila_ini + i, Columna_ini, color_Turno);
                }
            }
            if (Value_Down_Rigth(Fila_ini, Columna_ini, color_Turno, color_Rival))//analiza hacia abajo y derecha
            {
                for (int i = 1; i <= con_Fichas; i++)// el click en el boton me da la ficha actual
                {
                    voltearFicha(Fila_ini + i, Columna_ini + i, color_Turno);
                }
            }
            if (Value_Rigth(Fila_ini, Columna_ini, color_Turno, color_Rival))//analiza hacia la derecha
            {
                for (int i = 1; i <= con_Fichas; i++)// el click en el boton me da la ficha actual
                {
                    voltearFicha(Fila_ini, Columna_ini + i, color_Turno);
                }
            }
            if (Value_UP_Rigth(Fila_ini, Columna_ini, color_Turno, color_Rival))//analiza hacia arriba y derecha
            {
                for (int i = 1; i <= con_Fichas; i++)// el click en el boton me da la ficha actual
                {
                    voltearFicha(Fila_ini - i, Columna_ini + i, color_Turno);
                }
            }

        }



        private void revPartida()
        {
            for (int i = 0; i < tab_Alto; i++)
            {
                for (int j = 0; j < tab_Ancho; j++)
                {

                }
            }
        }
        //es para iniciar la partida
        protected void Iniciar_Extream(object sender, EventArgs e)
        {
            tab_Alto = Int32.Parse(txtAltura.Text) + 2;
            tab_Ancho = Int32.Parse(txtAncho.Text) + 2;
            CrearTablero();
            if (FileUpload1.HasFile)
            {
                LeerXML_Click();
            }
            else
            {

            }
            Btn_Extream.Visible = false;
            Btn_normal.Visible = false;
            PanelFormulario.Visible = false;
            Btn_Reset.Visible = true;
            CrearXML.Visible = true;

            //iniciarPartida_PVP();
            imprimir_Tab();
            //PartidaPVP.Visible = false;
            //Partida_CPU.Visible = false;

        }
        protected void Restart(object sender, EventArgs e)
        {
        }


        protected void selectcolor(object sender, EventArgs e)
        {
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if (CheckBoxList1.Items[i].Selected)
                {
                    
                }
            }
        }


        protected void buttonmania_Click(object sender, EventArgs e)
        {
            string s = string.Empty;

            for (int i = 0; i < CheckBoxList1.Items.Count; i++)

            {

                if (CheckBoxList1.Items[i].Selected)
                {
                    s += CheckBoxList1.Items[i].Value + ";";
                    
                    CheckBoxList2.Items[i].Enabled = false;
                }

            }
        }

        protected void Btn_Extream_Click(object sender, EventArgs e)
        {
            PanelFormulario.Visible = true;
            Btn_Extream.Visible = false;
            Btn_normal.Visible = false;
        }

        protected void CerrarPanelForm_Click(object sender, EventArgs e)
        {
            PanelFormulario.Visible = false;
            Btn_Extream.Visible = true;
            Btn_normal.Visible = true;
        }
    }

}