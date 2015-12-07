namespace DateTime.Service
{
    using System;
    using System.ServiceModel;

    [ServiceContract]
    public interface IDateTimeService
    {
        [OperationContract]
        string FindDayOfWeek(DateTime date);
    }
}
