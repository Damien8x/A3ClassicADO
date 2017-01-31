using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for RedirectClass
/// </summary>
public class RedirectClass
{
    private SqlConnection connect;
    public RedirectClass()
    {
        string connectString = ConfigurationManager.ConnectionStrings["Community_Assist_Connection"].ToString();
        connect = new SqlConnection(connectString);
    }
    public DataTable GetRequests( int GrantTypeKey)
    {

        DataTable table = new DataTable();
        string sql =

 "Select GrantReviewDate as [Date], "
 + "GrantRequestExplanation as Explanation, "
 + "GrantAllocationAmount as Amount, "
 + "GrantRequestStatus as [Status] "
 + "From GrantRequest r "
 + "inner join GrantReview gr "
 + "on r.GrantRequestKey = gr.GrantRequestKey "
 + " Where GrantTypeKey = @GrantTypeKey"; 

       
        
        SqlCommand cmd = new SqlCommand(sql, connect);
        cmd.Parameters.AddWithValue("@GrantTypeKey", GrantTypeKey);
        SqlDataReader reader = null;
        connect.Open();
        reader = cmd.ExecuteReader();
        table.Load(reader);
        reader.Close();
        connect.Close();
        return table;
    }

    public DataTable GetGrantTypes()
    {
        DataTable table = new DataTable();
        String sql =   "Select GrantTypeKey From GrantRequest Group By GrantTypeKey";



        SqlCommand cmd = new SqlCommand(sql, connect);
        SqlDataReader reader = null;
        connect.Open();
        reader = cmd.ExecuteReader();
        table.Load(reader);
        reader.Close();
        connect.Close();
        return table;
    }
}