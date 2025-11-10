using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using QuestSystem.API.Components;
using QuestSystem.API.Features;
using QuestSystemExample.Quests;

namespace QuestSystemExample.Events;

public static class PlayerHandler
{
    public static void OnVerified(VerifiedEventArgs ev)
    {
        //Registers a quest component for player that joined
        QuestComponent.Register(ev.Player, out QuestComponent component);
        //Adds a KillPlayersQuest with maxProgress to this player: 5
        component.Add(new KillPlayersQuest(5f));
    }

    public static void OnDied(DiedEventArgs ev)
    {
        //Adds 1f progress for attacker -> killer to the KillPlayersQuest
        BaseQuest.AddProgressQuest<KillPlayersQuest>(ev.Attacker, 1f);
    }

    public static void OnLeft(LeftEventArgs ev)
    {
        //Unregisters a quest component for the player that left
        QuestComponent.Unregister(ev.Player);
    }

}
