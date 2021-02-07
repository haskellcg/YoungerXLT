using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using System.Collections.Generic;

namespace ConsoleDB
{
    //这里的客户端只要求向制定的IP以及端口发送字符串
    class CClient
    {
        private int BufferSize = 8192;
        private string IP;
        private int Port;

        private TcpClient localClient;
        private NetworkStream streamToServer;

        public CClient(int _BufferSize,string _IP,int _Port)
        {
            BufferSize = _BufferSize;
            IP = _IP;
            Port = _Port;
        }

        public CClient(string _IP, int _Port)
        {
            IP = _IP;
            Port = _Port;
        }

        public void ConnectToServer()
        {
            try
            {
                localClient = new TcpClient();
                localClient.Connect(IPAddress.Parse(IP), Port);
                Console.WriteLine("Server Connected!{0}-->{1}", localClient.Client.LocalEndPoint, localClient.Client.RemoteEndPoint);
            }
            catch (Exception ex)
            {
 
            }
        }

        public void GetNetworkStream()
        {
            streamToServer = localClient.GetStream();
        }

        public void Communicate(string msg)
        {
            byte[] buffer = Encoding.Unicode.GetBytes(msg);
            try
            {
                lock (streamToServer)
                {
                    streamToServer.Write(buffer, 0, buffer.Length);
                }
            }
            catch (Exception ex)
            {
            }
        }
		
		public void ReleaseAll()
        {
            streamToServer.Dispose();
            localClient.Close();
        }
		
		public static void Main()
		{
			CClient cc=new CClient("192.168.0.66",8500);
			cc.ConnectToServer();
			cc.GetNetworkStream();
			cc.Communicate("809924#2#809924#09102#8600");
			cc.ReleaseAll();
		}
    }
}
