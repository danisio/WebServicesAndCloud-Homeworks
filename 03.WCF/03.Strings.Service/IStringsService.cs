namespace Strings.Service
{
    using System.ServiceModel;

    [ServiceContract]
    public interface IStringsService
    {
        [OperationContract]
        int FindSubstrings(string text, string subString);
    }
}
