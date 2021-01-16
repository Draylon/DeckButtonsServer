using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DeckButtonsServer{
    class Server:Comms{
        //private System.ComponentModel.Container components = null;

        private Thread serverThread;

        public Server(){
            // thread para aceitar multiplas conexões
            thread = new Thread(new ThreadStart(RunServer));
            thread.Start();
        }

        protected void RunServer() {
            TcpListener escutando;
            try{
                escutando = new TcpListener(3001);
                escutando.Start();
                while (true) {
                    Console.WriteLine("Aguardando Conexoes");
                    Socket st = escutando.AcceptSocket();
                    serverThread = new Thread(() => readBuffer(st));
                    serverThread.Start();
                }

            }catch(Exception error){
                Console.WriteLine(error.ToString());
            }
        }

        protected void Server_Closing(object sender, CancelEventArgs e) {
            System.Environment.Exit(System.Environment.ExitCode);
        }

        private int conn=0;

        protected void readBuffer(Object oSocket){
            Socket socket = (Socket)oSocket;
            int index = conn+0;
            NetworkStream socketStream = new NetworkStream(socket);
            BinaryWriter writer = new BinaryWriter(socketStream);
            BinaryReader read_buffer = new BinaryReader(socketStream);
            Console.WriteLine(conn + " Conexões Recebidas!");
            writer.Write("Cenexão Efetuada!");
            //Envia.ReadOnly = false;
            char buffer = '\0';
            string message = "";
            bool sigstart = false;
            do{
                try{
                    buffer = read_buffer.ReadChar();
                    if (buffer == _sigmsg)
                        if (!sigstart)
                            sigstart = true;
                        else{
                            received_message(writer,message);
                            sigstart = false;
                            message = "";
                        }
                    else
                        if (sigstart)
                        message += buffer;
                }catch (Exception){
                    Console.WriteLine("Failed reading stuff\nRead " + message + " before crashing");
                    System.Environment.Exit(System.Environment.ExitCode);
                }
            }while (buffer != _sigend);
            Console.WriteLine("Conexão "+index+" Finalizada!");
            //fechando a conexao
            writer.Close();
            read_buffer.Close();
            socketStream.Close();
            socket.Close();
            ++conn;
        }


        /*protected void Envia_KeyDown(object sender, KeyEventArgs e){
            // Aqui está o código responsável para mandar mensagens
            try {
                if (e.KeyCode == Keys.Enter && conexao != null) {
                    writer.Write(Envia.Text);
                    Exibe.Text += Envia.Text;
                    if (Envia.Text == "FINALIZAR")
                        conexao.Close();
                    Envia.Clear();
                }
            } catch (SocketException) {
                Exibe.Text += "Atneção! Erro...";
            }
        }*/
    }
}
