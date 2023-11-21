using Skul.Character.PC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    [SerializeField] private GameObject _notice;
    [SerializeField] public int sortingObject;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        _notice.SetActive(true);

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        if (collision.GetComponent<Player>().canInteractionObject == this)
            _notice.SetActive(true);
        else
            _notice.SetActive(false);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        _notice.SetActive(false);
    }

    public virtual void Interaction(Player player)
    {

    }
    public virtual void SeeDetails(Player player)
    {

    }
    public virtual void ColseDetails(Player player)
    {

    }
}
