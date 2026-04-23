using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Gun_Aimer : MonoBehaviour
{
    [SerializeField] private GameObject GunPrefab;
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private Transform itemSpawn;
    [SerializeField] private TMP_Text BulletCountText;

    [SerializeField] private int Bullets = 10;

    private int currentBullets;
    private Vector2 moveInput;
    private GameObject currentGun;
    private bool hasGun = false;
    private Transform BulletSpawnPoint;
    private ParticleSystem Effect;
    private Player player;

    void Start()
    {
        player = GetComponent<Player>();

        currentBullets = Bullets;
        UpdateBulletCountText();

        BulletCountText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (currentGun != null && moveInput.magnitude > 0.1f)
        {   
            float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
            currentGun.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        if(currentBullets <= 0)
        {
            destroyGun();
        }
    }

    //let gun prefab appear
    public void EnableGun()
    {
        currentGun = Instantiate(GunPrefab, itemSpawn.position, itemSpawn.rotation, transform);

        BulletSpawnPoint = currentGun.transform.Find("BulletSpawn");

        BulletCountText.gameObject.SetActive(true);
        UpdateBulletCountText();

        Effect = currentGun.GetComponentInChildren<ParticleSystem>();

        hasGun = true;

        player.hasItem = true;
    }

    //Gun rotation to aim 
    public void RotateGun(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    //Shoot gun 
    public void ShootGunAction(InputAction.CallbackContext context)
    {
        if (context.performed && hasGun)
        {
            ShootGun();
        }
    }

    void ShootGun()
    {
        Debug.Log("Bang! Gun fired.");

        if (BulletSpawnPoint != null)
        {
            GameObject bullet = Instantiate(BulletPrefab, BulletSpawnPoint.position, BulletSpawnPoint.rotation);

            bullet.GetComponent<Rigidbody2D>().linearVelocity = BulletSpawnPoint.right * 10f;

            currentBullets--;
            UpdateBulletCountText();

            Effect.Play();
        }
    }

    void UpdateBulletCountText()
    {
        BulletCountText.text = $"x {currentBullets}";
    }

    void destroyGun()
    {
        if (currentGun != null)
        {
            Destroy(currentGun);
            hasGun = false;
            BulletCountText.gameObject.SetActive(false);
            player.hasItem = false;
        }
    }
}