using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketDemo
{
    public class SocketRequestInfo : IRequestInfo
    {
        public SocketRequestInfo(byte[] header, byte[] bodyBuffer)
        {
            Key = ((header[0] * 256) + header[1]).ToString();
            Data = bodyBuffer;
        }
        /// <summary>
        /// 协议号对应自定义命令Name，会触摸自定义命令
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 正文字节码
        /// </summary>
        public byte[] Data { get; set; }
        /// <summary>
        /// 正文文本    需要定义格式
        /// </summary>
        public string Body
        {
            get
            {
                return Encoding.UTF8.GetString(Data);
            }
        }
    }
}
