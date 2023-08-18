using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day6ass6
{
    internal class Program
    {
        static SqlCommand cmd;
        static SqlConnection con;
        static string conStr = "server=LAPTOP-N8306ABM;database=ProductInventoryDb;trusted_connection=true;";
        static SqlDataReader reader;

        static void Main(string[] args)
        {
            try
            {
                con = new SqlConnection(conStr);
            start:
                Console.WriteLine("1.View 2.Insert 3.Update 4.Remove Products");
                Console.WriteLine("Enter your choice");
                int op = int.Parse(Console.ReadLine());
                switch (op)
                {
                    case 1:
                        {
                            cmd = new SqlCommand("select * from Products", con); cmd.Connection = con;
                            con.Open();
                            reader = cmd.ExecuteReader();
                            Console.WriteLine("ProductID   ProductName\t Price\t Quantity\t   MFG\t\tEXP");
                            while (reader.Read())
                            {
                                Console.Write(reader["Pid"] + "\t    ");
                                Console.Write(reader["Pname"] + "\t  ");
                                Console.Write(reader["Price"] + "\t ");
                                Console.Write(reader["Quantity"] + "\t   ");
                                Console.Write(reader["MFDate"] + "\t");
                                Console.WriteLine(reader["ExpDate"] + "\t");
                                Console.WriteLine("\n");
                            }
                            con.Close();
                            break;
                        }
                    case 2:
                        {
                            con = new SqlConnection(conStr);
                            cmd = new SqlCommand()
                            {
                                CommandText = "insert into Products(Pid ,Pname,Price,Quantity,MFDate,ExpDate) values (@pid, @pn, @pr, @qn, @mfg,@exp)",
                                Connection = con
                            };
                            Console.WriteLine("Enter Product Id");
                            cmd.Parameters.AddWithValue("@pid", int.Parse(Console.ReadLine()));
                            Console.WriteLine("Enter Product Name");
                            cmd.Parameters.AddWithValue("@pn", Console.ReadLine());
                            Console.WriteLine("Enter Price");
                            cmd.Parameters.AddWithValue("@pr", decimal.Parse(Console.ReadLine()));
                            Console.WriteLine("Enter Product Quantity");
                            cmd.Parameters.AddWithValue("@qn", int.Parse(Console.ReadLine()));
                            Console.WriteLine("Enter Product MFGDate");
                            cmd.Parameters.AddWithValue("@mfg", Convert.ToDateTime(Console.ReadLine()));
                            Console.WriteLine("Enter Product ExpDate");
                            cmd.Parameters.AddWithValue("@exp", Convert.ToDateTime(Console.ReadLine()));

                            con.Open();
                            int nor = cmd.ExecuteNonQuery();
                            if (nor >= 1)
                            {
                                Console.WriteLine("Record Inserted!!!");
                            }

                            break;
                        }
                    case 3:
                        {
                            int id;
                            Console.WriteLine("Enter Product ID to update details ");
                            id = int.Parse(Console.ReadLine());
                            con = new SqlConnection(conStr);
                            cmd = new SqlCommand()
                            {
                                CommandText = "select * from Products where Pid=@id ",
                                Connection = con
                            };
                            cmd.Parameters.AddWithValue("@id", id);
                            con.Open();
                            reader = cmd.ExecuteReader();

                            if (reader.HasRows)
                            {
                                con.Close();
                                con.Open();
                                cmd.CommandText = "update Products set Pname=@pn, Price=@pr, Quantity=@qn,MFDate=@mfg,ExpDate=@exp where Pid=@eid";
                                Console.WriteLine("Enter Product Name");
                                cmd.Parameters.AddWithValue("@pn", Console.ReadLine());
                                Console.WriteLine("Enter Price");
                                cmd.Parameters.AddWithValue("@pr", decimal.Parse(Console.ReadLine()));
                                Console.WriteLine("Enter Product Quantity");
                                cmd.Parameters.AddWithValue("@qn", int.Parse(Console.ReadLine()));
                                Console.WriteLine("Enter Product MFGDate");
                                cmd.Parameters.AddWithValue("@mfg", Convert.ToDateTime(Console.ReadLine()));
                                Console.WriteLine("Enter Product ExpDate");
                                cmd.Parameters.AddWithValue("@exp", Convert.ToDateTime(Console.ReadLine()));
                                cmd.Parameters.AddWithValue("@eid", id);
                                cmd.ExecuteNonQuery(); Console.WriteLine("Record Updated");
                            }
                            else
                            {
                                Console.WriteLine($"No Such Id {id} exist in our database");
                            }
                            break;
                        }
                    case 4:
                        {
                            con = new SqlConnection(conStr);
                            cmd = new SqlCommand()
                            {
                                CommandText = "delete from Products where Pid=@id",
                                Connection = con
                            };
                            Console.WriteLine("Enter Product Id to Delete");
                            cmd.Parameters.AddWithValue("@id", int.Parse(Console.ReadLine()));

                            con.Open();
                            int nor = cmd.ExecuteNonQuery();
                            if (nor >= 1)
                            {
                                Console.WriteLine("Record Deleted!!!");
                            }
                            break;
                        }
                    default: { Console.WriteLine("Invalid choice"); return; }
                }
                Console.WriteLine("Do you want to continue Press 1");
                string ch = Console.ReadLine();
                if (ch == "1") { goto start; }
                else { Console.WriteLine("End"); }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally
            {
                con.Close();
                Console.ReadKey();
            }
        }
    }

}
    