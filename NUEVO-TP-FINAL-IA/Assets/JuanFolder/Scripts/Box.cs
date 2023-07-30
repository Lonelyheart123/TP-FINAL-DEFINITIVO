using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    BoxManager instance;
    public bool onRange;

    private void Awake()
    {
        instance = FindObjectOfType<BoxManager>();
        instance.boxes.Add(this);
    }

    private void BreakBox()
    {
        if (onRange == true)
        {
            //var item = CajitaRandom.Roulette(instance._dic);
            //var ruletanueva = new CajitasManager();
            //instance.UpdateRandom();
            var item = BoxRandom.Roulette(instance._dic);

            if (item == "PocionAzul")
            {
                print(item);
                Instantiate(instance.bluePotionPrefab, this.transform.position, Quaternion.identity);
                //instance._dic["PocionAzul"] -= 1;
            }
            else if (item == "PocionRoja")
            {
                Instantiate(instance.redPotionPrefab, this.transform.position, Quaternion.identity);
                print(item);
                //instance._dic["PocionRoja"] -= 1;
            }
            else
            {
                //instance._dic["Vacio"] -= 1;
                print(item);
            }
            this.gameObject.SetActive(false);
            item = null;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer==9)
        {
            Debug.Log("ColisionCaja");
            onRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer ==9)
        {
            onRange = false;
        }

    }
    private void Update()
    {
        BreakBox();
    }
}
