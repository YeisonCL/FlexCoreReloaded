using System;

namespace FlexCoreLogic.exceptions
{
    internal class InsertException:Exception
    {

        public InsertException()
        {
        }

        public InsertException(string message)
            : base(message)
        {
        }

        public InsertException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}
