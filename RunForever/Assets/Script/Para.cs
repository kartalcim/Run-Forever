using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Para : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameObject.SetActive(false);
            FindAnyObjectByType<GameManager>().paraTopla.Play();
            GameManager.para += 1;
            PlayerPrefs.SetInt("ToplamPara", PlayerPrefs.GetInt("ToplamPara", 0) + 1);
            GameManager.puan += 3;
            Invoke("ParaAktifEt", 3);
        }
    }
    private void ParaAktifEt()
    {
        gameObject.SetActive(true);
    }
}
