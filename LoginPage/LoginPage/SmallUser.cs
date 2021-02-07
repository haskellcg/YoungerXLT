using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleDB
{
    class SmallUser                                     //用于存储在线用户的临时少量信息
    {
        private string _UserID;                         //用户ID
        private string _IPAddress;                      //用户的上线IP地址
        private int _IPPort;                            //用户的IP端口

        public SmallUser()
        { }
        public SmallUser(string _UserID, string _IPAddress, int _IPPort)
        {
            UserID = _UserID;
            IPAddress = _IPAddress;
            IPPort = _IPPort;
        }

        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public string IPAddress
        {
            get { return _IPAddress; }
            set { _IPAddress = value; }
        }

        public int IPPort
        {
            get { return _IPPort; }
            set { _IPPort = value; }
        }
    }
}
