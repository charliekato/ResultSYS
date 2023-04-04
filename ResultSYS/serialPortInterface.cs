using System;
using System.Text;
using System.IO.Ports;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShowLaneOrder
{
    public class timeData
    {
        Queue<int> dataFifo = new Queue<int>();
        private static int timeDataEncode(int timeint, int laneNo, int goalFlag)
        {
            return laneNo * 1000000 + (goalFlag * 10000000) + timeint;
        }
        private static void timeDataDecode(int timedata, ref int timeint, ref int laneNo, ref int goalFlag)
        {
            goalFlag = timedata / 10000000;
            laneNo = (timedata % 10000000) / 1000000;
            timeint = timedata % 1000000;
        }
        public void push()
        {
            dataFifo.Enqueue(0);
        }
        public void push(int timeint, int laneNo, int goalFlag)
        {
            dataFifo.Enqueue(timeDataEncode(timeint, laneNo, goalFlag));
        }
        public bool pop(ref int timeint, ref int laneNo, ref int goalFlag)
        {
            if (dataFifo.Count > 0)
            {
                timeDataDecode(dataFifo.Dequeue(), ref timeint, ref laneNo, ref goalFlag);
                return true;
            }
            return false;

        }
    }

    static public class serialPortInterface
    {
        static timeData tmd = new timeData();
        static SerialPort _serialPort;
        public static void serialPortOpen(string portname="COM3")
        {
            _serialPort = new SerialPort();
            _serialPort.PortName = portname; // this must be configurable...
            _serialPort.BaudRate = 9600;
            _serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), "Even");
            _serialPort.DataBits = 7; 
            _serialPort.ReadTimeout = -1;
            _serialPort.WriteTimeout = -1;
            _serialPort.Open();
        }

        public static void send(string data)
        {
            byte stx = 2;
            byte etx = 3;
            byte[] ctrdat = { stx, etx }; // stx, etx

            _serialPort.Write(ctrdat, 0, 1);
            _serialPort.Write(data);
            _serialPort.Write(ctrdat, 1, 1);
        }
        public static void lap(int lane, int mytime)
        {
            string data = "A1" + lane + "  " + timeint2str(mytime) + "L01";
            send(data);
        }
        public static void goal(int lane, int mytime)
        {
            string data = "A1" + lane + "  " + timeint2str(mytime) + "G01";
            send(data);
        }
        public static string timeint2str(int mytime)
        {
            int minutes = mytime / 10000;
            int temps = mytime % 10000;
            int seconds = temps / 100;
            int centiseconds = temps % 100;
            if (minutes > 0)
            {
                return minutes.ToString().PadLeft(2) + ":" + seconds.ToString().PadLeft(2) + "." + centiseconds.ToString().PadLeft(2, '0');
            }
            return "   " + seconds.ToString().PadLeft(2) + "." + centiseconds.ToString().PadLeft(2, '0');
        }
        public static void Write()
        {
            string startdata = "AR       0.0 S  ";
            int skip;
            for (int i = 0; i < 10; i++)
            {
                skip = i % 10;
                send(startdata);
                for (int l = 1; l < 10; l++)
                {
                    if (l != skip)
                    {
                        lap(l, 2934 - l + i);
                    }
                }
                Thread.Sleep(2000);
                for (int l = 1; l < 10; l++)
                {
                    if (l != skip)
                        goal(l, 12345 + l + i);
                }
                Thread.Sleep(2000);
            }

        }
    }
}
