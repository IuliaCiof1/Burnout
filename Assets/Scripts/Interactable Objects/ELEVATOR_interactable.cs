using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ELEVATOR_interactable : MonoBehaviour, IInteractable
{

    private GameObject Player;
    private bool PlayerInside = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("PlayerManager");
    }

    private void Update()
    {
        if(Vector3.Distance(Player.transform.position, this.transform.GetChild(0).GetChild(0).Find("Plane_16").transform.position) < 1f)
        {
            PlayerInside = true;
        }
        else
        {
            PlayerInside = false;
        }
    }

    public void Interact()
    {
        StartCoroutine(ElevatorCoroutine());
    }

    IEnumerator ElevatorCoroutine()
    {
        this.gameObject.GetComponent<Animator>().SetBool("ButtonPressed", true);
        yield return new WaitForSecondsRealtime(8f);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        this.gameObject.GetComponent<Animator>().SetFloat("AnimSpeed", 0f);
        while(PlayerInside == false)
        {
            yield return null;
        }
        Player.transform.parent = this.gameObject.transform;
        this.gameObject.GetComponent<Animator>().SetFloat("AnimSpeed", 1f);
        yield return new WaitForSecondsRealtime(4f);
        this.transform.position += new Vector3(0, -3f, 0);
        Player.transform.parent = GameObject.Find("==== Player ====").transform;
    }
}
