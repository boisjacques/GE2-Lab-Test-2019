using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour
{
    public GameObject target;
    public GameObject bulletPrefab;
    public GameObject homeBase;
    public float tiberium;
    
    // Start is called before the first frame update
    void Start()
    {
        tiberium = 7;
        StartCoroutine(Fire());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Fire()
    {
        if (Vector3.Distance(target.transform.position, transform.position) <= 1)
        {
            Instantiate(bulletPrefab);
            tiberium -= 1;
        }
        yield return new WaitForSeconds(0.01f);
    }

    public float GetTiberium()
    {
        return tiberium;
    }
}

public class SearchTarget : State
{
    public override void Enter()
    {
        owner.GetComponent<Attack>().target = owner.GetComponent<FighterController>().target.GetComponent<GameObject>();
        owner.GetComponent<Attack>().enabled = true;
    }

    public override void Think()
    {
        if (owner.GetComponent<FighterController>().tiberium <= 0)
        {
            owner.ChangeState(new AttackState());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<Attack>().enabled = false;
    }
}

public class AttackState : State
{
    public override void Enter()
    {
        owner.GetComponent<Attack>().target = owner.GetComponent<FighterController>().target.GetComponent<GameObject>();
        owner.GetComponent<Attack>().enabled = true;
    }

    public override void Think()
    {
        if (owner.GetComponent<FighterController>().tiberium <= 0)
        {
            owner.ChangeState(new RefuelState());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<Attack>().enabled = false;
    }
}

public class RefuelState : State
{
    public override void Enter()
    {
        owner.GetComponent<Refuel>().BaseGameObject = owner.GetComponent<FighterController>().target;
        owner.GetComponent<Refuel>().enabled = true;
    }

    public override void Think()
    {
        if (owner.GetComponent<FighterController>().tiberium >= 7)
        {
            owner.ChangeState(new SearchTarget());
        }
    }
    public override void Exit()
    {
        owner.GetComponent<Refuel>().enabled = false;
    }
}