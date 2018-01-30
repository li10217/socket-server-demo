using SuperSocket.SocketBase.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketDemo.Command
{
    public class CMD_0003_UpText : CommandBase<SocketSession, SocketRequestInfo>
    {
        private int Action = 3;
        public override string Name
        {
            get { return Action.ToString(); }
        }
        /// <summary>
        /// 上行
        /// </summary>
        public override void ExecuteCommand(SocketSession session, SocketRequestInfo requestInfo)
        {
            LogHelper.WriteLog(session.NickName + " " + requestInfo.Body);
            Push(session, "已收到文本");
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
