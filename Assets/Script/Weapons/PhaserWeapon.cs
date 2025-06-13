using UnityEngine;

public class PhaserWeapon : MonoBehaviour
{
    public static PhaserWeapon instance;

    [SerializeField] private GameObject prefab;
    [SerializeField] private ObjectPooler bulletPool;

    public float speed;
    public int damage;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Shoot()
    {
        //Instantiate(prefab, transform.position, transform.rotation);
        GameObject bullet = bulletPool.GetObject();
        AudioManager.instance.PlayModifiedSound(AudioManager.instance.shoot);
        bullet.transform.position = transform.position;
        bullet.SetActive(true);
    }

}
