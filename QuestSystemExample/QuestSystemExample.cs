using System;
using Exiled.API.Features;
using QuestSystemExample.Events;

namespace QuestSystemExample;
#pragma warning disable 1591
public class QuestSystemExample : Plugin<Config>
{
    public override string Name { get; } = "QuestSystemExample";
    public override string Author { get; } = "michcio";
    public static new Config Config { get; private set; } = null!;
    public override Version Version { get; } = new Version(1, 0, 0);

    public static QuestSystemExample? Instance;

    public override void OnEnabled()
    {
        Config = base.Config;
        Instance = this;
        Exiled.Events.Handlers.Player.Verified += PlayerHandler.OnVerified;
        Exiled.Events.Handlers.Player.Died += PlayerHandler.OnDied;
        Exiled.Events.Handlers.Player.Left += PlayerHandler.OnLeft;
    }

    public override void OnDisabled()
    {
        base.OnDisabled();
        Instance = null;
        Exiled.Events.Handlers.Player.Verified -= PlayerHandler.OnVerified;
        Exiled.Events.Handlers.Player.Died -= PlayerHandler.OnDied;
        Exiled.Events.Handlers.Player.Left -= PlayerHandler.OnLeft;
    }
}
