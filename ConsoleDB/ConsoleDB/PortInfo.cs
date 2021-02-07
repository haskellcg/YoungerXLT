using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Net;
using System.Collections;

namespace ConsoleDB
{
    class PortInfo
    {
        public static IList<int> PortIsUsed()
        {
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipsTCP = ipGlobalProperties.GetActiveTcpListeners();
            IPEndPoint[] ipsUDP = ipGlobalProperties.GetActiveUdpListeners();
            TcpConnectionInformation[] tcpConInfo = ipGlobalProperties.GetActiveTcpConnections();
            IList<int> allPorts = new List<int>();
            foreach(IPEndPoint ipEndp in ipsTCP)
                allPorts.Add(ipEndp.Port);
            foreach (IPEndPoint ipEndp in ipsUDP)
                allPorts.Add(ipEndp.Port);
            foreach (TcpConnectionInformation TcpInfo in tcpConInfo)
                allPorts.Add(TcpInfo.LocalEndPoint.Port);
            return allPorts;
        }

        public static bool PortIsAvailable(int port)
        {
            bool isAvailable = true;
            IList<int> postUsed = PortIsUsed();
            foreach (int p in postUsed)
            {
                if (p == port)
                {
                    isAvailable = false;
                }
            }
            return isAvailable;
        }

        public static int GetAvailablePort()
        {
            int START_PORT = 8500;
            int MAX_PORT = 65535;
            for (int i = START_PORT; i <= MAX_PORT; i++)
            {
                if (PortIsAvailable(i))
                    return i;
            }

            return -1;
        }

    }
}
