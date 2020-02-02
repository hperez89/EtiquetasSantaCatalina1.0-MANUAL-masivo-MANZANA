using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Drawing.Printing;
using iTextSharp.text.pdf;

namespace EtiquetasSantaCatalina1._0
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public static int retardo_seg = 10;
        //VARIABLE QUE CONTIENE LA CADENA DE CONEXION A SQL SERVER POR ODBC
        public static MySqlConnection conectar1;
        //VARIABLE QUE CONTIENE EL NOMBRE DE LA IMPRESORA A OCUPAR
        public static string impresora = "Adobe PDF";
        public static string variedad, productor, lote;
        public static Boolean led1, led2, led3, led4, led5, led6, led7, led8, led9, led10, led11, led12, led14, led15, led16, led17, led18, led19, led20, led21, led22, led23, led24;
        public static string ipimp1 = "", ipimp2 = "", ipimp3 = "", ipimp4 = "", ipimp5 = "", ipimp6 = "", ipimp7 = "", 
                            ipimp8 = "", ipimp9 = "", ipimp10 = "", ipimp11 = "", ipimp12 = "", ipimp13 = "", ipimp14 = "";
        public static string calibre1, calibre2, calibre3, calibre4, calibre5, calibre6, calibre7, calibre8, calibre9, calibre10, calibre11, calibre12, calibre13, calibre14;
        public static int contadorS1 = 0, contadorS2 = 0, contadorS3 = 0, contadorS4 = 0;
        public static int contadorS5 = 0, contadorS6 = 0, contadorS7 = 0, contadorS8 = 0;
        public static int contadorS9 = 0, contadorS10 = 0, contadorS11 = 0, contadorS12 = 0;
        public static int contadorS13 = 0, contadorS14 = 0, contadorS15 = 0, contadorS16 = 0;
        public static int contadorS17 = 0, contadorS18 = 0, contadorS19 = 0, contadorS20 = 0;
        public static int contadorS21 = 0, contadorS22 = 0, contadorS23 = 0, contadorS24 = 0;
        public static int contadorS25 = 0, contadorS26 = 0, contadorSC1 = 0, contadorSC2 = 0;
        //variables que contienen la ultima hora de impresion de cada salida
        public static DateTime hora_sC1, hora_sC2, hora_s1, hora_s2, hora_s3, hora_s4, hora_s5, hora_s6, hora_s7, hora_s8, hora_s9, hora_s10, hora_s11, hora_s12, hora_s13, hora_s14, hora_s15, hora_s16;
        public static DateTime hora_s17, hora_s18, hora_s19, hora_s20, hora_s21, hora_s22, hora_s23, hora_s24, hora_s25, hora_s26;
        public static string saposalida;
        public static Boolean[] statusImp = new Boolean[16];
        //VARIABLE QUE SE OCUPA DE ALMACENAR EL CODIGO EAN-13
        public static string numcode = "";
        public static string vaciado_csg = "", vaciado_productor = "", vaciado_lote = "", vaciado_proceso = "", vaciado_variedad = "", vaciado_cse = "";
        public Form1()
        {
            InitializeComponent();
        }
        private void Timer_vaciado_Tick(object sender, EventArgs e)
        {
            try
            {
                ConexionDB a1 = new ConexionDB();
                vaciado_csg = a1.ObtenerCSG_Vaciado();
                a1.Cerrar();
                a1 = new ConexionDB();
                vaciado_productor = a1.ObtenerProductor_Vaciado();
                a1.Cerrar();
                a1 = new ConexionDB();
                vaciado_lote = a1.ObtenerNumeroLote_Vaciado();
                a1.Cerrar();
                a1 = new ConexionDB();
                vaciado_proceso = a1.ObtenerNumeroProceso_Vaciado();
                a1.Cerrar();
                a1 = new ConexionDB();
                vaciado_cse = a1.ObtenerNumeroCSE_Vaciado();
                a1.Cerrar();
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error al obtener datos de vaciado");
            }
            TxtProductorVaciado.Text = vaciado_productor;
            TxtProcesoVaciado.Text = vaciado_proceso;
            TxtLoteVaciado.Text = vaciado_lote;
        }

        private void BtnImp1_Click(object sender, EventArgs e)
        {
            int diferencia = 0;
            if (saposalida.Equals("C1"))
            {
                if (hora_sC1.Year > 1900)
                {
                    if (hora_sC1.Hour < DateTime.Now.Hour)
                    {
                        diferencia += (DateTime.Now.Hour - hora_sC1.Hour) * 3600;
                    }
                    if (hora_sC1.Hour.Equals(DateTime.Now.Hour))
                    {
                        if (hora_sC1.Minute < DateTime.Now.Minute)
                        {
                            if ((DateTime.Now.Minute - hora_sC1.Minute).Equals(1))
                            {
                                diferencia += (60 - hora_sC1.Second) + DateTime.Now.Second;
                            }
                            else
                            {
                                diferencia += (DateTime.Now.Minute - hora_sC1.Minute) * 60;
                                if (DateTime.Now.Second > hora_sC1.Second)
                                {
                                    diferencia += DateTime.Now.Second - hora_sC1.Second;
                                }
                                else
                                {
                                    diferencia += DateTime.Now.Second + (60 - hora_sC1.Second);
                                }
                            }
                        }
                        if (hora_sC1.Minute.Equals(DateTime.Now.Minute))
                        {
                            diferencia += DateTime.Now.Second - hora_sC1.Second;
                        }
                    }
                }
                else
                {
                    diferencia = 9999999;
                }
                if (diferencia > retardo_seg)
                {
                    hora_sC1 = DateTime.Now;
                    Imprimir_etiqueta();
                }
            }
            if (saposalida.Equals("C2"))
            {
                if (hora_sC2.Year > 1900)
                {
                    if (hora_sC2.Hour < DateTime.Now.Hour)
                    {
                        diferencia += (DateTime.Now.Hour - hora_sC2.Hour) * 3600;
                    }
                    if (hora_sC2.Hour.Equals(DateTime.Now.Hour))
                    {
                        if (hora_sC2.Minute < DateTime.Now.Minute)
                        {
                            if ((DateTime.Now.Minute - hora_sC2.Minute).Equals(1))
                            {
                                diferencia += (60 - hora_sC2.Second) + DateTime.Now.Second;
                            }
                            else
                            {
                                diferencia += (DateTime.Now.Minute - hora_sC2.Minute) * 60;
                                if (DateTime.Now.Second > hora_sC2.Second)
                                {
                                    diferencia += DateTime.Now.Second - hora_sC2.Second;
                                }
                                else
                                {
                                    diferencia += DateTime.Now.Second + (60 - hora_sC2.Second);
                                }
                            }
                        }
                        if (hora_sC2.Minute.Equals(DateTime.Now.Minute))
                        {
                            diferencia += DateTime.Now.Second - hora_sC2.Second;
                        }
                    }
                }
                else
                {
                    diferencia = 9999999;
                }
                if (diferencia > retardo_seg)
                {
                    hora_sC2 = DateTime.Now;
                    Imprimir_etiqueta();
                }
            }
            if (saposalida.Equals("1"))
            {
                if (hora_s1.Year > 1900)
                {
                    if (hora_s1.Hour < DateTime.Now.Hour)
                    {
                        diferencia += (DateTime.Now.Hour - hora_s1.Hour) * 3600;
                    }
                    if (hora_s1.Hour.Equals(DateTime.Now.Hour))
                    {
                        if (hora_s1.Minute < DateTime.Now.Minute)
                        {
                            if ((DateTime.Now.Minute - hora_s1.Minute).Equals(1))
                            {
                                diferencia += (60 - hora_s1.Second) + DateTime.Now.Second;
                            }
                            else
                            {
                                diferencia += (DateTime.Now.Minute - hora_s1.Minute) * 60;
                                if (DateTime.Now.Second > hora_s1.Second)
                                {
                                    diferencia += DateTime.Now.Second - hora_s1.Second;
                                }
                                else
                                {
                                    diferencia += DateTime.Now.Second + (60 - hora_s1.Second);
                                }
                            }
                        }
                        if (hora_s1.Minute.Equals(DateTime.Now.Minute))
                        {
                            diferencia += DateTime.Now.Second - hora_s1.Second;
                        }
                    }
                }
                else
                {
                    diferencia = 9999999;
                }
                if (diferencia > retardo_seg)
                {
                    hora_s1 = DateTime.Now;
                    Imprimir_etiqueta();
                }
            }
            if (saposalida.Equals("2"))
            {
                if (hora_s2.Year > 1900)
                {
                    if (hora_s2.Hour < DateTime.Now.Hour)
                    {
                        diferencia += (DateTime.Now.Hour - hora_s2.Hour) * 3600;
                    }
                    if (hora_s2.Hour.Equals(DateTime.Now.Hour))
                    {
                        if (hora_s2.Minute < DateTime.Now.Minute)
                        {
                            if ((DateTime.Now.Minute - hora_s2.Minute).Equals(1))
                            {
                                diferencia += (60 - hora_s2.Second) + DateTime.Now.Second;
                            }
                            else
                            {
                                diferencia += (DateTime.Now.Minute - hora_s2.Minute) * 60;
                                if (DateTime.Now.Second > hora_s2.Second)
                                {
                                    diferencia += DateTime.Now.Second - hora_s2.Second;
                                }
                                else
                                {
                                    diferencia += DateTime.Now.Second + (60 - hora_s2.Second);
                                }
                            }
                        }
                        if (hora_s2.Minute.Equals(DateTime.Now.Minute))
                        {
                            diferencia += DateTime.Now.Second - hora_s2.Second;
                        }
                    }
                }
                else
                {
                    diferencia = 9999999;
                }
                if (diferencia > retardo_seg)
                {
                    hora_s2 = DateTime.Now;
                    Imprimir_etiqueta();
                }
            }
            if (saposalida.Equals("3"))
            {
                if (hora_s3.Year > 1900)
                {
                    if (hora_s3.Hour < DateTime.Now.Hour)
                    {
                        diferencia += (DateTime.Now.Hour - hora_s3.Hour) * 3600;
                    }
                    if (hora_s3.Hour.Equals(DateTime.Now.Hour))
                    {
                        if (hora_s3.Minute < DateTime.Now.Minute)
                        {
                            if ((DateTime.Now.Minute - hora_s3.Minute).Equals(1))
                            {
                                diferencia += (60 - hora_s3.Second) + DateTime.Now.Second;
                            }
                            else
                            {
                                diferencia += (DateTime.Now.Minute - hora_s3.Minute) * 60;
                                if (DateTime.Now.Second > hora_s3.Second)
                                {
                                    diferencia += DateTime.Now.Second - hora_s3.Second;
                                }
                                else
                                {
                                    diferencia += DateTime.Now.Second + (60 - hora_s3.Second);
                                }
                            }
                        }
                        if (hora_s3.Minute.Equals(DateTime.Now.Minute))
                        {
                            diferencia += DateTime.Now.Second - hora_s3.Second;
                        }
                    }
                }
                else
                {
                    diferencia = 9999999;
                }
                if (diferencia > retardo_seg)
                {
                    hora_s3 = DateTime.Now;
                    Imprimir_etiqueta();
                }
            }
            if (saposalida.Equals("4"))
            {
                if (hora_s4.Year > 1900)
                {
                    if (hora_s4.Hour < DateTime.Now.Hour)
                    {
                        diferencia += (DateTime.Now.Hour - hora_s4.Hour) * 3600;
                    }
                    if (hora_s4.Hour.Equals(DateTime.Now.Hour))
                    {
                        if (hora_s4.Minute < DateTime.Now.Minute)
                        {
                            if ((DateTime.Now.Minute - hora_s4.Minute).Equals(1))
                            {
                                diferencia += (60 - hora_s4.Second) + DateTime.Now.Second;
                            }
                            else
                            {
                                diferencia += (DateTime.Now.Minute - hora_s4.Minute) * 60;
                                if (DateTime.Now.Second > hora_s4.Second)
                                {
                                    diferencia += DateTime.Now.Second - hora_s4.Second;
                                }
                                else
                                {
                                    diferencia += DateTime.Now.Second + (60 - hora_s4.Second);
                                }
                            }
                        }
                        if (hora_s4.Minute.Equals(DateTime.Now.Minute))
                        {
                            diferencia += DateTime.Now.Second - hora_s4.Second;
                        }
                    }
                }
                else
                {
                    diferencia = 9999999;
                }
                if (diferencia > retardo_seg)
                {
                    hora_s4 = DateTime.Now;
                    Imprimir_etiqueta();
                }
            }
            if (saposalida.Equals("5"))
            {
                if (hora_s5.Year > 1900)
                {
                    if (hora_s5.Hour < DateTime.Now.Hour)
                    {
                        diferencia += (DateTime.Now.Hour - hora_s5.Hour) * 3600;
                    }
                    if (hora_s5.Hour.Equals(DateTime.Now.Hour))
                    {
                        if (hora_s5.Minute < DateTime.Now.Minute)
                        {
                            if ((DateTime.Now.Minute - hora_s5.Minute).Equals(1))
                            {
                                diferencia += (60 - hora_s5.Second) + DateTime.Now.Second;
                            }
                            else
                            {
                                diferencia += (DateTime.Now.Minute - hora_s5.Minute) * 60;
                                if (DateTime.Now.Second > hora_s5.Second)
                                {
                                    diferencia += DateTime.Now.Second - hora_s5.Second;
                                }
                                else
                                {
                                    diferencia += DateTime.Now.Second + (60 - hora_s5.Second);
                                }
                            }
                        }
                        if (hora_s5.Minute.Equals(DateTime.Now.Minute))
                        {
                            diferencia += DateTime.Now.Second - hora_s5.Second;
                        }
                    }
                }
                else
                {
                    diferencia = 9999999;
                }
                if (diferencia > retardo_seg)
                {
                    hora_s5 = DateTime.Now;
                    Imprimir_etiqueta();
                }
            }
            if (saposalida.Equals("6"))
            {
                if (hora_s6.Year > 1900)
                {
                    if (hora_s6.Hour < DateTime.Now.Hour)
                    {
                        diferencia += (DateTime.Now.Hour - hora_s6.Hour) * 3600;
                    }
                    if (hora_s6.Hour.Equals(DateTime.Now.Hour))
                    {
                        if (hora_s6.Minute < DateTime.Now.Minute)
                        {
                            if ((DateTime.Now.Minute - hora_s6.Minute).Equals(1))
                            {
                                diferencia += (60 - hora_s6.Second) + DateTime.Now.Second;
                            }
                            else
                            {
                                diferencia += (DateTime.Now.Minute - hora_s6.Minute) * 60;
                                if (DateTime.Now.Second > hora_s6.Second)
                                {
                                    diferencia += DateTime.Now.Second - hora_s6.Second;
                                }
                                else
                                {
                                    diferencia += DateTime.Now.Second + (60 - hora_s6.Second);
                                }
                            }
                        }
                        if (hora_s6.Minute.Equals(DateTime.Now.Minute))
                        {
                            diferencia += DateTime.Now.Second - hora_s6.Second;
                        }
                    }
                }
                else
                {
                    diferencia = 9999999;
                }
                if (diferencia > retardo_seg)
                {
                    hora_s6 = DateTime.Now;
                    Imprimir_etiqueta();
                }
            }
            if (saposalida.Equals("7"))
            {
                if (hora_s7.Year > 1900)
                {
                    if (hora_s7.Hour < DateTime.Now.Hour)
                    {
                        diferencia += (DateTime.Now.Hour - hora_s7.Hour) * 3600;
                    }
                    if (hora_s7.Hour.Equals(DateTime.Now.Hour))
                    {
                        if (hora_s7.Minute < DateTime.Now.Minute)
                        {
                            if ((DateTime.Now.Minute - hora_s7.Minute).Equals(1))
                            {
                                diferencia += (60 - hora_s7.Second) + DateTime.Now.Second;
                            }
                            else
                            {
                                diferencia += (DateTime.Now.Minute - hora_s7.Minute) * 60;
                                if (DateTime.Now.Second > hora_s7.Second)
                                {
                                    diferencia += DateTime.Now.Second - hora_s7.Second;
                                }
                                else
                                {
                                    diferencia += DateTime.Now.Second + (60 - hora_s7.Second);
                                }
                            }
                        }
                        if (hora_s7.Minute.Equals(DateTime.Now.Minute))
                        {
                            diferencia += DateTime.Now.Second - hora_s7.Second;
                        }
                    }
                }
                else
                {
                    diferencia = 9999999;
                }
                if (diferencia > retardo_seg)
                {
                    hora_s7 = DateTime.Now;
                    Imprimir_etiqueta();
                }
            }
            if (saposalida.Equals("8"))
            {
                if (hora_s8.Year > 1900)
                {
                    if (hora_s8.Hour < DateTime.Now.Hour)
                    {
                        diferencia += (DateTime.Now.Hour - hora_s8.Hour) * 3600;
                    }
                    if (hora_s8.Hour.Equals(DateTime.Now.Hour))
                    {
                        if (hora_s8.Minute < DateTime.Now.Minute)
                        {
                            if ((DateTime.Now.Minute - hora_s8.Minute).Equals(1))
                            {
                                diferencia += (60 - hora_s8.Second) + DateTime.Now.Second;
                            }
                            else
                            {
                                diferencia += (DateTime.Now.Minute - hora_s8.Minute) * 60;
                                if (DateTime.Now.Second > hora_s8.Second)
                                {
                                    diferencia += DateTime.Now.Second - hora_s8.Second;
                                }
                                else
                                {
                                    diferencia += DateTime.Now.Second + (60 - hora_s8.Second);
                                }
                            }
                        }
                        if (hora_s8.Minute.Equals(DateTime.Now.Minute))
                        {
                            diferencia += DateTime.Now.Second - hora_s8.Second;
                        }
                    }
                }
                else
                {
                    diferencia = 9999999;
                }
                if (diferencia > retardo_seg)
                {
                    hora_s8 = DateTime.Now;
                    Imprimir_etiqueta();
                }
            }
            if (saposalida.Equals("9"))
            {
                if (hora_s9.Year > 1900)
                {
                    if (hora_s9.Hour < DateTime.Now.Hour)
                    {
                        diferencia += (DateTime.Now.Hour - hora_s9.Hour) * 3600;
                    }
                    if (hora_s9.Hour.Equals(DateTime.Now.Hour))
                    {
                        if (hora_s9.Minute < DateTime.Now.Minute)
                        {
                            if ((DateTime.Now.Minute - hora_s9.Minute).Equals(1))
                            {
                                diferencia += (60 - hora_s9.Second) + DateTime.Now.Second;
                            }
                            else
                            {
                                diferencia += (DateTime.Now.Minute - hora_s9.Minute) * 60;
                                if (DateTime.Now.Second > hora_s9.Second)
                                {
                                    diferencia += DateTime.Now.Second - hora_s9.Second;
                                }
                                else
                                {
                                    diferencia += DateTime.Now.Second + (60 - hora_s9.Second);
                                }
                            }
                        }
                        if (hora_s9.Minute.Equals(DateTime.Now.Minute))
                        {
                            diferencia += DateTime.Now.Second - hora_s9.Second;
                        }
                    }
                }
                else
                {
                    diferencia = 9999999;
                }
                if (diferencia > retardo_seg)
                {
                    hora_s9 = DateTime.Now;
                    Imprimir_etiqueta();
                }
            }
            if (saposalida.Equals("10"))
            {
                if (hora_s10.Year > 1900)
                {
                    if (hora_s10.Hour < DateTime.Now.Hour)
                    {
                        diferencia += (DateTime.Now.Hour - hora_s10.Hour) * 3600;
                    }
                    if (hora_s10.Hour.Equals(DateTime.Now.Hour))
                    {
                        if (hora_s10.Minute < DateTime.Now.Minute)
                        {
                            if ((DateTime.Now.Minute - hora_s10.Minute).Equals(1))
                            {
                                diferencia += (60 - hora_s10.Second) + DateTime.Now.Second;
                            }
                            else
                            {
                                diferencia += (DateTime.Now.Minute - hora_s10.Minute) * 60;
                                if (DateTime.Now.Second > hora_s10.Second)
                                {
                                    diferencia += DateTime.Now.Second - hora_s10.Second;
                                }
                                else
                                {
                                    diferencia += DateTime.Now.Second + (60 - hora_s10.Second);
                                }
                            }
                        }
                        if (hora_s10.Minute.Equals(DateTime.Now.Minute))
                        {
                            diferencia += DateTime.Now.Second - hora_s10.Second;
                        }
                    }
                }
                else
                {
                    diferencia = 9999999;
                }
                if (diferencia > retardo_seg)
                {
                    hora_s10 = DateTime.Now;
                    Imprimir_etiqueta();
                }
            }
            if (saposalida.Equals("11"))
            {
                if (hora_s11.Year > 1900)
                {
                    if (hora_s11.Hour < DateTime.Now.Hour)
                    {
                        diferencia += (DateTime.Now.Hour - hora_s11.Hour) * 3600;
                    }
                    if (hora_s11.Hour.Equals(DateTime.Now.Hour))
                    {
                        if (hora_s11.Minute < DateTime.Now.Minute)
                        {
                            if ((DateTime.Now.Minute - hora_s11.Minute).Equals(1))
                            {
                                diferencia += (60 - hora_s11.Second) + DateTime.Now.Second;
                            }
                            else
                            {
                                diferencia += (DateTime.Now.Minute - hora_s11.Minute) * 60;
                                if (DateTime.Now.Second > hora_s11.Second)
                                {
                                    diferencia += DateTime.Now.Second - hora_s11.Second;
                                }
                                else
                                {
                                    diferencia += DateTime.Now.Second + (60 - hora_s11.Second);
                                }
                            }
                        }
                        if (hora_s11.Minute.Equals(DateTime.Now.Minute))
                        {
                            diferencia += DateTime.Now.Second - hora_s11.Second;
                        }
                    }
                }
                else
                {
                    diferencia = 9999999;
                }
                if (diferencia > retardo_seg)
                {
                    hora_s11 = DateTime.Now;
                    Imprimir_etiqueta();
                }
            }
            if (saposalida.Equals("12"))
            {
                if (hora_s12.Year > 1900)
                {
                    if (hora_s12.Hour < DateTime.Now.Hour)
                    {
                        diferencia += (DateTime.Now.Hour - hora_s12.Hour) * 3600;
                    }
                    if (hora_s12.Hour.Equals(DateTime.Now.Hour))
                    {
                        if (hora_s12.Minute < DateTime.Now.Minute)
                        {
                            if ((DateTime.Now.Minute - hora_s12.Minute).Equals(1))
                            {
                                diferencia += (60 - hora_s12.Second) + DateTime.Now.Second;
                            }
                            else
                            {
                                diferencia += (DateTime.Now.Minute - hora_s12.Minute) * 60;
                                if (DateTime.Now.Second > hora_s12.Second)
                                {
                                    diferencia += DateTime.Now.Second - hora_s12.Second;
                                }
                                else
                                {
                                    diferencia += DateTime.Now.Second + (60 - hora_s12.Second);
                                }
                            }
                        }
                        if (hora_s12.Minute.Equals(DateTime.Now.Minute))
                        {
                            diferencia += DateTime.Now.Second - hora_s12.Second;
                        }
                    }
                }
                else
                {
                    diferencia = 9999999;
                }
                if (diferencia > retardo_seg)
                {
                    hora_s12 = DateTime.Now;
                    Imprimir_etiqueta();
                }
            }
            if (saposalida.Equals("13"))
            {
                if (hora_s13.Year > 1900)
                {
                    if (hora_s13.Hour < DateTime.Now.Hour)
                    {
                        diferencia += (DateTime.Now.Hour - hora_s13.Hour) * 3600;
                    }
                    if (hora_s13.Hour.Equals(DateTime.Now.Hour))
                    {
                        if (hora_s13.Minute < DateTime.Now.Minute)
                        {
                            if ((DateTime.Now.Minute - hora_s13.Minute).Equals(1))
                            {
                                diferencia += (60 - hora_s13.Second) + DateTime.Now.Second;
                            }
                            else
                            {
                                diferencia += (DateTime.Now.Minute - hora_s13.Minute) * 60;
                                if (DateTime.Now.Second > hora_s13.Second)
                                {
                                    diferencia += DateTime.Now.Second - hora_s13.Second;
                                }
                                else
                                {
                                    diferencia += DateTime.Now.Second + (60 - hora_s13.Second);
                                }
                            }
                        }
                        if (hora_s13.Minute.Equals(DateTime.Now.Minute))
                        {
                            diferencia += DateTime.Now.Second - hora_s13.Second;
                        }
                    }
                }
                else
                {
                    diferencia = 9999999;
                }
                if (diferencia > retardo_seg)
                {
                    hora_s13 = DateTime.Now;
                    Imprimir_etiqueta();
                }
            }
            if (saposalida.Equals("14"))
            {
                if (hora_s14.Year > 1900)
                {
                    if (hora_s14.Hour < DateTime.Now.Hour)
                    {
                        diferencia += (DateTime.Now.Hour - hora_s14.Hour) * 3600;
                    }
                    if (hora_s14.Hour.Equals(DateTime.Now.Hour))
                    {
                        if (hora_s14.Minute < DateTime.Now.Minute)
                        {
                            if ((DateTime.Now.Minute - hora_s14.Minute).Equals(1))
                            {
                                diferencia += (60 - hora_s14.Second) + DateTime.Now.Second;
                            }
                            else
                            {
                                diferencia += (DateTime.Now.Minute - hora_s14.Minute) * 60;
                                if (DateTime.Now.Second > hora_s14.Second)
                                {
                                    diferencia += DateTime.Now.Second - hora_s14.Second;
                                }
                                else
                                {
                                    diferencia += DateTime.Now.Second + (60 - hora_s14.Second);
                                }
                            }
                        }
                        if (hora_s14.Minute.Equals(DateTime.Now.Minute))
                        {
                            diferencia += DateTime.Now.Second - hora_s14.Second;
                        }
                    }
                }
                else
                {
                    diferencia = 9999999;
                }
                if (diferencia > retardo_seg)
                {
                    hora_s14 = DateTime.Now;
                    Imprimir_etiqueta();
                }
            }
            if (saposalida.Equals("15"))
            {
                if (hora_s15.Year > 1900)
                {
                    if (hora_s15.Hour < DateTime.Now.Hour)
                    {
                        diferencia += (DateTime.Now.Hour - hora_s15.Hour) * 3600;
                    }
                    if (hora_s15.Hour.Equals(DateTime.Now.Hour))
                    {
                        if (hora_s15.Minute < DateTime.Now.Minute)
                        {
                            if ((DateTime.Now.Minute - hora_s15.Minute).Equals(1))
                            {
                                diferencia += (60 - hora_s15.Second) + DateTime.Now.Second;
                            }
                            else
                            {
                                diferencia += (DateTime.Now.Minute - hora_s15.Minute) * 60;
                                if (DateTime.Now.Second > hora_s15.Second)
                                {
                                    diferencia += DateTime.Now.Second - hora_s15.Second;
                                }
                                else
                                {
                                    diferencia += DateTime.Now.Second + (60 - hora_s15.Second);
                                }
                            }
                        }
                        if (hora_s15.Minute.Equals(DateTime.Now.Minute))
                        {
                            diferencia += DateTime.Now.Second - hora_s15.Second;
                        }
                    }
                }
                else
                {
                    diferencia = 9999999;
                }
                if (diferencia > retardo_seg)
                {
                    hora_s15 = DateTime.Now;
                    Imprimir_etiqueta();
                }
            }
            if (saposalida.Equals("16"))
            {
                if (hora_s16.Year > 1900)
                {
                    if (hora_s16.Hour < DateTime.Now.Hour)
                    {
                        diferencia += (DateTime.Now.Hour - hora_s16.Hour) * 3600;
                    }
                    if (hora_s16.Hour.Equals(DateTime.Now.Hour))
                    {
                        if (hora_s16.Minute < DateTime.Now.Minute)
                        {
                            if ((DateTime.Now.Minute - hora_s16.Minute).Equals(1))
                            {
                                diferencia += (60 - hora_s16.Second) + DateTime.Now.Second;
                            }
                            else
                            {
                                diferencia += (DateTime.Now.Minute - hora_s16.Minute) * 60;
                                if (DateTime.Now.Second > hora_s16.Second)
                                {
                                    diferencia += DateTime.Now.Second - hora_s16.Second;
                                }
                                else
                                {
                                    diferencia += DateTime.Now.Second + (60 - hora_s16.Second);
                                }
                            }
                        }
                        if (hora_s16.Minute.Equals(DateTime.Now.Minute))
                        {
                            diferencia += DateTime.Now.Second - hora_s16.Second;
                        }
                    }
                }
                else
                {
                    diferencia = 9999999;
                }
                if (diferencia > retardo_seg)
                {
                    hora_s16 = DateTime.Now;
                    Imprimir_etiqueta();
                }
            }
            if (saposalida.Equals("17"))
            {
                if (hora_s17.Year > 1900)
                {
                    if (hora_s17.Hour < DateTime.Now.Hour)
                    {
                        diferencia += (DateTime.Now.Hour - hora_s17.Hour) * 3600;
                    }
                    if (hora_s17.Hour.Equals(DateTime.Now.Hour))
                    {
                        if (hora_s17.Minute < DateTime.Now.Minute)
                        {
                            if ((DateTime.Now.Minute - hora_s17.Minute).Equals(1))
                            {
                                diferencia += (60 - hora_s17.Second) + DateTime.Now.Second;
                            }
                            else
                            {
                                diferencia += (DateTime.Now.Minute - hora_s17.Minute) * 60;
                                if (DateTime.Now.Second > hora_s17.Second)
                                {
                                    diferencia += DateTime.Now.Second - hora_s17.Second;
                                }
                                else
                                {
                                    diferencia += DateTime.Now.Second + (60 - hora_s17.Second);
                                }
                            }
                        }
                        if (hora_s17.Minute.Equals(DateTime.Now.Minute))
                        {
                            diferencia += DateTime.Now.Second - hora_s17.Second;
                        }
                    }
                }
                else
                {
                    diferencia = 9999999;
                }
                if (diferencia > retardo_seg)
                {
                    hora_s17 = DateTime.Now;
                    Imprimir_etiqueta();
                }
            }
            if (saposalida.Equals("18"))
            {
                if (hora_s18.Year > 1900)
                {
                    if (hora_s18.Hour < DateTime.Now.Hour)
                    {
                        diferencia += (DateTime.Now.Hour - hora_s18.Hour) * 3600;
                    }
                    if (hora_s18.Hour.Equals(DateTime.Now.Hour))
                    {
                        if (hora_s18.Minute < DateTime.Now.Minute)
                        {
                            if ((DateTime.Now.Minute - hora_s18.Minute).Equals(1))
                            {
                                diferencia += (60 - hora_s18.Second) + DateTime.Now.Second;
                            }
                            else
                            {
                                diferencia += (DateTime.Now.Minute - hora_s18.Minute) * 60;
                                if (DateTime.Now.Second > hora_s18.Second)
                                {
                                    diferencia += DateTime.Now.Second - hora_s18.Second;
                                }
                                else
                                {
                                    diferencia += DateTime.Now.Second + (60 - hora_s18.Second);
                                }
                            }
                        }
                        if (hora_s18.Minute.Equals(DateTime.Now.Minute))
                        {
                            diferencia += DateTime.Now.Second - hora_s18.Second;
                        }
                    }
                }
                else
                {
                    diferencia = 9999999;
                }
                if (diferencia > retardo_seg)
                {
                    hora_s18 = DateTime.Now;
                    Imprimir_etiqueta();
                }
            }
            if (saposalida.Equals("19"))
            {
                if (hora_s19.Year > 1900)
                {
                    if (hora_s19.Hour < DateTime.Now.Hour)
                    {
                        diferencia += (DateTime.Now.Hour - hora_s19.Hour) * 3600;
                    }
                    if (hora_s19.Hour.Equals(DateTime.Now.Hour))
                    {
                        if (hora_s19.Minute < DateTime.Now.Minute)
                        {
                            if ((DateTime.Now.Minute - hora_s19.Minute).Equals(1))
                            {
                                diferencia += (60 - hora_s19.Second) + DateTime.Now.Second;
                            }
                            else
                            {
                                diferencia += (DateTime.Now.Minute - hora_s19.Minute) * 60;
                                if (DateTime.Now.Second > hora_s19.Second)
                                {
                                    diferencia += DateTime.Now.Second - hora_s19.Second;
                                }
                                else
                                {
                                    diferencia += DateTime.Now.Second + (60 - hora_s19.Second);
                                }
                            }
                        }
                        if (hora_s19.Minute.Equals(DateTime.Now.Minute))
                        {
                            diferencia += DateTime.Now.Second - hora_s19.Second;
                        }
                    }
                }
                else
                {
                    diferencia = 9999999;
                }
                if (diferencia > retardo_seg)
                {
                    hora_s19 = DateTime.Now;
                    Imprimir_etiqueta();
                }
            }
            if (saposalida.Equals("20"))
            {
                if (hora_s20.Year > 1900)
                {
                    if (hora_s20.Hour < DateTime.Now.Hour)
                    {
                        diferencia += (DateTime.Now.Hour - hora_s20.Hour) * 3600;
                    }
                    if (hora_s20.Hour.Equals(DateTime.Now.Hour))
                    {
                        if (hora_s20.Minute < DateTime.Now.Minute)
                        {
                            if ((DateTime.Now.Minute - hora_s20.Minute).Equals(1))
                            {
                                diferencia += (60 - hora_s20.Second) + DateTime.Now.Second;
                            }
                            else
                            {
                                diferencia += (DateTime.Now.Minute - hora_s20.Minute) * 60;
                                if (DateTime.Now.Second > hora_s20.Second)
                                {
                                    diferencia += DateTime.Now.Second - hora_s20.Second;
                                }
                                else
                                {
                                    diferencia += DateTime.Now.Second + (60 - hora_s20.Second);
                                }
                            }
                        }
                        if (hora_s20.Minute.Equals(DateTime.Now.Minute))
                        {
                            diferencia += DateTime.Now.Second - hora_s20.Second;
                        }
                    }
                }
                else
                {
                    diferencia = 9999999;
                }
                if (diferencia > retardo_seg)
                {
                    hora_s20 = DateTime.Now;
                    Imprimir_etiqueta();
                }
            }
            if (saposalida.Equals("21"))
            {
                if (hora_s21.Year > 1900)
                {
                    if (hora_s21.Hour < DateTime.Now.Hour)
                    {
                        diferencia += (DateTime.Now.Hour - hora_s21.Hour) * 3600;
                    }
                    if (hora_s21.Hour.Equals(DateTime.Now.Hour))
                    {
                        if (hora_s21.Minute < DateTime.Now.Minute)
                        {
                            if ((DateTime.Now.Minute - hora_s21.Minute).Equals(1))
                            {
                                diferencia += (60 - hora_s21.Second) + DateTime.Now.Second;
                            }
                            else
                            {
                                diferencia += (DateTime.Now.Minute - hora_s21.Minute) * 60;
                                if (DateTime.Now.Second > hora_s21.Second)
                                {
                                    diferencia += DateTime.Now.Second - hora_s21.Second;
                                }
                                else
                                {
                                    diferencia += DateTime.Now.Second + (60 - hora_s21.Second);
                                }
                            }
                        }
                        if (hora_s21.Minute.Equals(DateTime.Now.Minute))
                        {
                            diferencia += DateTime.Now.Second - hora_s21.Second;
                        }
                    }
                }
                else
                {
                    diferencia = 9999999;
                }
                if (diferencia > retardo_seg)
                {
                    hora_s21 = DateTime.Now;
                    Imprimir_etiqueta();
                }
            }
            if (saposalida.Equals("22"))
            {
                if (hora_s22.Year > 1900)
                {
                    if (hora_s22.Hour < DateTime.Now.Hour)
                    {
                        diferencia += (DateTime.Now.Hour - hora_s22.Hour) * 3600;
                    }
                    if (hora_s22.Hour.Equals(DateTime.Now.Hour))
                    {
                        if (hora_s22.Minute < DateTime.Now.Minute)
                        {
                            if ((DateTime.Now.Minute - hora_s22.Minute).Equals(1))
                            {
                                diferencia += (60 - hora_s22.Second) + DateTime.Now.Second;
                            }
                            else
                            {
                                diferencia += (DateTime.Now.Minute - hora_s22.Minute) * 60;
                                if (DateTime.Now.Second > hora_s22.Second)
                                {
                                    diferencia += DateTime.Now.Second - hora_s22.Second;
                                }
                                else
                                {
                                    diferencia += DateTime.Now.Second + (60 - hora_s22.Second);
                                }
                            }
                        }
                        if (hora_s22.Minute.Equals(DateTime.Now.Minute))
                        {
                            diferencia += DateTime.Now.Second - hora_s22.Second;
                        }
                    }
                }
                else
                {
                    diferencia = 9999999;
                }
                if (diferencia > retardo_seg)
                {
                    hora_s22 = DateTime.Now;
                    Imprimir_etiqueta();
                }
            }
            if (saposalida.Equals("23"))
            {
                if (hora_s23.Year > 1900)
                {
                    if (hora_s23.Hour < DateTime.Now.Hour)
                    {
                        diferencia += (DateTime.Now.Hour - hora_s23.Hour) * 3600;
                    }
                    if (hora_s23.Hour.Equals(DateTime.Now.Hour))
                    {
                        if (hora_s23.Minute < DateTime.Now.Minute)
                        {
                            if ((DateTime.Now.Minute - hora_s23.Minute).Equals(1))
                            {
                                diferencia += (60 - hora_s23.Second) + DateTime.Now.Second;
                            }
                            else
                            {
                                diferencia += (DateTime.Now.Minute - hora_s23.Minute) * 60;
                                if (DateTime.Now.Second > hora_s23.Second)
                                {
                                    diferencia += DateTime.Now.Second - hora_s23.Second;
                                }
                                else
                                {
                                    diferencia += DateTime.Now.Second + (60 - hora_s23.Second);
                                }
                            }
                        }
                        if (hora_s23.Minute.Equals(DateTime.Now.Minute))
                        {
                            diferencia += DateTime.Now.Second - hora_s23.Second;
                        }
                    }
                }
                else
                {
                    diferencia = 9999999;
                }
                if (diferencia > retardo_seg)
                {
                    hora_s23 = DateTime.Now;
                    Imprimir_etiqueta();
                }
            }
            if (saposalida.Equals("24"))
            {
                if (hora_s24.Year > 1900)
                {
                    if (hora_s24.Hour < DateTime.Now.Hour)
                    {
                        diferencia += (DateTime.Now.Hour - hora_s24.Hour) * 3600;
                    }
                    if (hora_s24.Hour.Equals(DateTime.Now.Hour))
                    {
                        if (hora_s24.Minute < DateTime.Now.Minute)
                        {
                            if ((DateTime.Now.Minute - hora_s24.Minute).Equals(1))
                            {
                                diferencia += (60 - hora_s24.Second) + DateTime.Now.Second;
                            }
                            else
                            {
                                diferencia += (DateTime.Now.Minute - hora_s24.Minute) * 60;
                                if (DateTime.Now.Second > hora_s24.Second)
                                {
                                    diferencia += DateTime.Now.Second - hora_s24.Second;
                                }
                                else
                                {
                                    diferencia += DateTime.Now.Second + (60 - hora_s24.Second);
                                }
                            }
                        }
                        if (hora_s24.Minute.Equals(DateTime.Now.Minute))
                        {
                            diferencia += DateTime.Now.Second - hora_s24.Second;
                        }
                    }
                }
                else
                {
                    diferencia = 9999999;
                }
                if (diferencia > retardo_seg)
                {
                    hora_s24 = DateTime.Now;
                    Imprimir_etiqueta();
                }
            }
            if (saposalida.Equals("25"))
            {
                if (hora_s25.Year > 1900)
                {
                    if (hora_s25.Hour < DateTime.Now.Hour)
                    {
                        diferencia += (DateTime.Now.Hour - hora_s25.Hour) * 3600;
                    }
                    if (hora_s25.Hour.Equals(DateTime.Now.Hour))
                    {
                        if (hora_s25.Minute < DateTime.Now.Minute)
                        {
                            if ((DateTime.Now.Minute - hora_s25.Minute).Equals(1))
                            {
                                diferencia += (60 - hora_s25.Second) + DateTime.Now.Second;
                            }
                            else
                            {
                                diferencia += (DateTime.Now.Minute - hora_s25.Minute) * 60;
                                if (DateTime.Now.Second > hora_s25.Second)
                                {
                                    diferencia += DateTime.Now.Second - hora_s25.Second;
                                }
                                else
                                {
                                    diferencia += DateTime.Now.Second + (60 - hora_s25.Second);
                                }
                            }
                        }
                        if (hora_s25.Minute.Equals(DateTime.Now.Minute))
                        {
                            diferencia += DateTime.Now.Second - hora_s25.Second;
                        }
                    }
                }
                else
                {
                    diferencia = 9999999;
                }
                if (diferencia > retardo_seg)
                {
                    hora_s25 = DateTime.Now;
                    Imprimir_etiqueta();
                }
            }
            if (saposalida.Equals("26"))
            {
                if (hora_s26.Year > 1900)
                {
                    if (hora_s26.Hour < DateTime.Now.Hour)
                    {
                        diferencia += (DateTime.Now.Hour - hora_s26.Hour) * 3600;
                    }
                    if (hora_s26.Hour.Equals(DateTime.Now.Hour))
                    {
                        if (hora_s26.Minute < DateTime.Now.Minute)
                        {
                            if ((DateTime.Now.Minute - hora_s26.Minute).Equals(1))
                            {
                                diferencia += (60 - hora_s26.Second) + DateTime.Now.Second;
                            }
                            else
                            {
                                diferencia += (DateTime.Now.Minute - hora_s26.Minute) * 60;
                                if (DateTime.Now.Second > hora_s26.Second)
                                {
                                    diferencia += DateTime.Now.Second - hora_s26.Second;
                                }
                                else
                                {
                                    diferencia += DateTime.Now.Second + (60 - hora_s26.Second);
                                }
                            }
                        }
                        if (hora_s26.Minute.Equals(DateTime.Now.Minute))
                        {
                            diferencia += DateTime.Now.Second - hora_s26.Second;
                        }
                    }
                }
                else
                {
                    diferencia = 9999999;
                }
                if (diferencia > retardo_seg)
                {
                    hora_s26 = DateTime.Now;
                    Imprimir_etiqueta();
                }
            }
            //FIN salidas
        }

        private void PanelLed25_BackColorChanged(object sender, EventArgs e)
        {
            if (PanelLed25.BackColor.ToString().Equals("Color [Lime]"))
            {
                saposalida = "23";
                contadorS23 += 1;
                impresora = "IMPRESORA13";
                BtnImp1_Click(sender, e);
            }
        }

        private void PanelLed26_BackColorChanged(object sender, EventArgs e)
        {
            if (PanelLed26.BackColor.ToString().Equals("Color [Lime]"))
            {
                saposalida = "24";
                contadorS24 += 1;
                impresora = "IMPRESORA13";
                BtnImp1_Click(sender, e);
            }
        }

        private void PanelLed27_BackColorChanged(object sender, EventArgs e)
        {
            if (PanelLed27.BackColor.ToString().Equals("Color [Lime]"))
            {
                saposalida = "25";
                contadorS25 += 1;
                impresora = "IMPRESORA14";
                BtnImp1_Click(sender, e);
            }
        }

        private void PanelLed28_BackColorChanged(object sender, EventArgs e)
        {
            if (PanelLed28.BackColor.ToString().Equals("Color [Lime]"))
            {
                saposalida = "26";
                contadorS26 += 1;
                impresora = "IMPRESORA14";
                BtnImp1_Click(sender, e);
            }
        }

        public static void ObtenerConexion1()
        {
            conectar1 = new MySqlConnection("server=localhost; Port=3306; database=cintas_santacatalina; Uid=root; pwd=spsi2018;");
            conectar1.Open();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            hora_s1 = new DateTime(1900, 1, 1, 0, 0, 0);
            hora_s2 = new DateTime(1900, 1, 1, 0, 0, 0);
            hora_s3 = new DateTime(1900, 1, 1, 0, 0, 0);
            hora_s4 = new DateTime(1900, 1, 1, 0, 0, 0);
            hora_s5 = new DateTime(1900, 1, 1, 0, 0, 0);
            hora_s6 = new DateTime(1900, 1, 1, 0, 0, 0);
            hora_s7 = new DateTime(1900, 1, 1, 0, 0, 0);
            hora_s8 = new DateTime(1900, 1, 1, 0, 0, 0);
            hora_s9 = new DateTime(1900, 1, 1, 0, 0, 0);
            hora_s10 = new DateTime(1900, 1, 1, 0, 0, 0);
            hora_s11 = new DateTime(1900, 1, 1, 0, 0, 0);
            hora_s12 = new DateTime(1900, 1, 1, 0, 0, 0);
            hora_s13 = new DateTime(1900, 1, 1, 0, 0, 0);
            hora_s14 = new DateTime(1900, 1, 1, 0, 0, 0);
            hora_s15 = new DateTime(1900, 1, 1, 0, 0, 0);
            hora_s16 = new DateTime(1900, 1, 1, 0, 0, 0);
            hora_s17 = new DateTime(1900, 1, 1, 0, 0, 0);
            hora_s18 = new DateTime(1900, 1, 1, 0, 0, 0);
            hora_s19 = new DateTime(1900, 1, 1, 0, 0, 0);
            hora_s20 = new DateTime(1900, 1, 1, 0, 0, 0);
            hora_s21 = new DateTime(1900, 1, 1, 0, 0, 0);
            hora_s22 = new DateTime(1900, 1, 1, 0, 0, 0);
            hora_s23 = new DateTime(1900, 1, 1, 0, 0, 0);
            hora_s24 = new DateTime(1900, 1, 1, 0, 0, 0);
            hora_s25 = new DateTime(1900, 1, 1, 0, 0, 0);
            hora_s26 = new DateTime(1900, 1, 1, 0, 0, 0);
            hora_sC1 = new DateTime(1900, 1, 1, 0, 0, 0);
            hora_sC2 = new DateTime(1900, 1, 1, 0, 0, 0);
            //iniciar hilo lector de sensores
            //backgroundWorker1.RunWorkerAsync();
            timer_sensores.Start();
            timer_vaciado.Start();
            CargarEmbajales();
            CargarVariedades();
            CargarCalibres();
            //CARGAR TODAS LAS SALIDAS ULTIMA GUARDADA
            StreamReader lectura = new StreamReader("conf.txt");
            string linea = "";
            string[] variables = new string[120];
            int i = 0;
            while ((linea = lectura.ReadLine()) != null)            //recorro linea a linea el documento y lo almaceno en el array variables
            {                                                       //para ir guardando las variables ya que no todas tienen el mismo nombre
                                                                    //lectura linea a linea
                variables[i] = linea;
                i++;
            }
            EmbalajeBox1.Text = variables[0];
            EmbalajeBox2.Text = variables[1];
            EmbalajeBox3.Text = variables[2];
            EmbalajeBox4.Text = variables[3];
            EmbalajeBox5.Text = variables[4];
            EmbalajeBox6.Text = variables[5];
            EmbalajeBox7.Text = variables[6];
            EmbalajeBox8.Text = variables[7];
            EmbalajeBox9.Text = variables[8];
            EmbalajeBox10.Text = variables[9];
            EmbalajeBox11.Text = variables[10];
            EmbalajeBox12.Text = variables[11];
            EmbalajeBox13.Text = variables[12];
            EmbalajeBox14.Text = variables[13];
            EmbalajeBox15.Text = variables[14];
            EmbalajeBox16.Text = variables[15];
            EmbalajeBox17.Text = variables[16];
            EmbalajeBox18.Text = variables[17];
            EmbalajeBox19.Text = variables[18];
            EmbalajeBox20.Text = variables[19];
            EmbalajeBox21.Text = variables[20];
            EmbalajeBox22.Text = variables[21];
            EmbalajeBox23.Text = variables[22];
            EmbalajeBox24.Text = variables[23];
            EmbalajeBox25.Text = variables[24];
            EmbalajeBox26.Text = variables[25];
            EmbalajeBox27.Text = variables[26];
            EmbalajeBox28.Text = variables[27];
            TurnoBox1.Text = variables[28];
            TurnoBox2.Text = variables[29];
            TurnoBox3.Text = variables[30];
            TurnoBox4.Text = variables[31];
            TurnoBox5.Text = variables[32];
            TurnoBox6.Text = variables[33];
            TurnoBox7.Text = variables[34];
            TurnoBox8.Text = variables[35];
            TurnoBox9.Text = variables[36];
            TurnoBox10.Text = variables[37];
            TurnoBox11.Text = variables[38];
            TurnoBox12.Text = variables[39];
            TurnoBox13.Text = variables[40];
            TurnoBox14.Text = variables[41];
            TurnoBox15.Text = variables[42];
            TurnoBox16.Text = variables[43];
            TurnoBox17.Text = variables[44];
            TurnoBox18.Text = variables[45];
            TurnoBox19.Text = variables[46];
            TurnoBox20.Text = variables[47];
            TurnoBox21.Text = variables[48];
            TurnoBox22.Text = variables[49];
            TurnoBox23.Text = variables[50];
            TurnoBox24.Text = variables[51];
            TurnoBox25.Text = variables[52];
            TurnoBox26.Text = variables[53];
            TurnoBox27.Text = variables[54];
            TurnoBox28.Text = variables[55];
            VariedadBox1.Text = variables[56];
            VariedadBox2.Text = variables[57];
            VariedadBox3.Text = variables[58];
            VariedadBox4.Text = variables[59];
            VariedadBox5.Text = variables[60];
            VariedadBox6.Text = variables[61];
            VariedadBox7.Text = variables[62];
            VariedadBox8.Text = variables[63];
            VariedadBox9.Text = variables[64];
            VariedadBox10.Text = variables[65];
            VariedadBox11.Text = variables[66];
            VariedadBox12.Text = variables[67];
            VariedadBox13.Text = variables[68];
            VariedadBox14.Text = variables[69];
            VariedadBox15.Text = variables[70];
            VariedadBox16.Text = variables[71];
            VariedadBox17.Text = variables[72];
            VariedadBox18.Text = variables[73];
            VariedadBox19.Text = variables[74];
            VariedadBox20.Text = variables[75];
            VariedadBox21.Text = variables[76];
            VariedadBox22.Text = variables[77];
            VariedadBox23.Text = variables[78];
            VariedadBox24.Text = variables[79];
            VariedadBox25.Text = variables[80];
            VariedadBox26.Text = variables[81];
            VariedadBox27.Text = variables[82];
            VariedadBox28.Text = variables[83];
            CalibreBox1.Text = variables[84];
            CalibreBox2.Text = variables[85];
            CalibreBox3.Text = variables[86];
            CalibreBox4.Text = variables[87];
            CalibreBox5.Text = variables[88];
            CalibreBox6.Text = variables[89];
            CalibreBox7.Text = variables[90];
            CalibreBox8.Text = variables[91];
            CalibreBox9.Text = variables[92];
            CalibreBox10.Text = variables[93];
            CalibreBox11.Text = variables[94];
            CalibreBox12.Text = variables[95];
            CalibreBox13.Text = variables[96];
            CalibreBox14.Text = variables[97];
            CalibreBox15.Text = variables[98];
            CalibreBox16.Text = variables[99];
            CalibreBox17.Text = variables[100];
            CalibreBox18.Text = variables[101];
            CalibreBox19.Text = variables[102];
            CalibreBox20.Text = variables[103];
            CalibreBox21.Text = variables[104];
            CalibreBox22.Text = variables[105];
            CalibreBox23.Text = variables[106];
            CalibreBox24.Text = variables[107];
            CalibreBox25.Text = variables[108];
            CalibreBox26.Text = variables[109];
            CalibreBox27.Text = variables[110];
            CalibreBox28.Text = variables[111];
        }
        private void CargarEmbajales()
        {
            ConexionDB nu = new ConexionDB();
            
            string[] lista = new string[50];
            try
            {
                EmbalajeBox1.Items.Clear();
                EmbalajeBox2.Items.Clear();
                EmbalajeBox3.Items.Clear();
                EmbalajeBox4.Items.Clear();
                EmbalajeBox5.Items.Clear();
                EmbalajeBox6.Items.Clear();
                EmbalajeBox7.Items.Clear();
                EmbalajeBox8.Items.Clear();
                EmbalajeBox9.Items.Clear();
                EmbalajeBox10.Items.Clear();
                EmbalajeBox11.Items.Clear();
                EmbalajeBox12.Items.Clear();
                EmbalajeBox13.Items.Clear();
                EmbalajeBox14.Items.Clear();
                EmbalajeBox15.Items.Clear();
                EmbalajeBox16.Items.Clear();
                EmbalajeBox17.Items.Clear();
                EmbalajeBox18.Items.Clear();
                EmbalajeBox19.Items.Clear();
                EmbalajeBox20.Items.Clear();
                EmbalajeBox21.Items.Clear();
                EmbalajeBox22.Items.Clear();
                EmbalajeBox23.Items.Clear();
                EmbalajeBox24.Items.Clear();
                EmbalajeBox25.Items.Clear();
                EmbalajeBox26.Items.Clear();
                EmbalajeBox27.Items.Clear();
                EmbalajeBox28.Items.Clear();
                EmbalajeBoxGral.Items.Clear();
                EmbalajeBoxGral.Items.Add("No Cambiar");
                lista = nu.CargaEmbalajes();
                int i = 0;
                try
                {
                    while (lista[i].Length > 0)
                    {
                        EmbalajeBox1.Items.Add(lista[i]);
                        EmbalajeBox2.Items.Add(lista[i]);
                        EmbalajeBox3.Items.Add(lista[i]);
                        EmbalajeBox4.Items.Add(lista[i]);
                        EmbalajeBox5.Items.Add(lista[i]);
                        EmbalajeBox6.Items.Add(lista[i]);
                        EmbalajeBox7.Items.Add(lista[i]);
                        EmbalajeBox8.Items.Add(lista[i]);
                        EmbalajeBox9.Items.Add(lista[i]);
                        EmbalajeBox10.Items.Add(lista[i]);
                        EmbalajeBox11.Items.Add(lista[i]);
                        EmbalajeBox12.Items.Add(lista[i]);
                        EmbalajeBox13.Items.Add(lista[i]);
                        EmbalajeBox14.Items.Add(lista[i]);
                        EmbalajeBox15.Items.Add(lista[i]);
                        EmbalajeBox16.Items.Add(lista[i]);
                        EmbalajeBox17.Items.Add(lista[i]);
                        EmbalajeBox18.Items.Add(lista[i]);
                        EmbalajeBox19.Items.Add(lista[i]);
                        EmbalajeBox20.Items.Add(lista[i]);
                        EmbalajeBox21.Items.Add(lista[i]);
                        EmbalajeBox22.Items.Add(lista[i]);
                        EmbalajeBox23.Items.Add(lista[i]);
                        EmbalajeBox24.Items.Add(lista[i]);
                        EmbalajeBox25.Items.Add(lista[i]);
                        EmbalajeBox26.Items.Add(lista[i]);
                        EmbalajeBox27.Items.Add(lista[i]);
                        EmbalajeBox28.Items.Add(lista[i]);
                        EmbalajeBoxGral.Items.Add(lista[i]);
                        i = i + 1;
                    }
                }
                catch (Exception) { }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
            
            nu.Cerrar();
        }
        private void CargarCalibres()
        {
            ConexionDB nu = new ConexionDB();

            string[] lista = new string[50];
            try
            {
                CalibreBox1.Items.Clear();
                CalibreBox2.Items.Clear();
                CalibreBox3.Items.Clear();
                CalibreBox4.Items.Clear();
                CalibreBox5.Items.Clear();
                CalibreBox6.Items.Clear();
                CalibreBox7.Items.Clear();
                CalibreBox8.Items.Clear();
                CalibreBox9.Items.Clear();
                CalibreBox10.Items.Clear();
                CalibreBox11.Items.Clear();
                CalibreBox12.Items.Clear();
                CalibreBox13.Items.Clear();
                CalibreBox14.Items.Clear();
                CalibreBox15.Items.Clear();
                CalibreBox16.Items.Clear();
                CalibreBox17.Items.Clear();
                CalibreBox18.Items.Clear();
                CalibreBox19.Items.Clear();
                CalibreBox20.Items.Clear();
                CalibreBox21.Items.Clear();
                CalibreBox22.Items.Clear();
                CalibreBox23.Items.Clear();
                CalibreBox24.Items.Clear();
                CalibreBox25.Items.Clear();
                CalibreBox26.Items.Clear();
                CalibreBox27.Items.Clear();
                CalibreBox28.Items.Clear();
                CalibreBoxGral.Items.Clear();
                CalibreBoxGral.Items.Add("No Cambiar");
                lista = nu.CargaCalibres();
                int i = 0;
                try
                {
                    while (lista[i].Length > 0)
                    {
                        CalibreBox1.Items.Add(lista[i]);
                        CalibreBox2.Items.Add(lista[i]);
                        CalibreBox3.Items.Add(lista[i]);
                        CalibreBox4.Items.Add(lista[i]);
                        CalibreBox5.Items.Add(lista[i]);
                        CalibreBox6.Items.Add(lista[i]);
                        CalibreBox7.Items.Add(lista[i]);
                        CalibreBox8.Items.Add(lista[i]);
                        CalibreBox9.Items.Add(lista[i]);
                        CalibreBox10.Items.Add(lista[i]);
                        CalibreBox11.Items.Add(lista[i]);
                        CalibreBox12.Items.Add(lista[i]);
                        CalibreBox13.Items.Add(lista[i]);
                        CalibreBox14.Items.Add(lista[i]);
                        CalibreBox15.Items.Add(lista[i]);
                        CalibreBox16.Items.Add(lista[i]);
                        CalibreBox17.Items.Add(lista[i]);
                        CalibreBox18.Items.Add(lista[i]);
                        CalibreBox19.Items.Add(lista[i]);
                        CalibreBox20.Items.Add(lista[i]);
                        CalibreBox21.Items.Add(lista[i]);
                        CalibreBox22.Items.Add(lista[i]);
                        CalibreBox23.Items.Add(lista[i]);
                        CalibreBox24.Items.Add(lista[i]);
                        CalibreBox25.Items.Add(lista[i]);
                        CalibreBox26.Items.Add(lista[i]);
                        CalibreBox27.Items.Add(lista[i]);
                        CalibreBox28.Items.Add(lista[i]);
                        CalibreBoxGral.Items.Add(lista[i]);
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
                VariedadBox1.Items.Clear();
                VariedadBox2.Items.Clear();
                VariedadBox3.Items.Clear();
                VariedadBox4.Items.Clear();
                VariedadBox5.Items.Clear();
                VariedadBox6.Items.Clear();
                VariedadBox7.Items.Clear();
                VariedadBox8.Items.Clear();
                VariedadBox9.Items.Clear();
                VariedadBox10.Items.Clear();
                VariedadBox11.Items.Clear();
                VariedadBox12.Items.Clear();
                VariedadBox13.Items.Clear();
                VariedadBox14.Items.Clear();
                VariedadBox15.Items.Clear();
                VariedadBox16.Items.Clear();
                VariedadBox17.Items.Clear();
                VariedadBox18.Items.Clear();
                VariedadBox19.Items.Clear();
                VariedadBox20.Items.Clear();
                VariedadBox21.Items.Clear();
                VariedadBox22.Items.Clear();
                VariedadBox23.Items.Clear();
                VariedadBox24.Items.Clear();
                VariedadBox25.Items.Clear();
                VariedadBox26.Items.Clear();
                VariedadBox27.Items.Clear();
                VariedadBox28.Items.Clear();
                VariedadBoxGral.Items.Clear();
                VariedadBoxGral.Items.Add("No Cambiar");
                lista = nu.CargaVariedades();
                int i = 0;
                try
                {
                    while (lista[i].Length > 0)
                    {
                        VariedadBox1.Items.Add(lista[i]);
                        VariedadBox2.Items.Add(lista[i]);
                        VariedadBox3.Items.Add(lista[i]);
                        VariedadBox4.Items.Add(lista[i]);
                        VariedadBox5.Items.Add(lista[i]);
                        VariedadBox6.Items.Add(lista[i]);
                        VariedadBox7.Items.Add(lista[i]);
                        VariedadBox8.Items.Add(lista[i]);
                        VariedadBox9.Items.Add(lista[i]);
                        VariedadBox10.Items.Add(lista[i]);
                        VariedadBox11.Items.Add(lista[i]);
                        VariedadBox12.Items.Add(lista[i]);
                        VariedadBox13.Items.Add(lista[i]);
                        VariedadBox14.Items.Add(lista[i]);
                        VariedadBox15.Items.Add(lista[i]);
                        VariedadBox16.Items.Add(lista[i]);
                        VariedadBox17.Items.Add(lista[i]);
                        VariedadBox18.Items.Add(lista[i]);
                        VariedadBox19.Items.Add(lista[i]);
                        VariedadBox20.Items.Add(lista[i]);
                        VariedadBox21.Items.Add(lista[i]);
                        VariedadBox22.Items.Add(lista[i]);
                        VariedadBox23.Items.Add(lista[i]);
                        VariedadBox24.Items.Add(lista[i]);
                        VariedadBox25.Items.Add(lista[i]);
                        VariedadBox26.Items.Add(lista[i]);
                        VariedadBox27.Items.Add(lista[i]);
                        VariedadBox28.Items.Add(lista[i]);
                        VariedadBoxGral.Items.Add(lista[i]);
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
        private void Print_PrintPage(object sender, PrintPageEventArgs e)
        {
            //campos necesarios armado de etiqueta
            string especie;
            string variedad = "";
            string calibre = "";
            string embalaje = "";
            string productor = vaciado_productor;
            string comuna = "";
            ConexionDB conee = new ConexionDB();
            string provincia = conee.ObtenerProvincia(productor);
            conee.Cerrar();
            string region = "";
            //string ggn = "00000000000";
            string lote_huerto = vaciado_lote;
            string proceso_n = vaciado_proceso;
            int turno = 1;
            //ConexionDB cone2 = new ConexionDB();
            //string fda = cone2.ObtenerFDA(productor);
            //cone2.Cerrar();
            //string tipo_frio = "AR";
            especie = "CHERRIES";
            if (saposalida.Equals("C1"))
            {
                embalaje = EmbalajeBox1.Text;
                turno = Convert.ToInt32(TurnoBox1.Text);
                variedad = VariedadBox1.Text;
                calibre = CalibreBox1.Text;
            }
            if (saposalida.Equals("C2"))
            {
                embalaje = EmbalajeBox2.Text;
                turno = Convert.ToInt32(TurnoBox2.Text);
                variedad = VariedadBox2.Text;
                calibre = CalibreBox2.Text;
            }
            if (saposalida.Equals("1"))
            {
                embalaje = EmbalajeBox3.Text;
                turno = Convert.ToInt32(TurnoBox3.Text);
                variedad = VariedadBox3.Text;
                calibre = CalibreBox3.Text;
            }
            if (saposalida.Equals("2"))
            {
                embalaje = EmbalajeBox4.Text;
                turno = Convert.ToInt32(TurnoBox4.Text);
                variedad = VariedadBox4.Text;
                calibre = CalibreBox4.Text;
            }
            if (saposalida.Equals("3"))
            {
                embalaje = EmbalajeBox5.Text;
                turno = Convert.ToInt32(TurnoBox5.Text);
                variedad = VariedadBox5.Text;
                calibre = CalibreBox5.Text;
            }
            if (saposalida.Equals("4"))
            {
                embalaje = EmbalajeBox6.Text;
                turno = Convert.ToInt32(TurnoBox6.Text);
                variedad = VariedadBox6.Text;
                calibre = CalibreBox6.Text;
            }
            if (saposalida.Equals("5"))
            {
                embalaje = EmbalajeBox7.Text;
                turno = Convert.ToInt32(TurnoBox7.Text);
                variedad = VariedadBox7.Text;
                calibre = CalibreBox7.Text;
            }
            if (saposalida.Equals("6"))
            {
                embalaje = EmbalajeBox8.Text;
                turno = Convert.ToInt32(TurnoBox8.Text);
                variedad = VariedadBox8.Text;
                calibre = CalibreBox8.Text;
            }
            if (saposalida.Equals("7"))
            {
                embalaje = EmbalajeBox9.Text;
                turno = Convert.ToInt32(TurnoBox9.Text);
                variedad = VariedadBox9.Text;
                calibre = CalibreBox9.Text;
            }
            if (saposalida.Equals("8"))
            {
                embalaje = EmbalajeBox10.Text;
                turno = Convert.ToInt32(TurnoBox10.Text);
                variedad = VariedadBox10.Text;
                calibre = CalibreBox10.Text;
            }
            if (saposalida.Equals("9"))
            {
                embalaje = EmbalajeBox11.Text;
                turno = Convert.ToInt32(TurnoBox11.Text);
                variedad = VariedadBox11.Text;
                calibre = CalibreBox11.Text;
            }
            if (saposalida.Equals("10"))
            {
                embalaje = EmbalajeBox12.Text;
                turno = Convert.ToInt32(TurnoBox12.Text);
                variedad = VariedadBox12.Text;
                calibre = CalibreBox12.Text;
            }
            if (saposalida.Equals("11"))
            {
                embalaje = EmbalajeBox13.Text;
                turno = Convert.ToInt32(TurnoBox13.Text);
                variedad = VariedadBox13.Text;
                calibre = CalibreBox13.Text;
            }
            if (saposalida.Equals("12"))
            {
                embalaje = EmbalajeBox14.Text;
                turno = Convert.ToInt32(TurnoBox14.Text);
                variedad = VariedadBox14.Text;
                calibre = CalibreBox14.Text;
            }
            if (saposalida.Equals("13"))
            {
                embalaje = EmbalajeBox15.Text;
                turno = Convert.ToInt32(TurnoBox15.Text);
                variedad = VariedadBox15.Text;
                calibre = CalibreBox15.Text;
            }
            if (saposalida.Equals("14"))
            {
                embalaje = EmbalajeBox16.Text;
                turno = Convert.ToInt32(TurnoBox16.Text);
                variedad = VariedadBox16.Text;
                calibre = CalibreBox16.Text;
            }
            if (saposalida.Equals("15"))
            {
                embalaje = EmbalajeBox17.Text;
                turno = Convert.ToInt32(TurnoBox17.Text);
                variedad = VariedadBox17.Text;
                calibre = CalibreBox17.Text;
            }
            if (saposalida.Equals("16"))
            {
                embalaje = EmbalajeBox18.Text;
                turno = Convert.ToInt32(TurnoBox18.Text);
                variedad = VariedadBox18.Text;
                calibre = CalibreBox18.Text;
            }
            if (saposalida.Equals("17"))
            {
                embalaje = EmbalajeBox19.Text;
                turno = Convert.ToInt32(TurnoBox19.Text);
                variedad = VariedadBox19.Text;
                calibre = CalibreBox19.Text;
            }
            if (saposalida.Equals("18"))
            {
                embalaje = EmbalajeBox20.Text;
                turno = Convert.ToInt32(TurnoBox20.Text);
                variedad = VariedadBox20.Text;
                calibre = CalibreBox20.Text;
            }
            if (saposalida.Equals("19"))
            {
                embalaje = EmbalajeBox21.Text;
                turno = Convert.ToInt32(TurnoBox21.Text);
                variedad = VariedadBox21.Text;
                calibre = CalibreBox21.Text;
            }
            if (saposalida.Equals("20"))
            {
                embalaje = EmbalajeBox22.Text;
                turno = Convert.ToInt32(TurnoBox22.Text);
                variedad = VariedadBox22.Text;
                calibre = CalibreBox22.Text;
            }
            if (saposalida.Equals("21"))
            {
                embalaje = EmbalajeBox23.Text;
                turno = Convert.ToInt32(TurnoBox23.Text);
                variedad = VariedadBox23.Text;
                calibre = CalibreBox23.Text;
            }
            if (saposalida.Equals("22"))
            {
                embalaje = EmbalajeBox24.Text;
                turno = Convert.ToInt32(TurnoBox24.Text);
                variedad = VariedadBox24.Text;
                calibre = CalibreBox24.Text;
            }
            if (saposalida.Equals("23"))
            {
                embalaje = EmbalajeBox25.Text;
                turno = Convert.ToInt32(TurnoBox25.Text);
                variedad = VariedadBox25.Text;
                calibre = CalibreBox25.Text;
            }
            if (saposalida.Equals("24"))
            {
                embalaje = EmbalajeBox26.Text;
                turno = Convert.ToInt32(TurnoBox26.Text);
                variedad = VariedadBox26.Text;
                calibre = CalibreBox26.Text;
            }
            if (saposalida.Equals("25"))
            {
                embalaje = EmbalajeBox27.Text;
                turno = Convert.ToInt32(TurnoBox27.Text);
                variedad = VariedadBox27.Text;
                calibre = CalibreBox27.Text;
            }
            if (saposalida.Equals("26"))
            {
                embalaje = EmbalajeBox28.Text;
                turno = Convert.ToInt32(TurnoBox28.Text);
                variedad = VariedadBox28.Text;
                calibre = CalibreBox28.Text;
            }
            //FUENTES A USAR
            Font prFont = new Font("Arial", 5, FontStyle.Bold);
            Font prFont2 = new Font("Arial", 4, FontStyle.Bold);
            Font desFont = new Font("Arial", 8, FontStyle.Bold);
            Font desFont2 = new Font("Arial", 6, FontStyle.Bold);

            //genero numero codigo
            Random Generator = new Random();
            int numerorand, minimo, maximo;
            int anioean = DateTime.Now.Year;

            numcode = anioean + "00000000";
            //GENERO DIGITO VERIFICADOR ean13
            int v1;
            int v2;
            int verificador;

            v1 = Convert.ToInt32(Convert.ToString(numcode).Substring(1, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(3, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(5, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(7, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(9, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(11, 1));
            v1 = v1 * 3;
            v2 = Convert.ToInt32(Convert.ToString(numcode).Substring(0, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(2, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(4, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(6, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(8, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(10, 1));

            double redondea;
            double aux;
            redondea = (v1 + v2) / 10;
            aux = redondea;
            redondea = Math.Round(redondea, 0);
            if (redondea > aux)
            {
                redondea -= 1;
            }
            redondea = redondea + 1;
            redondea = redondea * 10;
            verificador = Convert.ToInt32(redondea) - (v1 + v2);
            if (verificador.Equals(10))
            {
                verificador = 0;
            }

            numcode = numcode + Convert.ToString(verificador);
            ConexionDB versiexite = new ConexionDB();
            while (versiexite.Existe_codigo(numcode))
            {
                minimo = 10000000;
                maximo = 99999999;
                numerorand = Generator.Next(minimo, maximo + 1);
                numcode = anioean + Convert.ToString(numerorand);
                //GENERO DIGITO VERIFICADOR ean13

                v1 = Convert.ToInt32(Convert.ToString(numcode).Substring(1, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(3, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(5, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(7, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(9, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(11, 1));
                v1 = v1 * 3;
                v2 = Convert.ToInt32(Convert.ToString(numcode).Substring(0, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(2, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(4, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(6, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(8, 1)) + Convert.ToInt32(Convert.ToString(numcode).Substring(10, 1));

                redondea = (v1 + v2) / 10;
                aux = redondea;
                redondea = Math.Round(redondea, 0);
                if (redondea > aux)
                {
                    redondea -= 1;
                }
                redondea = redondea + 1;
                redondea = redondea * 10;
                verificador = Convert.ToInt32(redondea) - (v1 + v2);
                if (verificador.Equals(10))
                {
                    verificador = 0;
                }

                numcode = numcode + Convert.ToString(verificador);
            }
            versiexite.Cerrar();
            Barcode128 bcode = new Barcode128
            {
                BarHeight = 60,
                Code = numcode,
                GenerateChecksum = true,
                CodeType = Barcode.CODE128
            };
            Barcode128 bcode2 = new Barcode128
            {
                BarHeight = 35,
                Code = numcode,
                GenerateChecksum = true,
                CodeType = Barcode.CODE128
            };
            try
            {
                Image img;
                img = bcode2.CreateDrawingImage(Color.Black, Color.White);
                img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                Image img2;
                img2 = bcode.CreateDrawingImage(Color.Black, Color.White);
                int yPos = 6;

                Image logo;
                logo = Image.FromFile("etiq_santacatalina.jpg");
                //e.Graphics.DrawImage(logo, 265, yPos + 135, 106, 44);
                e.Graphics.DrawImage(logo, 0, yPos + 120, 106, 44);

                e.Graphics.DrawString(especie.ToUpper()+ "-" + variedad.ToUpper(), new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 40, yPos + 0);
                //e.Graphics.DrawString("-" + variedad.ToUpper(), desFont2, Brushes.Black, 114, yPos + 0);
                //e.Graphics.DrawString("AR", desFont2, Brushes.Black, 196, yPos + 0);

                e.Graphics.DrawString("CSG:", desFont, Brushes.Black, 20, yPos + 24);
                e.Graphics.DrawString("GROWER / PRODUCTOR", new Font("Arial", 3, FontStyle.Bold), Brushes.Black, 11, yPos + 38);
                e.Graphics.DrawString("TOWNSHIP:", prFont, Brushes.Black, 80, yPos + 26);
                e.Graphics.DrawString("PROVINCE:", prFont, Brushes.Black, 80, yPos + 40);
                //e.Graphics.DrawString("GGN:", prFont, Brushes.Black, 0, yPos + 60);
                //ConexionDB cone = new ConexionDB();
                e.Graphics.DrawString(vaciado_csg, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 0, yPos + 42);
                //cone.Cerrar();
                //e.Graphics.DrawString(productor.ToUpper(), prFont, Brushes.Black, 70, yPos + 40);
                ConexionDB cone3 = new ConexionDB();
                comuna = cone3.ObtenerComuna(productor);
                e.Graphics.DrawString(comuna.ToUpper(), prFont, Brushes.Black, 130, yPos + 26);
                cone3.Cerrar();
                ConexionDB conex3 = new ConexionDB();
                provincia = conex3.ObtenerProvincia(productor);
                e.Graphics.DrawString(provincia.ToUpper(), prFont, Brushes.Black, 130, yPos + 40);
                conex3.Cerrar();
                //e.Graphics.DrawString("110987", desFont, Brushes.Black, 70, yPos + 26);                            //provisorio
                //e.Graphics.DrawString("SOCIEDAD PACKING SERVICE INGENIERIA LTDA.", prFont, Brushes.Black, 70, yPos + 40);   //provisorio
                //e.Graphics.DrawString("TENO - CURICO", prFont, Brushes.Black, 70, yPos + 50);                      //provisorio
                //e.Graphics.DrawString(ggn, prFont, Brushes.Black, 70, yPos + 60);
                ConexionDB cone5 = new ConexionDB();
                e.Graphics.DrawString("REGION: ", prFont, Brushes.Black, 80, yPos + 54);
                region = cone5.ObtenerRegion(productor);
                e.Graphics.DrawString(region, prFont, Brushes.Black, 130, yPos + 54);
                cone5.Cerrar();


                e.Graphics.DrawString("NET WEIGTH W.P.", prFont, Brushes.Black, 300, yPos + 5);
                e.Graphics.DrawString(embalaje, prFont, Brushes.Black, 320, yPos + 12);
                int diasemana;
                System.Globalization.CultureInfo norwCulture = System.Globalization.CultureInfo.CreateSpecificCulture("es");
                System.Globalization.Calendar cal = norwCulture.Calendar;
                diasemana = cal.GetWeekOfYear(DateTime.Now, norwCulture.DateTimeFormat.CalendarWeekRule, norwCulture.DateTimeFormat.FirstDayOfWeek);
                e.Graphics.DrawString("SEM: ", prFont, Brushes.Black, 300, yPos + 23);
                e.Graphics.DrawString(diasemana.ToString(), prFont, Brushes.Black, 333, yPos + 23);
                e.Graphics.DrawString("TIME: ", prFont, Brushes.Black, 300, yPos + 30);
                e.Graphics.DrawString(DateTime.Now.ToString("HH:mm:ss"), prFont, Brushes.Black, 333, yPos + 30);
                e.Graphics.DrawString("N° PROCESO:", new Font("Arial", 7, FontStyle.Bold), Brushes.Black, 298, yPos + 40);
                if (proceso_n.Length.Equals(1)) { e.Graphics.DrawString(proceso_n, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 328, yPos + 53); }
                if (proceso_n.Length.Equals(2)) { e.Graphics.DrawString(proceso_n, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 323, yPos + 53); }
                if (proceso_n.Length.Equals(3)) { e.Graphics.DrawString(proceso_n, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 318, yPos + 53); }
                if (proceso_n.Length.Equals(4)) { e.Graphics.DrawString(proceso_n, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 315, yPos + 53); }


                //=================
                if (calibre.Contains("D")) { e.Graphics.DrawString("DARK", new Font("Arial Black", 14, FontStyle.Bold), Brushes.Black, 200, yPos + 56); }
                else {
                    if (calibre.Contains("TC")) { e.Graphics.DrawString(" ", new Font("Arial Black", 12, FontStyle.Bold), Brushes.Black, 200, yPos + 56); }
                    else { e.Graphics.DrawString("LIGHT", new Font("Arial Black", 13, FontStyle.Bold), Brushes.Black, 200, yPos + 56); }
                }

                e.Graphics.DrawString("DATE: ", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 215, yPos + 77);
                //e.Graphics.DrawString("LOTE: ", prFont, Brushes.Black, 222, yPos + 57);
                //e.Graphics.DrawString("PROC N°: ", prFont, Brushes.Black, 222, yPos + 57);
                e.Graphics.DrawString(DateTime.Now.ToString("dd-MM-yyyy"), new Font("Arial", 12, FontStyle.Bold), Brushes.Black, 190, yPos + 89);
                //e.Graphics.DrawString(vaciado_lote, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 255, yPos + 51);
                //e.Graphics.DrawString(proceso_n, prFont, Brushes.Black, 255, yPos + 57);


                Font desFont3 = new Font("Verdana", 22, FontStyle.Bold);
                Font desFont4 = new Font("Verdana", 14, FontStyle.Bold);
                int exis = 150;
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
                if (calibre.Replace("D","").Length.Equals(2))
                {
                    e.Graphics.DrawString(calibre.Replace("D", ""), new Font("Arial Black", 35, FontStyle.Bold), Brushes.Black, exis + 43, yPos -3);
                }
                //else if (calibre.Replace("D", "").Length.Equals(3))
                //{
                //    e.Graphics.DrawString(calibre.Replace("D", ""), desFont4, Brushes.Black, exis + 8, yPos - 3);
                //}
                //else if (calibre.Replace("D", "").Length.Equals(4))
                //{
                //    e.Graphics.DrawString(calibre.Replace("D", ""), desFont3, Brushes.Black, exis - 4, yPos - 3);
                //}
                else if (calibre.Replace("D", "").Length.Equals(1))
                {
                    e.Graphics.DrawString(calibre.Replace("D", ""), new Font("Arial Black", 35, FontStyle.Bold), Brushes.Black, exis + 60, yPos - 3);
                }
                                
                e.Graphics.DrawString("CSP:", desFont, Brushes.Black, 20, yPos + 72);
                e.Graphics.DrawString("PACKING BY / EMPACADORA", new Font("Arial", 3, FontStyle.Bold), Brushes.Black, 7, yPos + 86);
                e.Graphics.DrawString("176227",  new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 0, yPos + 90);
                e.Graphics.DrawString("TOWNSHIP:", prFont, Brushes.Black, 80, yPos + 74);
                //e.Graphics.DrawString("SOCIEDAD PACKING SERVICE INGENIERIA", prFont, Brushes.Black, 70, yPos + 86);
                e.Graphics.DrawString("PROVINCE:", prFont, Brushes.Black, 80, yPos + 88);
                e.Graphics.DrawString("REGION: ", prFont, Brushes.Black, 80, yPos + 102);
                e.Graphics.DrawString("CURICO", prFont, Brushes.Black, 130, yPos + 74);
                e.Graphics.DrawString("CURICO", prFont, Brushes.Black, 130, yPos + 88);
                e.Graphics.DrawString("VII", prFont, Brushes.Black, 130, yPos + 102);
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
                string exp_abreviada = getexp.ObtenerExportadora(vaciado_cse);
                exp_abreviada = exp_abreviada.Replace("EXPORTADORA", "EXP.");
                exp_abreviada = exp_abreviada.Replace("EXPORTADOR", "EXP.");
                exp_abreviada = exp_abreviada.Replace("COMERCIAL", "COM.");
                exp_abreviada = exp_abreviada.Replace("COMERCIALIZADORA", "COM.");
                exp_abreviada = exp_abreviada.Replace("PRODUCTOR", "PROD.");
                e.Graphics.DrawString(exp_abreviada, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 245, yPos + 140);
                getexp.Cerrar();
                e.Graphics.DrawString("CSE:", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 245, yPos + 160);
                e.Graphics.DrawString(vaciado_cse, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 285, yPos + 160);
                
                //e.Graphics.DrawString("TURNO:", prFont2, Brushes.Black, 302, yPos + 74);
                //e.Graphics.DrawString(turno.ToString(), prFont2, Brushes.Black, 332, yPos + 74);
                //e.Graphics.DrawString("LINEA:", prFont2, Brushes.Black, 303, yPos + 85);
                //e.Graphics.DrawString("1", prFont2, Brushes.Black, 332, yPos + 85);
                e.Graphics.DrawString("SALIDA", new Font("Arial", 5, FontStyle.Bold), Brushes.Black, 317, yPos + 74);

                if (saposalida.Length > 1) { e.Graphics.DrawString(saposalida, new Font("Arial", 6, FontStyle.Bold), Brushes.Black, 325, yPos + 82); }
                else { e.Graphics.DrawString(saposalida, new Font("Arial", 6, FontStyle.Bold), Brushes.Black, 327, yPos + 82); }

                e.Graphics.DrawString("IMPRESORA", new Font("Arial", 5, FontStyle.Bold), Brushes.Black, 310, yPos + 96);

                if (impresora.Length > 10) { e.Graphics.DrawString(impresora.Substring(9, 2), new Font("Arial", 6, FontStyle.Bold), Brushes.Black, 325, yPos + 104); }
                else { e.Graphics.DrawString(impresora.Substring(9, 1), new Font("Arial", 6, FontStyle.Bold), Brushes.Black, 329, yPos + 104); }
                //e.Graphics.DrawString("SALIDA:", prFont2, Brushes.Black, 302, yPos + 96);
                //e.Graphics.DrawString(saposalida, prFont2, Brushes.Black, 332, yPos + 96);
                //e.Graphics.DrawString("IMP:", prFont2, Brushes.Black, 302, yPos + 107);
                //if(impresora.Length > 10) { e.Graphics.DrawString(impresora.Substring(9, 2), prFont2, Brushes.Black, 332, yPos + 107); }
                //else { e.Graphics.DrawString(impresora.Substring(9, 1), prFont2, Brushes.Black, 332, yPos + 107); }

                //'codigo vertical     CODE-128
                //e.Graphics.DrawImage(img, 343, yPos + 15);

                //'codigo horizontal   EAN13
                e.Graphics.DrawImage(img2, 115, yPos + 118);

                Font prFontx = new Font("Arial", 6, FontStyle.Bold);
                
                SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
                e.Graphics.FillRectangle(myBrush, new Rectangle(153, yPos + 172, 54, 10));
                e.Graphics.DrawString(numcode, prFont, Brushes.Black, 153, yPos + 173);

                //'GUARDO LA ETIQUETA EN LA BASE DE DATOS

                string cla = "COM";
                if (calibre.Equals("L")) { cla = "LIGHT"; }
                if (calibre.Equals("LD")) { cla = "DARK"; }
                if (calibre.Equals("XL")) { cla = "LIGHT"; }
                if (calibre.Equals("XLD")) { cla = "DARK"; }
                if (calibre.Equals("J")) { cla = "LIGHT"; }
                if (calibre.Equals("JD")) { cla = "DARK"; }
                if (calibre.Equals("SJ")) { cla = "LIGHT"; }
                if (calibre.Equals("SJD")) { cla = "DARK"; }
                if (calibre.Equals("P")) { cla = "LIGHT"; }
                if (calibre.Equals("PD")) { cla = "DARK"; }
                if (calibre.Equals("SP")) { cla = "LIGHT"; }
                if (calibre.Equals("SPD")) { cla = "DARK"; }
                if (calibre.Equals("SG")) { cla = "LIGHT"; }
                if (calibre.Equals("SGD")) { cla = "DARK"; }
                ConexionDB getiq = new ConexionDB();
                getiq.GuardarEtiqueta(numcode, "CHERRY", variedad.ToUpper(), cla, calibre.Replace("D", "").ToUpper(), embalaje.ToUpper(), productor, "CAT-1", lote_huerto, proceso_n, "Salida " + saposalida, "AR", turno, DateTime.Now.ToString("yyyy-MM-dd"), "00", "00");
                getiq.Cerrar();
                //'IMPRIMO LINEAS DEL DISEÑO
                Pen blackPen1 = new Pen(Color.Black, 1);
                Pen blackPen2 = new Pen(Color.Black, 2);
                //'horizontales
                e.Graphics.DrawLine(blackPen2, new Point(295, yPos + 22), new Point(370, yPos + 22));
                e.Graphics.DrawLine(blackPen2, new Point(295, yPos + 70), new Point(370, yPos + 70));
                e.Graphics.DrawLine(blackPen2, new Point(0, yPos + 70), new Point(175, yPos + 70));
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
            prtDoc.Print();
        }
        private void PictureBox1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            metroProgressBar1.Visible = true;
            BackWorkImpresoras.RunWorkerAsync();
            Cursor.Current = Cursors.Default;
        }
        private void BackWorkImpresoras_DoWork(object sender, DoWorkEventArgs e)
        {
            //barra porcentual para indicar progreso del trabajo de actualizar vista
            try
            {
                Ping HacerPing = new Ping();
                int TiempoRespuesta = 5;
                PingReply RespuestaPing;
                RespuestaPing = HacerPing.Send("192.168.0.201", TiempoRespuesta);
                BackWorkImpresoras.ReportProgress(7);
                if (RespuestaPing.Status == IPStatus.Success) { Form1.statusImp[0] = true; } else { Form1.statusImp[0] = false; }
                RespuestaPing = HacerPing.Send("192.168.0.202", TiempoRespuesta);
                BackWorkImpresoras.ReportProgress(14);
                if (RespuestaPing.Status == IPStatus.Success) { Form1.statusImp[1] = true; } else { Form1.statusImp[1] = false; }
                RespuestaPing = HacerPing.Send("192.168.0.203", TiempoRespuesta);
                BackWorkImpresoras.ReportProgress(21);
                if (RespuestaPing.Status == IPStatus.Success) { Form1.statusImp[2] = true; } else { Form1.statusImp[2] = false; }
                RespuestaPing = HacerPing.Send("192.168.0.204", TiempoRespuesta);
                BackWorkImpresoras.ReportProgress(28);
                if (RespuestaPing.Status == IPStatus.Success) { Form1.statusImp[3] = true; } else { Form1.statusImp[3] = false; }
                RespuestaPing = HacerPing.Send("192.168.0.205", TiempoRespuesta);
                BackWorkImpresoras.ReportProgress(35);
                if (RespuestaPing.Status == IPStatus.Success) { Form1.statusImp[4] = true; } else { Form1.statusImp[4] = false; }
                RespuestaPing = HacerPing.Send("192.168.0.206", TiempoRespuesta);
                BackWorkImpresoras.ReportProgress(42);
                if (RespuestaPing.Status == IPStatus.Success) { Form1.statusImp[5] = true; } else { Form1.statusImp[5] = false; }
                RespuestaPing = HacerPing.Send("192.168.0.207", TiempoRespuesta);
                BackWorkImpresoras.ReportProgress(49);
                if (RespuestaPing.Status == IPStatus.Success) { Form1.statusImp[6] = true; } else { Form1.statusImp[6] = false; }
                RespuestaPing = HacerPing.Send("192.168.0.208", TiempoRespuesta);
                BackWorkImpresoras.ReportProgress(56);
                if (RespuestaPing.Status == IPStatus.Success) { Form1.statusImp[7] = true; } else { Form1.statusImp[7] = false; }
                RespuestaPing = HacerPing.Send("192.168.0.209", TiempoRespuesta);
                BackWorkImpresoras.ReportProgress(63);
                if (RespuestaPing.Status == IPStatus.Success) { Form1.statusImp[8] = true; } else { Form1.statusImp[8] = false; }
                RespuestaPing = HacerPing.Send("192.168.0.210", TiempoRespuesta);
                BackWorkImpresoras.ReportProgress(70);
                if (RespuestaPing.Status == IPStatus.Success) { Form1.statusImp[9] = true; } else { Form1.statusImp[9] = false; }
                RespuestaPing = HacerPing.Send("192.168.0.211", TiempoRespuesta);
                BackWorkImpresoras.ReportProgress(77);
                if (RespuestaPing.Status == IPStatus.Success) { Form1.statusImp[10] = true; } else { Form1.statusImp[10] = false; }
                RespuestaPing = HacerPing.Send("192.168.0.212", TiempoRespuesta);
                BackWorkImpresoras.ReportProgress(84);
                if (RespuestaPing.Status == IPStatus.Success) { Form1.statusImp[11] = true; } else { Form1.statusImp[11] = false; }
                RespuestaPing = HacerPing.Send("192.168.0.213", TiempoRespuesta);
                BackWorkImpresoras.ReportProgress(91);
                if (RespuestaPing.Status == IPStatus.Success) { Form1.statusImp[12] = true; } else { Form1.statusImp[12] = false; }
                RespuestaPing = HacerPing.Send("192.168.0.214", TiempoRespuesta);
                BackWorkImpresoras.ReportProgress(98);
                if (RespuestaPing.Status == IPStatus.Success) { Form1.statusImp[13] = true; } else { Form1.statusImp[13] = false; }
                BackWorkImpresoras.ReportProgress(100);
                BackWorkImpresoras.CancelAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR");
            }
        }
        private void BackWorkImpresoras_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //revisar impresoras
            metroProgressBar1.Value = e.ProgressPercentage;

        }
        private void BackWorkImpresoras_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            EstadoImpCkeck1.Checked = statusImp[0];
            EstadoImpCkeck2.Checked = statusImp[1];
            EstadoImpCkeck3.Checked = statusImp[2];
            EstadoImpCkeck4.Checked = statusImp[3];
            EstadoImpCkeck5.Checked = statusImp[4];
            EstadoImpCkeck6.Checked = statusImp[5];
            EstadoImpCkeck7.Checked = statusImp[6];
            EstadoImpCkeck8.Checked = statusImp[7];
            EstadoImpCkeck9.Checked = statusImp[8];
            EstadoImpCkeck10.Checked = statusImp[9];
            EstadoImpCkeck11.Checked = statusImp[10];
            EstadoImpCkeck12.Checked = statusImp[11];
            EstadoImpCkeck13.Checked = statusImp[12];
            EstadoImpCkeck14.Checked = statusImp[13];
            metroProgressBar1.Value = 0;
            metroProgressBar1.Visible = false;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer_sensores.Stop();
            timer_vaciado.Stop();
        }
        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
        }
        private void MetroTile2_Click(object sender, EventArgs e) /*Guardar Configuracion en txt*/
        {
            //fijamos dondevamos a crear el archivo 
            StreamWriter escrito = File.CreateText("conf.txt"); // en el se guardan todos los parametros
                                                                //que se personalizaron para las etiquetas de cada salida
                                                                //asi no se perderá ningun datos al momento de cerrar o abrir de nuevo el programa
                                                                //las preferencias se almacenaran en este TXT
            String contenido = "";
            retardo_seg = Convert.ToInt32(retardo_segTXT.Value); /* guardando retardo en seg, embalajes salida a salida y turno salida a salida*/
            contenido += EmbalajeBox1.Text + Environment.NewLine;
            contenido += EmbalajeBox2.Text + Environment.NewLine;
            contenido += EmbalajeBox3.Text + Environment.NewLine;
            contenido += EmbalajeBox4.Text + Environment.NewLine;
            contenido += EmbalajeBox5.Text + Environment.NewLine;
            contenido += EmbalajeBox6.Text + Environment.NewLine;
            contenido += EmbalajeBox7.Text + Environment.NewLine;
            contenido += EmbalajeBox8.Text + Environment.NewLine;
            contenido += EmbalajeBox9.Text + Environment.NewLine;
            contenido += EmbalajeBox10.Text + Environment.NewLine;
            contenido += EmbalajeBox11.Text + Environment.NewLine;
            contenido += EmbalajeBox12.Text + Environment.NewLine;
            contenido += EmbalajeBox13.Text + Environment.NewLine;
            contenido += EmbalajeBox14.Text + Environment.NewLine;
            contenido += EmbalajeBox15.Text + Environment.NewLine;
            contenido += EmbalajeBox16.Text + Environment.NewLine;
            contenido += EmbalajeBox17.Text + Environment.NewLine;
            contenido += EmbalajeBox18.Text + Environment.NewLine;
            contenido += EmbalajeBox19.Text + Environment.NewLine;
            contenido += EmbalajeBox20.Text + Environment.NewLine;
            contenido += EmbalajeBox21.Text + Environment.NewLine;
            contenido += EmbalajeBox22.Text + Environment.NewLine;
            contenido += EmbalajeBox23.Text + Environment.NewLine;
            contenido += EmbalajeBox24.Text + Environment.NewLine;
            contenido += EmbalajeBox25.Text + Environment.NewLine;
            contenido += EmbalajeBox26.Text + Environment.NewLine;
            contenido += EmbalajeBox27.Text + Environment.NewLine;
            contenido += EmbalajeBox28.Text + Environment.NewLine;
            contenido += TurnoBox1.Text + Environment.NewLine;
            contenido += TurnoBox2.Text + Environment.NewLine;
            contenido += TurnoBox3.Text + Environment.NewLine;
            contenido += TurnoBox4.Text + Environment.NewLine;
            contenido += TurnoBox5.Text + Environment.NewLine;
            contenido += TurnoBox6.Text + Environment.NewLine;
            contenido += TurnoBox7.Text + Environment.NewLine;
            contenido += TurnoBox8.Text + Environment.NewLine;
            contenido += TurnoBox9.Text + Environment.NewLine;
            contenido += TurnoBox10.Text + Environment.NewLine;
            contenido += TurnoBox11.Text + Environment.NewLine;
            contenido += TurnoBox12.Text + Environment.NewLine;
            contenido += TurnoBox13.Text + Environment.NewLine;
            contenido += TurnoBox14.Text + Environment.NewLine;
            contenido += TurnoBox15.Text + Environment.NewLine;
            contenido += TurnoBox16.Text + Environment.NewLine;
            contenido += TurnoBox17.Text + Environment.NewLine;
            contenido += TurnoBox18.Text + Environment.NewLine;
            contenido += TurnoBox19.Text + Environment.NewLine;
            contenido += TurnoBox20.Text + Environment.NewLine;
            contenido += TurnoBox21.Text + Environment.NewLine;
            contenido += TurnoBox22.Text + Environment.NewLine;
            contenido += TurnoBox23.Text + Environment.NewLine;
            contenido += TurnoBox24.Text + Environment.NewLine;
            contenido += TurnoBox25.Text + Environment.NewLine;
            contenido += TurnoBox26.Text + Environment.NewLine;
            contenido += TurnoBox27.Text + Environment.NewLine;
            contenido += TurnoBox28.Text + Environment.NewLine;
            contenido += VariedadBox1.Text + Environment.NewLine;
            contenido += VariedadBox2.Text + Environment.NewLine;
            contenido += VariedadBox3.Text + Environment.NewLine;
            contenido += VariedadBox4.Text + Environment.NewLine;
            contenido += VariedadBox5.Text + Environment.NewLine;
            contenido += VariedadBox6.Text + Environment.NewLine;
            contenido += VariedadBox7.Text + Environment.NewLine;
            contenido += VariedadBox8.Text + Environment.NewLine;
            contenido += VariedadBox9.Text + Environment.NewLine;
            contenido += VariedadBox10.Text + Environment.NewLine;
            contenido += VariedadBox11.Text + Environment.NewLine;
            contenido += VariedadBox12.Text + Environment.NewLine;
            contenido += VariedadBox13.Text + Environment.NewLine;
            contenido += VariedadBox14.Text + Environment.NewLine;
            contenido += VariedadBox15.Text + Environment.NewLine;
            contenido += VariedadBox16.Text + Environment.NewLine;
            contenido += VariedadBox17.Text + Environment.NewLine;
            contenido += VariedadBox18.Text + Environment.NewLine;
            contenido += VariedadBox19.Text + Environment.NewLine;
            contenido += VariedadBox20.Text + Environment.NewLine;
            contenido += VariedadBox21.Text + Environment.NewLine;
            contenido += VariedadBox22.Text + Environment.NewLine;
            contenido += VariedadBox23.Text + Environment.NewLine;
            contenido += VariedadBox24.Text + Environment.NewLine;
            contenido += VariedadBox25.Text + Environment.NewLine;
            contenido += VariedadBox26.Text + Environment.NewLine;
            contenido += VariedadBox27.Text + Environment.NewLine;
            contenido += VariedadBox28.Text + Environment.NewLine;
            contenido += CalibreBox1.Text + Environment.NewLine;
            contenido += CalibreBox2.Text + Environment.NewLine;
            contenido += CalibreBox3.Text + Environment.NewLine;
            contenido += CalibreBox4.Text + Environment.NewLine;
            contenido += CalibreBox5.Text + Environment.NewLine;
            contenido += CalibreBox6.Text + Environment.NewLine;
            contenido += CalibreBox7.Text + Environment.NewLine;
            contenido += CalibreBox8.Text + Environment.NewLine;
            contenido += CalibreBox9.Text + Environment.NewLine;
            contenido += CalibreBox10.Text + Environment.NewLine;
            contenido += CalibreBox11.Text + Environment.NewLine;
            contenido += CalibreBox12.Text + Environment.NewLine;
            contenido += CalibreBox13.Text + Environment.NewLine;
            contenido += CalibreBox14.Text + Environment.NewLine;
            contenido += CalibreBox15.Text + Environment.NewLine;
            contenido += CalibreBox16.Text + Environment.NewLine;
            contenido += CalibreBox17.Text + Environment.NewLine;
            contenido += CalibreBox18.Text + Environment.NewLine;
            contenido += CalibreBox19.Text + Environment.NewLine;
            contenido += CalibreBox20.Text + Environment.NewLine;
            contenido += CalibreBox21.Text + Environment.NewLine;
            contenido += CalibreBox22.Text + Environment.NewLine;
            contenido += CalibreBox23.Text + Environment.NewLine;
            contenido += CalibreBox24.Text + Environment.NewLine;
            contenido += CalibreBox25.Text + Environment.NewLine;
            contenido += CalibreBox26.Text + Environment.NewLine;
            contenido += CalibreBox27.Text + Environment.NewLine;
            contenido += CalibreBox28.Text + Environment.NewLine;
            //escribimos. 
            escrito.Write(contenido.ToString());
            escrito.Flush();
            //Cerramos 
            escrito.Close();
            MetroFramework.MetroMessageBox.Show(this, "Configuración guardada exitosamente.", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void PanelLed1_DoubleClick(object sender, EventArgs e)
        {
            if (PanelLed1.BackColor == Color.DarkGreen || PanelLed1.BackColor == Color.Lime) { PanelLed1.BackColor = Color.Silver; } else { PanelLed1.BackColor = Color.DarkGreen; }
        }
        private void PanelLed2_DoubleClick(object sender, EventArgs e)
        {
            if (PanelLed2.BackColor == Color.DarkGreen || PanelLed2.BackColor == Color.Lime) { PanelLed2.BackColor = Color.Silver; } else { PanelLed2.BackColor = Color.DarkGreen; }
        }

        private void PanelLed3_DoubleClick(object sender, EventArgs e)
        {
            if (PanelLed3.BackColor == Color.DarkGreen || PanelLed3.BackColor == Color.Lime) { PanelLed3.BackColor = Color.Silver; } else { PanelLed3.BackColor = Color.DarkGreen; }
        }
        private void PanelLed4_DoubleClick(object sender, EventArgs e)
        {
            if (PanelLed4.BackColor == Color.DarkGreen || PanelLed4.BackColor == Color.Lime) { PanelLed4.BackColor = Color.Silver; } else { PanelLed4.BackColor = Color.DarkGreen; }
        }
        private void PanelLed5_DoubleClick(object sender, EventArgs e)
        {
            if (PanelLed5.BackColor == Color.DarkGreen || PanelLed5.BackColor == Color.Lime) { PanelLed5.BackColor = Color.Silver; } else { PanelLed5.BackColor = Color.DarkGreen; }
        }
        private void PanelLed6_DoubleClick(object sender, EventArgs e)
        {
            if (PanelLed6.BackColor == Color.DarkGreen || PanelLed6.BackColor == Color.Lime) { PanelLed6.BackColor = Color.Silver; } else { PanelLed6.BackColor = Color.DarkGreen; }
        }
        private void PanelLed7_DoubleClick(object sender, EventArgs e)
        {
            if (PanelLed7.BackColor == Color.DarkGreen || PanelLed7.BackColor == Color.Lime) { PanelLed7.BackColor = Color.Silver; } else { PanelLed7.BackColor = Color.DarkGreen; }
        }
        private void PanelLed8_DoubleClick(object sender, EventArgs e)
        {
            if (PanelLed8.BackColor == Color.DarkGreen || PanelLed8.BackColor == Color.Lime) { PanelLed8.BackColor = Color.Silver; } else { PanelLed8.BackColor = Color.DarkGreen; }
        }
        private void PanelLed14_DoubleClick(object sender, EventArgs e)
        {
            if (PanelLed14.BackColor == Color.DarkGreen || PanelLed14.BackColor == Color.Lime) { PanelLed14.BackColor = Color.Silver; } else { PanelLed14.BackColor = Color.DarkGreen; }
        }

        private void PanelLed13_DoubleClick(object sender, EventArgs e)
        {
            if (PanelLed13.BackColor == Color.DarkGreen || PanelLed13.BackColor == Color.Lime) { PanelLed13.BackColor = Color.Silver; } else { PanelLed13.BackColor = Color.DarkGreen; }
        }

        private void PanelLed12_DoubleClick(object sender, EventArgs e)
        {
            if (PanelLed12.BackColor == Color.DarkGreen || PanelLed12.BackColor == Color.Lime) { PanelLed12.BackColor = Color.Silver; } else { PanelLed12.BackColor = Color.DarkGreen; }
        }

        private void PanelLed11_DoubleClick(object sender, EventArgs e)
        {
            if (PanelLed11.BackColor == Color.DarkGreen || PanelLed11.BackColor == Color.Lime) { PanelLed11.BackColor = Color.Silver; } else { PanelLed11.BackColor = Color.DarkGreen; }
        }

        private void PanelLed10_DoubleClick(object sender, EventArgs e)
        {
            if (PanelLed10.BackColor == Color.DarkGreen || PanelLed10.BackColor == Color.Lime) { PanelLed10.BackColor = Color.Silver; } else { PanelLed10.BackColor = Color.DarkGreen; }
        }

        private void PanelLed9_DoubleClick(object sender, EventArgs e)
        {
            if (PanelLed9.BackColor == Color.DarkGreen || PanelLed9.BackColor == Color.Lime) { PanelLed9.BackColor = Color.Silver; } else { PanelLed9.BackColor = Color.DarkGreen; }
        }

        private void MetroTile1_Click(object sender, EventArgs e)
        {
            if (CheckSalida1.Checked.Equals(true))
            {
                if (EmbalajeBoxGral.Text != "No Cambiar") { EmbalajeBox1.Text = EmbalajeBoxGral.Text; }
                if (TurnoBoxGral.Text != "No Cambiar") { TurnoBox1.Text = TurnoBoxGral.Text; }
                if (VariedadBoxGral.Text != "No Cambiar") { VariedadBox1.Text = VariedadBoxGral.Text; }
                if (CalibreBoxGral.Text != "No Cambiar") { CalibreBox1.Text = CalibreBoxGral.Text; }
            }
            if (CheckSalida2.Checked.Equals(true))
            {
                if (EmbalajeBoxGral.Text != "No Cambiar") { EmbalajeBox2.Text = EmbalajeBoxGral.Text; }
                if (TurnoBoxGral.Text != "No Cambiar") { TurnoBox2.Text = TurnoBoxGral.Text; }
                if (VariedadBoxGral.Text != "No Cambiar") { VariedadBox2.Text = VariedadBoxGral.Text; }
                if (CalibreBoxGral.Text != "No Cambiar") { CalibreBox2.Text = CalibreBoxGral.Text; }
            }
            if (CheckSalida3.Checked.Equals(true))
            {
                if (EmbalajeBoxGral.Text != "No Cambiar") { EmbalajeBox3.Text = EmbalajeBoxGral.Text; }
                if (TurnoBoxGral.Text != "No Cambiar") { TurnoBox3.Text = TurnoBoxGral.Text; }
                if (VariedadBoxGral.Text != "No Cambiar") { VariedadBox3.Text = VariedadBoxGral.Text; }
                if (CalibreBoxGral.Text != "No Cambiar") { CalibreBox3.Text = CalibreBoxGral.Text; }
            }
            if (CheckSalida4.Checked.Equals(true))
            {
                if (EmbalajeBoxGral.Text != "No Cambiar") { EmbalajeBox4.Text = EmbalajeBoxGral.Text; }
                if (TurnoBoxGral.Text != "No Cambiar") { TurnoBox4.Text = TurnoBoxGral.Text; }
                if (VariedadBoxGral.Text != "No Cambiar") { VariedadBox4.Text = VariedadBoxGral.Text; }
                if (CalibreBoxGral.Text != "No Cambiar") { CalibreBox4.Text = CalibreBoxGral.Text; }
            }
            if (CheckSalida5.Checked.Equals(true))
            {
                if (EmbalajeBoxGral.Text != "No Cambiar") { EmbalajeBox5.Text = EmbalajeBoxGral.Text; }
                if (TurnoBoxGral.Text != "No Cambiar") { TurnoBox5.Text = TurnoBoxGral.Text; }
                if (VariedadBoxGral.Text != "No Cambiar") { VariedadBox5.Text = VariedadBoxGral.Text; }
                if (CalibreBoxGral.Text != "No Cambiar") { CalibreBox5.Text = CalibreBoxGral.Text; }
            }
            if (CheckSalida6.Checked.Equals(true))
            {
                if (EmbalajeBoxGral.Text != "No Cambiar") { EmbalajeBox6.Text = EmbalajeBoxGral.Text; }
                if (TurnoBoxGral.Text != "No Cambiar") { TurnoBox6.Text = TurnoBoxGral.Text; }
                if (VariedadBoxGral.Text != "No Cambiar") { VariedadBox6.Text = VariedadBoxGral.Text; }
                if (CalibreBoxGral.Text != "No Cambiar") { CalibreBox6.Text = CalibreBoxGral.Text; }
            }
            if (CheckSalida7.Checked.Equals(true))
            {
                if (EmbalajeBoxGral.Text != "No Cambiar") { EmbalajeBox7.Text = EmbalajeBoxGral.Text; }
                if (TurnoBoxGral.Text != "No Cambiar") { TurnoBox7.Text = TurnoBoxGral.Text; }
                if (VariedadBoxGral.Text != "No Cambiar") { VariedadBox7.Text = VariedadBoxGral.Text; }
                if (CalibreBoxGral.Text != "No Cambiar") { CalibreBox7.Text = CalibreBoxGral.Text; }
            }
            if (CheckSalida8.Checked.Equals(true))
            {
                if (EmbalajeBoxGral.Text != "No Cambiar") { EmbalajeBox8.Text = EmbalajeBoxGral.Text; }
                if (TurnoBoxGral.Text != "No Cambiar") { TurnoBox8.Text = TurnoBoxGral.Text; }
                if (VariedadBoxGral.Text != "No Cambiar") { VariedadBox8.Text = VariedadBoxGral.Text; }
                if (CalibreBoxGral.Text != "No Cambiar") { CalibreBox8.Text = CalibreBoxGral.Text; }
            }
            if (CheckSalida9.Checked.Equals(true))
            {
                if (EmbalajeBoxGral.Text != "No Cambiar") { EmbalajeBox9.Text = EmbalajeBoxGral.Text; }
                if (TurnoBoxGral.Text != "No Cambiar") { TurnoBox9.Text = TurnoBoxGral.Text; }
                if (VariedadBoxGral.Text != "No Cambiar") { VariedadBox9.Text = VariedadBoxGral.Text; }
                if (CalibreBoxGral.Text != "No Cambiar") { CalibreBox9.Text = CalibreBoxGral.Text; }
            }
            if (CheckSalida10.Checked.Equals(true))
            {
                if (EmbalajeBoxGral.Text != "No Cambiar") { EmbalajeBox10.Text = EmbalajeBoxGral.Text; }
                if (TurnoBoxGral.Text != "No Cambiar") { TurnoBox10.Text = TurnoBoxGral.Text; }
                if (VariedadBoxGral.Text != "No Cambiar") { VariedadBox10.Text = VariedadBoxGral.Text; }
                if (CalibreBoxGral.Text != "No Cambiar") { CalibreBox10.Text = CalibreBoxGral.Text; }
            }
            if (CheckSalida11.Checked.Equals(true))
            {
                if (EmbalajeBoxGral.Text != "No Cambiar") { EmbalajeBox11.Text = EmbalajeBoxGral.Text; }
                if (TurnoBoxGral.Text != "No Cambiar") { TurnoBox11.Text = TurnoBoxGral.Text; }
                if (VariedadBoxGral.Text != "No Cambiar") { VariedadBox11.Text = VariedadBoxGral.Text; }
                if (CalibreBoxGral.Text != "No Cambiar") { CalibreBox11.Text = CalibreBoxGral.Text; }
            }
            if (CheckSalida12.Checked.Equals(true))
            {
                if (EmbalajeBoxGral.Text != "No Cambiar") { EmbalajeBox12.Text = EmbalajeBoxGral.Text; }
                if (TurnoBoxGral.Text != "No Cambiar") { TurnoBox12.Text = TurnoBoxGral.Text; }
                if (VariedadBoxGral.Text != "No Cambiar") { VariedadBox12.Text = VariedadBoxGral.Text; }
                if (CalibreBoxGral.Text != "No Cambiar") { CalibreBox12.Text = CalibreBoxGral.Text; }
            }
            if (CheckSalida13.Checked.Equals(true))
            {
                if (EmbalajeBoxGral.Text != "No Cambiar") { EmbalajeBox13.Text = EmbalajeBoxGral.Text; }
                if (TurnoBoxGral.Text != "No Cambiar") { TurnoBox13.Text = TurnoBoxGral.Text; }
                if (VariedadBoxGral.Text != "No Cambiar") { VariedadBox13.Text = VariedadBoxGral.Text; }
                if (CalibreBoxGral.Text != "No Cambiar") { CalibreBox13.Text = CalibreBoxGral.Text; }
            }
            if (CheckSalida14.Checked.Equals(true))
            {
                if (EmbalajeBoxGral.Text != "No Cambiar") { EmbalajeBox14.Text = EmbalajeBoxGral.Text; }
                if (TurnoBoxGral.Text != "No Cambiar") { TurnoBox14.Text = TurnoBoxGral.Text; }
                if (VariedadBoxGral.Text != "No Cambiar") { VariedadBox14.Text = VariedadBoxGral.Text; }
                if (CalibreBoxGral.Text != "No Cambiar") { CalibreBox14.Text = CalibreBoxGral.Text; }
            }
            if (CheckSalida15.Checked.Equals(true))
            {
                if (EmbalajeBoxGral.Text != "No Cambiar") { EmbalajeBox15.Text = EmbalajeBoxGral.Text; }
                if (TurnoBoxGral.Text != "No Cambiar") { TurnoBox15.Text = TurnoBoxGral.Text; }
                if (VariedadBoxGral.Text != "No Cambiar") { VariedadBox15.Text = VariedadBoxGral.Text; }
                if (CalibreBoxGral.Text != "No Cambiar") { CalibreBox15.Text = CalibreBoxGral.Text; }
            }
            if (CheckSalida16.Checked.Equals(true))
            {
                if (EmbalajeBoxGral.Text != "No Cambiar") { EmbalajeBox16.Text = EmbalajeBoxGral.Text; }
                if (TurnoBoxGral.Text != "No Cambiar") { TurnoBox16.Text = TurnoBoxGral.Text; }
                if (VariedadBoxGral.Text != "No Cambiar") { VariedadBox16.Text = VariedadBoxGral.Text; }
                if (CalibreBoxGral.Text != "No Cambiar") { CalibreBox16.Text = CalibreBoxGral.Text; }
            }
            if (CheckSalida17.Checked.Equals(true))
            {
                if (EmbalajeBoxGral.Text != "No Cambiar") { EmbalajeBox17.Text = EmbalajeBoxGral.Text; }
                if (TurnoBoxGral.Text != "No Cambiar") { TurnoBox17.Text = TurnoBoxGral.Text; }
                if (VariedadBoxGral.Text != "No Cambiar") { VariedadBox17.Text = VariedadBoxGral.Text; }
                if (CalibreBoxGral.Text != "No Cambiar") { CalibreBox17.Text = CalibreBoxGral.Text; }
            }
            if (CheckSalida18.Checked.Equals(true))
            {
                if (EmbalajeBoxGral.Text != "No Cambiar") { EmbalajeBox18.Text = EmbalajeBoxGral.Text; }
                if (TurnoBoxGral.Text != "No Cambiar") { TurnoBox18.Text = TurnoBoxGral.Text; }
                if (VariedadBoxGral.Text != "No Cambiar") { VariedadBox18.Text = VariedadBoxGral.Text; }
                if (CalibreBoxGral.Text != "No Cambiar") { CalibreBox18.Text = CalibreBoxGral.Text; }
            }
            if (CheckSalida19.Checked.Equals(true))
            {
                if (EmbalajeBoxGral.Text != "No Cambiar") { EmbalajeBox19.Text = EmbalajeBoxGral.Text; }
                if (TurnoBoxGral.Text != "No Cambiar") { TurnoBox19.Text = TurnoBoxGral.Text; }
                if (VariedadBoxGral.Text != "No Cambiar") { VariedadBox19.Text = VariedadBoxGral.Text; }
                if (CalibreBoxGral.Text != "No Cambiar") { CalibreBox19.Text = CalibreBoxGral.Text; }
            }
            if (CheckSalida20.Checked.Equals(true))
            {
                if (EmbalajeBoxGral.Text != "No Cambiar") { EmbalajeBox20.Text = EmbalajeBoxGral.Text; }
                if (TurnoBoxGral.Text != "No Cambiar") { TurnoBox20.Text = TurnoBoxGral.Text; }
                if (VariedadBoxGral.Text != "No Cambiar") { VariedadBox20.Text = VariedadBoxGral.Text; }
                if (CalibreBoxGral.Text != "No Cambiar") { CalibreBox20.Text = CalibreBoxGral.Text; }
            }
            if (CheckSalida21.Checked.Equals(true))
            {
                if (EmbalajeBoxGral.Text != "No Cambiar") { EmbalajeBox21.Text = EmbalajeBoxGral.Text; }
                if (TurnoBoxGral.Text != "No Cambiar") { TurnoBox21.Text = TurnoBoxGral.Text; }
                if (VariedadBoxGral.Text != "No Cambiar") { VariedadBox21.Text = VariedadBoxGral.Text; }
                if (CalibreBoxGral.Text != "No Cambiar") { CalibreBox21.Text = CalibreBoxGral.Text; }
            }
            if (CheckSalida22.Checked.Equals(true))
            {
                if (EmbalajeBoxGral.Text != "No Cambiar") { EmbalajeBox22.Text = EmbalajeBoxGral.Text; }
                if (TurnoBoxGral.Text != "No Cambiar") { TurnoBox22.Text = TurnoBoxGral.Text; }
                if (VariedadBoxGral.Text != "No Cambiar") { VariedadBox22.Text = VariedadBoxGral.Text; }
                if (CalibreBoxGral.Text != "No Cambiar") { CalibreBox22.Text = CalibreBoxGral.Text; }
            }
            if (CheckSalida23.Checked.Equals(true))
            {
                if (EmbalajeBoxGral.Text != "No Cambiar") { EmbalajeBox23.Text = EmbalajeBoxGral.Text; }
                if (TurnoBoxGral.Text != "No Cambiar") { TurnoBox23.Text = TurnoBoxGral.Text; }
                if (VariedadBoxGral.Text != "No Cambiar") { VariedadBox23.Text = VariedadBoxGral.Text; }
                if (CalibreBoxGral.Text != "No Cambiar") { CalibreBox23.Text = CalibreBoxGral.Text; }
            }
            if (CheckSalida24.Checked.Equals(true))
            {
                if (EmbalajeBoxGral.Text != "No Cambiar") { EmbalajeBox24.Text = EmbalajeBoxGral.Text; }
                if (TurnoBoxGral.Text != "No Cambiar") { TurnoBox24.Text = TurnoBoxGral.Text; }
                if (VariedadBoxGral.Text != "No Cambiar") { VariedadBox24.Text = VariedadBoxGral.Text; }
                if (CalibreBoxGral.Text != "No Cambiar") { CalibreBox24.Text = CalibreBoxGral.Text; }
            }
            if (CheckSalida25.Checked.Equals(true))
            {
                if (EmbalajeBoxGral.Text != "No Cambiar") { EmbalajeBox25.Text = EmbalajeBoxGral.Text; }
                if (TurnoBoxGral.Text != "No Cambiar") { TurnoBox25.Text = TurnoBoxGral.Text; }
                if (VariedadBoxGral.Text != "No Cambiar") { VariedadBox25.Text = VariedadBoxGral.Text; }
                if (CalibreBoxGral.Text != "No Cambiar") { CalibreBox25.Text = CalibreBoxGral.Text; }
            }
            if (CheckSalida26.Checked.Equals(true))
            {
                if (EmbalajeBoxGral.Text != "No Cambiar") { EmbalajeBox26.Text = EmbalajeBoxGral.Text; }
                if (TurnoBoxGral.Text != "No Cambiar") { TurnoBox26.Text = TurnoBoxGral.Text; }
                if (VariedadBoxGral.Text != "No Cambiar") { VariedadBox26.Text = VariedadBoxGral.Text; }
                if (CalibreBoxGral.Text != "No Cambiar") { CalibreBox26.Text = CalibreBoxGral.Text; }
            }
            if (CheckSalida27.Checked.Equals(true))
            {
                if (EmbalajeBoxGral.Text != "No Cambiar") { EmbalajeBox27.Text = EmbalajeBoxGral.Text; }
                if (TurnoBoxGral.Text != "No Cambiar") { TurnoBox27.Text = TurnoBoxGral.Text; }
                if (VariedadBoxGral.Text != "No Cambiar") { VariedadBox27.Text = VariedadBoxGral.Text; }
                if (CalibreBoxGral.Text != "No Cambiar") { CalibreBox27.Text = CalibreBoxGral.Text; }
            }
            if (CheckSalida28.Checked.Equals(true))
            {
                if (EmbalajeBoxGral.Text != "No Cambiar") { EmbalajeBox28.Text = EmbalajeBoxGral.Text; }
                if (TurnoBoxGral.Text != "No Cambiar") { TurnoBox28.Text = TurnoBoxGral.Text; }
                if (VariedadBoxGral.Text != "No Cambiar") { VariedadBox28.Text = VariedadBoxGral.Text; }
                if (CalibreBoxGral.Text != "No Cambiar") { CalibreBox28.Text = CalibreBoxGral.Text; }
            }
        }

        
        private void CheckTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTodos.Checked.Equals(true))
            {
                CheckSalida1.Checked = true;
                CheckSalida2.Checked = true;
                CheckSalida3.Checked = true;
                CheckSalida4.Checked = true;
                CheckSalida5.Checked = true;
                CheckSalida6.Checked = true;
                CheckSalida7.Checked = true;
                CheckSalida8.Checked = true;
                CheckSalida9.Checked = true;
                CheckSalida10.Checked = true;
                CheckSalida11.Checked = true;
                CheckSalida12.Checked = true;
                CheckSalida13.Checked = true;
                CheckSalida14.Checked = true;
                CheckSalida15.Checked = true;
                CheckSalida16.Checked = true;
                CheckSalida17.Checked = true;
                CheckSalida18.Checked = true;
                CheckSalida19.Checked = true;
                CheckSalida20.Checked = true;
                CheckSalida21.Checked = true;
                CheckSalida22.Checked = true;
                CheckSalida23.Checked = true;
                CheckSalida24.Checked = true;
                CheckSalida25.Checked = true;
                CheckSalida26.Checked = true;
                CheckSalida27.Checked = true;
                CheckSalida28.Checked = true;
            }
            else
            {
                CheckSalida1.Checked = false;
                CheckSalida2.Checked = false;
                CheckSalida3.Checked = false;
                CheckSalida4.Checked = false;
                CheckSalida5.Checked = false;
                CheckSalida6.Checked = false;
                CheckSalida7.Checked = false;
                CheckSalida8.Checked = false;
                CheckSalida9.Checked = false;
                CheckSalida10.Checked = false;
                CheckSalida11.Checked = false;
                CheckSalida12.Checked = false;
                CheckSalida13.Checked = false;
                CheckSalida14.Checked = false;
                CheckSalida15.Checked = false;
                CheckSalida16.Checked = false;
                CheckSalida17.Checked = false;
                CheckSalida18.Checked = false;
                CheckSalida19.Checked = false;
                CheckSalida20.Checked = false;
                CheckSalida21.Checked = false;
                CheckSalida22.Checked = false;
                CheckSalida23.Checked = false;
                CheckSalida24.Checked = false;
                CheckSalida25.Checked = false;
                CheckSalida26.Checked = false;
                CheckSalida27.Checked = false;
                CheckSalida28.Checked = false;
            }
        }

        private void PanelLed21_DoubleClick(object sender, EventArgs e)
        {
            if (PanelLed21.BackColor == Color.DarkGreen || PanelLed21.BackColor == Color.Lime) { PanelLed21.BackColor = Color.Silver; } else { PanelLed21.BackColor = Color.DarkGreen; }
        }

        private void PanelLed20_DoubleClick(object sender, EventArgs e)
        {
            if (PanelLed20.BackColor == Color.DarkGreen || PanelLed20.BackColor == Color.Lime) { PanelLed20.BackColor = Color.Silver; } else { PanelLed20.BackColor = Color.DarkGreen; }
        }

        private void PanelLed15_DoubleClick(object sender, EventArgs e)
        {
            if (PanelLed15.BackColor == Color.DarkGreen || PanelLed15.BackColor == Color.Lime) { PanelLed15.BackColor = Color.Silver; } else { PanelLed15.BackColor = Color.DarkGreen; }
        }

        private void PanelLed16_DoubleClick(object sender, EventArgs e)
        {
            if (PanelLed16.BackColor == Color.DarkGreen || PanelLed16.BackColor == Color.Lime) { PanelLed16.BackColor = Color.Silver; } else { PanelLed16.BackColor = Color.DarkGreen; }
        }

        private void PanelLed17_DoubleClick(object sender, EventArgs e)
        {
            if (PanelLed17.BackColor == Color.DarkGreen || PanelLed17.BackColor == Color.Lime) { PanelLed17.BackColor = Color.Silver; } else { PanelLed17.BackColor = Color.DarkGreen; }
        }

        private void PanelLed18_DoubleClick(object sender, EventArgs e)
        {
            if (PanelLed18.BackColor == Color.DarkGreen || PanelLed18.BackColor == Color.Lime) { PanelLed18.BackColor = Color.Silver; } else { PanelLed18.BackColor = Color.DarkGreen; }
        }

        private void PanelLed19_DoubleClick(object sender, EventArgs e)
        {
            if (PanelLed19.BackColor == Color.DarkGreen || PanelLed19.BackColor == Color.Lime) { PanelLed19.BackColor = Color.Silver; } else { PanelLed19.BackColor = Color.DarkGreen; }
        }

        private void PanelLed22_DoubleClick(object sender, EventArgs e)
        {
            if (PanelLed22.BackColor == Color.DarkGreen || PanelLed22.BackColor == Color.Lime) { PanelLed22.BackColor = Color.Silver; } else { PanelLed22.BackColor = Color.DarkGreen; }
        }

        private void PanelLed23_DoubleClick(object sender, EventArgs e)
        {
            if (PanelLed23.BackColor == Color.DarkGreen || PanelLed23.BackColor == Color.Lime) { PanelLed23.BackColor = Color.Silver; } else { PanelLed23.BackColor = Color.DarkGreen; }
        }

        private void PanelLed24_DoubleClick(object sender, EventArgs e)
        {
            if (PanelLed24.BackColor == Color.DarkGreen || PanelLed24.BackColor == Color.Lime) { PanelLed24.BackColor = Color.Silver; } else { PanelLed24.BackColor = Color.DarkGreen; }
        }
        private void PanelLed25_DoubleClick(object sender, EventArgs e)
        {
            if (PanelLed25.BackColor == Color.DarkGreen || PanelLed25.BackColor == Color.Lime) { PanelLed25.BackColor = Color.Silver; } else { PanelLed25.BackColor = Color.DarkGreen; }
        }

        private void PanelLed26_DoubleClick(object sender, EventArgs e)
        {
            if (PanelLed26.BackColor == Color.DarkGreen || PanelLed26.BackColor == Color.Lime) { PanelLed26.BackColor = Color.Silver; } else { PanelLed26.BackColor = Color.DarkGreen; }
        }

        private void PanelLed27_DoubleClick(object sender, EventArgs e)
        {
            if (PanelLed27.BackColor == Color.DarkGreen || PanelLed27.BackColor == Color.Lime) { PanelLed27.BackColor = Color.Silver; } else { PanelLed27.BackColor = Color.DarkGreen; }
        }

        private void PanelLed28_DoubleClick(object sender, EventArgs e)
        {
            if (PanelLed28.BackColor == Color.DarkGreen || PanelLed28.BackColor == Color.Lime) { PanelLed28.BackColor = Color.Silver; } else { PanelLed28.BackColor = Color.DarkGreen; }
        }
        private void BtnStopImpresion_Click(object sender, EventArgs e)
        {
            if(BtnStopImpresion.Text == "Parar Impresiones")
            {
                BtnStopImpresion.Text = "Iniciar Impresiones";
                PanelLed1.BackColor = Color.Silver;
                PanelLed2.BackColor = Color.Silver;
                PanelLed3.BackColor = Color.Silver;
                PanelLed4.BackColor = Color.Silver;
                PanelLed5.BackColor = Color.Silver;
                PanelLed6.BackColor = Color.Silver;
                PanelLed7.BackColor = Color.Silver;
                PanelLed8.BackColor = Color.Silver;
                PanelLed9.BackColor = Color.Silver;
                PanelLed10.BackColor = Color.Silver;
                PanelLed11.BackColor = Color.Silver;
                PanelLed12.BackColor = Color.Silver;
                PanelLed13.BackColor = Color.Silver;
                PanelLed14.BackColor = Color.Silver;
                PanelLed15.BackColor = Color.Silver;
                PanelLed16.BackColor = Color.Silver;
                PanelLed17.BackColor = Color.Silver;
                PanelLed18.BackColor = Color.Silver;
                PanelLed19.BackColor = Color.Silver;
                PanelLed20.BackColor = Color.Silver;
                PanelLed21.BackColor = Color.Silver;
                PanelLed22.BackColor = Color.Silver;
                PanelLed23.BackColor = Color.Silver;
                PanelLed24.BackColor = Color.Silver;
                PanelLed25.BackColor = Color.Silver;
                PanelLed26.BackColor = Color.Silver;
                PanelLed27.BackColor = Color.Silver;
                PanelLed28.BackColor = Color.Silver;
            }
            else
            {
                BtnStopImpresion.Text = "Parar Impresiones";
                PanelLed1.BackColor = Color.DarkGreen;
                PanelLed2.BackColor = Color.DarkGreen;
                PanelLed3.BackColor = Color.DarkGreen;
                PanelLed4.BackColor = Color.DarkGreen;
                PanelLed5.BackColor = Color.DarkGreen;
                PanelLed6.BackColor = Color.DarkGreen;
                PanelLed7.BackColor = Color.DarkGreen;
                PanelLed8.BackColor = Color.DarkGreen;
                PanelLed9.BackColor = Color.DarkGreen;
                PanelLed10.BackColor = Color.DarkGreen;
                PanelLed11.BackColor = Color.DarkGreen;
                PanelLed12.BackColor = Color.DarkGreen;
                PanelLed13.BackColor = Color.DarkGreen;
                PanelLed14.BackColor = Color.DarkGreen;
                PanelLed15.BackColor = Color.DarkGreen;
                PanelLed16.BackColor = Color.DarkGreen;
                PanelLed17.BackColor = Color.DarkGreen;
                PanelLed18.BackColor = Color.DarkGreen;
                PanelLed19.BackColor = Color.DarkGreen;
                PanelLed20.BackColor = Color.DarkGreen;
                PanelLed21.BackColor = Color.DarkGreen;
                PanelLed22.BackColor = Color.DarkGreen;
                PanelLed23.BackColor = Color.DarkGreen;
                PanelLed24.BackColor = Color.DarkGreen;
                PanelLed25.BackColor = Color.DarkGreen;
                PanelLed26.BackColor = Color.DarkGreen;
                PanelLed27.BackColor = Color.DarkGreen;
                PanelLed28.BackColor = Color.DarkGreen;
            }
        }

        private void Timer_sensores_Tick(object sender, EventArgs e)
        {
            // Revisar sensores de salidas para imprimir etiqueta
            try
            {
                ObtenerConexion1();
                MySqlCommand _comando = new MySqlCommand(String.Format(
                "SELECT S1,S2,S3,S4,S5,S6,S7,S8,S9,S10,S11,S12,S13,S14,S15,S16,S17,S18,S19,S20,S21,S22,S23,S24,S25,S26,S27,S28 FROM entradas"), conectar1);
                MySqlDataReader _reader = _comando.ExecuteReader();
                while (_reader.Read())
                {
                    //actualizo leds lineas 
                    if (_reader.GetInt32(0) > 0) { if (PanelLed1.BackColor != Color.Silver) { PanelLed1.BackColor = Color.Lime; } } else { if (PanelLed1.BackColor != Color.Silver) { PanelLed1.BackColor = Color.DarkGreen; } }
                    if (_reader.GetInt32(1) > 0) { if (PanelLed2.BackColor != Color.Silver) { PanelLed2.BackColor = Color.Lime; } } else { if (PanelLed2.BackColor != Color.Silver) { PanelLed2.BackColor = Color.DarkGreen; } }
                    if (_reader.GetInt32(2) > 0) { if (PanelLed3.BackColor != Color.Silver) { PanelLed3.BackColor = Color.Lime; } } else { if (PanelLed3.BackColor != Color.Silver) { PanelLed3.BackColor = Color.DarkGreen; } }
                    if (_reader.GetInt32(3) > 0) { if (PanelLed4.BackColor != Color.Silver) { PanelLed4.BackColor = Color.Lime; } } else { if (PanelLed4.BackColor != Color.Silver) { PanelLed4.BackColor = Color.DarkGreen; } }
                    if (_reader.GetInt32(4) > 0) { if (PanelLed5.BackColor != Color.Silver) { PanelLed5.BackColor = Color.Lime; } } else { if (PanelLed5.BackColor != Color.Silver) { PanelLed5.BackColor = Color.DarkGreen; } }
                    if (_reader.GetInt32(5) > 0) { if (PanelLed6.BackColor != Color.Silver) { PanelLed6.BackColor = Color.Lime; } } else { if (PanelLed6.BackColor != Color.Silver) { PanelLed6.BackColor = Color.DarkGreen; } }
                    if (_reader.GetInt32(6) > 0) { if (PanelLed7.BackColor != Color.Silver) { PanelLed7.BackColor = Color.Lime; } } else { if (PanelLed7.BackColor != Color.Silver) { PanelLed7.BackColor = Color.DarkGreen; } }
                    if (_reader.GetInt32(7) > 0) { if (PanelLed8.BackColor != Color.Silver) { PanelLed8.BackColor = Color.Lime; } } else { if (PanelLed8.BackColor != Color.Silver) { PanelLed8.BackColor = Color.DarkGreen; } }
                    if (_reader.GetInt32(8) > 0) { if (PanelLed9.BackColor != Color.Silver) { PanelLed9.BackColor = Color.Lime; } } else { if (PanelLed9.BackColor != Color.Silver) { PanelLed9.BackColor = Color.DarkGreen; } }
                    if (_reader.GetInt32(9) > 0) { if (PanelLed10.BackColor != Color.Silver) { PanelLed10.BackColor = Color.Lime; } } else { if (PanelLed10.BackColor != Color.Silver) { PanelLed10.BackColor = Color.DarkGreen; } }
                    if (_reader.GetInt32(10) > 0) { if (PanelLed11.BackColor != Color.Silver) { PanelLed11.BackColor = Color.Lime; } } else { if (PanelLed11.BackColor != Color.Silver) { PanelLed11.BackColor = Color.DarkGreen; } }
                    if (_reader.GetInt32(11) > 0) { if (PanelLed12.BackColor != Color.Silver) { PanelLed12.BackColor = Color.Lime; } } else { if (PanelLed12.BackColor != Color.Silver) { PanelLed12.BackColor = Color.DarkGreen; } }
                    if (_reader.GetInt32(12) > 0) { if (PanelLed13.BackColor != Color.Silver) { PanelLed13.BackColor = Color.Lime; } } else { if (PanelLed13.BackColor != Color.Silver) { PanelLed13.BackColor = Color.DarkGreen; } }
                    if (_reader.GetInt32(13) > 0) { if (PanelLed14.BackColor != Color.Silver) { PanelLed14.BackColor = Color.Lime; } } else { if (PanelLed14.BackColor != Color.Silver) { PanelLed14.BackColor = Color.DarkGreen; } }
                    if (_reader.GetInt32(14) > 0) { if (PanelLed15.BackColor != Color.Silver) { PanelLed15.BackColor = Color.Lime; } } else { if (PanelLed15.BackColor != Color.Silver) { PanelLed15.BackColor = Color.DarkGreen; } }
                    if (_reader.GetInt32(15) > 0) { if (PanelLed16.BackColor != Color.Silver) { PanelLed16.BackColor = Color.Lime; } } else { if (PanelLed16.BackColor != Color.Silver) { PanelLed16.BackColor = Color.DarkGreen; } }
                    if (_reader.GetInt32(16) > 0) { if (PanelLed17.BackColor != Color.Silver) { PanelLed17.BackColor = Color.Lime; } } else { if (PanelLed17.BackColor != Color.Silver) { PanelLed17.BackColor = Color.DarkGreen; } }
                    if (_reader.GetInt32(17) > 0) { if (PanelLed18.BackColor != Color.Silver) { PanelLed18.BackColor = Color.Lime; } } else { if (PanelLed18.BackColor != Color.Silver) { PanelLed18.BackColor = Color.DarkGreen; } }
                    if (_reader.GetInt32(18) > 0) { if (PanelLed19.BackColor != Color.Silver) { PanelLed19.BackColor = Color.Lime; } } else { if (PanelLed19.BackColor != Color.Silver) { PanelLed19.BackColor = Color.DarkGreen; } }
                    if (_reader.GetInt32(19) > 0) { if (PanelLed20.BackColor != Color.Silver) { PanelLed20.BackColor = Color.Lime; } } else { if (PanelLed20.BackColor != Color.Silver) { PanelLed20.BackColor = Color.DarkGreen; } }
                    if (_reader.GetInt32(20) > 0) { if (PanelLed21.BackColor != Color.Silver) { PanelLed21.BackColor = Color.Lime; } } else { if (PanelLed21.BackColor != Color.Silver) { PanelLed21.BackColor = Color.DarkGreen; } }
                    if (_reader.GetInt32(21) > 0) { if (PanelLed22.BackColor != Color.Silver) { PanelLed22.BackColor = Color.Lime; } } else { if (PanelLed22.BackColor != Color.Silver) { PanelLed22.BackColor = Color.DarkGreen; } }
                    if (_reader.GetInt32(22) > 0) { if (PanelLed23.BackColor != Color.Silver) { PanelLed23.BackColor = Color.Lime; } } else { if (PanelLed23.BackColor != Color.Silver) { PanelLed23.BackColor = Color.DarkGreen; } }
                    if (_reader.GetInt32(23) > 0) { if (PanelLed24.BackColor != Color.Silver) { PanelLed24.BackColor = Color.Lime; } } else { if (PanelLed24.BackColor != Color.Silver) { PanelLed24.BackColor = Color.DarkGreen; } }
                    if (_reader.GetInt32(24) > 0) { if (PanelLed25.BackColor != Color.Silver) { PanelLed25.BackColor = Color.Lime; } } else { if (PanelLed25.BackColor != Color.Silver) { PanelLed25.BackColor = Color.DarkGreen; } }
                    if (_reader.GetInt32(25) > 0) { if (PanelLed26.BackColor != Color.Silver) { PanelLed26.BackColor = Color.Lime; } } else { if (PanelLed26.BackColor != Color.Silver) { PanelLed26.BackColor = Color.DarkGreen; } }
                    if (_reader.GetInt32(26) > 0) { if (PanelLed27.BackColor != Color.Silver) { PanelLed27.BackColor = Color.Lime; } } else { if (PanelLed27.BackColor != Color.Silver) { PanelLed27.BackColor = Color.DarkGreen; } }
                    if (_reader.GetInt32(27) > 0) { if (PanelLed28.BackColor != Color.Silver) { PanelLed28.BackColor = Color.Lime; } } else { if (PanelLed28.BackColor != Color.Silver) { PanelLed28.BackColor = Color.DarkGreen; } }
                }
                conectar1.Close();
            }
            catch (Exception ex)
            {
                // nada
                MessageBox.Show(ex.ToString(), "ERROR");
            }
        }
        //private void PanelLedP2_DoubleClick(object sender, EventArgs e)
        //{
        //    if (PanelLedP2.BackColor == Color.DarkGreen || PanelLedP2.BackColor == Color.Lime) { PanelLedP2.BackColor = Color.Silver; } else { PanelLedP2.BackColor = Color.DarkGreen; }
        //}

        //private void PanelLedP1_DoubleClick(object sender, EventArgs e)
        //{
        //    if (PanelLedP1.BackColor == Color.DarkGreen || PanelLedP1.BackColor == Color.Lime) { PanelLedP1.BackColor = Color.Silver; } else { PanelLedP1.BackColor = Color.DarkGreen; }
        //}

        //private void PanelLedC2_DoubleClick(object sender, EventArgs e)
        //{
        //    if (PanelLedC2.BackColor == Color.DarkGreen || PanelLedC2.BackColor == Color.Lime) { PanelLedC2.BackColor = Color.Silver; } else { PanelLedC2.BackColor = Color.DarkGreen; }
        //}

        //private void PanelLedC1_DoubleClick(object sender, EventArgs e)
        //{
        //    if (PanelLedC1.BackColor == Color.DarkGreen || PanelLedC1.BackColor == Color.Lime) { PanelLedC1.BackColor = Color.Silver; } else { PanelLedC1.BackColor = Color.DarkGreen; }
        //}

        private void PanelLed1_BackColorChanged(object sender, EventArgs e)
        {
            if (PanelLed1.BackColor.ToString().Equals("Color [Lime]"))
            {
                saposalida = "C1";
                contadorSC1 += 1;
                impresora = "IMPRESORA1";
                BtnImp1_Click(sender, e);
            }
        }

        private void PanelLed2_BackColorChanged(object sender, EventArgs e)
        {
            if (PanelLed2.BackColor.ToString().Equals("Color [Lime]"))
            {
                saposalida = "C2";
                contadorSC2 += 1;
                impresora = "IMPRESORA1";
                BtnImp1_Click(sender, e);
            }
        }

        private void PanelLed3_BackColorChanged(object sender, EventArgs e)
        {
            if (PanelLed3.BackColor.ToString().Equals("Color [Lime]"))
            {
                saposalida = "1";
                contadorS1 += 1;
                impresora = "IMPRESORA2";
                BtnImp1_Click(sender, e);
            }
        }

        private void PanelLed4_BackColorChanged(object sender, EventArgs e)
        {
            if (PanelLed4.BackColor.ToString().Equals("Color [Lime]"))
            {
                saposalida = "2";
                contadorS2 += 1;
                impresora = "IMPRESORA2";
                BtnImp1_Click(sender, e);
            }
        }

        private void PanelLed5_BackColorChanged(object sender, EventArgs e)
        {
            if (PanelLed5.BackColor.ToString().Equals("Color [Lime]"))
            {
                saposalida = "3";
                contadorS3 += 1;
                impresora = "IMPRESORA3";
                BtnImp1_Click(sender, e);
            }
        }

        private void PanelLed6_BackColorChanged(object sender, EventArgs e)
        {
            if (PanelLed6.BackColor.ToString().Equals("Color [Lime]"))
            {
                saposalida = "4";
                contadorS4 += 1;
                impresora = "IMPRESORA3";
                BtnImp1_Click(sender, e);
            }
        }

        private void PanelLed7_BackColorChanged(object sender, EventArgs e)
        {
            if (PanelLed7.BackColor.ToString().Equals("Color [Lime]"))
            {
                saposalida = "5";
                contadorS5 += 1;
                impresora = "IMPRESORA4";
                BtnImp1_Click(sender, e);
            }
        }

        private void PanelLed8_BackColorChanged(object sender, EventArgs e)
        {
            if (PanelLed8.BackColor.ToString().Equals("Color [Lime]"))
            {
                saposalida = "6";
                contadorS6 += 1;
                impresora = "IMPRESORA4";
                BtnImp1_Click(sender, e);
            }
        }

        private void PanelLed9_BackColorChanged(object sender, EventArgs e)
        {
            if (PanelLed9.BackColor.ToString().Equals("Color [Lime]"))
            {
                saposalida = "7";
                contadorS7 += 1;
                impresora = "IMPRESORA5";
                BtnImp1_Click(sender, e);
            }
        }

        private void PanelLed10_BackColorChanged(object sender, EventArgs e)
        {
            if (PanelLed10.BackColor.ToString().Equals("Color [Lime]"))
            {
                saposalida = "8";
                contadorS8 += 1;
                impresora = "IMPRESORA5";
                BtnImp1_Click(sender, e);
            }
        }

        private void PanelLed11_BackColorChanged(object sender, EventArgs e)
        {
            if (PanelLed11.BackColor.ToString().Equals("Color [Lime]"))
            {
                saposalida = "9";
                contadorS9 += 1;
                impresora = "IMPRESORA6";
                BtnImp1_Click(sender, e);
            }
        }

        private void PanelLed12_BackColorChanged(object sender, EventArgs e)
        {
            if (PanelLed12.BackColor.ToString().Equals("Color [Lime]"))
            {
                saposalida = "10";
                contadorS10 += 1;
                impresora = "IMPRESORA6";
                BtnImp1_Click(sender, e);
            }
        }

        private void PanelLed13_BackColorChanged(object sender, EventArgs e)
        {
            if (PanelLed13.BackColor.ToString().Equals("Color [Lime]"))
            {
                saposalida = "11";
                contadorS11 += 1;
                impresora = "IMPRESORA7";
                BtnImp1_Click(sender, e);
            }
        }

        private void PanelLed14_BackColorChanged(object sender, EventArgs e)
        {
            if (PanelLed14.BackColor.ToString().Equals("Color [Lime]"))
            {
                saposalida = "12";
                contadorS12 += 1;
                impresora = "IMPRESORA7";
                BtnImp1_Click(sender, e);
            }
        }

        private void PanelLed15_BackColorChanged(object sender, EventArgs e)
        {
            if (PanelLed15.BackColor.ToString().Equals("Color [Lime]"))
            {
                saposalida = "13";
                contadorS13 += 1;
                impresora = "IMPRESORA8";
                BtnImp1_Click(sender, e);
            }
        }

        private void PanelLed16_BackColorChanged(object sender, EventArgs e)
        {
            if (PanelLed16.BackColor.ToString().Equals("Color [Lime]"))
            {
                saposalida = "14";
                contadorS14 += 1;
                impresora = "IMPRESORA8";
                BtnImp1_Click(sender, e);
            }
        }

        private void PanelLed17_BackColorChanged(object sender, EventArgs e)
        {
            if (PanelLed17.BackColor.ToString().Equals("Color [Lime]"))
            {
                saposalida = "15";
                contadorS15 += 1;
                impresora = "IMPRESORA9";
                BtnImp1_Click(sender, e);
            }
        }

        private void PanelLed18_BackColorChanged(object sender, EventArgs e)
        {
            if (PanelLed18.BackColor.ToString().Equals("Color [Lime]"))
            {
                saposalida = "16";
                contadorS16 += 1;
                impresora = "IMPRESORA9";
                BtnImp1_Click(sender, e);
            }
        }

        private void PanelLed19_BackColorChanged(object sender, EventArgs e)
        {
            if (PanelLed19.BackColor.ToString().Equals("Color [Lime]"))
            {
                saposalida = "17";
                contadorS17 += 1;
                impresora = "IMPRESORA10";
                BtnImp1_Click(sender, e);
            }
        }

        private void PanelLed20_BackColorChanged(object sender, EventArgs e)
        {
            if (PanelLed20.BackColor.ToString().Equals("Color [Lime]"))
            {
                saposalida = "18";
                contadorS18 += 1;
                impresora = "IMPRESORA10";
                BtnImp1_Click(sender, e);
            }
        }

        private void PanelLed21_BackColorChanged(object sender, EventArgs e)
        {
            if (PanelLed21.BackColor.ToString().Equals("Color [Lime]"))
            {
                saposalida = "19";
                contadorS19 += 1;
                impresora = "IMPRESORA11";
                BtnImp1_Click(sender, e);
            }
        }

        private void PanelLed22_BackColorChanged(object sender, EventArgs e)
        {
            if (PanelLed22.BackColor.ToString().Equals("Color [Lime]"))
            {
                saposalida = "20";
                contadorS20 += 1;
                impresora = "IMPRESORA11";
                BtnImp1_Click(sender, e);
            }
        }

        private void PanelLed23_BackColorChanged(object sender, EventArgs e)
        {
            if (PanelLed23.BackColor.ToString().Equals("Color [Lime]"))
            {
                saposalida = "21";
                contadorS21 += 1;
                impresora = "IMPRESORA12";
                BtnImp1_Click(sender, e);
            }
        }

        private void PanelLed24_BackColorChanged(object sender, EventArgs e)
        {
            if (PanelLed24.BackColor.ToString().Equals("Color [Lime]"))
            {
                saposalida = "22";
                contadorS22 += 1;
                impresora = "IMPRESORA12";
                BtnImp1_Click(sender, e);
            }
        }

    }
}
