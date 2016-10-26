using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Reportes_Planeacion.Tomas.Negocio;
using SIAC.Metodos_Generales;

namespace Formulario_Tomas
{
    public partial class Frm_Pruebas : Form
    {
        public Frm_Pruebas()
        {
            InitializeComponent();
        }

        //*******************************************************************************
        //NOMBRE DE LA FUNCIÓN:Btn_Prueba_Click
        //DESCRIPCIÓN: Metodo que genera la informacion de las tomas
        //PARAMETROS: 
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 26/Octubre/2016
        //MODIFICO:
        //FECHA_MODIFICO:
        //CAUSA_MODIFICACIÓN:
        //*******************************************************************************
        private void Btn_Prueba_Click(object sender, EventArgs e)
        {
            try
            {
                Actualizar_Informacion();

                MessageBox.Show("Proceso exitos", "Mensaje", MessageBoxButtons.OK);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error_:" + Ex.Message, "Mensaje", MessageBoxButtons.OK);
            }
        }




        //*******************************************************************************
        //NOMBRE DE LA FUNCIÓN:Actualizar_Informacion
        //DESCRIPCIÓN: Metodo que permite llenar el Grid con la informacion de la consulta
        //PARAMETROS: 
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 07/Abril/2016
        //MODIFICO:
        //FECHA_MODIFICO:
        //CAUSA_MODIFICACIÓN:
        //*******************************************************************************
        public void Actualizar_Informacion()
        {
            Cls_Rpt_Plan_Tomas_Negocio Rs_Consulta = new Cls_Rpt_Plan_Tomas_Negocio();
            DataTable Dt_Consulta = new DataTable();
            DataTable Dt_Tarifas = new DataTable();
            DataTable Dt_Reporte = new DataTable();
            DataTable Dt_Reporte_Agua = new DataTable();
            DataTable Dt_Existencia = new DataTable();
            DataTable Dt_Reporte_MicroMedidor = new DataTable();
            DataTable Dt_Reporte_Descarga = new DataTable();
            DataTable Dt_Auxiliar = new DataTable();
            DataTable Dt_Resumen = new DataTable();
            DataRow Dr_Nuevo_Elemento;
            Int32 Int_Mes = 0;
            String Str_Nombre_Mes = "";
            Dictionary<Int32, String> Dic_Meses;

            try
            {
                Dic_Meses = Cls_Metodos_Generales.Crear_Diccionario_Meses();


                Rs_Consulta.P_Anio = DateTime.Now.Year;

                Dt_Tarifas = Rs_Consulta.Consultar_Tarifas_Giro();


                Dt_Reporte = Crear_Tabla_Reporte();
                Dt_Reporte_Agua = Crear_Tabla_Reporte();
                Dt_Reporte_MicroMedidor = Crear_Tabla_Reporte();
                Dt_Reporte_Descarga = Crear_Tabla_Reporte();



                //********************************************************************************************************************
                //********************************************************************************************************************
                //********************************************************************************************************************
                //  Tomas domésticas  en localidades rurales integradas al sistema
                //Dr_Nuevo_Elemento = Dt_Reporte.NewRow();
                //Dr_Nuevo_Elemento["tarifa_Id"] = "Y";
                //Dr_Nuevo_Elemento["Giro_id"] = "0000000000";
                //Dr_Nuevo_Elemento["Tomas_Por_Tarifa"] = "Tomas domésticas  en localidades rurales integradas al sistema";
                //Dt_Reporte.Rows.Add(Dr_Nuevo_Elemento);



                //********************************************************************************************************************
                //********************************************************************************************************************
                //********************************************************************************************************************
                //  Se ingresan los encabezados para las tomas 
                foreach (DataRow Registro in Dt_Tarifas.Rows)
                {
                    Dr_Nuevo_Elemento = Dt_Reporte_Agua.NewRow();
                    Dr_Nuevo_Elemento["tarifa_Id"] = Registro["giro_id"].ToString();
                    Dr_Nuevo_Elemento["Giro_id"] = Registro["giro_id"].ToString();
                    Dr_Nuevo_Elemento["Tomas_Por_Tarifa"] = "Tomas " + Registro["Nombre_Giro"].ToString() + " (" +
                                        Registro["clave"].ToString() + ")" + " agua potable";


                    Dt_Reporte_Agua.Rows.Add(Dr_Nuevo_Elemento);
                }

                //   se consultan las tomas que se realizaron en el año de consulta
                Dt_Consulta = Rs_Consulta.Consultar_Tomas_Realizadas();

                //  se recorre la tabla del reporte final
                foreach (DataRow Registro_Reporte in Dt_Reporte_Agua.Rows)
                {
                    Registro_Reporte.BeginEdit();

                    //  se recorre la informacion de la consulta
                    foreach (DataRow Registro_Consulta in Dt_Consulta.Rows)
                    {
                        //  se valida que sea la misma tarifa que la que se encuentra en el reporte
                        if (Registro_Reporte["Tarifa_Id"].ToString() == Registro_Consulta["giro_id"].ToString())
                        {
                            Int_Mes = 0;

                            //  validamos que tenga informacion el campo de mes, ya que se convertira a numerico
                            if (!String.IsNullOrEmpty(Registro_Consulta["Mes"].ToString()))
                            {
                                Int_Mes = Convert.ToInt32(Registro_Consulta["Mes"].ToString());

                                if (Dic_Meses.ContainsKey(Int_Mes) == true)
                                {
                                    Str_Nombre_Mes = Dic_Meses[Int_Mes];

                                }//    fin de la validacion del diccionario

                                Registro_Reporte[Str_Nombre_Mes] = Convert.ToDouble(Registro_Consulta["Total_Tomas"].ToString());

                            }// fin de la validacion mes

                        }// fin del if tarifa id

                    }// fin foreach consulta

                    Registro_Reporte.EndEdit();
                    Registro_Reporte.AcceptChanges();

                }// fin foreach reporte


                //********************************************************************************************************************
                //********************************************************************************************************************
                //********************************************************************************************************************
                //  segundo proceso tomas con medidor********************************************************************************************************************
                //  se ingresa el envabezao principal de los micromedidores

                foreach (DataRow Registro in Dt_Tarifas.Rows)
                {
                    Dr_Nuevo_Elemento = Dt_Reporte_MicroMedidor.NewRow();
                    Dr_Nuevo_Elemento["tarifa_Id"] = Registro["giro_id"].ToString();
                    Dr_Nuevo_Elemento["Tomas_Por_Tarifa"] = "Tomas " + Registro["Nombre_Giro"].ToString() + " (" +
                                        Registro["clave"].ToString() + ")" + " con micromedidor";

                    Dt_Reporte_MicroMedidor.Rows.Add(Dr_Nuevo_Elemento);
                }

                Dt_Consulta = new DataTable();
                Dt_Consulta = Rs_Consulta.Consultar_Tomas_Con_Medidor();

                //  se recorre la tabla del reporte final
                foreach (DataRow Registro_Reporte in Dt_Reporte_MicroMedidor.Rows)
                {
                    Registro_Reporte.BeginEdit();

                    //  se recorre la informacion de la consulta
                    foreach (DataRow Registro_Consulta in Dt_Consulta.Rows)
                    {
                        //  se valida que sea la misma tarifa que la que se encuentra en el reporte
                        if (Registro_Reporte["Tarifa_Id"].ToString() == Registro_Consulta["giro_id"].ToString())
                        {
                            Int_Mes = 0;

                            //  validamos que tenga informacion el campo de mes, ya que se convertira a numerico
                            if (!String.IsNullOrEmpty(Registro_Consulta["Mes"].ToString()))
                            {
                                Int_Mes = Convert.ToInt32(Registro_Consulta["Mes"].ToString());

                                if (Dic_Meses.ContainsKey(Int_Mes) == true)
                                {
                                    Str_Nombre_Mes = Dic_Meses[Int_Mes];

                                }//    fin de la validacion del diccionario

                                Registro_Reporte[Str_Nombre_Mes] = Convert.ToDouble(Registro_Consulta["Total_Tomas"].ToString());

                            }// fin de la validacion mes

                        }// fin del if tarifa id

                    }// fin foreach consulta

                    Registro_Reporte.EndEdit();
                    Registro_Reporte.AcceptChanges();

                }// fin foreach reporte

                //********************************************************************************************************************
                //********************************************************************************************************************
                //********************************************************************************************************************
                //  tomas con descarga a la red de drenaje y saneamiento********************************************************************************************************************
                foreach (DataRow Registro in Dt_Tarifas.Rows)
                {
                    Dr_Nuevo_Elemento = Dt_Reporte_Descarga.NewRow();
                    Dr_Nuevo_Elemento["tarifa_Id"] = Registro["giro_id"].ToString();
                    Dr_Nuevo_Elemento["Tomas_Por_Tarifa"] = "Descargas " + Registro["Nombre_Giro"].ToString() + " (" +
                                        Registro["clave"].ToString() + ")" + " al sistema de drenaje sanitario";

                    Dt_Reporte_Descarga.Rows.Add(Dr_Nuevo_Elemento);
                }

                Dt_Consulta = new DataTable();

                Dt_Consulta = Rs_Consulta.Consultar_Tomas_Descarga();

                //  se recorre la tabla del reporte final
                foreach (DataRow Registro_Reporte in Dt_Reporte_Descarga.Rows)
                {
                    Registro_Reporte.BeginEdit();

                    //  se recorre la informacion de la consulta
                    foreach (DataRow Registro_Consulta in Dt_Consulta.Rows)
                    {
                        //  se valida que sea la misma tarifa que la que se encuentra en el reporte
                        if (Registro_Reporte["Tarifa_Id"].ToString() == Registro_Consulta["giro_id"].ToString())
                        {
                            Int_Mes = 0;

                            //  validamos que tenga informacion el campo de mes, ya que se convertira a numerico
                            if (!String.IsNullOrEmpty(Registro_Consulta["Mes"].ToString()))
                            {
                                Int_Mes = Convert.ToInt32(Registro_Consulta["Mes"].ToString());

                                if (Dic_Meses.ContainsKey(Int_Mes) == true)
                                {
                                    Str_Nombre_Mes = Dic_Meses[Int_Mes];

                                }//    fin de la validacion del diccionario

                                Registro_Reporte[Str_Nombre_Mes] = Convert.ToDouble(Registro_Consulta["Total_Tomas"].ToString());

                            }// fin de la validacion mes

                        }// fin del if tarifa id

                    }// fin foreach consulta

                    Registro_Reporte.EndEdit();
                    Registro_Reporte.AcceptChanges();

                }// fin foreach reporte

                //********************************************************************************************************************
                //********************************************************************************************************************
                //********************************************************************************************************************

                //  se borraran las tarifas id, ya que comenzara con el tercer proceso
                foreach (DataRow Registro_Reporte in Dt_Reporte_Agua.Rows)
                {
                    Registro_Reporte.BeginEdit();

                    for (int Cont_For = 1; Cont_For <= 12; Cont_For++)
                    {
                        Str_Nombre_Mes = "";
                        if (Dic_Meses.ContainsKey(Cont_For) == true)
                        {
                            Str_Nombre_Mes = Dic_Meses[Cont_For];

                        }//    fin de la validacion del diccionario

                        if (String.IsNullOrEmpty(Registro_Reporte[Str_Nombre_Mes].ToString()))
                        {
                            Registro_Reporte[Str_Nombre_Mes] = 0;
                        }
                    }

                    Registro_Reporte.EndEdit();
                    Registro_Reporte.AcceptChanges();

                }

                //  se borraran las tarifas id, ya que comenzara con el tercer proceso
                foreach (DataRow Registro_Reporte in Dt_Reporte_MicroMedidor.Rows)
                {
                    Registro_Reporte.BeginEdit();

                    for (int Cont_For = 1; Cont_For <= 12; Cont_For++)
                    {
                        Str_Nombre_Mes = "";
                        if (Dic_Meses.ContainsKey(Cont_For) == true)
                        {
                            Str_Nombre_Mes = Dic_Meses[Cont_For];

                        }//    fin de la validacion del diccionario

                        if (String.IsNullOrEmpty(Registro_Reporte[Str_Nombre_Mes].ToString()))
                        {
                            Registro_Reporte[Str_Nombre_Mes] = 0;
                        }
                    }

                    Registro_Reporte.EndEdit();
                    Registro_Reporte.AcceptChanges();

                }

                //  se borraran las tarifas id, ya que comenzara con el tercer proceso
                foreach (DataRow Registro_Reporte in Dt_Reporte_Descarga.Rows)
                {
                    Registro_Reporte.BeginEdit();

                    for (int Cont_For = 1; Cont_For <= 12; Cont_For++)
                    {
                        Str_Nombre_Mes = "";
                        if (Dic_Meses.ContainsKey(Cont_For) == true)
                        {
                            Str_Nombre_Mes = Dic_Meses[Cont_For];

                        }//    fin de la validacion del diccionario

                        if (String.IsNullOrEmpty(Registro_Reporte[Str_Nombre_Mes].ToString()))
                        {
                            Registro_Reporte[Str_Nombre_Mes] = 0;
                        }
                    }

                    Registro_Reporte.EndEdit();
                    Registro_Reporte.AcceptChanges();

                }



                //******************************************************************************************************************
                //******************************************************************************************************************
                //  se ingresara la informacion
                //  se realizara la insercion de la informacion
                foreach (DataRow Registro in Dt_Reporte_Agua.Rows)
                {
                    Dt_Existencia.Clear();
                    Str_Nombre_Mes = Dic_Meses[DateTime.Now.Month];
                    Rs_Consulta.P_Str_Nombre_Mes = Str_Nombre_Mes;
                    Rs_Consulta.P_Anio = DateTime.Now.Year;
                    Rs_Consulta.P_Giro_Id = Registro["tarifa_id"].ToString();
                    Rs_Consulta.P_Dr_Registro = Registro;
                    Rs_Consulta.P_Str_Usuario = "Servicio";

                    //  se consulta la existencia
                    Dt_Existencia = Rs_Consulta.Consultar_Si_Historico_Agua();

                    //  validacion de la consulta
                    if (Dt_Existencia != null && Dt_Existencia.Rows.Count > 0)
                    {
                        //  actualizacion
                        Rs_Consulta.P_Id = Dt_Existencia.Rows[0]["ID"].ToString();
                        Rs_Consulta.Actualizar_Registro_Agua();

                    }// fin del if
                    else
                    {
                        //  insercion
                        Rs_Consulta.Insertar_Registro_Agua();

                    }// fin el else
                }

                //******************************************************************************************************************
                //******************************************************************************************************************
                //  se ingresara la informacion de los micromedidores **************************************************************
                foreach (DataRow Registro in Dt_Reporte_MicroMedidor.Rows)
                {
                    Dt_Existencia.Clear();
                    Str_Nombre_Mes = Dic_Meses[DateTime.Now.Month];
                    Rs_Consulta.P_Str_Nombre_Mes = Str_Nombre_Mes;
                    Rs_Consulta.P_Anio = DateTime.Now.Year;
                    Rs_Consulta.P_Giro_Id = Registro["tarifa_id"].ToString();
                    Rs_Consulta.P_Dr_Registro = Registro;
                    Rs_Consulta.P_Str_Usuario = "Servicio";

                    //  se consulta la existencia
                    Dt_Existencia = Rs_Consulta.Consultar_Si_Historico_Micromedidor();

                    //  validacion de la consulta
                    if (Dt_Existencia != null && Dt_Existencia.Rows.Count > 0)
                    {
                        //  actualizacion
                        Rs_Consulta.P_Id = Dt_Existencia.Rows[0]["ID"].ToString();
                        Rs_Consulta.Actualizar_Registro_Micromedidor();

                    }// fin del if
                    else
                    {
                        //  insercion
                        Rs_Consulta.Insertar_Registro_Micromedidor();

                    }// fin el else
                }


                //******************************************************************************************************************
                //******************************************************************************************************************
                //  se ingresara la informacion de los descargas **************************************************************
                foreach (DataRow Registro in Dt_Reporte_Descarga.Rows)
                {
                    Dt_Existencia.Clear();
                    Str_Nombre_Mes = Dic_Meses[DateTime.Now.Month];
                    Rs_Consulta.P_Str_Nombre_Mes = Str_Nombre_Mes;
                    Rs_Consulta.P_Anio = DateTime.Now.Year;
                    Rs_Consulta.P_Giro_Id = Registro["tarifa_id"].ToString();
                    Rs_Consulta.P_Dr_Registro = Registro;
                    Rs_Consulta.P_Str_Usuario = "Servicio";

                    //  se consulta la existencia
                    Dt_Existencia = Rs_Consulta.Consultar_Si_Historico_Descargas();

                    //  validacion de la consulta
                    if (Dt_Existencia != null && Dt_Existencia.Rows.Count > 0)
                    {
                        //  actualizacion
                        Rs_Consulta.P_Id = Dt_Existencia.Rows[0]["ID"].ToString();
                        Rs_Consulta.Actualizar_Registro_Descargas();

                    }// fin del if
                    else
                    {
                        //  insercion
                        Rs_Consulta.Insertar_Registro_Descargas();

                    }// fin el else
                }


            }
            catch (Exception Ex)
            {
                
            }
        }// fin del metodo



        /////*******************************************************************************************************
        ///// <summary>
        ///// genera un datatable nuevo con los campos para la 
        ///// </summary>
        ///// <returns>un datatable con los campos para mostrar accesos e ingresos por año y mes</returns>
        ///// <creo>Hugo Enrique Ramírez Aguilera</creo>
        ///// <fecha_creo>13-Enero-2016</fecha_creo>
        ///// <modifico></modifico>
        ///// <fecha_modifico></fecha_modifico>
        ///// <causa_modificacion></causa_modificacion>
        ///*******************************************************************************************************
        private DataTable Crear_Tabla_Reporte()
        {
            DataTable Dt_Reporte = new DataTable();

            try
            {
                Dt_Reporte.Columns.Add("tarifa_Id");
                Dt_Reporte.Columns.Add("giro_Id");
                Dt_Reporte.Columns.Add("Tomas_Por_Tarifa");
                Dt_Reporte.Columns.Add("Enero", typeof(System.Double));
                Dt_Reporte.Columns.Add("Febrero", typeof(System.Double));
                Dt_Reporte.Columns.Add("Marzo", typeof(System.Double));
                Dt_Reporte.Columns.Add("Abril", typeof(System.Double));
                Dt_Reporte.Columns.Add("Mayo", typeof(System.Double));
                Dt_Reporte.Columns.Add("Junio", typeof(System.Double));
                Dt_Reporte.Columns.Add("Julio", typeof(System.Double));
                Dt_Reporte.Columns.Add("Agosto", typeof(System.Double));
                Dt_Reporte.Columns.Add("Septiembre", typeof(System.Double));
                Dt_Reporte.Columns.Add("Octubre", typeof(System.Double));
                Dt_Reporte.Columns.Add("Noviembre", typeof(System.Double));
                Dt_Reporte.Columns.Add("Diciembre", typeof(System.Double));
                //Dt_Reporte.Columns.Add("Total", typeof(System.Double));
            }
            catch (Exception Ex)
            {
            
            }

            return Dt_Reporte;

        }// fin del metodo




    }
}
