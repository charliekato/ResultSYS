
using ShowLaneOrder;
using System;
using System.ComponentModel.Design;
using System.IO;
using System.Text;
using System.Windows.Forms;




namespace ResultSys
{

    public partial class Form1 : Form
    {
        string folderName;
        public static string comPort;
        private static int interval2NextRace;
        private static int lapAliveTime;

        private const int NUMSTYLE = 7;
        private const int NUMDISTANCE = 7;

        private const int TIME4DNS = 999999;
        public const int TIME4DQ = 999998;
        public const int DNS = 1;
        public const int DQ = 2;


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
        static public Form2 form2;

        public static int get_lap_alive_time() { return lapAliveTime; }
        public static void set_lap_alive_time(int newLapAliveTime) { lapAliveTime = newLapAliveTime; }
        public static int get_interval_2_next_race() { return interval2NextRace; }
        public static void set_interval_2_next_race(int newInterval2NextRace) {  interval2NextRace = newInterval2NextRace; }
        public Form1()
        {
            InitializeComponent();
            init_size(930,780);
            set_size_of_list_box(900, 520);
            setupFileIo.read_setup_file(ref folderName, ref interval2NextRace,
                ref lapAliveTime ) ;
            //MessageBox.Show("db path  " + folderName + " " + interval2NextRace + " " + lapAliveTime);
            //ExcelConnection.delete();
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



        private void btnShowLaneOrder_Click(object sender, EventArgs e)
        {

            string selectedString;
            string[] sep = { " " };

            if (lbxDbContents.SelectedItem==null)
            {
                MessageBox.Show("大会を選択してください。");
            } else
            {

                if (serial_interface.open_serial_port(comPort) )
                {
                    selectedString = lbxDbContents.SelectedItem.ToString();
                    //------------------------
                    string[] eventInfo = selectedString.Split(sep,StringSplitOptions.RemoveEmptyEntries);
                    string fullpathDBName = folderName + "\\" + eventInfo[0];

                    form2 = new Form2(fullpathDBName);
                    form2.Show();
                } else
                {
                    MessageBox.Show("設定画面でCOM PORTを指定してください。");
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
//                setupFileIo.write_setup_file(folderName);
                call_showEventList();
                
            }
        }
        

        public static void Read_event_db(string dbFileName,ref string eventName, ref string eventDate, ref string eventVenue)
        {
            String connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" +
              "Data Source="+dbFileName;

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
            if (serial_interface._serialPort != null)
            {
                serial_interface._serialPort.ReadTimeout = 1;
                serial_interface._serialPort.Close();
                serial_interface.threadStop = true;
            }
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form form3 = new frmsetup();
            form3.Show();
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
        private const string setupFileName = "ResultMonitor.ini";
        public static string logFilePath;
        public static void set_log_file_path(string logFile) { logFilePath=logFile; }
        public static void writeLog( string msg)
        {
            try
            {
                StreamWriter logFile = new StreamWriter(logFilePath,true);
                DateTime dt = DateTime.Now;
                logFile.WriteLine(dt.ToString("HH:mm:ss") + ">" + msg);
                logFile.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        

        public static void read_setup_file(ref string dbPath, ref int interval2NextRace,
            ref int lapAliveTime )
        {
            string resultDbPath;
            string cmdFile;
            try
            {
                using (StreamReader reader = new StreamReader(setupFileName, System.Text.Encoding.GetEncoding("sjis")))
                {
                     string line = "";
                    while ((line = reader.ReadLine()) != null) { 
                        if (line == "") continue;
                        if (line.Substring(0, 1) == "#") continue;
                        string[] words = line.Split('>');
                        if (words[0]=="DBPATH")
                        {
                            dbPath = words[1];
                        }
                        if (words[0]=="INTERVAL2NEXTRACE")
                        {
                            interval2NextRace = Int32.Parse(words[1]);
                        }
                        if (words[0]=="LAPALIVETIME")
                        {
                            lapAliveTime = Int32.Parse(words[1]);
                        }
                        if (words[0] == "LOGFILE")
                        {
                            setupFileIo.set_log_file_path( words[1]);
                        }
                        if (words[0] == "RESULT")
                        {
                            resultDbPath = words[1];
                            ExcelConnection.set_mdb_file_name(resultDbPath)  ;
                        }
                        if (words[0] == "CMDFILE")
                        {
                            cmdFile = words[1];
                            if (! File.Exists(cmdFile))
                            {
                                MessageBox.Show("Command File not found. Check if " + cmdFile + " exists");
                                cmdFileIo.cmdNotFound = true;
                            } else
                            {
                                cmdFileIo.set_cmd_file(cmdFile);
                            }

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
 


        
