using Spoon.DBConfig;

namespace Spoon.Admin.Service
{
    class AExtract
    {
        DatabaseConfiguration dbconfig = null;
        public void Extract(string warehouse, string reference, string path)
        {

            try
            {
                dbconfig = new DatabaseConfiguration();
                dbconfig.Set_Connection(dbconfig.ConnectionString);
                dbconfig.Begin_transaction(dbconfig.conn);

                dbconfig.Execute_Tsql("CALL LA_PullTransaction(@reference,@warehouse,@path);", dbconfig.conn);
                dbconfig.cmd.Parameters.AddWithValue("@reference", reference);
                dbconfig.cmd.Parameters.AddWithValue("@warehouse", warehouse);
                dbconfig.cmd.Parameters.AddWithValue("@path", path);
                dbconfig.cmd.ExecuteNonQuery();
                dbconfig.Close_Connection(dbconfig.conn);

            }
            catch
            {

            }
        }
    }

}
