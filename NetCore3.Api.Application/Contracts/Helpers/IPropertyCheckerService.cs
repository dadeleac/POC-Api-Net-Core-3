namespace NetCore3.Api.Application.Contracts.Helpers
{
    public interface IPropertyCheckerService
    {
        bool TypeHasProperties<T>(string fields);
    }
}