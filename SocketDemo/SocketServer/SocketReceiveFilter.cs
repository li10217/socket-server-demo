using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.Facility.Protocol;

namespace SocketDemo
{
    public class SocketReceiveFilter : FixedHeaderReceiveFilter<SocketRequestInfo>
    {
        public SocketReceiveFilter()
            : base(4)
        {

        }

        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            return (int)header[offset + 2] * 256 + (int)header[offset + 3];
        }

        protected override SocketRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            byte[] body = bodyBuffer.Skip(offset).Take(length).ToArray();
            return new SocketRequestInfo(header.Array, body);
        }
    }
}
