using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace DeckButtonsServer {

    class Comms{

        protected Thread thread;

        protected const char _sigmsg = '┼';
        protected const char _sigend = '¤';
        protected const char _sighead = '¦';
        protected const char _sigresp = '─';
        public static string chToStr(char c) { return char.ToString(c); }


        protected void received_message(BinaryWriter writer,string buffer){
            string[] spl = buffer.Split(_sighead);
            string head = spl[0];
            string content = "";
            if (spl.Length > 1) content = spl[1];
            switch (head) {
                case "usb_conn_stb":
                    Console.WriteLine("Connected over usb");
                    break;
                case "wifi_conn_stb":
                    Console.WriteLine("Connected over wifi");
                    break;
                case "request_panels":
                    Console.WriteLine("sending latest panels file");
                    writer.Write(chToStr(_sigmsg) + "" + chToStr(_sigresp) + "latest_panels¦" + readFile("data", "panels.json") + _sigmsg);
                    Console.WriteLine("Sent latest panels file");
                    break;
                case "switch_wifi":
                    Console.WriteLine("Switching to wifi");
                    break;
                case "switch_usb":
                    Console.WriteLine("Switching to usb");
                    break;

                //===============================

                case "editor_conn_stb":
                    Console.WriteLine("Editor connected!");
                    break;
                case "editor_push_panels":
                    Console.WriteLine("Receive panels from editor and send to phone");
                    break;
                case "editor_pull_panels":
                    Console.WriteLine("Request panels from remote and send to editor");
                    break;
                case "editor_refresh_remote":
                    Console.WriteLine("reload panels on remote");
                    break;
                case "command":
                    Console.WriteLine("Received command: " + content);
                    break;
                default:
                    Console.WriteLine("Unknown message:\nHeader:" + head + "\nContent: " + content);
                    break;
            }
        }

        private string readFile(string path, string filename) { return readFile(path + "/" + filename); }
        private string readFile(string path) {
            if (!File.Exists(path))
                File.OpenWrite(path);
            return File.ReadAllText(path);
        }
        private void writeFile(string content, string path, string filename) { writeFile(content, path + "/" + filename); }
        private void writeFile(string content, string path){
            if (!File.Exists(path))
                File.OpenWrite(path);
            File.WriteAllText(path, content);
        }
    }
}
