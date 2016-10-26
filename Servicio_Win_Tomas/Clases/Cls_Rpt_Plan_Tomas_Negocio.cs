using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Reportes_Planeacion.Tomas.Datos;
using System.Data;

namespace Reportes_Planeacion.Tomas.Negocio
{
    public class Cls_Rpt_Plan_Tomas_Negocio
    {
        public Cls_Rpt_Plan_Tomas_Negocio()
        {
        }


        #region Variables_Publicas
        public String P_Rpu { get; set; }
        public String P_No_Cuenta { get; set; }
        public Int32 P_Anio { get; set; }
        public String P_Str_Nombre_Mes { get; set; }
        public String P_Giro_Id { get; set; }
        public DataRow P_Dr_Registro { get; set; }
        public String P_Str_Usuario { get; set; }
        public String P_Id{ get; set; }
        #endregion


        #region Consultas
        public DataTable Consultar_Tarifas_Giro() { return Cls_Rpt_Plan_Tomas_Datos.Consultar_Tarifas_Giro(this); }

        public DataTable Consultar_Tomas_Realizadas() { return Cls_Rpt_Plan_Tomas_Datos.Consultar_Tomas_Realizadas(this); }
        public DataTable Consultar_Tomas_Con_Medidor() { return Cls_Rpt_Plan_Tomas_Datos.Consultar_Tomas_Con_Medidor(this); }
        public DataTable Consultar_Tomas_Descarga() { return Cls_Rpt_Plan_Tomas_Datos.Consultar_Tomas_Descarga(this); }


        //  metodos para el historico de registros de "AGUA"
        public DataTable Consultar_Tabla_Historicos_Agua() { return Cls_Rpt_Plan_Tomas_Datos.Consultar_Tabla_Historicos_Agua(this); }
        public DataTable Consultar_Si_Historico_Agua() { return Cls_Rpt_Plan_Tomas_Datos.Consultar_Si_Historico_Agua(this); }
        public void Insertar_Registro_Agua() { Cls_Rpt_Plan_Tomas_Datos.Insertar_Registro_Agua(this); }
        public void Actualizar_Registro_Agua() { Cls_Rpt_Plan_Tomas_Datos.Actualizar_Registro_Agua(this); }

        //  metodos para el historico de registros de "MICROMEDIDOR"
        public DataTable Consultar_Tabla_Historicos_Micromedidor() { return Cls_Rpt_Plan_Tomas_Datos.Consultar_Tabla_Historicos_Micromedidor(this); }
        public DataTable Consultar_Si_Historico_Micromedidor() { return Cls_Rpt_Plan_Tomas_Datos.Consultar_Si_Historico_Micromedidor(this); }
        public void Insertar_Registro_Micromedidor() { Cls_Rpt_Plan_Tomas_Datos.Insertar_Registro_Micromedidor(this); }
        public void Actualizar_Registro_Micromedidor() { Cls_Rpt_Plan_Tomas_Datos.Actualizar_Registro_Micromedidor(this); }

        //  metodos para el historico de registros de "Descargas"
        public DataTable Consultar_Tabla_Historicos_Descargas() { return Cls_Rpt_Plan_Tomas_Datos.Consultar_Tabla_Historicos_Descargas(this); }
        public DataTable Consultar_Si_Historico_Descargas() { return Cls_Rpt_Plan_Tomas_Datos.Consultar_Si_Historico_Descargas(this); }
        public void Insertar_Registro_Descargas() { Cls_Rpt_Plan_Tomas_Datos.Insertar_Registro_Descargas(this); }
        public void Actualizar_Registro_Descargas() { Cls_Rpt_Plan_Tomas_Datos.Actualizar_Registro_Descargas(this); }

        #endregion


        #region 05 Cartera Vencida
        public DataTable Consultar_05_Cartera_Vencida() { return Cls_Rpt_Plan_Tomas_Datos.Consultar_05_Cartera_Vencida(this); }

        #endregion Fin 05 cartera vencida
    }
}