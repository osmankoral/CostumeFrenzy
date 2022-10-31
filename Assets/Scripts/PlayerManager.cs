using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    GameController gameController;

    private int failIndex;
    private int successIndex;
    private int passedIndex;
    private int requiredNumber;
    private GameObject[] costumes;
    private Transform costumeParent;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        costumeParent = transform.GetChild(1).GetChild(0);
        failIndex = 0;
        successIndex = 0;
        passedIndex = 0;
        requiredNumber = costumeParent.childCount;
        costumes = new GameObject[requiredNumber];
        for(int i=0; i<requiredNumber;i++)
        {
            costumes[i] = costumeParent.GetChild(i).gameObject;
        }
        costumes[successIndex].SetActive(true);

        gameController.LevelStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            Invoke(nameof(PlayerFail), 1f);
            
        }


        if(collision.gameObject.CompareTag("Target"))
        {
            Transform model = collision.transform.GetChild(0).GetChild(0);
            Transform costume = collision.transform.GetChild(0).GetChild(1);
            model.GetChild(successIndex).gameObject.SetActive(false);
            costume.GetChild(successIndex).gameObject.SetActive(true);
            passedIndex++;
            PlayerSuccess();
        }
    }

    private void PlayerFail()
    {
        failIndex++;
        if (failIndex == 2) PlayerSuccess();
        else
        {
            transform.position = new Vector3(0, 2, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void PlayerSuccess()
    {
        successIndex++;
        failIndex = 0;
        if (successIndex == requiredNumber) gameController.LevelEnd(requiredNumber, passedIndex);
        else
        {
            costumes[successIndex - 1].SetActive(false);
            costumes[successIndex].SetActive(true);
        }

        transform.position = new Vector3(0, 2, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }


}
