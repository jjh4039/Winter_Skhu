using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Scanner : MonoBehaviour
{
    public string targetTag = "Enemy";
    public Enemy targetEnemy;
    public GameObject targetArrow;

    public GameObject FindNearestObjectWithTag()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);

        GameObject nearestObject = null;
        float shortestDistance = Mathf.Infinity; 

        Vector3 currentPosition = transform.position;

        foreach (GameObject target in targets)
        {
            if (target != null)
            {
                float distance = Vector3.Distance(currentPosition, target.transform.position);

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestObject = target;
                }
            }
        }

        return nearestObject;
    }

    void Update()
    {
        GameObject nearest = FindNearestObjectWithTag();

        if (nearest != null)
        {
            targetEnemy = nearest.GetComponent<Enemy>();
            targetArrow.SetActive(true);
            targetArrow.transform.position = new Vector3(nearest.transform.position.x, nearest.transform.position.y + 1.2f, nearest.transform.position.z);
        }
        else targetArrow.SetActive(false);
    }
}
