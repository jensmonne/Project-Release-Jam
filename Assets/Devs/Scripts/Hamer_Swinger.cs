using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hamer_Swinger : MonoBehaviour
{

    [SerializeField] private GameObject HammerPrefab;
    [SerializeField] private ParticleSystem effectPrefab;
    [SerializeField] private Transform itemSpawn;


    private bool hasHammer = false;
    private Player player;
    private GameObject currentHammer;
    private Transform EffectSpawnPoint;

    void Start()
    {
        player = GetComponent<Player>();
    }

    //Hammer spawn
    public void EnableHammer()
    {
        player.hasItem = true;

        currentHammer = Instantiate(HammerPrefab, itemSpawn.position, itemSpawn.rotation * Quaternion.Euler(0f, 0f, -130f), transform);
        hasHammer = true;
    }

        public void SwingHammerAction(InputAction.CallbackContext context)
    {
        if (context.performed && hasHammer)
        {
            SwingHammer();
        }
    }

    //Hammer swing action
    void SwingHammer()
    {
        Debug.Log("Hammer swung!");

        //current hammer animation
        Animator hammerAnimator = currentHammer.GetComponent<Animator>();
        if (hammerAnimator != null)
        {
            hammerAnimator.SetTrigger("Swing");
        }
    }

    public void StopAnimation()
    {
        if (currentHammer != null)
        {
            Animator hammerAnimator = currentHammer.GetComponent<Animator>();
            if (hammerAnimator != null)
            {
                hammerAnimator.ResetTrigger("Swing");
            }
        }

        //Particles werken nog niet!!!!
        EffectSpawnPoint = currentHammer.transform.Find("EffectSpawn");
        if (EffectSpawnPoint != null && effectPrefab != null)
        {
            Debug.Log("Effect played at: " + EffectSpawnPoint.position);
            ParticleSystem currentEffect = Instantiate(effectPrefab, EffectSpawnPoint.position, EffectSpawnPoint.rotation);
            currentEffect.Play();
        }
        else
        {
            Debug.LogError("AAAHHHHHHH!!!!!!!");
        }

        StartAction();
    }

    //Action (Force) Werkt nog niet!!!
    void StartAction()
    {
        float radius = 3f;        // bereik van de klap
        float force = 800f;       // kracht van de explosie
        float upwards = 1.5f;     // hoeveel omhoog ze vliegen (optioneel, voelt lekkerder)

        Collider[] hitObjects = Physics.OverlapSphere(EffectSpawnPoint.position, radius);

        foreach (Collider col in hitObjects)
        {
            Rigidbody rb = col.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(force, EffectSpawnPoint.position, radius, upwards, ForceMode.Impulse);
            }
        }

        Destroy(currentHammer);
        hasHammer = false;
        player.hasItem = false;
    }
}