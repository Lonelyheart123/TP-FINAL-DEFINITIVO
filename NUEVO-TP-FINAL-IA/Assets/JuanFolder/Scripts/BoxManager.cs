using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    //public PlayerModel playermodel;

    public List<Box> boxes = new List<Box>();
    public float boxCount;
    public GameObject redPotionPrefab;
    public GameObject bluePotionPrefab;
    //public GameObject coinPrefab;

    public Dictionary<string, float> _dic = new Dictionary<string, float>();

    private void Start()
    {
        //playermodel = FindAnyObjectByType<PlayerModel>();
        boxCount = boxes.Count;
        Debug.Log(boxCount);
        _dic["Vacio"] = boxCount;
        _dic["PocionAzul"] = 5f;
        _dic["PocionRoja"] = 1f;
        //_dic["Coin"] = 1;
        boxes = null;
    }
    //public void UpdateRandom()
    //{
    //    _dic["PocionAzul"] = 2f;
    //    _dic["PocionRoja"] = 1f;
    //    _dic["PocionAzul"] = 7f * playermodel.multiplicadorEscudo;
    //    _dic["PocionRoja"] = 10f * playermodel.multiplicadorVida;
    //    _dic["Coin"] = 5 * GetCoinMultiplier();
    //}
    //public float GetCoinMultiplier()
    //{
    //    if (playermodel.multiplicadorEscudo == 1 && playermodel.multiplicadorVida == 1)
    //    {
    //        playermodel.multiplicadorMoneda = 10;
    //    }
    //    else
    //    {
    //        playermodel.multiplicadorMoneda = playermodel.multiplicadorEscudo / playermodel.multiplicadorVida;
    //    }
    //    return playermodel.multiplicadorMoneda;
    //}
}
