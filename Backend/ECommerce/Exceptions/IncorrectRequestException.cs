
namespace Exceptions
{
    public class IncorrectRequestException : MyExceptions
    {
        public IncorrectRequestException(string errorMessage) : base(errorMessage) { }
    }
}
