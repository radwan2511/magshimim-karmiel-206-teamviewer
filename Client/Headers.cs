using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace client_ppp
{
    public enum Headers : byte
    {
        Queue,
        Start,
        Stop,
        Pause,
        Chunk
    }
}
