using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleDB
{
    class Relationship
    {
        public Relationship()
        { }
        public Relationship(int _RelatID, string _RelatUser1, string _RelatUser2, bool _RelatConfirm,
                           DateTime _RelatCreateDate)
        {
            RelatID = _RelatID;
            RelatUser1 = _RelatUser1;
            RelatUser2 = _RelatUser2;
            RelatConfirm = _RelatConfirm;
            RelatCreateDate = _RelatCreateDate;
        }

        private int _RelatID;                      //关系ID
        public int RelatID
        {
            get { return _RelatID;}
            set { _RelatID = value; }
        }
        private string _RelatUser1;                 //用户1(被请求的用户)
        public string RelatUser1
        {
            get { return _RelatUser1; }
            set { _RelatUser1 = value; }
        }
        private string _RelatUser2;                 //用户2(请求的用户)
        public string RelatUser2
        {
            get { return _RelatUser2; }
            set { _RelatUser2 = value; }
        }
        private bool _RelatConfirm;                 //好友关系是否确定
        public bool RelatConfirm
        {
            get { return _RelatConfirm; }
            set { _RelatConfirm = value; }
        }
        private DateTime _RelatCreateDate;          //建立或申请时间
        public DateTime RelatCreateDate
        {
            get { return _RelatCreateDate; }
            set { _RelatCreateDate = value; }
        }
    }
}
