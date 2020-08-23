using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace JSONWebAPI
{
    public class ServiceAPI : IServiceAPI
    {
        SqlConnection dbConnection;
        SqlCommand cmd;
        SqlDataReader dr;
        JSONMaker jm = new JSONMaker();
        public ServiceAPI()
        {
            dbConnection = DBConnect.getConnection();
        }

        public bool ulogin(string id, string pass)
        {
            bool f = false;
            try
            {
                if (dbConnection.State.ToString() == "Closed")
                {
                    dbConnection.Open();
                }
                SqlCommand cmd = new SqlCommand("Select Pass from Cust where UId = '" + id + "' AND Pass = '" + pass + "'", dbConnection);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    f = true;
                }
                dbConnection.Close();
            }
            catch (Exception ep)
            {
                f = false;
                dbConnection.Close();
            }

            return f;
        }
        public string getUId()
        {
            string uid = "1001";
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            string query = "SELECT top 1 UId FROM Cust order By UId Desc";
            SqlCommand command = new SqlCommand(query, dbConnection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                int id = Convert.ToInt32(reader[0].ToString());
                id += 1;
                uid = "" + id;
            }
            reader.Close();
            dbConnection.Close();
            return uid;
        }

        public string register(string Id, string name, string mobile, string email, string pass)
        {
            string ans = "True";
            try
            {
                SqlCommand cmd = new SqlCommand("Insert into Cust values ('" + Id + "','" + name + "', '" + mobile + "','" + email + "','0','" + pass + "')", dbConnection);
                if (dbConnection.State.ToString() == "Closed")
                {
                    dbConnection.Open();
                }
                cmd.ExecuteNonQuery();
                dbConnection.Close();
            }
            catch (Exception ep)
            {
                ans = "False";
            }
            return ans;
        }

        public string getName(string Id)
        {
            string name = "";
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            string query = "SELECT Name FROM Cust where UId = '" + Id + "'";
            SqlCommand command = new SqlCommand(query, dbConnection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                name = reader[0].ToString();
            }
            else
            {
                name = "False";
            }
            reader.Close();
            dbConnection.Close();
            return name;
        }



        public string feedback(string Id, string feed, string date, string time)
        {
            string ans = "false";
            try
            {
                SqlCommand cmd = new SqlCommand("Insert Into Feedback values ('" + Id + "','" + feed + "','" + date + "','" + time + "')", dbConnection);
                if (dbConnection.State.ToString() == "Closed")
                {
                    dbConnection.Open();
                }
                cmd.ExecuteNonQuery();
                dbConnection.Close();
                ans = "true";
            }
            catch (Exception ep)
            {
                ans = "false";
            }
            return ans;
        }



        public string getslot(string plots)
        {
            string slot = "false";
            SqlDataAdapter da = new SqlDataAdapter("Select Distinct Slot from Slots where Plot = '" + plots + "'", dbConnection);
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                slot = "";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    slot += ds.Tables[0].Rows[i][0].ToString() + ",";
                }
            }

            dbConnection.Close();
            return slot;
        }


        public string checkslot(string Plot, string slot, string date, string time, string etime)
        {
            string cslot = "false";
            string s = "SELECT ID FROM BSlots WHERE (Plot = '" + Plot + "') AND (Slot = '" + slot + "') AND (Date = '" + date + "') AND (Time >= " + time + ") AND (ETime <= " + etime + ") OR (Plot = '" + Plot + "') AND (Slot = '" + slot + "') AND (Date = '" + date + "') AND (Time < " + time + ") AND (ETime >= " + etime + ") OR (Plot = '" + Plot + "') AND (Slot = '" + slot + "') AND (Date = '" + date + "') AND (Time > " + time + ") AND (ETime >= " + etime + ") AND (Time < " + etime + ") OR (Plot = '" + Plot + "') AND (Slot = '" + slot + "') AND (Date = '" + date + "') AND (Time < " + time + ") AND (ETime <= " + etime + ") AND (ETime > " + time + ")";
            SqlDataAdapter da = new SqlDataAdapter(s, dbConnection);

            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                cslot = "";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    cslot += ds.Tables[0].Rows[i][0].ToString() + ",";
                }
            }

            return cslot;
        }


        public string viewbooking(string Id, string edate)
        {
            string bid = "false";
            SqlDataAdapter da = new SqlDataAdapter("Select TId from BSlots where UId = '" + Id + "' AND Date <= '" + edate + "'", dbConnection);
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                bid = "";
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    bid += ds.Tables[0].Rows[i][0].ToString() + ",";
                }
            }
            dbConnection.Close();
            return bid;
        }

        public string details(string Id, string date)
        {
            string detail = "false";
            SqlCommand cmd = new SqlCommand("Select Name,Mobile,Email,Bal from Cust where UId = '" + Id + "'", dbConnection);
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            detail = dr[0].ToString() + "," + dr[1].ToString() + "," + dr[2].ToString() + "," + dr[3].ToString() + ",";
            dbConnection.Close();

            string bid = "No Bookings";
            SqlDataAdapter da = new SqlDataAdapter("Select TId from BSlots where UId = '" + Id + "' AND Date >= '" + date + "'", dbConnection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                bid = "";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    bid += ds.Tables[0].Rows[i][0].ToString() + "  ";
                }
            }
            detail += bid;
            return detail;
        }


        public string cancelbook(string BId, string Id)
        {
            string det = "true";
            try
            {
                SqlCommand cmd = new SqlCommand("Delete from BSlots where UId = '" + Id + "' AND TId = '" + BId + "'", dbConnection);
                if (dbConnection.State.ToString() == "Closed")
                {
                    dbConnection.Open();
                }
                cmd.ExecuteNonQuery();
                dbConnection.Close();
            }
            catch (Exception ep)
            {
                det = "false";
            }
            return det;
        }

        public string getnear()
        {
            string plot = "false";
            SqlDataAdapter da = new SqlDataAdapter("Select Distinct Lat,Long from Slots", dbConnection);
            if (dbConnection.State.ToString() == "Closed")
            {
                dbConnection.Open();
            }
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                plot = "";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    plot += ds.Tables[0].Rows[i][0].ToString() + "$";
                    plot += ds.Tables[0].Rows[i][1].ToString() + "$";
                    plot += ds.Tables[0].Rows[i][2].ToString() + "*";
                }
            }
            dbConnection.Close();
            return plot;
        }


    }
}
