using SuperSocket.SocketBase.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketDemo.Command
{
    public class CMD_0002_Heart : CommandBase<SocketSession, SocketRequestInfo>
    {
        private int Action = 2;
        public override string Name
        {
            get { return Action.ToString(); }
        }
        /// <summary>
        /// 上行
        /// </summary>
        public override void ExecuteCommand(SocketSession session, SocketRequestInfo requestInfo)
        {
            if (requestInfo.Data.Length > 5)
            {
                var time = string.Join(" ", requestInfo.Data);
                LogHelper.WriteLog(session.NickName + "的心跳");
                Push(session, "服务器已经收到你的心跳");
            }
        }

        /// <summary>
        ///  下行(推送)
        /// </summary>
        public void Push(SocketSession session, string text)
        { 
            var response = BitConverter.GetBytes((ushort)Action).Reverse().ToList();
            var arr = System.Text.Encoding.UTF8.GetBytes(text);
            response.AddRange(BitConverter.GetBytes((ushort)arr.Length).Reverse().ToArray());
            response.AddRange(arr);

            session.Send(response.ToArray(), 0, response.Count);
        }
        
    }
}
