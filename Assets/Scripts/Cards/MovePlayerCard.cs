using UnityEngine;

public class MovePlayerCard : MonoBehaviour
{
    private Vector3 fromPosition;
    private Vector3 toPosition;
    private bool move = false;
    private bool rotate = false;

    [SerializeField]
    private float moveDistanceImprecision = 0.01f;

    [SerializeField]
    private float moveVelocity = 5f;

    public void MoveTo(Vector3 position, bool rotateCard = false)
    {
        rotate = rotateCard;
        fromPosition = transform.position;
        toPosition = position;
        transform.position = fromPosition;
        move = true;
    }

    public void Update()
    {
        if (move)
        {
            transform.position = Vector3.Lerp(transform.position, toPosition, moveVelocity * Time.deltaTime);

            if (Vector3.Distance(transform.position, toPosition) <= moveDistanceImprecision)
            {
                move = false;

                if (rotate)
                    transform.Rotate(0, 180, 0);
            }
        }
    }
}