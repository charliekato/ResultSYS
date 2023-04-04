// 2023/03/07 kana deleted, lap time supported
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace ResultSys
{


    public partial class Form2 : Form
    {
        const string magicWord = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=";

        public int timerInterval = 1;
        private bool relayFlag = false;

        private int[] lapCounter = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private int[] prgNofromLane = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //private int[] kumifromLane = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //private string[] namefromLane = new string[10] { "", "", "", "", "", "", "", "", "", "" };

        
        private int[] lastLapTime = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private int[] arrivalOrder = new int[10] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };




        private event_db evtDB;
        private program_db prgDB;
        private class_db clsDB;
        private team_db tmDB;
        private swimmer_db swmDB;
        private record_db rcdDB;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer timerL0;
        private System.Windows.Forms.Timer timerL1;
        private System.Windows.Forms.Timer timerL2;
        private System.Windows.Forms.Timer timerL3;
        private System.Windows.Forms.Timer timerL4;
        private System.Windows.Forms.Timer timerL5;
        private System.Windows.Forms.Timer timerL6;
        private System.Windows.Forms.Timer timerL7;
        private System.Windows.Forms.Timer timerL8;
        private System.Windows.Forms.Timer timerL9;
        private EventHandler ev1;
        private EventHandler evl1, evl2, evl3, evl4, evl5, evl6, evl7, evl8, evl9, evl0;
        private bool monitorEnable;
        private int maxLaneNo;
        private string dbfilename;
        private Label mylabel;
        private int FirstPrgNo;
        private int LastPrgNo;
        // for label (mainly lane label) position...
        private Thread readThread=null;
        
        private timeData tmd;

        public static int[] occupied = new int[10];
        private int[] uidFromLane = new int[10];


        public bool enableAutoRun = true;

        private void init_lap_counter()
        {

            int ix;
            for ( ix=1; ix<10; ix++)
            {
                lapCounter[ix] = 0;

            }
        }
        public int get_lap_interval()
        {
            return 2 * evtDB.get_pool_length();
        }
        public int how_many_laps(int uid)
        {
            int distance = Convert.ToInt32(program_db.get_distance_from_uid(uid).Substring(0, 4));
            int laps = distance / get_lap_interval();
            return laps;
        }
        public void set_program_number(int prgNo)
        {
            this.tbxPrgNo.Text = align(prgNo);
        }
        public int get_program_number()
        {
            return Convert.ToInt32(this.tbxPrgNo.Text);
        }
        public void set_kumi_number(int kumi)
        {
            this.tbxKumi.Text = align2(kumi);
        }
        public int get_kumi_number()
        {
            return Convert.ToInt32(this.tbxKumi.Text);
        }



        public Form2(string filename)
        {
            InitializeComponent();
            this.dbfilename = filename;
            string connectionString = magicWord + filename;
            result_db.set_connectionString(connectionString);

            evtDB = new event_db(connectionString);
            prgDB = new program_db(connectionString);
            clsDB = new class_db(connectionString);
            tmDB = new team_db(connectionString);
            swmDB = new swimmer_db(connectionString);
            rcdDB = new record_db(connectionString);
            maxLaneNo = event_db.get_max_lane_number();


            setup_file_io.writeLog("Form2 started.");

            init_form2();

        }


        private void btnQuit_Click(object sender, EventArgs e)
        {
            stop_monitor();
            if (serial_interface._serialPort != null)
            {
                serial_interface._serialPort.ReadTimeout = 1;
                serial_interface._serialPort.Close();
                serial_interface.threadStop = true;
            }
            this.Close();

        }

        private void layout_header_label()
        {
            Font myFont = new Font("MS UI Gothic", 13*Width/1200);
            Controls["lblPrgNo"].Left = 10*Width/1200;
            Controls["lblPrgNo"].Top = 3*Width/1200;
            //            Controls["lblPrgNo"].Width = 25;
            Controls["lblPrgNo"].Height = 20*Width/1200;
            Controls["lblPrgNo"].Font = myFont;

            Controls["tbxPrgNo"].Width = 60*Width/1200;
            Controls["tbxPrgNo"].Height = 22*Width/1200;
            Controls["tbxPrgNo"].Left = 5*Width/1200;
            Controls["tbxPrgNo"].Top = 25*Width/1200;
            Controls["tbxPrgNo"].Font = myFont;
            Controls["lblHyphen"].Left = 75*Width/1200;
            Controls["lblHyphen"].Top = 25*Width/1200;
            Controls["lblHyphen"].Height = 25*Width/1200;
            Controls["lblHyphen"].Font = myFont;
            Controls["lblKumi"].Height = 20*Width/1200;
            Controls["lblKumi"].Left = 95*Width/1200;
            Controls["lblKumi"].Top = 3*Width/1200;
            Controls["lblKumi"].Font = myFont;
            Controls["tbxKumi"].Width = 40*Width/1200;
            Controls["tbxKumi"].Height = 22*Width/1200;
            Controls["tbxKumi"].Left = 95*Width/1200;
            Controls["tbxKumi"].Top = 25*Width/1200;
            Controls["tbxKumi"].Font = myFont;

            
        }
        private void layout_button()
        {
            int top = 5*Width/1200;
            int left = 150*Width/1200;
            int width = 40*Width/1200;
            int height = 30*Width/1200;
            int space = 4*Width/1200;
            Controls["btnShow"].Left = left;
            Controls["btnShow"].Top = top;
            Controls["btnShow"].Height = height;
            Controls["btnShow"].Width = width;
            Controls["btnShowPrev"].Left = left + width + space;
            Controls["btnShowPrev"].Top = top;
            Controls["btnShowPrev"].Height = height;
            Controls["btnShowPrev"].Width = width;
            Controls["btnShowNext"].Left = 2 * (width + space) + left;
            Controls["btnShowNext"].Top = top;
            Controls["btnShowNext"].Height = height;
            Controls["btnShowNext"].Width = width;
            Controls["lblRaceName0"].Top = 45*Width/1200;
            Controls["lblRaceName0"].Left = /*2 * (width + space) +*/ left;
        }
        private void layout_label()
        {

            const int STDWIDTH = 1200;
            const int STDHEIGHT = 800;
            int topMargin = this.Height / 12;  // 88

            int buttomMargin = this.Height / 15; //53
            int leftMargin = this.Width / 80;     //15
            int rightMargin = this.Width / 100;     //12 
            int laneNoWidth = this.Width / 20;   //60
            int nameWidth = (this.Width - leftMargin - rightMargin - laneNoWidth) / 4; //220
            int relayNameWidth = nameWidth - 5;
            int shozokuWidth = nameWidth - 5;
            int relaySwimmerWidth = nameWidth * 5 / 10;
            int laneHeight = (this.Height - topMargin - buttomMargin) / maxLaneNo;
            int kanaHeight = 21*Height/STDHEIGHT; // laneHeight / 5;
            int raceNameHeight = 14*Height/STDHEIGHT; // laneHeight * 3 / 10;
            //      laneHeight = 100;
            int fontSize = 30*this.Width/STDWIDTH; // was  30
            //int fontSizeKana = 12*Width/STDWIDTH;
            int timeWidth = fontSize * 5;
            int fontSize4Rly = 16*Width/STDWIDTH;
            int fontSizeShozoku = 22*Width/STDWIDTH;
            int fontsize4relayTeam = 24*Width/STDWIDTH;
            int fontsize4RaceName = 17 * Width / STDWIDTH;
            const string fontName = "MS UI Gothic";
            Font nameFont = new Font(fontName, fontSize); // was 23
            Font relayTeamFont = new Font(fontName, fontsize4relayTeam);
            Font shozokuFont = new Font(fontName, fontSizeShozoku);
            //Font kanaFont = new Font(fontName, fontSizeKana); // was 11
            Font raceNameFont = new Font(fontName, fontsize4RaceName);
            Font smallNameFont = new Font(fontName, fontSize4Rly);
            int halfLaneHeight = fontSize + 4;  // was laneHeight/2

            int laneNo;

            for (laneNo = 1; laneNo <= maxLaneNo; laneNo++)
            {
                int xpos = leftMargin;
                int yposr = topMargin + (laneHeight * (laneNo - 1))+2;
                int yposk = yposr + raceNameHeight;
                int ypos = yposk + kanaHeight;
                create_lblName("lblLane", laneNo, xpos, ypos, nameFont, "" + laneNo + ".");
                create_lblName("lblRaceName", laneNo, xpos, yposr, raceNameFont, "");
                xpos = xpos + laneNoWidth;
                create_lblName("lblName", laneNo, xpos, ypos, nameFont, "");
                create_lblName("lblRealyTeamName", laneNo, xpos, ypos, relayTeamFont);
                //create_lblName("lblKana", laneNo, xpos + 15, yposk, kanaFont, "");
                xpos += nameWidth;
                create_lblName("lblShozoku", laneNo, xpos, ypos + 3, shozokuFont, "");
                int xpos4relay = xpos - 5;
                xpos += nameWidth;
                create_lblName("lblArrivalOrder", laneNo, xpos,ypos, nameFont, "");
                xpos += fontSize * 2;
                create_lblName("lblTime", laneNo, xpos, ypos, nameFont, "");
                create_lblName("lblLapTime", laneNo, xpos-(fontSize/2), ypos + halfLaneHeight, nameFont, "");//2023/03/07
                create_lblName("lblNewRecord", laneNo, xpos + timeWidth, ypos, nameFont, "");
                for (int j = 1; j < 5; j++)
                {
                    create_lblName("lblRelaySwimmer" + j, laneNo, xpos4relay, ypos + 5, smallNameFont, "");
                    //create_lblName("lblRelaySwimmerKana" + j, laneNo, xpos4relay, yposk + 5, kanaFont, "");
                    xpos4relay += relaySwimmerWidth;
                }

                create_lblName("lblArrivalOrder4Relay", laneNo, xpos4relay, ypos, nameFont, "");
                xpos4relay += fontSize * 2;
                create_lblName("lblTime4Relay", laneNo, xpos4relay, ypos, nameFont, "");
                create_lblName("lblLapTime4Relay", laneNo, xpos4relay-(fontSize/2), ypos+halfLaneHeight, nameFont, "");//2023/03/07
                create_lblName("lblNewRecord4Relay", laneNo, xpos4relay+timeWidth, ypos, nameFont, "");

            }

        }

        private void create_lblName(string head, int laneNo, int x, int y, Font myFont, string txt = "")
        {

            this.mylabel = new Label();
            this.mylabel.AutoSize = true;
            this.mylabel.Location = new System.Drawing.Point(x, y);
            this.mylabel.Name = head + laneNo;
            //this.mylabel.Size = new System.Drawing.Size(w, h);
            // this.mylabel.TabIndex = 19;
            this.mylabel.Text = txt;
            this.mylabel.Font = myFont;
            this.Controls.Add(this.mylabel);
        }

        public void show_swimmer_name(int laneNo, int swimmerID)
        {
            if (swimmerID == 0)
            {
                this.Controls["lblName" + laneNo].Text = "";
                //this.Controls["lblKana" + laneNo].Text = "";
                this.Controls["lblShozoku" + laneNo].Text = "";
            } else {
                this.Controls["lblName" + laneNo].Text = swmDB.get_name(swimmerID);
                //this.Controls["lblKana" + laneNo].Text = swmDB.get_furigana(swimmerID);
                this.Controls["lblShozoku" + laneNo].Text = swmDB.get_team_name(swimmerID);
            }

        }
        public void show_relay_team(int laneNo, int teamID, int s1, int s2, int s3, int s4)
        {
            this.Controls["lblName" + laneNo].Text = tmDB.get_name(teamID);
            //this.Controls["lblKana" + laneNo].Text = "";
            this.Controls["lblRelaySwimmer1" + laneNo].Text = swmDB.get_name(s1);
            this.Controls["lblRelaySwimmer2" + laneNo].Text = swmDB.get_name(s2);
            this.Controls["lblRelaySwimmer3" + laneNo].Text = swmDB.get_name(s3);
            this.Controls["lblRelaySwimmer4" + laneNo].Text = swmDB.get_name(s4);
            //this.Controls["lblRelaySwimmerKana1" + laneNo].Text = swmDB.get_furigana(s1);
            //this.Controls["lblRelaySwimmerKana2" + laneNo].Text = swmDB.get_furigana(s2);
            //this.Controls["lblRelaySwimmerKana3" + laneNo].Text = swmDB.get_furigana(s3);
            //this.Controls["lblRelaySwimmerKana4" + laneNo].Text = swmDB.get_furigana(s4);
        }
        public void show_reason_and_set_occupied(int laneNo, int reason_code)
        {
            if (reason_code == 0)
            {
                occupied[laneNo] = 1;
                return;
            }
            if (reason_code == 1)
            {
                Controls["lblTime" + laneNo].Text = " 棄権";
                Controls["lblTime" + laneNo].BackColor = Color.FromArgb(240, 240, 240);
            }
            if (reason_code == 2)
            {
                Controls["lblTime" + laneNo].Text = " 失格";
                Controls["lblTime" + laneNo].BackColor = Color.FromArgb(240, 240, 240);
            }
        }
        public void show_reason4Relay_and_set_occupied(int laneNo, int reason_code)
        {
            if (reason_code == 0)
            {
                occupied[laneNo] = 1;
                return;
            }
            if (reason_code == 1) Controls["lblTime4Relay" + laneNo].Text = " 棄権";
            if (reason_code == 2) Controls["lblTime4Relay" + laneNo].Text = " 失格";
        }
        private string get_race_name(int uid)
        {
            string returnStr = string.Empty;
            returnStr= ""+program_db.get_class_from_uid(uid) + program_db.get_gender_from_uid(uid) + " " ;
            returnStr +=   program_db.get_distance_from_uid(uid) + program_db.get_shumoku_from_uid(uid) + " " + program_db.get_phase_from_uid(uid);
            return returnStr;


        }
        private void show_header(int uid, int prgNo, int laneNo)
        {

            Controls["lblRaceName" + laneNo].Text = "No." + prgNo + "   " + get_race_name(uid);

        }

        private void set_tooltip2()
        {
            toolTip2.SetToolTip(cbxMonitorEnable, "結果(タイム)取り込みする場合は\n" +
                    "このcheckを入れる。\n");
        }
        private void set_monitor_checkbox()
        {
            int boxSize = 10* Width/1200;
            cbxMonitorEnable.Visible = true;
            cbxMonitorEnable.Location = new Point(this.Width - (280*Width/1200), (5*Width/1200));
            cbxMonitorEnable.Size = new Size(boxSize, boxSize);
            cbxMonitorEnable.Show();

        }
        private void set_lblPortNo()
        {
            lblPortNo.Location = new Point(this.Width - (300*Width/1200), (25*Width/1200));
            lblPortNo.Font = new Font("MS UI Gothic", 12*Width/1200);
            lblPortNo.Show();
            lblPortNo.Visible = false;
        }

        private void set_cmbBox()
        {
            cmbBox.Location = new Point(this.Width - 250*Width/1200, 25*Width/1200);
            set_portNO_to_combobox();
            cmbBox.Show();
            cmbBox.Visible = false;

        }


        private void set_txtBoxTimer()
        {

        }

        private void set_quit_button()
        {
            btnQuit.Location = new Point(this.Width - 65*Width/1200, 10);
            btnQuit.Size = new Size(55*Width/1200, 25*Width/1200);
            btnQuit.Show();
            btnQuit.Font = new Font("MS UI Gothic", 12*Width/1200);
        }
        private void set_start_button()
        {
            btnStart.Location = new Point(this.Width - 125*Width/1200, 10);
            btnStart.Size = new Size(55*Width/1200, 25*Width/1200);
            btnStart.Font = new Font("MS UI Gothic", 12*Width/1200);
            btnStart.Show();
            btnStart.Visible = false;
        }
        private void set_lbl2xpoolLength()
        {
            lbl2xpoolLength.Location = new Point(this.Width - 60*Width/1200, 38*Width/1200);
            lbl2xpoolLength.Size = new Size(40*Width/1200, 25*Width/1200);
            lbl2xpoolLength.Show();
            lbl2xpoolLength.Font = new Font("MS UI Gothic", 12*Width/1200);
            lbl2xpoolLength.Text = Convert.ToString(get_lap_interval());
        }
        private void set_lblLapInterval()
        {
            lblLapInterval.Location = new Point(this.Width - 142*Width/1200, 40*Width/1200);

            lblLapInterval.Show();
            lblLapInterval.Font = new Font("MS UI Gothic", 12*Width/1200);
        }

        private void set_portNO_to_combobox()
        {
            foreach (string s in SerialPort.GetPortNames())
            {
                cmbBox.Items.Add(s);
            }

        }
//        private void disable_monitor()
//        {
//            lblPortNo.Visible = false;
//            cmbBox.Visible = false;
//        }
        private void init_form2()
        {
            const int outerMarginX = 0;
            const int outerMarginY = 0;
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - outerMarginX;
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - outerMarginY;

            this.Text = evtDB.get_eventName() + " " + evtDB.get_eventDate() + " " + evtDB.get_eventVenue();
            //            MessageBox.Show(" width: " + Width + "   Height: " + Height);
            // in my surface   width=1196 height=791

            layout_header_label();
            layout_label();
            layout_button();
            set_monitor_checkbox();
            set_cmbBox();
            set_lblPortNo();
            set_txtBoxTimer();
            set_tooltip2();
            set_quit_button();
            set_start_button();
            set_lblLapInterval();
            set_lbl2xpoolLength();
            set_program_number(1);
            set_kumi_number(1);
            FirstPrgNo = 1;

            show_lane_order();
        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }


        private void show_lane_order()
        {
            int prgNo;
            int kumi;
            int uid;
            int maxPrgNo = program_db.get_max_program_no();

            init_array(occupied);
            init_array(lastLapTime);
            prgNo = get_program_number();
            kumi = get_kumi_number();
            if (prgNo > maxPrgNo)
            {
                MessageBox.Show("該当のレースはありません。最終レースを表示します。");
                prgNo = maxPrgNo;
                kumi = 1;
                uid = program_db.get_uid_from_prgno(prgNo);
                while (result_db.race_exist(uid, kumi)) kumi++;
                kumi--;
                set_program_number(prgNo);
                set_kumi_number(kumi);
            }
            uid = program_db.get_uid_from_prgno(prgNo);
            if (!result_db.race_exist(uid, kumi)) MessageBox.Show("該当のレースはありません。");
            else
            {
                if (kumi == 1)
                {
                    uid = program_db.get_uid_from_prgno(prgNo);
                    while (mdb_interface.can_go_with_prev(uid, kumi))
                    {
                        program_db.dec_race_number(ref prgNo);
                        uid = program_db.get_uid_from_prgno(prgNo);
                    }
                }
            }
            set_program_number(prgNo);
            set_kumi_number(kumi);
            show();
        }
        private static bool already_occupied(int[] array, int laneNo) { return (array[laneNo] > 0); }

        private void clear_lane_info()
        {
            for (int lane = 1; lane <= maxLaneNo; lane++)
            {

                clear_time(lane);
                Controls["lblRaceName" + lane].Text = "";
                Controls["lblName" + lane].Text = "";
                Controls["lblShozoku" + lane].Text = "";
                //Controls["lblKana" + lane].Text = "";
                for (int j = 1; j < 5; j++)
                {
                    Controls["lblRelaySwimmer" + j + lane].Text = "";
                    //Controls["lblRelaySwimmerKana" + j + lane].Text = "";
                }
                Controls["lblNewRecord4Relay" + lane].Text = "";
                Controls["lblNewRecord" + lane].Text = "";
            }
        }

        public void show()
        {
            int prgNo = get_program_number();
            int kumi = get_kumi_number();
            int[] swimmerID = new int[10];
            int uid = program_db.get_uid_from_prgno(prgNo);
            int prevUID = uid;
            string connectionString = magicWord + dbfilename;


            LastPrgNo = prgNo;
            FirstPrgNo = prgNo;
            show_header(uid, prgNo, 0);

            int laneNo;
            int lastOccupiedLane = 0;
            bool togetherflag = false;

            int first_lane = 0;
            clear_lane_info();
            do
            {
                OleDbConnection conn = new OleDbConnection(connectionString);
                using (conn)
                {
                    String sql = "SELECT UID, 選手番号, 組, 水路, 事由入力ステータス, " +
                                        "第１泳者, 第２泳者, 第３泳者, 第４泳者 " +
                                        "FROM 記録マスター WHERE 組=" + kumi + " AND UID=" + uid + ";";
                    OleDbCommand comm = new OleDbCommand(sql, conn);
                    conn.Open();
                    prgNo = program_db.get_race_number_from_uid(uid);
                    using (var dr = comm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            laneNo = Convert.ToInt32(dr["水路"]);
                                
                            //if (dr["選手番号"] == DBNull.Value) continue;
                            if (laneNo > 9) continue;
                            swimmerID[laneNo] = misc.if_not_null(dr["選手番号"]);
                            if (swimmerID[laneNo] > 0)
                            {
                                prgNofromLane[laneNo] = prgNo;
                                if (first_lane == 0) first_lane = laneNo;

                                if (prevUID != uid) {  //change header
                                    togetherflag = true;
                                    show_header(uid, prgNo, laneNo);
                                    prevUID = uid;
                                }
                                uidFromLane[laneNo] = uid;

                                ExcelConnection.program_append(prgNo, program_db.get_class_from_uid(uid),program_db.get_gender_from_uid(uid),
                                    program_db.get_distance_from_uid(uid),program_db.get_shumoku_from_uid(uid), program_db.get_phase_from_uid(uid));

                                if (program_db.is_relay(uid))
                                {
                                    relayFlag = true;
                                    ExcelConnection.append( prgNo, kumi, laneNo, tmDB.get_name(swimmerID[laneNo]));
                                    show_relay_team(laneNo, swimmerID[laneNo], misc.if_not_null(dr["第１泳者"]),
                                      misc.if_not_null(dr["第２泳者"]), misc.if_not_null(dr["第３泳者"]), misc.if_not_null(dr["第４泳者"]));
                                    show_reason4Relay_and_set_occupied(laneNo, Convert.ToInt32(dr["事由入力ステータス"]));
                                } else
                                {
                                    relayFlag = false;
                                    show_swimmer_name(laneNo, swimmerID[laneNo]);
                                    ExcelConnection.append( prgNo, kumi, laneNo, swmDB.get_name(swimmerID[laneNo]));
                                    show_reason_and_set_occupied(laneNo, Convert.ToInt32(dr["事由入力ステータス"]));
                                }
                                lastOccupiedLane = laneNo;
                            }
                        }
                    }
                }
                if (result_db.race_exist(uid, kumi + 1)) break;
                prevUID = uid;
                uid = program_db.get_next_uid(uid);

            } while (can_go_with_next(uid, prevUID, kumi, lastOccupiedLane));
            LastPrgNo = prgNo;
            if (togetherflag)
            {
                Controls["lblRaceName" + first_lane].Text = Controls["lblRaceName0"].Text;
                Controls["lblRaceName0"].Text = "合同レース";
            }
        }
        private static bool can_go_with_next(int uid, int prevUID, int kumi, int prevLastLane)
        {
            if (kumi > 1) return false;

            if (!program_db.is_same_distance_style(uid, prevUID)) return false;
            //          if (result_db.race_exist(uid, 2)) return false;
            if (result_db.get_first_occupied_lane(uid, 1) <= prevLastLane) return false;
            return true;
        }

        private void btnShow_click(object sender, EventArgs e)
        {
            show_lane_order();
        }

        private void btnShowPrev_Click(object sender, EventArgs e)
        {
            int prgNo = get_program_number();
            int kumi = get_kumi_number();
            result_db.get_prev_race(ref prgNo, ref kumi);

            set_program_number(prgNo);
            set_kumi_number(kumi);
            show_lane_order();
        }
        public void show_next_race(object sender, EventArgs e)
        {
            int prgNo = LastPrgNo;
            int kumi = get_kumi_number();

            if (timer != null)
                timer.Tick -= ev1;
            while (true)
            {
                int uid = program_db.get_uid_from_prgno(prgNo);
                kumi++;
                if (result_db.race_exist(uid, kumi))
                {
                    set_program_number(program_db.get_race_number_from_uid(uid));
                    set_kumi_number(kumi);
                    show_lane_order();
                    return;
                }
                if (program_db.inc_race_number(ref prgNo) == false)
                {
                    //stop_monitor();
                    MessageBox.Show("最終レースです。");
                    return;
                }

                kumi = 0;
            }
        }
        private void btnShowNext_Click(object sender, EventArgs e)
        {
            show_next_race(sender, e);


        }
        private string align(int n)
        {
            if (n < 10) return "  " + n;
            if (n < 100) return " " + n;
            return "" + n;
        }
        private string align2(int n)
        {
            if (n < 10) return " " + n;

            return "" + n;
        }
        private void write_time(string goalTime, int laneNo,ushort orderOfArrival)
        {
            if (relayFlag)
            {
                Controls["lblTime4Relay" + laneNo].Text = goalTime + "  Fin";
                Controls["lblArrivalOrder4Relay" + laneNo].Text = "" + orderOfArrival;
            } else
            {
                Controls["lblArrivalOrder" + laneNo].Text = ""+orderOfArrival;
                Controls["lblTime" + laneNo].Text = goalTime + "  Fin";
            }

        }
        
        private void write_time(string goalTime, int laneNo, int distance)
        {
            if (relayFlag)
            {
                Controls["lblTime4Relay" + laneNo].Text = goalTime + "  " + distance + "m";
            } else
            {
                Controls["lblTime" + laneNo].Text = goalTime + "  " + distance + "m";
            }
            enable_timer(laneNo);
        }
        private void write_lap(int laptime,int laneNo)
        {
            if (relayFlag)
            {
                Controls["lblLapTime4Relay" + laneNo].Text = "(" +
                    misc.timeint2str(laptime) + ")";
            } else
            {
                Controls["lblLapTime"+laneNo].Text = "("+
                    misc.timeint2str(laptime)+ ")";
            }
        }
        private void clear_time(int laneNo)
        {
            Controls["lblTime" + laneNo].Text = "";
            Controls["lblTime4Relay" + laneNo].Text = "";
            Controls["lblLapTime" + laneNo].Text = "";
            Controls["lblLapTime4Relay" + laneNo].Text = "";
            Controls["lblArrivalOrder" + laneNo].Text = "";
            Controls["lblArrivalOrder4Relay" + laneNo].Text = "";
        }
        private void erase_lane0(object s, EventArgs e)
        {
            timerL0.Tick -= evl0;
            timerL0.Enabled = false;
            clear_time(0);
        }
        private void erase_lane1(object s, EventArgs e)
        {
            timerL1.Tick -= evl1;
            timerL1.Enabled = false;
            clear_time(1);
        }
        private void erase_lane2(object s, EventArgs e)
        {
            timerL2.Tick -= evl2;
            timerL2.Enabled = false;
            clear_time(2);
        }
        private void erase_lane3(object s, EventArgs e)
        {
            timerL3.Tick -= evl3;
            timerL3.Enabled = false;
            clear_time(3);
        }
        private void erase_lane4(object s, EventArgs e)
        {
            timerL4.Tick -= evl4;
            timerL4.Enabled = false;
            clear_time(4);
        }
        private void erase_lane5(object s, EventArgs e)
        {
            timerL5.Tick -= evl5;
            timerL5.Enabled = false;
            clear_time(5);
        }
        private void erase_lane6(object s, EventArgs e)
        {
            timerL6.Tick -= evl6;
            timerL6.Enabled = false;
            clear_time(6);
        }
        private void erase_lane7(object s, EventArgs e)
        {
            timerL7.Tick -= evl7;
            timerL7.Enabled = false;
            clear_time(7);
        }
        private void erase_lane8(object s, EventArgs e)
        {
            timerL8.Tick -= evl8;
            timerL8.Enabled = false;
            clear_time(8);
        }
        private void erase_lane9(object s, EventArgs e)
        {
            timerL9.Tick -= evl9;
            timerL9.Enabled = false;
            clear_time(9);
        }
        private void enable_timer(int laneNo)
        {
            if (laneNo==0)
            {
                timerL0.Tick += evl0;
                timerL0.Enabled = true;
            }
            if (laneNo==1)
            {
                timerL1.Tick += evl1;
                timerL1.Enabled = true;
            }
            if (laneNo==2)
            {
                timerL2.Tick += evl2;
                timerL2.Enabled = true;
            }
            if (laneNo==3)
            {
                timerL3.Tick += evl3;
                timerL3.Enabled = true;
            }
            if (laneNo==4)
            {
                timerL4.Tick += evl4;
                timerL4.Enabled = true;
            }
            if (laneNo==5)
            {
                timerL5.Tick += evl5;
                timerL5.Enabled = true;
            }
            if (laneNo==6)
            {
                timerL6.Tick += evl6;
                timerL6.Enabled = true;
            }
            if (laneNo==7)
            {
                timerL7.Tick += evl7;
                timerL7.Enabled = true;
            }
            if (laneNo==8)
            {
                timerL8.Tick += evl8;
                timerL8.Enabled = true;
            }
            if (laneNo==9)
            {
                timerL9.Tick += evl9;
                timerL9.Enabled = true;
            }
        }

        private void init_array(int[] array)
        {
            int ix;
            for (ix = 0; ix < 10; ix++)
            {
                array[ix] = 0;
            }
        }
        private bool is_race_comp()
        {
            int laneNo;
            int temp;
            for (laneNo = 1; laneNo < 10; laneNo++)
            {
                temp = occupied[laneNo];
                if ((occupied[laneNo] == 1) && (lane_monitor.Is_goal(laneNo) == false))
                {
                    return false;
                }
            }
            return true;
        }

        private void tbxKumi_TextChanged(object sender, EventArgs e)
        {

        }

        private int calculate_arrival_order(int timeint)
        {
            int laneNo;
            for (laneNo = 1;laneNo<=maxLaneNo; laneNo++)
            {
                //
            }
            return 0;
        }
        public async void serial_read()
        {
            int timeint = 111111;
            int laneNo = 0;
            int goal = 0;
            int arrivalOrder;
            int lapinterval = evtDB.Get_lap_interval();
            tmd = serial_interface.tmd;

            while (true)
            {
                if (!monitorEnable) break;
                bool rc;
                rc = tmd.pop(ref timeint, ref laneNo, ref goal);
                if (rc)
                {
                    if (timeint==0)
                    {
                        // do nothing so far...
                        setup_file_io.writeLog("line868: reset comes.");
                        
                    }
                    else
                    {
                        lapCounter[laneNo]++;
                        string mytime = misc.timeint2str(timeint);
                        
                        string distance;
                        int intDistance = lapCounter[laneNo] * lapinterval;
                        distance = "" +intDistance + "m";
                        ExcelConnection.insert_time(prgNofromLane[laneNo], get_kumi_number(), laneNo, mytime, distance);
                        arrivalOrder=calculate_arrival_order(timeint);
                        if (goal>=1)
                        { 
                            write_time(mytime, laneNo, orderOfArrival: (ushort) goal);
                            if (lastLapTime[laneNo]>0)
                            {
                                write_lap(misc.substract_time(timeint, lastLapTime[laneNo]),laneNo);
                            }
                            lane_monitor.Set_goal(laneNo);
                            ExcelConnection.insert_time(prgNofromLane[laneNo], get_kumi_number(), laneNo,mytime);
                            lapCounter[laneNo] = 0;
                        } else
                        {
                            write_time(mytime, laneNo, lapCounter[laneNo] * lapinterval);
                        }

                        lastLapTime[laneNo] = timeint;
                    }
                }
                await Task.Delay(300);
                if (is_race_comp())
                {
                    lane_monitor.init_lane_monitor();
                    init_lap_counter();
                    timer.Tick += ev1;
                    timer.Interval = Form1.interval2NextRace;
                    timer.Enabled = true;
                }
            }
                    ///---check---
           
        }



        public bool is_occupied(int laneNo)
        {
            int prgNo = get_program_number();
            int kumi = get_kumi_number();
            int swimmerID;

            string connectionString = magicWord + dbfilename;
            bool rc = false;
            int uid = program_db.get_uid_from_prgno(prgNo);
            OleDbConnection conn = new OleDbConnection(connectionString);
            using (conn)
            {
                String sql = "SELECT UID, 選手番号, 組, 水路, 事由入力ステータス,ゴール " +
                                    "FROM 記録マスター WHERE 組=" + kumi + " AND UID=" + uid +
                                    " AND 水路=" + laneNo + ";";
                OleDbCommand comm = new OleDbCommand(sql, conn);
                conn.Open();

                using (OleDbDataReader dr = comm.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        swimmerID = misc.if_not_null(dr["選手番号"]);
                        if (swimmerID > 0)
                        {
                            if (Convert.ToInt32(dr["事由入力ステータス"]) == 0)
                            {
                                rc = true;
                            }
                        }
                    }
                }
            }
            return rc;

        }
        public bool show_record()
        {
            int prgNo = get_program_number();
            int kumi = get_kumi_number();
            int swimmerID;

            string connectionString = magicWord + dbfilename;
            bool rc = true;
            int laneNo;

            while (prgNo <= LastPrgNo)
            {
                int uid = program_db.get_uid_from_prgno(prgNo);
                OleDbConnection conn = new OleDbConnection(connectionString);
                using (conn)
                {
                    String sql = "SELECT UID, 選手番号, 組, 水路, 事由入力ステータス,ゴール " +
                                        "FROM 記録マスター WHERE 組=" + kumi + " AND UID=" + uid + ";";
                    OleDbCommand comm = new OleDbCommand(sql, conn);
                    conn.Open();

                    using (OleDbDataReader dr = comm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            laneNo = Convert.ToInt32(dr["水路"]);
                            if (laneNo > 10) continue;
                            swimmerID = misc.if_not_null(dr["選手番号"]);
                            if (swimmerID > 0)
                            {
                                if (Convert.ToInt32(dr["事由入力ステータス"]) == 0)
                                {
                                    if (dr["ゴール"] == DBNull.Value) rc = false;
                                    else
                                    {
                                        string goalTime = Convert.ToString(dr["ゴール"]);
                                        if (goalTime == "") rc = false;
                                        else
                                        {
                                            bool newRecord = (misc.timestr2int(goalTime) < program_db.bestRecord[uid]);
                                            if (program_db.is_relay(uid))
                                            {
                                                Controls["lblTime4Relay" + laneNo].Text = goalTime;
                                                if (newRecord) show_new_record("lblNewRecord4Relay" + laneNo);

                                            } else
                                            {
                                                Controls["lblTime" + laneNo].Text = goalTime;
                                                if (newRecord) show_new_record("lblNewRecord" + laneNo);
                                            }
                                        }
                                    }


                                }
                            }
                        }
                    }
                }
                program_db.inc_race_number(ref prgNo);
            }
            return rc;
        }
        public void show_new_record(string lblname)
        {
            Controls[lblname].Text = "大会新";
            Controls[lblname].BackColor = Color.Pink;
        }


        private void stop_monitor()
        {
            Controls["btnStart"].Text = "開始";
            monitorEnable = false;
            //----from here 10/22
            //serial_interface.threadStop = true;
            //readThread = null;

        }

        private void set_labeltimer()
        {
            /////----------------------------------lapAliveTime
            int dispTime = Form1.lapAliveTime;

            timerL0 = new System.Windows.Forms.Timer();
            timerL1 = new System.Windows.Forms.Timer();
            timerL2 = new System.Windows.Forms.Timer();
            timerL3 = new System.Windows.Forms.Timer();
            timerL4 = new System.Windows.Forms.Timer();
            timerL5 = new System.Windows.Forms.Timer();
            timerL6 = new System.Windows.Forms.Timer();
            timerL7 = new System.Windows.Forms.Timer();
            timerL8 = new System.Windows.Forms.Timer();
            timerL9 = new System.Windows.Forms.Timer();
            timerL0.Interval = dispTime; // should be updated to 5000 or so.
            timerL1.Interval = dispTime; // should be updated to 5000 or so.
            timerL2.Interval = dispTime; // should be updated to 5000 or so.
            timerL3.Interval = dispTime; // should be updated to 5000 or so.
            timerL4.Interval = dispTime; // should be updated to 5000 or so.
            timerL5.Interval = dispTime; // should be updated to 5000 or so.
            timerL6.Interval = dispTime; // should be updated to 5000 or so.
            timerL7.Interval = dispTime; // should be updated to 5000 or so.
            timerL8.Interval = dispTime; // should be updated to 5000 or so.
            timerL9.Interval = dispTime; // should be updated to 5000 or so.
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (readThread==null)
            {
                readThread = new Thread(serial_interface.readandFifoPush);
                readThread.Start();
            }
            if (!monitorEnable)
            {

                Controls["btnStart"].Text = "停止";
                monitorEnable = true;
                serial_interface.init_serial_port(cmbBox.Text);
                timer = new System.Windows.Forms.Timer();
                set_labeltimer();
                timer.Interval = Form1.interval2NextRace;
                timer.Enabled = false;
                ev1 = new EventHandler(show_next_race);
                register_event();
                //readThread.Start();
                serial_interface.threadPause = false;
                serial_read();

            } else
            {
                stop_monitor();
                serial_interface.threadPause = true;
                
            }
        }

        private void register_event()
        {
            evl1 = new EventHandler(erase_lane1);
            evl2 = new EventHandler(erase_lane2);
            evl3 = new EventHandler(erase_lane3);
            evl4 = new EventHandler(erase_lane4);
            evl5 = new EventHandler(erase_lane5);
            evl6 = new EventHandler(erase_lane6);
            evl7 = new EventHandler(erase_lane7);
            evl8 = new EventHandler(erase_lane8);
            evl9 = new EventHandler(erase_lane9);
            evl0 = new EventHandler(erase_lane0);
        }
        private void cbxMonitorEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxMonitorEnable.Checked) {
                lblPortNo.Visible = true;
                cmbBox.Visible = true;
                btnStart.Visible = true;
            } else
            {
                lblPortNo.Visible = false;
                cmbBox.Visible = false;
                btnStart.Visible = false;

            }


        }
    }
    public static class serial_interface {
        public static bool threadStop = false;
        public static bool threadPause = false; // Oct.22
        public static SerialPort _serialPort=(SerialPort)null;

        public static timeData tmd=new timeData();
        public static void init_serial_port(string portName)
        {
            if (_serialPort==null)
            {
                _serialPort = new SerialPort();
                _serialPort.PortName = portName;
                _serialPort.BaudRate = 9600;
                _serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), "Even");
                _serialPort.DataBits = 7;
                _serialPort.ReadTimeout = -1;
                _serialPort.Open();


            }
        }

        public static bool is_start(byte[] timedt)
        {
            return ((timedt[13] == 'S')); // (timedt[0]=='A'  ) & (timedt[1]=='R')
        }
        public static bool is_lap(byte[] timedt)
        {
            return (timedt[13] == 'L');
        }
        public static bool is_goal(byte[] timedt)
        {
            return (timedt[13] == 'G');
        }


        public static void readandFifoPush()
        {
            byte[] buffer = new byte[100];
            byte[] charbyte = new byte[20];
            const byte stx = 2;
            const byte etx = 3;
            int howmanyread;
            int counter = -1;
            int laneNo = 0;
            
            string mytime = "";
            string mytimePrev = "";
            int laneNoPrev = 0;
            try
            {
                while (true)
                {
                    try
                    {
                        while (threadPause) { }
                        howmanyread = _serialPort.Read(buffer, 0, 54); // 54=18*3
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                        howmanyread = 0;
                    }
                    for (int j = 0; j < howmanyread; j++)
                    {
                        if (buffer[j] == stx)
                        {
                            counter = 0;
                        }
                        else if (buffer[j] == etx)
                        {
                            counter = -1;
                            if (is_start(charbyte))
                            {
                                tmd.push();
                            }

                            if (is_lap(charbyte))
                            {
                                laneNo = charbyte[2] - 48;
                                mytime = Encoding.ASCII.GetString(charbyte, 5, 8);
                                if ((mytime!=mytimePrev)||(laneNo!=laneNoPrev)) {
                                    tmd.push(misc.timestr2int(mytime), laneNo, 0);
                                }
                                mytimePrev = mytime;
                                laneNoPrev = laneNo;

                            }
                            if (is_goal(charbyte))
                            {
                                int orderOfArrival;
                                laneNo = charbyte[2] - 48;
                                orderOfArrival = charbyte[3] - '0';
                                if ((orderOfArrival <= 0) || (orderOfArrival >=11)) {
                                    orderOfArrival = 1;
                                    setup_file_io.writeLog("Invalid order of arrival.");
                                    
                                }

                                mytime = Encoding.ASCII.GetString(charbyte, 5, 8);
                                if ((mytime != mytimePrev) || (laneNo != laneNoPrev))
                                {
                                    tmd.push(misc.timestr2int(mytime), laneNo, orderOfArrival);
                                }
                                mytimePrev = mytime;
                                laneNoPrev = laneNo;
                            }

                        } else if (counter >= 0)
                        {
                            charbyte[counter++] = buffer[j];
                            //Console.Write("{0} ,{1}", buffer[j], Encoding.ASCII.GetString(buffer, j,1));
                            if (counter > 16)
                            {
                                setup_file_io.writeLog("error counter reaches 17.");
                                counter = -1;
                            }
                        }

                        if (threadStop) return;
                    }
                    if (threadStop) return;
                }


            }
            catch (TimeoutException e) {
                Console.WriteLine(e.Message);
            }
        }    

    }
    public  static class lane_monitor
    {
        private static bool[] goaled=new bool[10];
        public static void init_lane_monitor()
        {
            int ix;
            for (ix=0; ix<10; ix++)
            {
                goaled[ix] = false;
            }
        }
        public static void Set_goal(int laneNo) => goaled[laneNo] = true;
        public static bool Is_goal(int laneNo) => goaled[laneNo];

    }
    public static class mdb_interface
    {

        public static bool can_go_with_next(int uid, int kumi, int maxLaneNumber)
        {
            int prgNo = program_db.get_race_number_from_uid(uid);
            int nextuid;
            if (maxLaneNumber == event_db.get_max_lane_number()) return false;
            if (kumi > 1) return false;
            prgNo++;
            if (prgNo > program_db.get_max_program_no()) return false;
            nextuid = program_db.get_uid_from_prgno(prgNo);
            if (!program_db.is_same_distance_style(uid, nextuid)) return false;
            if (maxLaneNumber < result_db.get_first_occupied_lane(nextuid, 1)) return true;
            return false;

        }
        public static bool can_go_with_prev(int uid, int kumi)
        {
            int prgNo;
            int prevuid;
            int minLaneNumber = result_db.get_first_occupied_lane(uid, kumi);

            if (minLaneNumber == 1) return false;
            prgNo = program_db.get_race_number_from_uid(uid);
            if (prgNo == 1) return false;
            program_db.dec_race_number(ref prgNo);
            if (prgNo == 0) return false;
            prevuid = program_db.get_uid_from_prgno(prgNo);
            if (result_db.race_exist(prevuid, 2)) return false;
            if (!program_db.is_same_distance_style(uid, prevuid)) return false;
            if (minLaneNumber > result_db.get_last_occupied_lane(prevuid, 1)) return true;
            return false;
        }
        
    }
   
    public  class timeData
    {
        Queue<int> dataFifo = new Queue<int>();
        private static int timeDataEncode(int timeint, int laneNo, int orderOfArrival)
        {
            return laneNo * 1000000 + (orderOfArrival * 10000000) + timeint;
        }
        private static void timeDataDecode(int timedata, ref int timeint, ref int laneNo, ref int goalRank)
        {
            goalRank = timedata / 10000000;
            laneNo = (timedata % 10000000) / 1000000;
            timeint = timedata % 1000000;
        }
        public void push()
        {
            dataFifo.Enqueue(0);
        }
        public void push(int timeint, int laneNo, int orderOfArrival)
        {
            dataFifo.Enqueue(timeDataEncode(timeint, laneNo, orderOfArrival));
        }
        public bool pop(ref int timeint, ref int laneNo, ref int orderOfArrival)
        {
            if (dataFifo.Count>0)
            {
                timeDataDecode(dataFifo.Dequeue(), ref timeint, ref laneNo, ref orderOfArrival);
                return true;
            }
            return false;
            
        }
    }

}
//------------------------trash--------------------------------
