using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;

namespace GeDoc
{
    using System.Web.Mvc;

    public class UtilesController : Controller
    {
        //
        // GET: /Utiles/

        public UtilesController()
        {
        }

        public string GetPath(string pPath)
        {
            if (Url == null)
            {
                return pPath.Replace("~/", "/GeDoc/");
            }
            return Url.Content(pPath);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult _GetArchivoImportado(string ArchivoImportado)
        {
            try
            {
                var Resultado = new ArchivoImportado();

                var Importar = new Utiles();

                string PathFisico = Server.MapPath("~/Content/Archivos/Importaciones");
                PathFisico = Path.Combine(PathFisico, Session["upArchivo"].ToString());
                var Datos = Importar.ImportFromExcel(PathFisico);

                Session["ArchivoImportado"] = Datos;

                Resultado.Columnas = new List<ArchivoImportadoColumna>();
                Resultado.Valores = new List<ArchivoImportadoValor>();

                for (var Index = 0; Index < Datos.Tables[0].Columns.Count; Index++)
                {
                    Resultado.Columnas.Add(new ArchivoImportadoColumna()
                    {
                        Nombre = Datos.Tables[0].Columns[Index].Caption
                    });
                }
                Session["ArchivoImportadoColumnas"] = Resultado.Columnas;

                for (int Fila = 0; Fila < Datos.Tables[0].Rows.Count; Fila++)
                {
                    var Valor = new string[Datos.Tables[0].Columns.Count];
                    for (var Columna = 0; Columna < Datos.Tables[0].Columns.Count; Columna++)
                    {
                        //Resultado.Columnas.Add(new ArchivoImportadoColumna() { Nombre = Datos.Tables[0].Columns[Index].Caption });
                        Valor[Columna] = Datos.Tables[0].Rows[Fila][Columna].ToString();
                    }
                    Resultado.Valores.Add(new ArchivoImportadoValor() { Valor = Valor });
                }

                return PartialView("_VerArchivoImportado", Resultado);
            }
            catch (Exception ex)
            {
                return PartialView("_VerErrorImportacion", (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
        }
    }

    public class Utiles
    {
        public Utiles()
        {
        }

        public class TituloColumnasGrid
        {
            public string sTitle
            {
                set;
                get;
            }
        }

        public string _GetXML(int CampoId, int CampoDetalle, DataSet Datos)
        {
            var Resultado = "<ROOT>";

            for (int Fila = 0; Fila < Datos.Tables[0].Rows.Count; Fila++)
            {
                var Valor = Datos.Tables[0].Rows[Fila][CampoId].ToString();
                var Detalle = "";
                if (CampoDetalle > -1)
                {
                    Detalle = Datos.Tables[0].Rows[Fila][CampoDetalle].ToString();
                }
                if (Valor.Length > 0)
                {
                    Resultado += "<Archivo Valor=\"" + Valor + "\" Detalle=\"" + Detalle + "\"/>";
                }
            }
            Resultado += "</ROOT>";

            return Resultado;
        }

        public DataSet ImportFromExcel(string file)
        {
            // Create new dataset
            DataSet ds = new DataSet();

            // -- Start of Constructing OLEDB connection string to Excel file
            Dictionary<string, string> props = new Dictionary<string, string>();

            // For Excel 2007/2010
            if (file.EndsWith(".xlsx"))
            {
                props["Provider"] = "Microsoft.ACE.OLEDB.12.0";
                props["Extended Properties"] = "Excel 12.0 XML";
                //props["Connect Timeout"] = "300";
            }
            // For Excel 2003 and older
            else if (file.EndsWith(".xls"))
            {
                props["Provider"] = "Microsoft.Jet.OLEDB.4.0";
                props["Extended Properties"] = "Excel 8.0";
                //props["Connect Timeout"] = "300";
            }
            else
                return null;

            props["Data Source"] = file;

            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, string> prop in props)
            {
                sb.Append(prop.Key);
                sb.Append('=');
                sb.Append(prop.Value);
                sb.Append(';');
            }

            string connectionString = sb.ToString();
            // -- End of Constructing OLEDB connection string to Excel file

            // Connecting to Excel File
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.CommandTimeout = 300;

                DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                foreach (DataRow dr in dtSheet.Rows)
                {
                    string sheetName = dr["TABLE_NAME"].ToString();

                    // you can choose the colums you want.
                    cmd.CommandText = "SELECT * FROM [" + sheetName + "]";

                    DataTable dt = new DataTable();
                    dt.TableName = sheetName.Replace("$", string.Empty);

                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);

                    // Add table into DataSet
                    ds.Tables.Add(dt);
                    //Como no se necesita ver más de una hoja, salgo al ver la primera
                    break;
                }

                cmd = null;
                conn.Close();
            }

            return ds;
        }

    }
}
