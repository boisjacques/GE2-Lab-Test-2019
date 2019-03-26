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
        SetColor();
        StartCoroutine(IncreaseTiberium());
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
        fighter.transform.parent = transform;
        fighter.transform.TransformPoint(transform.position);
        fighter.GetComponent<FighterController>().homeBase = gameObject;
    }

    void SetColor()
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1, 1);
            r.material.color = color;
        }
    }
}