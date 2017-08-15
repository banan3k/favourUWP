using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace JSONWebAPI
{

    public class ServiceAPI : IServiceAPI
    {
        SqlConnection dbConnection;

        public ServiceAPI()
        {
            dbConnection = DBConnect.getConnection();
        }

        string passwordGeneretor()
        {
            string pass = "";
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                int num = rand.Next(0, 26); // Zero to 25
                char let = (char)('A' + num);
                pass += let;
            }
            Debug.WriteLine("password for you : " + pass);
            return pass;
        }

        public void updateRequest(int chatID, int requestID, int whatAnswer)
        {
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            string query = "UPDATE chat"+ chatID + " SET answer="+ whatAnswer + " WHERE id="+ requestID + ";";
            SqlCommand command = new SqlCommand(query, dbConnection);
            command.ExecuteNonQuery();
            dbConnection.Close();
        }
        public void passRequest(int chatID, string content, string from)
        {
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            string query = "INSERT INTO chat"+ chatID+" VALUES ('" + content + "',null,'" + from + "');";
            SqlCommand command = new SqlCommand(query, dbConnection);
            command.ExecuteNonQuery();
            dbConnection.Close();
        }

        public int CreateNewAccount(string nick, string passwordAcc, string chatName)
        {
            string passGonnaBe = "";
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            string query = "";
            SqlCommand command;
            int passowrdPass = 0;
            while (passowrdPass == 0)
            {
                try
                {
                    passGonnaBe = passwordGeneretor();
                    query = "INSERT INTO chatList VALUES ('" + passGonnaBe + "','" + chatName + "');";
                    command = new SqlCommand(query, dbConnection);
                    command.ExecuteNonQuery();
                    passowrdPass = 1;
                }
                catch(SqlException ex)
                {
                    passowrdPass = 0;
                }
            }



            int toReturnID = -1;
            try
            {
                query = "INSERT INTO UserDetails VALUES ('" + nick + "','" + passwordAcc + "',IDENT_CURRENT('chatList'));";

                command = new SqlCommand(query, dbConnection);
                command.ExecuteNonQuery();

             
            }
            catch(SqlException ex)
            {
                toReturnID = -2;
                query = "delete from chatList where pass="+ passGonnaBe + ";";

                command = new SqlCommand(query, dbConnection);
                command.ExecuteNonQuery();
            };

            if (toReturnID != -2)
            {
                DataTable userDetailsTable = new DataTable();
                userDetailsTable.Columns.Add(new DataColumn("idToChat", typeof(String)));
                //  query = " SELECT IDENT_CURRENT('chatList') as 'idToChat';";
                query = "select max(id) as 'idToChat' from chatList;";
                command = new SqlCommand(query, dbConnection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        userDetailsTable.Rows.Add(reader["idToChat"]);

                    }
                }
                reader.Close();


                int.TryParse(userDetailsTable.Rows[0][0].ToString(), out toReturnID);
                //userDetailsTable.Rows[0][0].ToString()
                query = "create table chat" + toReturnID + "(id integer primary key identity(1,1),content varchar(10) not null,answer varchar(max), fromWho varchar(max)); ";

                command = new SqlCommand(query, dbConnection);
                command.ExecuteNonQuery();

                query = "create table listForUser" + toReturnID + "(id integer primary key identity(1,1),chatID integer unique, FOREIGN KEY (chatID) REFERENCES chatList(id)); ";
                command = new SqlCommand(query, dbConnection);
                command.ExecuteNonQuery();

                dbConnection.Close();

            }
            return toReturnID;
        }

        public DataTable addChatForUser2(int id, string pass)
        {
            DataTable userDetailsTable = new DataTable();
            userDetailsTable.Columns.Add(new DataColumn("id", typeof(String)));


            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

           // string query = "SELECT nick,chatID FROM UserDetails WHERE id='" + id + "';";
            string query = "SELECT id FROM chatList WHERE pass='" + pass + "';";

            SqlCommand command = new SqlCommand(query, dbConnection);
            SqlDataReader reader = command.ExecuteReader();
            int addedID = -1;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    userDetailsTable.Rows.Add(reader["id"]);
                    addedID = int.Parse(reader["id"].ToString());
                    
                }
            }
            reader.Close();
            if (addedID != -1)
            {
                string query2 = "INSERT INTO listForUser" + id + " VALUES ('" + addedID + "');";
                SqlCommand command2 = new SqlCommand(query2, dbConnection);
                command2.ExecuteNonQuery();
            }

            
            dbConnection.Close();
            return userDetailsTable;

        }

        public DataTable addChatForUser(int id, string pass)
        {
            int addedID=-1;// = chatAuthentication(pass);



            DataTable userDetailsTable = new DataTable();
            userDetailsTable.Columns.Add(new DataColumn("add", typeof(String)));

            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }


            string query;
            SqlCommand command;
            query = "SELECT id FROM chatList WHERE pass='" + pass + "';";

            
            command = new SqlCommand(query, dbConnection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
               // auth = true;
                while (reader.Read())
                {
                    int.TryParse(reader["id"].ToString(), out addedID);
                }
            }

            if (addedID != -1)
            {
                //userDetailsTable.Rows.Add("1");
                try
                {
                    query = "INSERT INTO listForUser" + id + " VALUES ('" + addedID + "');";
                    command = new SqlCommand(query, dbConnection);
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex) { };
            }
          //  else
          //      userDetailsTable.Rows.Add("0");
            dbConnection.Close();
            return userDetailsTable;
        }
        public int chatAuthentication(string passsword)
        {
            bool auth = false;
            int idReturn = -1;
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            string query = "SELECT id FROM chatList WHERE pass='" + passsword + "';";

            SqlCommand command = new SqlCommand(query, dbConnection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                auth = true;
                while (reader.Read())
                {
                    int.TryParse(reader["id"].ToString(),out idReturn);
                }
            }

            reader.Close();
            dbConnection.Close();

            return idReturn;

        }



        public DataTable GetUserDetails(int id)
        {
            DataTable userDetailsTable = new DataTable();
            userDetailsTable.Columns.Add(new DataColumn("nick", typeof(String)));
            userDetailsTable.Columns.Add(new DataColumn("chatID", typeof(String)));

            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            string query = "SELECT nick,chatID FROM UserDetails WHERE id='" + id + "';";

            SqlCommand command = new SqlCommand(query, dbConnection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    userDetailsTable.Rows.Add(reader["nick"], reader["chatID"]);
                }
            }

            reader.Close();
            dbConnection.Close();
            return userDetailsTable;

        }

        public DataTable UserAuthentication(string nick, string pass)
        {
            DataTable userDetailsTable = new DataTable();
            userDetailsTable.Columns.Add(new DataColumn("id", typeof(String)));
   

            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            string query = "SELECT id FROM UserDetails WHERE nick='"+ nick + "' AND pass='"+ pass + "';";

            SqlCommand command = new SqlCommand(query, dbConnection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    userDetailsTable.Rows.Add(reader["id"]);
                }
            }

            reader.Close();
            dbConnection.Close();
            return userDetailsTable;

        }
        /* public bool UserAuthentication(string userName, string pass)
         {
             bool auth = false;

             if (dbConnection.State.ToString() == "Closed")
             {
                 dbConnection.Open();
             }

          //   string query = "SELECT id FROM UserDetails WHERE nick='" + userName + "' AND pass='" + password + "';";
             string query = "SELECT id FROM UserDetails WHERE nick='admin' AND pass='test';";
             SqlCommand command = new SqlCommand(query, dbConnection);
             SqlDataReader reader = command.ExecuteReader();

             if (reader.HasRows)
             {
                 auth = true;
             }

             reader.Close();
             dbConnection.Close();

             return auth;

         }*/

        public DataTable checkAnswers(int id, int ifAdmin)
        {

            DataTable deptTable = new DataTable();
            deptTable.Columns.Add("id", typeof(String));


            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            string query;
            if (ifAdmin == 0)
                query = "SELECT id FROM chat" + id + " where answer is null;";
            else
                query = "SELECT id FROM chat" + id + " where answer is not null and fromWho='" + ifAdmin + "' and getIt=0;";
            SqlCommand command = new SqlCommand(query, dbConnection);
            SqlDataReader reader = command.ExecuteReader();

            

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    
                    deptTable.Rows.Add(reader["id"]);
                    //    deptTable.Rows.Add(reader["content"], reader["answer"]);
                }
            }


            reader.Close();

            


            dbConnection.Close();

            return deptTable;

        }


        public DataTable getChatContent(int id, int ifAdmin)
        {

            DataTable deptTable = new DataTable();
            deptTable.Columns.Add("id", typeof(String));
            deptTable.Columns.Add("content", typeof(String));
            deptTable.Columns.Add("answer", typeof(String));
            deptTable.Columns.Add("fromWho", typeof(String));

            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            string query;
            if (ifAdmin==0)
                query = "SELECT id,content,answer,fromWho FROM chat"+ id + " ORDER BY id ASC;";
            else
                query = "SELECT id,content,answer,fromWho FROM chat" + id + " where fromWho='"+ ifAdmin + "' ORDER BY id ASC;";
            SqlCommand command = new SqlCommand(query, dbConnection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string answer = "";
                    if (reader["answer"] == null)
                        answer = "null";
                    else
                        answer = reader["answer"].ToString();
                    deptTable.Rows.Add(reader["id"],reader["content"], answer, reader["fromWho"]);
                //    deptTable.Rows.Add(reader["content"], reader["answer"]);
                }
            }

            reader.Close();

            if (ifAdmin != 0)
            {
                query = "update chat"+ id + " set getIt=1 where answer is not null and fromWho='" + ifAdmin + "' and getIt=0;";
                command = new SqlCommand(query, dbConnection);
                command.ExecuteNonQuery();
            }

            dbConnection.Close();

            return deptTable;

        }

       

        public DataTable getChatList(int id)
        {

            DataTable deptTable = new DataTable();
            deptTable.Columns.Add("chatID", typeof(String));
           

            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }

            string query = "SELECT chatID FROM listForUser" + id + " ;";
            SqlCommand command = new SqlCommand(query, dbConnection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    deptTable.Rows.Add(reader["chatID"]);

                }
            }

            reader.Close();
            dbConnection.Close();

            return deptTable;

        }
    }
}