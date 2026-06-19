using UnityEngine;
using UnityEngine.UI;

public enum ArrowDirection
{
    Left, Up, Right, Down
}

public class HistoryIcon : MonoBehaviour
{
    [SerializeField] private Image image;

    [SerializeField] private Sprite upSprite;
    [SerializeField] private Sprite downSprite;
    [SerializeField] private Sprite leftSprite;
    [SerializeField] private Sprite rightSprite;

    public void SetValue(ArrowDirection direction)
    {
        switch (direction)
        {
            case ArrowDirection.Up:
                image.sprite = upSprite;
                break;
            case ArrowDirection.Down:
                image.sprite = downSprite;
                break;
            case ArrowDirection.Left:
                image.sprite = leftSprite;
                break;
            case ArrowDirection.Right:
                image.sprite = rightSprite;
                break;
        }
    }
}
