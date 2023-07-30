using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePotion : MonoBehaviour
{
    public bool bluePotionUtilizada = false;
    private void Update()
    {
        if (bluePotionUtilizada)
        {
            this.gameObject.SetActive(false);
            bluePotionUtilizada = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        var minion = other.GetComponent<MinionModel>();
        var leader = other.GetComponent<PathfindingEnemyModel>();

        /*if (other.gameObject.layer == 9 && other.CompareTag("Minions"))
        {
            minion.speed += 1;
            bluePotionUtilizada = true;

        }
        if (other.gameObject.layer == 9 && other.CompareTag("Leader"))
        {
            leader.speed += 5;
            bluePotionUtilizada = true;
        }*/
        if (minion)
        {
            minion.speed += 1;
            bluePotionUtilizada = true;
        }
        if (leader)
        {
            leader._currentLife += 25;
            bluePotionUtilizada = true;
        }
    }
}
