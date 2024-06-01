using System;

public class EmptyPoolException : Exception
{
    public EmptyPoolException() {}

    public EmptyPoolException(string message) 
        : base(message) {}

    public EmptyPoolException(string message, Exception innerException) 
        : base(message, innerException) {}
}