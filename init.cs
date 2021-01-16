using System;
using System.Windows.Forms;

namespace DeckButtonsServer {
    class init {
        static void Main(string[] args){


            Client usb_device = new Client();
            Server main_server = new Server();
            NotifyIcon icon = new NotifyIcon();
            icon.Icon = new System.Drawing.Icon("src/stonks.ico");
            icon.Visible = true;
            icon.BalloonTipText = "Henlo";
            icon.BalloonTipTitle = "_/)] StonkS [(\\_";
            icon.BalloonTipIcon = ToolTipIcon.Info;
            icon.ShowBalloonTip(2000);
            //Server s1 = new Server();
            /*
             * Client connects: 
             * server sends a "connected client x" to client
             * client signals to server a "mainentance" time, to run the Panels_Editor (side-software)
             * server awaits new updates, or a "mainentance_complete" signal, to close the port
             * client finishes updating server with Panels.json file
             * server signals launch complete
             * server starts communicating Action stuff
             * 
             * Config button on app:
             *    ├─> Start mainentance
             *    └─>
             *    
             *    
             *    https://stackoverflow.com/questions/3571627/show-hide-the-console-window-of-a-c-sharp-console-application
             *    https://ambilykk.com/2019/03/20/system-tray-icon-for-net-core-console-app/
             */
        }
    }
}