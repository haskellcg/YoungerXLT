using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleDB
{
    class GroupInfo
    {
        public GroupInfo()
        { }
        public GroupInfo(int _GroupID,string _GroupName,string _GroupCreator,DateTime _GroupCreateDate,
                        string _GroupLogo)
        {
            GroupID = _GroupID;
            GroupName = _GroupName;
            GroupCreator = _GroupCreator;
            GroupCreateDate = _GroupCreateDate;
            GroupLogo = _GroupLogo;
        }
        private int _GroupID;                       //组群ID
        public int GroupID
        {
            get { return _GroupID; }
            set { _GroupID = value; }
        }
        private string _GroupName;                  //组群名字
        public string GroupName
        {
            get { return _GroupName; }
            set { _GroupName = value; }
        }
        private string _GroupCreator;               //组群创建者
        public string GroupCreator
        {
            get { return _GroupCreator; }
            set { _GroupCreator = value; }
        }
        private DateTime _GroupCreateDate;          //组群创建日期
        public DateTime GroupCreateDate
        {
            get { return _GroupCreateDate; }
            set { _GroupCreateDate = value; }
        }
        private string _GroupLogo;                  //组群的logo
        public string GroupLogo
        {
            get { return _GroupLogo; }
            set { _GroupLogo = value; }
        }
    }
}
