using DIST.SUIST.BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;

namespace DIST.SUIST.Web
{
    public class Globales
    {
        #region Metodos Publicos

        public static string MonthName(int month)
        {
            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            return dtinfo.GetMonthName(month);
        }

        public static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            string[] arryS = s.Split(' ');
            string text = "";

            for (int i = 0; i < arryS.Length; i++)
                text += i == 0 ? char.ToUpper((arryS[i])[0]) + (arryS[i]).Substring(1) : " " + char.ToUpper((arryS[i])[0]) + (arryS[i]).Substring(1);

            return text;
        }

        public static DateTime ConvertirFecha(string strFecha)
        {
            string[] ArrayFecha = strFecha.Split(' ');
            string[] fecha = ArrayFecha[0].Split('/');
            string[] tiempo = ArrayFecha[1].Split(':');

            int dia = Convert.ToInt32(fecha[1]) , mes = Convert.ToInt32(fecha[0]), anio = Convert.ToInt32(fecha[2]);
            int hora = Convert.ToInt32(tiempo[0]), min = Convert.ToInt32(tiempo[1]);

            return new DateTime(anio, mes, dia, hora, min, 0);
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        #endregion
    }
}