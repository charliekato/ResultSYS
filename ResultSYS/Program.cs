using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace ResultSYS
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new Form1());
        }
    }
    public static class ExcelConnection
    {
        private static string mdbFile="swmresult.accdb";
        private const string magicWord = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=";
        //  private const string magicTail = ";Extended Properties='Excel 12.0; HDR=Yes'";
        private const string magicTail = "";
        private const string tblName = "RESULT";
        private const string prgTblName = "PROGRAM";

        public static void set_mdb_file_name(string mdbFileName)
        {
            mdbFile = mdbFileName; 
        }

        public static void delete()
        {

            String connectionString = magicWord + mdbFile + magicTail;
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                String sql = "DELETE * from " + tblName;
                using (OleDbCommand comm = new OleDbCommand(sql, conn)) {


                    conn.Open();
                    //	    comm.CommandText = query;
                    comm.ExecuteNonQuery();
                    comm.CommandText = "DELETE * from " + prgTblName;
                    comm.ExecuteNonQuery();

                }
            }


        }
        public static void program_append(int prgNo, string ageClass, string gender, string distance, string style, string phase)
        {
            String connectionString = magicWord + mdbFile + magicTail;
            string bquery = " SELECT * FROM " + prgTblName + " WHERE " +
                "PRG_NO = @PRGNO;";
            string pquery = " INSERT INTO " + prgTblName + " (PRG_NO, CLASS, GENDER, DISTANCE, STYLE, PHASE)" +
                " VALUES ( @PRGNO, @CLASS, @GENDER, @DISTANCE, @STYLE, @PHASE );";
            bool dataExist;
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                using (OleDbCommand comm = new OleDbCommand("", conn))
                {
                    comm.Parameters.Add(new OleDbParameter("@PRGNO", prgNo));
                    comm.Parameters.Add(new OleDbParameter("@CLASS", ageClass));
                    comm.Parameters.Add(new OleDbParameter("@GENDER", gender));
                    comm.Parameters.Add(new OleDbParameter("@DISTANCE", distance));
                    comm.Parameters.Add(new OleDbParameter("@STYLE", style));
                    comm.Parameters.Add(new OleDbParameter("@PHASE", phase));
                    comm.CommandText = bquery;
                    using (OleDbDataReader dr = comm.ExecuteReader())
                    {
                        dataExist = dr.Read();
                    }
                    if (!dataExist)
                    {

                        comm.CommandText = pquery;
                        comm.ExecuteNonQuery();
                    }
                }
            }
                

        }
        public static void append( int prgNo, int kumi, int laneNo, string name = "")
        {

            String connectionString = magicWord + mdbFile + magicTail;
            string bquery = " SELECT * FROM " + tblName + " WHERE " +
                "PRG_NO = @PRGNO AND KUMI = @KUMI AND LANE_NO = @LANENO AND NAME = @NAME;";
            string  pquery = " INSERT INTO " + tblName + " (PRG_NO, KUMI, LANE_NO, SWIMMER_NAME)" +
                " VALUES ( @PRGNO , @KUMI , @LANENO, @NAME );";
            bool dataExist;

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                
                OleDbCommand comm = new OleDbCommand("", conn);
                comm.Parameters.Add(new OleDbParameter("@PRGNO", prgNo));
                comm.Parameters.Add(new OleDbParameter("@KUMI", kumi));
                comm.Parameters.Add(new OleDbParameter("@LANENO", laneNo));
                comm.Parameters.Add(new OleDbParameter("@NAME", name));
                comm.CommandText = bquery;
                using ( OleDbDataReader dr = comm.ExecuteReader())
                {
                     dataExist = dr.Read();
                }
                if (!dataExist) {

                    comm.CommandText = pquery;
                    comm.ExecuteNonQuery();
                }
            }
        }
        private static string get_old_time(int prgNo, int kumi, int laneNo)
        {
            return "";
        }

        public static void insert_time(int prgNo, int kumi, int laneNo, int intTime, string distance="GOAL")
        {
             String connectionString = magicWord + mdbFile + magicTail;

	     string time = misc.timeint2str(intTime);
               
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = string.Empty;
                string oldTime;
                string newTime;
                oldTime = get_old_time(prgNo, kumi, laneNo);
                if (oldTime == "") newTime = time;
                else newTime = oldTime + "<BR>" + time;
                query = string.Empty;
                String mquery = " UPDATE " + tblName + " SET " + distance + " = '" + newTime + "'" +
                    " WHERE PRG_NO = @PRGNO AND KUMI = @KUMI AND LANE_NO = @LANENO;";


                query += " UPDATE " + tblName;
                query += " SET ";
                query += "      "+distance + " = '" + newTime + "' ";
                query += " WHERE ";
                query += "     PRG_NO =" + prgNo + " AND KUMI = " + kumi;
                query += " AND LANE_NO = " + laneNo + " ;";
                
                conn.Open();
                OleDbCommand comm = new OleDbCommand("", conn);
                comm.Parameters.Add(new OleDbParameter("@PRGNO", prgNo));
                comm.Parameters.Add(new OleDbParameter("@KUMI", kumi));
                comm.Parameters.Add(new OleDbParameter("@LANENO", laneNo));
//                comm.Parameters.Add(new OleDbParameter("@STIME", "'"+newTime+"'"));
 
                comm.CommandText = mquery;
                comm.ExecuteNonQuery();

            }
        }

        public static void insert_lap(int prgNo, int kumi, int laneNo, string time, string distance)
        {
            String connectionString = magicWord + mdbFile + magicTail;
            String pquery = " SELECT DISTINCT * FROM " + tblName + " WHERE " +
               " PRG_NO = @PRGNO AND KUMI = @KUMI AND LANE_NO = @LANENO;";
            String mquery = " UPDATE " + tblName + " SET " + distance  + " = @TIME" +
                " WHERE PRG_NO = @PRGNO AND KUMI = @KUMI AND LANE_NO = @LANENO;";

                
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = string.Empty;
                string oldTime;
                string newTime;
                conn.Open();
                /*
                query += "SELECT * ";
                //query += distance;
                query += " FROM " + tblName;
                query += " WHERE ";
                query += "     PRG_NO =" + prgNo + " AND KUMI = " + kumi;
                query += " AND LANE_NO = " + laneNo + " ;";
                */
                OleDbCommand  comm = new OleDbCommand("", conn);
                comm.Parameters.Add(new OleDbParameter("@PRGNO", prgNo));
                comm.Parameters.Add(new OleDbParameter("@KUMI", kumi));
                comm.Parameters.Add(new OleDbParameter("@LANENO", laneNo));
                comm.CommandText = pquery;
                using (var dr = comm.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        oldTime = misc.if_not_null_string(dr[distance]);
                    }
                    else oldTime = "";
                }
                if (oldTime == "") newTime = time;
                else newTime = oldTime + "<BR>" + time;

                query = string.Empty;

                query += " UPDATE " + tblName;
                query += " SET ";
                query += "      "+distance + " = '" + newTime + "' ";
                query += " WHERE ";
                query += "     PRG_NO =" + prgNo + " AND KUMI = " + kumi;
                query += " AND LANE_NO = " + laneNo + " ;";
                //          Console.WriteLine(">>" + query);
                
//                comm.Parameters.Add(new OleDbParameter("@TIME", newTime));

                comm = new OleDbCommand(query, conn);
                comm.CommandText = query;
                comm.ExecuteNonQuery();

            }
        }
        public static void insert_goal(int prgNo, int kumi, int laneNo, string time)
        {
            String connectionString = magicWord + mdbFile + magicTail;
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                string query = string.Empty;
                string oldTime;
                query += "SELECT PRG_NO, KUMI, LANE_NO, ";
                query += "GOAL FROM " + tblName;
                query += " WHERE ";
                query += "     PRG_NO =" + prgNo + " AND KUMI = " + kumi;
                query += " AND LANE_NO = " + laneNo + " ;";
                OleDbCommand comm=new OleDbCommand(query, conn);
                using (var dr = comm.ExecuteReader())
                {
                    dr.Read();
                    oldTime = misc.if_not_null_string(dr["GOAL"]);
                }
                if (oldTime == "") oldTime = time;
                else oldTime = oldTime + "<BR>" + time;

                query = string.Empty;

                query += " UPDATE " + tblName;
                query += " SET ";
                query += "      GOAL = '" + oldTime + "' ";
                query += " WHERE ";
                query += "     PRG_NO =" + prgNo + " AND KUMI = " + kumi;
                query += " AND LANE_NO = " + laneNo + " ;";
                //          Console.WriteLine(">>" + query);

                comm = new OleDbCommand(query, conn);
                comm.CommandText = query;
                comm.ExecuteNonQuery();

            }
        }
    }
                /*
                query += "SELECT * ";
                //query += distance;
                query += " FROM " + tblName;
                query += " WHERE ";
                query += "     PRG_NO =" + prgNo + " AND KUMI = " + kumi;
                query += " AND LANE_NO = " + laneNo + " ;";
                */

}
