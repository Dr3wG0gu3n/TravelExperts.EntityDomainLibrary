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

        //a Simple method to Get the Supplier using the SupplierId
        public static Supplier GetSupplierById(int supplierId)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            string selectStatement = "SELECT Supplier FROM Suppliers WHERE SupplierId = @SupplierId";

            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);

            try
            {
                selectCommand.Parameters.AddWithValue("@SupplierId", supplierId);
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
        public static int AddSupplier(Supplier supplier)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();

            string insertStatement ="INSERT into Suppliers (SupName) VALUES (@Name)";

            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);

            insertCommand.Parameters.AddWithValue("@Name", supplier.Name);
      
            try
            {
                connection.Open();

                int SupplierID = Convert.ToInt32(insertCommand.ExecuteScalar());    //returns the Id
                return SupplierID;
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
