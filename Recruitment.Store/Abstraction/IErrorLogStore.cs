using Recruitment.Common.Models;

namespace Recruitment.Store.Abstraction
{
    public interface IErrorLogStore
    {
        Task InsertErrorLogAsync(ErrorLog errorLog);
    }
}