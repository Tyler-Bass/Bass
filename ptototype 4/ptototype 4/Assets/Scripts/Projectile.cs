using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Enemy[] enemy;
    public GameObject target;
    public PlayerController values;
    
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.Translate((target.transform.position - transform.position).normalized * Time.deltaTime * 10, Space.World);
        }
        else
            Destroy(gameObject);
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == target)
        {
            Destroy(target);
            Destroy(gameObject);
        }
    }
}
