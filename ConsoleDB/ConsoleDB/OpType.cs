using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleDB
{

    //该类为存储静态常量数据的类，相当于宏定义，便于理解 
    class OpType
    {
        /*
         * Fromat:
         *          User_ID#OpType#User_Name#User_Password#User_Sex#User_Age#User_Address#User_HeadPic#User_CreateDate#User_Email
         */
        public const  int RIGISTER = 0;                       //注册

        /*
        * Fromat:
        *           Server#OpType#User_ID
        */
        public const  int RIGISTER_RES = 1;                   //注册后的回复

        /*
        * Fromat:
        *          UserID#OpType#User_ID#User_Password#Port
        */
        public const  int LOGIN = 2;                          //登录

        /*
        * Fromat:
        *          Server#OpType#1(0)
        */
        public const  int LOGIN_RES = 3;                      //登录后的回复

        /*
        * Fromat:
        *          Server#OpType#User_ID
        */
        public const  int INFORM_FRIENDS = 4;                 //通知其他好友自己的上线消息



        /*
        * Fromat:
        *          UserID#OpType#User_ID
        */
        public const  int ALL_GROUPS = 6;                     //获得所有所属的群组

        /*
        * Fromat:
        *          Server#OpType#Group_ID#Group_Name#Group_CreatorID#Group_CreateDate#Group_Logo#..............(重复小组信息)
        */
        public const  int ALL_GROUPS_RES = 7;                 //获得所有群组的回复
       
        /*
        * Fromat:
        *          UserID#OpType#User_ID
        */
        public const  int ALL_FRIENDS = 8;                    //获得所有好友
        
        /*
        * Fromat:
        *          Server#OpType#User_ID#User_Name#User_Password#User_Sex#User_Age#User_Address#User_HeadPic#User_CreateDate#User_Email#...........(重复个人信息)
        */
        public const  int ALL_FRIENDS_RES = 9;                //获得所有好友的回复
        
        /*
        * Fromat:
        *          UserID#OpType#User_ID
        */
        public const  int ALL_ONLINE_FRIENDS = 10;            //获得所有在线好友
       
        /*
       * Fromat:
       *          Server#OpType#User_ID#User_Name#User_Password#User_Sex#User_Age#User_Address#User_HeadPic#User_CreateDate#User_Email#...........(重复个人信息)
       */
        public const  int ALL_ONLINE_FRIENDS_RES = 11;         //获得所有在线好友的回复
       
        /*
        * Fromat:
        *          UserID#OpType#User_ID
        */
        public const  int ALL_FRIEND_REQUESTS_REC = 12;       //获得所有收到的好友请求
       
        /*
        * Fromat:
        *         Server#OpType#Relat_ID#Relat_User1#Relat_User2#Relat_Confirm#Relat_CreateDate#........(重复好友关系信息)
        */
        public const  int ALL_FRIEND_REQUESTS_REC_RES = 13;   //获得所有收到的好友请求的回复

        /*
        * Fromat:
        *          UserID#OpType#User_ID
        */
        public const  int ALL_FRIEND_REQUESTS_SED = 14;       //获得所有发出的好友请求

        /*
        * Fromat:
        *         Server#OpType#Relat_ID#Relat_User1#Relat_User2#Relat_Confirm#Relat_CreateDate#........(重复好友关系信息)
        */
        public const  int ALL_FRIEND_REQUESTS_SED_RES = 15;   //获得所有发出的好友请求的回复

        /*
        * Fromat:
        *          UserID#OpType#User_ID
        */
        public const  int ALL_OFFLINE_MSG = 16;               //获得所有离线消息

        /*
        * Fromat:
        *          Server#OpType#Msg_ID#Msg_User1#Msg_User2#Msg_Content#Msg_SendDate#...........(重复消息信息)
        */
        public const  int ALL_OFFLINE_MSG_RES = 17;             //获得所有离线消息的回复

        /*
        * Fromat:
        *          UserID#OpType#User_ID
        */
        public const  int FRIEND_IP = 18;                       //获得好友IP

        /*
        * Fromat:
        *          Server#OpType#User_IPAddress#User_IPPort
        */
        public const  int FRIEND_IP_RES = 19;                   //获得好友IP的回复

        /*
        * Fromat:
        *          UserID#OpType#User1#User2
        */
        public const  int SEND_FRIEND_REQUEST = 20;             //发送好友请求

        /*
        * Fromat:
        *          Server#OpType#Relat_ID
        */
        public const  int SEND_FRIEND_REQUEST_RES = 21;         //发送好友请求的回复

        /*
        * Fromat:
        *          UserID#OpType#User1#User2
        */
        public const  int AGREE_FRIEND_REQUEST = 22;            //同意好友请求

        /*
        * Fromat:
        *          Server#OpType#User1#User2#Successed
        */
        public const  int AGREE_FRIEND_REQUEST_RES = 23;        //同意好友请求的回复

        /*
        * Fromat:
        *          UserID#OpType#User1#User2
        */
        public const  int DISAGREE_FRIEND_REQUEST = 24;         //拒绝好友请求

        /*
        * Fromat:
        *          Server#OpType#User1#User2#Failed
        */
        public const  int DISAGREE_FRIEND_REQUEST_RES = 25;       //拒绝好友请求的回复

        /*
        * Fromat:
        *          UserID#OpType#User1#User2#Msg_Content
        */
        public const  int SEND_OFFLINE_MSG = 26;                //发送离线消息

        /*
        * Fromat:
        *          UserID#OpType#Msg_ID
        */
        public const  int RECIEVE_OFFLINE_MSG = 27;             //接收离线消息

        /*
        * Fromat:
        *          UserID#OpType#Group_Name#Group_Creator#Group_CreateDate#Group_Logo
        */
        public const  int CREATE_GROUP = 28;                    //创建群组

        /*
        * Fromat:
        *          Server#OpType#Group_ID#Group_Name
        */
        public const  int CREATE_GROUP_RES = 29;                //创建群组的回复

        /*
        * Fromat:
        *          UserID#OpType#User_ID#Group_ID
        */
        public const  int JOIN_GROUP = 30;                      //加入群组

        /*
        * Fromat:
        *          Server#OpType#Group_ID#Group_Name#Group_Creator#Group_CreateDate#Group_Logo
        */
        public const  int JOIN_GROUP_RES = 31;                  //加入群组的回复

        /*
        * Fromat:
        *          UserID#OpType#Group_ID
        */
        public const  int ALL_GROUP_REQUEST = 32;               //获得群组的所有加入请求

        /*
        * Fromat:
        *          Server#OpType#Entity_ID#Entity_GroupID#Entity_MemID#Entity_Confirm#Entity_CreateDate#........(重复组内关系信息)
        */
        public const  int ALL_GROUP_REQUEST_RES = 33;           //获得群组的所有加入请求的回复

        /*
       * Fromat:
       *          UserID#OpType#User_ID#Group_ID
       */
        public const  int AGREE_JOIN_GROUP = 34;                //同意加入群组

        /*
       * Fromat:
       *          Server#OpType#User_ID#Group_ID#Successed
       */
        public const  int AGREE_JOIN_GROUP_RES = 35;            //同意加入群组的回复

        /*
       * Fromat:
       *          UserID#OpType#User_ID#Group_ID
       */
        public const  int DISAGREE_JOIN_GROUP = 36;             //拒绝加入群组

        /*
       * Fromat:
       *          Server#OpType#User_ID#Group_ID#Failed
       */
        public const  int DISAGREE_JOIN_GROUP_RES = 37;         //拒绝加入群组的回复

        /*
         * Fromat:
         *          User_ID#OpType#User_ID#User_Name#User_Password#User_Sex#User_Age#User_Address#User_HeadPic#User_CreateDate#User_Email
         */
        public const int ALTER_USER_INFO = 38;                  //修改个人信息

        /*
         * Fromat:
         *          User_ID#OpType#User_ID
         */
        public const int USER_INFO = 39;                        //获得用户信息

        /*
         * Fromat:
         *          Server#OpType#User_ID#User_Name#User_Password#User_Sex#User_Age#User_Address#User_HeadPic#User_CreateDate#User_Email
         */
        public const int USER_INFO_RES = 40;                    //获得用户信息的回复

        /*
        * Fromat:
        *          User_ID#OpType#Group_ID
        */
        public const int GROUP_INFO = 41;                       //获得群组的信息

        /*
        * Fromat:
        *          Server#OpType#Group_ID#Group_Name#Group_Creator#Group_CreateDate#Group_Logo
        */
        public const int GROUP_INFO_RES = 42;                   //获得群组信息的回复

        /*
        * Fromat:
        *          User_ID#OpType#Group_ID#Group_Name#Group_Creator#Group_CreateDate#Group_Logo
        */
        public const int ALTER_GROUP_INFO = 43;                 //修改群组的信息

        /*
        * Fromat:
        *          User_ID#OpType#User1#User2#File_Name#File_PartContent
        */
        public const int SEND_NET_FILE = 44;                    //发送网络文件

        /*
       * Fromat:
       *          User_ID#OpType#User_ID#Group_ID#Content
       */
        public const int SEND_GRUOP_MSG = 45;                   //发送群组消息

        /*
       * Fromat:
       *          Server#OpType#User_ID#User_Name#Group_ID#Content
       */
        public const int SEND_GROUP_MSG_RES = 46;               //发送群组消息的回复

        /*
       * Fromat:
       *          User_ID#OpType#Group_ID
       */
        public const int MEMBERS_IN_GROUP = 47;                 //获得群组成员

        /*
       * Fromat:
       *          Server#OpType#User_ID#User_Name#User_Password#User_Sex#User_Age#User_Address#User_HeadPic#User_CreateDate#User_Email#...........(重复个人信息)
       */
        public const int MEMBERS_IN_GROUP_RES = 48;             //获得群组成员的回复

        /*
      * Fromat:
      *          User_ID#OpType#Msg
      */
        public const int COMMUNICATION = 49;                    //聊天消息

        /*
       * Fromat:
       *         Server#OpType#Msg_ID
       */
        public const int SEND_OFFLINE_MSG_RES = 50;                //发送离线消息的回复

        /*
      * Fromat:
      *          UserID#OpType#UserID#GroupID
      */
        public const int LEAVE_GROUP = 51;                 //退出群组

        /*
      * Fromat:
      *          Server#OpType#User1#User2#FileName#FileContent
      */
        public const int SEND_NET_FILE_RES = 52;                 //发送文件的回复

        /*
         * Fromat:
         *          UserID#OpType#UserName
        */
        public const int FIND_USERS = 53;                 //查找用户

        /*
         * Fromat:
         *          Server#OpType#User_ID#User_Name#User_Password#User_Sex#User_Age#User_Address#User_HeadPic#User_CreateDate#User_Email#...........(重复个人信息)
         */
        public const int FIND_USERS_RES = 54;                 //查找用户的回复


        /*
         * Fromat:
         *          UserID#OpType#GroupName
        */
        public const int FIND_GROUPS = 55;                 //查找群组


        /*
         * Fromat:
         *          Server#OpType#Group_ID#Group_Name#Group_Creator#Group_CreateDate#Group_Logo
        */
        public const int FIND_GROUPS_RES = 56;                 //查找群组的回复


        /*
         * Fromat:
         *          UserID#OpType#User1#User2
        */
        public const int IS_FRIEND = 57;                 //查看是否为好友

        /*
         * Fromat:
         *          Server#OpType#1(0)
        */
        public const int IS_FRIEND_RES = 58;                 //查看是否为好友的回复

        /*
         * Fromat:
         *          UserID#OpType#UserID#GroupID
        */
        public const int IS_IN_GROUP = 59;                 //查看是否组员

        /*
         * Fromat:
         *          Server#OpType#1(0)
        */
        public const int IS_IN_GROUP_RES = 60;                 //查看是否组员的回复

        /*
         * Fromat:
         *          Server#OpType#User1#User2
        */
        public const int SEND_FRIEND_REQUEST_NOTE = 61;                 //好友请求提示


        /*
         * Fromat:
         *          Server#OpType#UserID#GroupID
        */
        public const int JOIN_GROUP_NOTE = 62;                 //请求加入群组的给创建者的提示

        /*
         * Fromat:
         *          UserID#OpType#UserID
        */
        public const int LOGOUT = 63;                 //注销或退出

        /*
         * Fromat:
         *          Server#OpType#UserID
        */
        public const int LOGOUT_NOTE=64;                 //注销或退出的提示


        /*
          * Fromat:
          *          UserID#OpType#User1#User2
         */
        public const int DELETE_FRIEND=65;                 //删除好友

        /*
         * 注意：在输入路径时使用"/"而不要用"\"
         *       用户输入的字串中不应该包含"#"，除了文件考虑到，其他的有一定的概率
         * 
         *       这里为了测试端口不固定，最后在发布时应该把端口固定插入在线队列
         *       目前文件传输字串未处理
         */
    }
}
