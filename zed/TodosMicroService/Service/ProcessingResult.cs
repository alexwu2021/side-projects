namespace Todos.Api.Services
{
    public enum ProcessingResult
    {
        Success = 1, 
        Failure = -1,
        Nooperation = 0,
        Unknown = 9999
    }
}