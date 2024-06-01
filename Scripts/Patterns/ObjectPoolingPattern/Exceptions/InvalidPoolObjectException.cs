using System;

public class InvalidPoolObjectException : Exception
{
    public InvalidPoolObjectException() {}

    public InvalidPoolObjectException(string message) 
        : base(message) {}

    public InvalidPoolObjectException(string message, Exception innerException) 
        : base(message, innerException) {}
}