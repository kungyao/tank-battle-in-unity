using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavAgent : MonoBehaviour
{
    private NavMeshAgent agent;
    public List<GameObject>enemy = new List<GameObject>();
    Vector3 target;
    Vector3 lastPos;
    bool chasePlayer = false;
    void Start()
    {
        chasePlayer = false;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.speed = 10;
        agent.angularSpeed = 60;
        agent.stoppingDistance = 50;
        GameObject[] tmpEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        enemy.Clear();
        for (int i = 0; i < tmpEnemy.Length; i++)
        {
            if (tmpEnemy[i].Equals(this.gameObject))
                continue;
            enemy.Add(tmpEnemy[i]);
        }
        agent.updateRotation = false;
        agent.updatePosition = true;
    }
    void Update()
    {
        List<int> beRemove = new List<int>();
        target = this.transform.position;
        float distance = float.MaxValue;
        for (int i = 0; i < enemy.Count; i++)
        {
            if (!enemy[i])
            {
                beRemove.Add(i);
                continue;
            }
            float tmpDis = (enemy[i].transform.position - this.transform.position).magnitude;
            if (tmpDis < distance)
            {
                distance = tmpDis;
                target = enemy[i].transform.position;
            }
        }
        if (distance <= 150 && distance >= 50)
        {
            agent.SetDestination(target);
            Quaternion dir = Quaternion.identity;
            dir.SetLookRotation(this.transform.position - lastPos);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, dir, 0.5f);
            chasePlayer = true;
        }
        else if (chasePlayer)
        {
            agent.SetDestination(target);
            chasePlayer = false;
        }
        if(distance < 50)
        {
            Quaternion dir = Quaternion.identity;
            dir.SetLookRotation(target - this.transform.position);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, dir, 0.5f);
        }
        lastPos = this.transform.position;

        for (int i = 0; i < beRemove.Count; i++)
            enemy.RemoveAt(beRemove[i]);
    }
}
