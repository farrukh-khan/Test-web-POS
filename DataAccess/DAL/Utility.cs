using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess.DAL;

public class Utility
{
    private static Connection connection = null;
    public static DataSet SelectRow(DataSet ds, string query)
    {
        try
        {
            if (connection == null) connection = new Connection();
            SqlConnection conn = connection.GetConnection();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, conn);
            //adapter.SelectCommand.CommandTimeout = 600;
            adapter.Fill(ds);
            return ds;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    public static DataSet ExcuteProcedure(string pname, SqlParameter[] para)
    {
        try
        {
            if (connection == null)
            {
                connection = new Connection();
            }

            SqlConnection conn = connection.GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = pname;
            cmd.Parameters.AddRange(para);
            cmd.Connection = conn;
            //cmd.CommandTimeout = 600;
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet table = new DataSet();
            adapter.Fill(table);
            conn.Close();
            return table;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public static DataTable ExcuteProc(string pname, SqlParameter[] para)
    {
        try
        {
            if (connection == null) 
            {
                connection = new Connection();
            }

            SqlConnection conn = connection.GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = pname;
            cmd.Parameters.AddRange(para);
            cmd.Connection = conn;
            //cmd.CommandTimeout = 600;
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            conn.Close();
            return table;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public static DataTable Insert(string query)
    {
        try
        {
            if (connection == null) connection = new Connection();
            SqlConnection conn = connection.GetConnection();
            SqlCommand cmd = new SqlCommand(query);
           // cmd.CommandText = CommandType.StoredProcedure;
            //cmd.CommandText = pname;
            //cmd.Parameters.AddRange(para);
            cmd.Connection = conn;
            //cmd.CommandTimeout = 600;
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    public static DataTable Get(string query)
    {
        try
        {
            if (connection == null) connection = new Connection();
            SqlConnection con = connection.GetConnection();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, con);
            adapter.SelectCommand.CommandTimeout = 600;
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        catch (Exception e)
        {
            throw;
        }
    }
}
