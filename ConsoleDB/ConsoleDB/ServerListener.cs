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
		private  string IP="192.168.1.112";
		private  int Port=8500;
        private  int QueenLength = 50;
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
            serverFunc = new ServerFunction(ref OLList, ref endPoint);
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

        //检测函数，检测客户端是否关闭，检测异常关闭以及挂掉
        public void TrueOnLineUsers()
        {

            foreach (string userID in OLList.GetAllOLUsersID())
            {
                if (OLList.IsOnLine(userID))
                {
                    try
                    {
                        CClient client = new CClient(OLList.GetUserByID(userID).IPAddress, OLList.GetUserByID(userID).IPPort);
                        client.ConnectToServer();
                        client.GetNetworkStream();
                        client.Communicate("###");
                        client.ReLeaseAll();
                    }
                    catch (Exception e)
                    {
                        OLList.OffLined(userID);

                        System.Console.WriteLine(userID+"客户端异常关闭，现在在线用户数量:" + OLList.Length());

                    }
                }
            }
        }
        public void OnLineUserCheckFunction()
        {
            while (true)
            {
                Thread.Sleep(500);
                TrueOnLineUsers();
            }
        }

        public void Run()
        {
            
            StartListener();
            while (true)
            {
                GetNetworkStream();
                string format = Communicate();
                ReleaseAll();

                System.Console.WriteLine(format);

                string ret = serverFunc.DistributeTasks(format);

                System.Console.Write("在线用户:" + OLList.Length() + "位，分别为");
                foreach (string userID in OLList.GetAllOLUsersID())
                {
                    System.Console.Write("<" + userID + ">__");
                }
                System.Console.WriteLine();



                if (!ret.Equals("###"))
                {
                    CClient client=new  CClient(endPoint.Address.ToString(), endPoint.Port);

                    System.Console.WriteLine("Server发送时IP以及端口:"+endPoint.Address.ToString() + ":" + endPoint.Port);

                    try
                    {
                        client.ConnectToServer();
                        client.GetNetworkStream();
                        client.Communicate(ret);
                        client.ReLeaseAll();
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine("无法连接对方...");
                        foreach (string userID in OLList.GetAllOLUsersID())
                        {
                            if (OLList.GetUserByID(userID).IPAddress.Equals(endPoint.Address.ToString()) && OLList.GetUserByID(userID).IPPort == endPoint.Port)
                            {
                                OLList.OffLined(userID);
                                System.Console.WriteLine("检测到用户ID为:<"+userID+">的用户异常关闭客户端...");
                            }
                        }
                        System.Console.WriteLine("此时在线用户:" + OLList.Length() + "位");
                    }
                    
                    
                }
                
            }
        }
	}
}