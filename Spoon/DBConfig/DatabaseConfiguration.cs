using MySql.Data.MySqlClient;
using Spoon.SIDC;
using System;
using System.Configuration;
using System.Data;

namespace Spoon.DBConfig
{
    class DatabaseConfiguration : IDisposable
    {
        public string ConnectionString { get; set; }
        public string ConnectionStringWithNoDB { get; set; }
        public MySqlCommand cmd { get; set; }
        public MySqlConnection conn { get; set; }
        string password = "";

        public MySqlTransaction trans { get; set; }

        public MySqlConnection Myconn()
        {
            MySqlConnection My;
            My = new MySqlConnection();
            return My;
        }

        public DatabaseConfiguration()
        {
            // ConnectionString = "server=localhost;user=root;password=supervisor;database=sap_dashboard;Allow User Variables=True";

            ACryptoServiceProvider crypt = new ACryptoServiceProvider();
            password = crypt.Decrypt(ConfigurationManager.ConnectionStrings["DBPassword"].ToString(), "*sup3r5dm1n*"); ;

            string DBHost = "server =" + ConfigurationManager.ConnectionStrings["DBHost"].ToString() + ";";
            string DBUser = "user =" + ConfigurationManager.ConnectionStrings["DBUser"].ToString() + ";";
            string DBName = "database =" + ConfigurationManager.ConnectionStrings["DBName"].ToString() + ";";
            string DBPass = "password = " + password + ";";

            ConnectionString = DBHost + DBUser + DBName + DBPass + "Allow User Variables=True;";
            ConnectionStringWithNoDB = DBHost + DBUser + DBPass + "Allow User Variables=True;";

        }

        public void Set_Connection(string connectionString)
        {
            conn = new MySqlConnection(connectionString);
        }

        public bool Open_Connection(MySqlConnection conn)
        {
            try
            {
                conn.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Close_Connection(MySqlConnection conn)
        {
            conn.Close();
        }

        public void Execute_Sql(string sqlCmd, MySqlConnection conn)
        {
            try
            {
                Open_Connection(conn);
                MySqlCommand cmd = new MySqlCommand(sqlCmd, conn);
            }
            catch (Exception)
            {
                //return false;
            }
        }


        public void Begin_transaction(MySqlConnection conn, IsolationLevel isoLevel)
        {
            if (conn.State != ConnectionState.Open)
            {
                Close_Connection(conn);
                Open_Connection(conn);

            }
            //Open_Connection(conn);
            trans = conn.BeginTransaction(isoLevel);
        }

        public void Begin_transaction(MySqlConnection conn)
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

        public void OpenConnection(MySqlConnection conn)
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

        public void Rollback_transaction(MySqlConnection conn)
        {
            trans.Rollback();
        }

        public void Execute_Tsql(string sqlCmd, MySqlConnection conn)
        {
            try
            {
                cmd = new MySqlCommand(sqlCmd, conn, trans);
            }
            catch (Exception)
            {

                trans.Rollback();
            }
        }




        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~DatabaseConfiguration() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion


    }
}
