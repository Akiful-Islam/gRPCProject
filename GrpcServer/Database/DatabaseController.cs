using MySql.Data.MySqlClient;
using System;

namespace GrpcServer.Database
{
    public class DatabaseController
    {
        MySqlConnection mySqlConnection;
        public readonly string dbConnectionString = String.Format("server={0}; uid={1}; pwd={2};database={3}", "localhost", "root", "", "grpc_project");
        public DatabaseController()
        {
            mySqlConnection = new MySqlConnection(dbConnectionString);
        }

        public int InsertUserInfo(UserInfoModel userInfoModel)
        {

            int result;

            mySqlConnection.Open();

            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.Parameters.AddWithValue("@username", userInfoModel.username);
            mySqlCommand.Parameters.AddWithValue("@email", userInfoModel.email);
            mySqlCommand.Parameters.AddWithValue("@password", userInfoModel.password);

            mySqlCommand.CommandText = "INSERT INTO user_info (user_name, email_address, user_password) VALUES (@username, @email, @password)";

            try
            {
                result = mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result = -1;
            }
            finally
            {
                mySqlConnection.Close();
            }
            return result;
        }
        public int CheckUserInfo(UserInfoModel userInfoModel)
        {
            int userId = -1;

            mySqlConnection.Open();

            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.Parameters.AddWithValue("@username", userInfoModel.username);
            mySqlCommand.Parameters.AddWithValue("@password", userInfoModel.password);

            mySqlCommand.CommandText = "SELECT * FROM user_info WHERE user_name = @username AND user_password = @password";

            try
            {
                MySqlDataReader dataReader = mySqlCommand.ExecuteReader();
                if (dataReader.Read())
                {
                    userId = dataReader.GetInt32(0);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }
            return userId;
        }

    }
}
