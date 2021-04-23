using Spoon.SIDC;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Spoon.DBConfig
{

    public class DBSql
    {


        public string ConnectionString { get; set; }
        public string ConnectionStringWithNoDB { get; set; }
        public SqlCommand cmd { get; set; }
        public SqlConnection conn { get; set; }
        string password = "";

        public SqlTransaction trans { get; set; }
        public SqlConnection Myconn()
        {
            SqlConnection My;
            My = new SqlConnection();
            return My;
        }


        string DBHost = "";
        string DBUser = "";
        string DBName = "";
        string DBPass = "";


        public DBSql()
        {
            ACryptoServiceProvider crypt = new ACryptoServiceProvider();
            string password = crypt.Decrypt(ConfigurationManager.ConnectionStrings["SAPPassword"].ToString(), "*sup3r5dm1n*"); ;

            DBHost = ConfigurationManager.ConnectionStrings["SAPDBHost"].ToString();
            DBUser = ConfigurationManager.ConnectionStrings["SAPDBUser"].ToString();
            DBName = ConfigurationManager.ConnectionStrings["SAPDBName"].ToString();
            DBPass = password;
            //DBPass = "p@ssw0rd";

            ConnectionString = "Data Source=" + DBHost + ";Initial Catalog=" + DBName + ";User ID=" + DBUser + ";Password=" + DBPass + ";";


        }

        public void Set_Connection(string connectionString)
        {
            conn = new SqlConnection(connectionString);
        }

        public bool Open_Connection(SqlConnection conn)
        {
            try
            {
                conn.Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void Close_Connection(SqlConnection conn)
        {
            conn.Close();
        }

        public void Execute_Sql(string sqlCmd, SqlConnection conn)
        {
            try
            {
                Open_Connection(conn);
                SqlCommand cmd = new SqlCommand(sqlCmd, conn);
            }
            catch (Exception)
            {
                //return false;
            }
        }

        public void Begin_transaction(SqlConnection conn, System.Data.IsolationLevel isoLevel)
        {
            if (conn.State != ConnectionState.Open)
            {
                Close_Connection(conn);
                Open_Connection(conn);

            }
            trans = conn.BeginTransaction(isoLevel);
        }

        public void Begin_transaction(SqlConnection conn)
        {
            try
            {
                Open_Connection(conn);
                trans = conn.BeginTransaction();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }

        public void OpenConnection(SqlConnection conn)
        {
            try
            {
                Open_Connection(conn);
                trans = conn.BeginTransaction();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }

        public void Rollback_transaction(SqlConnection conn)
        {
            trans.Rollback();
        }

        public void Execute_Tsql(string sqlCmd, SqlConnection conn)
        {
            try
            {
                cmd = new SqlCommand(sqlCmd, conn, trans);
            }
            catch (Exception)
            {

                trans.Rollback();
            }
        }


        //public void GetBranch()
        //{
        //    try
        //    {
        //        //Document transaction = new Document();
        //        SqlConnection conn = new SqlConnection();
        //        //conn.ConnectionString = "Data Source=192.168.12.100;Initial Catalog=SIDCDB;User ID=sa;Password=p@ssw0rd";
        //        conn.ConnectionString = "Data Source=" + DBHost + ";Initial Catalog=" + DBName + ";User ID=" + DBUser + ";Password=" + DBPass + ";";
        //        conn.Open();

        //        SqlCommand command = new SqlCommand(" SELECT TOP 1 "
        //                + " [Code] "
        //                + " ,[Name] "
        //                + " ,[U_MSegment] "
        //                + " ,[U_BSegment], "
        //                + " (SELECT TOP 1 Name FROM [@BSEGMENT] where code = b.U_BSegment) as bname, "
        //                + " (SELECT TOP 1 Name FROM [@MSEGMENT] where code = b.U_MSegment) as mname "
        //                + " FROM [@BRANCH] b where b.code = @0 ; ", conn);
        //        command.Parameters.Add(new SqlParameter("0", "test"));
        //        SqlDataReader dreader = command.ExecuteReader();
        //        DataTable dataTable = new DataTable();
        //        dataTable.Load(dreader);


        //        foreach (DataRow dr in dataTable.Rows)
        //        {
        //            partner.BranchCode = dr["Code"].ToString();
        //            partner.BranchDescription = dr["Name"].ToString();
        //            partner.BSegmentDescription = dr["bname"].ToString();
        //            partner.BranchSegment = dr["U_BSegment"].ToString();
        //            partner.MainSegment = dr["U_MSegment"].ToString();
        //            partner.MainDescription = dr["mname"].ToString();

        //        }
        //        conn.Close();
        //        conn.Dispose();

        //        // transaction.DataLines = new DataTable();

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error:" + ex.Message);
        //    }
        //    return partner;
        //}


    }

}
