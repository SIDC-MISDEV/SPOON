using Spoon.SIDC;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sap.Data.Hana;

namespace Spoon.DBConfig
{
    public class HanaSQL : IDisposable
    {
        private System.ComponentModel.Component components = new System.ComponentModel.Component();
        private bool disposed = false;

        private string dbHost = string.Empty;
        private string dbName = string.Empty;
        private string dbUser = string.Empty;
        private string dbPass = string.Empty;
        private string query = string.Empty;
        private string connString = string.Empty;

        public HanaSQL()
        {
            ACryptoServiceProvider crypt = new ACryptoServiceProvider();
            string password = crypt.Decrypt(ConfigurationManager.ConnectionStrings["SAPPassword"].ToString(), "*sup3r5dm1n*");

            dbHost = ConfigurationManager.ConnectionStrings["SAPDBHost"].ToString();
            dbUser = ConfigurationManager.ConnectionStrings["SAPDBUser"].ToString();
            dbName = ConfigurationManager.ConnectionStrings["SAPDBName"].ToString();
            dbPass = password;

            connString = $"Server={dbHost};Username={dbUser};Password={dbPass};";
        }

        public DataTable GetData(object reference)
        {
            try
            {
                DataTable dt = new DataTable();

                using (var conn = new HanaConnection(connString))
                {
                    conn.Open();

                    using (HanaDataAdapter da = new HanaDataAdapter(query, conn))
                    {
                        da.SelectCommand.Parameters.AddWithValue("?reference", reference);
                        da.Fill(dt);
                    }
                }

                return dt;
            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// <para>1 - Master Data </para>
        /// <para>2 - Transaction </para>
        /// <para>3 - Pcosting </para>
        /// </summary>
        public void SetQuery(int queryNum)
        {
            switch (queryNum)
            {
                case 1:
                    query = $@"SELECT 
                            ""ItemCode"" as master_idstock
                             ,""ItemName"" as master_name
                             , 'forselect' as master_type, 
                             '' as master_supplier, 
                             '' as master_status, 
                             '' as master_remarks, 
                             '' as master_barcode, 
                             case when ""Canceled"" = 'Y' then 1 else 0 end as master_discontinued, 
                             '1' as master_consignment, 
                             '1' as master_taxable, 
                             '1' as master_discountable, 
                             'forselect' as master_CatID, 
                             '' as master_packaging, 
                             '' as master_subcategory, 
                             '' as master_subcatid, 
                             '' as master_brand, 
                             '' as master_brandid
                            FROM {dbName}.OITM WHERE ""ItemCode"" IN (SELECT i.""ItemCode"" FROM {dbName}.IGE1 as i
                                INNER JOIN {dbName}.OIGE as t ON i.""DocEntry"" = t.""DocEntry""
                                WHERE t.""DocNum"" = ?);";
                    break;

                case 2:
                    query = $@"SELECT
                                h.""DocDate"" as date_posted,h.""CardName"" as branch_name,
                                h.""U_FromWhse"" as warehouse_code, ""U_MSegment"" as segment, ""U_BSegment"" as business_segment, 
                                h.""U_Branch"" as branch_code,
                                left(h.""U_Reference"", 2) as transaction_type,
                                h.""U_Reference"" as reference,
                                h.""CreateDate"" as posted_date,
                                h.""CardCode"" as idfile,
                                '' as iduser,
                                '' as crossreference,
                                d.""ItemCode"" as item_code,
                                d.""Dscription"" as item_description,
                                d.""Quantity"" as item_qty,
                                d.""UomCode"" as item_unit, 
                                d.""NumPerMsr"" * d.""StockPrice"" as item_cost, d.""NumPerMsr"" * d.""StockPrice"" as item_price,
                                (d.""NumPerMsr"" * d.""StockPrice"") * d.""Quantity"" as item_total, 
                                d.""NumPerMsr"" as item_conversion
                                FROM {dbName}.OIGE h inner join {dbName}.IGE1 d on h.""DocEntry"" = d.""DocEntry""
                                where h.""DocNum"" = ?;";
                    break;

                case 3:
                    query = $@"SELECT 
                                h.""UgpCode"" as p_idstock,
                                b.""BcdCode"" as p_barcode,
                                u.""UomCode"" as p_unit,
                                0 as p_cost,
                                0 as p_selling, 
                                0 as p_markup,
                                d.""BaseQty"" as p_conversion,
                                'CURRENT' as p_costingMethod 
                                FROM {dbName}.OUGP h inner join {dbName}.UGP1 d on h.""UgpEntry"" = d.""UgpEntry""
                                inner join {dbName}.OUOM u  on u.""UomEntry"" = d.""UomEntry""
                                left join {dbName}.OBCD b on b.""ItemCode"" = h.""UgpCode"" and b.""UomEntry"" = d.""UomEntry""
                                 where u.""UomCode"" not in ('WHOLESALE','wholesale','Wholesale') and h.""UgpCode"" in (SELECT i.""ItemCode"" FROM {dbName}.IGE1 as i
                                INNER JOIN {dbName}.OIGE as t ON i.""DocEntry"" = t.""DocEntry""
                                WHERE t.""DocNum"" = ?);";
                    break;

                default:
                    break;
            }
        }



        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !disposed)
            {
                components.Dispose();
            }

            disposed = true;
        }

        ~HanaSQL()
        {
            Dispose(false);
        }
    }
}
