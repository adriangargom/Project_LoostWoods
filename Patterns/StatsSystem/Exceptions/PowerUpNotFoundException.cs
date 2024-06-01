using System;

public class PowerUpNotFoundException : Exception
{
    public PowerUpNotFoundException() {}

    public PowerUpNotFoundException(string message)
        : base(message) {}

    public PowerUpNotFoundException(string message, Exception innerException)
        : base(message, innerException) {}
}