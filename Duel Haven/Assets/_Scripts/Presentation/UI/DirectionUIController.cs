using UnityEngine;

public class DirectionUIController : MonoBehaviour
{
    public RectTransform arrowImage;

    private float maxArrowLength;

    void Start()
    {
        maxArrowLength = arrowImage.sizeDelta.y;
    }

    public void SetDirection(Vector2 input)
    {
        float magnitude = input.magnitude;

        if (magnitude < 0.01f)
        {
            arrowImage.gameObject.SetActive(false);
            return;
        }

        arrowImage.gameObject.SetActive(true);

        // Rotate
        float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
        arrowImage.localRotation = Quaternion.Euler(0, 0, angle);

        // Scale length
        arrowImage.sizeDelta = new Vector2(
            maxArrowLength * magnitude,
            arrowImage.sizeDelta.y
        );
    }
}
