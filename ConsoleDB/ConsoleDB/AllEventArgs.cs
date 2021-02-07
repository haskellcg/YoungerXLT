using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleDB
{
    //注册回复
    class RegisterResEventArgs : EventArgs
    {
        private string _userID;
        public string userID
        {
            get{return _userID;}
            set{_userID=value;}
        }
        public RegisterResEventArgs(string userID)
        {
            this.userID = userID;
        }
        
    }
    //登陆回复
    class LoginResEventArgs : EventArgs
    {
        private bool _isSuccess;
        public bool isSuccess 
        {
            get { return _isSuccess; }
            set { _isSuccess = value; }
        }
        public LoginResEventArgs(bool isSuccess)
        {
            this.isSuccess = isSuccess;
        }
    }
    //好友登陆提示
    class InformFriendsEventArgs : EventArgs
    {
        private string _userID;
        public string userID
        {
            get { return _userID; }
            set { _userID = value; }
        }
        public InformFriendsEventArgs(string userID)
        {
            this.userID = userID;
        }
    }
    //获得所属群组信息
    class AllGroupsResEventAgrs : EventArgs
    {
        private List<GroupInfo> _allGroups;
        public List<GroupInfo> allGroups
        {
            get { return _allGroups; }
            set { _allGroups = value; }
        }

        public AllGroupsResEventAgrs(ref List<GroupInfo> allGroups)
        {
            this.allGroups = allGroups;
        }
    }
    //获得所有好友信息
    class AllFriendsResEventArgs : EventArgs
    {
        private List<UserInfo> _allFriends;
        public List<UserInfo> allFriends
        {
            get { return _allFriends; }
            set { _allFriends = value; }
        }
        public AllFriendsResEventArgs(ref List<UserInfo> allFriends)
        {
            this.allFriends = allFriends;
        }
    }
    //获得所有在线好友
    class AllOnLineFriendResEventArgs : EventArgs
    {
        private List<UserInfo> _allOnLineFriends;
        public List<UserInfo> allOnLineFriends
        {
            get { return _allOnLineFriends; }
            set { _allOnLineFriends = value; }
        }

        public AllOnLineFriendResEventArgs(ref List<UserInfo> allOnLineFriends)
        {
            this.allOnLineFriends = allOnLineFriends;
        }
    }
    //获得所有收到的好友请求
    class AllFriendRequestsRecResEventArgs : EventArgs
    {
        private List<Relationship> _allFriendRequestsRec;
        public List<Relationship> allFriendRequestsRec
        {
            get { return _allFriendRequestsRec; }
            set { _allFriendRequestsRec = value; }
        }
        public AllFriendRequestsRecResEventArgs(ref List<Relationship> allFriendRequestsRec)
        {
            this.allFriendRequestsRec = allFriendRequestsRec;
        }
    }
    //获得所有发出的好友请求
    class AllFriendRequestsSedResEventArgs : EventArgs
    {
        private List<Relationship> _allFriendRequestsSed;
        public List<Relationship> allFriendRequestsSed
        {
            get { return _allFriendRequestsSed; }
            set { _allFriendRequestsSed = value; }
        }
        public AllFriendRequestsSedResEventArgs(ref List<Relationship> allFriendRequestsSed)
        {
            this.allFriendRequestsSed = allFriendRequestsSed;
        }
    }
    //获得所有离线消息
    class AllOffLineMsgResEventArgs : EventArgs
    {
        private List<OffLineMessages> _allOffLineMsg;
        public List<OffLineMessages> allOffLineMsg
        {
            get { return _allOffLineMsg; }
            set { _allOffLineMsg = value; }
        }
        public AllOffLineMsgResEventArgs(ref List<OffLineMessages> allOffLineMsg)
        {
            this.allOffLineMsg = allOffLineMsg;
        }
    }
    //获得好友IP
    class FriendIPResEventArgs : EventArgs
    {

        private string _userIPAddress;
        public string userIPAddress
        {
            get { return _userIPAddress; }
            set { _userIPAddress = value; }
        }
        private int _userPort;
        public int userPort
        {
            get { return _userPort; }
            set { _userPort = value; }
        }
        public FriendIPResEventArgs(string userIPAddress,int userPort)
        {
            this.userIPAddress = userIPAddress;
            this.userPort = userPort;
        }
    }
    //发好友请求的服务器回复
    class SendFriendRequestResEventArgs : EventArgs
    {

        private int _relatID;
        public int relatID
        {
            get { return _relatID; }
            set { _relatID = value; }
        }
        public SendFriendRequestResEventArgs(int relatID)
        {
            this.relatID = relatID;
        }
    }
    //收到好友请求的提示
    class SendFriendequestNoteEventArgs : EventArgs
    {
        private string _user1;
        public string user1
        {
            get { return _user1; }
            set { _user1 = value; }
        }
        private string _user2;
        public string user2
        {
            get { return _user2; }
            set { _user2 = value; }
        }
        public SendFriendequestNoteEventArgs(string user1,string user2)
        {
            this.user1 = user1;
            this.user2 = user2;
        }
    }
    //好友请求被接受的提示
    class AgreeFriendRequestResEventArgs : EventArgs
    {
        private string _user1;
        public string user1
        {
            get { return _user1; }
            set { _user1 = value; }
        }
        private string _user2;
        public string user2
        {
            get { return _user2; }
            set { _user2 = value; }
        }

        public AgreeFriendRequestResEventArgs(string user1, string user2)
        {
            this.user1 = user1;
            this.user2 = user2;
        }
    }
    //好友请求被拒绝的提示
    class DisagreeFriendRequestResEventArgs:EventArgs
    {
        private string _user1;
        public string user1
        {
            get { return _user1; }
            set { _user1 = value; }
        }
        private string _user2;
        public string user2
        {
            get { return _user2; }
            set { _user2 = value; }
        }

        public DisagreeFriendRequestResEventArgs(string user1, string user2)
        {
            this.user1 = user1;
            this.user2 = user2;
        }
    }
    //创建群组的服务器回复
    class CreateGroupResEventArgs:EventArgs
    {
        private int _groupID;
        public int groupID
        {
            get { return _groupID; }
            set { _groupID = value; }
        }
        private string _groupName;
        public string groupName
        {
            get { return _groupName; }
            set { _groupName = value; }
        }
        public CreateGroupResEventArgs(int groupID,string groupName)
        {
            this.groupID = groupID;
            this.groupName = groupName;
        }
    }
    //发送离线消息的服务器回复
    class SendOffLineMsgResEventArgs:EventArgs
    {
        private int _msgID;
        public int msgID
        {
            get { return _msgID; }
            set { _msgID = value; }
        }
        public SendOffLineMsgResEventArgs(int msgID)
        {
            this.msgID = msgID;
        }
    }
    //请求加入群组的服务器回复
    class JoinGroupResEventArgs:EventArgs
    {
        private int _entityID;
        public int entityID
        {
            get { return _entityID; }
            set { _entityID = value; }
        }
        public JoinGroupResEventArgs(int entityID)
        {
            this.entityID = entityID;
        }
    }
    //请求加入群组对管理员的提示
    class JoinGroupNoteEventArgs : EventArgs
    {
        private string _userID;
        public string userID
        {
            get { return _userID; }
            set { _userID = value; }
        }
        private int _groupID;
        public int groupID
        {
            get { return _groupID; }
            set { _groupID = value; }
        }
        public JoinGroupNoteEventArgs(string userID, int groupID)
        {
            this.userID = userID;
            this.groupID = groupID;
        }
    }
    //获得所有加入群组的请求
    class AllGroupRequestResEventArgs : EventArgs
    {
        private List<GroupEntity> _allGroupRequest;
        public List<GroupEntity> allGroupRequest
        {
            get { return _allGroupRequest; }
            set { _allGroupRequest = value; }
        }

        public AllGroupRequestResEventArgs(ref List<GroupEntity> allGroupRequest)
        {
            this.allGroupRequest = allGroupRequest;
        }
    }
    //同意加入群组的提示
    class AgreeJoinGroupResEventArgs : EventArgs
    {
        private string _userID;
        public string userID
        {
            get { return _userID; }
            set { _userID = value; }
        }
        private int _groupID;
        public int groupID
        {
            get { return _groupID; }
            set { _groupID = value; }
        }

        public AgreeJoinGroupResEventArgs(string userID, int groupID)
        {
            this.userID = userID;
            this.groupID = groupID;
        }
    }
    //拒绝加入群租的提示
    class DisagreeJoinGroupResEventArgs : EventArgs
    {
        private string _userID;
        public string userID
        {
            get { return _userID; }
            set { _userID = value; }
        }
        private int _groupID;
        public int groupID
        {
            get { return _groupID; }
            set { _groupID = value; }
        }

        public DisagreeJoinGroupResEventArgs(string userID, int groupID)
        {
            this.userID = userID;
            this.groupID = groupID;
        }
    }
    //获得用户信息
    class UserInfoResEventArgs : EventArgs
    {
        private UserInfo _user;
        public UserInfo user
        {
            get { return _user; }
            set { _user = value; }
        }

        public UserInfoResEventArgs(ref UserInfo user)
        {
            this.user = user;
        }
    }
    //获得群组信息
    class GroupInfoResEventArgs : EventArgs
    {
        private GroupInfo _group;
        public GroupInfo group
        {
            get { return _group; }
            set { _group = value; }
        }
        public GroupInfoResEventArgs(ref GroupInfo group)
        {
            this.group = group;
        }
    }
    //发送网络文件的回复
    class SendNetFileResEventArgs : EventArgs
    {
        private string _user1;
        public string user1
        {
            get { return _user1; }
            set { _user1 = value; }
        }
        private string _user2;
        public string user2
        {
            get { return _user2; }
            set { _user2 = value; }
        }
        private string _fileName;
        public string fileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }
        private string _fileContent;
        public string fileContent
        {
            get { return _fileContent; }
            set { _fileContent= value; }
        }
        public SendNetFileResEventArgs(string user1, string user2, string fileName, string fileContent)
        {
            this.user1 = user1;
            this.user2 = user2;
            this.fileName = fileName;
            this.fileContent = fileContent;
        }
    }
    //发送群组消息的提示
    class SendGroupMsgResEventArgs : EventArgs
    {
        private string _userID;
        public string userID
        {
            get { return _userID; }
            set { _userID = value; }
        }
        private string _userName;
        public string userName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        private int _groupID;
        public int groupID
        {
            get { return _groupID; }
            set { _groupID = value; }
        }
        private string _msg;
        public string msg
        {
            get { return _msg; }
            set { _msg = value; }
        }

        public SendGroupMsgResEventArgs(string userID, string userName,int groupID, string msg)
        {
            this.userID = userID;
            this.userName = userName;
            this.groupID = groupID;
            this.msg = msg;
        }
    }
    //获得群组的成员
    class MembersInGroupResEventArgs:EventArgs
    {
        private List<UserInfo> _membersInGroup;
        public List<UserInfo> membersInGroup
        {
            get {return _membersInGroup;}
            set {_membersInGroup=value;}
        }

        public MembersInGroupResEventArgs(ref List<UserInfo> membersInGroup)
        {
            this.membersInGroup=membersInGroup;
        }
    }
    //聊天消息
    class CommunicationEventArgs : EventArgs
    {
        private string _msg;
        public string msg
        {
            get { return _msg; }
            set { _msg = value; }
        }
        public CommunicationEventArgs(string msg)
        {
            this.msg = msg;
        }
    }
    //查找用户
    class FindUsersResEventArgs : EventArgs
    {
        private List<UserInfo> _userList;
        public List<UserInfo> userList
        {
            get { return _userList; }
            set { _userList = value; }
        }
        public FindUsersResEventArgs(ref List<UserInfo> userList)
        {
            this.userList = userList;
        }
    }
    //查找群组
    class FindGroupsResEventArgs : EventArgs
    {
        private List<GroupInfo> _groupList;
        public List<GroupInfo> groupList
        {
            get { return _groupList; }
            set { _groupList = value; }
        }

        public FindGroupsResEventArgs(ref List<GroupInfo> groupList)
        {
            this.groupList = groupList;
        }

    }
    //是否为好友
    class IsFriendResEventArgs : EventArgs
    {
        private bool _isFriend;
        public bool isFriend
        {
            get { return _isFriend; }
            set { _isFriend = value; }
        }
        public IsFriendResEventArgs(bool isFriend)
        {
            this.isFriend = isFriend;
        }
    }
    //是否为群组成员
    class IsGroupMemberResEventArgs : EventArgs
    {
        private bool _isGroupMember;
        public bool isGroupMember
        {
            get { return _isGroupMember; }
            set { _isGroupMember = value; }
        }
        public IsGroupMemberResEventArgs(bool isGroupMember)
        {
            this.isGroupMember = isGroupMember;
        }
    }
    //用户注销的提示
    class LogoutResEventArgs : EventArgs
    {
        private string _userID;
        public string userID
        {
            get { return _userID; }
            set { _userID = value; }
        }
        public LogoutResEventArgs(string userID)
        {
            this.userID = userID;        
        }
    }
}
