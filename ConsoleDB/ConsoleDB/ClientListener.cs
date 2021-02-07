using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using System.Collections.Generic;

namespace ConsoleDB
{
    class ClientListener
    {
        private  int BufferSize=8192;
		private  string IP="127.0.0.1";
		private  int Port=8600;
        private  int QueenLength = 20;
		public TcpListener listener;
		private TcpClient remoteClient;
		private NetworkStream streamToClient;
        private IPEndPoint endPoint;
        public ClientFunction clientFunction;

        public event RegisterResHandler registerRes;
        public event LoginResHandler loginRes;
        public event InformFriendsHandler informFriend;
        public event AllGroupsResHandler allGroupsRes;
        public event AllFriendsResHandler allFriendsRes;
        public event AllOnLineFriendsResHandler allOnLineFriendsRes;
        public event AllFriendRequestsRecResHandler allFriendRequestsRecRes;
        public event AllFriendRequestSedResHandler allFriendRequestSedRes;
        public event AllOffLineMsgResHandler allOffLineMsgRes;
        public event FriendIPResHandler friendIPRes;
        public event SendFriendRequestResHandler sendFriendRequestRes;
        public event SendFriendRequestNoteHandler sendFriendRequestNote;
        public event AgreeFriendRequestResHandler agreeFriendRequestRes;
        public event DisagreeFriendRequestResHandler disagreeFriendRequestRes;
        public event CreateGroupResHandler createGroupRes;
        public event SendOffLineMsgResHandler sendOffLineMsgRes;
        public event JoinGroupResHandler joinGroupRes;
        public event JoinGroupNoteHandler joinGroupNote;
        public event AllGroupRequestResHandler allGroupRequestRes;
        public event AgreeJoinGroupResHandler agreeJoinGroupRes;
        public event DisagreeJoinGroupResHandler disagreeJoinGroupRes;
        public event UserInfoResHandler userInfoRes;
        public event GroupInfoResHandler groupInfoRes;
        public event SendNetFileResHandler sendNetFileRes;
        public event SendGroupMsgResHandler sendGroupMsgRes;
        public event MemebersInGroupResHandler memberInGroupRes;
        public event CommunicationHandler communication;
        public event FindUsersResHandler findUsersRes;
        public event FindGroupsResHandler findGroupsRes;
        public event IsFriendResHandler isFriendRes;
        public event IsGroupMemberResHandler isGroupMemberRes;
        public event LogoutResHandler logoutRes;


		public ClientListener()
		{
            endPoint = new IPEndPoint(IPAddress.Parse("0.0.0.0"),0);
            clientFunction = new ClientFunction();
        }
        public ClientListener(int _BufferSize, string _IP, int _Port, int _QueenLength)
		{
			BufferSize=_BufferSize;
			IP=_IP;
			Port=_Port;
            QueenLength = _QueenLength;

            endPoint = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0);
            clientFunction = new ClientFunction();
		}	
		public void StartListener()
		{
			IPAddress ipAddress=IPAddress.Parse(IP);
			listener=new TcpListener(ipAddress,Port);
			listener.Start(QueenLength);
		}
		public void GetNetworkStream()
		{
			remoteClient=listener.AcceptTcpClient();
			Console.WriteLine("Client Connected!{0}-->{1}",remoteClient.Client.LocalEndPoint,remoteClient.Client.RemoteEndPoint);
            endPoint.Address = IPAddress.Parse(remoteClient.Client.RemoteEndPoint.ToString().Split(new char[] { ':' })[0]);
            endPoint.Port = int.Parse(remoteClient.Client.RemoteEndPoint.ToString().Split(new char[] { ':' })[1]);
            streamToClient=remoteClient.GetStream();
		}
		public string Communicate()
		{
			string msg="";
			byte[] buffer=new byte[BufferSize];
			int bytesRead=0;
			try{
				    lock(streamToClient){
				    	bytesRead=streamToClient.Read(buffer,0,BufferSize);
				    }
					
			        if(bytesRead==0)
					    throw new Exception("读取到0字节");
			        msg=Encoding.Unicode.GetString(buffer,0,bytesRead);
					
			    }catch(Exception ex){
				}
            return msg;
		}		
		public void ReleaseAll()
		{
			streamToClient.Dispose();
			remoteClient.Close();
		}


        //测试专用
        public void test()
        {
            StartListener();
            while (true)
            {
                GetNetworkStream();
                string msg = Communicate();
                ReleaseAll();
                AnalysisStr analy = new AnalysisStr(msg);
                switch (analy.GetKindOfReq())
                {
                    case OpType.RIGISTER_RES:
                        break;
                    case OpType.LOGIN_RES:
                        break;
                    case OpType.INFORM_FRIENDS:
                        string userID = analy.GetParaIndexOf(0);
                        informFriend(this, new InformFriendsEventArgs(userID));
                        break;
                    case OpType.ALL_GROUPS_RES:
                        List<GroupInfo> listGroups = (List<GroupInfo>)clientFunction.DistributeResults(msg);
                        allGroupsRes(this, new AllGroupsResEventAgrs(ref listGroups));
                        break;
                    case OpType.ALL_FRIENDS_RES:
                        List<UserInfo> listFriends = (List<UserInfo>)clientFunction.DistributeResults(msg);
                        allFriendsRes(this, new AllFriendsResEventArgs(ref listFriends));
                        break;
                    case OpType.ALL_ONLINE_FRIENDS_RES:
                        List<UserInfo> listOnLineFriends = (List<UserInfo>)clientFunction.DistributeResults(msg);
                        allOnLineFriendsRes(this, new AllOnLineFriendResEventArgs(ref listOnLineFriends));
                        break;
                    case OpType.ALL_FRIEND_REQUESTS_REC_RES:
                        List<Relationship> listFriendRequestRec = (List<Relationship>)clientFunction.DistributeResults(msg);
                        allFriendRequestsRecRes(this, new AllFriendRequestsRecResEventArgs(ref listFriendRequestRec));
                        break;
                    case OpType.ALL_FRIEND_REQUESTS_SED_RES:
                        List<Relationship> listFriendRequestSed = (List<Relationship>)clientFunction.DistributeResults(msg);
                        allFriendRequestSedRes(this, new AllFriendRequestsSedResEventArgs(ref listFriendRequestSed));
                        break;
                    case OpType.ALL_OFFLINE_MSG_RES:
                        List<OffLineMessages> listOffMsg = (List<OffLineMessages>)clientFunction.DistributeResults(msg);
                        allOffLineMsgRes(this, new AllOffLineMsgResEventArgs(ref listOffMsg));
                        break;
                    case OpType.FRIEND_IP_RES:
                        string userIP = analy.GetParaIndexOf(0);
                        int userPort=int.Parse(analy.GetParaIndexOf(1));
                        friendIPRes(this, new FriendIPResEventArgs(userIP,userPort));
                        break;
                    case OpType.SEND_FRIEND_REQUEST_RES:
                        int relatID = (int)clientFunction.DistributeResults(msg);
                        sendFriendRequestRes(this, new SendFriendRequestResEventArgs(relatID));
                        break;
                    case OpType.SEND_FRIEND_REQUEST_NOTE:
                        string user1 = analy.GetParaIndexOf(0);
                        string user2 = analy.GetParaIndexOf(1);
                        sendFriendRequestNote(this, new SendFriendequestNoteEventArgs(user1,user2));
                        break;
                    case OpType.AGREE_FRIEND_REQUEST_RES:
                        string user11= analy.GetParaIndexOf(0);
                        string user22 = analy.GetParaIndexOf(1);
                        agreeFriendRequestRes(this, new AgreeFriendRequestResEventArgs(user11, user22));
                        break;
                    case OpType.DISAGREE_FRIEND_REQUEST_RES:
                        string user111 = analy.GetParaIndexOf(0);
                        string user222 = analy.GetParaIndexOf(1);
                        disagreeFriendRequestRes(this, new DisagreeFriendRequestResEventArgs(user111, user222));
                        break;
                    case OpType.CREATE_GROUP_RES:
                        int groupID = (int)clientFunction.DistributeResults(msg);
                        string groupName=analy.GetParaIndexOf(1);
                        createGroupRes(this, new CreateGroupResEventArgs(groupID,groupName));
                        break;
                    case OpType.SEND_OFFLINE_MSG_RES:
                        int offMsgID = (int)clientFunction.DistributeResults(msg);
                        sendOffLineMsgRes(this, new SendOffLineMsgResEventArgs(offMsgID));
                        break;
                    case OpType.JOIN_GROUP_RES:
                        int groupEntityID = (int)clientFunction.DistributeResults(msg);
                        joinGroupRes(this, new JoinGroupResEventArgs(groupEntityID));
                        break;
                    case OpType.JOIN_GROUP_NOTE:
                        string userID1 = analy.GetParaIndexOf(0);
                        int groupID1 = int.Parse(analy.GetParaIndexOf(1));
                        joinGroupNote(this, new JoinGroupNoteEventArgs(userID1, groupID1));
                        break;
                    case OpType.ALL_GROUP_REQUEST_RES:
                        List<GroupEntity> listGroupRequest = (List<GroupEntity>)clientFunction.DistributeResults(msg);
                        allGroupRequestRes(this, new AllGroupRequestResEventArgs(ref listGroupRequest));
                        break;
                    case OpType.AGREE_JOIN_GROUP_RES:
                        string userID2 = analy.GetParaIndexOf(0);
                        int groupID2 = int.Parse(analy.GetParaIndexOf(1));
                        agreeJoinGroupRes(this, new AgreeJoinGroupResEventArgs(userID2, groupID2));
                        break;
                    case OpType.DISAGREE_JOIN_GROUP_RES:
                        string userID3 = analy.GetParaIndexOf(0);
                        int groupID3 = int.Parse(analy.GetParaIndexOf(1));
                        disagreeJoinGroupRes(this, new DisagreeJoinGroupResEventArgs(userID3, groupID3));
                        break;
                    case OpType.USER_INFO_RES:
                        UserInfo user = (UserInfo)clientFunction.DistributeResults(msg);
                        userInfoRes(this, new UserInfoResEventArgs(ref user));
                        break;
                    case OpType.GROUP_INFO_RES:
                        GroupInfo group = (GroupInfo)clientFunction.DistributeResults(msg);
                        groupInfoRes(this, new GroupInfoResEventArgs(ref group));
                        break;
                    case OpType.SEND_NET_FILE_RES:
                        string user1F = analy.GetParaIndexOf(0);
                        string user2F = analy.GetParaIndexOf(1);
                        string fileName = analy.GetParaIndexOf(2);
                        string fileContent = analy.GetParaFromIndex(3);
                        sendNetFileRes(this,new SendNetFileResEventArgs(user1F,user2F,fileName,fileContent));
                        break;
                    case OpType.SEND_GROUP_MSG_RES:
                        string user1M = analy.GetParaIndexOf(0);
                        string userName1M = analy.GetParaIndexOf(1);
                        int groupID1M = int.Parse(analy.GetParaIndexOf(2));
                        string msgContent = analy.GetParaFromIndex(3);
                        sendGroupMsgRes(this, new SendGroupMsgResEventArgs(user1M,userName1M ,groupID1M, msgContent));
                        break;
                    case OpType.MEMBERS_IN_GROUP_RES:
                        List<UserInfo> listMembers = (List<UserInfo>)clientFunction.DistributeResults(msg);
                        memberInGroupRes(this, new MembersInGroupResEventArgs(ref listMembers));
                        break;
                    case OpType.COMMUNICATION:
                        string msgComm = analy.GetParaFromIndex(0);
                        communication(this, new CommunicationEventArgs(msgComm));
                        break;
                    case OpType.FIND_USERS_RES:
                        List<UserInfo> listUserFind = (List<UserInfo>)clientFunction.DistributeResults(msg);
                        findUsersRes(this, new FindUsersResEventArgs(ref listUserFind));
                        break;
                    case OpType.FIND_GROUPS_RES:
                        List<GroupInfo> listGroupFind = (List<GroupInfo>)clientFunction.DistributeResults(msg);
                        findGroupsRes(this, new FindGroupsResEventArgs(ref listGroupFind));
                        break;
                    case OpType.IS_FRIEND_RES:
                        bool isFriend = (bool)clientFunction.DistributeResults(msg);
                        isFriendRes(this, new IsFriendResEventArgs(isFriend));
                        break;
                    case OpType.IS_IN_GROUP_RES:
                        bool isInGroup = (bool)clientFunction.DistributeResults(msg);
                        isGroupMemberRes(this, new IsGroupMemberResEventArgs(isInGroup));
                        break;
                    case OpType.LOGOUT_NOTE:
                        string userIDOut = analy.GetParaIndexOf(0);
                        logoutRes(this, new LogoutResEventArgs(userIDOut));
                        break;
                }
            }
            
        }
    }
}
