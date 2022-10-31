using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]private Camera danceCamera;
    [SerializeField]private Camera playCamera;

    private GameObject target;
    private Animator targetAnimator;

    public void LevelStart()
    {
        danceCamera.gameObject.SetActive(false);
        playCamera.gameObject.SetActive(true);
        target = GameObject.FindGameObjectWithTag("Target");
        targetAnimator = target.GetComponentInChildren<Animator>();
        targetAnimator.SetBool("isDancing", false);
        
    }

    public void LevelEnd(int _reqItem, int _successItem)
    {
        StartCoroutine(LevelEndDelay(_reqItem, _successItem));
    }
    IEnumerator LevelEndDelay(int _reqItem, int _successItem)
    {
        yield return new WaitForSeconds(0.4f);
        targetAnimator.SetBool("isDancing", true);
        playCamera.gameObject.SetActive(false);
        danceCamera.gameObject.SetActive(true);

        // ending Animation State
        yield return new WaitForSeconds(3f);
        float dif = _reqItem - _successItem;
        float ration =  dif/_reqItem;
        bool isPass = (1- ration > 0.2f) ? true : false;
        LevelStateFunc(isPass);
    }

    private void LevelStateFunc(bool _state)
    {
        GameManager.Instance.LevelState(_state);

    }

}
