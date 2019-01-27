using UnityEngine;
using UnityEngine.UI;
using Game.Components.UI.Abstract;
using Game.Gameplay;

namespace Game.Components.UI.GameplayScreen
{
    [RequireComponent(typeof(Text))]
    public sealed class BudgetTextDisplay : UIMonoBehaviour
    {
        private Text text;

        protected override void Awake()
        {
            base.Awake();
            text = GetComponent<Text>();
        }

        void Start()
        {
            GameplayController.Instance.OnBudgetChanged += OnBudgetChanged;
            OnBudgetChanged(GameplayController.Instance.Budget);
        }

        void OnDestroy()
        {
            GameplayController.Instance.OnBudgetChanged -= OnBudgetChanged;
        }

        private void OnBudgetChanged(int budget)
        {
            text.text = $"Budget: {GameplayText.GetCostText(budget)}";
        }
    }
}