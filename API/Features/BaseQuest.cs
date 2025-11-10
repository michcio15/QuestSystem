using Exiled.API.Features;
using JetBrains.Annotations;
using QuestSystem.API.Components;
namespace QuestSystem.API.Features

{
    /// <summary>
    ///
    /// </summary>
    [PublicAPI]
    public abstract class BaseQuest
    {
        /// <summary>
        /// Helps with adding progress to ALL the Quests of this type
        /// </summary>
        /// <param name="player">Player to add the progress</param>
        /// <param name="amount">Amount to add</param>
        /// <typeparam name="T">BaseQuest</typeparam>
        public static void AddProgressQuest<T>(Player player, float amount) where T : BaseQuest
        {
            var questComponent = QuestComponent.Get(player);
            var questsOfType = questComponent?.GetOfType<T>();

            if (questsOfType == null) return;

            foreach (T quest in questsOfType)
            {
                if (questComponent != null) quest.AddProgress(player, amount);
            }
        }

        /// <summary>
        /// Name of the quest
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// Current quest progress
        /// </summary>
        public float CurrentProgress { get; set; } = 0;
        /// <summary>
        /// Max progress
        /// </summary>
        public float MaxProgress { get; set; }
        /// <summary>
        /// Wheather or not the quest is completed
        /// </summary>
        public bool IsCompleted
        {
            get => CurrentProgress >= MaxProgress;
            private set { }
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="maxProgress">What is the max progress of thsi quest</param>
        protected BaseQuest(float maxProgress)
        {
            MaxProgress = maxProgress;
            CurrentProgress = 0;
        }

        /// <summary>
        /// Adds progress to this quest
        /// </summary>
        /// <param name="player">Player to whom we add this progress</param>
        /// <param name="progress">Amount</param>
        public void AddProgress(Player player, float progress)
        {
            CurrentProgress += progress;
            OnAddedProgress(player, progress);
            if (IsCompleted)
                Complete(player);
        }

        /// <summary>
        /// Completes the quest
        /// </summary>
        /// <param name="player">player for who this quest is completed</param>
        public void Complete(Player player)
        {
            QuestComponent component = QuestComponent.Get(player);
            component.Remove(this);
            OnQuestCompleted(player);
        }

        /// <summary>
        /// Sets this quest progr
        /// </summary>
        /// <param name="player"></param>
        /// <param name="progress"></param>
        public void SetProgress(Player player, float progress)
        {
            CurrentProgress = progress;
            OnAddedProgress(player, progress);

            if (IsCompleted)
                Complete(player);
        }


        protected abstract void OnAddedProgress(Player player, float progress);

        protected abstract void OnQuestCompleted(Player player);
    }
}
