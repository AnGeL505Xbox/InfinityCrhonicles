using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public Text txCode;
    public Text txCodeDescription;

    public void ChangeReward()
    {
        for (int i = 0; i < GameController.currentGame.playerCodes.Count; i++)
        {
            if (txCode.text == "DebugPls" && GameController.currentGame.playerCodes[i] == txCode.text)
            {
                Debug.Log("DebugPls");
                GameController.currentGame.stellarCoin += 500;
                GameController.currentGame.goldCoin += 1000;
                txCodeDescription.text = "Canjeaste el codigo: DebugPls, lo cual te da 500 Monedas Estelares y 1000 Monedas de Oro para poder canjear en la ruleta";
                GameController.currentGame.playerCodes.RemoveAt(i);
            }
            else if (txCode.text == "Lumity" && GameController.currentGame.playerCodes[i] == txCode.text)
            {
                Debug.Log("Lumity");
                GameController.currentGame.playerFigtherCards.Add(GameController.currentGame.tFigtherCard.Find(x => x.idName.Contains("Leah - Jefa de seguridad")));
                GameController.currentGame.playerFigtherCards.Add(GameController.currentGame.tFigtherCard.Find(x => x.idName.Contains("Lisbeth - Investigadora")));
                txCodeDescription.text = $"Canjeaste el codigo: Lumity, lo cual te da la Carta Peleador \"Leah - Jefa de Seguridad\" y la Carta Peleador \"Lisbeth - Investigadora\"";
                GameController.currentGame.playerCodes.RemoveAt(i);
            }
            else
            {
                txCodeDescription.text = "El codigo no existe o ya ha sido canjeado";
            }
        }
        SaveLoad.SaveData(GameController.currentGame, "Data", "Save");
    }
}
