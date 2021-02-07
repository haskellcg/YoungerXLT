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
		private  string IP="192.168.0.23";
		private  int Port=8500;
        private  int QueenLength = 20;
		public TcpListener listener;
		private TcpClient remoteClient;
		private NetworkStream streamToClient;
        private IPEndPoint endPoint;
        private OnLineList OLList;
        private ServerFunction serverFunc;
		
		public CServer()
		{
            OLList = new OnLineList();
            endPoint = new IPEndPoint(IPAddress.Parse("0.0.0.0"),0);
            serverFunc = new ServerFunction(ref OLList,ref endPoint);
        }
        public CServer(int _BufferSize, string _IP, int _Port, int _QueenLength)
		{
			BufferSize=_BufferSize;
			IP=_IP;
			Port=_Port;
            QueenLength = _QueenLength;

            OLList = new OnLineList();
            endPoint = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0);
            serverFunc = new ServerFunction(ref OLList,ref endPoint);
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
            endPoint.Port = 8600;
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

        //测试专用函数
        public void Test()
        {
            StartListener();
            GetNetworkStream();
            string format = Communicate();
            ReleaseAll();
            OLList.OnLined(new SmallUser("809924", "127.0.0.1", 8700));
            System.Console.WriteLine(serverFunc.DistributeTasks(format));
        }

        public void Run()
        {
            StartListener();
            while (true)
            {
                GetNetworkStream();
                string format = Communicate();
                ReleaseAll();
                string ret = serverFunc.DistributeTasks(format);
                if (!ret.Equals("###"))
                {
                    CClient client=new  CClient(endPoint.Address.ToString(), endPoint.Port);
                    client.ConnectToServer();
                    client.GetNetworkStream();
                    client.Communicate(ret);
                }
                
            }
        }
	}
}