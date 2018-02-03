﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Commands
{
    internal class GetOpenPrograms : ICommand
    {
        public string name { get; } = "getopenprograms";

        public string description { get; } = "Displays a list of all open programs on the remote computer";

        public string syntaxHelper { get; } = "getopenprograms";

        public bool isLocal { get; } = false;

        public List<string> validArguments { get; } = null;


        public bool PreProcessCommand(List<string> args)
        {
            throw new NotImplementedException();
        }

        public void ClientMethod(List<string> args)
        {
            var data = "";
            while (data != "{end}")
            {
                if(data != "")
                    Console.WriteLine(data);
                data = CommandsManager.networkManager.ReadLine();
            }
        }

        public void ServerMethod(List<string> args)
        {
            var processlist = Process.GetProcesses();
            var processesInfos = new List<Tuple<string, string>>();
            foreach (var process in processlist)
            {
                if (!string.IsNullOrEmpty(process.MainWindowTitle))
                {
                    processesInfos.Add(new Tuple<string, string>(process.Id.ToString(), process.ProcessName));
                }
            }

            CommandsManager.networkManager.WriteLine(CommandsManager.TableDisplay(processesInfos));
            CommandsManager.networkManager.WriteLine("{end}");
        }
    }
}