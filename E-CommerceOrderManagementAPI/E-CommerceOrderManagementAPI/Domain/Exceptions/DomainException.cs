namespace E_CommerceOrderManagementAPI.Domain.Exceptions
{
    // a marker that indicates a class or structure can be converted into a format
    //(such as a stream of bytes, XML, or JSON) that can be stored or transmitted
    [Serializable]
    //These are our own defiend exceptions we have predefiend onces in the Systems Namespace
    public class DomainException : Exception
    {
        public DomainException() : base() { }
        public DomainException(string message) : base(message) { }
        public DomainException(string message, Exception innerException) : base(message, innerException) { }
    }
}
