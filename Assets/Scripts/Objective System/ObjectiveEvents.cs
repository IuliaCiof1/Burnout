using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectiveEvents
{
    public static event Action <Objective> OnObjectiveCompleted;


    public static event Action OnEmailSent;
    public static event Action OnSpookyEmailSent;
    public static event Action OnOpenSpookyMail;


    public static void ObjectiveCompleted(Objective objective) => OnObjectiveCompleted?.Invoke(objective);

    public static void SendEmail() => OnEmailSent?.Invoke();
    public static void SendSpookyEmail() => OnSpookyEmailSent?.Invoke();
    public static void OpenSpookyMail() => OnOpenSpookyMail?.Invoke();
}
