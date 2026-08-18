using UnityEngine;

public class RigidbodyBoost : MonoBehaviour
{
    public float force = 10f; // сила ускорения
    public Vector3 direction = Vector3.forward; // направление

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

    }
}
