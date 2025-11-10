using Exiled.API.Features;
using QuestSystem.API.Features;

namespace QuestSystemExample.Quests;
/// <summary>
/// Creates a KillPlayersQuest
/// </summary>
/// <param name="maxProgress"> this will be needed when setting maxProgress</param>
public class KillPlayersQuest(float maxProgress) : BaseQuest(maxProgress)
{
    /// <summary>
    /// Quest name
    /// </summary>
    public override string Name { get; } = "Kill Players";

    /// <summary>
    /// Called whenever the progress is added.
    /// In this instance this will Log,Info players nickname and his progress
    /// </summary>
    /// <param name="player"><see cref="Player"/> to whom we added progress</param>
    /// <param name="progress">the amount we added</param>
    protected override void OnAddedProgress(Player player, float progress)
    {
        Log.Info($"{player.Nickname} has killquest progress : {CurrentProgress}/{MaxProgress}");
    }

    /// <summary>
    /// Called whenever the quest is completed.
    /// In this instance we give the player 1 adrenaline
    /// </summary>
    /// <param name="player"><see cref="Player"/> who complited this quest</param>
    protected override void OnQuestCompleted(Player player)
    {
        Log.Info("Quest completed");
        player.AddItem(ItemType.Adrenaline);
    }
}
