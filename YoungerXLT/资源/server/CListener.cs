using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using System.Collections.Generic;

namespace ConsoleDB
{
    //该服务器端要求一直等待知道要连接并且 获得客户端的字符串
	class CServer
	{
		private  int BufferSize=8192;
		private  string IP="192.168.0.66";
		private  int Port=8900;
        private  int QueenLength = 20;
		public TcpListener listener;
		private TcpClient remoteClient;
		private NetworkStream streamToClient;
		
		public CServer()
		{
        }
        public CServer(int _BufferSize, string _IP, int _Port, int _QueenLength)
		{
			BufferSize=_BufferSize;
			IP=_IP;
			Port=_Port;
            QueenLength = _QueenLength;
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
        public List<string> GetAllMsg()
        {
            List<string> list = new List<string>();
            GetNetworkStream();
            list.Add(Communicate());
            ReleaseAll();
            while (listener.Pending())
            {
                GetNetworkStream();
                list.Add(Communicate());
                ReleaseAll();
            }
            return list;
        }


        public static void Main()
        {
			CServer cs=new CServer();
            cs.StartListener();
            cs.GetNetworkStream();
            string msg=cs.Communicate();
			cs.ReleaseAll();
			System.Console.WriteLine(msg);
        }
	}
}