/*SupplierDB class written by Andrew Goguen, and Vishnu on Jan. 16, 2014
 * to Access the Database and perform basic CRUD functions
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;              //we need to use these namespaces to access
using System.Data.SqlClient;    //databases

namespace TravelExperts.EntityDomainLibrary
{
    public static class SupplierDB
    {

        //a method to get a list of all suppliers from the Datasource
        public static List<Supplier> GetAllSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();
            Supplier sup;

            SqlConnection connection = TravelExpertsDB.GetConnection();
            string selectStatement = "SELECT * FROM Suppliers";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();

                while (reader.Read())
                {
                    sup = new Supplier((int)reader[0], reader[1].ToString());       //read in the values and assign to
                    suppliers.Add(sup);                                             //to new supplier object
                }
                return suppliers;                                                   //return the list of suppliers
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
               connection.Close();                                                 //close the connection
            }
        }

        //a Simple method to Get the Supplier using the SupplierId
        public static Supplier GetSupplierById(int supplierId)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            string selectStatement = "SELECT SupplierId,SupName FROM Suppliers WHERE SupplierId = @SupplierId";

            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@SupplierId", supplierId);
            try
            {
                connection.Open();
                SqlDataReader supplierReader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (supplierReader.Read())
                {
                    Supplier supplier = new Supplier();
                    supplier.Id = (int)supplierReader["SupplierId"];
                    supplier.Name = supplierReader["SupName"].ToString();
                    return supplier;
                }            
                else
                {
                    return null;
                }
            }

            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
             
        }

        //A method to Add a Supplier into the database, returning the newly created supplierId
        public static bool AddSupplier(Supplier supplier)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();

            string insertStatement ="INSERT into Suppliers (SupplierId, SupName) VALUES (@SupplierId, @Name)";

            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);

            insertCommand.Parameters.AddWithValue("@SupplierId", supplier.Id);
            insertCommand.Parameters.AddWithValue("@Name", supplier.Name);
      
            try
            {
                connection.Open();

                int count = Convert.ToInt32(insertCommand.ExecuteScalar());
                if (count > 0)                              //make sure the insert Supplier worked
                    return true;
                else return false;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
                catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
   
   

        public static bool UpdateSupplier(int supplierId, string newName)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();

            string updateStatement = "UPDATE Supplier Set Name = @NewName WHERE SupplierId = @SupplierId";

            SqlCommand updateCommand = new SqlCommand(updateStatement, connection);

            updateCommand.Parameters.AddWithValue("@SupplierId", supplierId);
            updateCommand.Parameters.AddWithValue("@NewName", newName);

            try
            {
                connection.Open();
                int count = updateCommand.ExecuteNonQuery();
                if (count > 0)
                    return true;
                else
                    return false;
            }
            catch(SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        // A method to delete a supplier using the supplier Id of the Supplier passed in
        public static bool DeleteSupplier(Supplier supplier)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            string deleteStatement = "DELETE FROM Suppliers WHERE SupplierId = @SupplierID";
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
            deleteCommand.Parameters.AddWithValue("@SupplierId", supplier.Id);

            try
            {
                connection.Open();
                int count = deleteCommand.ExecuteNonQuery();
                if (count > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
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
