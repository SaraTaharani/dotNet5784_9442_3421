namespace BO;

//Convert dal exeptions to bl exeptions
[Serializable]
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string message, Exception innerException)
                : base(message, innerException) { }
}
[Serializable]
public class BlNotValidInputException : Exception
{
    public BlNotValidInputException(string? message) : base(message) { }
    public BlNotValidInputException(string message, Exception innerException)
                : base(message, innerException) { }
}
[Serializable]
public class BlNotCorectDate : Exception
{
    public BlNotCorectDate(string? message) : base(message) { }
    public BlNotCorectDate(string message, Exception innerException)
                : base(message, innerException) { }
}
[Serializable]
public class BlAlreadyExistsException : Exception
{
    public BlAlreadyExistsException(string? message) : base(message) { }
    public BlAlreadyExistsException(string message, Exception innerException)
                    : base(message, innerException) { }
}


[Serializable]
public class BlDoesNotExistExeption : Exception
{
    public BlDoesNotExistExeption(string? message) : base(message) { }
    public BlDoesNotExistExeption(string message, Exception innerException)
                   : base(message, innerException) { }
}
//Programs exeptions
[Serializable]
public class BlLogicException : Exception
{
    public BlLogicException(string? message) : base(message) { }

}


