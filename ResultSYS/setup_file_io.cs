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
