using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gun : MonoBehaviour
{
    public Game_Manager game_Manager;
    public bool inUsed = false;
    public GameObject projectile;
    public float bulletSpeed;
    public Transform firePoint;
    Rigidbody2D rb2d;
    //float speedRotate = 5f;
    public float fireRate = 0.1f;
    private float nextFire = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(game_Manager.playerIn == this && Input.touchCount > 0 )
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    Debug.Log("Touched the UI");
                }
                else
                {
                    if(Time.time > nextFire)
                    {
                        nextFire = Time.time + fireRate;
                        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                        Vector2 lookdir = touchPos - rb2d.position;
                        float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg - 90f;
                        rb2d.rotation = angle;

                        Vector2 firedir = touchPos - (new Vector2(firePoint.position.x, firePoint.position.y));
                        firedir.Normalize();
                        GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
                        bullet.GetComponent<Rigidbody2D>().velocity = firedir * bulletSpeed;
                    }    

                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                //
            }
        }    
    }
}
