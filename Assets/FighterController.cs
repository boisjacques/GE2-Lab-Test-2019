using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour
{
    public GameObject target;
    public GameObject bulletPrefab;
    public GameObject homeBase;
    public float tiberium;
    public bool inAttack;

    // Start is called before the first frame update
    void Start()
    {
        tiberium = 7;
        StartCoroutine(Fire());
        GetComponent<StateMachine>().ChangeState(new SearchTarget());
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator Fire()
    {
        while (true)
        {
            if (inAttack && tiberium > 0)
            {
                SpawnBullet();
                tiberium -= 1;
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    void SpawnBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
    }
}

public class SearchTarget : State
{
    public override void Enter()
    {
        owner.GetComponent<JitterWander>().enabled = true;
    }

    public override void Think()
    {
        GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag("base");
        GameObject maybeTarget = possibleTargets[Random.Range(0, possibleTargets.Length)];
        if (maybeTarget != owner.GetComponent<FighterController>().homeBase)
        {
            owner.GetComponent<FighterController>().target = maybeTarget;
            owner.GetComponent<Approach>().targetPosition = maybeTarget.transform.position;
            owner.ChangeState(new ApproachState());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<JitterWander>().enabled = false;
    }
}

public class ApproachState : State
{
    public override void Enter()
    {
        owner.GetComponent<Approach>().enabled = true;
    }

    public override void Think()
    {
        if (Vector3.Distance(
                owner.GetComponent<FighterController>().target.transform.position,
                owner.transform.position) < 3)
        {
            owner.ChangeState(new AttackState());
        }
    }

    public override void Exit()
    {
    }
}

public class AttackState : State
{
    public override void Enter()
    {
        owner.GetComponent<FighterController>().inAttack = true;
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
        owner.GetComponent<Approach>().enabled = false;
        owner.GetComponent<FighterController>().inAttack = false;
    }
}

public class RefuelState : State
{
    public override void Enter()
    {
        owner.GetComponent<Refuel>().baseGameObject = owner.GetComponent<FighterController>().homeBase;
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