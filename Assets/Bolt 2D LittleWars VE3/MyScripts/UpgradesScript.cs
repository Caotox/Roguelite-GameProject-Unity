using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesScript : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Upgrade> upgrades; // créer la classe Update
    void Start()
    {
        upgrades = new List<Upgrade>();
        upgrades.Add(new Upgrade("Upgrade"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}