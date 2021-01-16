using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace DeckButtonsServer{
    class Client:Comms{
        private TcpClient cliente;
        private NetworkStream sockStream;
        private BinaryWriter writer;
        private BinaryReader buffer;
        private Thread tipoThread;

        public static string chToStr(char c) {return char.ToString(c);}

        /// <summary>

        /// Required designer variable.

        /// </summary>
        /// 


        private System.ComponentModel.Container components = null;

        public Client(){
            tipoThread = new Thread(()=> RunClient() );
            tipoThread.Start();
        }

        public void RunClient(){
            Console.WriteLine("Running Client connection");
            try{
                cliente = new TcpClient();
                cliente.Connect("localhost", 3000);
                //Se preferir altere localhost pelo IP do server
                sockStream = cliente.GetStream();
                writer = new BinaryWriter(sockStream);
                buffer = new BinaryReader(sockStream);
                char reading = '\0';
                string message = "";
                bool sigstart = false;
                do{
                    try{
                        reading=buffer.ReadChar();
                        if (reading== _sigmsg)
                            if (!sigstart)
                                sigstart = true;
                            else {
                                received_message(writer,message);
                                sigstart = false;
                                message = "";
                            }
                        else
                            if (sigstart)
                            message += reading;
                    } catch(Exception){
                        Console.WriteLine("Failed reading stuff\nRead "+message+" before crashing");
                        System.Environment.Exit(System.Environment.ExitCode);
                    }
                } while (reading!= _sigend);
                writer.Close();
                buffer.Close();
                sockStream.Close();
                cliente.Close();
            } catch (Exception error) {
                Console.WriteLine(error.ToString());
            }
        }
    }
}
