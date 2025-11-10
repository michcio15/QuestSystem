using System;
using Exiled.API.Features;

namespace QuestSystem;

public class QuestSystem : Plugin<Config>
{
    public override string Name { get; } = "QuestSystem";
    public override string Author { get; } = "michcio";
    public override string Prefix { get; } = "QuestSystem";
    public override Version Version { get; } = new Version(1,0,0);

    public static QuestSystem? Instance;

    public override void OnEnabled()
    {
        base.OnEnabled();
        Instance = this;
    }

    public override void OnDisabled()
    {
        base.OnDisabled();
        Instance = null;
    }
}
