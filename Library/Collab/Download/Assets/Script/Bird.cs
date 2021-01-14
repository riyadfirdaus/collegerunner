using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    private Vector2 playerPos, birdPos;
    [SerializeField] private float speed = 7f;
    [SerializeField] int distance = 30;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        birdPos = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        playerPos = GameObject.Find("Player").transform.position;
        if (Vector2.Distance(playerPos, birdPos) < distance)
            rb.velocity = new Vector2(-speed, rb.velocity.y);
    }
    public void keinjek()
    {
        anim.SetTrigger("Mati");
        rb.velocity = new Vector2(0, rb.velocity.y);
    }
    private void mati()
    {
        Destroy(this.gameObject);
    }
}
