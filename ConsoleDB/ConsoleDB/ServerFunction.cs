using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ConsoleDB
{
    class ServerFunction
    {
        OnLineList OLList;
        IPEndPoint endPoint;

        public ServerFunction(ref OnLineList _list,ref IPEndPoint _endPoint)
        {
            OLList = _list;
            endPoint = _endPoint;
        }
        public string DistributeTasks(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            if (analy.GetKindOfReq() != OpType.RIGISTER && analy.GetKindOfReq() != OpType.LOGIN)
            {
                if (OLList.GetUserByID(analy.GetUserID()) == null)
                    return "###";
                endPoint.Address =IPAddress.Parse(OLList.GetUserByID(analy.GetUserID()).IPAddress);
                endPoint.Port = OLList.GetUserByID(analy.GetUserID()).IPPort;
            }
            switch (analy.GetKindOfReq())
            {
                case OpType.RIGISTER:
                    return DoRegister(formatString);
                case OpType.LOGIN:
                    return DoLogin(formatString);
                case OpType.ALL_GROUPS:
                    return DoGetAllGroupJoined(formatString);
                case OpType.ALL_FRIENDS:
                    return DoGetAllFriends(formatString);
                case OpType.ALL_ONLINE_FRIENDS:
                    return DoGetAllFriendsOL(formatString);
                case OpType.ALL_FRIEND_REQUESTS_REC:
                    return DoGetAllRecievedFriendRequests(formatString);
                case OpType.ALL_FRIEND_REQUESTS_SED:
                    return DoGetAllSentFriendRequests(formatString);
                case OpType.ALL_OFFLINE_MSG:
                    return DoGetAllOffLineMsg(formatString);
                case OpType.FRIEND_IP:
                    return DoGetFriendIP(formatString);
                case OpType.DELETE_FRIEND:
                    DoDeleteFriend(formatString);
                    break;
                case OpType.SEND_FRIEND_REQUEST:
                    return DoSendFriendRequest(formatString);
                case OpType.AGREE_FRIEND_REQUEST:
                    DoAgreeFriendRequest(formatString);
                    break;
                case OpType.DISAGREE_FRIEND_REQUEST:
                    DoDisagreeFriendRequest(formatString);
                    break;
                case OpType.SEND_OFFLINE_MSG:
                    return DoSendOffLineMsg(formatString);
                case OpType.RECIEVE_OFFLINE_MSG:
                    DoRecieveOffLineMsg(formatString);
                    break;
                case OpType.CREATE_GROUP:
                    return DoCreateGroup(formatString);
                case OpType.JOIN_GROUP:
                    return DoJoinGroup(formatString);
                case OpType.ALL_GROUP_REQUEST:
                    return DoGetAllJoinGroupRequests(formatString);
                case OpType.AGREE_JOIN_GROUP:
                    DoAgreeJoinGroup(formatString);
                    break;
                case OpType.DISAGREE_JOIN_GROUP:
                    DoDisagreeJoinGroup(formatString);
                    break;
                case OpType.ALTER_USER_INFO:
                    DoAlterUserInfo(formatString);
                    break;
                case OpType.USER_INFO:
                    return DoGetUserInfo(formatString);
                case OpType.GROUP_INFO:
                    return DoGetGroupInfo(formatString);
                case OpType.ALTER_GROUP_INFO:
                    DoAlterGroupInfo(formatString);
                    break;
                case OpType.SEND_NET_FILE:
                    return DoSendNetFile(formatString);
                case OpType.SEND_GRUOP_MSG:
                    DoSendGroupMsg(formatString);
                    break;
                case OpType.MEMBERS_IN_GROUP:
                    return DoGetGroupMembers(formatString);
                case OpType.LEAVE_GROUP:
                    DoLeaveGroup(formatString);
                    break;
                case OpType.FIND_USERS:
                    return DoFindUsers(formatString);
                case OpType.FIND_GROUPS:
                    return DoFindGroups(formatString);
                case OpType.IS_FRIEND:
                    return DoIsFriend(formatString);
                case OpType.IS_IN_GROUP:
                    return DoIsInGroup(formatString);
                case OpType.LOGOUT:
                    DoLogout(formatString);
                    break;
                default:
                    break;
            }

            return "###";                 //表示调用函数无返回值
        }

        /*######################################################################*/
        /* 下面的函数做相应的操作                                               */
        /*                       最后应该需要返回的字符串格式化                 */
        /*                                 需要自己构造，按照OpType.cs中的说明  */
        /*         例如:Server#1#1213113141                                     */
        /*######################################################################*/

        private string DoRegister(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            UserInfo user = new UserInfo();
            user.UserName = analy.GetParaIndexOf(0);
            user.UserPassword = analy.GetParaIndexOf(1);
            user.UserSex = int.Parse(analy.GetParaIndexOf(2))==1?true:false;
            user.UserAge = int.Parse(analy.GetParaIndexOf(3));
            user.UserAddress = analy.GetParaIndexOf(4);
            user.UserHeadpic = analy.GetParaIndexOf(5);
            user.UserCreateDate = DateTime.Parse(analy.GetParaIndexOf(6));
            user.UserEmail = analy.GetParaIndexOf(7);

            DBFunction dbf = new DBFunction();
            string format ="Server#"+OpType.RIGISTER_RES+"#";
            format+=dbf.Register(user);

            endPoint.Port = int.Parse(analy.GetParaIndexOf(8));
            return format;
        }

        private string DoLogin(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            DBFunction dbf = new DBFunction();
            string format = "Server#"+OpType.LOGIN_RES+"#";
            format+=(dbf.Login(analy.GetParaIndexOf(0),analy.GetParaIndexOf(1))==true?1:0);
            if (dbf.Login(analy.GetParaIndexOf(0), analy.GetParaIndexOf(1)))
            {
                string IP = endPoint.Address.ToString();
                int Port = int.Parse(analy.GetParaIndexOf(2));
                endPoint.Port = Port;
                OLList.OnLined(new SmallUser(analy.GetUserID(),IP,Port));

                System.Console.WriteLine("登录时的IP:"+IP+"以及   Port:"+Port);

                string msg = "Server#" + OpType.INFORM_FRIENDS + "#" + analy.GetParaIndexOf(0);
                foreach (string userID in OLList.GetAllOLUsersID())
                {
                    if (dbf.IsFriend(analy.GetParaIndexOf(0), userID))
                    {
                        SmallUser sUser=OLList.GetUserByID(userID);
                        CClient client = new CClient(sUser.IPAddress,sUser.IPPort);
                        try
                        {
                            client.ConnectToServer();
                            client.GetNetworkStream();
                            client.Communicate(msg);
                            client.ReLeaseAll();
                        }
                        catch (Exception e)
                        {
                            System.Console.WriteLine("无法连接用户");
                        }
                    }
                }

            }
            return format;
        }

        private void DoLogout(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            DBFunction dbf=new DBFunction();
            OLList.OffLined(analy.GetParaIndexOf(0));
            string msg = "Server#" + OpType.LOGOUT_NOTE + "#" + analy.GetParaIndexOf(0);
            foreach (string userID in OLList.GetAllOLUsersID())
            {
                if (dbf.IsFriend(userID, analy.GetParaIndexOf(0)))
                {
                    SmallUser sUser = OLList.GetUserByID(userID);
                    CClient client = new CClient(sUser.IPAddress, sUser.IPPort);
                    try
                    {
                        client.ConnectToServer();
                        client.GetNetworkStream();
                        client.Communicate(msg);
                        client.ReLeaseAll();
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine("无法连接用户");
                    }
                }
            }

        }


        private string DoGetAllGroupJoined(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            List<GroupInfo> list = new List<GroupInfo>();
            DBFunction dbf = new DBFunction();
            list = dbf.JoinedGroups(analy.GetParaIndexOf(0));
            list.AddRange(dbf.CreatedGroups(analy.GetParaIndexOf(0)));
            string msg = "Server#"+OpType.ALL_GROUPS_RES;
            foreach (GroupInfo group in list)
            {
                msg += "#" + group.GroupID.ToString();
                msg += "#" + group.GroupName;
                msg += "#" + group.GroupCreator;
                msg += "#" + group.GroupCreateDate.ToString("yyyy-MM-dd HH:mm:ss");
                msg += "#" + group.GroupLogo;
            }
            return msg;
            
        }

        private string DoGetAllFriends(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            DBFunction dbf = new DBFunction();
            List<UserInfo> list = new List<UserInfo>();
            list = dbf.GetFriendsByUserID(analy.GetParaIndexOf(0));
            string msg = "Server#" + OpType.ALL_FRIENDS_RES;
            foreach (UserInfo user in list)
            {
                msg += "#" + user.UserID;
                msg += "#" + user.UserName;
                msg += "#" + user.UserPassword;
                msg += "#" + (user.UserSex==true?1:0);
                msg += "#" + user.UserAge.ToString();
                msg += "#" + user.UserAddress;
                msg += "#" + user.UserHeadpic;
                msg += "#" + user.UserCreateDate.ToString("yyyy-MM-dd HH:mm:ss");
                msg += "#" + user.UserEmail;
            }
            return msg;
        }

        private string DoGetAllFriendsOL(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            DBFunction dbf = new DBFunction();
            List<UserInfo> list = new List<UserInfo>();
            foreach (string userID in OLList.GetAllOLUsersID())
            {
                if (dbf.IsFriend(analy.GetParaIndexOf(0), userID))
                {
                    list.Add(dbf.GetUserInfo(userID));
                }
            }
            string msg = "Server#" + OpType.ALL_ONLINE_FRIENDS_RES;
            foreach (UserInfo user in list)
            {
                msg += "#" + user.UserID;
                msg += "#" + user.UserName;
                msg += "#" + user.UserPassword;
                msg += "#" + (user.UserSex == true ? 1 : 0);
                msg += "#" + user.UserAge.ToString();
                msg += "#" + user.UserAddress;
                msg += "#" + user.UserHeadpic;
                msg += "#" + user.UserCreateDate.ToString("yyyy-MM-dd HH:mm:ss");
                msg += "#" + user.UserEmail;
            }

            return msg;
        }

        private string DoGetAllRecievedFriendRequests(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            DBFunction dbf = new DBFunction();
            List<Relationship> list = new List<Relationship>();
            list = dbf.RecievedFriendRequests(analy.GetParaIndexOf(0));
            string msg = "Server#" + OpType.ALL_FRIEND_REQUESTS_REC_RES;
            foreach (Relationship relat in list)
            {
                msg += "#" + relat.RelatID.ToString();
                msg += "#" + relat.RelatUser1;
                msg += "#" + relat.RelatUser2;
                msg += "#" + (relat.RelatConfirm == true ? 1 : 0);
                msg += "#" + relat.RelatCreateDate.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return msg;
        }

        private string DoGetAllSentFriendRequests(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            DBFunction dbf = new DBFunction();
            List<Relationship> list = new List<Relationship>();
            list = dbf.SentFriendRequests(analy.GetParaIndexOf(0));
            string msg = "Server#" + OpType.ALL_FRIEND_REQUESTS_SED_RES;
            foreach (Relationship relat in list)
            {
                msg += "#" + relat.RelatID.ToString();
                msg += "#" + relat.RelatUser1;
                msg += "#" + relat.RelatUser2;
                msg += "#" + (relat.RelatConfirm == true ? 1 : 0);
                msg += "#" + relat.RelatCreateDate.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return msg;
        }

        private string DoGetAllOffLineMsg(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            DBFunction dbf = new DBFunction();
            List<OffLineMessages> list = new List<OffLineMessages>();
            list = dbf.RecievedOffLineMessages(analy.GetParaIndexOf(0));
            list.AddRange(dbf.SentOffLineMessages(analy.GetParaIndexOf(0)));
            string msg = "Server#" + OpType.ALL_OFFLINE_MSG_RES;
            foreach (OffLineMessages message in list)
            {
                msg += "#" + message.MessageID.ToString();
                msg += "#" + message.MessageUser1;
                msg += "#" + message.MessageUser2;
                msg += "#" + message.MessageContent;
                msg += "#" + message.MessageSendTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return msg;
        }

        private string DoGetFriendIP(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            string msg = "Server#" + OpType.FRIEND_IP_RES;
            if (OLList.IsOnLine(analy.GetParaIndexOf(0)))
            {
                msg += "#" + OLList.GetUserByID(analy.GetParaIndexOf(0)).IPAddress;
                msg += "#" + OLList.GetUserByID(analy.GetParaIndexOf(0)).IPPort;
            }

            System.Console.WriteLine(msg);


            return msg;
        }

        //删除好友
        private void DoDeleteFriend(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            DBFunction dbf=new DBFunction();
            dbf.DeleteFriend(analy.GetParaIndexOf(0),analy.GetParaIndexOf(1));
        }

        //这里调用时应该注意应确保两者不是好友，且两者没有未处理的加好友请求,否则返回空的序列值 
        private string DoSendFriendRequest(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            DBFunction dbf = new DBFunction();
            string msg = "Server#" + OpType.SEND_FRIEND_REQUEST_RES;
            if (!dbf.IsFriend(analy.GetParaIndexOf(0), analy.GetParaIndexOf(1)) && !dbf.RequestExists(analy.GetParaIndexOf(0), analy.GetParaIndexOf(1)) && !dbf.RequestExists(analy.GetParaIndexOf(1),analy.GetParaIndexOf(0)) && !analy.GetParaIndexOf(0).Equals(analy.GetParaIndexOf(1)))
            {
                msg += "#" + dbf.RequestFriend(analy.GetParaIndexOf(0), analy.GetParaIndexOf(1));
                string notice = "Server#" + OpType.SEND_FRIEND_REQUEST_NOTE + "#" + analy.GetParaIndexOf(0) + "#" + analy.GetParaIndexOf(1);
                if (OLList.IsOnLine(analy.GetParaIndexOf(0)))
                {
                    SmallUser sUser = OLList.GetUserByID(analy.GetParaIndexOf(0));
                    CClient client = new CClient(sUser.IPAddress, sUser.IPPort);
                    try
                    {
                        client.ConnectToServer();
                        client.GetNetworkStream();
                        client.Communicate(notice);
                        client.ReLeaseAll();
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine("无法连接用户");
                    }

                }
            }
            return msg;
        }

        private void DoAgreeFriendRequest(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            DBFunction dbf = new DBFunction();
            dbf.AgreeFriendRequest(analy.GetParaIndexOf(0),analy.GetParaIndexOf(1));
            string msg = "Server#" + OpType.AGREE_FRIEND_REQUEST_RES+"#"+analy.GetParaIndexOf(0)+"#"+analy.GetParaIndexOf(1);
            if (OLList.IsOnLine(analy.GetParaIndexOf(1)))
            {
                SmallUser sUser = OLList.GetUserByID(analy.GetParaIndexOf(1));
                CClient client = new CClient(sUser.IPAddress, sUser.IPPort);
                try
                {
                    client.ConnectToServer();
                    client.GetNetworkStream();
                    client.Communicate(msg);
                    client.ReLeaseAll();
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("无法连接用户");
                }
            }
        }

        private void DoDisagreeFriendRequest(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            DBFunction dbf = new DBFunction();
            dbf.DisagreeFriendRequest(analy.GetParaIndexOf(0),analy.GetParaIndexOf(1));
            string msg = "Server#" + OpType.DISAGREE_FRIEND_REQUEST_RES+"#"+analy.GetParaIndexOf(0)+"#"+analy.GetParaIndexOf(1);
            if (OLList.IsOnLine(analy.GetParaIndexOf(1)))
            {
                SmallUser sUser = OLList.GetUserByID(analy.GetParaIndexOf(1));
                CClient client = new CClient(sUser.IPAddress, sUser.IPPort);
                try
                {
                    client.ConnectToServer();
                    client.GetNetworkStream();
                    client.Communicate(msg);
                    client.ReLeaseAll();
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("无法连接用户");
                }
            }
        }

        private string DoSendOffLineMsg(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            DBFunction dbf = new DBFunction();
            string msg = "Server#" + OpType.SEND_OFFLINE_MSG_RES;
            msg += "#" + dbf.SendOffLineMessage(analy.GetParaIndexOf(0),analy.GetParaIndexOf(1),analy.GetParaFromIndex(2));
            return msg;
        }

        private void  DoRecieveOffLineMsg(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            DBFunction dbf = new DBFunction();
            dbf.RecieveOffLineMessage(int.Parse(analy.GetParaIndexOf(0)));
        }

        private string DoCreateGroup(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            DBFunction dbf = new DBFunction();
            GroupInfo group = new GroupInfo();
            group.GroupName = analy.GetParaIndexOf(0);
            group.GroupCreator = analy.GetParaIndexOf(1);
            group.GroupCreateDate = DateTime.Parse(analy.GetParaIndexOf(2));
            group.GroupLogo = analy.GetParaIndexOf(3);
            string msg = "Server#" + OpType.CREATE_GROUP_RES;
            msg+="#"+dbf.InsertGroupInfo(group);
            msg += "#" + group.GroupName;
            return msg;
        }

        //应该确保不是加入自己创建的群组，以及应该没有已有的请求,否则返回空的想序列值
        private string DoJoinGroup(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            DBFunction dbf = new DBFunction();
            string msg = "Server#" + OpType.JOIN_GROUP_RES;
            if (!dbf.GetGroupInfo(int.Parse(analy.GetParaIndexOf(1))).GroupCreator.Equals(analy.GetParaIndexOf(0)) && !dbf.EntityExists(int.Parse(analy.GetParaIndexOf(1)), analy.GetParaIndexOf(0)))
            {
                msg += "#" + dbf.RequestJoinGroup(int.Parse(analy.GetParaIndexOf(1)), analy.GetParaIndexOf(0));
                string notice="Server#"+OpType.JOIN_GROUP_NOTE+"#"+analy.GetParaIndexOf(0)+"#"+analy.GetParaIndexOf(1);
                if (OLList.IsOnLine(dbf.GetGroupInfo(int.Parse(analy.GetParaIndexOf(1))).GroupCreator))
                {
                    SmallUser sUser = OLList.GetUserByID(dbf.GetGroupInfo(int.Parse(analy.GetParaIndexOf(1))).GroupCreator);
                    CClient client = new CClient(sUser.IPAddress, sUser.IPPort);
                    try
                    {
                        client.ConnectToServer();
                        client.GetNetworkStream();
                        client.Communicate(notice);
                        client.ReLeaseAll();
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine("无法连接用户");
                    }
                }
            }
            return msg;
        }

        private string DoGetAllJoinGroupRequests(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            DBFunction dbf = new DBFunction();
            List<GroupEntity> list = new List<GroupEntity>();
            list = dbf.JoinGroupRequests(int.Parse(analy.GetParaIndexOf(0)));
            string msg="Server#"+OpType.ALL_GROUP_REQUEST_RES;
            foreach (GroupEntity entity in list)
            {
                msg += "#" + entity.EntityID;
                msg += "#" + entity.EntityGroupID;
                msg += "#" + entity.EntityMemID;
                msg += "#" + (entity.EntityConfirm == true ? 1 : 0);
                msg += "#" + entity.EntityCreateDate.ToString("yyyy-MM-dd HH:mm:ss");


            }
            return msg;

        }

        
        private void DoAgreeJoinGroup(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            DBFunction dbf = new DBFunction();
            dbf.AgreeJoinGroup(int.Parse(analy.GetParaIndexOf(1)),analy.GetParaIndexOf(0));
            string msg = "Server#" + OpType.AGREE_JOIN_GROUP_RES + "#" + analy.GetParaIndexOf(0) + "#" + analy.GetParaIndexOf(1);
            if (OLList.IsOnLine(analy.GetParaIndexOf(0)))
            {
                SmallUser sUser = OLList.GetUserByID(analy.GetParaIndexOf(0));
                CClient client = new CClient(sUser.IPAddress, sUser.IPPort);
                try
                {
                    client.ConnectToServer();
                    client.GetNetworkStream();
                    client.Communicate(msg);
                    client.ReLeaseAll();
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("无法连接用户");
                }
            }
            
        }

        
        private void DoDisagreeJoinGroup(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            DBFunction dbf = new DBFunction();
            dbf.DisagreeJoinGroup(int.Parse(analy.GetParaIndexOf(1)), analy.GetParaIndexOf(0));
            string msg = "Server#" + OpType.DISAGREE_JOIN_GROUP_RES + "#" + analy.GetParaIndexOf(0) + "#" + analy.GetParaIndexOf(1);
            if (OLList.IsOnLine(analy.GetParaIndexOf(0)))
            {
                SmallUser sUser = OLList.GetUserByID(analy.GetParaIndexOf(0));
                CClient client = new CClient(sUser.IPAddress, sUser.IPPort);
                try
                {
                    client.ConnectToServer();
                    client.GetNetworkStream();
                    client.Communicate(msg);
                    client.ReLeaseAll();
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("无法连接用户");
                }
            }
        }

        private void DoAlterUserInfo(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            DBFunction dbf = new DBFunction();
            UserInfo user = new UserInfo();
            user.UserID = analy.GetParaIndexOf(0);
            user.UserName = analy.GetParaIndexOf(1);
            user.UserPassword = analy.GetParaIndexOf(2);
            user.UserSex = int.Parse(analy.GetParaIndexOf(3)) == 1 ? true : false;
            user.UserAge = int.Parse(analy.GetParaIndexOf(4));
            user.UserAddress = analy.GetParaIndexOf(5);
            user.UserHeadpic = analy.GetParaIndexOf(6);
            user.UserCreateDate = DateTime.Parse(analy.GetParaIndexOf(7));
            user.UserEmail = analy.GetParaIndexOf(8);
            dbf.AlterUserInfo(user);
        }

        private string DoGetUserInfo(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            DBFunction dbf = new DBFunction();
            UserInfo user = dbf.GetUserInfo(analy.GetParaIndexOf(0).Trim());
            string msg = "Server#" + OpType.USER_INFO_RES;
            msg += "#" + user.UserID;
            msg += "#" + user.UserName;
            msg += "#" + user.UserPassword;
            msg += "#" + (user.UserSex == true ? 1 : 0);
            msg += "#" + user.UserAge.ToString();
            msg += "#" + user.UserAddress;
            msg += "#" + user.UserHeadpic;
            msg += "#" + user.UserCreateDate.ToString("yyyy-MM-dd HH:mm:ss");
            msg += "#" + user.UserEmail;
            return msg;

        }

        private string DoGetGroupInfo(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            DBFunction dbf = new DBFunction();
            GroupInfo group = dbf.GetGroupInfo(int.Parse(analy.GetParaIndexOf(0).Trim()));
            string msg = "Server#" + OpType.GROUP_INFO_RES;
            msg += "#" + group.GroupID.ToString();
            msg += "#" + group.GroupName;
            msg += "#" + group.GroupCreator;
            msg += "#" + group.GroupCreateDate.ToString("yyyy-MM-dd HH:mm:ss");
            msg += "#" + group.GroupLogo;
            return msg;
        }

        private void DoAlterGroupInfo(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            DBFunction dbf = new DBFunction();
            GroupInfo group = new GroupInfo();
            group.GroupID = int.Parse(analy.GetParaIndexOf(0));
            group.GroupName = analy.GetParaIndexOf(1);
            group.GroupCreator = analy.GetParaIndexOf(2);
            group.GroupCreateDate = DateTime.Parse(analy.GetParaIndexOf(3));
            group.GroupLogo = analy.GetParaIndexOf(4);
            dbf.AlterGroupInfo(group);
        }

        /////////////////////////////未完
        private string DoSendNetFile(string formatString)
        {
            return null;
        }

        //发送群组消息，如果在线则提示对方，如果不在线则送到离线数据库
        private void DoSendGroupMsg(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            DBFunction dbf = new DBFunction();
            List<UserInfo> list = dbf.GetMembersInGroup(int.Parse(analy.GetParaIndexOf(1)));
            list.Add(dbf.GetUserInfo(dbf.GetGroupInfo(int.Parse(analy.GetParaIndexOf(1))).GroupCreator));
            string msg="Server#"+OpType.SEND_GROUP_MSG_RES+"#"+analy.GetParaIndexOf(0)+"#"+dbf.GetUserInfo(analy.GetParaIndexOf(0)).UserName+"#"+analy.GetParaIndexOf(1)+"#"+analy.GetParaFromIndex(2);
            foreach (UserInfo user in list)
            {
                if (OLList.IsOnLine(user.UserID))
                {
                    SmallUser sUser = OLList.GetUserByID(user.UserID);
                    CClient client = new CClient(sUser.IPAddress, sUser.IPPort);
                    try
                    {
                        client.ConnectToServer();
                        client.GetNetworkStream();
                        client.Communicate(msg);
                        client.ReLeaseAll();
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine("无法连接用户");
                    }
                }
                else
                {
                    dbf.SendOffLineMessage(user.UserID, analy.GetParaIndexOf(0), analy.GetParaFromIndex(2));
                }
            }
        }

        private string DoGetGroupMembers(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            DBFunction dbf = new DBFunction();
            List<UserInfo> list = new List<UserInfo>();
            list.Add(dbf.GetUserInfo(dbf.GetGroupInfo(int.Parse(analy.GetParaIndexOf(0))).GroupCreator));
            list.AddRange(dbf.GetMembersInGroup(int.Parse(analy.GetParaIndexOf(0))));

            string msg = "Server#" + OpType.MEMBERS_IN_GROUP_RES;
            foreach (UserInfo user in list)
            {
                msg += "#" + user.UserID;
                msg += "#" + user.UserName;
                msg += "#" + user.UserPassword;
                msg += "#" + (user.UserSex == true ? 1 : 0);
                msg += "#" + user.UserAge.ToString();
                msg += "#" + user.UserAddress;
                msg += "#" + user.UserHeadpic;
                msg += "#" + user.UserCreateDate.ToString("yyyy-MM-dd HH:mm:ss");
                msg += "#" + user.UserEmail;
            }
            return msg;
        }

        private void DoLeaveGroup(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            DBFunction dbf = new DBFunction();
            dbf.LeaveGroup(int.Parse(analy.GetParaIndexOf(1)), analy.GetParaIndexOf(0));
        }

        private string DoFindUsers(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            DBFunction dbf = new DBFunction();
            List<UserInfo> list = dbf.FindUserByName(analy.GetParaIndexOf(0).Trim());
            string msg = "Server#" + OpType.FIND_USERS_RES;
            foreach (UserInfo user in list)
            {
                msg += "#" + user.UserID;
                msg += "#" + user.UserName;
                msg += "#" + user.UserPassword;
                msg += "#" + (user.UserSex == true ? 1 : 0);
                msg += "#" + user.UserAge.ToString();
                msg += "#" + user.UserAddress;
                msg += "#" + user.UserHeadpic;
                msg += "#" + user.UserCreateDate.ToString("yyyy-MM-dd HH:mm:ss");
                msg += "#" + user.UserEmail;
            }
            return msg;
        }

        private string DoFindGroups(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            DBFunction dbf = new DBFunction();
            List<GroupInfo> list = dbf.FindGroupByName(analy.GetParaIndexOf(0).Trim());
            string msg = "Server#" + OpType.FIND_GROUPS_RES;
            foreach (GroupInfo group in list)
            {
                msg += "#" + group.GroupID.ToString();
                msg += "#" + group.GroupName;
                msg += "#" + group.GroupCreator;
                msg += "#" + group.GroupCreateDate.ToString("yyyy-MM-dd HH:mm:ss");
                msg += "#" + group.GroupLogo;
            }
            return msg;
        }

        private string DoIsFriend(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            DBFunction dbf = new DBFunction();
            string msg = "Server#" + OpType.IS_FRIEND_RES;
            msg += "#"+(dbf.IsFriend(analy.GetParaIndexOf(0), analy.GetParaIndexOf(1))==true?1:0);
            return msg;
        }

        private string DoIsInGroup(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            DBFunction dbf = new DBFunction();
            string msg = "Server#" + OpType.IS_IN_GROUP_RES;
            msg += "#" + (dbf.IsInTheGroup(int.Parse(analy.GetParaIndexOf(1)),analy.GetParaIndexOf(0))==true?1:0);
            return msg;
        }

    }
}
