using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySQLDriverCS;

namespace ConsoleDB
{
    class DBFunction
    {

        //根据用户ID查询该用户是否存在
        public bool UserExists(string userID)
        {
            DBConnection dbc = new DBConnection();
            bool flag;
            dbc.GetConnection();
            string cmdText = "select * from younger_user_info where user_id="+"'"+userID+"'";
            dbc.GetDataReader(cmdText);
            if (dbc.dataReader.Read())
                flag = true;
            else 
                flag = false;
            dbc.Close();
            return flag;
        }
        //存入用户信息
        public void InsertUserInfo(UserInfo user)
        {
            DBConnection dbc = new DBConnection();
            dbc.GetConnection();
            string cmdText="insert younger_user_info(user_id,user_name,user_password,user_sex,user_age,user_address,user_headpic,user_createdate,user_email)"+
                         " values(" + "'" + user.UserID + "'" + ",'" + user.UserName + "'" + ",'" + user.UserPassword + "'," + (user.UserSex == true ? 1 : 0) + "," + user.UserAge
                         + ",'" + user.UserAddress + "'" + ",'" + user.UserHeadpic + "'" + ",'" + user.UserCreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + ",'" + user.UserEmail + "')";
            dbc.ExecuteNonQuery(cmdText);
        }
        //根据用户ID修改个人信息
        public void AlterUserInfo(UserInfo user)
        {
            DBConnection dbc = new DBConnection();
            dbc.GetConnection();
            string cmdText = "update younger_user_info set user_name='"+user.UserName+"',user_password='"+user.UserPassword+"',user_sex="+(user.UserSex==true?1:0)+",user_age="+user.UserAge+",user_address='"+user.UserAddress+"',user_headpic='"+user.UserHeadpic+"',user_createdate='"+user.UserCreateDate.ToString("yyyy-MM-dd HH:mm:ss")+"',user_email='"+user.UserEmail+"' where user_id='"+user.UserID+"'";
            dbc.ExecuteNonQuery(cmdText);
        }
        //根据用户ID返回用户的信息
        public UserInfo GetUserInfo(string userID)
        {
            DBConnection dbc = new DBConnection();
            UserInfo userTemp = new UserInfo();
            dbc.GetConnection();
            string cmdText = "select * from younger_user_info where user_id="+"'"+userID+"'";
            dbc.GetDataReader(cmdText);
            while (dbc.dataReader.Read())
            {
                userTemp.UserID = userID;
                userTemp.UserName = dbc.dataReader.GetString(1);
                userTemp.UserPassword = dbc.dataReader.GetString(2);
                userTemp.UserSex = dbc.dataReader.GetInt16(3) == 1 ? true : false;
                userTemp.UserAge = dbc.dataReader.GetInt16(4);
                userTemp.UserAddress = dbc.dataReader.GetString(5);
                userTemp.UserHeadpic = dbc.dataReader.GetString(6);
                userTemp.UserCreateDate = dbc.dataReader.GetDateTime(7);
                userTemp.UserEmail = dbc.dataReader.GetString(8);
            }
            dbc.dataReader.Close();

            return userTemp;
        }
        //随机产生11位的用户ID，且不重复
        public string GeneralUserID()
        {
            DBConnection dbc = new DBConnection();

            string temp;

            do
            {
                Random rd = new Random(Environment.TickCount);
                DateTime nowDate = DateTime.Now;
                temp = nowDate.Month.ToString("D2") + nowDate.Day.ToString("D2") + nowDate.Hour.ToString("D2") + nowDate.Minute.ToString("D2") + rd.Next(100, 999);
            } while (UserExists(temp));
               
            
            return temp;

        }
        //注册函数:输入用户信息，返回用户的ID（用于登陆）
        public string Register(UserInfo user)
        {
            user.UserID = GeneralUserID();
            InsertUserInfo(user);
            return user.UserID;
        }
        //登陆函数:根据输入信息，返回成功与否
        public bool Login(string userID, string userPassword)
        {
            if (UserExists(userID))
                if (GetUserInfo(userID).UserPassword.Equals(userPassword))
                    return true;
            return false;
        }

        //存入好友关系,返回自增字段的值
        public int InsertRelationship(Relationship relat)
        {
            DBConnection dbc = new DBConnection();
            dbc.GetConnection();
            string cmdText = "insert younger_relat_info(relat_user1,relat_user2,relat_confirm,relat_createdate) "+
                "values('" + relat.RelatUser1 + "'" + ",'" + relat.RelatUser2 + "'," + (relat.RelatConfirm==true?1:0) + ",'" +relat.RelatCreateDate.ToString("yyyy-MM-dd HH:mm:ss")+"'" +")";
            return dbc.ExecuteNonQueryForInc(cmdText);
        }
        //参看好友关系ID是否存在
        public bool RelatExist(int relatID)
        {
            DBConnection dbc = new DBConnection();

            bool flag = false;
            dbc.GetConnection();
            string cmdText="select * from younger_relat_info where relat_id="+relatID;
            dbc.GetDataReader(cmdText);
            if (dbc.dataReader.Read())
                flag = true;

            return flag;
        }
        //根据ID查询好友关系
        public Relationship GetRelationship(int relatID)
        {
            DBConnection dbc = new DBConnection();

            Relationship relatTemp = new Relationship();
            dbc.GetConnection();
            string cmdText = "select * from younger_relat_info where relat_id="+relatID;
            dbc.GetDataReader(cmdText);
            while (dbc.dataReader.Read())
            {
                relatTemp.RelatID = relatID;
                relatTemp.RelatUser1 = dbc.dataReader.GetString(1);
                relatTemp.RelatUser2 = dbc.dataReader.GetString(2);
                relatTemp.RelatConfirm = dbc.dataReader.GetInt16(3) == 1 ? true : false;
                relatTemp.RelatCreateDate = dbc.dataReader.GetDateTime(4);
            }
            return relatTemp;
        }
        //根据给出的用户ID确定是否为好友
        public bool IsFriend(string user1, string user2)
        {
            DBConnection dbc = new DBConnection();

            bool flag = false;
            dbc.GetConnection();
            string cmdText1 = "select * from younger_relat_info where relat_user1=" + "'" + user1 + "'&&" + "relat_user2=" + "'" + user2 + "'&&" + "relat_confirm=1";
            dbc.GetDataReader(cmdText1);
            if (dbc.dataReader.Read())
            {
                flag = true;
                return flag;
            }
            string cmdText2 = "select * from younger_relat_info where relat_user1=" + "'" + user2 + "'&&" + "relat_user2=" + "'" + user1 + "'&&" + "relat_confirm=1";
            dbc.GetDataReader(cmdText2);
            if (dbc.dataReader.Read())
                flag = true;

            return flag;
        }
        //根据用户ID返回所有好友
        public List<UserInfo> GetFriendsByUserID(string userID)
        {
            DBConnection dbc = new DBConnection();

            List<UserInfo> list = new List<UserInfo>();
            dbc.GetConnection();
            string cmdText1 = "select relat_user2 from younger_relat_info where relat_user1=" + "'" + userID + "'&&relat_confirm=1";
            dbc.GetDataReader(cmdText1);
            while (dbc.dataReader.Read())
            {
                UserInfo userTemp = new UserInfo();
                userTemp = GetUserInfo(dbc.dataReader.GetString(0));
                list.Add(userTemp);
            }
            string cmdText2 = "select relat_user1 from younger_relat_info where relat_user2=" + "'" + userID + "'&&relat_confirm=1";
            dbc.GetDataReader(cmdText2);
            while (dbc.dataReader.Read())
            {
                UserInfo userTemp = new UserInfo();
                userTemp = GetUserInfo(dbc.dataReader.GetString(0));
                list.Add(userTemp);
            }

            return list;
        }
        //查询是否有用户2对用户1的好友请求
        public bool RequestExists(string user1, string user2)
        {
            DBConnection dbc = new DBConnection();

            bool flag = false;
            dbc.GetConnection();
            string cmdText = "select * from younger_relat_info where relat_user1='" + user1 + "'&&relat_user2='" + user2 + "'&&relat_confirm=0";
            dbc.GetDataReader(cmdText);
            while (dbc.dataReader.Read())
            {
                flag = true;
            }

            return flag;
        }
        //用户2同意用户1的提出好友请求
        public int RequestFriend(string user1, string user2)
        {
            DBConnection dbc = new DBConnection();

            dbc.GetConnection();
            string cmdText = "insert younger_relat_info(relat_user1,relat_user2,relat_confirm,relat_createdate) " +
                "values('" + user1 + "'" + ",'" + user2 + "'," +0+ ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" + ")";
            return dbc.ExecuteNonQueryForInc(cmdText);

        }
        //用户1同意用户2的好友请求
        public void AgreeFriendRequest(string user1, string user2)
        {
            DBConnection dbc = new DBConnection();

            dbc.GetConnection();
            string cmdText = "update younger_relat_info set relat_confirm=1 where relat_user1='"+user1+"'&&relat_user2='"+user2+"'";
            dbc.ExecuteNonQuery(cmdText);
        }
        //用户1拒绝用户2的好友请求
        public void DisagreeFriendRequest(string user1, string user2)
        {
            DBConnection dbc = new DBConnection();

            dbc.GetConnection();
            string cmdText = "delete from younger_relat_info where relat_user1='"+user1+"'&&relat_user2='"+user2+"'";
            dbc.ExecuteNonQuery(cmdText);
        }
        //根据用户ID返回所有的收到的好友请求
        public List<Relationship> RecievedFriendRequests(string userID)
        {
            DBConnection dbc = new DBConnection();

            List<Relationship> list = new List<Relationship>();
            dbc.GetConnection();
            string cmdText = "select * from younger_relat_info where relat_user1='"+userID+"'&&relat_confirm=0";
            dbc.GetDataReader(cmdText);
            while (dbc.dataReader.Read())
            {
                Relationship relatTemp = new Relationship();
                relatTemp.RelatID = dbc.dataReader.GetInt16(0);
                relatTemp.RelatUser1 = dbc.dataReader.GetString(1);
                relatTemp.RelatUser2 = dbc.dataReader.GetString(2);
                relatTemp.RelatConfirm = dbc.dataReader.GetInt16(3) == 1 ? true : false;
                relatTemp.RelatCreateDate = dbc.dataReader.GetDateTime(4);
                list.Add(relatTemp);
            }
            return list;
        }
        //根据用户ID返回所有的提出的好友请求
        public List<Relationship> SentFriendRequests(string userID)
        {
            DBConnection dbc = new DBConnection();

            List<Relationship> list = new List<Relationship>();
            dbc.GetConnection();
            string cmdText = "select * from younger_relat_info where relat_user2='"+userID+"'&&relat_confirm=0";
            dbc.GetDataReader(cmdText);
            while (dbc.dataReader.Read())
            {
                Relationship relatTemp = new Relationship();
                relatTemp.RelatID = dbc.dataReader.GetInt16(0);
                relatTemp.RelatUser1 = dbc.dataReader.GetString(1);
                relatTemp.RelatUser2 = dbc.dataReader.GetString(2);
                relatTemp.RelatConfirm = dbc.dataReader.GetInt16(3) == 1 ? true : false;
                relatTemp.RelatCreateDate = dbc.dataReader.GetDateTime(4);
                list.Add(relatTemp);
            }
            return list;
        }

        
        //创建群组
        public int InsertGroupInfo(GroupInfo group)
        {
            DBConnection dbc = new DBConnection();

            dbc.GetConnection();
            string cmdText = "insert younger_group_info(group_name,group_creator,group_createdate,group_logo) values("+
                            "'"+group.GroupName+"',"+"'"+group.GroupCreator+"',"+"'"+group.GroupCreateDate.ToString("yyyy-MM-dd HH:mm:ss")+"',"+
                            "'"+group.GroupLogo+"'"+")";
            return dbc.ExecuteNonQueryForInc(cmdText);
            
        }
        //根据群组ID获得群组信息
        public GroupInfo GetGroupInfo(int groupID)
        {
            DBConnection dbc = new DBConnection();

            GroupInfo group = new GroupInfo();
            dbc.GetConnection();
            string cmdText = "select * from younger_group_info where group_id="+groupID;
            dbc.GetDataReader(cmdText);
            while (dbc.dataReader.Read())
            {
                group.GroupID = groupID;
                group.GroupName = dbc.dataReader.GetString(1);
                group.GroupCreator=dbc.dataReader.GetString(2);
                group.GroupCreateDate = dbc.dataReader.GetDateTime(3);
                group.GroupLogo = dbc.dataReader.GetString(4);
            }
            return group;
        }
        //修改群组信息
        public void AlterGroupInfo(GroupInfo group)
        {
            DBConnection dbc = new DBConnection();

            dbc.GetConnection();
            string cmdText = "update younger_group_info set group_name='" + group.GroupName + "',group_creator='" + group.GroupCreator + "',group_createdate='" + group.GroupCreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "',group_logo='" + group.GroupLogo + "' where group_id="+group.GroupID;                 
            dbc.ExecuteNonQueryForInc(cmdText);
        }
        //根据用户ID返回所有创建的群
        public List<GroupInfo> CreatedGroups(string userID)
        {
            DBConnection dbc = new DBConnection();
            List<GroupInfo> list = new List<GroupInfo>();
            
            dbc.GetConnection();
            string cmdText = "select * from younger_group_info where group_creator='"+userID+"'";
            dbc.GetDataReader(cmdText);
            while (dbc.dataReader.Read())
            {
                GroupInfo groupTemp = new GroupInfo();
                groupTemp.GroupID = dbc.dataReader.GetInt16(0);
                groupTemp.GroupName = dbc.dataReader.GetString(1);
                groupTemp.GroupCreator = dbc.dataReader.GetString(2);
                groupTemp.GroupCreateDate = dbc.dataReader.GetDateTime(3);
                groupTemp.GroupLogo = dbc.dataReader.GetString(4);

                list.Add(groupTemp);
            }

            return list;
        }
        //根据用户ID返回所有加入的群(除去创建的群)
        public List<GroupInfo> JoinedGroups(string userID)
        {
            DBConnection dbc = new DBConnection();
            List<GroupInfo> list = new List<GroupInfo>();
            dbc.GetConnection();
            string cmdText = "select entity_groupid from younger_entity_info where entity_memid='"+userID+"'";
            dbc.GetDataReader(cmdText);
            while (dbc.dataReader.Read())
            {
                GroupInfo groupTemp = new GroupInfo();
                groupTemp = GetGroupInfo(dbc.dataReader.GetInt16(0));
                list.Add(groupTemp);
            }
            return list;
        }
        //加入群组，在创建者同意后执行该操作
        public int InsertGroupEntity(GroupEntity entity)
        {
            DBConnection dbc = new DBConnection();

            dbc.GetConnection();
            string cmdText = "insert younger_entity_info(entity_groupid,entity_memid,entity_confirm,entity_createdate) values("+
                entity.EntityGroupID+",'"+entity.EntityMemID+"',"+(entity.EntityConfirm==true?1:0)+",'"+entity.EntityCreateDate.ToString("yyyy-MM-dd HH:mm:ss")+"'"+")";
            return dbc.ExecuteNonQueryForInc(cmdText);
        }
        //请求加入群组
        public int RequestJoinGroup(int groupID, string userID)
        {
            DBConnection dbc = new DBConnection();

            dbc.GetConnection();
            string cmdText = "insert younger_entity_info(entity_groupid,entity_memid,entity_confirm,entity_createdate) values(" +
                groupID + ",'" + userID + "'," + 0+ ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" + ")";
            return dbc.ExecuteNonQueryForInc(cmdText);
        }
        //创建者同意用户加入
        public void AgreeJoinGroup(int groupID, string userID)
        {
            DBConnection dbc = new DBConnection();

            dbc.GetConnection();
            string cmdText="update younger_entity_info set entity_confirm=1 where entity_groupid="+groupID+"&&entity_memid='"+userID+"'";
            dbc.ExecuteNonQuery(cmdText);
        }
        //创建者拒绝用户加入
        public void DisagreeJoinGroup(int groupID, string userID)
        {
            DBConnection dbc = new DBConnection();

            dbc.GetConnection();
            string cmdText = "delete from younger_entity_info where entity_groupid=" + groupID + "&&entity_memid='" + userID + "'";
            dbc.ExecuteNonQuery(cmdText);
        }
        //确定是否已经存在用户加入改组的请求
        public bool EntityExists(int groupID, string userID)
        {
            bool flag = false;
            DBConnection dbc = new DBConnection();
            dbc.GetConnection();
            string cmdText = "select * from younger_entity_info where entity_groupid="+groupID+"&&entity_memid='"+userID+"'&&entity_confirm=0";
            dbc.GetDataReader(cmdText);
            if (dbc.dataReader.Read())
            {
                flag = true;
            }
            return flag;
        }
        //根据用户ID以及群组ID确认该用户是否在该群组
        public bool IsInTheGroup(int groupID, string userID)
        {
            DBConnection dbc = new DBConnection();
            bool flag = false;

            dbc.GetConnection();
            string cmdText = "select * from younger_entity_info where entity_groupid="+groupID+"&&entity_memid='"+userID+"'&&entity_confirm=1";
            dbc.GetDataReader(cmdText);

            while (dbc.dataReader.Read())
            {
                flag = true;
            }
            return flag;
        }
        //退出群组，根据用户ID以及群组ID
        public void LeaveGroup(int groupID, string userID)
        {
            DBConnection dbc = new DBConnection();
            dbc.GetConnection();
            string cmdText = "delete from younger_entity_info where entity_groupid="+groupID+"&&entity_memid='"+userID+"'&&entity_confirm=1";
            dbc.ExecuteNonQuery(cmdText);
        }
        //根据群组ID获得该群组的所有成员(除管理员即创建者)
        public List<UserInfo> GetMembersInGroup(int groupID)
        {
            DBConnection dbc = new DBConnection();

            List<UserInfo> list = new List<UserInfo>();
            dbc.GetConnection();
            string cmdText = "select entity_memid from younger_entity_info where entity_groupid="+groupID+"&&entity_confirm=1";
            dbc.GetDataReader(cmdText);
            while(dbc.dataReader.Read())
            {
                UserInfo userTemp = new UserInfo();
                userTemp = GetUserInfo(dbc.dataReader.GetString(0));
                list.Add(userTemp);
            }

            return list;

        }
        //根据群组ID返回该群的所有加入请求
        public List<GroupEntity> JoinGroupRequests(int groupID)
        {
            DBConnection dbc = new DBConnection();
            List<GroupEntity> list = new List<GroupEntity>();

            dbc.GetConnection();
            string cmdText = "select * from younger_entity_info where entity_groupid="+groupID+"&&entity_confirm=0";
            dbc.GetDataReader(cmdText);
            while (dbc.dataReader.Read())
            {
                GroupEntity entityTemp = new GroupEntity();
                entityTemp.EntityID = dbc.dataReader.GetInt16(0);
                entityTemp.EntityGroupID = dbc.dataReader.GetInt16(1);
                entityTemp.EntityMemID = dbc.dataReader.GetString(2);
                entityTemp.EntityConfirm = dbc.dataReader.GetInt16(3) == 1 ? true : false;
                entityTemp.EntityCreateDate = dbc.dataReader.GetDateTime(4);
                list.Add(entityTemp);

            }

            return list;

        }

       
        //存入离线消息
        public int InsertOffLineMessage(OffLineMessages msg)
        {
            DBConnection dbc = new DBConnection();
            dbc.GetConnection();
            string cmdText = "insert younger_message_info(message_user1,message_user2,message_content,message_senddate) values("+
                            "'"+msg.MessageUser1+"',"+"'"+msg.MessageUser2+"',"+"'"+msg.MessageContent+"',"+"'"+msg.MessageSendTime.ToString("yyyy-MM-dd HH:mm:ss")+"')";
            return dbc.ExecuteNonQueryForInc(cmdText);
        }
        //根据消息ID删除离线消息
        public void DeleteOffLineMessage(int msgID)
        {
            DBConnection dbc = new DBConnection();
            dbc.GetConnection();
            string cmdText = "delete from younger_message_info where message_id="+msgID;
            dbc.ExecuteNonQuery(cmdText);
        }
        //根据消息ID获得消息的详细信息
        public OffLineMessages GetOffLineMessage(int msgID)
        {
            DBConnection dbc = new DBConnection();
            OffLineMessages msg = new OffLineMessages();
            dbc.GetConnection();
            string cmdText = "select * from younger_message_info where message_id="+msgID;
            dbc.GetDataReader(cmdText);
            while (dbc.dataReader.Read())
            {
                msg.MessageID = msgID;
                msg.MessageUser1 = dbc.dataReader.GetString(1);
                msg.MessageUser2 = dbc.dataReader.GetString(2);
                msg.MessageContent = dbc.dataReader.GetString(3);
                msg.MessageSendTime = dbc.dataReader.GetDateTime(4);
            }

            return msg;
        }
        //用户2向用户1发出离线消息
        public int SendOffLineMessage(string user1, string user2,string content)
        {
            DBConnection dbc = new DBConnection();
            dbc.GetConnection();
            string cmdText = "insert younger_message_info(message_user1,message_user2,message_content,message_senddate) values(" +
                           "'" + user1 + "'," + "'" + user2 + "'," + "'" + content + "'," + "'" +DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            return dbc.ExecuteNonQueryForInc(cmdText);
        }
        //用户1接收用户2的离线消息
        public void RecieveOffLineMessage(int messageID)
        {
            DBConnection dbc = new DBConnection();
            dbc.GetConnection();
            string cmdText = "delete from younger_message_info where message_id="+messageID;
            dbc.ExecuteNonQuery(cmdText);
        }
        //获得用户1的所有收到的离线消息
        public List<OffLineMessages> RecievedOffLineMessages(string userID)
        {
            List<OffLineMessages> list = new List<OffLineMessages>();
            DBConnection dbc = new DBConnection();
            dbc.GetConnection();
            string cmdText = "select * from younger_message_info where message_user1='"+userID+"' order by message_senddate desc";
            dbc.GetDataReader(cmdText);
            while (dbc.dataReader.Read())
            {
                OffLineMessages msgTemp = new OffLineMessages();
                msgTemp.MessageID = dbc.dataReader.GetInt16(0);
                msgTemp.MessageUser1 = dbc.dataReader.GetString(1);
                msgTemp.MessageUser2 = dbc.dataReader.GetString(2);
                msgTemp.MessageContent = dbc.dataReader.GetString(3);
                msgTemp.MessageSendTime = dbc.dataReader.GetDateTime(4);
                list.Add(msgTemp);
            }

            return list;
        }
        //获得用户2的所有发出的离线消息
        public List<OffLineMessages> SentOffLineMessages(string userID)
        {
            List<OffLineMessages> list = new List<OffLineMessages>();
            DBConnection dbc = new DBConnection();
            dbc.GetConnection();
            string cmdText = "select * from younger_message_info where message_user2='" + userID + "' order by message_senddate desc";
            dbc.GetDataReader(cmdText);
            while (dbc.dataReader.Read())
            {
                OffLineMessages msgTemp = new OffLineMessages();
                msgTemp.MessageID = dbc.dataReader.GetInt16(0);
                msgTemp.MessageUser1 = dbc.dataReader.GetString(1);
                msgTemp.MessageUser2 = dbc.dataReader.GetString(2);
                msgTemp.MessageContent = dbc.dataReader.GetString(3);
                msgTemp.MessageSendTime = dbc.dataReader.GetDateTime(4);
                list.Add(msgTemp);
            }

            return list;
        }

        //根据输入的字符串返回用户名中包含该字符串的所有用户信息
        public List<UserInfo> FindUserByName(string userName)
        {
            List<UserInfo> list=new List<UserInfo>();
            DBConnection dbc = new DBConnection();
            dbc.GetConnection();
            string cmdText = "select * from younger_user_info where user_name like '%"+userName+"%'";
            dbc.GetDataReader(cmdText);
            while (dbc.dataReader.Read())
            {
                UserInfo userTemp = new UserInfo();
                userTemp.UserID = dbc.dataReader.GetString(0);
                userTemp.UserName = dbc.dataReader.GetString(1);
                userTemp.UserPassword = dbc.dataReader.GetString(2);
                userTemp.UserSex = dbc.dataReader.GetInt16(3) == 1 ? true : false;
                userTemp.UserAge = dbc.dataReader.GetInt16(4);
                userTemp.UserAddress = dbc.dataReader.GetString(5);
                userTemp.UserHeadpic = dbc.dataReader.GetString(6);
                userTemp.UserCreateDate = dbc.dataReader.GetDateTime(7);
                userTemp.UserEmail = dbc.dataReader.GetString(8);

                list.Add(userTemp);
            }

            return list;

        }
        //根据输入的字符串返回群组名包含该字符串的所有群组信息
        public List<GroupInfo> FindGroupByName(string groupName)
        {
            List<GroupInfo> list = new List<GroupInfo>();
            DBConnection dbc = new DBConnection();
            dbc.GetConnection();
            string cmdText = "select * from younger_group_info where group_name like '%" + groupName + "%'";
            dbc.GetDataReader(cmdText);
            while (dbc.dataReader.Read())
            {
                GroupInfo groupTemp = new GroupInfo();
                groupTemp.GroupID = dbc.dataReader.GetInt16(0);
                groupTemp.GroupName = dbc.dataReader.GetString(1);
                groupTemp.GroupCreator = dbc.dataReader.GetString(2);
                groupTemp.GroupCreateDate = dbc.dataReader.GetDateTime(3);
                groupTemp.GroupLogo = dbc.dataReader.GetString(4);

                list.Add(groupTemp);
            }

            return list;
        }
    }
}
