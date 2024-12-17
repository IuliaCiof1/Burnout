using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public List<Objective> objectives;
    //public ObjectiveUI objectiveUI;
    private int currentObjectiveIndex;


    [Header(header: "Objectives UI")]
    [SerializeField] GameObject objectivePrefab;
    [SerializeField] Transform objectiveContainer;
    TMP_Text description;

    private void OnEnable()
    {
        ObjectiveEvents.OnObjectiveCompleted += HandleObjectiveCompleted;
    }

    private void OnDisable()
    {
        ObjectiveEvents.OnObjectiveCompleted -= HandleObjectiveCompleted;
    }

    private void Start()
    {
        StartNextObjective();
    }

    private void HandleObjectiveCompleted(Objective completedObjective)
    {
        description.text = "<s>" + description.text;
        completedObjective.DeactivateObjective(); // Clean up
        currentObjectiveIndex++;
        print("completed quest");
        StartNextObjective();
    }

    private void StartNextObjective()
    {
        if (currentObjectiveIndex < objectives.Count)
        {
           
            Objective nextObjective = objectives[currentObjectiveIndex];

            DisplayObjective(nextObjective);

            nextObjective.ActivateObjective();
            //Debug.Log($"Objective Started: {nextObjective.description}");
        }
        else
        {
            Debug.Log("All objectives completed!");
        }
    }

    private void DisplayObjective(Objective objective)
    {
        GameObject objectiveUI =  Instantiate(objectivePrefab, objectiveContainer);
        objectiveUI.transform.SetAsFirstSibling();


        description =  objectiveUI.transform.GetChild(0).GetComponent<TMP_Text>();
        description.text = objective.description;
    }

    //void Start()
    //{
    //    objectives[objectiveIndex].ActivateObjective();

    //    //foreach (var objective in objectives)
    //    //{
    //    //    objective.StartObjective();
    //    //    //objectiveUI.DisplayObjective(objective);
    //    //}
    //}

//    public void CompleteObjective(Objective objective)
//    {
//        if (!objective.isCompleted)
//        {
//            objective.DeactivateObjective();
//            objectiveIndex++;

//            //Start the next objective
//            objectives[objectiveIndex].ActivateObjective();
//            //objectiveUI.RemoveObjective(objective);
//        }
//    }
}
