using UnityEngine;

namespace Dennis.UI
{
    public class ParallaxUI : MonoBehaviour
    {
        [SerializeField]
        private float moveModifier;
        [SerializeField]
        private float time;

        private Vector2 startPosition;

        private void Start()
        {
            startPosition = transform.position;
        }

        private void Update()
        {
            Vector2 position = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            float positionX = Mathf.Lerp(transform.position.x, startPosition.x + (position.x * moveModifier), time * Time.deltaTime);
            float positionY = Mathf.Lerp(transform.position.y, startPosition.y + (position.y * moveModifier), time * Time.deltaTime);

            transform.position = new Vector2(positionX, positionY);
        }
    }
}