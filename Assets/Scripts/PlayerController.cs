using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Class Attributes
    Vector2 direction;
    Animator anim;
    Rigidbody2D rb;
    AudioSource audio;

    [SerializeField] float speed = 10;
    [SerializeField] DialogManager dialogManager;
    [SerializeField] LevelManager levelManager;

    [Header("Sound Effects")]
    [SerializeField] AudioClip click;
    [SerializeField] AudioClip accept;
    [SerializeField] AudioClip cancel;
    [SerializeField] AudioClip levelOver;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector2(0,-0.1f);
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();

        levelManager.OnAcceptBlock += () => { audio.PlayOneShot(accept); };
        levelManager.OnResetBlocks += () => { audio.PlayOneShot(cancel); };
        levelManager.OnCompleteLevel += () => { audio.PlayOneShot(levelOver); };
    }

    // Update is called once per frame
    void Update()
    {
        if (!dialogManager.IsActive())
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            Vector2 newVelocity = Vector2.zero;

            if (x != 0 || y != 0)
            {
                direction = new Vector2(x, y);
                anim.SetFloat("VelocityX", x * 5);
                anim.SetFloat("VelocityY", y * 5);

                newVelocity = direction.normalized * speed;
            }
            else
            {
                anim.SetFloat("VelocityX", direction.x);
                anim.SetFloat("VelocityY", direction.y);
            }

            rb.velocity = newVelocity;

            if (Input.GetButtonDown("Interact"))
            {
                Vector2 origin = transform.position + Vector3.down * 0.25f;
                Collider2D other = Physics2D.OverlapCircle(origin + direction.normalized * 1, 0.5f);
                Interactable i;
                if (other != null && other.TryGetComponent<Interactable>(out i))
                {
                    i.Interact();
                    audio.PlayOneShot(click);
                }
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
