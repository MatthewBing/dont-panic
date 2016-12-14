using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour
{

    private CannonBehavior Cannon;
    ContactPoint Contact;

    // Use this for initialization
    void Start()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.gameObject.tag == "CannonCollider")
            {
                Contact = collision.contacts[0];

                Cannon = Contact.otherCollider.GetComponent<CannonBehavior>();

                Cannon.isPushed = true;

                Destroy(gameObject);
            }
            else Destroy(gameObject);
        }
    }


    //Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 8;
    }
}
