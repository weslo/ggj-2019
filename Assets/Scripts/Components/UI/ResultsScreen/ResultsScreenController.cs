﻿using UnityEngine;
using UnityEngine.UI;
using Game.Components.UI.Abstract;
using Game.CSharpExtensions;
using Game.Gameplay;

namespace Game.Components.UI.ResultsScreen
{
    public sealed class ResultsScreenController : UIMonoBehaviour
    {
        [SerializeField]
        private FamilyMemberPortrait familyMemberPortraitPrefab = default(FamilyMemberPortrait);

        [SerializeField]
        private RectTransform familyMemberResultsContainer = default(RectTransform);

        [SerializeField]
        private Text scoreText = default(Text);

        void Start()
        {
            EndOfGameResults results = ResultsEvaluation
                .EvaluateGameResults(GameplayController
                    .Instance
                    .GetSelectedGifts());

            results
                .FamilyMemberResults
                .ForEach(result =>
                {
                    Instantiate(familyMemberPortraitPrefab, familyMemberResultsContainer)
                        .SetFamilyMember(result.FamilyMember)
                        .SetHappinessLevel(result.HappinessLevel);
                });

            scoreText.text = $"SCORE: {results.Score}";
        }
    }
}