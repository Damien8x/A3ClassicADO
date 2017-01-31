using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;  //general data tools
using System.Data.SqlClient;    //sqlserver data tools
using System.Configuration;     //tools to talk to config file
/// <summary>
///Summary description to login to class
/// </summary>
/// //Login = email
//Password = LastName+Pass
public class LoginClass
{
    SqlConnection connect;
    string user;
    string pass;

    public LoginClass(string userName, string password)
    {
        user = userName;
        pass = password;
        string connectString = ConfigurationManager.ConnectionStrings["Community_Assist_Connection"].ToString();
        connect = new SqlConnection(connectString);

    }

    public int ValidateLogin()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connect;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "usp_Login";
        cmd.Parameters.AddWithValue("@Email", user);
        cmd.Parameters.AddWithValue("@password", pass);
        connect.Open();
        int result = cmd.ExecuteNonQuery();
        connect.Close();
        int key = 0;
        if(result != -1)
        {
            key = GetUserKey();
        }


        return key;
    }

    private int GetUserKey()
    {
        string sql = "select personkey from person where personemail = @user";
        SqlCommand cmd = new SqlCommand(sql, connect);
        cmd.Parameters.AddWithValue("@user", user);
        SqlDataReader reader = null;
        connect.Open();
        reader = cmd.ExecuteReader();
        int key = 0;
        while (reader.Read())
        {
            key = (int)reader["personkey"];
        }
        connect.Close();
        return key;
    }
}