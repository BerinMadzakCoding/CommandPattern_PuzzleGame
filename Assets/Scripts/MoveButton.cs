using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MoveButton : MonoBehaviour
{
    [Tooltip("Use (0,1)=up, (0,-1)=down, (1,0)=right, (-1,0)=left")]
    [SerializeField] private Vector2Int direction;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => PlayerController.Instance.Move(direction));
    }
}