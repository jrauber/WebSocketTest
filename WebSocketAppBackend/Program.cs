using Fleck;
using System;
using System.Collections.Generic;

namespace WebSocketAppBackend
{
    class Program
    {
        static void Main(string[] args)
        {
            var proc = new ServerProcess();

            proc.Start("ws://127.0.0.1:8181");
        }
    }
}
