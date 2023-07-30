using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPotion : MonoBehaviour
{
    public bool redPotionUtilizada = false;
    private void Update()
    {
        if (redPotionUtilizada)
        {
            this.gameObject.SetActive(false);
            redPotionUtilizada = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        var minion = other.GetComponent<MinionModel>();
        var leader = other.GetComponent<PathfindingEnemyModel>();

        /*if (other.gameObject.layer == 9 && other.CompareTag("Minions"))
        {
            minion.speed+=1;
            redPotionUtilizada = true;

        }
        if (other.gameObject.layer == 9 && other.CompareTag("Leader"))
        {
            leader._currentLife += 1;
            redPotionUtilizada = true;
        }*/
        if (minion)
        {
            minion.speed += 1;
            redPotionUtilizada = true;
        }
        if (leader)
        {
            leader._currentLife += 25;
            redPotionUtilizada = true;
        }
    }
}
