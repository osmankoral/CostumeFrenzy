using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapHold : MonoBehaviour
{
    [SerializeField] private float playerAngle;

    private float startTime, power, rotation;
    private Vector3 mouseStartPos;
    private bool isPlayerTouch;
    private GameObject sightObject;
    Rigidbody rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        sightObject = transform.GetChild(0).gameObject;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTime = Time.time;
            mouseStartPos = Input.mousePosition;
            sightObject.SetActive(true);
            isPlayerTouch = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (Time.time - startTime < 0.15f)
            {
                Tap();
            }
            else
            {
                Hold();
            }
        }

        if (isPlayerTouch) PlayerUpdate();
    }

    void Tap()
    {
    }

    void Hold()
    {
        isPlayerTouch = false;
        sightObject.SetActive(false);
        transform.rotation = Quaternion.Euler(playerAngle, rotation, 0);
        rb.AddForce(transform.forward* power);
        
        
    }

    void PlayerUpdate()
    {
        Vector3 mouseFinishPos = mouseStartPos - Input.mousePosition;
        power = mouseFinishPos.y;
        rotation = mouseFinishPos.x / 20; 
        sightObject.transform.localScale = new Vector3(1, 1, power / 1000);
        transform.rotation = Quaternion.Euler(0, rotation, 0);
        //Debug.Log(power);
    }
}
