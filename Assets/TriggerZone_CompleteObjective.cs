using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class TriggerZone_CompleteObjective : TriggerZone
{
    [SerializeField] private string methodName;

    public override void Trigger()
    {
        //Reflection -> use strings to call a function
        // Get the type of the static class
        System.Type type = typeof(ObjectiveEvents);

       
        //Get the method information using the method info class
        // mi = type.GetType().GetMethod(methodName);
        MethodInfo mi = type.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

        if (mi == null)
        {
            Debug.LogError($"TriggerZone_CompletedObjective: Method {methodName} not found in ObjectiveEvents.");
            return;
        }


        //Invoke the method
        // (null- no parameter for the method call
        // or you can pass the array of parameters...)
        mi.Invoke(null, null);

        
    }

   
}
