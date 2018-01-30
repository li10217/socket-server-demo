using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketDemo
{
    public class SocketSession : AppSession<SocketSession, SocketRequestInfo>
    {
        public SocketSession()
        {

        }

        public string NickName = "未登录";

        protected override void OnSessionStarted()
        {

        }

        protected override void OnInit()
        {
            base.OnInit();
        }

        protected override void HandleUnknownRequest(SocketRequestInfo requestInfo)
        {
            LogHelper.WriteLog("Unknown Command!");
        }

        protected override void HandleException(Exception e)
        {
            LogHelper.WriteLog("Handle Exception!");
        }

        protected override void OnSessionClosed(CloseReason reason)
        {
            base.OnSessionClosed(reason);
        }
    }
}
