﻿using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands
{
    class CreateMultiplayerGameCommand : ICommand
    {
        IModel model;

        public CreateMultiplayerGameCommand(IModel model)
        {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client = null)
        {
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            bool result =  model.OpenRoom(name, rows, cols, client);
            return result ? "" : "Game name already exists";
        }

        public void Finish(TcpClient client)
        {
            
        }
    }
}
