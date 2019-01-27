using UnityEngine;
using UnityEngine.UI;
using Game.Attributes;
using Game.Components.Scheduling;
using Game.Components.UI.Abstract;
using Game.CSharpExtensions;
using Game.Gameplay;
using Game.UnityExtensions.Scenes;

namespace Game.Components.UI.ResultsScreen
{
    public sealed class ResultsScreenController : UIMonoBehaviour
    {
        [Header("Object References")]

        [SerializeField]
        private FamilyMemberPortrait familyMemberPortraitPrefab = default(FamilyMemberPortrait);

        [SerializeField]
        private RectTransform familyMemberResultsContainer = default(RectTransform);

        [SerializeField]
        private Text scoreText = default(Text);

        [Header("Timing")]
        
        [SerializeField]
        private float familyMemberEnterWaitTime = default(float);

        [SerializeField]
        private float familyMemberEnterStaggerTime = default(float);

        void Start()
        {
            EndOfGameResults results = ResultsEvaluation
                .EvaluateGameResults(GameplayController
                    .Instance
                    .GetSelectedGifts());

            results
                .FamilyMemberResults
                .ForEach((result, i) =>
                {
                    FamilyMemberPortrait portrait = Instantiate(familyMemberPortraitPrefab, familyMemberResultsContainer)
                        .SetFamilyMember(result.FamilyMember)
                        .SetHappinessLevel(result.HappinessLevel);


                    TimerManager.Schedule(
                        time: familyMemberEnterWaitTime + i * familyMemberEnterStaggerTime,
                        id: this)
                        .OnComplete(portrait.PlayEnterAnimation);
                });

            scoreText.text = $"SCORE: {results.Score}";
        }

        void OnDestroy()
        {
            TimerManager.Cancel(this);
        }

        [UnityEventBinding]
        public void OnPressPlayAgain()
        {
            GameplayController.Instance.RestartGame();
            SceneFunctions.TransitionScene("EndOfGameResults", "Gameplay");
        }
    }
}