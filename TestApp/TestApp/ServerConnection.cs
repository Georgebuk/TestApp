using Newtonsoft.Json;
using Sockets.Plugin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace TestApp
{
    public class ServerConnection
    {
        private TcpClient client;
        private NetworkStream Stream;
        private BinaryWriter Writer;
        private BinaryReader Reader;
        private Thread Thread;

        private static readonly object padlock = new object();
        private static ServerConnection instance;
        private Thread thread;

        public static ServerConnection Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new ServerConnection("192.168.0.24", 4445);
                    return instance;
                }
            }
        }

        private ServerConnection(string host, int portnum)
        {
            try
            {
                client = new TcpClient();
                string hostname = Dns.GetHostName();
                string meme = Dns.GetHostByName(hostname).AddressList[0].ToString();
                client.Connect("192.168.0.24", 4445);
                Stream = client.GetStream();
                sendRequest("Device");
                thread = new Thread(listenForResponse);
                thread.Start();
            }
            catch (Exception e)
            {
                string s = e.ToString(); 
            }
            
        }

        private void listenForResponse()
        {
            while (true)
            {
                try
                {
                    string data = null;
                    byte[] buffer = new byte[client.ReceiveBufferSize];
                    int bytesRead = Stream.Read(buffer, 0, client.ReceiveBufferSize);
                    if (bytesRead > 0)
                    {
                        string dataRecieved = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        List<Booking> bookings = JsonConvert.DeserializeObject<List<Booking>>(dataRecieved);
                        Customer book = Customer.ActiveCustomer;
                        book.Bookings = bookings;
                     }
                }
                catch (Exception e)
                {
                    string n = e.ToString();
                    Stream.Close();
                    instance = null;
                }
            }
        }

        public void sendRequest(string request)
        {
            byte[] bytesToSend = UTF8Encoding.UTF8.GetBytes(request);
            Stream.Write(bytesToSend, 0, bytesToSend.Length);
        }
    }
}
