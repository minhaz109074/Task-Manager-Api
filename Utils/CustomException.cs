using System;
namespace Task_Manager_Api.Exceptions
{
    public class NotFoundException: Exception
    {
        public NotFoundException(): base() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, Exception inner): base(message, inner) { }
    }
    public class RecordAlreadyExistException : Exception
    {
        public RecordAlreadyExistException() : base() { }
        public RecordAlreadyExistException(string message) : base(message) { }
        public RecordAlreadyExistException(string message, Exception inner) : base(message, inner) { }
    }


}

