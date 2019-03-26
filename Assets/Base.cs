using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Base : MonoBehaviour
{
    public float tiberium = 0;

    public TextMeshPro text;

    public GameObject fighterPrefab;

    public Color color;


    // Start is called before the first frame update
    void Start()
    {
        color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1, 1);
        SetColor();
        StartCoroutine(IncreaseTiberium());
        tag = "base";
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "" + tiberium;
        if (tiberium >= 10)
        {
            SpawnFighter();
            SetColor();
            tiberium -= 10;
        }
    }

    IEnumerator IncreaseTiberium()
    {
        while (true)
        {
            tiberium += 1;
            yield return new WaitForSeconds(1);
        }
    }

    void SpawnFighter()
    {
        GameObject fighter = Instantiate(fighterPrefab);
        //fighter.transform.TransformPoint(transform.position);
        fighter.transform.position = transform.position;
        fighter.GetComponent<FighterController>().homeBase = gameObject;
        fighter.GetComponent<Refuel>().baseGameObject = gameObject;
        fighter.GetComponent<Refuel>().basePosition = transform.position;
    }

    void SetColor()
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.material.color = color;
        }
    }
}