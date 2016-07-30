using System.Data.SqlClient;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using Microsoft.WindowsAzure.Mobile.Service;


namespace ShelfService.Controllers
{
    public class ShelfController : ApiController
    {
        public static int Result { set; get; }

        public void GetShelfNum(int selected)
        {
            switch (selected)
            {
                case 1:
                    Result = 2000;
                    break;
                case 2:
                    Result = 5050;
                    break;
                case 3:
                    Result = 8100;
                    break;
                case -1:
                    Result = 10;
                    break;
            }
        }

        public string GetSelected()
        {
            return
                "<ROMAN" + Result + "ALEXEY>";
        }

        public List<string> GetShelfItems(string user, int shelfno)
        {
            //  try
            //    {
            //   return new List<string>() { "1", "2", "3" };
            //        return 2;
            //try
            //       }
            //        catch (Exception ex)
            //         {
            //             return 11;
            //         }

            List<string> result = new List<string>();
            using (SqlConnection connection = new SqlConnection("Server=tcp:r0yb33xx31.database.windows.net,1433;Database=romanService_db;User ID=romanService@r0yb33xx31;Password=Spalding1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;")) {
                connection.Open();
                string command = string.Format("SELECT Item from {0} WHERE ShelfNumber = '{1}'", user, shelfno);
                SqlCommand selectCommand = new SqlCommand(command, connection);
                SqlDataReader myReader = selectCommand.ExecuteReader();
                while (myReader.Read())
                {
                    string TempItem = myReader[0].ToString().Trim();
                    result.Add(TempItem);
                    //  result
                }
                //  myReader= selectCo

            }

            //      return result;
            return result;
        }
        public int GetShelfAddItem(string username, string item, int numshelf, string function)
        {
            using (SqlConnection connection = new SqlConnection("Server=tcp:r0yb33xx31.database.windows.net,1433;Database=romanService_db;User ID=romanService@r0yb33xx31;Password=Spalding1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;"))
            {
                connection.Open();
                string func="";
                if (function == "add")
                {
                    func = string.Format("INSERT INTO {0}(Item,ShelfNumber) VALUES ('{1}','{2}')", username, item, numshelf);
                }
                if (function == "remove")
                {
                    func = string.Format("DELETE FROM {0} WHERE Item = '{1}' AND ShelfNumber = '{2}'", username, item, numshelf);
                }
                    using (SqlCommand command = new SqlCommand(func, connection))
                        command.ExecuteNonQuery();
                    return 0;
                

            }
        }
 /*      
       public int GetShelfRemoveItem(string username, string item, int numshelf)
        {
            
            using (SqlConnection connection = new SqlConnection("Server=tcp:r0yb33xx31.database.windows.net,1433;Database=romanService_db;User ID=romanService@r0yb33xx31;Password=Spalding1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;"))
            {
                connection.Open();
                string removeItem = string.Format("INSERT INTO {0}(Item,ShelfNumber) VALUES ('{1}','{2}')", username, item, numshelf);
                using (SqlCommand command = new SqlCommand(removeItem, connection))
                command.ExecuteNonQuery();
                return 0;
            }
            
        }
  */      
        // insert or remove item function
        public int GetAddOrRemoveItem(string item, string user, int shelf , int function)
        {

            return 0;
        }

        public int GetAction(string user, string pass, int function)
        {
            
            return calc(user,pass,function);
        }
        private int calc(string user, string pass,int function)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(
                    "Server=tcp:r0yb33xx31.database.windows.net,1433;Database=romanService_db;User ID=romanService@r0yb33xx31;Password=Spalding1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;"))
                {
                    connection.Open();
                    switch (function)
                    {
                        case 1:    //login

             /*

                                   string insertComme = string.Format("INSERT INTO {0}(Item,ShelfNumber) VALUES ('Wine','3')", user);
                                         using (SqlCommand command = new SqlCommand(insertComme, connection))
                                                 command.ExecuteNonQuery();
                                                 */

                            // insert

                            //    string insertComm  = string.Format("INSERT INTO {0}(Item,ShelfNumber) VALUES ('Apple','1')", user);
                            //   string testcommand = string.Format("INSERT INTO UserTable(Username,Password) VALUES ('timur','bagadamov')");
                            //        string insertComma = string.Format("INSERT INTO {0}(Item,ShelfNumber) VALUES ('Ananas','1')", user);
                            //       string insertCommb = string.Format("INSERT INTO {0}(Item,ShelfNumber) VALUES ('Pie','2')", user);
                            //      string insertCommc = string.Format("INSERT INTO {0}(Item,ShelfNumber) VALUES ('Cake','2')", user);
                            //       string insertCommd = string.Format("INSERT INTO {0}(Item,ShelfNumber) VALUES ('Vodka','3')", user);
                            //       string insertComme = string.Format("INSERT INTO {0}(Item,ShelfNumber) VALUES ('Wine','3')", user);
                            //              using (SqlCommand command = new SqlCommand(testcommand, connection))
                            //                      command.ExecuteNonQuery();
                            //  //                using (SqlCommand command = new SqlCommand(insertComm, connection))
                            //                command.ExecuteNonQuery();
                            //                   using (SqlCommand command = new SqlCommand(insertComma, connection))
                            //                      command.ExecuteNonQuery();
                            //                  using (SqlCommand command = new SqlCommand(insertCommb, connection))
                            //                      command.ExecuteNonQuery();
                            //                  using (SqlCommand command = new SqlCommand(insertCommc, connection))
                            //                      command.ExecuteNonQuery();
                            //                  using (SqlCommand command = new SqlCommand(insertCommd, connection))
                            //                     command.ExecuteNonQuery();
                            //                 using (SqlCommand command = new SqlCommand(insertComme, connection))
                            //                     command.ExecuteNonQuery();

                            //insert done

                            string countTryy = string.Format("SELECT COUNT(*) from UserTable where Username = '{0}'", user);
                            int counter = 0;
                            using (SqlCommand command = new SqlCommand(countTryy, connection))
                                counter = (int)command.ExecuteScalar();
                     
                            if (counter == 0)
                            {
                                return 1;    //user   doesn't exist
                            }
                            if (counter == 1)   //user found , check pass
                            {
                              
                                string checkPass = string.Format("SELECT Password FROM UserTable where Username = '{0}'", user);
                                SqlCommand SelectCommand = new SqlCommand(checkPass, connection);
                                SqlDataReader myreader;
                                myreader = SelectCommand.ExecuteReader();
                                while (myreader.Read())
                                {

                                    //  Console.WriteLine(pass);
                                    //  Console.WriteLine(myreader[0].ToString());
                                    string TempPass = myreader[0].ToString();
                                    string RealPass = TempPass.Trim();
                                    if (RealPass==pass)
                                    {
                                       
                                        return 0;   //login done
                                    }
                                }
                                return 2;   //no pass

                                //       string checkPass = string.Format("SELECT COUNT(*) from UserTable where Username = '{0}' and Password = '{1}'", user, pass);
                                //        int spy = 0;
                                //       using (SqlCommand command = new SqlCommand(checkPass, connection))
                                //            spy = (int)command.ExecuteScalar();
                                //         if (spy == 0)
                                //       {
                                //          return 2; // password didn't match
                                //        }
                                //       if (spy == 1)
                                //        {
                                //           return 0; // login finished
                                //       }
                            }                          
                            break;
                        case 2:   //register
                      //      return 5;
                            string countTry = string.Format("SELECT COUNT(*) from UserTable where Username = '{0}'", user);
                            int userCount = 0;
                            using (SqlCommand command = new SqlCommand(countTry, connection))
                                userCount = (int)command.ExecuteScalar();
                            if (userCount > 0)
                            {
                                return userCount;    //already exists
                            }
                            string insertCommad = string.Format("INSERT INTO {0}(Username,Password) VALUES ('{1}','{2}')", "UserTable",user,pass);
                                using (SqlCommand command = new SqlCommand(insertCommad, connection))
                                command.ExecuteNonQuery();
                            string createTable = string.Format("CREATE TABLE {0}(Item char(50),ShelfNumber char(50));", user);
                                 using (SqlCommand command = new SqlCommand(createTable, connection))
                                 command.ExecuteNonQuery();
                                 
                            string num1 = "1";
                            string num2 = "2";
                            string num3 = "3";
                            string value1 = "The Shelf Is Empty";
                            string value2 = "The Shelf Is Empty";
                            string value3 = "The Shelf Is Empty";
                            string insertDummy1 = string.Format("INSERT INTO {0}(Item,ShelfNumber) VALUES ('{1}','{2}')", user,value1,num1);
                            string insertDummy2 = string.Format("INSERT INTO {0}(Item,ShelfNumber) VALUES ('{1}','{2}')", user,value2,num2);
                            string insertDummy3 = string.Format("INSERT INTO {0}(Item,ShelfNumber) VALUES ('{1}','{2}')", user,value3,num3);
                            using (SqlCommand command = new SqlCommand(insertDummy1, connection))
                                command.ExecuteNonQuery();
                            using (SqlCommand command = new SqlCommand(insertDummy2, connection))
                                command.ExecuteNonQuery();
                            using (SqlCommand command = new SqlCommand(insertDummy3, connection))
                                command.ExecuteNonQuery();
                                
                            return 0;       //registered user and created item table for him
                    }
                }
            }
            catch (Exception ex)
            {
                //handle exception
               Console.WriteLine(  ex.ToString());
            }
          return -1;
        }
        
    }
}
