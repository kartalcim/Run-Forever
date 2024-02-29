using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class YolManager : MonoBehaviour
{
    public GameObject[] yollar;
    private List<GameObject> aktifYollar;
    public float yolSpawn = 0;
    public float yolUzunlugu = 30;
    public int yolSayisi = 3;
    public int toplamYolSayisi = 8;
    private Transform playerTransform;
    private int oncekiIndex;
    void Start()
    {
        aktifYollar = new List<GameObject>();
        for (int i = 0; i < yolSayisi; i++)
        {
            if (i == 0)
                YolSpawn();
            else
                YolSpawn(Random.Range(0, toplamYolSayisi));
        }
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (playerTransform.position.z - 33 >= yolSpawn - (yolSayisi * yolUzunlugu))
        {
            int index = Random.Range(0, toplamYolSayisi);
            while (index == oncekiIndex)
                index = Random.Range(0, toplamYolSayisi);
            YolSil();
            YolSpawn(index);
        }
    }
    public void YolSpawn(int index = 0)
    {
        GameObject yol = yollar[index];
        if (yol.activeInHierarchy)
            yol = yollar[index + 8];

        if (yol.activeInHierarchy)
            yol = yollar[index + 16];

        yol.transform.position = Vector3.forward * yolSpawn;
        yol.transform.rotation = Quaternion.identity;
        yol.SetActive(true);

        aktifYollar.Add(yol);
        yolSpawn += yolUzunlugu;
        oncekiIndex = index;
    }
    private void YolSil()
    {
        aktifYollar[0].SetActive(false);
        aktifYollar.RemoveAt(0);
        GameManager.puan += 8;
    }
}
