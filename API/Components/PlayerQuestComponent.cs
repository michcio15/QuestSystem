using System.Collections.Generic;
using Exiled.API.Features;
using QuestSystem.API.Features;

namespace QuestSystem.API.Components
{
    /// <summary>
    /// QuestComponent
    /// </summary>
    public class QuestComponent
    {
        /// <summary>
        /// Dictionary with assigned <see cref="QuestComponent"/> to each <see cref="Player"/>
        /// </summary>
        private static Dictionary<Player, QuestComponent> PlayerQuests { get; set; } = new();


        /// <summary>
        /// Registers a QuestComponent for player
        /// </summary>
        /// <param name="player"><see cref="Player"/> for whom we register a QuestComponent</param>
        public static void Register(Player player)
        {
            if (!PlayerQuests.TryAdd(player, new QuestComponent()))
            {
                Log.Debug($"{player.Nickname} already has a QuestComponent");
                return;
            }
            Log.Debug($"Registered QuestComponent for {player.Nickname}");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="player"></param>
        /// <param name="questComponent"></param>
        public static void Register(Player player, out QuestComponent questComponent)
        {
            var newComponent = new QuestComponent();

            if (!PlayerQuests.TryAdd(player, newComponent))
            {
                Log.Debug($"{player.Nickname} already has a QuestComponent");
                questComponent = PlayerQuests[player];
                return;
            }
            Log.Debug($"Registered QuestComponent for {player.Nickname}");
            questComponent = newComponent;
        }

        /// <summary>
        /// Gets player's quest component
        /// </summary>
        /// <param name="player"><see cref="Player"/> for whom we get <see cref="QuestComponent"/></param>
        /// <returns><see cref="QuestComponent"/></returns>
        public static QuestComponent Get(Player player)
        {
            if (!PlayerQuests.TryGetValue(player, out QuestComponent questComponent))
            {
                Register(player, out questComponent);
            }
            return questComponent;
        }

        /// <summary>
        /// Unregisters a <see cref="QuestComponent"/> for player
        /// </summary>
        /// <param name="player"></param>
        public static void Unregister(Player player)
        {
            if (!PlayerQuests.TryGetValue(player, out QuestComponent questComponent))
            {
                Log.Debug($"{player.Nickname} has no QuestComponent");
                return;
            }
            questComponent.Clear();
            PlayerQuests.Remove(player);
            Log.Debug($"Quest component unregistered for {player.Nickname}");
        }

        // ReSharper disable once MemberCanBePrivate.Global
        /// <summary>
        /// List of active quests for owner of this component
        /// </summary>
        public List<BaseQuest> ActiveQuests { get; private set; } = [];

        /// <summary>
        /// Adds a quest to the player.
        /// </summary>
        public void Add(BaseQuest quest)
        {
            if (!ActiveQuests.Contains(quest))
            {
                ActiveQuests.Add(quest);
                Log.Debug($"Quest {quest.Name} added to player.");
            }
        }

        /// <summary>
        /// Removes a quest from the player.
        /// </summary>
        public void Remove(BaseQuest quest)
        {
            if (ActiveQuests.Remove(quest))
            {
                Log.Debug($"Quest {quest.Name} removed from player.");
            }
        }

        /// <summary>
        /// Clears all active quests.
        /// </summary>
        public void Clear()
        {
            ActiveQuests.Clear();
            Log.Debug("All quests cleared for player.");
        }

        /// <summary>
        /// Gets all active quests of a specific type.
        /// </summary>
        /// <typeparam name="T">
        /// The type of quest to filter for (must inherit from <see cref="BaseQuest"/>).
        /// </typeparam>
        /// <returns>
        /// A list of quests of the specified type.
        /// Returns an empty list if none are found.
        /// </returns>
        public List<T> GetOfType<T>() where T : BaseQuest
        {
            var returnList = new List<T>();
            foreach (var quest in ActiveQuests)
            {
                if (quest is T typedQuest)
                    returnList.Add(typedQuest);
            }
            return returnList;
        }
    }
}
