using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;
using System.Drawing.Printing;
using System.Data.SqlClient;
using iTextSharp.text.pdf;

namespace EtiquetasSantaCatalina1._0
{
    public partial class Form2 : MetroFramework.Forms.MetroForm
    {
        public static string numcode = "";
        //VARIABLE QUE CONTIENE LA CADENA DE CONEXION A SQL SERVER POR ODBC
        public static MySqlConnection conectar1;
        //VARIABLE QUE CONTIENE EL NOMBRE DE LA IMPRESORA A OCUPAR
        //public static string impresora = "Adobe PDF";
        public static string variedad, productor, lote, sql_masivo="";
        public static int ultimocodigo = 0;
        public static string provincia, comuna, region;
        public static string vaciado_cli_codigo = "", vaciado_csg = "", vaciado_productor = "", vaciado_lote = "", vaciado_proceso = "", vaciado_variedad = "", vaciado_cse = "";
        public static SqlDataReader reader;
        public static SqlCommand comando;
        public static string codigo_especie;
        private void metroTile1_Click(object sender, EventArgs e)
        {
            //sql_masivo = "";
            Imprimir_etiqueta();
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConexionDB a1 = new ConexionDB();
            //vaciado_csg = a1.ObtenerCSG_Vaciado();
            //a1.Cerrar();
            a1 = new ConexionDB();
            ProductorBox.Text = a1.ObtenerProductor_Vaciado();
            a1.Cerrar();
            a1 = new ConexionDB();
            loteBox.Text = a1.ObtenerNumeroLote_Vaciado();
            a1.Cerrar();
            a1 = new ConexionDB();
            nproceso_box.Text = a1.ObtenerNumeroProceso_Vaciado();
            a1.Cerrar();

            a1 = new ConexionDB();
            VariedadBox.Text = a1.ObtenerVariedad_Vaciado();
            a1.Cerrar();
            a1 = new ConexionDB();
            vaciado_cli_codigo = a1.ObtenerExportadora_vaciado();
            a1.Cerrar();
            //a1 = new ConexionDB();
            //vaciado_cse = a1.ObtenerNumeroCSE_Vaciado(vaciado_cli_codigo);
            //a1.Cerrar();
            ConexionDB getexp = new ConexionDB();
            ExportadorBox.Text = getexp.ObtenerExportadora(vaciado_cli_codigo);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            CargarVariedades();
            //CargarCalibres();
            CargarEmbajales();
            CargarProductores();
            CargarExportadores();
            //leer codigo_especie
            StreamReader lectura2 = new StreamReader("codigo_especie.txt");
            string lineax = "";
            while ((lineax = lectura2.ReadLine()) != null)            //recorro linea a linea el documento y lo almaceno en el array variables
            {                                                       //para ir guardando las variables ya que no todas tienen el mismo nombre
                codigo_especie = Convert.ToString(lineax);
            }
            lectura2.Close();
            //ultimo codigo guardado
            //StreamReader lectura2 = new StreamReader("ultimaetiq.txt");
            //string lineax = "";
            //while ((lineax = lectura2.ReadLine()) != null)            //recorro linea a linea el documento y lo almaceno en el array variables
            //{                                                       //para ir guardando las variables ya que no todas tienen el mismo nombre
            //    ultimocodigo = Convert.ToInt32(lineax);
            //}
            //lectura2.Close();
            //MessageBox.Show(ultimocodigo.ToString(), "info");
        }
        private void Print_PrintPage(object sender, PrintPageEventArgs e)
        {
            //campos necesarios armado de etiqueta
            string especie;
            string variedad = "";
            string calibre = "";
            string embalaje = "";
            string productor = ProductorBox.Text;
            string comuna = "";
            ConexionDB conee = new ConexionDB();
            string provincia = conee.ObtenerProvincia(productor);
            conee.Cerrar();
            string region = "";
            //string ggn = "00000000000";
            string lote_huerto = loteBox.Text;
            string proceso_n = nproceso_box.Text;
            int turno = 1;

            //ConexionDB cone2 = new ConexionDB();
            //string fda = cone2.ObtenerFDA(productor);
            //cone2.Cerrar();
            //string tipo_frio = "AR";
            especie = "APPLES";
            //if (saposalida.Equals("C1"))
            //{
            embalaje = EmbalajeBox.Text;
            turno = 1;
            variedad = VariedadBox.Text;
            calibre = CalibreBox.Text;
            //}
           
            //FUENTES A USAR
            Font prFont = new Font("Arial", 5, FontStyle.Bold);
            Font prFont2 = new Font("Arial", 4, FontStyle.Bold);
            Font desFont = new Font("Arial", 8, FontStyle.Bold);
            Font desFont2 = new Font("Arial", 6, FontStyle.Bold);

            ////genero numero codigo
            //numcode = "20201" + (ultimocodigo + 1).ToString();
            //StreamWriter escrito = File.CreateText("ultimaetiq.txt");
            //escrito.Write((ultimocodigo + 1).ToString());
            //escrito.Flush();
            //////Cerramos 
            //escrito.Close();
            //ultimocodigo = ultimocodigo + 1;
            //Random Generator = new Random();
            //int numerorand, minimo, maximo;
            //int anioean = DateTime.Now.Year;

            //numcode = anioean + "00000000";
            ////GENERO DIGITO VERIFICADOR ean13
            //int v1;
            //int v2;
            //int verificador;

            //v1 = Convert.ToInt32(Convert.ToString(numcode).Substring(1, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(3, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(5, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(7, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(9, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(11, 1));
            //v1 = v1 * 3;
            //v2 = Convert.ToInt32(Convert.ToString(numcode).Substring(0, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(2, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(4, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(6, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(8, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(10, 1));

            //double redondea;
            //double aux;
            //redondea = (v1 + v2) / 10;
            //aux = redondea;
            //redondea = Math.Round(redondea, 0);
            //if (redondea > aux)
            //{
            //    redondea -= 1;
            //}
            //redondea = redondea + 1;
            //redondea = redondea * 10;
            //verificador = Convert.ToInt32(redondea) - (v1 + v2);
            //if (verificador.Equals(10))
            //{
            //    verificador = 0;
            //}

            //numcode = numcode + Convert.ToString(verificador);
            //ConexionDB versiexite = new ConexionDB();
            //while (versiexite.Existe_codigo(numcode))
            //{
            //    minimo = 10000000;
            //    maximo = 99999999;
            //    numerorand = Generator.Next(minimo, maximo + 1);
            //    numcode = anioean + Convert.ToString(numerorand);
            //    //GENERO DIGITO VERIFICADOR ean13

            //    v1 = Convert.ToInt32(Convert.ToString(numcode).Substring(1, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(3, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(5, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(7, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(9, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(11, 1));
            //    v1 = v1 * 3;
            //    v2 = Convert.ToInt32(Convert.ToString(numcode).Substring(0, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(2, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(4, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(6, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(8, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(10, 1));

            //    redondea = (v1 + v2) / 10;
            //    aux = redondea;
            //    redondea = Math.Round(redondea, 0);
            //    if (redondea > aux)
            //    {
            //        redondea -= 1;
            //    }
            //    redondea = redondea + 1;
            //    redondea = redondea * 10;
            //    verificador = Convert.ToInt32(redondea) - (v1 + v2);
            //    if (verificador.Equals(10))
            //    {
            //        verificador = 0;
            //    }

            //    numcode = numcode + Convert.ToString(verificador);
            //}
            //versiexite.Cerrar();
            //Barcode128 bcode = new Barcode128
            //{
            //    BarHeight = 60,
            //    Code = numcode,
            //    GenerateChecksum = true,
            //    CodeType = Barcode.CODE128
            //};
            //Barcode128 bcode2 = new Barcode128
            //{
            //    BarHeight = 35,
            //    Code = numcode,
            //    GenerateChecksum = true,
            //    CodeType = Barcode.CODE128
            //};
            try
            {
                //Image img;
                //img = bcode2.CreateDrawingImage(Color.Black, Color.White);
                //img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                //Image img2;
                //img2 = bcode.CreateDrawingImage(Color.Black, Color.White);
                int yPos = 6;

                Image logo;
                logo = Image.FromFile("etiq_santacatalina.jpg");
                //e.Graphics.DrawImage(logo, 265, yPos + 135, 106, 44);
                e.Graphics.DrawImage(logo, 0, yPos + 120, 106, 44);
                variedad = "red sensation";
                e.Graphics.DrawString(especie.ToUpper() + "-" + variedad.ToUpper(), new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 40, yPos + 0);


                //e.Graphics.DrawString("AR", desFont2, Brushes.Black, 196, yPos + 0);
                Font prFont3 = new Font("Arial", 8, FontStyle.Bold);
                e.Graphics.DrawString("CSG:", desFont, Brushes.Black, 20, yPos + 24);
                e.Graphics.DrawString("GROWER / PRODUCTOR", new Font("Arial", 3, FontStyle.Bold), Brushes.Black, 11, yPos + 38);
                e.Graphics.DrawString("TOWNSHIP:", prFont3, Brushes.Black, 80, yPos + 26);
                e.Graphics.DrawString("PROVINCE:", prFont3, Brushes.Black, 80, yPos + 40);
                //e.Graphics.DrawString("GGN:", prFont, Brushes.Black, 0, yPos + 60);
                //ConexionDB cone = new ConexionDB();
                ConexionDB obtcsg = new ConexionDB();
                e.Graphics.DrawString(obtcsg.ObtenerCSG(productor), new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 0, yPos + 42);
                obtcsg.Cerrar();
                //cone.Cerrar();
                //e.Graphics.DrawString(productor.ToUpper(), prFont, Brushes.Black, 70, yPos + 40);
                ConexionDB cone3 = new ConexionDB();
                comuna = cone3.ObtenerComuna(productor);
                e.Graphics.DrawString(comuna.ToUpper(), prFont3, Brushes.Black, 145, yPos + 26);
                cone3.Cerrar();
                ConexionDB conex3 = new ConexionDB();
                provincia = conex3.ObtenerProvincia(productor);
                e.Graphics.DrawString(provincia.ToUpper(), prFont3, Brushes.Black, 145, yPos + 40);
                conex3.Cerrar();
                //e.Graphics.DrawString("110987", desFont, Brushes.Black, 70, yPos + 26);                            //provisorio
                //e.Graphics.DrawString("SOCIEDAD PACKING SERVICE INGENIERIA LTDA.", prFont, Brushes.Black, 70, yPos + 40);   //provisorio
                //e.Graphics.DrawString("TENO - CURICO", prFont, Brushes.Black, 70, yPos + 50);                      //provisorio
                //e.Graphics.DrawString(ggn, prFont, Brushes.Black, 70, yPos + 60);
                ConexionDB cone5 = new ConexionDB();
                e.Graphics.DrawString("REGION: ", prFont3, Brushes.Black, 80, yPos + 54);
                region = cone5.ObtenerRegion(productor);
                e.Graphics.DrawString(region, prFont3, Brushes.Black, 145, yPos + 54);
                cone5.Cerrar();


                e.Graphics.DrawString("NET WEIGHT W.P.", prFont, Brushes.Black, 300, yPos + 5);
                string kilos = "5,0";
                if (embalaje.Contains("25K"))
                {
                    kilos = "2,5";
                }
                if (embalaje.Contains("9K"))
                {
                    kilos = "9";
                }
                if (embalaje.Contains("10K"))
                {
                    kilos = "10";
                }
                if (embalaje.Contains("18K"))
                {
                    kilos = "18";
                }
                if (embalaje.Contains("19K"))
                {
                    kilos = "19";
                }
                e.Graphics.DrawString(embalaje.TrimEnd().TrimStart() + " - " + kilos + " Kg.", prFont, Brushes.Black, 300, yPos + 12);
                int diasemana;
                System.Globalization.CultureInfo norwCulture = System.Globalization.CultureInfo.CreateSpecificCulture("es");
                System.Globalization.Calendar cal = norwCulture.Calendar;
                diasemana = cal.GetWeekOfYear(DateTime.Now, norwCulture.DateTimeFormat.CalendarWeekRule, norwCulture.DateTimeFormat.FirstDayOfWeek);
                e.Graphics.DrawString("SEM: ", prFont, Brushes.Black, 300, yPos + 23);
                e.Graphics.DrawString(diasemana.ToString(), prFont, Brushes.Black, 333, yPos + 23);
                //e.Graphics.DrawString("TIME: ", prFont, Brushes.Black, 300, yPos + 30);
                
                //e.Graphics.DrawString(DateTime.Now.ToString("HH:mm:ss"), prFont, Brushes.Black, 333, yPos + 30);
                e.Graphics.DrawString("N° PROCESO:", new Font("Arial", 7, FontStyle.Bold), Brushes.Black, 298, yPos + 40);
                if (proceso_n.Length.Equals(1)) { e.Graphics.DrawString(proceso_n, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 328, yPos + 53); }
                if (proceso_n.Length.Equals(2)) { e.Graphics.DrawString(proceso_n, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 323, yPos + 53); }
                if (proceso_n.Length.Equals(3)) { e.Graphics.DrawString(proceso_n, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 318, yPos + 53); }
                if (proceso_n.Length.Equals(4)) { e.Graphics.DrawString(proceso_n, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 315, yPos + 53); }


                //=================
                //if (calibre.Contains("D")) { e.Graphics.DrawString("DARK", new Font("Arial Black", 14, FontStyle.Bold), Brushes.Black, 200, yPos + 56); }
                //else
                //{
                //    //if (calibre.Contains("TC")) { e.Graphics.DrawString(" ", new Font("Arial Black", 12, FontStyle.Bold), Brushes.Black, 200, yPos + 56); }
                //    e.Graphics.DrawString("LIGHT", new Font("Arial Black", 13, FontStyle.Bold), Brushes.Black, 200, yPos + 56);
                //}

                e.Graphics.DrawString("DATE: ", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 160, yPos + 130);
                //e.Graphics.DrawString("LOTE: ", prFont, Brushes.Black, 222, yPos + 57);
                //e.Graphics.DrawString("PROC N°: ", prFont, Brushes.Black, 222, yPos + 57);
                e.Graphics.DrawString(FechaBox.Value.ToString("dd-MM-yyyy"), new Font("Arial", 12, FontStyle.Bold), Brushes.Black, 135, yPos + 142);
                //e.Graphics.DrawString(vaciado_lote, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 255, yPos + 51);
                //e.Graphics.DrawString(proceso_n, prFont, Brushes.Black, 255, yPos + 57);


                Font desFont3 = new Font("Verdana", 22, FontStyle.Bold);
                Font desFont4 = new Font("Verdana", 14, FontStyle.Bold);
                //int exis = 150;
                //OBTENGO SIGLA CALIBRE
                //Cargar_siglaymm(calidad);
                //////////Sigla_clasificacion = "LL";
                //////////Calibre_mm = "< 23,9 mm";
                //if (Sigla_clasificacion.Length.Equals(2))
                //{
                //    e.Graphics.DrawString(Sigla_clasificacion, desFont3, Brushes.Black, exis + 2, yPos + 17);
                //}
                //else if (Sigla_clasificacion.Length.Equals(3))
                //{
                //    e.Graphics.DrawString(Sigla_clasificacion, desFont4, Brushes.Black, exis + 6, yPos + 17);
                //}
                //else if (Sigla_clasificacion.Length.Equals(4))
                //{
                //    e.Graphics.DrawString(Sigla_clasificacion, desFont3, Brushes.Black, exis - 4, yPos + 17);
                //}
                //else if (Sigla_clasificacion.Length.Equals(1))
                //{
                //    e.Graphics.DrawString(Sigla_clasificacion, desFont3, Brushes.Black, exis + 12, yPos + 17);
                //}
                //if (calibre.Replace("D", "").Length.Equals(4))
                //{
                //    e.Graphics.DrawString(calibre.Replace("D", ""), new Font("Arial Black", 24, FontStyle.Bold), Brushes.Black, exis + 24, yPos + 5);
                //}
                //if (calibre.Replace("D", "").Length.Equals(2))
                //{
                //    e.Graphics.DrawString(calibre.Replace("D", ""), new Font("Arial Black", 35, FontStyle.Bold), Brushes.Black, exis + 43, yPos - 3);
                //}
                //else if (calibre.Replace("D", "").Length.Equals(3))
                //{
                //    e.Graphics.DrawString(calibre.Replace("D", ""), desFont4, Brushes.Black, exis + 8, yPos - 3);
                //}
                //else if (calibre.Replace("D", "").Length.Equals(4))
                //{
                //    e.Graphics.DrawString(calibre.Replace("D", ""), desFont3, Brushes.Black, exis - 4, yPos - 3);
                //}
                //else if (calibre.Replace("D", "").Length.Equals(1))
                //{
                //    e.Graphics.DrawString(calibre.Replace("D", ""), new Font("Arial Black", 35, FontStyle.Bold), Brushes.Black, exis + 60, yPos - 3);
                //}

                e.Graphics.DrawString("CSP:", desFont, Brushes.Black, 20, yPos + 72);
                e.Graphics.DrawString("PACKING BY / EMPACADORA", new Font("Arial", 3, FontStyle.Bold), Brushes.Black, 7, yPos + 86);
                e.Graphics.DrawString("176227", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 0, yPos + 90);
                e.Graphics.DrawString("TOWNSHIP:", prFont3, Brushes.Black, 80, yPos + 74);
                //e.Graphics.DrawString("SOCIEDAD PACKING SERVICE INGENIERIA", prFont, Brushes.Black, 70, yPos + 86);
                e.Graphics.DrawString("PROVINCE:", prFont3, Brushes.Black, 80, yPos + 88);
                e.Graphics.DrawString("REGION: ", prFont3, Brushes.Black, 80, yPos + 102);
                e.Graphics.DrawString("CURICO", prFont3, Brushes.Black, 145, yPos + 74);
                e.Graphics.DrawString("CURICO", prFont3, Brushes.Black, 145, yPos + 88);
                e.Graphics.DrawString("VII", prFont3, Brushes.Black, 145, yPos + 102);
                //e.Graphics.DrawString(provincia.ToUpper()+" - "+comuna.ToUpper(), prFont, Brushes.Black, 70, yPos + 96);
                //e.Graphics.DrawString("FDA:", prFont, Brushes.Black, 0, yPos + 106);
                //e.Graphics.DrawString(fda, prFont, Brushes.Black, 70, yPos + 106);

                //e.Graphics.DrawString("CAT-1", new Font("Arial", 19, FontStyle.Bold), Brushes.Black, 15, yPos + 135);
                e.Graphics.DrawString("CLASS 1 PRODUCE OF CHILE", prFont, Brushes.Black, 5, yPos + 170);
                //e.Graphics.DrawString("EXPORTED BY: ______", prFont2, Brushes.Black, 182, yPos + 136);
                //e.Graphics.DrawString("RUT: xx.xxx.xxx-x     CSE: xxxxx", prFont2, Brushes.Black, 212, yPos + 145);
                //e.Graphics.DrawString("FUNDO xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", prFont2, Brushes.Black, 180, yPos + 154);
                //
                //if (calibre.Equals("TC")) { e.Graphics.DrawString("COMERCIAL", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 220, yPos + 85); }
                //else { e.Graphics.DrawString("CAT-1", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 235, yPos + 85); }
                e.Graphics.DrawString("EXPORTED BY:", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 245, yPos + 120);
                ConexionDB getexp = new ConexionDB();
                string exp_abreviada = ExportadorBox.Text;
                exp_abreviada = exp_abreviada.Replace("EXPORTADORA", "EXP.");
                exp_abreviada = exp_abreviada.Replace("EXPORTADOR", "EXP.");
                exp_abreviada = exp_abreviada.Replace("COMERCIAL", "COM.");
                exp_abreviada = exp_abreviada.Replace("COMERCIALIZADORA", "COM.");
                exp_abreviada = exp_abreviada.Replace("PRODUCTOR", "PROD.");
                e.Graphics.DrawString(exp_abreviada, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 245, yPos + 140);
                getexp.Cerrar();
                e.Graphics.DrawString("CSE:", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 245, yPos + 160);
                ConexionDB obtCSE = new ConexionDB();
                e.Graphics.DrawString(obtCSE.ObtenerNumeroCSE(ExportadorBox.Text), new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 285, yPos + 160);
                obtCSE.Cerrar();
                //e.Graphics.DrawString("TURNO:", prFont2, Brushes.Black, 302, yPos + 74);
                //e.Graphics.DrawString(turno.ToString(), prFont2, Brushes.Black, 332, yPos + 74);
                //e.Graphics.DrawString("LINEA:", prFont2, Brushes.Black, 303, yPos + 85);
                //e.Graphics.DrawString("1", prFont2, Brushes.Black, 332, yPos + 85);
                e.Graphics.DrawString("SALIDA", new Font("Arial", 5, FontStyle.Bold), Brushes.Black, 317, yPos + 74);
                Random rnd2 = new Random();
                int salida = rnd2.Next(1, 28);
                int impre = 1;
                if (salida.Equals(1) || salida.Equals(2)) { impre = 1; }
                if (salida.Equals(3) || salida.Equals(4)) { impre = 2; }
                if (salida.Equals(5) || salida.Equals(6)) { impre = 3; }
                if (salida.Equals(7) || salida.Equals(8)) { impre = 4; }
                if (salida.Equals(9) || salida.Equals(10)) { impre = 5; }
                if (salida.Equals(11) || salida.Equals(12)) { impre = 6; }
                if (salida.Equals(13) || salida.Equals(14)) { impre = 7; }
                if (salida.Equals(15) || salida.Equals(16)) { impre = 8; }

                if (salida.Equals(17) || salida.Equals(18)) { impre = 9; }
                if (salida.Equals(19) || salida.Equals(20)) { impre = 10; }
                if (salida.Equals(21) || salida.Equals(22)) { impre = 11; }
                if (salida.Equals(23) || salida.Equals(24)) { impre = 12; }
                if (salida.Equals(25) || salida.Equals(26)) { impre = 13; }
                if (salida.Equals(27) || salida.Equals(28)) { impre = 14; }
                
                if (salida > 9) { e.Graphics.DrawString(salida.ToString(), new Font("Arial", 6, FontStyle.Bold), Brushes.Black, 325, yPos + 82); }
                else { e.Graphics.DrawString(salida.ToString(), new Font("Arial", 6, FontStyle.Bold), Brushes.Black, 327, yPos + 82); }
                

                e.Graphics.DrawString("IMPRESORA", new Font("Arial", 5, FontStyle.Bold), Brushes.Black, 310, yPos + 96);

                if (impre > 9) { e.Graphics.DrawString(impre.ToString(), new Font("Arial", 6, FontStyle.Bold), Brushes.Black, 325, yPos + 104); }
                else { e.Graphics.DrawString(impre.ToString(), new Font("Arial", 6, FontStyle.Bold), Brushes.Black, 329, yPos + 104); }
                //e.Graphics.DrawString("SALIDA:", prFont2, Brushes.Black, 302, yPos + 96);
                //e.Graphics.DrawString(saposalida, prFont2, Brushes.Black, 332, yPos + 96);
                //e.Graphics.DrawString("IMP:", prFont2, Brushes.Black, 302, yPos + 107);
                //if(impresora.Length > 10) { e.Graphics.DrawString(impresora.Substring(9, 2), prFont2, Brushes.Black, 332, yPos + 107); }
                //else { e.Graphics.DrawString(impresora.Substring(9, 1), prFont2, Brushes.Black, 332, yPos + 107); }

                //'codigo vertical     CODE-128
                //e.Graphics.DrawImage(img, 343, yPos + 15);

                //'codigo horizontal   EAN13
                //e.Graphics.DrawImage(img2, 115, yPos + 118);

                Font prFontx = new Font("Arial", 6, FontStyle.Bold);

                //SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
                //e.Graphics.FillRectangle(myBrush, new Rectangle(153, yPos + 172, 54, 10));
                //e.Graphics.DrawString(numcode, prFont, Brushes.Black, 153, yPos + 173);

                //'GUARDO LA ETIQUETA EN LA BASE DE DATOS

                string cla = "";
                //if (calibre.Equals("TC"))
                //{
                //    cla = "COM";
                //}
                //else
                //{
                //    if (calibre.Contains("D")) { cla = "DARK"; }
                //    else { cla = "LIGHT"; }
                //}
                //if (calibre.Equals("L")) { cla = "LIGHT"; }
                //if (calibre.Equals("LD")) { cla = "DARK"; }
                //if (calibre.Equals("XL")) { cla = "LIGHT"; }
                //if (calibre.Equals("XLD")) { cla = "DARK"; }
                //if (calibre.Equals("J")) { cla = "LIGHT"; }
                //if (calibre.Equals("JD")) { cla = "DARK"; }
                //if (calibre.Equals("SJ")) { cla = "LIGHT"; }
                //if (calibre.Equals("SJD")) { cla = "DARK"; }
                //if (calibre.Equals("P")) { cla = "LIGHT"; }
                //if (calibre.Equals("PD")) { cla = "DARK"; }
                //if (calibre.Equals("SP")) { cla = "LIGHT"; }
                //if (calibre.Equals("SPD")) { cla = "DARK"; }
                //if (calibre.Equals("SG")) { cla = "LIGHT"; }
                //if (calibre.Equals("SGD")) { cla = "DARK"; }

                //sql_masivo += "INSERT INTO etiquetado_system.dbo.etiqueta(codigo_interno, codigo_ean13, especie, variedad, clasificacion, calibre, embalaje, productor, categoria, lote_huerto, proceso_n, cuartel, salida, tipo_frio, turno, fecha, fda, ggn, paso1, codigo_tarja, cuadrilla) "
                //    + "VALUES('" + numcode + "', '" + numcode + "', 'APPLES', '" + variedad.ToUpper() + "', '" + cla + "', '" + calibre.Replace("D", "").ToUpper() + "', '" + embalaje.ToUpper() + "', '" + productor + "', 'CAT-1', '" + lote_huerto + "', '" + proceso_n + "', NULL, 'Salida " + salida + "', 'AR', 1, '" + FechaBox.Value.ToString("yyyy-MM-dd") + "', '00', '00', 0, NULL, NULL);";

                //ConexionDB getiq = new ConexionDB();
                //getiq.GuardarEtiqueta(numcode, "CHERRIES", variedad.ToUpper(), cla, calibre.Replace("D", "").ToUpper(), embalaje.ToUpper(), productor, "CAT-1", lote_huerto, proceso_n, "Salida " + salidaa, "AR", turno, FechaBox.Value.ToString("yyyy-MM-dd"), "00", "00");
                //getiq.Cerrar();
                //'IMPRIMO LINEAS DEL DISEÑO
                Pen blackPen1 = new Pen(Color.Black, 1);
                Pen blackPen2 = new Pen(Color.Black, 2);
                //'horizontales
                e.Graphics.DrawLine(blackPen2, new Point(295, yPos + 22), new Point(370, yPos + 22));
                e.Graphics.DrawLine(blackPen2, new Point(295, yPos + 70), new Point(370, yPos + 70));
                e.Graphics.DrawLine(blackPen2, new Point(0, yPos + 70), new Point(295, yPos + 70));
                e.Graphics.DrawLine(blackPen2, new Point(0, yPos + 115), new Point(370, yPos + 115));

                //'verticales
                //e.Graphics.DrawLine(blackPen2, new Point(160, yPos + 116), new Point(160, yPos + 22));
                e.Graphics.DrawLine(blackPen2, new Point(295, yPos + 116), new Point(295, yPos + 0));
                e.Graphics.DrawLine(blackPen2, new Point(370, yPos + 116), new Point(370, yPos + 0));
            }
            catch (Exception ex)
            {
                MetroFramework.MetroMessageBox.Show(this, "Hubo un error en generar el codigo de barra. " + ex.ToString(), "INFO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //' indicamos que ya no hay nada más que imprimir
            //' (el valor predeterminado de esta propiedad es False)
            e.HasMorePages = false;
        }
        public void Imprimir_etiqueta()
        {
            PrinterSettings prtSettings = new PrinterSettings();
            prtSettings.DefaultPageSettings.Landscape = false;
            prtSettings.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Custom", 384, 192);  // 100x50 mm
            //prtSettings.PrinterName = "IMPRESORA1";
            //prtSettings.PrinterName = impresora;
            //prtSettings.PrinterName = "Adobe PDF"; 
            PrintDocument prtDoc = new System.Drawing.Printing.PrintDocument();
            prtDoc.PrintPage += new PrintPageEventHandler(Print_PrintPage);
            //la configuración a usar en la impresión
            prtDoc.PrinterSettings = prtSettings;
            int i = 0;
            int copias = Convert.ToInt32(copiasBox.Text);
            while (i < copias)
            {
                System.Threading.Thread.Sleep(50);
                prtDoc.Print();
                //System.Threading.Thread.Sleep(200);
                i++;
            }
            //ConexionDB masiva = new ConexionDB();
            //masiva.GuardarMasivo(sql_masivo);
        }
        private void CargarEmbajales()
        {
            ConexionDB nu = new ConexionDB();

            string[] lista = new string[50];
            try
            {
                EmbalajeBox.Items.Clear();
                lista = nu.CargaEmbalajes();
                int i = 0;
                try
                {
                    while (lista[i].Length > 0)
                    {
                        EmbalajeBox.Items.Add(lista[i]);
                        i = i + 1;
                    }
                }
                catch (Exception) { }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

            nu.Cerrar();
        }

        private void ProductorBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string proceso = nproceso_box.Text;
            try
            {
                ConexionDB conector = new ConexionDB();
                comando = new SqlCommand("SELECT TOP 1 SFruticola_SPSI.dba.variedades.vari_nombre, SFruticola_SPSI.dba.clientes.cli_nombre, SFruticola_SPSI.dba.productores.prod_nombre, convert(nvarchar(6),SFruticola_SPSI.dba.TRA.nlote) AS Lote, SFruticola_SPSI.dba.TRA.proceso FROM SFruticola_SPSI.dba.TRA, SFruticola_SPSI.dba.variedades, SFruticola_SPSI.dba.clientes, SFruticola_SPSI.dba.productores WHERE SFruticola_SPSI.dba.variedades.vari_codigo = SFruticola_SPSI.dba.TRA.vari_codigo AND SFruticola_SPSI.dba.clientes.cli_codigo = SFruticola_SPSI.dba.TRA.cli_codigo AND SFruticola_SPSI.dba.productores.prod_codigo = SFruticola_SPSI.dba.TRA.prod_codigo AND SFruticola_SPSI.dba.TRA.proceso = '" + proceso+ "' ORDER BY SFruticola_SPSI.dba.TRA.FechaDigi DESC", ConexionDB.cn2);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    VariedadBox.Text = reader.GetString(0);
                    ExportadorBox.Text = reader.GetString(1);
                    ProductorBox.Text = reader.GetString(2);
                    loteBox.Text = reader.GetString(3);
                    nproceso_box.Text = reader.GetString(4);
                }
                conector.Cerrar();
            }
            catch (Exception ex)
            {
                /// 
            }
        }

        private void CargarCalibres()
        {
            ConexionDB nu = new ConexionDB();

            string[] lista = new string[50];
            try
            {
                CalibreBox.Items.Clear();
                lista = nu.CargaCalibres();
                int i = 0;
                try
                {
                    while (lista[i].Length > 0)
                    {
                        CalibreBox.Items.Add(lista[i]);
                        i = i + 1;
                    }
                }
                catch (Exception) { }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

            nu.Cerrar();
        }
        private void CargarVariedades()
        {
            ConexionDB nu = new ConexionDB();

            string[] lista = new string[50];
            try
            {
                VariedadBox.Items.Clear();
                lista = nu.CargaVariedades();
                int i = 0;
                try
                {
                    while (lista[i].Length > 0)
                    {
                        VariedadBox.Items.Add(lista[i]);
                        i = i + 1;
                    }
                }
                catch (Exception) { }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

            nu.Cerrar();
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            string set_variedad, set_calibre, set_embalaje, set_productor, set_exportador;
            set_variedad = VariedadBox.Text;
            set_embalaje = EmbalajeBox.Text;
            set_productor = ProductorBox.Text;
            set_exportador = ExportadorBox.Text;
            set_calibre = CalibreBox.Text;
            CargarVariedades();
            CargarCalibres();
            CargarEmbajales();
            CargarProductores();
            CargarExportadores();
            VariedadBox.Text = set_variedad;
            EmbalajeBox.Text = set_embalaje;
            ProductorBox.Text = set_productor;
            ExportadorBox.Text = set_exportador;
            CalibreBox.Text = set_calibre;
            StreamReader lectura2 = new StreamReader("codigo_especie.txt");
            string lineax = "";
            while ((lineax = lectura2.ReadLine()) != null)            //recorro linea a linea el documento y lo almaceno en el array variables
            {                                                       //para ir guardando las variables ya que no todas tienen el mismo nombre
                codigo_especie = Convert.ToString(lineax);
            }
            lectura2.Close();
        }

        private void CargarExportadores()
        {
            ConexionDB nu = new ConexionDB();

            string[] lista = new string[50];
            try
            {
                ExportadorBox.Items.Clear();
                lista = nu.CargaExportadores();
                int i = 0;
                try
                {
                    while (lista[i].Length > 0)
                    {
                        ExportadorBox.Items.Add(lista[i]);
                        i = i + 1;
                    }
                }
                catch (Exception) { }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

            nu.Cerrar();
        }
        private void CargarProductores()
        {
            ConexionDB nu = new ConexionDB();

            string[] lista = new string[50];
            try
            {
                ProductorBox.Items.Clear();
                lista = nu.CargaProductores();
                int i = 0;
                try
                {
                    while (lista[i].Length > 0)
                    {
                        ProductorBox.Items.Add(lista[i]);
                        i = i + 1;
                    }
                }
                catch (Exception) { }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

            nu.Cerrar();
        }
    }
}
