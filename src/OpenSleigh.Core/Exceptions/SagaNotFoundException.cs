using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace OpenSleigh.Core.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class SagaNotFoundException : Exception
    {
        public SagaNotFoundException(string message) : base(message) { }

        protected SagaNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}