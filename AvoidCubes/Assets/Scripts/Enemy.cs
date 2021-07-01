using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Root root;
    Hero hs;

    float size;
    float speed;
    float rotation;

    Vector3 target;
    Vector3 napr;

    private void Start()
    {
        hs = GameObject.Find("hero").GetComponent<Hero>();
        root = Camera.main.GetComponent<Root>();

        size = Random.Range(0.15f, 0.3f);
        speed = Random.Range(0.025f, 0.05f);
        rotation = Random.Range(-0.5f, 0.5f);

        this.transform.localScale = new Vector3(size, size, size);

        target = new Vector3(Random.Range(-2.5f, 2.5f), -6, 0);
    }

    private void FixedUpdate()
    {
        this.transform.Find("sprite").Rotate(0, 0, rotation);

        if (root.isStarted == true)
        {
            napr = target - this.transform.position;

            this.transform.Translate(napr.normalized * speed);
        }

        if (this.transform.position.y < -5.5f ||
            this.transform.position.y > 7f ||
            this.transform.position.x < -2.5f ||
            this.transform.position.x > 2.5f)
        {
            Destroy(this.gameObject);
        }

        if (hs.score > 100)
        {
            speed = Random.Range(0.04f, 0.065f);
        }
    }
}
