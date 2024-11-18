using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CardUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] PlayerCards cards;
    [SerializeField] GameObject ui_Card;

    public void Start()
    {
        GameEvents.current.onFilledHand += SetUpInitCards;
        cards = GameObject.FindAnyObjectByType<PlayerCards>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void SetUpInitCards()
    {
        if (cards.playerHand.Count > 0)
        {
            for (int i = 0; i < cards.playerHand.Count; i++)
            {
                GameObject card = Instantiate(ui_Card, gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
                card.GetComponent<Card>().SetCard(cards.playerHand[i]);
            }
        }
    }

}
