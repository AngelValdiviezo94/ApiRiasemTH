
using System.Diagnostics.CodeAnalysis;

namespace EnrolApp.Domain.Exceptions;

public abstract class ApplicationException : Exception
{
    [ExcludeFromCodeCoverage]
    protected ApplicationException(string title, string message)
        : base(message) =>
        Title = title;

    public string Title { get; }
}

