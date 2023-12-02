
using System;
using System.ComponentModel.Design;
using System.IO;
using System.Text;
using System.Windows.Forms;

using System.Data.OleDb;



namespace ResultSYS
{

#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
    public partial class MainForm : Form
    {
        string folderName="";

        event_db evtDB;
        program_db prgDB;
        class_db clsDB;
        team_db tmDB;
        swimmer_db swmDB;
        record_db rcdDB;


        const string magicWord = "Provider=Microsoft.ACE.OLEDB.12.0;Mode=Read;Data Source=";

        static Encoding shiftjisEnc = Encoding.GetEncoding("Shift-JIS");
        public bool StopAutoRun;
        // related 大会設定
//        private string EventName;
//        private string EventDate;
//        private string EventVenue;

        
        static public string[] Team = new string[100];
        static public int LastPrgNo;
        static public int FirstPrgNo;
        static public int MaxTeamNum=0;
        // for チームマスター
        static public string[] TeamName4Relay;

        public MainForm()
        {
            setupFileIo.read_setup_file(ref folderName);
            InitializeComponent();
            init_size(930,780);
            set_size_of_list_box(900, 520);
            if (folderName != "")
            {
                txtBxFolder.Text = folderName;
                call_showEventList();
            }   
        }
        private void set_size_of_list_box(int width, int height)
        {
            lbxDbContents.Width = width;
            lbxDbContents.Height = height;
        }
        private void init_size(int width, int height) {
            this.Width = width;
            this.Height = height;
        }



        private void btnGo_click(object sender, EventArgs e)
        {

            string selectedString="";
            string[] sep = { " " };

            if (lbxDbContents.SelectedItem==null)
            {
                MessageBox.Show("大会を選択してください。");
            } else
            {

                selectedString = lbxDbContents.SelectedItem.ToString();
                //------------------------
                string[] eventInfo = selectedString.Split(sep,StringSplitOptions.RemoveEmptyEntries);
                string fullpathDBName = folderName + "\\" + eventInfo[0];
                convert_go(fullpathDBName);

             }
        }
        private void convert_go(string filename)
        {

            string connectionString = magicWord + filename;

            this.Cursor = Cursors.WaitCursor;
            evtDB = new event_db(connectionString);
            prgDB = new program_db(connectionString);
            clsDB = new class_db(connectionString);
            tmDB = new team_db(connectionString);
            swmDB = new swimmer_db(connectionString);
            rcdDB = new record_db(connectionString);
//            maxLaneNo = event_db.get_max_lane_number();

            result_db.set_connectionString(connectionString);
            set_excel_program_table();
            set_excel_result_table(connectionString);

            this.Cursor = Cursors.Default;

        }
        private void set_excel_program_table() // excel db ==> see ExcelConnection in Program.cs
        {
            int prgNo;
            int uid;
            string style;
            string gender;
            string distance;
            string classname;
            string phase;
            for (uid=1; uid>0; uid=program_db.get_next_uid(uid))
            {
                prgNo = program_db.get_race_number_from_uid(uid);
                style = program_db.get_shumoku_from_uid(uid);
                gender = program_db.get_gender_from_uid(uid);
                distance = program_db.get_distance_from_uid(uid);
                classname = program_db.get_class_from_uid(uid);
                phase = program_db.get_phase_from_uid(uid) ;
                ExcelConnection.program_append(prgNo, classname, gender, distance, style, phase);
            }
        }
        private void set_excel_result_table(string connectionString)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                String sql = "SELECT UID, 選手番号, 組, 水路 " +
                                     "FROM 記録マスター ";
                OleDbCommand comm = new OleDbCommand(sql, conn);
                conn.Open();

                using (var dr = comm.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int prgNo;
                        int uid;
                        int laneNo;
                        int kumi;
                        int swimmerNo;
                        string swimmerName;
                        uid = Misc.if_not_null(dr["UID"]);
                        prgNo = program_db.get_race_number_from_uid(uid);
                        laneNo = Misc.if_not_null(dr["水路"]);
                        kumi = Misc.if_not_null(dr["組"]);
                        swimmerNo = Misc.if_not_null(dr["選手番号"]);
                        if (swimmerNo == 0)
                        {
                            ExcelConnection.append(prgNo, kumi, laneNo);
                        }  else
                        {
                            if (program_db.is_relay(uid)) {
                                swimmerName = tmDB.get_name(Misc.if_not_null(dr["選手番号"]));

                            } else
                            {
                                swimmerName = swmDB.SwimmerName[Misc.if_not_null(dr["選手番号"])];
                            }
                            ExcelConnection.append(prgNo, kumi, laneNo, swimmerName);
                        }
                    }
                }


            }


        }
        private void call_showEventList()
        {
            DialogResult dr=MessageBox.Show("大会名を読み込みます","OK?",MessageBoxButtons.YesNo);
            if (dr==System.Windows.Forms.DialogResult.Yes)
            {
                this.Cursor = Cursors.WaitCursor;
                folderName = txtBxFolder.Text;
                showEventList(folderName);
                this.Cursor = Cursors.Default;
            }
            
        }
        private void btnFolderSelect_Click(object sender, EventArgs e)
        {
            
            FolderBrowserDialog dbPickDialog = new FolderBrowserDialog();
            dbPickDialog.Description = "データベースパスの選択";
    //        dbPickDialog.RootFolder = Environment.SpecialFolder.UserProfile;
            dbPickDialog.SelectedPath = folderName;
            DialogResult result = dbPickDialog.ShowDialog();
            if (result==DialogResult.OK)
            {
                folderName = dbPickDialog.SelectedPath;
                txtBxFolder.Text = folderName;
                call_showEventList();
                
            }
        }
        

        public static void Read_event_db(string dbFileName,ref string eventName, ref string eventDate, ref string eventVenue)
        {
            String connectionString = magicWord + dbFileName;

            event_db evtDB = new event_db(connectionString);
            
            {
                eventName = evtDB.get_eventName();
                eventDate = evtDB.get_eventDate();
                eventVenue = evtDB.get_eventVenue();
            }
                
        }
       
        public void showEventList(string dbPath)
        {
            string[] eventNameA = new string[100];
            string[] eventVenueA = new string[100];
            string[] eventDateA = new string[100];
            string[] fileName = new string[100];
            int counter = 0;
            int numRecords;

            int myLength;
            int[] myByteLen = new int[100];
            string eventName = "";
            string myDate = "";
            string myVenue = "";

            lbxDbContents.Items.Clear();


            try
            {
                var txtFiles = Directory.EnumerateFiles(dbPath, "*.mdb");
                int maxEventNameLength = 0;
                foreach (string currentFile in txtFiles)
                {
                    fileName[counter] = currentFile.Substring(currentFile.Length - 10);
                    try
                    {
                        Read_event_db(currentFile, ref eventName, ref myDate, ref myVenue);
                        if (eventName != null)
                        {
                            myLength = eventName.Length;
                            myByteLen[counter] = shiftjisEnc.GetByteCount(eventName);
                            if (maxEventNameLength < myByteLen[counter]) maxEventNameLength = myByteLen[counter];
                            eventNameA[counter] = eventName;
                            eventDateA[counter] = myDate;
                            eventVenueA[counter] = myVenue;
                            counter++;
                        }
                    }
                    catch (Exception e)
                    {
                                              MessageBox.Show(e.Message);
                    }
                }
                numRecords = counter - 1;
                lbxDbContents.Items.Add("File名      大会名" + new string(' ', maxEventNameLength + 2) +
                    "期間" + new string(' ', 14) + "場所");
                for (counter = 0; counter <= numRecords; counter++)
                {
                    lbxDbContents.Items.Add(fileName[counter] + "  " + eventNameA[counter] +
                        new string(' ', (maxEventNameLength + 2 - myByteLen[counter]))
                         + eventDateA[counter] + "  " + eventVenueA[counter]);
                }
            } catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void txtBxFolder_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnInitDB_Click(object sender, EventArgs e)
        {
            ExcelConnection.delete();
        }
    }
    public static class setupFileIo
    {
        private const string setupFileName = "mdb2acc.ini";
        public static void read_setup_file(ref string dbPath)
        {
            string resultDbPath="";
            dbPath = "";
            try
            {
                using (StreamReader reader = new StreamReader(setupFileName, System.Text.Encoding.GetEncoding("sjis")))
                {
                     string line = "";
                    while ((line = reader.ReadLine()) != null) { 
                        if (line == "") continue;
                        if (line.Substring(0, 1) == "#") continue;
                        string[] words = line.Split('>');
                        if (words[0]=="DBPATH")  // 入力mdb
                        {
                            dbPath = words[1];
                        }
                        if (words[0] == "RESULT") // swmresult.accdb のpath
                        {
                            resultDbPath = words[1];
                            ExcelConnection.set_mdb_file_name(resultDbPath)  ;
                            ExcelConnection.delete();
                        }
                    }
                }
            } catch (Exception e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show("Maybe " + setupFileName + " does not exist.");
            }

        }
    }
}
 


        
