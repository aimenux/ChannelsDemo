using System.Threading.Tasks;

namespace App.Examples
{
    public interface IExample
    {
        string Description { get; }
        Task RunAsync();
    }
}
