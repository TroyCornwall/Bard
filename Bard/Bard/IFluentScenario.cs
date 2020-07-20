namespace Bard
{
    public interface IFluentScenario<out TIBeginAScenario> where TIBeginAScenario : StoryBook
    {
        IGiven<TIBeginAScenario> Given { get; }

        IWhen When { get; }

        IThen Then { get; }
    }
}