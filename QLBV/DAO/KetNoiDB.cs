using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBV.DAO
{
    public class KetNoiDB
    {
        private static KetNoiDB khoa;

        public static KetNoiDB Khoa
        {
            get
            {
                if (khoa == null) khoa = new KetNoiDB { };
                return khoa;
            }

            private set
            {
                khoa = value;
            }
        }

        private KetNoiDB() { }

        private string constr = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLBV;Integrated Security=True";

        #region sử dụng PROC
        /*
        public DataTable LayBangPROC(string sql, object[] prm = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(constr))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                if (prm != null)
                {
                    string[] listprm = sql.Split(' ');
                    int i = 0;
                    foreach (string item in listprm)
                    {
                        if (item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item, prm[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                adt.Fill(data);
                connection.Close();

            }
            return data;
        }

        public int ChayLenhPROC(string sql, object[] prm = null)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(constr))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                if (prm != null)
                {
                    string[] listprm = sql.Split(' ');
                    int i = 0;
                    foreach (string item in listprm)
                    {
                        if (item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item, prm[i]);
                            i++;
                        }
                    }
                }
                data = cmd.ExecuteNonQuery();
                connection.Close();
            }
            return data;
        }

        public object LayGiaTriPROC(string sql, object[] prm = null)
        {
            object data = 0;
            using (SqlConnection connection = new SqlConnection(constr))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                if (prm != null)
                {
                    string[] listprm = sql.Split(' ');
                    int i = 0;
                    foreach (string item in listprm)
                    {
                        if (item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item, prm[i]);
                            i++;
                        }
                    }
                }
                data = cmd.ExecuteScalar();
                connection.Close();
            }
            return data;
        }
        */
        #endregion

        #region lệnh thường

        public DataTable LayBang(string sql)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(constr))
            {
                connection.Open();
                SqlDataAdapter adt = new SqlDataAdapter(sql, connection);
                adt.Fill(data);
                connection.Close();
            }
            return data;
        }

        public void ChayLenh(string sql)
        {
            using (SqlConnection connection = new SqlConnection(constr))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public string LayGiaTrị(string sql)
        {
            string data = null;
            using (SqlConnection connection = new SqlConnection(constr))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                    data = da[0].ToString();
                connection.Close();
            }
            return data;
        }

#endregion

    }
}
