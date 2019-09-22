using System.Threading.Tasks;

namespace RevenueRecognition.Core
{
    public interface IAppService
    {
        Task Handle(object command);
    }
}