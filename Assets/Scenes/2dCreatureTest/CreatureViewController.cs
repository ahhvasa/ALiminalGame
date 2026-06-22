using UnityEngine;

public class CreatureViewController : MonoBehaviour
{
    public ObjectFlipByTarget objectFlipByTarget;
    public IKLegController iKLegController;

    public Rigidbody rb;

    public Vector2 moveSpeedRange = new Vector2(0, 1);
    public float moveSpeedBase = 1;

    private Vector3 _lastPosition;

    private void Start()
    {
        _lastPosition = rb.transform.position;
    }

    private void Update()
    {
        Vector3 velocity;
        if (rb.isKinematic)
        {
            velocity = (rb.transform.position - _lastPosition) / Time.deltaTime;
        }
        else
        {
            velocity = rb.velocity;
        }
        _lastPosition = rb.transform.position;

        moveDirection = velocity.normalized;
        moveSpeed = Mathf.InverseLerp(moveSpeedRange.x, moveSpeedRange.y, velocity.magnitude) * moveSpeedBase;
        lookDirection = rb.transform.forward;
        Debug.DrawRay(rb.transform.position, velocity.normalized * 2, Color.red);
        Debug.DrawRay(rb.transform.position, rb.transform.forward);
    }


    public Vector3 moveDirection
    {
        set
        {
            if (value.magnitude < 0.5f) { return; }

            iKLegController.lookTarget = value;
        }
    }
    public float moveSpeed
    {
        set
        {
            iKLegController.speed = value;
        }
    }
    public Vector3 lookDirection
    {
        set
        {
            objectFlipByTarget.lookTarget = value;
        }
    }


}