/* This Class was written by all 4 members of our Group (Andrew, YuWen, Chris, and Vishnu)
 * on jan. 16, 2014 as a bridge between the Products and Suppliers tables
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TravelExperts.EntityDomainLibrary
{
    public static class Product_SupplierDB
    {

        //a get all Products using the SupplierId
        public static List<Product> GetAllProducts(int supplierId)
        {
            List<Product> products = new List<Product>();
            Product product;
            SqlConnection connection = TravelExpertsDB.GetConnection();
            string getProducts = "SELECT p.ProductId, ProdName " + 
                                 "FROM Products p, Products_Suppliers s " +
                                 "WHERE p.ProductId = s.ProductId AND s.SupplierId = @SupplierId";

            SqlCommand command = new SqlCommand(getProducts, connection);
            command.Parameters.AddWithValue("@SupplierId", supplierId);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    product = new Product();
                    product.ProductId = (int)reader["ProductId"];
                    product.ProdName = reader["ProdName"]as string;
                    products.Add(product);
                }
                return products;              
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

        //get all suppliers using a Product Id
        public static List<Supplier> GetAllSuppliers(int productId)
        {
            List<Supplier> suppliers = new List<Supplier>();
            Supplier supplier;
            SqlConnection connection = TravelExpertsDB.GetConnection();
            string getSuppliers = "SELECT p.SupplierId, SupName " + 
                                 "FROM Suppliers p, Products_Suppliers s " +
                                 "WHERE p.SupplierId = s.SupplierId AND s.ProductId = @ProductId";

            SqlCommand command = new SqlCommand(getSuppliers, connection);
            command.Parameters.AddWithValue("@ProductId", productId);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    supplier = new Supplier();
                    supplier.Id = (int)reader["SupplierId"];
                    supplier.Name = reader["SupName"] as string;
                    suppliers.Add(supplier);
                }
                return suppliers;              
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

        public static bool InsertProduct_Supplier(int prodId, int suppId)
        {

            SqlConnection connection = TravelExpertsDB.GetConnection();
            string commandStatement = "INSERT INTO Products_Suppliers (ProductId, SupplierId) Values(@ProductId, @SupplierId)";
           
            SqlCommand command = new SqlCommand(commandStatement, connection);

            command.Parameters.AddWithValue("@ProductId", prodId);
            command.Parameters.AddWithValue("@SupplierId", suppId);
            try
            {
                connection.Open();
                int result = command.ExecuteNonQuery();
                if (result > 0)
                    return true;
                else return false;
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
