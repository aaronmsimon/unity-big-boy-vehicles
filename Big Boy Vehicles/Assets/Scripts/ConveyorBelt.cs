using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private List<GameObject> onBelt;

    private void Start()
    {
        onBelt = new List<GameObject>();
    }

    private void Update()
    {
        for (int i = 0; i < onBelt.Count; i++)
        {
            onBelt[i].GetComponent<Rigidbody>().velocity = speed * transform.forward * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Conveyorable"))
        {
            collision.rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            onBelt.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
    }
}
