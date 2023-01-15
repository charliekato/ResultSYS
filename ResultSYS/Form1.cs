
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;




namespace ResultSys
{

    public partial class Form1 : Form
    {
        string folderName;
        public static int interval2NextRace;
        public static int lapAliveTime;
        public const int NUMSTYLE = 7;
        public const int NUMDISTANCE = 7;

        public const int TIME4DNS = 999999;
        public const int TIME4DQ = 999998;
        public const int DNS = 1;
        public const int DQ = 2;


        static Encoding shiftjisEnc = Encoding.GetEncoding("Shift-JIS");
        public bool StopAutoRun;
        // related 大会設定
        public string EventName;
        public string EventDate;
        public string EventVenue;

        
        static public string[] Team = new string[100];
        static public int LastPrgNo;
        static public int FirstPrgNo;
        static public int MaxTeamNum=0;
        // for チームマスター
        static public string[] TeamName4Relay;
        static public Form2 form2;

        public Form1()
        {
            InitializeComponent();
            init_size(930,780);
            set_size_of_list_box(900, 520);
            setup_file_io.read_setup_file(ref ExcelConnection.mdbFile,ref folderName,ref interval2NextRace,ref lapAliveTime);
            //MessageBox.Show("db path  " + folderName + " " + interval2NextRace + " " + lapAliveTime);
            ExcelConnection.delete();
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


        static string get_file(string initFile)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = initFile;
            ofd.InitialDirectory = @"C:\Users\ykato";
            ofd.Filter = "エクセルファイル(*.xlsx)|*.xlsx";
            ofd.FilterIndex = 2;
            ofd.Title = "タイムデータが入っているエクセルファイルを選択してください";
            ofd.RestoreDirectory = true;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき、選択されたファイル名を表示する
                Console.WriteLine("> {0} <", ofd.FileName);
                return (ofd.FileName);
            }
            return (initFile);
        }

        private void btnShowLaneOrder_Click(object sender, EventArgs e)
        {

            string selectedString;
            string[] sep = { " " };

            selectedString = lbxDbContents.SelectedItem.ToString();
            //------------------------
            string[] eventInfo = selectedString.Split(sep,StringSplitOptions.RemoveEmptyEntries);
            string fullpathDBName = folderName + "\\" + eventInfo[0];
            
            
             

            form2 = new Form2(fullpathDBName);
            form2.Show();
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
            dbPickDialog.RootFolder = Environment.SpecialFolder.UserProfile;
            dbPickDialog.SelectedPath = folderName;
            DialogResult result = dbPickDialog.ShowDialog();
            if (result==DialogResult.OK)
            {
                folderName = dbPickDialog.SelectedPath;
                txtBxFolder.Text = folderName;
                setup_file_io.write_setup_file(folderName);
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
            this.Close();
        }

    }
    public class setup_file_io
    {

        
        private const string setupFileName = "swmsys.setup.sjis.txt";

        public static void write_setup_file(string dbPath )
        {
            string[] lineBuf = new string[10];
            string line;
            int lineNum = 0;
            int lastLine=0;
            try
            {

                using (StreamReader reader = new StreamReader(setupFileName, System.Text.Encoding.GetEncoding("sjis")))
                {

                    while ((line = reader.ReadLine()) != null)
                    {
                        lineBuf[lineNum] = line;
                        string[] words = line.Split('>');
                        if (words[0]=="DBPATH")
                        {
                            lineBuf[lineNum] = "DBPATH>" + dbPath;
                        }
                        lineNum++;
                    }
                }
                lastLine = lineNum;
            } catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
            try
            {
                using (StreamWriter writer = new StreamWriter(setupFileName, false, System.Text.Encoding.GetEncoding("sjis")))
                {
                    for (lineNum=0;lineNum<lastLine;lineNum++)
                    {
                        writer.WriteLine(lineBuf[lineNum]);
                    }
                }
            } catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }


        public static void read_setup_file(ref string resultDbPath, ref string dbPath,ref int interval2NextRace, ref int lapAliveTime)
        {
            try
            {
                using (StreamReader reader = new StreamReader(setupFileName, System.Text.Encoding.GetEncoding("sjis")))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
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
                    }
                }
            } catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

    }
}
 


        
