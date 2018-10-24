using System;


namespace Bluespire.Emerge.Common.Exceptions
{

    [Serializable]
    public class EmergeUnityException : Exception
    {
        public EmergeUnityException() { }

        public EmergeUnityException(string message) : base(message) { }

        public EmergeUnityException(string message, System.Exception inner) : base(message, inner) { }

        protected EmergeUnityException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
