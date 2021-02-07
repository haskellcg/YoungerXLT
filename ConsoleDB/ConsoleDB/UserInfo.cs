using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ConsoleDB
{
    class UserInfo
    {
        public UserInfo()
        { }
        public UserInfo(string _UserID,string _UserName,string _UserPassword,bool _UserSex,
                       int _UserAge,string _UserAddress,string _UserHeadpic,DateTime _UserCreateDate,
                        string _UserEmail)
        {
            UserID = _UserID;
            UserName = _UserName;
            UserPassword = _UserPassword;
            UserSex = _UserSex;
            UserAge = _UserAge;
            UserAddress = _UserAddress;
            UserHeadpic = _UserHeadpic;
            UserCreateDate = _UserCreateDate;
            UserEmail = _UserEmail;
        }
        private string _UserID;                     //用户ID，由函数生成
        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        private string _UserName;                   //用户姓名
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private string _UserPassword;               //用户密码
        public string UserPassword
        {
            get { return _UserPassword; }
            set { _UserPassword = value; }
        }
        private bool _UserSex;                     //用户性别
        public bool UserSex
        {
            get { return _UserSex; }
            set { _UserSex = value; }
        }
        private int _UserAge;                      //用户年龄
        public int UserAge
        {
            get { return _UserAge; }
            set { _UserAge = value; }
        }
        private string _UserAddress;                //用户地址
        public string UserAddress
        {
            get { return _UserAddress; }
            set { _UserAddress = value; }
        }
        private string _UserHeadpic;                //用户的头像路径
        public string UserHeadpic
        {
            get { return _UserHeadpic; }
            set { _UserHeadpic = value; }
        }
        private DateTime _UserCreateDate;           //用户帐号的创建时间
        public DateTime UserCreateDate
        {
            get { return _UserCreateDate; }
            set { _UserCreateDate = value; }
        }
        private string _UserEmail;                  //用户的邮箱地址
        public string UserEmail
        {
            get { return _UserEmail; }
            set { _UserEmail = value; }
        }
    }
}
