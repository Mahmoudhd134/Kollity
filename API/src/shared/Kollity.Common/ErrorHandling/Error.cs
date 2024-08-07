﻿namespace Kollity.Common.ErrorHandling;

public class Error
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);
    public static readonly Error UnKnown = new("Error.Unknown", "Un Known Error Occurred", ErrorType.Failure);

    private Error(string code, string description, ErrorType type)
    {
        Code = code;
        Description = description;
        Type = type;
    }

    public string Code { get; }
    public string Description { get; }
    public ErrorType Type { get; }

    public static Error Validation(string code, string description)
    {
        return new Error(code, description, ErrorType.Validation);
    }

    public static Error NotFound(string code, string description)
    {
        return new Error(code, description, ErrorType.NotFound);
    }

    public static Error Conflict(string code, string description)
    {
        return new Error(code, description, ErrorType.Conflict);
    }
}