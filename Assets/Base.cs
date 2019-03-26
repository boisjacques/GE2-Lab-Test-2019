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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            tiberium -= 0.5f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("fighter")
            && tiberium >= 7
            && other.GetComponent<FighterController>().needsRefueling)
        {
            other.GetComponent<FighterController>().tiberium = 7;
            tiberium -= 7;
        }
    }

    void SpawnFighter()
    {
        GameObject fighter = Instantiate(fighterPrefab);
        fighter.transform.position = transform.position;
        fighter.GetComponent<FighterController>().homeBase = gameObject;
        fighter.GetComponent<Refuel>().baseGameObject = gameObject;
        fighter.GetComponent<Refuel>().basePosition = transform.position;
        Renderer renderer = fighter.GetComponent<Renderer>();
        renderer.material.color = color;
    }

    void SetColor()
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.material.color = color;
        }
    }
}