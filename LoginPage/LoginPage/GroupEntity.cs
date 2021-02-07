using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleDB
{
    class GroupEntity
    {
        public GroupEntity()
        { }
        public GroupEntity(int _EntityID, int _EntityGroupID, string _EntityMemID, bool _EntityConfirm,
                            DateTime _EntityCreateDate)
        {
            EntityID = _EntityID;
            EntityGroupID = _EntityGroupID;
            EntityMemID = _EntityMemID;
            EntityConfirm = _EntityConfirm;
            EntityCreateDate = _EntityCreateDate;
        }

        private int _EntityID;                      //组内关系ID             
        public int EntityID
        {
            get { return _EntityID; }
            set { _EntityID = value; }
        }
        private int _EntityGroupID;              //组群ID
        public int EntityGroupID
        {
            get { return _EntityGroupID; }
            set { _EntityGroupID = value; }
        }
        private string _EntityMemID;                //组员ID
        public string EntityMemID
        {
            get { return _EntityMemID; }
            set { _EntityMemID = value; }
        }
        private bool _EntityConfirm;                //关系是否确认
        public bool EntityConfirm
        {
            get { return _EntityConfirm; }
            set { _EntityConfirm = value; }
        }
        private DateTime _EntityCreateDate;         //申请或确认时间
        public DateTime EntityCreateDate
        {
            get { return _EntityCreateDate; }
            set { _EntityCreateDate = value; }
        }
    }
}
