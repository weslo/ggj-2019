using System.Collections.Generic;
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

        [SerializeField]
        private float quirkEnterWaitTime = default(float);

        [SerializeField]
        private float quirkStaggerTime = default(float);

        private int _displayedScore = -1;
        public int DisplayedScore
        {
            get => _displayedScore;
            set
            {
                if(_displayedScore != value)
                {
                    _displayedScore = value;

                    if(scoreText != null)
                    {
                        scoreText.text = $"SCORE: {_displayedScore}";
                    }
                }
            }
        }

        private Dictionary<FamilyMember, FamilyMemberPortrait> portraits
            = new Dictionary<FamilyMember, FamilyMemberPortrait>();

        private Dictionary<FamilyMember, FamilyMemberResult> results
            = new Dictionary<FamilyMember, FamilyMemberResult>();

        void Start()
        {
            DisplayedScore = 0;

            EndOfGameResults endOfGameResults = ResultsEvaluation
                .EvaluateGameResults(GameplayController
                    .Instance
                    .GetSelectedGifts());

                float totalEnterTime = familyMemberEnterWaitTime + endOfGameResults.FamilyMemberResults.Length * familyMemberEnterStaggerTime;
                float quirkBeginTime = totalEnterTime + quirkEnterWaitTime;

            endOfGameResults
                .FamilyMemberResults
                .ForEach((result, i) =>
                {
                    FamilyMemberPortrait portrait = Instantiate(familyMemberPortraitPrefab, familyMemberResultsContainer)
                        .SetFamilyMember(result.FamilyMember)
                        .SetHappinessLevel(result.HappinessLevel);

                    portraits.Add(result.FamilyMember, portrait);
                    results.Add(result.FamilyMember, result);

                    TimerManager.Schedule(
                        time: familyMemberEnterWaitTime + i * familyMemberEnterStaggerTime,
                        id: this)
                        .OnComplete(portrait.PlayEnterAnimation)
                        .OnComplete(() =>
                        {
                            DisplayedScore += ResultsEvaluation
                                .CalculateBaseScoreForGiftChoice(result.FamilyMember, result.Gift);
                        });

                    TimerManager.Schedule(
                        time: quirkBeginTime + i * quirkStaggerTime,
                        id: this)
                        .OnComplete(() =>
                        {
                            EvaluateQuirk(result.FamilyMember);
                        });
                });
        }

        void OnDestroy()
        {
            TimerManager.Cancel(this);
            TimerManager.Cancel(Gameplay.Quirks.AbstractQuirk.QuirkSchedulingKey);
        }

        [UnityEventBinding]
        public void OnPressPlayAgain()
        {
            GameplayController.Instance.RestartGame();
            SceneFunctions.TransitionScene("EndOfGameResults", "Gameplay");
        }

        private void EvaluateQuirk(FamilyMember member)
        {
            int modifiedScore;

            member.Quirk.AnimateChanges(
                source: member,
                results: results,
                portraits: portraits,
                score: DisplayedScore,
                modifiedScore: out modifiedScore);

            DisplayedScore = modifiedScore;
        }
    }
}