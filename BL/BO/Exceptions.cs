﻿namespace BO;
[Serializable]
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
}
[Serializable]
public class BlNotValidInputException : Exception
{
    public BlNotValidInputException(string? message) : base(message) { }
}
[Serializable]
public class BlAlreadyExistsException : Exception
{
    public BlAlreadyExistsException(string? message, Exception ex) : base(message) { }
}
