using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace EtiquetasSantaCatalina1._0
{
    class ConexionDB
    {
        public static SqlConnection cn,cn2;
        public static SqlDataReader reader;
        public static SqlCommand comando;

        public ConexionDB()
        {
            cn = new SqlConnection("Data Source=192.168.3.112;Initial Catalog=etiquetado_system;Persist Security Info=True;User ID=spsi2;Password=Spsi.2018");
            cn.Open();
            cn2 = new SqlConnection("Data Source=192.168.3.112;Initial Catalog=SFruticola_SPSI;Persist Security Info=True;User ID=spsi2;Password=Spsi.2018");
            cn2.Open();
        }
        
        public void Cerrar()
        {
            cn.Close();
        }
        public void GuardarEtiqueta(string codigo_ean13, string especie, string variedad,string clasificacion, string calibre,string embalaje, string productor, string categoria, string lote_huerto, string proceso_n, string salida, string tipo_frio, int turno, string fecha, string fda, string ggn)
        {
            try
            {
                comando = new SqlCommand("INSERT INTO etiquetado_system.dbo.etiqueta(codigo_interno, codigo_ean13, especie, variedad, clasificacion, calibre, embalaje, productor, categoria, lote_huerto, proceso_n, cuartel, salida, tipo_frio, turno, fecha, fda, ggn, paso1, codigo_tarja, cuadrilla) "
                    + "VALUES('" + codigo_ean13 + "', '" + codigo_ean13 + "', '" + especie + "', '" + variedad + "', '" + clasificacion + "', '" + calibre + "', '" + embalaje + "', '" + productor + "', '" + categoria + "', '" + lote_huerto + "', '" + proceso_n + "', NULL, '" + salida + "', '" + tipo_frio + "', " + turno + ", '" + fecha + "', '" + fda + "', '" + ggn + "', 0, NULL, NULL)", cn);
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //
            }
        }
        public void GuardarMasivo(string cod_sql)
        {
            try
            {
                comando = new SqlCommand(cod_sql, cn);
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //
            }
        }
        public string ObtenerExportadora(string codigo)
        {
            string exportadora = "";
            try
            {
                //comando = new SqlCommand("SELECT etiquetado_system.dbo.productores.region FROM etiquetado_system.dbo.productores", cn);
                comando = new SqlCommand("SELECT SFruticola_SPSI.dba.clientes.cli_nombre FROM SFruticola_SPSI.dba.clientes WHERE SFruticola_SPSI.dba.clientes.cli_codigo = '" + codigo + "'", cn2);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    exportadora = reader.GetString(0);
                }
            }
            catch (Exception ex)
            {
                /// 
            }
            return exportadora;
        }
        public string ObtenerExportadora_vaciado()
        {
            string exportadora = "--";
            try
            {
                comando = new SqlCommand("SELECT TOP 1 convert(varchar(10),SFruticola_SPSI.dba.TRA.cli_codigo) FROM SFruticola_SPSI.dba.TRA WHERE SFruticola_SPSI.dba.TRA.espe_codigo = '" + Form2.codigo_especie + "' ORDER BY SFruticola_SPSI.dba.TRA.FechaDigi DESC", cn2);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    exportadora = reader.GetString(0);
                }
            }
            catch (Exception ex)
            {
                //
            }
            return exportadora;
        }
        public Boolean Existe_codigo(string codigo)
        {
            Boolean existe = false;
            try
            {
                comando = new SqlCommand("SELECT etiquetado_system.dbo.etiqueta.codigo_interno, etiquetado_system.dbo.etiqueta.codigo_ean13 FROM etiquetado_system.dbo.etiqueta WHERE etiquetado_system.dbo.etiqueta.codigo_interno='" + codigo + "' OR etiquetado_system.dbo.etiqueta.codigo_ean13='" + codigo + "'", cn);
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    //existe
                    existe = true;
                }
            }
            catch (Exception ex)
            {
                /// 
            }
            
            return existe;
        }
        public string[] CargaCalibres()
        {
            comando = new SqlCommand("SELECT etiquetado_system.dbo.calibres_ciruela.calibre FROM etiquetado_system.dbo.calibres_ciruela ORDER BY etiquetado_system.dbo.calibres_ciruela.calibre ASC", cn);
            reader = comando.ExecuteReader();

            int i = 0;
            string[] calibres = new string[50];
            while (reader.Read())
            {
                calibres[i] = (reader.GetString(0));
                i = i + 1;
            }
            return calibres;
        }

        // FUNCIONES PARA BD SFruticola_SPSI
        public string ObtenerCSG_Vaciado()
        {
            string csg_vaciado = "--";
            try
            {
                comando = new SqlCommand("SELECT TOP 1 convert(varchar(10),SFruticola_SPSI.dba.TRA.prod_codigo) FROM SFruticola_SPSI.dba.TRA ORDER WHERE SFruticola_SPSI.dba.TRA.espe_codigo = '" + Form2.codigo_especie + "' BY SFruticola_SPSI.dba.TRA.FechaDigi DESC", cn2);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    csg_vaciado = reader.GetString(0);
                }
            }
            catch (Exception ex)
            {
                /// 
            }
            return csg_vaciado;
        }
        public string ObtenerProductor_Vaciado()
        {
            string productor_nombre = "--";
            try
            {
                comando = new SqlCommand("SELECT TOP 1 SFruticola_SPSI.dba.productores.prod_nombre FROM SFruticola_SPSI.dba.TRA, SFruticola_SPSI.dba.productores WHERE SFruticola_SPSI.dba.TRA.prod_codigo = SFruticola_SPSI.dba.productores.prod_codigo AND SFruticola_SPSI.dba.TRA.espe_codigo = '" + Form2.codigo_especie + "' ORDER BY SFruticola_SPSI.dba.TRA.FechaDigi DESC", cn2);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    productor_nombre = reader.GetString(0);
                }
            }
            catch (Exception ex)
            {
                /// 
            }
            return productor_nombre;
        }
        //public string ObtenerNumeroBins_Vaciado()
        //{
        //    string numero_bins = "--";
        //    try
        //    {
        //        comando = new SqlCommand("SELECT etiquetado_system.dbo.ingreso.codigo_bins FROM etiquetado_system.dbo.ingreso WHERE etiquetado_system.dbo.ingreso.id = '1'", cn);
        //        SqlDataReader reader = comando.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            numero_bins = reader.GetString(0);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        /// 
        //    }
        //    return numero_bins;
        //}
        public string ObtenerVariedad_Vaciado()
        {
            string variedad = "--";
            try
            {
                comando = new SqlCommand("SELECT TOP 1 SFruticola_SPSI.dba.variedades.vari_nombre FROM SFruticola_SPSI.dba.TRA, SFruticola_SPSI.dba.variedades WHERE SFruticola_SPSI.dba.TRA.vari_codigo = SFruticola_SPSI.dba.variedades.vari_codigo AND SFruticola_SPSI.dba.TRA.espe_codigo = '" + Form2.codigo_especie + "' ORDER BY SFruticola_SPSI.dba.TRA.FechaDigi DESC", cn2);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    variedad = reader.GetString(0);
                }
            }
            catch (Exception ex)
            {
                ///
            }
            return variedad;
        }
        public string ObtenerNumeroLote_Vaciado()
        {
            string lote = "--";
            try
            {
                comando = new SqlCommand("SELECT TOP 1 convert(varchar(10),SFruticola_SPSI.dba.TRA.nlote) FROM SFruticola_SPSI.dba.TRA ORDER WHERE SFruticola_SPSI.dba.TRA.espe_codigo = '" + Form2.codigo_especie + "' BY SFruticola_SPSI.dba.TRA.FechaDigi DESC", cn2);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    lote = reader.GetString(0);
                }
            }
            catch (Exception ex)
            {
                ///
            }
            return lote;
        }
        public string ObtenerNumeroProceso_Vaciado()
        {
            string proceso = "--";
            try
            {
                comando = new SqlCommand("SELECT TOP 1 convert(varchar(10),SFruticola_SPSI.dba.TRA.proceso) FROM SFruticola_SPSI.dba.TRA WHERE SFruticola_SPSI.dba.TRA.espe_codigo = '" + Form2.codigo_especie + "' ORDER BY SFruticola_SPSI.dba.TRA.FechaDigi DESC", cn2);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    proceso = reader.GetString(0);
                }
            }
            catch (Exception ex)
            {
                ///
            }
            return proceso;
        }
        public string ObtenerNumeroCSE_Vaciado()
        {
            string CSE = "000000";
            try
            {
                comando = new SqlCommand("SELECT TOP 1 convert(varchar(10),SFruticola_SPSI.dba.exportadores.exp_id) FROM SFruticola_SPSI.dba.TRA, SFruticola_SPSI.dba.exportadores WHERE SFruticola_SPSI.dba.TRA.cli_codigo = SFruticola_SPSI.dba.exportadores.exp_codigo AND SFruticola_SPSI.dba.TRA.espe_codigo = '" + Form2.codigo_especie + "' ORDER BY SFruticola_SPSI.dba.TRA.FechaDigi DESC", cn2);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    CSE = reader.GetString(0);
                }
            }
            catch (Exception ex)
            {
                //
            }
            return CSE;
        }
        public string ObtenerNumeroCSE(string exportadora)
        {
            string CSE = "000000";
            try
            {
                comando = new SqlCommand("SELECT TOP 1 convert(varchar(10),SFruticola_SPSI.dba.exportadores.exp_id) FROM SFruticola_SPSI.dba.exportadores WHERE SFruticola_SPSI.dba.exportadores.exp_nombre = '"+ exportadora + "'", cn2);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    CSE = reader.GetString(0);
                }
            }
            catch (Exception ex)
            {
                //
            }
            return CSE;
        }
        public string[] CargaEmbalajes()
        {
            comando = new SqlCommand("SELECT SFruticola_SPSI.dba.envases.enva_id FROM SFruticola_SPSI.dba.envases ORDER BY SFruticola_SPSI.dba.envases.enva_id ASC", cn2);
            reader = comando.ExecuteReader();

            int i = 0;
            string[] embalajes = new string[50];
            while (reader.Read())
            {
                embalajes[i] = (reader.GetString(0));
                i = i + 1;
            }
            return embalajes;
        }
        public string[] CargaExportadores()
        {
            comando = new SqlCommand("SELECT SFruticola_SPSI.dba.clientes.cli_nombre FROM SFruticola_SPSI.dba.clientes ORDER BY SFruticola_SPSI.dba.clientes.cli_nombre ASC", cn2);
            reader = comando.ExecuteReader();

            int i = 0;
            string[] exportadores = new string[50];
            while (reader.Read())
            {
                exportadores[i] = (reader.GetString(0));
                i = i + 1;
            }
            return exportadores;
        }
        public string[] CargaVariedades()
        {
            comando = new SqlCommand("SELECT SFruticola_SPSI.dba.variedades.vari_nombre FROM SFruticola_SPSI.dba.variedades ORDER BY SFruticola_SPSI.dba.variedades.vari_nombre ASC", cn2);
            reader = comando.ExecuteReader();

            int i = 0;
            string[] variedades = new string[50];
            while (reader.Read())
            {
                variedades[i] = (reader.GetString(0));
                i = i + 1;
            }
            return variedades;
        }
        public string[] CargaProductores()
        {
            string[] productores = new string[100];
            try
            {
                //    //comando = new SqlCommand("SELECT etiquetado_system.dbo.productores.nombre_productor FROM etiquetado_system.dbo.productores", cn);
                comando = new SqlCommand("SELECT SFruticola_SPSI.dba.productores.prod_nombre FROM SFruticola_SPSI.dba.productores ORDER BY SFruticola_SPSI.dba.productores.prod_nombre ASC", cn2);
                SqlDataReader reader = comando.ExecuteReader();
                int i = 0;
                while (reader.Read())
                {
                    productores[i] = reader.GetString(0);
                    i++;
                }
            }
            catch (Exception ex)
            {
                /// 
            }
            return productores;
        }
        public string ObtenerCSG(string productor)
        {
            string csg = "--";
            try
            {
                //comando = new SqlCommand("SELECT etiquetado_system.dbo.productores.csg FROM etiquetado_system.dbo.productores WHERE etiquetado_system.dbo.productores.nombre_productor ='"+ productor + "'", cn);
                comando = new SqlCommand("SELECT SFruticola_SPSI.dba.productores.prod_codigo FROM SFruticola_SPSI.dba.productores WHERE SFruticola_SPSI.dba.productores.prod_nombre ='" + productor + "'", cn2);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    csg = reader.GetString(0);
                }
            }
            catch (Exception ex)
            {
                /// 
            }

            return csg;
        }
       
        public string ObtenerComuna(string productor)
        {
            string comuna = "--";
            try
            {
                //comando = new SqlCommand("SELECT etiquetado_system.dbo.productores.comuna FROM etiquetado_system.dbo.productores", cn);
                comando = new SqlCommand("SELECT SFruticola_SPSI.dba.productores.prod_comuna FROM SFruticola_SPSI.dba.productores WHERE SFruticola_SPSI.dba.productores.prod_nombre = '" + productor + "'", cn2);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    comuna = reader.GetString(0);
                }
            }
            catch (Exception ex)
            {
                /// 
            }
            return comuna;
        }
        public string ObtenerProvincia(string productor)
        {
            string provincia = "--";
            try
            {
                //comando = new SqlCommand("SELECT etiquetado_system.dbo.productores.provincia FROM etiquetado_system.dbo.productores", cn);
                comando = new SqlCommand("SELECT SFruticola_SPSI.dba.productores.prod_provincia FROM SFruticola_SPSI.dba.productores WHERE SFruticola_SPSI.dba.productores.prod_nombre = '" + productor + "'", cn2);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    provincia = reader.GetString(0);
                }
            }
            catch (Exception ex)
            {
                /// 
            }
            return provincia;
        }
        public string ObtenerRegion(string productor)
        {
            string region = "--";
            try
            {
                //comando = new SqlCommand("SELECT etiquetado_system.dbo.productores.region FROM etiquetado_system.dbo.productores", cn);
                comando = new SqlCommand("SELECT SFruticola_SPSI.dba.productores.Region_rotulada FROM SFruticola_SPSI.dba.productores WHERE SFruticola_SPSI.dba.productores.prod_nombre = '" + productor + "'", cn2);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    region = reader.GetString(0);
                }
            }
            catch (Exception ex)
            {
                /// 
            }
            return region;
        }
        
        public string ObtenerKilosEmbalaje(string embalaje)
        {
            string kilos = "--";
            try
            {
                //comando = new SqlCommand("SELECT etiquetado_system.dbo.productores.region FROM etiquetado_system.dbo.productores", cn);
                comando = new SqlCommand("SELECT convert(varchar(10),SFruticola_SPSI.dba.envases.enva_capacidadsag) FROM SFruticola_SPSI.dba.envases WHERE SFruticola_SPSI.dba.envases.enva_id = '" + embalaje + "'", cn2);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    kilos = reader.GetString(0);
                }
            }
            catch (Exception ex)
            {
                /// 
            }
            return kilos;
        }

    }
}
