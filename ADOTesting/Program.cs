using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace ADOTesting
{
    class Program
    {
        public static void Main()
        {
            string sqlconnectionstring = "Server = localhost; Database = adn103movies; Uid = root;";
            // Interfaces ADO.net
            IDbConnection conn = new MySqlConnection(sqlconnectionstring);
            conn?.Open();
            IDbCommand comm = conn.CreateCommand();
            comm.CommandText = "SELECT * FROM movies WHERE minutes < @p_minutes;";
            comm.CommandType = CommandType.Text;
            IDbDataParameter para = comm.CreateParameter();
            para.ParameterName = "@p_minutes";
            para.Value = 45;
            para.Direction = ParameterDirection.Input;
            para.DbType = DbType.Int16;
            comm.Parameters.Add(para);



            var reader = comm.ExecuteReader();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.Write($"{reader.GetName(i),40}");
            }
            Console.WriteLine();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write($"{reader[i],30}");
                }
                Console.WriteLine();
            }

            IDbCommand sec = conn.CreateCommand();
            sec.CommandText = "SELECT * FROM movies LEFT JOIN ratings ON movies.RatingID = ratings.ID";
            var data = sec.ExecuteReader();


            conn.Close();
        }
    }
}