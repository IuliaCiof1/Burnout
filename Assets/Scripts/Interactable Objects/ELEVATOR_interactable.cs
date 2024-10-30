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
        yield return new WaitWhile(() => PlayerInside == true);
        this.gameObject.GetComponent<Animator>().SetFloat("AnimSpeed", 1f);
        GameObject.Find("PlayerManager").transform.parent = this.gameObject.transform;
        yield return new WaitForSecondsRealtime(2f);
        this.transform.position += new Vector3(0, -3.2f, 0);

    }
}
