using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] private float moveSpeed = 8f;

    public Vector2Int CurrentCoord { get; private set; }
    public bool IsMoving { get; private set; }

    public event Action<Vector2Int> OnMoveCompleted;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        SetCoords();
    }

    public void SetCoords()
    {
        CurrentCoord = GridManager.Instance.StartCoord;
        transform.position = GridManager.Instance.GetSurfaceWorld(CurrentCoord);
    }

    public void Move(Vector2Int direction)
    {
        if (IsMoving) return;

        Vector2Int finalCoord = CurrentCoord;
        Vector2Int nextCoord = CurrentCoord + direction;

        while (!GridManager.Instance.IsBlocked(nextCoord))
        {
            finalCoord = nextCoord;
            nextCoord += direction;
        }

        if (finalCoord == CurrentCoord) return;

        transform.LookAt(GridManager.Instance.GridToWorld(finalCoord));
        StartCoroutine(MoveRoutine(finalCoord));
    }

    private IEnumerator MoveRoutine(Vector2Int targetCoord)
    {
        IsMoving = true;
        Vector3 targetPos = GridManager.Instance.GetSurfaceWorld(targetCoord);

        while (Vector3.Distance(transform.position, targetPos) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
        CurrentCoord = targetCoord;
        IsMoving = false;

        OnMoveCompleted?.Invoke(CurrentCoord);
    }
}