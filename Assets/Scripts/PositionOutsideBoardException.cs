using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    class PositionOutsideBoardException : Exception
    {
        public  PositionOutsideBoardException() : base() { }
        public PositionOutsideBoardException(string message) : base(message) { }
        public PositionOutsideBoardException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected PositionOutsideBoardException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }
}
