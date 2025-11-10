using Exiled.API.Interfaces;

#pragma warning disable 1591
namespace QuestSystemExample
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
    }
}
