using UnityEngine;

public class HammerOnAnimation : MonoBehaviour
{
    private Hamer_Swinger hammerSwinger;

    void Start()
    {
        hammerSwinger = FindAnyObjectByType<Hamer_Swinger>();
    }

    public void StopAnimation()
    {
        hammerSwinger.StopAnimation();
    }
}
