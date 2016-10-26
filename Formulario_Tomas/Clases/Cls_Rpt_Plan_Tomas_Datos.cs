using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SIAC.Constantes;
using SharpContent.ApplicationBlocks.Data;
using Reportes_Planeacion.Tomas.Negocio;
using System.Data.SqlClient;


/// <summary>
/// Descripción breve de Cls_Rpt_Plan_Tomas_Datos
/// </summary>
namespace Reportes_Planeacion.Tomas.Datos
{
    public class Cls_Rpt_Plan_Tomas_Datos
    {
        public Cls_Rpt_Plan_Tomas_Datos()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }


        //*******************************************************************************
        //NOMBRE_FUNCION:  Consultar_Tarifas_Giro
        //DESCRIPCION: Metodo que Consulta las cuentas congeladas con estatus de cobranza
        //PARAMETROS : 1.- Cls_Rpt_Cor_Cc_Reportes_Varios_Neogcio Datos, objeto de la clase de negocios
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 11/Abril/2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public static DataTable Consultar_Tarifas_Giro(Cls_Rpt_Plan_Tomas_Negocio Datos)
        {
            DataTable Dt_Consulta = new DataTable();
            String Str_My_Sql = "";
            try
            {
                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                Str_My_Sql = "select  *";

                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  from **********************************************************************************************************************************
                Str_My_Sql += " from Cat_Cor_Giros";

                Dt_Consulta = SqlHelper.ExecuteDataset(Cls_Constantes.Str_Conexion, CommandType.Text, Str_My_Sql).Tables[0];

            }
            catch (Exception Ex)
            {
                throw new Exception("Error: " + Ex.Message);
            }

            return Dt_Consulta;

        }// fin de consulta


        //*******************************************************************************
        //NOMBRE_FUNCION:  Consultar_Tomas_Realizadas
        //DESCRIPCION: Metodo que las tomas que se realizaron durante el año
        //PARAMETROS : 1.- Cls_Rpt_Cor_Cc_Reportes_Varios_Neogcio Datos, objeto de la clase de negocios
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 11/Abril/2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public static DataTable Consultar_Tomas_Realizadas(Cls_Rpt_Plan_Tomas_Negocio Datos)
        {
            DataTable Dt_Consulta = new DataTable();
            String Str_My_Sql = "";
            try
            {
                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                Str_My_Sql = "select ";
                Str_My_Sql += " g.GIRO_ID ";
                Str_My_Sql += ", g.Nombre_Giro as Nombre";
                //Str_My_Sql += ", tp.DESCRIPCION";
                Str_My_Sql += ", g.Nombre_Giro";
                Str_My_Sql += ", COUNT(*) as Total_Tomas";
                Str_My_Sql += ", Month(getdate()) as Mes";
                Str_My_Sql += ", year(getdate()) as Año";

                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  from **********************************************************************************************************************************
                Str_My_Sql += " FROM Cat_Cor_Predios p";
                Str_My_Sql += " JOIN Cat_Cor_Tarifas t ON p.Tarifa_ID = t.Tarifa_ID";
                Str_My_Sql += " JOIN CAT_COR_TIPOS_CUOTAS tp ON tp.CUOTA_ID = t.Cuota_ID";
                Str_My_Sql += " JOIN Cat_Cor_Giros_Actividades ga ON ga.Actividad_Giro_ID = p.Giro_Actividad_ID";
                Str_My_Sql += " JOIN Cat_Cor_Giros g ON g.GIRO_ID = ga.Giro_ID";
                //Str_My_Sql += " join Cat_Cor_Grupos_Conceptos gc on gc.giro_id = g.GIRO_ID";
                //Str_My_Sql += "  join Cat_Cor_Grupos_Conceptos_Detalles grd on grd.GRUPO_CONCEPTO_ID = gc.GRUPO_CONCEPTO_ID";

                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  where **********************************************************************************************************************************
                Str_My_Sql += " where";
                //Str_My_Sql += " year(p.Fecha_Creo) = " + Datos.P_Anio;
                Str_My_Sql += " p.Estatus  ='ACTIVO' ";
                //Str_My_Sql += " AND grd.CONCEPTO_ID in (SELECT concepto_agua from Cat_Cor_Parametros)";

                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  GROUP BY **********************************************************************************************************************************
                Str_My_Sql += " GROUP BY";
                Str_My_Sql += " g.GIRO_ID";
                Str_My_Sql += ", g.Nombre_Giro";
                //Str_My_Sql += ", tp.DESCRIPCION ";
                //Str_My_Sql += ", month(p.Fecha_Creo)";
                //Str_My_Sql += ", year(p.Fecha_Creo)";

                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  ORDER BY **********************************************************************************************************************************
                Str_My_Sql += " ORDER BY";
                Str_My_Sql += " g.GIRO_ID";
                //Str_My_Sql += ", month(p.Fecha_Creo)";


                Dt_Consulta = SqlHelper.ExecuteDataset(Cls_Constantes.Str_Conexion, CommandType.Text, Str_My_Sql).Tables[0];

            }
            catch (Exception Ex)
            {
                throw new Exception("Error: " + Ex.Message);
            }

            return Dt_Consulta;

        }// fin de consulta



        //*******************************************************************************
        //NOMBRE_FUNCION:  Consultar_Tomas_Con_Medidor
        //DESCRIPCION: Metodo que las tomas que se realizaron durante el año
        //PARAMETROS : 1.- Cls_Rpt_Cor_Cc_Reportes_Varios_Neogcio Datos, objeto de la clase de negocios
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 11/Abril/2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public static DataTable Consultar_Tomas_Con_Medidor(Cls_Rpt_Plan_Tomas_Negocio Datos)
        {
            DataTable Dt_Consulta = new DataTable();
            String Str_My_Sql = "";
            try
            {
                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                Str_My_Sql = "select ";
                Str_My_Sql += " g.GIRO_ID ";
                Str_My_Sql += ", g.Nombre_Giro as Nombre";
                //Str_My_Sql += ", tp.DESCRIPCION";
                Str_My_Sql += ", g.Nombre_Giro";
                Str_My_Sql += ", COUNT(*) as Total_Tomas";
                Str_My_Sql += ", Month(getdate()) as Mes";
                Str_My_Sql += ", year(getdate()) as Año";

                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  from **********************************************************************************************************************************
                Str_My_Sql += " FROM Cat_Cor_Predios p";
                Str_My_Sql += " JOIN Cat_Cor_Tarifas t ON p.Tarifa_ID = t.Tarifa_ID";
                Str_My_Sql += " JOIN CAT_COR_TIPOS_CUOTAS tp ON tp.CUOTA_ID = t.Cuota_ID";
                Str_My_Sql += " JOIN Cat_Cor_Giros_Actividades ga ON ga.Actividad_Giro_ID = p.Giro_Actividad_ID";
                Str_My_Sql += " JOIN Cat_Cor_Giros g ON g.GIRO_ID = ga.Giro_ID";
                Str_My_Sql += " join Cat_Cor_Predios_Medidores pm on pm.PREDIO_ID = p.Predio_ID";


                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  where **********************************************************************************************************************************
                Str_My_Sql += " where";
                //Str_My_Sql += " year(pm.FECHA_INSTALACION) = " + Datos.P_Anio;
                Str_My_Sql += " p.Estatus = 'ACTIVO'";

                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  GROUP BY **********************************************************************************************************************************
                Str_My_Sql += " GROUP BY";
                Str_My_Sql += " g.GIRO_ID";
                Str_My_Sql += ", g.Nombre_Giro";
                //Str_My_Sql += ", tp.DESCRIPCION ";
                //Str_My_Sql += ", month(pm.FECHA_INSTALACION)";
                //Str_My_Sql += ", year(pm.FECHA_INSTALACION)";

                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  ORDER BY **********************************************************************************************************************************
                Str_My_Sql += " ORDER BY";
                Str_My_Sql += " g.GIRO_ID";
                //Str_My_Sql += ", month(pm.FECHA_INSTALACION)";


                Dt_Consulta = SqlHelper.ExecuteDataset(Cls_Constantes.Str_Conexion, CommandType.Text, Str_My_Sql).Tables[0];

            }
            catch (Exception Ex)
            {
                throw new Exception("Error: " + Ex.Message);
            }

            return Dt_Consulta;

        }// fin de consulta



        //*******************************************************************************
        //NOMBRE_FUNCION:  Consultar_Tomas_Descarga
        //DESCRIPCION: Metodo que las tomas que tiene el uso de descargar a la red de saneamiento
        //PARAMETROS : 1.- Cls_Rpt_Cor_Cc_Reportes_Varios_Neogcio Datos, objeto de la clase de negocios
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 11/Abril/2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public static DataTable Consultar_Tomas_Descarga(Cls_Rpt_Plan_Tomas_Negocio Datos)
        {
            DataTable Dt_Consulta = new DataTable();
            String Str_My_Sql = "";
            try
            {
                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                Str_My_Sql = "select ";
                Str_My_Sql += " g.GIRO_ID ";
                Str_My_Sql += ", g.Nombre_Giro as Nombre";
                //Str_My_Sql += ", tp.DESCRIPCION";
                Str_My_Sql += ", g.Nombre_Giro";
                Str_My_Sql += ", COUNT(*) as Total_Tomas";
                Str_My_Sql += ", Month(getdate()) as Mes";
                Str_My_Sql += ", year(getdate()) as Año";

                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  from **********************************************************************************************************************************
                Str_My_Sql += " FROM Cat_Cor_Predios p";
                Str_My_Sql += " JOIN Cat_Cor_Tarifas t ON p.Tarifa_ID = t.Tarifa_ID";
                Str_My_Sql += " JOIN CAT_COR_TIPOS_CUOTAS tp ON tp.CUOTA_ID = t.Cuota_ID";
                Str_My_Sql += " JOIN Cat_Cor_Giros_Actividades ga ON ga.Actividad_Giro_ID = p.Giro_Actividad_ID";
                Str_My_Sql += " JOIN Cat_Cor_Giros g ON g.GIRO_ID = ga.Giro_ID";
                Str_My_Sql += " join Cat_Cor_Grupos_Conceptos gc on gc.giro_id = g.GIRO_ID";
                Str_My_Sql += " join Cat_Cor_Grupos_Conceptos_Detalles grd on grd.GRUPO_CONCEPTO_ID = gc.GRUPO_CONCEPTO_ID";

                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  where **********************************************************************************************************************************
                Str_My_Sql += " where";
                //Str_My_Sql += " year(p.Fecha_Creo) = " + Datos.P_Anio;
                //Str_My_Sql += " and gc.NOMBRE like ('%Drenaje%')";
                Str_My_Sql += " p.Estatus = 'ACTIVO' ";
                Str_My_Sql += " AND grd.CONCEPTO_ID in (SELECT concepto_drenaje from Cat_Cor_Parametros)";

                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  GROUP BY **********************************************************************************************************************************
                Str_My_Sql += " GROUP BY";
                Str_My_Sql += " g.GIRO_ID";
                Str_My_Sql += ", g.Nombre_Giro";
                //Str_My_Sql += ", tp.DESCRIPCION ";
                //Str_My_Sql += ", g.Nombre_Giro";
                //Str_My_Sql += ", month(p.Fecha_Creo)";
                //Str_My_Sql += ", year(p.Fecha_Creo)";

                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  ORDER BY **********************************************************************************************************************************
                Str_My_Sql += " ORDER BY";
                Str_My_Sql += " g.GIRO_ID";
                //Str_My_Sql += ", month(p.Fecha_Creo)";


                Dt_Consulta = SqlHelper.ExecuteDataset(Cls_Constantes.Str_Conexion, CommandType.Text, Str_My_Sql).Tables[0];

            }
            catch (Exception Ex)
            {
                throw new Exception("Error: " + Ex.Message);
            }

            return Dt_Consulta;

        }// fin de consulta





        //*******************************************************************************
        //NOMBRE_FUNCION:  Consultar_05_Cartera_Vencida
        //DESCRIPCION: Metodo que consulta la informacion de la cartera vencida
        //PARAMETROS : 1.- Cls_Rpt_Cor_Cc_Reportes_Varios_Neogcio Datos, objeto de la clase de negocios
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 11/Abril/2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public static DataTable Consultar_05_Cartera_Vencida(Cls_Rpt_Plan_Tomas_Negocio Datos)
        {
            DataTable Dt_Consulta = new DataTable();
            String Str_My_Sql = "";
            try
            {
                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                Str_My_Sql = "select cv.* from Ope_Cor_Cc_Cartera_Vencidad_Historico cv";

                Str_My_Sql += " where cv.anio = " + Datos.P_Anio;

                Dt_Consulta = SqlHelper.ExecuteDataset(Cls_Constantes.Str_Conexion, CommandType.Text, Str_My_Sql).Tables[0];

            }
            catch (Exception Ex)
            {
                throw new Exception("Error: " + Ex.Message);
            }

            return Dt_Consulta;

        }// fin de consulta



        #region Agua

        //*******************************************************************************
        //NOMBRE_FUNCION:  Consultar_Tabla_Historicos_Agua
        //DESCRIPCION: consulta los historicos
        //PARAMETROS : 1.- Cls_Rpt_Cor_Cc_Reportes_Varios_Neogcio Datos, objeto de la clase de negocios
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 26/Octubre/2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public static DataTable Consultar_Tabla_Historicos_Agua(Cls_Rpt_Plan_Tomas_Negocio Datos)
        {
            DataTable Dt_Consulta = new DataTable();

            String Str_My_Sql = "";
            try
            {
                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                Str_My_Sql = "select ";
                Str_My_Sql += " * ";


                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  from **********************************************************************************************************************************
                Str_My_Sql += " from Ope_Cor_Plan_Tomas_Agua";

                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  where **********************************************************************************************************************************
                Str_My_Sql += " where";
                Str_My_Sql += " Año = " + Datos.P_Anio;

                if (!String.IsNullOrEmpty(Datos.P_Giro_Id))
                {
                    Str_My_Sql += " and giro_Id = '" + Datos.P_Giro_Id + "'";
                }

                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  **********************************************************************************************************************************

                Dt_Consulta = SqlHelper.ExecuteDataset(Cls_Constantes.Str_Conexion, CommandType.Text, Str_My_Sql).Tables[0];

            }
            catch (Exception Ex)
            {
                throw new Exception("Error: " + Ex.Message);
            }

            return Dt_Consulta;

        }// fin de consulta

        //*******************************************************************************
        //NOMBRE_FUNCION:  Consultar_Si_Historico_Agua
        //DESCRIPCION: Metodo que las tomas que tiene el uso de descargar a la red de saneamiento
        //PARAMETROS : 1.- Cls_Rpt_Cor_Cc_Reportes_Varios_Neogcio Datos, objeto de la clase de negocios
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 11/Abril/2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public static DataTable Consultar_Si_Historico_Agua(Cls_Rpt_Plan_Tomas_Negocio Datos)
        {
            DataTable Dt_Consulta = new DataTable();
            String Str_My_Sql = "";

            try
            {
                Str_My_Sql = "select * from Ope_Cor_Plan_Tomas_Agua";
                Str_My_Sql += " where Giro_Id = '" + Datos.P_Giro_Id + "'";
                Str_My_Sql += " and Año = " + Datos.P_Anio;

                Dt_Consulta = SqlHelper.ExecuteDataset(Cls_Constantes.Str_Conexion, CommandType.Text, Str_My_Sql).Tables[0];

            }
            catch (Exception Ex)
            {
                throw new Exception("Error: " + Ex.Message);
            }

            return Dt_Consulta;

        }// fin de consulta


        //*******************************************************************************
        //NOMBRE_FUNCION:  Insertar_Registro_Agua
        //DESCRIPCION: Metodo que ingresa la informacion
        //PARAMETROS : 1.- Cls_Rpt_Plan_Montos_Negocio Clase_Negocios, objeto de la clase de negocios
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 25-Octubre-2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public static void Insertar_Registro_Agua(Cls_Rpt_Plan_Tomas_Negocio Datos)
        {
            //Declaración de las variables
            SqlTransaction Obj_Transaccion = null;
            SqlConnection Obj_Conexion = new SqlConnection(Cls_Constantes.Str_Conexion);
            SqlCommand Obj_Comando = new SqlCommand();
            String Mi_SQL = "";

            try
            {
                Obj_Conexion.Open();
                Obj_Transaccion = Obj_Conexion.BeginTransaction();
                Obj_Comando.Transaction = Obj_Transaccion;
                Obj_Comando.Connection = Obj_Conexion;


                #region historico


                Mi_SQL = "INSERT INTO  Ope_Cor_Plan_Tomas_Agua (";
                Mi_SQL += "  Giro_Id";                          //  1
                Mi_SQL += ", Concepto";                         //  2
                Mi_SQL += ", Año";                              //  3
                Mi_SQL += ", " + Datos.P_Str_Nombre_Mes;        //  4
                Mi_SQL += ", Fecha_Creo";                       //  5
                Mi_SQL += ", Usuario_Creo";                     //  6
                Mi_SQL += ")";
                //***************************************************************************
                Mi_SQL += " values ";
                //***************************************************************************
                Mi_SQL += "(";
                Mi_SQL += "  '" + Datos.P_Giro_Id + "'";                                            //  1
                Mi_SQL += ", '" + Datos.P_Dr_Registro["Tomas_Por_Tarifa"].ToString() + "'";                 //  2
                Mi_SQL += ",  " + Datos.P_Anio + "";                                                //  3
                Mi_SQL += ",  " + Datos.P_Dr_Registro[Datos.P_Str_Nombre_Mes].ToString() + "";      //  4
                Mi_SQL += ",  getdate()";                                                           //  5
                Mi_SQL += ", '" + Datos.P_Str_Usuario + "'";                                        //  7
                Mi_SQL += ")";

                Obj_Comando.CommandText = Mi_SQL;
                Obj_Comando.ExecuteNonQuery();

                #endregion Fin historico

                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //ejecucion de la transaccion    ***********************************************************************************
                Obj_Transaccion.Commit();


            }
            catch (SqlException Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            catch (DBConcurrencyException Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            catch (Exception Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            finally
            {
                Obj_Conexion.Close();
            }


        }// fin del metodo


        //*******************************************************************************
        //NOMBRE_FUNCION:  Actualizar_Registro_Facturacion
        //DESCRIPCION: Metodo que ingresa la informacion de los montos de la facturacion
        //PARAMETROS : 1.- Cls_Rpt_Plan_Montos_Negocio Clase_Negocios, objeto de la clase de negocios
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 25-Octubre-2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public static void Actualizar_Registro_Agua(Cls_Rpt_Plan_Tomas_Negocio Datos)
        {
            //Declaración de las variables
            SqlTransaction Obj_Transaccion = null;
            SqlConnection Obj_Conexion = new SqlConnection(Cls_Constantes.Str_Conexion);
            SqlCommand Obj_Comando = new SqlCommand();
            String Mi_SQL = "";

            try
            {
                Obj_Conexion.Open();
                Obj_Transaccion = Obj_Conexion.BeginTransaction();
                Obj_Comando.Transaction = Obj_Transaccion;
                Obj_Comando.Connection = Obj_Conexion;


                #region historico


                Mi_SQL = "update  Ope_Cor_Plan_Tomas_Agua set ";
                Mi_SQL += "  " + Datos.P_Str_Nombre_Mes + " = " + Datos.P_Dr_Registro[Datos.P_Str_Nombre_Mes].ToString();
                Mi_SQL += ", fecha_modifico = getdate()";
                Mi_SQL += ", usuario_modifico = '" + Datos.P_Str_Usuario + "'";
                Mi_SQL += " where id = '" + Datos.P_Id + "'";
                Obj_Comando.CommandText = Mi_SQL;
                Obj_Comando.ExecuteNonQuery();

                #endregion Fin historico

                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //ejecucion de la transaccion    ***********************************************************************************
                Obj_Transaccion.Commit();


            }
            catch (SqlException Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            catch (DBConcurrencyException Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            catch (Exception Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            finally
            {
                Obj_Conexion.Close();
            }


        }// fin del metodo


        #endregion Fin Agua


        #region Micromedidor

        //*******************************************************************************
        //NOMBRE_FUNCION:  Consultar_Tabla_Historicos_Micromedidor
        //DESCRIPCION: consulta los historicos
        //PARAMETROS : 1.- Cls_Rpt_Cor_Cc_Reportes_Varios_Neogcio Datos, objeto de la clase de negocios
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 26/Octubre/2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public static DataTable Consultar_Tabla_Historicos_Micromedidor(Cls_Rpt_Plan_Tomas_Negocio Datos)
        {
            DataTable Dt_Consulta = new DataTable();

            String Str_My_Sql = "";
            try
            {
                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                Str_My_Sql = "select ";
                Str_My_Sql += " * ";


                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  from **********************************************************************************************************************************
                Str_My_Sql += " from Ope_Cor_Plan_Tomas_Micromedidor";

                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  where **********************************************************************************************************************************
                Str_My_Sql += " where";
                Str_My_Sql += " Año = " + Datos.P_Anio;

                if (!String.IsNullOrEmpty(Datos.P_Giro_Id))
                {
                    Str_My_Sql += " and giro_Id = '" + Datos.P_Giro_Id + "'";
                }

                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  **********************************************************************************************************************************

                Dt_Consulta = SqlHelper.ExecuteDataset(Cls_Constantes.Str_Conexion, CommandType.Text, Str_My_Sql).Tables[0];

            }
            catch (Exception Ex)
            {
                throw new Exception("Error: " + Ex.Message);
            }

            return Dt_Consulta;

        }// fin de consulta


        //*******************************************************************************
        //NOMBRE_FUNCION:  Consultar_Si_Historico_Agua
        //DESCRIPCION: Metodo que las tomas que tiene el uso de descargar a la red de saneamiento
        //PARAMETROS : 1.- Cls_Rpt_Cor_Cc_Reportes_Varios_Neogcio Datos, objeto de la clase de negocios
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 11/Abril/2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public static DataTable Consultar_Si_Historico_Micromedidor(Cls_Rpt_Plan_Tomas_Negocio Datos)
        {
            DataTable Dt_Consulta = new DataTable();
            String Str_My_Sql = "";

            try
            {
                Str_My_Sql = "select * from Ope_Cor_Plan_Tomas_Micromedidor";
                Str_My_Sql += " where Giro_Id = '" + Datos.P_Giro_Id + "'";
                Str_My_Sql += " and Año = " + Datos.P_Anio;

                Dt_Consulta = SqlHelper.ExecuteDataset(Cls_Constantes.Str_Conexion, CommandType.Text, Str_My_Sql).Tables[0];

            }
            catch (Exception Ex)
            {
                throw new Exception("Error: " + Ex.Message);
            }

            return Dt_Consulta;

        }// fin de consulta


        //*******************************************************************************
        //NOMBRE_FUNCION:  Insertar_Registro_Micromedidor
        //DESCRIPCION: Metodo que ingresa la informacion
        //PARAMETROS : 1.- Cls_Rpt_Plan_Montos_Negocio Clase_Negocios, objeto de la clase de negocios
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 25-Octubre-2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public static void Insertar_Registro_Micromedidor(Cls_Rpt_Plan_Tomas_Negocio Datos)
        {
            //Declaración de las variables
            SqlTransaction Obj_Transaccion = null;
            SqlConnection Obj_Conexion = new SqlConnection(Cls_Constantes.Str_Conexion);
            SqlCommand Obj_Comando = new SqlCommand();
            String Mi_SQL = "";

            try
            {
                Obj_Conexion.Open();
                Obj_Transaccion = Obj_Conexion.BeginTransaction();
                Obj_Comando.Transaction = Obj_Transaccion;
                Obj_Comando.Connection = Obj_Conexion;


                #region historico


                Mi_SQL = "INSERT INTO  Ope_Cor_Plan_Tomas_Micromedidor (";
                Mi_SQL += "  Giro_Id";                          //  1
                Mi_SQL += ", Concepto";                         //  2
                Mi_SQL += ", Año";                              //  3
                Mi_SQL += ", " + Datos.P_Str_Nombre_Mes;        //  4
                Mi_SQL += ", Fecha_Creo";                       //  5
                Mi_SQL += ", Usuario_Creo";                     //  6
                Mi_SQL += ")";
                //***************************************************************************
                Mi_SQL += " values ";
                //***************************************************************************
                Mi_SQL += "(";
                Mi_SQL += "  '" + Datos.P_Giro_Id + "'";                                            //  1
                Mi_SQL += ", '" + Datos.P_Dr_Registro["Tomas_Por_Tarifa"].ToString() + "'";                 //  2
                Mi_SQL += ",  " + Datos.P_Anio + "";                                                //  3
                Mi_SQL += ",  " + Datos.P_Dr_Registro[Datos.P_Str_Nombre_Mes].ToString() + "";      //  4
                Mi_SQL += ",  getdate()";                                                           //  5
                Mi_SQL += ", '" + Datos.P_Str_Usuario + "'";                                        //  7
                Mi_SQL += ")";

                Obj_Comando.CommandText = Mi_SQL;
                Obj_Comando.ExecuteNonQuery();

                #endregion Fin historico

                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //ejecucion de la transaccion    ***********************************************************************************
                Obj_Transaccion.Commit();


            }
            catch (SqlException Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            catch (DBConcurrencyException Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            catch (Exception Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            finally
            {
                Obj_Conexion.Close();
            }


        }// fin del metodo


        //*******************************************************************************
        //NOMBRE_FUNCION:  Actualizar_Registro_Micromedidor
        //DESCRIPCION: Metodo que ingresa la informacion de los montos de la facturacion
        //PARAMETROS : 1.- Cls_Rpt_Plan_Montos_Negocio Clase_Negocios, objeto de la clase de negocios
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 25-Octubre-2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public static void Actualizar_Registro_Micromedidor(Cls_Rpt_Plan_Tomas_Negocio Datos)
        {
            //Declaración de las variables
            SqlTransaction Obj_Transaccion = null;
            SqlConnection Obj_Conexion = new SqlConnection(Cls_Constantes.Str_Conexion);
            SqlCommand Obj_Comando = new SqlCommand();
            String Mi_SQL = "";

            try
            {
                Obj_Conexion.Open();
                Obj_Transaccion = Obj_Conexion.BeginTransaction();
                Obj_Comando.Transaction = Obj_Transaccion;
                Obj_Comando.Connection = Obj_Conexion;


                #region historico


                Mi_SQL = "update  Ope_Cor_Plan_Tomas_Micromedidor set ";
                Mi_SQL += "  " + Datos.P_Str_Nombre_Mes + " = " + Datos.P_Dr_Registro[Datos.P_Str_Nombre_Mes].ToString();
                Mi_SQL += ", fecha_modifico = getdate()";
                Mi_SQL += ", usuario_modifico = '" + Datos.P_Str_Usuario + "'";
                Mi_SQL += " where id = '" + Datos.P_Id + "'";
                Obj_Comando.CommandText = Mi_SQL;
                Obj_Comando.ExecuteNonQuery();

                #endregion Fin historico

                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //ejecucion de la transaccion    ***********************************************************************************
                Obj_Transaccion.Commit();


            }
            catch (SqlException Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            catch (DBConcurrencyException Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            catch (Exception Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            finally
            {
                Obj_Conexion.Close();
            }


        }// fin del metodo


        #endregion Fin Micromedidor


        #region Descargas


        //*******************************************************************************
        //NOMBRE_FUNCION:  Consultar_Tabla_Historicos_Descargas
        //DESCRIPCION: consulta los historicos
        //PARAMETROS : 1.- Cls_Rpt_Cor_Cc_Reportes_Varios_Neogcio Datos, objeto de la clase de negocios
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 26/Octubre/2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public static DataTable Consultar_Tabla_Historicos_Descargas(Cls_Rpt_Plan_Tomas_Negocio Datos)
        {
            DataTable Dt_Consulta = new DataTable();

            String Str_My_Sql = "";
            try
            {
                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                Str_My_Sql = "select ";
                Str_My_Sql += " * ";


                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  from **********************************************************************************************************************************
                Str_My_Sql += " from Ope_Cor_Plan_Tomas_Descargas";

                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  where **********************************************************************************************************************************
                Str_My_Sql += " where";
                Str_My_Sql += " Año = " + Datos.P_Anio;

                if (!String.IsNullOrEmpty(Datos.P_Giro_Id))
                {
                    Str_My_Sql += " and giro_Id = '" + Datos.P_Giro_Id + "'";
                }

                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  **********************************************************************************************************************************

                Dt_Consulta = SqlHelper.ExecuteDataset(Cls_Constantes.Str_Conexion, CommandType.Text, Str_My_Sql).Tables[0];

            }
            catch (Exception Ex)
            {
                throw new Exception("Error: " + Ex.Message);
            }

            return Dt_Consulta;

        }// fin de consulta



        //*******************************************************************************
        //NOMBRE_FUNCION:  Consultar_Si_Historico_Descargas
        //DESCRIPCION: Metodo que las tomas que tiene el uso de descargar a la red de saneamiento
        //PARAMETROS : 1.- Cls_Rpt_Cor_Cc_Reportes_Varios_Neogcio Datos, objeto de la clase de negocios
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 11/Abril/2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public static DataTable Consultar_Si_Historico_Descargas(Cls_Rpt_Plan_Tomas_Negocio Datos)
        {
            DataTable Dt_Consulta = new DataTable();
            String Str_My_Sql = "";

            try
            {
                Str_My_Sql = "select * from Ope_Cor_Plan_Tomas_Descargas";
                Str_My_Sql += " where Giro_Id = '" + Datos.P_Giro_Id + "'";
                Str_My_Sql += " and Año = " + Datos.P_Anio;

                Dt_Consulta = SqlHelper.ExecuteDataset(Cls_Constantes.Str_Conexion, CommandType.Text, Str_My_Sql).Tables[0];

            }
            catch (Exception Ex)
            {
                throw new Exception("Error: " + Ex.Message);
            }

            return Dt_Consulta;

        }// fin de consulta


        //*******************************************************************************
        //NOMBRE_FUNCION:  Insertar_Registro_Descargas
        //DESCRIPCION: Metodo que ingresa la informacion
        //PARAMETROS : 1.- Cls_Rpt_Plan_Montos_Negocio Clase_Negocios, objeto de la clase de negocios
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 25-Octubre-2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public static void Insertar_Registro_Descargas(Cls_Rpt_Plan_Tomas_Negocio Datos)
        {
            //Declaración de las variables
            SqlTransaction Obj_Transaccion = null;
            SqlConnection Obj_Conexion = new SqlConnection(Cls_Constantes.Str_Conexion);
            SqlCommand Obj_Comando = new SqlCommand();
            String Mi_SQL = "";

            try
            {
                Obj_Conexion.Open();
                Obj_Transaccion = Obj_Conexion.BeginTransaction();
                Obj_Comando.Transaction = Obj_Transaccion;
                Obj_Comando.Connection = Obj_Conexion;


                #region historico


                Mi_SQL = "INSERT INTO  Ope_Cor_Plan_Tomas_Descargas (";
                Mi_SQL += "  Giro_Id";                          //  1
                Mi_SQL += ", Concepto";                         //  2
                Mi_SQL += ", Año";                              //  3
                Mi_SQL += ", " + Datos.P_Str_Nombre_Mes;        //  4
                Mi_SQL += ", Fecha_Creo";                       //  5
                Mi_SQL += ", Usuario_Creo";                     //  6
                Mi_SQL += ")";
                //***************************************************************************
                Mi_SQL += " values ";
                //***************************************************************************
                Mi_SQL += "(";
                Mi_SQL += "  '" + Datos.P_Giro_Id + "'";                                            //  1
                Mi_SQL += ", '" + Datos.P_Dr_Registro["Tomas_Por_Tarifa"].ToString() + "'";                 //  2
                Mi_SQL += ",  " + Datos.P_Anio + "";                                                //  3
                Mi_SQL += ",  " + Datos.P_Dr_Registro[Datos.P_Str_Nombre_Mes].ToString() + "";      //  4
                Mi_SQL += ",  getdate()";                                                           //  5
                Mi_SQL += ", '" + Datos.P_Str_Usuario + "'";                                        //  7
                Mi_SQL += ")";

                Obj_Comando.CommandText = Mi_SQL;
                Obj_Comando.ExecuteNonQuery();

                #endregion Fin historico

                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //ejecucion de la transaccion    ***********************************************************************************
                Obj_Transaccion.Commit();


            }
            catch (SqlException Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            catch (DBConcurrencyException Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            catch (Exception Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            finally
            {
                Obj_Conexion.Close();
            }


        }// fin del metodo


        //*******************************************************************************
        //NOMBRE_FUNCION:  Actualizar_Registro_Descargas
        //DESCRIPCION: Metodo que ingresa la informacion de los montos
        //PARAMETROS : 1.- Cls_Rpt_Plan_Montos_Negocio Clase_Negocios, objeto de la clase de negocios
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 25-Octubre-2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public static void Actualizar_Registro_Descargas(Cls_Rpt_Plan_Tomas_Negocio Datos)
        {
            //Declaración de las variables
            SqlTransaction Obj_Transaccion = null;
            SqlConnection Obj_Conexion = new SqlConnection(Cls_Constantes.Str_Conexion);
            SqlCommand Obj_Comando = new SqlCommand();
            String Mi_SQL = "";

            try
            {
                Obj_Conexion.Open();
                Obj_Transaccion = Obj_Conexion.BeginTransaction();
                Obj_Comando.Transaction = Obj_Transaccion;
                Obj_Comando.Connection = Obj_Conexion;


                #region historico


                Mi_SQL = "update  Ope_Cor_Plan_Tomas_Descargas set ";
                Mi_SQL += "  " + Datos.P_Str_Nombre_Mes + " = " + Datos.P_Dr_Registro[Datos.P_Str_Nombre_Mes].ToString();
                Mi_SQL += ", fecha_modifico = getdate()";
                Mi_SQL += ", usuario_modifico = '" + Datos.P_Str_Usuario + "'";
                Mi_SQL += " where id = '" + Datos.P_Id + "'";
                Obj_Comando.CommandText = Mi_SQL;
                Obj_Comando.ExecuteNonQuery();

                #endregion Fin historico

                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //ejecucion de la transaccion    ***********************************************************************************
                Obj_Transaccion.Commit();


            }
            catch (SqlException Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            catch (DBConcurrencyException Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            catch (Exception Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            finally
            {
                Obj_Conexion.Close();
            }


        }// fin del metodo


        #endregion Fin Micromedidor
    }
}