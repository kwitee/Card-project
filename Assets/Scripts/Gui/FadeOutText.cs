using UnityEngine;
using UnityEngine.UI;

namespace CardProject.Gui
{
    public class FadeOutText : MonoBehaviour
    {
        [SerializeField]
        private float upShiftVelocity = 0.7f;

        [SerializeField]
        private float colorFadeVelocity = 0.015f;

        [SerializeField]
        private float destroyAfter = 1.4f;

        [SerializeField]
        private Text text = null;

        [SerializeField]
        private RectTransform rectTransform = null;

        private string showedText;

        public void Start()
        {
            text.text = showedText;
            DestroyObject(gameObject, destroyAfter);
        }

        public void Update()
        {
            transform.position += Vector3.up * upShiftVelocity;
            var newColor = text.color;
            newColor.a -= colorFadeVelocity;
            text.color = newColor;
        }

        public static void Instantiate(GameObject playerStateChangePrefab, Canvas canvas, string showedText)
        {
            var obj = Instantiate(playerStateChangePrefab);
            var fadeText = obj.GetComponent<FadeOutText>();
            obj.transform.SetParent(canvas.transform);
            fadeText.rectTransform.anchoredPosition = new Vector2();
            fadeText.showedText = showedText;
        }
    }
}