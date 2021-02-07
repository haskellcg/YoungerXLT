using System.Net;
using System.Net.Sockets;
using System;
using System.Text;

class Client
{
	static void Main(string[] args)
	{
		Console.WriteLine("Client Running...");
		TcpClient client;
		ConsoleKey key;
		const int BufferSize=8192;
		try
		{
			client=new TcpClient();
			client.Connect(IPAddress.Parse("127.0.0.1"),8500);
		}catch(Exception ex)
		{
			Console.WriteLine(ex.Message);
			return;
		}
		
		Console.WriteLine("Server Connected!{0}-->{1}",client.Client.LocalEndPoint,client.Client.RemoteEndPoint);
		
		NetworkStream streamToServer=client.GetStream();
		Console.WriteLine("Menu:S-Send X-Exit");
	do
	{
		key=Console.ReadKey(true).Key;
		
		if(key== ConsoleKey.S)
		{
			Console.Write("input the message:");
			string msg=Console.ReadLine();
		
		
			byte[] buffer=Encoding.Unicode.GetBytes(msg);
			try
			{
				lock(streamToServer){
				streamToServer.Write(buffer,0,buffer.Length);
				}
				Console.WriteLine("Send:{0}",msg);
				
				int bytesRead;
				buffer=new byte[BufferSize];
				lock(streamToServer){
				bytesRead=streamToServer.Read(buffer,0,buffer.Length);
				}
				msg=Encoding.Unicode.GetString(buffer,0,bytesRead);
			}catch(Exception ex)
			{
				break;
			}
		}
		
	}while(key!=ConsoleKey.X);
	
	streamToServer.Dispose();
	client.Close();
		
	Console.WriteLine("\n\n ‰»Î\"Q\"º¸ÕÀ≥ˆ");
		do
		{
			key=Console.ReadKey(true).Key;
		}while(key != ConsoleKey.Q);	
	}
	
	
}