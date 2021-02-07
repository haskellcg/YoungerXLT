using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;

namespace ClientServer
{
	class CServer
	{
		private  int BufferSize=8192;
		private  string IP="127.0.0.1";
		private  int Port=8500;
		public TcpListener listener;
		private TcpClient remoteClient;
		private NetworkStream streamToClient;
		
		public CServer()
		{}
		
		public CServer(int _BufferSize,string _IP,int _Port)
		{
			BufferSize=_BufferSize;
			IP=_IP;
			Port=_Port;
		}
		
		
		public void StartListener()
		{
			IPAddress ipAddress=IPAddress.Parse(IP);
			listener=new TcpListener(ipAddress,Port);
			listener.Start();
			Console.WriteLine("Start Listening...");
		}
		
		public void GetNetworkStream()
		{
			remoteClient=listener.AcceptTcpClient();
			Console.WriteLine("Client Connected!{0}-->{1}",remoteClient.Client.LocalEndPoint,remoteClient.Client.RemoteEndPoint);
			streamToClient=remoteClient.GetStream();
		}
		
		public void Communicate()
		{
			string msg;
			do{
				byte[] buffer=new byte[BufferSize];
				int bytesRead=0;
				try{
					lock(streamToClient){
						bytesRead=streamToClient.Read(buffer,0,BufferSize);
					}
					
					if(bytesRead==0)
							throw new Exception("¶ÁÈ¡µ½0×Ö½Ú");
					msg=Encoding.Unicode.GetString(buffer,0,bytesRead);
					Console.WriteLine("Recieve:{0}",msg);
					msg=" ";
					buffer=Encoding.Unicode.GetBytes(msg);
					lock(streamToClient){
						streamToClient.Write(buffer,0,buffer.Length);
					}
				}catch(Exception ex){
					break;
				}
				
				if(msg.Equals("BYE"))
						break;
			}while(true);
		}
		
		
		public void ReleaseAll()
		{
			streamToClient.Dispose();
			remoteClient.Close();
		}
		
		static void Main()
		{
		
			CServer cs1=new CServer();
			cs1.StartListener();
			cs1.GetNetworkStream();
			cs1.Communicate();
			cs1.ReleaseAll();
			if(cs1.listener.Pending())
			{
				cs1.GetNetworkStream();
				cs1.Communicate();
				cs1.ReleaseAll();
			}
		}
	}
}