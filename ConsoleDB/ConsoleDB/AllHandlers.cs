using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleDB
{
    //注册回复
    delegate void RegisterResHandler(object sender, RegisterResEventArgs e);
    //登录回复
    delegate void LoginResHandler(object sender, LoginResEventArgs e);
    //好友登陆提示
    delegate void InformFriendsHandler(object sender, InformFriendsEventArgs e);
    //获得所属群组信息
    delegate void AllGroupsResHandler(object sender,AllGroupsResEventAgrs e);
    //获得所有好友信息
    delegate void AllFriendsResHandler(object sender,AllFriendsResEventArgs e);
    //获得所有在线好友信息
    delegate void AllOnLineFriendsResHandler(object sender, AllOnLineFriendResEventArgs e);
    //获得所有收到的好友请求
    delegate void AllFriendRequestsRecResHandler(object sender, AllFriendRequestsRecResEventArgs e);
    //获得所有发出的好友请求
    delegate void AllFriendRequestSedResHandler(object sender, AllFriendRequestsSedResEventArgs e);
    //获得所有离线消息
    delegate void AllOffLineMsgResHandler(object sender, AllOffLineMsgResEventArgs e);
    //获得好友IP
    delegate void FriendIPResHandler(object sender,FriendIPResEventArgs e);
    //发送好友请求的回复
    delegate void SendFriendRequestResHandler(object sender,SendFriendRequestResEventArgs e);
    //收到好友请求的提示
    delegate void SendFriendRequestNoteHandler(object sender,SendFriendequestNoteEventArgs e);
    //好友请求被接受的提示
    delegate void AgreeFriendRequestResHandler(object sender,AgreeFriendRequestResEventArgs e);
    //好友请求被拒绝的提示
    delegate void DisagreeFriendRequestResHandler(object sender,DisagreeFriendRequestResEventArgs e);
    //创建群租的服务器回复
    delegate void CreateGroupResHandler(object sender,CreateGroupResEventArgs e);
    //发送离线消息额服务器回复
    delegate void SendOffLineMsgResHandler(object sender,SendOffLineMsgResEventArgs e);
    //请求加入群租的服务器回复
    delegate void JoinGroupResHandler(object sender,JoinGroupResEventArgs e);
    //加入群租对管理员的提示
    delegate void JoinGroupNoteHandler(object sender,JoinGroupNoteEventArgs e);
    //获得所有加入群组的请求
    delegate void AllGroupRequestResHandler(object sender,AllGroupRequestResEventArgs e);
    //同意加入群租的提示
    delegate void AgreeJoinGroupResHandler(object sender,AgreeJoinGroupResEventArgs e);
    //拒绝加入群组的提示
    delegate void DisagreeJoinGroupResHandler(object sender,DisagreeJoinGroupResEventArgs e);
    //获得用户信息
    delegate void UserInfoResHandler(object sender,UserInfoResEventArgs e);
    //获得群组信息
    delegate void GroupInfoResHandler(object sender,GroupInfoResEventArgs e);
    //发送网络文件的提示
    delegate void SendNetFileResHandler(object sender,SendNetFileResEventArgs e);
    //发送群组消息的提示 
    delegate void SendGroupMsgResHandler(object sender,SendGroupMsgResEventArgs e);
    //获得群组成员
    delegate void MemebersInGroupResHandler(object sender,MembersInGroupResEventArgs e);
    //聊天消息
    delegate void CommunicationHandler(object sender,CommunicationEventArgs e);
    //查找用户信息
    delegate void FindUsersResHandler(object sender,FindUsersResEventArgs e);
    //查找群组信息
    delegate void FindGroupsResHandler(object sender,FindGroupsResEventArgs e);
    //判断是否为好友
    delegate void IsFriendResHandler(object sender,IsFriendResEventArgs e);
    //判断是否为群组员
    delegate void IsGroupMemberResHandler(object sender,IsGroupMemberResEventArgs e);
    //用户注销的提示
    delegate void LogoutResHandler(object sender,LogoutResEventArgs e);

}
