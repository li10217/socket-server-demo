using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.Common;
using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using SuperSocket.SocketBase.Protocol;

namespace SocketDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            MyServer appServer = new MyServer();
            //服务器端口  
            int port = 2000;

            //设置服务监听端口  
            if (!appServer.Setup(port))
            {
                Console.WriteLine("端口设置失败!");
                Console.ReadKey();
                return;
            }
            //新连接事件  
            //appServer.NewSessionConnected += new SessionHandler<SocketSession>(NewSessionConnected);

            //收到消息事件  
            //appServer.NewRequestReceived += new RequestHandler<MySession, MyRequestInfo>(NewRequestReceived);

            //连接断开事件  
            //appServer.SessionClosed += new SessionHandler<SocketSession, CloseReason>(SessionClosed);

            //启动服务  
            if (!appServer.Start())
            {
                Console.WriteLine("启动服务失败!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("启动服务成功，输入exit退出!");

            while (true)
            {
                var str = Console.ReadLine();
                if (str.ToLower().Equals("exit"))
                {
                    break;
                }
            }

            Console.WriteLine();

            //停止服务  
            appServer.Stop();

            Console.WriteLine("服务已停止，按任意键退出!");
            Console.ReadKey();  
        }
        static void NewSessionConnected(SocketSession session)
        {
            var response = BitConverter.GetBytes((ushort)4).Reverse().ToList();
            var arr = System.Text.Encoding.UTF8.GetBytes("Hello User!");
            response.AddRange(BitConverter.GetBytes((ushort)arr.Length).Reverse().ToArray());
            response.AddRange(arr);

            session.Send(response.ToArray(), 0, response.Count);
            //向对应客户端发送数据  
            //session.Send("Hello User!");
        }
        static int i = 0;
        static void NewRequestReceived(SocketSession session, SocketRequestInfo requestInfo)
        {
            /** 
             * requestInfo为客户端发送的指令，默认为命令行协议 
             * 例： 
             * 发送 ping 127.0.0.1 -n 5 
             * requestInfo.Key: "ping" 
             * requestInfo.Body: "127.0.0.1 -n 5" 
             * requestInfo.Parameters: ["127.0.0.1","-n","5"] 
             **/
            switch (requestInfo.Key.ToUpper())
            {
                case ("3"):
                    var response = BitConverter.GetBytes((ushort)3).Reverse().ToList();
                    var arr = System.Text.Encoding.UTF8.GetBytes("success");
                    response.AddRange(BitConverter.GetBytes((ushort)arr.Length).Reverse().ToArray());
                    response.AddRange(arr);
                    session.Send(response.ToArray(), 0, response.Count);
                    break;
                case ("2"):
                    response = BitConverter.GetBytes((ushort)2).Reverse().ToList();
                    arr = System.Text.Encoding.UTF8.GetBytes("heart");
                    response.AddRange(BitConverter.GetBytes((ushort)arr.Length).Reverse().ToArray());
                    response.AddRange(arr);
                    session.Send(response.ToArray(), 0, response.Count);
                    break;
                default:
                    session.Send("未知的指");
                    break;
            }
        }

        static void SessionClosed(SocketSession session, CloseReason reason)
        {

        }  
    }
}
