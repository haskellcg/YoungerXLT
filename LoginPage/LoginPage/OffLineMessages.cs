using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleDB
{
    class OffLineMessages
    {
        public OffLineMessages()
        { }
        public OffLineMessages(int _MessageID, string _MessageUser1, string _MessageUser2, string _MessageContent,
                            DateTime _MessageSendTime)
        {
            MessageID = _MessageID;
            MessageUser1 = _MessageUser1;
            MessageUser2 = _MessageUser2;
            MessageContent = _MessageContent;
            MessageSendTime = _MessageSendTime;
        }
        private int _MessageID;                     //消息ID
        public int MessageID
        {
            get { return _MessageID; }
            set { _MessageID = value; }
        }
        private string _MessageUser1;               //消息的接受者
        public string MessageUser1
        {
            get { return _MessageUser1; }
            set { _MessageUser1 = value; }
        }
        private string _MessageUser2;               //消息的发送者
        public string MessageUser2
        {
            get { return _MessageUser2; }
            set { _MessageUser2 = value; }
        }
        private string _MessageContent;             //消息内容
        public string MessageContent
        {
            get { return _MessageContent; }
            set { _MessageContent = value; }
        }
        private DateTime _MessageSendTime;          //消息的发送时间
        public DateTime MessageSendTime
        {
            get { return _MessageSendTime; }
            set { _MessageSendTime = value; }
        }
    }
}
