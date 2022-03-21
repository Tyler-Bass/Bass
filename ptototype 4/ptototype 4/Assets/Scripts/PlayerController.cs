using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject focalPoint;
    private Rigidbody playerRb;
    public float speed = 2.0f;
    private float powerupStrength = 15.0f;
    public GameObject powerupIndicator;
    public Enemy[] enemies;
    public GameObject ammoPrefab;
    public bool hasPowerup = true;
    public int enemyNumber = 0;
    public GameObject spawn;
    public SpawnManager spawnManager;
    public GameObject specificEnemy;
    public Projectile ammoScript;
    public float flySpeed;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        enemies = FindObjectsOfType<Enemy>();
        spawn = GameObject.Find("Spawn Manager");
        spawnManager = spawn.GetComponent<SpawnManager>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput * Time.deltaTime);
        powerupIndicator.transform.position = transform.position + new Vector3(0, 3, 0);
        if (Input.GetKeyDown("space"))
        {
            StartCoroutine(Shoot());
        }



    }
    IEnumerator Shoot()
    {

        int count = 0;
        rb.isKinematic = true;
        while (count < 10)
        {
            transform.Translate(Vector3.up * flySpeed * Time.deltaTime, Space.World);
            yield return new WaitForSeconds(0.01f);
            count++;
            
        }
        while (transform.position.y > 0.5)
        {
            transform.Translate(Vector3.down * flySpeed * 2 * Time.deltaTime, Space.World);
            yield return new WaitForSeconds(0.01f);
        }
            enemies = FindObjectsOfType<Enemy>();
            Fire();
        yield return new WaitForSeconds(0.05f);
        rb.isKinematic = false;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
        IEnumerator PowerupCountdownRoutine()
        {
            yield return new WaitForSeconds(7); hasPowerup = false;
            powerupIndicator.gameObject.SetActive(false);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);

            Debug.Log("Collided with " + collision.gameObject.name + " with powerup set to " + hasPowerup);

        }
    }
    
    private void Fire()
    {
        GameObject ammo;
        enemyNumber = 0;
        for (int i = 0; i < enemies.Length; i++)
        {
            enemyNumber = 0;

            ammo = Instantiate(ammoPrefab, transform.position, transform.rotation);
            ammoScript = ammo.GetComponent<Projectile>();
            ammoScript.target = enemies[i].gameObject;
            
        }
    }
}
