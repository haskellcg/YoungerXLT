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
            GetNetworkStream();
            string msg = Communicate();
            ReleaseAll();
            object ob = clientFunction.DistributeResults(msg);
            
            
        }

        public string  Run()
        {
            string format;
            StartListener();
            while (true)
            {
                GetNetworkStream();
                format = Communicate();
                string a;
              
                ReleaseAll();
            }
        }
    }
}
