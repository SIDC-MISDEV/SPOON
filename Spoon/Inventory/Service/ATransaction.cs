using MySql.Data.MySqlClient;
using Spoon.DBConfig;
using Spoon.Inventory.Model;
using Spoon.SIDC;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Spoon.Inventory.Service
{
    public class ATransaction
    {
        DatabaseConfiguration dbconfig = null;
        DBSql dbSQL = null;
        HanaSQL hanaSQL = null;
        DataTable dt = null;
        string idUser = "SPOON";
        string response = string.Empty;
        string logs = "";
        #region SET

        public string Save(Document document, List<Items> itemList = null, List<Units> unitList = null)
        {
            logs = "";
            string msg = "";
            string errorDescription = null;

            dbconfig = new DatabaseConfiguration();
            dbconfig.Set_Connection(dbconfig.ConnectionString);
            dbconfig.Begin_transaction(dbconfig.conn);

            try
            {
                logs += "Started at " + DateTime.Now;

                if (itemList.Count > 0)
                    this.SaveItem(itemList, dbconfig.conn);

                if (unitList.Count > 0)
                    this.SaveUnit(unitList, dbconfig.conn);

                ////get last series and set/format reference
                int series;
                string prefix;
                bool withEffectInInventory = true;
                if (document.CrossReference.Substring(0, 2) == "ST"
                    ||
                    document.CrossReference.Substring(0, 2) == "SF")
                {
                    document.Reference = this.GetReference(dbconfig.conn, "RR");
                    prefix = "RR";
                }
                else if (document.CrossReference.Substring(0, 2) == "PO")
                {
                    document.Reference = this.GetReference(dbconfig.conn, "PO");
                    prefix = "PO";
                    withEffectInInventory = false;
                }
                else
                    throw new Exception("Error encountered, invalid crossreference.");




                if (string.IsNullOrWhiteSpace(document.Reference))
                    throw new Exception("Error in generating reference, transaction failed to save.");

                if (document.Total <= 0)
                    throw new Exception("Error encountered in validating inputs.");

                if (!this.CheckReferenceIfExist(dbconfig.conn, document.Reference))
                    throw new Exception(string.Format("Reference : {0} generated by the system already used in other transaction, please try to re-save this transaction.", document.Reference));

                if (!this.CheckCrossReferenceIfExist(dbconfig.conn, document.Code, document.CrossReference))
                    throw new Exception(string.Format("{0}-{1} is already exist in the system.", document.Code, document.CrossReference));

                ////INSERT HEADER
                SaveHeader(document, dbconfig.conn);

                //// INSERT DETAIL
                SaveDetail(document, dbconfig.conn, withEffectInInventory);

                ////UPDATE SERIES
                if (document.DocNum == 0)
                    this.UpdateSeries(dbconfig.conn, document.Reference, prefix);



                dbconfig.trans.Commit();
                //string logsTobeWrite = logs;
                Logs.WriteLog("", logs);

                return null;


            }
            catch (Exception ex)
            {


                msg = ex.Message;
                dbconfig.Rollback_transaction(dbconfig.conn);

                return msg;
            }
            finally
            {
                dbconfig.Close_Connection(dbconfig.conn);
            }


        }

        private void SaveItem(List<Items> itemList, MySqlConnection conn)
        {
            int affectedRows = 0;
            logs += "\n\n\nAdded Item Master Data";
            #region Items
            foreach (var detail in itemList)
            {

                //dbconfig.Execute_Tsql("INSERT INTO stocks (idstock, name, type) "
                //                + " SELECT * FROM ( "
                //                + " @itemCode as idstock, @name as name, @type as type) AS tmp "
                //                + " WHERE NOT EXISTS( "
                //                + "     SELECT idStock FROM stocks WHERE idStock = @itemCode "
                //                + " ); ", dbconfig.conn);


                dbconfig.Execute_Tsql("INSERT INTO stocks (idstock, name, type, supplier, status, remarks, barcode, discontinued, consignment, taxable, discountable, CatID, "
                               + " packaging, subcategory, subcatid, brand, brandid) "
                               + " SELECT * FROM ( SELECT "
                               + " @itemCode as idstock, @name as name, "
                               + " ifnull((select distinct type from stocks where left(idstock,3)=left(@itemCode,3) and CatID > 0 limit 1),0) as type, @supplier as supplier, @status as status, @remarks as remarks, @barcode as barcode, "
                               + " @discontinued as discontinued, @consignment as consignment, @taxable as taxable, @discountable as discountable, "
                               + " ifnull((select distinct CatID from stocks where left(idstock,3)=left(@itemCode,3) and CatID > 0 limit 1),0) as CatID, "
                               + " @packaging as packaging, @subcategory as subcategory, @subcatid as subcatid, @brand as brand, @brandid as brandid "
                               + " ) AS tmp "
                               + " WHERE NOT EXISTS( "
                               + "     SELECT idStock FROM stocks WHERE idStock = @itemCode "
                               + " ); ", dbconfig.conn);


                dbconfig.cmd.Parameters.AddWithValue("@itemCode", detail.ItemCode);
                dbconfig.cmd.Parameters.AddWithValue("@name", detail.Description);
                dbconfig.cmd.Parameters.AddWithValue("@type", detail.s1Category);
                dbconfig.cmd.Parameters.AddWithValue("@supplier", detail.s1Supplier);
                dbconfig.cmd.Parameters.AddWithValue("@status", detail.Status);
                dbconfig.cmd.Parameters.AddWithValue("@remarks", detail.Remarks);
                dbconfig.cmd.Parameters.AddWithValue("@barcode", detail.Barcode);
                dbconfig.cmd.Parameters.AddWithValue("@discontinued", detail.Discountinued ? 1 : 0);
                dbconfig.cmd.Parameters.AddWithValue("@consignment", detail.Consignment ? 1 : 0);
                dbconfig.cmd.Parameters.AddWithValue("@taxable", detail.Taxable ? 1 : 0);
                dbconfig.cmd.Parameters.AddWithValue("@discountable", detail.Discountinued ? 1 : 0);
                dbconfig.cmd.Parameters.AddWithValue("@CatID", detail.s1CategoryID);

                dbconfig.cmd.Parameters.AddWithValue("@packaging", detail.Packaging);
                dbconfig.cmd.Parameters.AddWithValue("@subcategory", detail.s1SubCategory);
                dbconfig.cmd.Parameters.AddWithValue("@subcatid", DataConversion.ToInt(detail.s1SubCategoryID));
                dbconfig.cmd.Parameters.AddWithValue("@brand", detail.s1Brand);
                dbconfig.cmd.Parameters.AddWithValue("@brandid", DataConversion.ToInt(detail.s1BrandID));

                int rowsAffected = dbconfig.cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    //logs += string.Format("\nitemCode:{0}|name:{1}|type:{2}|supplier:{3}|status:{4}|remarks:{5}|barcode:{6}|"
                    //    + "discontinued:{7}|consignment:{8}|taxable:{9}|discountable:{10}|CatID:{11}"
                    //    + "CatID:{12}|packaging:{13}|subcategory:{14}|subcatid:{15}|brand:{16}|brandid:{17}|Nothingfollows****"
                    //    , detail.ItemCode, detail.Description, detail.s1Category, detail.s1Supplier, detail.Status, detail.Remarks, detail.Barcode
                    //    , detail.Discountinued ? 1 : 0,
                    //    detail.Consignment ? 1 : 0,
                    //    detail.Taxable ? 1 : 0,
                    //    detail.Discountinued ? 1 : 0,
                    //    detail.s1CategoryID, detail.Packaging, detail.s1SubCategory, detail.s1SubCategory, DataConversion.ToInt(detail.s1SubCategoryID),
                    //    detail.s1Brand, DataConversion.ToInt(detail.s1BrandID)
                    //    );

                    logs += string.Format("\n{0},{1},{2},{3},{4},{5},{6},"
                        + "{7},{8},{9},{10},{11}"
                        + ",{12},{13},{14},{15},{16},{17},"
                        , detail.ItemCode, detail.Description, detail.s1Category, detail.s1Supplier, detail.Status, detail.Remarks, detail.Barcode
                        , detail.Discountinued ? 1 : 0,
                        detail.Consignment ? 1 : 0,
                        detail.Taxable ? 1 : 0,
                        detail.Discountinued ? 1 : 0,
                        detail.s1CategoryID, detail.Packaging, detail.s1SubCategory, detail.s1SubCategory, DataConversion.ToInt(detail.s1SubCategoryID),
                        detail.s1Brand, DataConversion.ToInt(detail.s1BrandID)
                        );
                    affectedRows++;
                }

            }
            //logs += "\nTotal Item : " + itemList.Count;
            //logs += "\nTotal Existing item : " + (itemList.Count - affectedRows);
            //logs += "\nTotal Item loaded : " + affectedRows;
            logs += "\nEnd of Item Master Data.";
            #endregion
        }

        private void SaveUnit(List<Units> unitList, MySqlConnection conn)
        {
            int affectedRows = 0;
            int affectedRowsForBin = 0;
            logs += "\n\n\n Added Unit for PCOSTING";

            string binLogs = "\n\n\n Added item for BIN";
            #region Units
            foreach (var detail in unitList)
            {

                dbconfig.Execute_Tsql("INSERT INTO pcosting (idStock, barcode, unit, cost, selling, markup,quantity, bin, costingMethod) "
                            + " SELECT * FROM(SELECT @itemCode as idStock, @barcode as barcode, @unit as unit, 0 as cost, 0 as selling, 0 as markup, "
                            + " @conversion as quantity, (SELECT idbranch FROM business_segments limit 1) as bin, @costingmethod as costingMethod) AS tmp "
                            + " WHERE NOT EXISTS( "
                            + "     SELECT idStock FROM pcosting WHERE idStock = @itemCode and unit = @unit "
                            + " ); ", dbconfig.conn);


                dbconfig.cmd.Parameters.AddWithValue("@itemCode", detail.ItemCode);
                dbconfig.cmd.Parameters.AddWithValue("@barcode", detail.Barcode);
                dbconfig.cmd.Parameters.AddWithValue("@unit", detail.Unit);
                dbconfig.cmd.Parameters.AddWithValue("@conversion", detail.Conversion);
                dbconfig.cmd.Parameters.AddWithValue("@costingmethod", detail.CostingMethod);
                int rowsAffected = dbconfig.cmd.ExecuteNonQuery();

                #region PCOSTING LOGS

                if (rowsAffected > 0)
                {
                    //logs += string.Format("\nitemCode:{0}|barcode:{1}|unit:{2}|conversion:{3}|costingmethod:{4}|Nothingfollows****"
                    //    , detail.ItemCode, detail.Barcode, detail.Unit, detail.Conversion, detail.CostingMethod);
                    logs += string.Format("\n{0},{1},{2},{3},{4}"
                        , detail.ItemCode, detail.Barcode, detail.Unit, detail.Conversion, detail.CostingMethod);


                    affectedRows++;
                }

                #endregion

                if (detail.Conversion == 1
                    &&
                    (
                    detail.Unit.ToLower() == "pcs" ||
                    detail.Unit.ToLower() == "bag" ||
                    detail.Unit.ToLower() == "kilo" ||
                    detail.Unit.ToLower() == "bags" ||
                    detail.Unit.ToLower() == "sv" ||
                    detail.Unit.ToLower() == "mtr" ||
                    detail.Unit.ToLower() == "ft" ||
                    detail.Unit.ToLower() == "set" ||
                    detail.Unit.ToLower() == "gal" ||
                    detail.Unit.ToLower() == "liter" ||
                    detail.Unit.ToLower() == "kl" ||
                    detail.Unit.ToLower() == "box" ||
                    detail.Unit.ToLower() == "bundle" ||
                    detail.Unit.ToLower() == "dozen" ||
                    detail.Unit.ToLower() == "inner box" ||
                    detail.Unit.ToLower() == "pack" ||
                    detail.Unit.ToLower() == "case" ||
                    detail.Unit.ToLower() == "doz" ||
                    detail.Unit.ToLower() == "meter"
                    )
                    )
                {
                    dbconfig.Execute_Tsql("INSERT INTO inventorybin (bin, barcode,idstock, unit, beginningQTY, beginningDATE, runningQTY) "
                            + " SELECT * FROM(SELECT "
                            + " (SELECT idbranch FROM business_segments limit 1) as bin,@barcode as barcode, @itemCode as idstock, @unit as unit, "
                            + " 0 as beginningQTY, date(DATE_FORMAT(now() - INTERVAL 1 MONTH, '%Y-%m-%d')) beginningDATE, 0 as runningQTY "
                            + " ) AS tmp "
                            + " WHERE NOT EXISTS( "
                            + "     SELECT idStock FROM inventorybin WHERE idStock = @itemCode and bin = (SELECT idbranch FROM business_segments limit 1) "
                            + " ); ", dbconfig.conn);


                    dbconfig.cmd.Parameters.AddWithValue("@itemCode", detail.ItemCode);
                    dbconfig.cmd.Parameters.AddWithValue("@barcode", detail.Barcode);
                    dbconfig.cmd.Parameters.AddWithValue("@unit", detail.Unit);
                    int rowsAffectedForBin = dbconfig.cmd.ExecuteNonQuery();


                    #region BIN LOGS


                    if (rowsAffectedForBin > 0)
                    {
                        //binLogs += string.Format("\nitemCode:{0}|barcode:{1}|unit:{2}|Nothingfollows****"
                        //    , detail.ItemCode, detail.Barcode, detail.Unit);


                        binLogs += string.Format("\n{0},{1},{2}"
                            , detail.ItemCode, detail.Barcode, detail.Unit);
                        affectedRowsForBin++;
                    }


                    #endregion


                }

            }

            //logs += "\nTotal Item Unit : " + unitList.Count;
            //logs += "\nTotal Existing item unit : " + (unitList.Count - affectedRows);
            //logs += "\nTotal Item Unit loaded : " + affectedRows;
            logs += "\nEnd of Item Unit.";


            logs += binLogs;

            //logs += "\nTotal BIN Item : " + unitList.Count;
            //logs += "\nTotal Existing BIN Item : " + (unitList.Count - affectedRowsForBin);
            //logs += "\nTotal BIN Item loaded : " + affectedRowsForBin;
            logs += "\nEnd of BIN Item .";

            #endregion
        }

        private void SaveHeader(Document document, MySqlConnection conn)
        {

            dbconfig.Execute_Tsql("INSERT INTO ledger "
                + " (date,reference,idfile,debit,idBranch,idUser,crossReference) VALUES "
                + " (@date,@reference,@idfile,@debit,(SELECT idbranch FROM business_segments limit 1),@idUser,@crossReference)", dbconfig.conn);
            dbconfig.cmd.Parameters.AddWithValue("@date", document.PostedDate);
            dbconfig.cmd.Parameters.AddWithValue("@reference", document.Reference);
            dbconfig.cmd.Parameters.AddWithValue("@idfile", document.Code);
            dbconfig.cmd.Parameters.AddWithValue("@debit", document.Total);
            dbconfig.cmd.Parameters.AddWithValue("@idUser", idUser);
            dbconfig.cmd.Parameters.AddWithValue("@crossReference", document.CrossReference);
            dbconfig.cmd.ExecuteNonQuery();
        }


        private string SaveDetail(Document transaction, MySqlConnection conn, bool withEffectInInventory)
        {
            string errorMessage = null;
            string operand = "-";
            logs += "\n\n Updated Cost if old cost is zero.";

            //if (!string.IsNullOrWhiteSpace(transaction.CrossReference))
            //    operand = "+";

            #region DETAILS AND RUNNING QTY
            foreach (var detail in transaction.DocumentLine)
            {



                dbconfig.Execute_Tsql("INSERT INTO invoice (date, reference, idstock, idfile, quantity, cost, selling, amount, unit, idBranch, unitQuantity, idUser, variance) VALUES  "
                    + " (@date, @reference, @idstock, @idfile, @quantity, @cost, @selling, @amount, @unit, (SELECT idbranch FROM business_segments limit 1), @unitQuantity, @idUser, @variance)", dbconfig.conn);
                dbconfig.cmd.Parameters.AddWithValue("@date", transaction.PostedDate);
                dbconfig.cmd.Parameters.AddWithValue("@reference", transaction.Reference);
                dbconfig.cmd.Parameters.AddWithValue("@idstock", detail.Item.ItemCode);
                dbconfig.cmd.Parameters.AddWithValue("@idfile", transaction.Code);
                dbconfig.cmd.Parameters.AddWithValue("@quantity", detail.Item.Quantity);
                dbconfig.cmd.Parameters.AddWithValue("@cost", detail.Item.Price);
                //dbconfig.cmd.Parameters.AddWithValue("@cost", Math.Truncate(100 * detail.Item.Price) / 100);
                dbconfig.cmd.Parameters.AddWithValue("@selling", 0);
                dbconfig.cmd.Parameters.AddWithValue("@amount", detail.Total);
                dbconfig.cmd.Parameters.AddWithValue("@unit", detail.Item.Unit);
                dbconfig.cmd.Parameters.AddWithValue("@unitQuantity", detail.Item.Conversion);
                dbconfig.cmd.Parameters.AddWithValue("@idUser", idUser);
                dbconfig.cmd.Parameters.AddWithValue("@variance", detail.Item.Quantity * -1);
                dbconfig.cmd.ExecuteNonQuery();

                #region Update Cost

                //check appconfig, if true, this will update all uom for that itemcode
                bool bupdateCost = false;
                string sUpdateScript = "";
                try
                {
                    bool.TryParse(ConfigurationManager.AppSettings["UpdateCostInRecording"].ToString(), out bupdateCost);
                }
                catch
                {
                    bupdateCost = false;
                }

                if (bupdateCost)
                {
                    sUpdateScript = "UPDATE pcosting  set cost = "
                    + " (@cost / (select x.* from(select quantity from pcosting where idStock = @itemCode and unit = @unit) as x)) "
                    + " * quantity "
                    + " WHERE idStock = @itemCode and cost=0 ; ";
                }
                else
                {
                    sUpdateScript = "UPDATE pcosting  "
                    + " set cost = if(cost=0,@cost,cost) "
                    + " WHERE idStock = @itemCode and unit =@unit and cost=0 limit 1; ";
                }



                dbconfig.Execute_Tsql(sUpdateScript, dbconfig.conn);

                dbconfig.cmd.Parameters.AddWithValue("@itemCode", detail.Item.ItemCode);
                dbconfig.cmd.Parameters.AddWithValue("@cost", detail.Item.Price);
                dbconfig.cmd.Parameters.AddWithValue("@unit", detail.Item.Unit);
                int rowsAffected = dbconfig.cmd.ExecuteNonQuery();

                #region COSTING LOGS
                int affectedRows = 0;
                if (rowsAffected > 0)
                {
                    logs += string.Format("\n{0},{1},{2}"
                        , detail.Item.ItemCode, detail.Item.Unit, detail.Item.Price);
                    affectedRows++;
                }

                #endregion

                #endregion

                #region Update Inventory BIN
                if (withEffectInInventory)
                {
                    dbconfig.Execute_Tsql("UPDATE inventorybin  "
                        + " set runningQTY = runningQTY + @quantity "
                        + " WHERE idStock = @itemCode and bin = (SELECT idbranch FROM business_segments limit 1); ", dbconfig.conn);

                    dbconfig.cmd.Parameters.AddWithValue("@itemCode", detail.Item.ItemCode);
                    dbconfig.cmd.Parameters.AddWithValue("@quantity", detail.Item.Quantity * detail.Item.Conversion);
                    dbconfig.cmd.Parameters.AddWithValue("@unit", detail.Item.Unit);
                    dbconfig.cmd.ExecuteNonQuery();
                }
                #endregion





            }
            #endregion



            return errorMessage;
        }

        public void UpdateSeries(MySqlConnection conn, string reference, string transaction)
        {
            string query = "UPDATE serial set lastnumber = @reference where prefix=@transaction";
            dbconfig.Execute_Tsql(query, dbconfig.conn);
            dbconfig.cmd.Parameters.AddWithValue("@reference", reference);
            dbconfig.cmd.Parameters.AddWithValue("@transaction", transaction);
            dbconfig.cmd.ExecuteNonQuery();
        }

        #endregion


        #region GET
        public string GetReference(MySqlConnection conn, string transaction)
        {
            string reference = "";
            dbconfig.Execute_Tsql("SELECT "
                    + " concat(prefix, LPAD((right(ifnull(lastnumber, 0), 10) * 1) + 1, 10, 0))  as reference "
                    + "  FROM serial where prefix = @transaction limit 1;", dbconfig.conn);
            dbconfig.cmd.Parameters.AddWithValue("@transaction", transaction);
            MySqlDataReader dr = dbconfig.cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    reference = dr["reference"].ToString();
                }
            }
            dr.Close();
            return reference;
        }
        public bool CheckReferenceIfExist(MySqlConnection conn, string reference)
        {
            int result = 0;
            dbconfig.Execute_Tsql("select count(x.reference) as result from ( "
                + " select reference from ledger where reference = @reference "
                + " union "
                + " select reference from invoice where reference = @reference) as x; ", dbconfig.conn);
            dbconfig.cmd.Parameters.AddWithValue("@reference", reference);
            MySqlDataReader dr = dbconfig.cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    result = DataConversion.ToInt(dr["result"].ToString());
                }
            }
            dr.Close();

            return result == 0 ? true : false;
        }

        public bool CheckCrossReferenceIfExist(MySqlConnection conn, string idfile, string reference)
        {
            int result = 0;
            dbconfig.Execute_Tsql("select count(crossreference) as result from ledger where idfile=@idfile and crossreference=@reference and cancelled=0", dbconfig.conn);
            dbconfig.cmd.Parameters.AddWithValue("@reference", reference);
            dbconfig.cmd.Parameters.AddWithValue("@idfile", idfile);
            MySqlDataReader dr = dbconfig.cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    result = DataConversion.ToInt(dr["result"].ToString());
                }
            }
            dr.Close();

            return result == 0 ? true : false;
        }

        public DataTable GetTransaction(string reference, bool isSAPConnection, string transtype = "ST")
        {
            try
            {
                dt = new DataTable();

                if (isSAPConnection && transtype == "ST")
                {
                    using (hanaSQL = new HanaSQL())
                    {
                        hanaSQL.SetQuery(2);
                        dt = hanaSQL.GetData(reference);
                    }
                }
                else
                {
                    try
                    {
                        dbconfig = new DatabaseConfiguration();
                        dbconfig.Set_Connection(dbconfig.ConnectionString);
                        MySqlDataAdapter da = new MySqlDataAdapter("SELECT  date as date_posted,"
                                    + " (select branchName from business_segments where idbranch = i.idbranch limit 1) as branch_name, "
                                    + " (select whse from business_segments where idbranch = i.idbranch limit 1) as warehouse_code, "
                                    + " (select main_segment from business_segments where idbranch = i.idbranch limit 1) as segment, "
                                    + " (select business_segment from business_segments where idbranch = i.idbranch limit 1) as business_segment, "
                                    + " (select branchCode from business_segments where idbranch = i.idbranch limit 1) as branch_code, "
                                    + " left(i.reference, 2) as transaction_type,i.reference as reference,i.date as posted_date,i.idfile as idfile,i.iduser as iduser, "
                                    + " ifnull((select crossreference from ledger l where reference = i.reference limit 1),'') as crossreference, "
                                    + " ifnull(i.idstock, '') item_code, "
                                    + " ifnull((select name from stocks  where idstock = i.idstock limit 1),'') as item_description, "
                                    + " ifnull(i.quantity, '') as item_qty, ifnull(i.unit, '') as item_unit,  ifnull(i.cost, '0.00') as item_cost, "
                                    + " ifnull(i.cost, '0.00') as item_price,ifnull(i.amount, '0.00') as item_total,unitQuantity as item_conversion, "
                                    + " '' "
                                    + " from invoice i where not cancelled "
                                    + " and i.quantity <> '0' and reference = @reference;", dbconfig.conn);
                        da.SelectCommand.Parameters.AddWithValue("@reference", reference);
                        da.Fill(dt);
                        da.Dispose();
                    }
                    catch
                    {
                        throw;
                    }
                    finally
                    {
                        dbconfig.Close_Connection(dbconfig.conn);
                    }
                }

                return dt;
            }
            catch
            {

                throw;
            }

        }

        public DataTable GetItemMaster(string reference, bool isSAPConnection, string transtype = "ST")
        {
            dt = new DataTable();

            try
            {
                if (isSAPConnection && transtype == "ST")
                {
                    using (hanaSQL = new HanaSQL())
                    {
                        hanaSQL.SetQuery(1);
                        dt = hanaSQL.GetData(reference);
                    }
                }
                else
                {
                    try
                    {
                        dbconfig = new DatabaseConfiguration();
                        dbconfig.Set_Connection(dbconfig.ConnectionString);
                        MySqlDataAdapter da = new MySqlDataAdapter("SELECT idstock as master_idstock, name as master_name, type as master_type, supplier as master_supplier,  "
                                + " status as master_status, remarks as master_remarks, barcode as master_barcode, discontinued as master_discontinued, "
                                + " consignment as master_consignment, taxable as master_taxable, discountable as master_discountable, CatID as master_CatID, "
                                + " packaging as master_packaging, subcategory as master_subcategory, subcatid as master_subcatid, brand as master_brand, brandid as master_brandid FROM stocks "
                                + " where idstock in ("
                                + " select idstock "
                                + " from invoice i where not cancelled "
                                + " and i.quantity <> '0' and reference = @reference); ", dbconfig.conn);
                        da.SelectCommand.Parameters.AddWithValue("@reference", reference);
                        da.Fill(dt);
                        da.Dispose();

                    }
                    catch
                    {
                        throw;
                    }
                    finally
                    {
                        dbconfig.Close_Connection(dbconfig.conn);
                    }
                }

                return dt;
            }
            catch
            {

                throw;
            }

        }

        public DataTable GetItemPcosting(string reference, bool isSAPConnection, string transtype = "ST")
        {
            try
            {
                dt = new DataTable();

                if (isSAPConnection && transtype == "ST")
                {
                    using (hanaSQL = new HanaSQL())
                    {
                        hanaSQL.SetQuery(3);
                        dt = hanaSQL.GetData(reference);
                    }
                }
                else
                {
                    try
                    {
                        dbconfig = new DatabaseConfiguration();
                        dbconfig.Set_Connection(dbconfig.ConnectionString);
                        MySqlDataAdapter da = new MySqlDataAdapter("SELECT idStock as p_idstock, barcode as p_barcode, unit as p_unit, 0 as p_cost,  "
                            + " 0 as p_selling, 0 as p_markup, quantity as p_conversion, costingMethod as p_costingMethod "
                            + "  FROM pcosting "
                            + " where idstock in ( "
                            + " select idstock "
                            + " from invoice i where not cancelled "
                            + " and i.quantity <> '0' and reference = @reference); ", dbconfig.conn);
                        da.SelectCommand.Parameters.AddWithValue("@reference", reference);
                        da.Fill(dt);
                        da.Dispose();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        dbconfig.Close_Connection(dbconfig.conn);
                    }
                }

                return dt;
            }
            catch
            {

                throw;
            }

        }

        public string GetWareHouseCode()
        {

            bool defaultSAP = false;
            try
            {
                bool.TryParse(ConfigurationManager.AppSettings["SAPasDefaultConnection"].ToString(), out defaultSAP);
            }
            catch
            {
                defaultSAP = false;
            }

            if (defaultSAP)
            {
                return "CentralWarehouse";
            }
            else
            {
                try
                {
                    string result = null;
                    dbconfig = new DatabaseConfiguration();
                    dbconfig.Set_Connection(dbconfig.ConnectionString);
                    dbconfig.Begin_transaction(dbconfig.conn);

                    dbconfig.Execute_Tsql("select whse from business_segments limit 1", dbconfig.conn);
                    //dbconfig.cmd.Parameters.AddWithValue("@reference", reference);
                    MySqlDataReader dr = dbconfig.cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            result = dr["whse"].ToString();
                        }
                    }
                    dr.Close();
                    dbconfig.Close_Connection(dbconfig.conn);
                    return result;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    //  Application.Exit();
                    return null;
                }
            }
        }
        #endregion
    }
}
