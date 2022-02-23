using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    public float randomJumpForce;
    public float xRandomSpawn = 4.0f;
    public float ySpawn = 2.0f;
    private GameManager gameManagerScript;
    public int pointValue;
    public ParticleSystem explosionEffect;

    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        randomJumpForce = Random.Range(12, 16);
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawn();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(gameManagerScript.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionEffect, transform.position, explosionEffect.transform.rotation);
            gameManagerScript.ScoreUpdate(pointValue);
        }        
    }

    Vector3 RandomForce()
    {
        return Vector3.up * randomJumpForce;
    }

    float RandomTorque()
    {
        return Random.Range(-10, 10);
    }

    Vector3 RandomSpawn()
    {
        return new Vector3(Random.Range(-xRandomSpawn, xRandomSpawn), -ySpawn, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Sensor"))
        {
            Destroy(gameObject);
            if(!gameObject.CompareTag("Enemy"))
            {
                gameManagerScript.GameOver();
            }            
        }
    }
}
