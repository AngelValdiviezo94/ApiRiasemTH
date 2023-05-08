

using System.Diagnostics.CodeAnalysis;

namespace EnrolApp.Domain.Exceptions;
public abstract class BadRequestException : ApplicationException
{
    [ExcludeFromCodeCoverage]
    protected BadRequestException(string message)
        : base("Bad Request", message)
    {
    }
}
