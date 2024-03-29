﻿using System;

using System.Data.OleDb;

namespace ResultSYS
{


    public static class result_db
    {
        static string connectionString;
        public static void get_prev_race(ref int prgNo,ref int kumi)
        {
            int uid;
            if (kumi == 1)
            {
                prgNo--;
                if (prgNo == 0) return;
                while (program_db.get_uid_from_prgno(prgNo) == 0) ;
                kumi = 1;
                uid = program_db.get_uid_from_prgno(prgNo);
                while (race_exist(uid, kumi)) kumi++;
            }
            kumi--;
             
        }

        public static void set_connectionString(string constring)
        {
            connectionString = constring;
        }
        public static int get_last_kumi(int prgNo)
        {
            int uid = program_db.get_uid_from_prgno(prgNo);
            int kumi = 1;
            while (race_exist(uid, kumi)) kumi++;
            return --kumi;
        }
        public static bool race_exist(int uid, int kumi)
        {
            
            int swimmerNo;
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                String sql = "SELECT UID, 選手番号, 組, 水路, 事由入力ステータス " +
                                     "FROM 記録マスター WHERE 組=" + kumi + " AND UID=" + uid + " ORDER BY 水路;";
                OleDbCommand comm = new OleDbCommand(sql, conn);
                conn.Open();

                using (var dr = comm.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        
                        swimmerNo = Misc.if_not_null(dr["選手番号"]);
                        if (swimmerNo > 0) return true;
                    }
                }
            }               
            return false;
        }
        public static int get_first_occupied_lane(int uid,int kumi)
        {
            int first_lane = event_db.get_max_lane_number();
            int swimmer;
           
            int this_lane;
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string sql = "SELECT UID, 選手番号, 組, 水路 FROM 記録マスター WHERE 組=" + kumi +
                    " AND UID = " + uid + ";";
                OleDbCommand comm = new OleDbCommand(sql, conn);
                conn.Open();

                using (var dr = comm.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        
                        swimmer = Misc.if_not_null(dr["選手番号"]);
                        if (swimmer > 0)
                        {
                            this_lane = Misc.if_not_null(dr["水路"]);
                            if (first_lane > this_lane)
                            {
                                first_lane = this_lane;
                            }
                        }

                    }
                }
            }
            return first_lane;
        }

        public static int get_last_occupied_lane(int uid, int kumi)
        {
            int last_lane = 0;
            int swimmer;

            int this_lane;
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string  sql = "SELECT UID, 選手番号, 組, 水路 FROM 記録マスター WHERE 組=" + kumi +
                    " AND UID = " + uid + ";";
                OleDbCommand comm = new OleDbCommand(sql, conn);
                conn.Open();

                using (var dr = comm.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        
                        swimmer = Misc.if_not_null(dr["選手番号"]);
                        if (swimmer > 0)
                        {
                            this_lane = Misc.if_not_null(dr["水路"]);
                            if (last_lane < this_lane)
                            {
                                last_lane = this_lane;
                            }
                        }

                    }
                }
            }
            return last_lane;
        }
    } //---end of class result_db

    public class event_db
    {
        static int maxLaneNo4Yosen;
        static int maxLaneNo4TimeFinal;
        static int maxLaneNo4Final;
        static int maxLaneNo;
        string eventName;
        string eventVenue;
        string eventDate;
        int poolLength;
        int lapInterval;
        public int Get_lap_interval() { return lapInterval; }
        public int get_pool_length()
        {
            return poolLength;
        }
        public  static int get_max_lane_number()
        {
            if (maxLaneNo > 0) return maxLaneNo; 
            maxLaneNo = maxLaneNo4Final;
            if (maxLaneNo4TimeFinal>maxLaneNo) maxLaneNo = maxLaneNo4TimeFinal;
            
            if (maxLaneNo4Yosen > maxLaneNo) maxLaneNo = maxLaneNo4Yosen;
            
            return maxLaneNo;
        }
        public string get_eventName()
        {
            return eventName;
        }
        public string get_eventVenue()
        {
            return eventVenue;
        }
        public string get_eventDate()
        {
            return eventDate;
        }
#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
        public event_db(string connectionString)
        {
            OleDbConnection conn = new OleDbConnection(connectionString);
            maxLaneNo = 0;
            read_event_db_(conn);
        }
        void read_event_db_(OleDbConnection conn)
        {
            int touchBoard;

            using (conn )
            {

                String sql = "SELECT *  " +
                 "FROM 大会設定 WHERE 大会名１ IS NOT NULL;";
//                String sql = "SELECT 大会名１, 開催地, 始期間, 終期間, プール, タッチ板,   使用水路予選, 使用水路タイム決勝, 使用水路決勝 " +
//                 "FROM 大会設定 WHERE 大会名１ IS NOT NULL;";
                OleDbCommand comm = new OleDbCommand(sql, conn);
                conn.Open();
                using (var dr = comm.ExecuteReader())
                {
                    //------------------------------zzzzzzzzzzzzzzzzzzzzzzzzzzzz
                    dr.Read();
                    eventName = Misc.if_not_null_string(dr["大会名１"]);
                    if ((Misc.if_not_null_string(dr["始期間"])).Equals(Misc.if_not_null_string(dr["終期間"])))
                    {
                        eventDate = "      " + Misc.if_not_null_string(dr["始期間"]) + "      ";
                    }
                    else
                    {
                        eventDate = Misc.if_not_null_string(dr["始期間"]) + "～" + Misc.if_not_null_string(dr["終期間"]);
                    }
                    eventVenue = Misc.if_not_null_string(dr["開催地"]);
                    maxLaneNo4Final = Misc.if_not_null(dr["使用水路決勝"]);
                    maxLaneNo4TimeFinal = Misc.if_not_null(dr["使用水路タイム決勝"]);
                    maxLaneNo4Yosen = Misc.if_not_null(dr["使用水路予選"]);
                    touchBoard = Misc.if_not_null(dr["タッチ板"]);  

                    if (Misc.if_not_null(dr["プール"]) <= 2) poolLength = 50;
                    else poolLength = 25;
                    lapInterval = 50;
                    if ((poolLength == 50) && (touchBoard==2) ) lapInterval = 100;
                    if ((poolLength == 25) && (touchBoard==4) ) lapInterval = 50;
                }
            }
        }
        }
    


    public class team_db
    {
        string[] teamName4Relay;
        public string get_name(int teamID)
        {
            return teamName4Relay[teamID];
        }
        public team_db(string connectionString)
        {
            OleDbConnection conn = new OleDbConnection(connectionString);
            init_team_db_array(conn);
            conn = new OleDbConnection(connectionString);
            read_team_db_(conn);
        }
        public  void init_team_db_array(OleDbConnection conn)
        {
            String sql = "SELECT チーム番号 FROM チームマスター where チーム番号=(select max(チーム番号) from チームマスター);  ";
            int numTeam;

            using (conn)
            {
                OleDbCommand comm = new OleDbCommand(sql, conn);
                conn.Open();
                using (var dr = comm.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        numTeam = Convert.ToInt32(dr["チーム番号"]);
                        redim_team_db_array(numTeam);
                    }
                }
            }
        }
        public void redim_team_db_array(int numTeam)
        {
            int ubound = numTeam + 1;
            Array.Resize<string>(ref teamName4Relay, ubound);
        }
        public void read_team_db_(OleDbConnection conn)
        {

            String sql = "SELECT チーム番号, チーム名  FROM チームマスター ;";
            int teamID;

            using (conn)
            {
                OleDbCommand comm = new OleDbCommand(sql, conn);
                conn.Open();
                using (
                    var dr = comm.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        teamID = Convert.ToInt32(dr["チーム番号"]);
                        teamName4Relay[teamID] = (Misc.if_not_null_string(dr["チーム名"])).Trim();
                    }
                }
            }
        }
    }


    public class swimmer_db
    {

        public string[] SwimmerName;
        public string[] SwimmerNameKANA;
        public int[] BelongsTo;
        string[] team = new string[1000];

        int maxTeamNum = 0;
        public string get_name(int swimmerID)
        {
            return SwimmerName[swimmerID];
        }
        public string get_furigana(int swimmerID)
        {
            return SwimmerNameKANA[swimmerID];
        }
        public string get_team_name(int swimmerID)
        {
            return team[BelongsTo[swimmerID]];
        }
        public swimmer_db(string connectionString)
        {
            OleDbConnection conn = new OleDbConnection(connectionString);
            init_swimmer_db_array(conn);
            conn = new OleDbConnection(connectionString);
            read_swimmer_db_(conn);
        }
        public void init_swimmer_db_array(OleDbConnection conn)
        {
            String sql = "SELECT 選手番号 FROM 選手マスター where 選手番号=(select max(選手番号) from 選手マスター);  ";
            int numSwimmer;

            using (conn)
            {
                OleDbCommand comm = new OleDbCommand(sql, conn);
                conn.Open();
                using (var dr = comm.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        numSwimmer = Convert.ToInt32(dr["選手番号"]);
                        redim_swimmer_db_array(numSwimmer);
                    }
                }

            }
        }
        public void redim_swimmer_db_array(int numSwimmer)
        {
            int ubound = numSwimmer + 1;
            Array.Resize<string>(ref SwimmerName, ubound);
            Array.Resize<string>(ref SwimmerNameKANA, ubound);
            Array.Resize<int>(ref BelongsTo, ubound);
        }
        public void read_swimmer_db_(OleDbConnection conn)
        {

            String sql = "SELECT 選手番号, 氏名, カナ, 所属１, 所属名称１  FROM 選手マスター ;";
            int swimmerID;
            int clubNo;
            using (conn)
            {

                OleDbCommand comm = new OleDbCommand(sql, conn);
                conn.Open();
                using (
                    var dr = comm.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        if (dr["選手番号"] == null) swimmerID = 0;
                        else
                        swimmerID = Convert.ToInt32(dr["選手番号"]);
                        SwimmerName[swimmerID] = (Misc.if_not_null_string(dr["氏名"])).Trim();
                        SwimmerNameKANA[swimmerID] = (Misc.if_not_null_string(dr["カナ"])).Trim();
                        clubNo = locate_team_id(Misc.if_not_null_string(dr["所属名称１"]));
                        BelongsTo[swimmerID] = clubNo;
                    }
                }
            }
        }
        public int locate_team_id(string teamName)
        {
            int cnt;
            for (cnt = 0; cnt < maxTeamNum; cnt++)
            {
                if (team[cnt] == teamName) return cnt;
            }
            team[maxTeamNum++] = teamName;
            return maxTeamNum-1;
        }
    }

    public class record_db
    {

        public record_db(string connectionString)
        {

            OleDbConnection conn = new OleDbConnection(connectionString);

            conn = new OleDbConnection(connectionString);
            read_record_db_(conn);
        }
        public void read_record_db_(OleDbConnection conn)
        {
        
            String sql = "SELECT 記録区分番号, 記録名称番号, 性別, 種目, 距離, 記録 FROM 新記録;";
            using (conn)
            {
                OleDbCommand comm = new OleDbCommand(sql, conn);
                conn.Open();
                using (OleDbDataReader dr = comm.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        
                        int classNo = Convert.ToInt32(dr["記録区分番号"]);
                        int gender = Convert.ToInt32(dr["性別"]);
                       
                        int styleNo = program_db.locate_style_number(Misc.if_not_null_string(dr["種目"]));
                        int distanceNo = program_db.locate_distance_number(Misc.if_not_null_string(dr["距離"]));
                        int uid = program_db.locate_uid(classNo, gender, distanceNo, styleNo);
                        program_db.bestRecord[uid] = Misc.timestr2int(Misc.if_not_null_string(dr["記録"]));
                    }
                }

            }
        }
    }



    public class class_db
    {
        static string[] className;
        public class_db(string connectionString)
        {

            OleDbConnection conn = new OleDbConnection(connectionString);
            init_class_db_array(conn);
            conn = new OleDbConnection(connectionString);
            read_class_db_(conn);
        }
        public static string get_name(int classID)
        {
            return className[classID];
        }
        public void init_class_db_array(OleDbConnection conn)
        {
            String sql = "SELECT 番号 FROM クラス where 番号=(select max(番号) from クラス);  ";
            int maxClassNumber;

            using (conn)
            {
                OleDbCommand comm = new OleDbCommand(sql, conn);
                conn.Open();
                using (var dr = comm.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        maxClassNumber = Convert.ToInt32(dr["番号"]);
                        Array.Resize<string>(ref className, maxClassNumber + 1);
                    }
                }
            }
        }
        public void read_class_db_(OleDbConnection conn)
        {

            String sql = "SELECT 番号,クラス名称 FROM クラス;";
            using (conn)
            {
                OleDbCommand comm = new OleDbCommand(sql, conn);
                conn.Open();
                using ( OleDbDataReader dr = comm.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        className[Misc.if_not_null(dr["番号"])] = Misc.if_not_null_string(dr["クラス名称"]);

                    }
                }

            }
        }
    }

    
    
    public class program_db
    {
        static int[] classNumberbyUID;
        static string[] phase;
        static string[] distancebyUID;


        static string currentConnectionString;

        static int[] ShumokubyUID;
        static int[] raceNobyUID;
        static int[] UIDFromRaceNo;
        public static int[] bestRecord;
        static int[] GenderbyUID;
        static int maxUID;
        static int maxProgramNo;
        const int NUMSTYLE = 7;
        const int NUMDISTANCE = 7;
 
        const string magicWord = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=";

        readonly static string[] ShumokuTable = new string[NUMSTYLE]
            { "自由形","背泳ぎ","平泳ぎ","バタフライ", "個人メドレー", "リレー", "メドレーリレー" };
        readonly static  string[] distanceTable = new string[NUMDISTANCE] { "  25m","  50m",
        " 100m"," 200m"," 400m", " 800m", "1500m" };
        readonly static string[] GenderStr = new string[3] { "男子", "女子", "混合" };
        public static int get_max_program_no() { return maxProgramNo; }

        public int Get_max_laneNo()
        {
            int maxLaneNo;
            int maxLaneNo4tf;
            int maxLaneNo4f;
            OleDbConnection conn = new OleDbConnection(currentConnectionString);
            using (conn)
            {
                String sql = "SELECT 使用水路予選, 使用水路タイム決勝, 使用水路決勝 FROM 大会設定 ;";
                OleDbCommand comm = new OleDbCommand(sql, conn);
                conn.Open();
                using (OleDbDataReader dr = comm.ExecuteReader())
                {
                    dr.Read();
                    maxLaneNo = Misc.if_not_null(dr["使用水路予選"]);
                    maxLaneNo4tf = Misc.if_not_null(dr["使用水路タイム決勝"]);
                    maxLaneNo4f = Misc.if_not_null(dr["使用水路決勝"]);
                    if (maxLaneNo < maxLaneNo4tf) maxLaneNo = maxLaneNo4tf;
                    if (maxLaneNo < maxLaneNo4f) maxLaneNo = maxLaneNo4f;
                }
            }
            return maxLaneNo;
        }
        public bool get_next_race(ref int prgNo, ref int kumi)
        {

            if (prgNo == 0)
            {
                prgNo = 1;
                kumi = 1;
                return true;
            }
            int maxKumi = Get_max_kumi(prgNo);
            if (kumi == maxKumi)
            {
                int maxPrgNo = get_max_program_no();
                if (maxPrgNo == prgNo) return false;
                prgNo++;
                kumi = 1;
                return true;
            }

            kumi++;
            return true;
        }

        public int get_swimmer_id(int prgNo, int kumi, int laneNo)
        {
            int swimmerID = 0;


            int uid = program_db.get_uid_from_prgno(prgNo);
            OleDbConnection conn = new OleDbConnection(currentConnectionString);
            using (conn)
            {
                String sql = "SELECT UID, 選手番号, 組, 水路, ラップ１, ラップ２, ラップ３,事由入力ステータス " +
                                    "FROM 記録マスター WHERE 組=" + kumi + " AND UID=" + uid +
                                    " AND 水路=" + laneNo + ";";
                OleDbCommand comm = new OleDbCommand(sql, conn);
                conn.Open();
                using (OleDbDataReader dr = comm.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        swimmerID = Misc.if_not_null(dr["選手番号"]);
                    }
                }
            }
            return swimmerID;

        }
        private int Get_first_occupied_lane(int prgNo, int kumi)
        {
            int laneNo;
            for (laneNo = 1; laneNo <= Get_max_laneNo(); laneNo++)
            {
                if (get_swimmer_id(prgNo, kumi, laneNo) > 0) return laneNo;
            }
            return 0;
        }
        private int Get_last_occupied_lane(int prgNo, int kumi)
        {
            int laneNo;
            int lastOccupiedLane = 0;
            for (laneNo = 1; laneNo <= Get_max_laneNo(); laneNo++)
            {
                if (get_swimmer_id(prgNo, kumi, laneNo) > 0) lastOccupiedLane = laneNo;
            }
            return lastOccupiedLane;
        }
        public int can_go_with_next(ref int prgNo, ref int kumi)
        /* if true, returns firstLaneNo of the next race */
        {
            int nextPrgNo = prgNo;
            int nextKumi = kumi;
            int firstLaneNo;
            get_next_race(ref nextPrgNo, ref nextKumi);
            firstLaneNo = Get_first_occupied_lane(nextPrgNo, nextKumi);
            if (Get_last_occupied_lane(prgNo, kumi) < firstLaneNo)
            {
                prgNo = nextPrgNo; kumi = nextKumi;
                return firstLaneNo;
            }
            return 0;


        }
        public static int get_next_uid(int uid)
        {
            int prgNo = get_race_number_from_uid(uid);
            int nextUID;
            do
            {
                prgNo++;
                if (prgNo > maxProgramNo) return 0;
                nextUID = UIDFromRaceNo[prgNo];
            } while (nextUID == 0);
 
            return nextUID;
        }
        public static int get_prev_uid(int uid)
        {
            int prgNo = get_race_number_from_uid(uid);
            int prevUID;
            do
            {
                prgNo--;
                if (prgNo == 0) return 0;
                prevUID = UIDFromRaceNo[prgNo];
            } while (prevUID == 0);
            return prevUID;
        }
       
        public static int locate_uid(int classNo, int gender, int distanceNo, int styleNo )
        {
            int uid;
            const int notFound = 0;
            for (uid=1; uid<=maxUID; uid++)
            {
                if ((ShumokubyUID[uid] == styleNo) && (distancebyUID[uid] == distanceTable[distanceNo]) &&
                     (classNumberbyUID[uid] == classNo) && (GenderbyUID[uid] == gender)) return uid;
            }
            return notFound;
        }
        
        public static bool inc_race_number(ref int prgNo)
        {
            do
            {
                prgNo++;
                if (prgNo > maxProgramNo)
                {
                 //   dec_race_number(ref prgNo);
                    return false;
                }
            } while (UIDFromRaceNo[prgNo] == 0);
            return true;
        }
        public static void dec_race_number(ref int prgNo)
        {
            do
            {
                prgNo--;
                if (prgNo == 0) return;
            } while (UIDFromRaceNo[prgNo] == 0);
        }
        public static int get_uid_from_prgno(int prgNO)
        {
            return UIDFromRaceNo[prgNO];
        }
        public static string get_gender_from_uid(int UID)
        {
            return GenderStr[GenderbyUID[UID]-1];
        }
        public static string get_class_from_uid(int UID)
        {
            if (UID < 1) { return "NC"; }
            if (UID > maxUID) { return "NC"; }
            return class_db.get_name(classNumberbyUID[UID]);
        }
        public static string get_phase_from_uid(int UID)
        {
            if (UID < 1) { return "?"; }
            if (UID > maxUID) { return "?"; }
            return phase[UID];
        }
        public static string get_distance_from_uid(int UID)
        {
            if (UID < 1) { return "?"; }
            if (UID > maxUID) { return "?"; }
            return distancebyUID[UID];
        }
        
        public static string get_shumoku_from_uid(int UID)
        {
            if (UID < 1) { return "?"; }
            if (UID > maxUID) { return "?"; }
            return ShumokuTable[ShumokubyUID[UID]];
        }
        public static int get_race_number_from_uid(int UID)
        {
            if (UID < 1) { return -1; }
            if (UID > maxUID) { return -1; }
            return raceNobyUID[UID];
        }
        public static bool is_relay(int UID)
        {
            if (ShumokubyUID[UID] > 4) return true;
            return false;
        }
        public static bool is_same_distance_style(int uid1, int uid2)
        {
        //    if (get_distance_from_uid(uid1) != get_distance_from_uid(uid2)) return false;
            if (get_shumoku_from_uid(uid1) != get_shumoku_from_uid(uid2)) return false;
            if (get_phase_from_uid(uid1) != get_phase_from_uid(uid2)) return false;
            return true;
        }
        public program_db(String connectionString)
        {

            currentConnectionString= connectionString;
            OleDbConnection conn = new OleDbConnection(connectionString);
            init_program_db_array(conn);
            conn = new OleDbConnection(connectionString);
            read_program_db_(conn);
        }
        public void init_program_db_array(OleDbConnection conn)
        {
            String sql = "SELECT UID FROM プログラム where UID=(select max(UID) from プログラム);  ";
            

            using (conn)
            {
                OleDbCommand comm = new OleDbCommand(sql, conn);
                conn.Open();
                using (var dr = comm.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        maxUID = Convert.ToInt32(dr["UID"]);
                        redim_program_db_array();
                    }
                }
                sql = "SELECT 競技番号 FROM プログラム where 競技番号=(select max(競技番号) from プログラム);";
                comm = new OleDbCommand(sql, conn);
                using (var dr = comm.ExecuteReader())
                {
                    dr.Read();
                    maxProgramNo = Convert.ToInt32(dr["競技番号"]);
                    Array.Resize<int>(ref UIDFromRaceNo, maxProgramNo + 1);
                }
            }

        }
        public void redim_program_db_array()
        {

            int ubound = maxUID + 1;
            classNumberbyUID = new int[ubound];


            phase = new string[ubound];
            distancebyUID = new string[ubound];
            ShumokubyUID = new int[ubound];
            raceNobyUID = new int[ubound];
            bestRecord = new int[ubound];
            GenderbyUID = new int[ubound];

        }
        public static int locate_style_number(string styleName)
        {
            int cnt;
            for (cnt = 0; cnt < NUMSTYLE; cnt++)
            {
                if (ShumokuTable[cnt] == styleName) return cnt;
            }
            return -1;
        }
        public static int locate_distance_number(string distance)
        {
            int cnt;
            for (cnt = 0; cnt < NUMDISTANCE; cnt++)
            {
                if (distanceTable[cnt] == distance) return cnt;
            }
            return -1;
        }

        public string get_distance(int prgNo)
        {
            int uid = get_uid_from_prgno(prgNo);
            return get_distance_from_uid(uid);
        }
        public static int Get_max_kumi(int prgNo)
        {

            int maxKumi;
            int uid = program_db.get_uid_from_prgno(prgNo);
            OleDbConnection conn = new OleDbConnection(currentConnectionString);
            using (conn)
            {
                String sql = "SELECT  MAX(組) AS maxkumi FROM 記録マスター WHERE UID=" + uid + ";";
                OleDbCommand comm = new OleDbCommand(sql, conn);
                conn.Open();
                using (OleDbDataReader dr = comm.ExecuteReader())
                {
                    dr.Read();
                    maxKumi = Convert.ToInt32(dr["maxkumi"]);
                }
            }
            return maxKumi;
        }
 
        public void read_program_db_(OleDbConnection conn)
        {

            String sql = "SELECT UID, 競技番号, 種目, 距離, 性別, 予決, クラス番号  FROM プログラム ;";
            int raceNo;
            int uid;
            using (conn)
            {

                OleDbCommand comm = new OleDbCommand(sql, conn);
                conn.Open();
                using (
                    var dr = comm.ExecuteReader())
                {
                    while (dr.Read())
                    {
                       
                        raceNo = Convert.ToInt32(dr["競技番号"]);
                        uid = Convert.ToInt32(dr["UID"]);
                        raceNobyUID[uid] = raceNo;
                        UIDFromRaceNo[raceNo] = uid;
                        ShumokubyUID[uid] = locate_style_number(Misc.if_not_null_string(dr["種目"]));
                        distancebyUID[uid] = Convert.ToString(dr["距離"]);
                        GenderbyUID[uid] = Convert.ToInt32(dr["性別"]);
                        phase[uid] = Convert.ToString(dr["予決"]);
                        
                        classNumberbyUID[uid] = Convert.ToInt32(dr["クラス番号"]);
                    }
                }

            }
           
        }
    }
    public static class Misc
    {
        static int[] lapCounter = new int[10] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public static int substract_time(int currentTime, int lastTime)
        {
            int minute;
            int second;
            int centiSecond;

            minute = (currentTime / 10000) - (lastTime / 10000);
            second = ((currentTime % 10000)/100) - ((lastTime % 10000)/100);
            centiSecond = (currentTime % 100) -(lastTime % 100);

            if (centiSecond<0)
            {
                second -= 1;
                centiSecond += 100;
            }
            if (second<0)
            {
                minute -= 1;
                second += 60;
            }
            return minute*10000+second*100+centiSecond;
        }


        public static string timeint2str(int mytime)
        {
            int minutes = mytime / 10000;
            int temps = mytime % 10000;
            int seconds = temps / 100;
            int centiseconds = temps % 100;
            if (minutes > 0)
            {
                return minutes.ToString().PadLeft(2) + ":" + seconds.ToString().PadLeft(2, '0') + "." + centiseconds.ToString().PadLeft(2, '0');
            }
            return "   " + seconds.ToString().PadLeft(2) + "." + centiseconds.ToString().PadLeft(2, '0');
        }
        public static int timestr2int(string timeStr)
        {
            string intstr;
            intstr = timeStr.Replace(":", "");
            intstr = intstr.Replace(".", "");
            return Int32.Parse(intstr);

        }
        public static string format_time(string orgtime)
        {
            const int totalLength = 9;
            int mylength = orgtime.Length;
            string header = new string(' ', totalLength - mylength);
            return header + orgtime;
        }

        public static int get_lapcounter(int lane)
        {
            return lapCounter[lane - 1];
        }
        public static void inc_lapcounter(int lane)
        {
            lapCounter[lane - 1]++;
        }

        public static void init_lapcounter(int lane)
        {

            lapCounter[lane - 1] = 1;

        }
        public static int if_not_null(object dr)
        {
            if (dr == DBNull.Value) return 0;
            return Convert.ToInt32(dr);
        }
        public static string if_not_null_string(object dr)
        {
            if (dr == DBNull.Value) return "";
            if (dr == null) return "";
            return (string) (dr);
        }
        public static string get_ith_lap(string lapstr, int ith)
        {

            const int ELEMENTLENGTH = 18;
            if (lapstr.Length >= ELEMENTLENGTH * ith)
            {
                return lapstr.Substring(ELEMENTLENGTH * (ith - 1), ELEMENTLENGTH).Trim();
            }

            return "";
        }
    }
}
