using Fleck;
using System;
using System.Collections.Generic;

namespace WebSocketAppBackend
{
    public class ServerProcess
    {
        public void Start(string location)
        {
            List<IWebSocketConnection> sockets = new List<IWebSocketConnection>();
            WebSocketServer server = new WebSocketServer(location);

            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine("CONNECTION OPENED BY CLIENT");
                    sockets.Add(socket);
                };

                socket.OnClose = () =>
                {
                    Console.WriteLine("CONNECTION CLOSED BY CLIENT");
                    sockets.Remove(socket);
                };

                socket.OnMessage = message =>
                {
                    Console.WriteLine($"RX FROM CLIENT: {message}");
                    sockets.ForEach(s => s.Send($"OTHER CLIENT SAYS: {message}"));
                };     
            });

            Console.WriteLine("TYPE CLOSE TO EXIT AND PRESS ENTER");

            string input = Console.ReadLine();

            while (input != "CLOSE")
            {
                sockets.ForEach(s => s.Send($"SERVER SAYS: {input}"));
                input = Console.ReadLine();
            }
        }
    }
}
