using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject decal;
    public float forceShoot = 100f;
    public float waitTime = .3f;
    public AudioSource source;
    public AudioClip clip;

    private float _wait = 0;

    public void shoot()
    {
        if (_wait > 0)
            return;
        _wait = waitTime;
        source.PlayOneShot(clip);
        RaycastHit hit;
        Camera cam = Camera.main;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            if (hit.collider.tag == "Enemy")
            {
                hit.transform.gameObject.SendMessage("damage");
            }
            else
            {
                Rigidbody rigidBody = hit.transform.GetComponent<Rigidbody>();
                if (rigidBody != null)
                {
                    rigidBody.AddForceAtPosition(-hit.normal * forceShoot, hit.point);
                }

                GameObject obj = Instantiate<GameObject>(decal);
                obj.transform.position = hit.point + hit.normal * .1f;
                obj.transform.rotation = Quaternion.LookRotation(-hit.normal);
                obj.transform.SetParent(hit.transform);
            }
        }
    }

    void Update()
    {
        if (_wait > 0)
            _wait -= Time.deltaTime;
    }
}
