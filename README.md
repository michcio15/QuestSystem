# Quest System
An **Exiled** plugin that makes it easier to create custom quests.
## Usage/Examples
There is an [example](https://github.com/michcio15/QuestSystem/tree/master/QuestSystemExample) in this repo. 
**Usage** in short you need to create a PlayerQuestComponent for example in 
```cs
public static void OnVerified(VerifiedEventArgs ev)
```
with 
```cs
QuestComponent.Register(ev.Player, out QuestComponent component);
``` 
and to create a new quest you need to make a new class inheriting from `BaseQuest`
```cs
public class KillPlayersQuest : BaseQuest
```
and in there you provide what should happen when progress to this quest is added and when the quest is finished
```cs
 protected override void OnAddedProgress(Player player, float progress)
    {
        Log.Info($"{player.Nickname} has killquest progress : {CurrentProgress}/{MaxProgress}");
    }

    protected override void OnQuestCompleted(Player player)
    {
        Log.Info("Quest completed");
        player.AddItem(ItemType.Adrenaline);
    }
```
and to add that quest to a player you need to have a `QuestComponent`. You can use for it `QuestComponent.Get(Player)`.
Once you have it need too 
```cs
QuestComponent:Add(Basequest);
```
so in this example
```cs
QuestComponent.Add(new KillPlayersQuest(5f));
```
this `5f` means what is the max progress that once its reached the `BaseQuest:OnQuestCompleted` would be called.
## Support 
If you have any questions feel free to dm me on discord (@michcio15) or ping on Exiled discord server


