using UnityEngine;
using UnityEngine.UI;
using Game.Components.UI.Abstract;

namespace Game.Components.UI
{
    [RequireComponent(typeof(RawImage))]
    public sealed class ScrollingBackgroundTexture : UIMonoBehaviour
    {
        private const float speed = 0.05f;

        [SerializeField]
        private Color clearColor = default(Color);

        private Vector2 velocity;

        private RawImage image;

        protected override void Awake()
        {
            base.Awake();
            image = GetComponent<RawImage>();
            float angle = UnityEngine.Random.Range(0, 2 * Mathf.PI);
            velocity = new Vector2(
                speed * Mathf.Cos(angle),
                speed * Mathf.Sin(angle));
        }

        void Start()
        {
            Camera.main.backgroundColor = clearColor;
        }

        void Update()
        {
            Rect uvRect = image.uvRect;
            uvRect.position += velocity * Time.deltaTime;
            image.uvRect = uvRect;
        }
    }
}