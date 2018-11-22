using AnyCompany.Entities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AnyCompany.DataAccess
{
    public static class CustomerRepository
    {

        public static Customer Load(int customerId)
        {
            Customer customer = null;
            try
            {
                if (customerId < 0)
                {
                    DataSet _result = null;

                    SqlParameter[] SqlParm =
                    {
                new SqlParameter { ParameterName = "@CustomerId", Value = customerId, Direction = ParameterDirection.Input },
                };


                    _result = BaseDb.ExecuteSqlText(SqlParm, "SELECT * FROM Customer WHERE CustomerId = @CustomerID");
                    if (_result != null)
                    {
                        customer = new Customer();
                        customer.Name = _result.Tables[0].Rows[0]["Name"].ToString();
                        customer.DateOfBirth = DateTime.Parse(_result.Tables[0].Rows[0]["DateOfBirth"].ToString());
                        customer.Country = _result.Tables[0].Rows[0]["Country"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //Log exceptions here
            }

            return customer;
        }
    }
}
