using Fluent.Testing.Library.Then;
using Fluent.Testing.Library.When;

namespace Fluent.Testing.Library
{
    public interface IFluentScenario<out TIBeginAScenario> where TIBeginAScenario : BeginAScenario
    {
        IGiven<TIBeginAScenario> Given { get; }

        IWhen When { get; }

        IThen Then { get; }
    }
}