using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Studio.Core.Singleton;

public class ItemManager : Singleton<ItemManager>
{
    public SOInt coins;
    public TMP_Text textHud;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins.value = 0;
        
    }

    //caso não seja passado no parametro, ele vai ser naturalmente 1
    public void AddCoins(int amount = 1)
    {
        coins.value += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        //em vez de colocar instance aqui, podemos colocar no manager "CUIDADO PRA NÃO USAR MUITOS SINGLETONS"
        //JÁ QUE ESTAMOS UTILIZANDO SCRIPTABLE OBJECT, O UPDATE PARA DE OCORRER AQUI
        //UiInGameManager.UpdateTextCoins("X"+coins.value.ToString());
    }
}
