using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace RegistDefault
{
    public partial class NamePickForm : Form
    {
        string dbName;
        swimmer_db swimmer_db;
        program_db program_db;
        const string magicWord = "Provider=Microsoft.ACE.OLEDB.12.0;Mode=Read;Data Source=";
        public NamePickForm(string dbName, string gameName)
        {
            InitializeComponent();
            this.dbName = dbName;
            this.Text = gameName;
            swimmer_db = new swimmer_db(magicWord+dbName);
            program_db = new program_db(magicWord+dbName);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtBoxName.Text=="")
            {
                MessageBox.Show("少なくとも一文字以上入力してください");
            } else {
                show_name_list(txtBoxName.Text);
            }

        }


        private void btnTeamSearch_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在利用できません。");
        }
        private void show_name_list( string partOftheName)
        {
            string connectionString = magicWord + dbName;
            int counter = 0;

            this.Cursor = Cursors.WaitCursor;
            lbxName.Items.Clear();
            lbxName.MultiColumn = true;
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                String sql = "SELECT 氏名, 所属名称1 FROM  選手マスター " +
                    "WHERE 氏名 LIKE '%" + partOftheName + "%';";
                int maxLen = 0;
                string[] Names1 = new string[100];
                string[] belongsTo = new string[100];
                int[] myByteLen = new int[100];
                Encoding shiftjisEnc = Encoding.GetEncoding("Shift-JIS");
                OleDbCommand comm = new OleDbCommand(sql, conn);
                conn.Open();
                using (var dr = comm.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string myName = (string)dr["氏名"];
                        Names1[counter] = myName;
                        myByteLen[counter] = shiftjisEnc.GetByteCount(myName);
                        if (maxLen < myByteLen[counter]) maxLen = myByteLen[counter];

                        belongsTo[counter] = (string)dr["所属名称1"];
                        counter++;

                    }
                    for (int i = 0; i < counter; i++)
                    {
                        lbxName.Items.Add(Names1[i] + new string(' ', (maxLen + 2 - myByteLen[i])) + belongsTo[i]);
                    }
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void btnNameSelect_Click(object sender, EventArgs e)
        {
            if (btnNameSelect.Text=="選択")
            {
                if (lbxName.SelectedItems == null)
                {
                    MessageBox.Show("名前を選択してください。");
                }
                else
                {
                    string
                    selectedName = lbxName.SelectedItem.ToString();
                    string[] sep = { " " };
                    string[] selectedArray = selectedName.Split(sep, StringSplitOptions.RemoveEmptyEntries);

                    show_race_list(selectedArray[0]);
                }

            }
            else
            {
                foreach (object item in lbxName.SelectedItems)
                {
                    MessageBox.Show(item.ToString());
                }
            }
        }

        private string right(string org, int length)
        {
            string modifiedStr = "        " + org;
            int orglength = modifiedStr.Length;
            return modifiedStr.Substring(orglength-length);
        }
        private void show_race_list(string myName)

        {
            string connectionString = magicWord + dbName;
            lbxName.Items.Clear();
            lbxName.MultiColumn = false;
            lbxName.Items.Add("競技番号  組  レーン  名前");
            lbxName.SelectionMode = SelectionMode.MultiSimple;
            btnNameSelect.Text = "棄権";
            int swimmerID;
            swimmerID = swimmer_db.get_id(myName);
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                String sql = "SELECT 選手番号, 組, 水路,  UID  FROM  記録マスター where 選手番号 = " + swimmerID.ToString();

                OleDbCommand comm = new OleDbCommand(sql, conn);
                int prgNo;
                conn.Open();
                using (var dr = comm.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        prgNo = program_db.get_race_number_from_uid(Convert.ToInt32(dr["UID"]));
                        lbxName.Items.Add(right(prgNo.ToString(), 8) + right(dr["組"].ToString(), 4) + 
                            right(dr["水路"].ToString(), 6)+" "+myName);
                    }
                }

            }
        }
    }
}
