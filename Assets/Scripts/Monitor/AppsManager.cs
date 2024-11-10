using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppsManager : MonoBehaviour
{
    [SerializeField] private GameObject []apps;
    [SerializeField] private GameObject bottomBar;

    [Header (header:"Names of app's children")]
    [ReadOnly] [SerializeField] private string windowObjectName = "Window";
    [ReadOnly] [SerializeField] private string desktopIconObjectName = "Desktop Icon";
    [ReadOnly] [SerializeField] private string barTabObjectName = "Bar Tab";
    [ReadOnly] [SerializeField] private string closeButtonName = "CloseButton";


    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject app in apps)
        {
            GameObject window, icon, barTab;

            window = app.transform.Find(windowObjectName).gameObject;
            icon = app.transform.Find(desktopIconObjectName).gameObject;
            barTab = app.transform.Find(barTabObjectName).gameObject;

           

            barTab.transform.SetParent(bottomBar.transform);

            Button iconButton = icon.GetComponent<Button>();
            iconButton.onClick.AddListener(() => { OpenApp(app, window, barTab); });

            
        }
    }

    void OpenApp(GameObject app, GameObject window, GameObject barTab)
    {
       

        barTab.SetActive(true);
        window.SetActive(true);

        //Setting the windows's position at the center of the monitor
        //
        RectTransform windowRect = window.GetComponent<RectTransform>();
        RectTransform appRect = app.GetComponent<RectTransform>();
        RectTransform monitorRect = GetComponent<RectTransform>();

        //converts monitor's center to world space from local space. Then converts the position into local space relative to the window's parent (which is app)
        windowRect.localPosition = appRect.InverseTransformPoint(monitorRect.TransformPoint(monitorRect.rect.center));

        
     

        app.transform.SetAsLastSibling();


        //Set close app listener
        GameObject closeButton_;
        print(app.name);
        closeButton_ = window.transform.Find("TopBar").Find(closeButtonName).gameObject;
        Button closeButton = closeButton_.GetComponent<Button>();
        closeButton.onClick.AddListener(() => { CloseApp(window, barTab); });
    }

    
    void CloseApp(GameObject window, GameObject barTab)
    {
        window.SetActive(false);
        barTab.SetActive(false);
    }
 
}
