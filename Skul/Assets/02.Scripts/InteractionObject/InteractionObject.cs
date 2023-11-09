using Skul.Character.PC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    [SerializeField] private GameObject _notice;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        _notice.SetActive(true);
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
}
