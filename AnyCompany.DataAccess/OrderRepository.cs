using AnyCompany.Entities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AnyCompany.DataAccess
{
    public class OrderRepository
    {
        public bool SavedStatus { get; set; }
        public void Save(Order order)
        {
            try
            {
                SavedStatus = false;
                SqlParameter[] SqlParm =
                    {
                 new SqlParameter { ParameterName = "@OrderId", Value = order.OrderId, Direction = ParameterDirection.Input },
                 new SqlParameter { ParameterName = "@Amount", Value = order.Amount, Direction = ParameterDirection.Input },
                 new SqlParameter { ParameterName = "@VAT", Value = order.VAT, Direction = ParameterDirection.Input },
                 new SqlParameter { ParameterName = "@CustomerID", Value = order.CustomerID, Direction = ParameterDirection.Input }
                };

                BaseDb.ExecuteOnlySqlNonQuery(SqlParm, "INSERT INTO Orders VALUES (@OrderId, @Amount, @VAT,@CustomerID)"); //Instead of this, it should be a stored procedure which can insert order and return back orderiD or order number kind of thing...but ebcause of time constraint its left like this
                SavedStatus = true;
            }
            catch(Exception ex)
            {
                SavedStatus = false;
                //Log exception here
            }
            
        }
    }
}
