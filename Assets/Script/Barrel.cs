using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    private Vector2 playerPos, barrelPos;
    [SerializeField] private float speed = 7f;
    [SerializeField] int distance = 30;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        barrelPos = transform.position;
    }

    private void Update()
    {
        playerPos = GameObject.Find("Player").transform.position;
        if (Vector2.Distance(playerPos, barrelPos) < distance)
            rb.velocity = new Vector2(-speed, rb.velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Land")
        {
            anim.SetTrigger("Mati");
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
