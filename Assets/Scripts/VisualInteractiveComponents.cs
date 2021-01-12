
using UnityEngine;
[RequireComponent(typeof(AudioSource), typeof(SpriteRenderer), typeof(Collider2D))]
public class VisualInteractiveComponents : MonoBehaviour
{
    public AudioSource AudioS { private set; get; }
    public SpriteRenderer SRend { private set; get; }

    public Collider2D Collider { private set; get; }

    private void Awake()
    {
        AudioS = GetComponent<AudioSource>();
        SRend = GetComponent<SpriteRenderer>();
        Collider = GetComponent<Collider2D>();
    }
}
