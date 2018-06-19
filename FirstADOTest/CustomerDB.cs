using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstADOTest
{
    public static class CustomerDB
    {
        public static Customer GetCustomer(int Id)
        {
            Customer customer = new Customer();
            SqlConnection connection = ContactsDB.GetConnection();

            string selectStatement =
                "SELECT Id, Name, NickName " +
                "FROM Customer " +
                "WHERE Id = @Id";

            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@Id", Id);

            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    customer.Id = (int)reader["Id"];
                    customer.Name = reader["Name"].ToString();
                    customer.NickName = reader["NickName"].ToString();
                }
                else
                {
                    customer = null;
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return customer;
        }

        public static int AddCustomer(Customer customer)
        {
            
            SqlConnection connection = ContactsDB.GetConnection();
            string insertStatement =
                "INSERT Customer" +
                "(Name, NickName) " +
                "VALUES (@Name, @NickName)";


            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue("@Name", customer.Name);
            if (customer.NickName == "")
                insertCommand.Parameters.AddWithValue("@NickName", DBNull.Value);
            else
            insertCommand.Parameters.AddWithValue("@NickName", customer.NickName);
            
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                string selectStatement =
                    "SELECT IDENT_CURRENT('Customer') FROM Customer";
                SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
                int Id = Convert.ToInt32(selectCommand.ExecuteNonQuery());
                return Id;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
