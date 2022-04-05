using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web;
using Core.Common;
using System.Text.RegularExpressions;

namespace Core.Extensions
{
    public static class SqlExtensions
    {

        public static bool DataReaderHasColumn(this IDataReader reader, string columnName)
        {
            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName= '" + columnName + "'";
            return (reader.GetSchemaTable().DefaultView.Count > 0);
        }

        public static bool isNotNull(this IDataReader rd, string kolonAdi)
        {
            if (rd[kolonAdi] != null && rd[kolonAdi] != DBNull.Value)
                return true;
            else
                return false;
        }

        public static string drToString(this SqlDataReader dr, string kolonAdi)
        {
            if (dr.DataReaderHasColumn(kolonAdi) && dr.isNotNull(kolonAdi))
                return dr[kolonAdi].ToString();
            else
                return string.Empty;
        }

        public static int? drToInt(this SqlDataReader dr, string kolonAdi)
        {
            if (dr.DataReaderHasColumn(kolonAdi) && dr.isNotNull(kolonAdi))
                return (int?)dr[kolonAdi];
            else
                return null;
        }

        public static DateTime? drToDateTime(this SqlDataReader dr, string kolonAdi)
        {
            if (dr.DataReaderHasColumn(kolonAdi) && dr.isNotNull(kolonAdi))
                return (DateTime?)dr[kolonAdi];
            else
                return null;
        }
        public static bool drToBoolean2(this SqlDataReader dr, string kolonAdi)
        {
            bool? v = drToBoolean(dr, kolonAdi);
            if (v == null)
                return false;
            else
                return v.Value;
        }
        public static bool? drToBoolean(this SqlDataReader dr, string kolonAdi)
        {
            if (dr.DataReaderHasColumn(kolonAdi) && dr.isNotNull(kolonAdi))
                return (bool?)dr[kolonAdi];
            else
                return null;
        }
        public static Guid? drToGuid(this SqlDataReader dr, string kolonAdi)
        {
            if (dr.DataReaderHasColumn(kolonAdi) && dr.isNotNull(kolonAdi))
                return (Guid?)dr[kolonAdi];
            else
                return null;
        }
        public static Guid drToGuid2(this SqlDataReader dr, string kolonAdi)
        {
            if (dr.DataReaderHasColumn(kolonAdi) && dr.isNotNull(kolonAdi))
                return (Guid)dr[kolonAdi];
            else
                return Guid.Empty;
        }
        public static double? drToDouble(this SqlDataReader dr, string kolonAdi)
        {
            if (dr.DataReaderHasColumn(kolonAdi) && dr.isNotNull(kolonAdi))
            {
                return Math.Round(((double?)dr[kolonAdi]).Value, 2);
            }
            else
                return null;
        }
        public static decimal? drToDecimal(this SqlDataReader dr, string kolonAdi)
        {
            if (dr.DataReaderHasColumn(kolonAdi) && dr.isNotNull(kolonAdi))
            {
                return Math.Round(((decimal?)dr[kolonAdi]).Value, 2);
            }
            else
                return null;
        }

        public static object fillerSetter(this SqlDataReader dr, string kolonAdi, Type propType)
        {
            if (dr.DataReaderHasColumn(kolonAdi) && dr.isNotNull(kolonAdi))
            {
                object o;
                if (propType == typeof(string))
                    o = dr.drToString(kolonAdi);
                else if (propType == typeof(Nullable<bool>))
                    o = dr.drToBoolean(kolonAdi);
                else if (propType == typeof(Nullable<int>))
                    o = dr.drToInt(kolonAdi);
                else if (propType == typeof(Nullable<DateTime>))
                    o = dr.drToDateTime(kolonAdi);
                else if (propType == typeof(Nullable<decimal>))
                    o = dr.drToDecimal(kolonAdi);
                else if (propType == typeof(Nullable<double>))
                    o = dr.drToDouble(kolonAdi);
                else if (propType == typeof(int))
                    o = dr.drToInt(kolonAdi);
                else if (propType == typeof(bool))
                    o = dr.drToBoolean2(kolonAdi);
                else if (propType == typeof(Guid))
                    o = dr.drToGuid2(kolonAdi);
                else if (propType == typeof(Nullable<Guid>))
                    o = dr.drToGuid(kolonAdi);
                else
                    o = null;
                return o;
            }
            else
                return null;
        }

    }
}
