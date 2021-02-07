using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleDB
{
    class ClientFunction
    {
        public Object DistributeResults(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            switch (analy.GetKindOfReq())
            {
                case OpType.LOGIN_RES:
                    return GetLogicValue(formatString);
                case OpType.ALL_GROUPS_RES:
                    return GetGroupList(formatString);
                case OpType.ALL_FRIENDS_RES:
                    return GetUserList(formatString);
                case OpType.ALL_ONLINE_FRIENDS_RES:
                    return GetUserList(formatString);
                case OpType.ALL_FRIEND_REQUESTS_REC_RES:
                    return GetRelationshipList(formatString);
                case OpType.ALL_FRIEND_REQUESTS_SED_RES:
                    return GetRelationshipList(formatString);
                case OpType.ALL_OFFLINE_MSG_RES:
                    return GetOffLineMessagesList(formatString);
                case OpType.SEND_FRIEND_REQUEST_RES:
                    return GetAutoIncNumber(formatString);
                case OpType.SEND_OFFLINE_MSG_RES:
                    return GetAutoIncNumber(formatString);
                case OpType.CREATE_GROUP_RES:
                    return GetAutoIncNumber(formatString);
                case OpType.JOIN_GROUP_RES:
                    return GetAutoIncNumber(formatString);
                case OpType.ALL_GROUP_REQUEST_RES:
                    return GetGroupEntityList(formatString);
                case OpType.USER_INFO_RES:
                    return GetUserInfo(formatString);
                case OpType.GROUP_INFO_RES:
                    return GetGroupInfo(formatString);
                case OpType.MEMBERS_IN_GROUP_RES:
                    return GetUserList(formatString);
                case OpType.FIND_USERS_RES:
                    return GetUserList(formatString);
                case OpType.FIND_GROUPS_RES:
                    return GetGroupList(formatString);
                case OpType.IS_FRIEND_RES:
                    return GetLogicValue(formatString);
                case OpType.IS_IN_GROUP_RES:
                    return GetLogicValue(formatString);


                default:
                    break;
            }

            return null; ;
        }


        /*######################################################################*/
        /* 下面的函数主要是处理格式化的字符串                                   */
        /*                       把他们封装为需要的参数                         */
        /*                             为客户端提供参数,操作码部分共客户端使用  */
        /*         例如:Server#1#1 --->>>>    true                              */
        /*######################################################################*/

        private bool GetLogicValue(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            return (int.Parse(analy.GetParaIndexOf(0)) == 1 ? true : false);
        }

        private int GetAutoIncNumber(string formatstring)
        {
            AnalysisStr analy = new AnalysisStr(formatstring);
            return int.Parse(analy.GetParaIndexOf(0));
        }

        private List<GroupInfo> GetGroupList(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            List<GroupInfo> list = new List<GroupInfo>();
            int paraIndex = 0;
            while (analy.GetParaIndexOf(paraIndex) != null)
            {
                GroupInfo groupTemp = new GroupInfo();
                groupTemp.GroupID = int.Parse(analy.GetParaIndexOf(paraIndex++));
                groupTemp.GroupName = analy.GetParaIndexOf(paraIndex++);
                groupTemp.GroupCreator = analy.GetParaIndexOf(paraIndex++);
                groupTemp.GroupCreateDate = DateTime.Parse(analy.GetParaIndexOf(paraIndex++));
                groupTemp.GroupLogo = analy.GetParaIndexOf(paraIndex++);
                list.Add(groupTemp);
            }
            return list;

        }

        private List<UserInfo> GetUserList(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            List<UserInfo> list = new List<UserInfo>();
            int paraIndex = 0;
            while (analy.GetParaIndexOf(paraIndex) != null)
            {
                UserInfo userTemp = new UserInfo();
                userTemp.UserID = analy.GetParaIndexOf(paraIndex++);
                userTemp.UserName = analy.GetParaIndexOf(paraIndex++);
                userTemp.UserPassword=analy.GetParaIndexOf(paraIndex++);
                userTemp.UserSex = int.Parse(analy.GetParaIndexOf(paraIndex++)) == 1 ? true : false;
                userTemp.UserAge = int.Parse(analy.GetParaIndexOf(paraIndex++));
                userTemp.UserAddress = analy.GetParaIndexOf(paraIndex++);
                userTemp.UserHeadpic = analy.GetParaIndexOf(paraIndex++);
                userTemp.UserCreateDate = DateTime.Parse(analy.GetParaIndexOf(paraIndex++));
                userTemp.UserEmail = analy.GetParaIndexOf(paraIndex++);

                list.Add(userTemp);

            }

            return list;
        }

        private List<Relationship> GetRelationshipList(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            List<Relationship> list = new List<Relationship>();
            int paraIndex = 0;
            while (analy.GetParaIndexOf(paraIndex) != null)
            {
                Relationship relatTemp = new Relationship();
                relatTemp.RelatID = int.Parse(analy.GetParaIndexOf(paraIndex++));
                relatTemp.RelatUser1 = analy.GetParaIndexOf(paraIndex++);
                relatTemp.RelatUser2 = analy.GetParaIndexOf(paraIndex++);
                relatTemp.RelatConfirm = int.Parse(analy.GetParaIndexOf(paraIndex++)) == 1 ? true : false;
                relatTemp.RelatCreateDate = DateTime.Parse(analy.GetParaIndexOf(paraIndex++));

                list.Add(relatTemp);
            }
            return list;
        }

        private List<GroupEntity> GetGroupEntityList(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            List<GroupEntity> list = new List<GroupEntity>();
            int paraIndex = 0;
            while (analy.GetParaIndexOf(paraIndex) != null)
            {
                GroupEntity entityTemp = new GroupEntity();
                entityTemp.EntityID = int.Parse(analy.GetParaIndexOf(paraIndex++));
                entityTemp.EntityGroupID = int.Parse(analy.GetParaIndexOf(paraIndex++));
                entityTemp.EntityMemID = analy.GetParaIndexOf(paraIndex++);
                entityTemp.EntityConfirm = int.Parse(analy.GetParaIndexOf(paraIndex++)) == 1 ? true : false;
                entityTemp.EntityCreateDate = DateTime.Parse(analy.GetParaIndexOf(paraIndex++));

                list.Add(entityTemp);
            }

            return list;
        }

        //这里解析遇到问题：输入的信息不应该包含'#'否则发生解析错误，可以在客户端防止用户输入该符号
        private List<OffLineMessages> GetOffLineMessagesList(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            List<OffLineMessages> list = new List<OffLineMessages>();
            int paraIndex = 0;
            while (analy.GetParaIndexOf(paraIndex) != null)
            {
                OffLineMessages msgTemp = new OffLineMessages();
                msgTemp.MessageID = int.Parse(analy.GetParaIndexOf(paraIndex++));
                msgTemp.MessageUser1 = analy.GetParaIndexOf(paraIndex++);
                msgTemp.MessageUser2 = analy.GetParaIndexOf(paraIndex++);
                msgTemp.MessageContent = analy.GetParaIndexOf(paraIndex++);
                msgTemp.MessageSendTime = DateTime.Parse(analy.GetParaIndexOf(paraIndex++));
                list.Add(msgTemp);
            }
            return list;
        }

        private UserInfo GetUserInfo(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            UserInfo user = new UserInfo();
            int paraIndex = 0;
            if (analy.GetParaIndexOf(paraIndex) != null)
            {
                user.UserID = analy.GetParaIndexOf(paraIndex++);
                user.UserName = analy.GetParaIndexOf(paraIndex++);
                user.UserPassword = analy.GetParaIndexOf(paraIndex++);
                user.UserSex = int.Parse(analy.GetParaIndexOf(paraIndex++)) == 1 ? true : false;
                user.UserAge = int.Parse(analy.GetParaIndexOf(paraIndex++));
                user.UserAddress = analy.GetParaIndexOf(paraIndex++);
                user.UserHeadpic = analy.GetParaIndexOf(paraIndex++);
                user.UserCreateDate = DateTime.Parse(analy.GetParaIndexOf(paraIndex++));
                user.UserEmail = analy.GetParaIndexOf(paraIndex++);
                return user;
            }

            return null;

            
        }

        private GroupInfo GetGroupInfo(string formatString)
        {
            AnalysisStr analy = new AnalysisStr(formatString);
            GroupInfo group = new GroupInfo();
            int paraIndex = 0;
            if (analy.GetParaIndexOf(paraIndex) != null)
            {
                group.GroupID = int.Parse(analy.GetParaIndexOf(paraIndex++));
                group.GroupName = analy.GetParaIndexOf(paraIndex++);
                group.GroupCreator = analy.GetParaIndexOf(paraIndex++);
                group.GroupCreateDate = DateTime.Parse(analy.GetParaIndexOf(paraIndex++));
                group.GroupLogo = analy.GetParaIndexOf(paraIndex++);
                return group;
            }

            return null;
           
        }


    }
}
