using UnityEngine;

public class MeatExplosion : MonoBehaviour
{
    public Player player;
    public ObjectPull<Transform> objectPull;
    public GameObject meatPrefab;

    public SoundData meathExplosionSound;

    public void Awake()
    {
        objectPull = new ObjectPull<Transform>(meatPrefab.transform, meatCount);
    }

    public void Update()
    {

    }

    public int meatCount = 5;
    public float minForce = 1;
    public float maxForce = 5;

    public void Explode()
    {
        SoundManager.PlaySound(meathExplosionSound, player.soundPlayer);

        for (int i = 0; i != meatCount; i++)
        {
            GameObject meat = objectPull.GetObject().gameObject;

            Vector3 direction = (new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f))).normalized;
            float force = Random.Range(minForce, maxForce);
            meat.transform.position = transform.position;

            meat.GetComponentInChildren<Rigidbody>().AddForce(direction.normalized * force, ForceMode.Impulse);
        }
    }
}
