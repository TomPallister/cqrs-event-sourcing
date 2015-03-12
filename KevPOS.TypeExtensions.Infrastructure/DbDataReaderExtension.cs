using System;
using System.Data.Common;
using System.Globalization;

namespace KevPOS.TypeExtensions.Infrastructure
{
    public static class DbDataReaderExtension
    {
        /// <summary>
        ///     Returns a boolean value from a datareader for the specified column. If the db value is null the supplied default
        ///     value is returned
        /// </summary>
        /// <param name="dr">The datareader from which the value is to be retrieved</param>
        /// <param name="columnName">The name of the column in the datareader from which the value should be taken</param>
        /// <param name="defaultValue">The default value to be used in the case where the db value is null</param>
        /// <returns>
        ///     A boolean value representing the column value in the datareader or the supplied default if no value is present
        ///     in the datareader
        /// </returns>
        public static bool SafeValue(this DbDataReader dr, string columnName, bool defaultValue)
        {
            return dr.SafeBool(columnName, defaultValue);
        }

        /// <summary>
        ///     Returns an integer value from a datareader for the specified column. If the db value is null the supplied default
        ///     value is returned
        /// </summary>
        /// <param name="dr">The datareader from which the value is to be retrieved</param>
        /// <param name="columnName">The name of the column in the datareader from which the value should be taken</param>
        /// <param name="defaultValue">The default value to be used in the case where the db value is null</param>
        /// <returns>
        ///     An integer value representing the column value in the datareader or the supplied default if no value is
        ///     present in the datareader
        /// </returns>
        public static int SafeValue(this DbDataReader dr, string columnName, int defaultValue)
        {
            return dr.SafeInt(columnName, defaultValue);
        }

        public static long SafeValue(this DbDataReader dr, string columnName, long defaultValue)
        {
            return dr.SafeLong(columnName, defaultValue);
        }

        /// <summary>
        ///     Returns a string value from a datareader for the specified column. If the db value is null the supplied default
        ///     value is returned
        /// </summary>
        /// <param name="dr">The datareader from which the value is to be retrieved</param>
        /// <param name="columnName">The name of the column in the datareader from which the value should be taken</param>
        /// <param name="defaultValue">The default value to be used in the case where the db value is null</param>
        /// <returns>
        ///     A string value representing the column value in the datareader or the supplied default if no value is present
        ///     in the datareader
        /// </returns>
        public static string SafeValue(this DbDataReader dr, string columnName, string defaultValue)
        {
            return dr.SafeString(columnName, defaultValue);
        }


        /// <summary>
        ///     Returns a char value from a datareader for the specified column. If the db value is null the supplied default value
        ///     is returned
        /// </summary>
        /// <param name="dr">The datareader from which the value is to be retrieved</param>
        /// <param name="columnName">The name of the column in the datareader from which the value should be taken</param>
        /// <param name="defaultValue">The default value to be used in the case where the db value is null</param>
        /// <returns>
        ///     A char value representing the column value in the datareader or the supplied default if no value is present in
        ///     the datareader
        /// </returns>
        public static char SafeValue(this DbDataReader dr, string columnName, char defaultValue)
        {
            return dr.SafeChar(columnName, defaultValue);
        }

        /// <summary>
        ///     Returns a decimal value from a datareader for the specified column. If the db value is null the supplied default
        ///     value is returned
        /// </summary>
        /// <param name="dr">The datareader from which the value is to be retrieved</param>
        /// <param name="columnName">The name of the column in the datareader from which the value should be taken</param>
        /// <param name="defaultValue">The default value to be used in the case where the db value is null</param>
        /// <returns>
        ///     A decimal value representing the column value in the datareader or the supplied default if no value is present
        ///     in the datareader
        /// </returns>
        public static decimal SafeValue(this DbDataReader dr, string columnName, decimal defaultValue)
        {
            return dr.SafeDecimal(columnName, defaultValue);
        }

        /// <summary>
        ///     Returns a double value from a datareader for the specified column. If the db value is null the supplied default
        ///     value is returned
        /// </summary>
        /// <param name="dr">The datareader from which the value is to be retrieved</param>
        /// <param name="columnName">The name of the column in the datareader from which the value should be taken</param>
        /// <param name="defaultValue">The default value to be used in the case where the db value is null</param>
        /// <returns>
        ///     A double value representing the column value in the datareader or the supplied default if no value is present
        ///     in the datareader
        /// </returns>
        public static double SafeValue(this DbDataReader dr, string columnName, double defaultValue)
        {
            return dr.SafeDouble(columnName, defaultValue);
        }

        public static Guid SafeValue(this DbDataReader dr, string columnName, Guid defaultValue)
        {
            return dr.SafeGuid(columnName, defaultValue);
        }

        /// <summary>
        ///     Returns a DateTime value from a datareader for the specified column. If the db value is null the supplied default
        ///     value is returned
        /// </summary>
        /// <param name="dr">The datareader from which the value is to be retrieved</param>
        /// <param name="columnName">The name of the column in the datareader from which the value should be taken</param>
        /// <param name="defaultValue">The default value to be used in the case where the db value is null</param>
        /// <returns>
        ///     A DateTime value representing the column value in the datareader or the supplied default if no value is
        ///     present in the datareader
        /// </returns>
        public static DateTime SafeValue(this DbDataReader dr, string columnName, DateTime defaultValue)
        {
            return dr.SafeDateTime(columnName, defaultValue);
        }

        /// <summary>
        ///     Returns a boolean value from a datareader for the specified column. If the db value is null the supplied default
        ///     value is returned
        /// </summary>
        /// <param name="dr">The datareader from which the value is to be retrieved</param>
        /// <param name="columnName">The name of the column in the datareader from which the value should be taken</param>
        /// <param name="defaultValue">The default value to be used in the case where the db value is null</param>
        /// <returns>
        ///     A boolean value representing the column value in the datareader or the supplied default if no value is present
        ///     in the datareader
        /// </returns>
        public static bool SafeBool(this DbDataReader dr, string columnName, bool defaultValue)
        {
            object dbValue = dr[columnName];

            if (dbValue == DBNull.Value || dbValue == null)
                return defaultValue;

            bool bValue;

            if (dbValue is bool)
            {
                bValue = (bool) dbValue;
            }
            else
            {
                switch ((int) dbValue)
                {
                    case 1:
                        bValue = true;
                        break;
                    case 0:
                        bValue = false;
                        break;
                    default:
                        bValue = defaultValue;
                        break;
                }
            }
            return bValue;
        }

        /// <summary>
        ///     Returns an integer value from a datareader for the specified column. If the db value is null the supplied default
        ///     value is returned
        /// </summary>
        /// <param name="dr">The datareader from which the value is to be retrieved</param>
        /// <param name="columnName">The name of the column in the datareader from which the value should be taken</param>
        /// <param name="defaultValue">The default value to be used in the case where the db value is null</param>
        /// <returns>
        ///     An integer value representing the column value in the datareader or the supplied default if no value is
        ///     present in the datareader
        /// </returns>
        public static int SafeInt(this DbDataReader dr, string columnName, int defaultValue)
        {
            object dbValue = dr[columnName];

            return dbValue == DBNull.Value
                ? defaultValue
                : Int32.Parse(dbValue.ToString(), CultureInfo.InvariantCulture);
        }

        public static long SafeLong(this DbDataReader dr, string columnName, long defaultValue)
        {
            object dbValue = dr[columnName];

            return dbValue == DBNull.Value
                ? defaultValue
                : Int64.Parse(dbValue.ToString(), CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Returns a string value from a datareader for the specified column. If the db value is null the supplied default
        ///     value is returned
        /// </summary>
        /// <param name="dr">The datareader from which the value is to be retrieved</param>
        /// <param name="columnName">The name of the column in the datareader from which the value should be taken</param>
        /// <param name="defaultValue">The default value to be used in the case where the db value is null</param>
        /// <returns>
        ///     A string value representing the column value in the datareader or the supplied default if no value is present
        ///     in the datareader
        /// </returns>
        public static string SafeString(this DbDataReader dr, string columnName, string defaultValue)
        {
            object dbValue = dr[columnName];

            return dbValue == DBNull.Value ? defaultValue : dbValue.ToString().Trim();
        }

        /// <summary>
        ///     Returns a char value from a datareader for the specified column. If the db value is null the supplied default value
        ///     is returned
        /// </summary>
        /// <param name="dr">The datareader from which the value is to be retrieved</param>
        /// <param name="columnName">The name of the column in the datareader from which the value should be taken</param>
        /// <param name="defaultValue">The default value to be used in the case where the db value is null</param>
        /// <returns>
        ///     A char value representing the column value in the datareader or the supplied default if no value is present in
        ///     the datareader
        /// </returns>
        public static char SafeChar(this DbDataReader dr, string columnName, char defaultValue)
        {
            object dbValue = dr[columnName];

            return dbValue == DBNull.Value ? defaultValue : char.Parse(dbValue.ToString());
        }

        /// <summary>
        ///     Returns a decimal value from a datareader for the specified column. If the db value is null the supplied default
        ///     value is returned
        /// </summary>
        /// <param name="dr">The datareader from which the value is to be retrieved</param>
        /// <param name="columnName">The name of the column in the datareader from which the value should be taken</param>
        /// <param name="defaultValue">The default value to be used in the case where the db value is null</param>
        /// <returns>
        ///     A decimal value representing the column value in the datareader or the supplied default if no value is present
        ///     in the datareader
        /// </returns>
        public static decimal SafeDecimal(this DbDataReader dr, string columnName, decimal defaultValue)
        {
            object dbValue = dr[columnName];

            return dbValue == DBNull.Value
                ? defaultValue
                : decimal.Parse(dbValue.ToString(), CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Returns a double value from a datareader for the specified column. If the db value is null the supplied default
        ///     value is returned
        /// </summary>
        /// <param name="dr">The datareader from which the value is to be retrieved</param>
        /// <param name="columnName">The name of the column in the datareader from which the value should be taken</param>
        /// <param name="defaultValue">The default value to be used in the case where the db value is null</param>
        /// <returns>
        ///     A double value representing the column value in the datareader or the supplied default if no value is present
        ///     in the datareader
        /// </returns>
        public static double SafeDouble(this DbDataReader dr, string columnName, double defaultValue)
        {
            object dbValue = dr[columnName];

            return dbValue == DBNull.Value
                ? defaultValue
                : double.Parse(dbValue.ToString(), CultureInfo.InvariantCulture);
        }

        public static Guid SafeGuid(this DbDataReader dr, string columnName, Guid defaultValue)
        {
            object dbValue = dr[columnName];

            return dbValue == DBNull.Value ? defaultValue : Guid.Parse(dbValue.ToString());
        }

        /// <summary>
        ///     Returns a DateTime value from a datareader for the specified column. If the db value is null the supplied default
        ///     value is returned
        /// </summary>
        /// <param name="dr">The datareader from which the value is to be retrieved</param>
        /// <param name="columnName">The name of the column in the datareader from which the value should be taken</param>
        /// <param name="defaultValue">The default value to be used in the case where the db value is null</param>
        /// <returns>
        ///     A DateTime value representing the column value in the datareader or the supplied default if no value is
        ///     present in the datareader
        /// </returns>
        public static DateTime SafeDateTime(this DbDataReader dr, string columnName, DateTime defaultValue)
        {
            object dbValue = dr[columnName];

            return dbValue != DBNull.Value && dbValue is DateTime ? (DateTime) dbValue : defaultValue;
        }
    }
}