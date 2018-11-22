using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.DataAccess
{
    public class BaseDb
    {
        private static string connString;
        public BaseDb()
        {
            //Connection string.
            connString = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;
        }

        /// <summary>
        /// This procedure will the Dataset
        /// </summary>
        /// <param name="SqlParm">The SQL parm.</param>
        /// <param name="SqlCommandText">The SQL command text.</param>
        /// <returns></returns>
        public static DataSet ExecuteSqlProcedure(SqlParameter[] SqlParm, string SqlCommandText)
        {
            //Variable declarations
            DataSet ObjectDataset = null;

            //creating sql command object and set up the parameter.
            using (SqlCommand sqlCommand = new SqlCommand { CommandType = CommandType.StoredProcedure, CommandText = SqlCommandText })
            {
                if (SqlParm != null)
                {
                    foreach (SqlParameter param in SqlParm)
                    {
                        sqlCommand.Parameters.Add(param);
                    }
                }
                //sqlCommand.Parameters.Add(SqlParm);
                ObjectDataset = FillDataSet(sqlCommand);
            }

            return ObjectDataset;
        }
        /// <summary>
        /// This function will return the string output.
        /// </summary>
        /// <param name="SqlParm">The SQL parm.</param>
        /// <param name="SqlCommandText">The SQL command text.</param>
        /// <returns></returns>
        public static string ExecuteSqlScalar(SqlParameter[] SqlParm, string SqlCommandText)
        {
            string Result = string.Empty;
            using (SqlConnection dbConn = new SqlConnection(connString))
            {
                dbConn.Open();
                using (SqlCommand sqlCommand = new SqlCommand { CommandType = CommandType.StoredProcedure, CommandText = SqlCommandText, Connection = dbConn })
                {
                    foreach (SqlParameter param in SqlParm)
                    {
                        sqlCommand.Parameters.Add(param);
                    }
                    //Result = sqlCommand.ExecuteScalar().ToString();
                    sqlCommand.ExecuteScalar();

                }

            }
            return Result;
        }

        /// <summary>
        /// This procedure will the Dataset 
        /// </summary>
        /// <param name="SqlParm"></param>
        /// <param name="SqlCommandText"></param>
        public static void ExecuteOnlySqlNonQuery(SqlParameter[] SqlParm, string SqlCommandText)
        {
            //Variable declarations

            //creating sql command object and set up the parameter.
            try
            {
                using (SqlConnection dbConn = new SqlConnection(connString))
                {
                    dbConn.Open();
                    using (SqlCommand sqlCommand = new SqlCommand { CommandType = CommandType.StoredProcedure, CommandText = SqlCommandText, Connection = dbConn })
                    {
                        sqlCommand.CommandTimeout = 7200;
                        if (SqlParm != null)
                        {
                            foreach (SqlParameter param in SqlParm)
                            {
                                sqlCommand.Parameters.Add(param);
                            }
                        }
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }
        /// <summary>
        /// Executes read only SELECT commands and returns a dataset.
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public static DataSet FillDataSet(SqlCommand sqlCommand)
        {
            DataSet ds = new DataSet();
            using (SqlConnection dbConn = new SqlConnection(connString))
            {
                dbConn.Open();
                sqlCommand.Connection = dbConn;
                using (SqlDataAdapter da = new SqlDataAdapter(sqlCommand))
                {
                    da.SelectCommand.CommandTimeout = dbConn.ConnectionTimeout;
                    da.Fill(ds);
                }
            }

            return ds;
        }

 
      
        /// <summary>
        /// Executes sql command passed as plain text with parameters
        /// </summary>
        /// <param name="SqlParm"></param>
        /// <param name="SqlCommandText"></param>
        /// <returns></returns>
        public static DataSet ExecuteSqlText(SqlParameter[] SqlParm, string SqlCommandText)
        {
            //Variable declarations
            DataSet ObjectDataset = null;

            //creating sql command object and set up the parameter.
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand { CommandType = CommandType.Text, CommandText = SqlCommandText })
                {
                    foreach (SqlParameter param in SqlParm)
                    {
                        sqlCommand.Parameters.Add(param);
                    }
                    //sqlCommand.Parameters.Add(SqlParm);
                    ObjectDataset = FillDataSet(sqlCommand);
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return ObjectDataset;
        }

    }
}
