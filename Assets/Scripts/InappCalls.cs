using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InappCalls : MonoBehaviour
{
    [Space(5)]
    [Header("First inapp mainmenu")]
    public Button inappGetGranny;
    public Button btninappGetGrannyCall;
    public GameObject GetGrannyPanel;
    [Space(5)]
    [Header("Second inapp mainmenu")]
    public Button inappGetCat;
    public Button btninappGetCatCall;
    public GameObject GetCatPanel;
    [Space(5)]
    [Header("Third inapp mainmenu")]
    public Button inappGetAll;
    public Button btninappGetAllCall;
    public GameObject GetAllPanel;



    [Space(5)]
    [Header("Pet2")]
    public Button inappPet2;
    public Button btninappPet2Call;
    public GameObject GetPet2Panel;
    [Space(5)]
    [Header("Pet3")]
    public Button inappPet3;
    public Button btninappPet3Call;
    public GameObject GetPet3Panel;
    [Space(5)]
    [Header("Gran2")]
    public Button inappGran2;
    public Button btninappGran2Call;
    public GameObject GetGran2Panel;
    [Space(5)]
    [Header("Gran3")]
    public Button inappGran3;
    public Button btninappGran3Call;
    public GameObject GetGran3Panel;


    private void Start()
    {
        inappGetGranny.onClick.AddListener(OnInappGetGrannyClicked);
        inappGetCat.onClick.AddListener(OnInappGetCatClicked);
        inappGetAll.onClick.AddListener(OnInappGetAllClicked);

        btninappGetGrannyCall.onClick.AddListener(PurchaseAllGranny);
        btninappGetCatCall.onClick.AddListener(PurchaseAllCats);
        btninappGetAllCall.onClick.AddListener(PurchaseEverything);

        inappPet2.onClick.AddListener(PanelPet2);
        inappPet3.onClick.AddListener(PanelPet3);
        inappGran2.onClick.AddListener(PanelGranny2);
        inappGran3.onClick.AddListener(PanelGranny3);

        btninappPet2Call.onClick.AddListener(PurchasePet2);
        btninappPet3Call.onClick.AddListener(PurchasePet3);
        btninappGran2Call.onClick.AddListener(PurchaseGrann2);
        btninappGran3Call.onClick.AddListener(PurchaseGrann3);
    }


    //CALL FOR PANELS OPEN
    void OnInappGetGrannyClicked()
    {
        InterstitialAdCall.Instance.StartLoading(() =>
        {
            GetGrannyPanel.SetActive(true);
        });
    }  
    
    void OnInappGetCatClicked()
    {
        InterstitialAdCall.Instance.StartLoading(() =>
        {
            GetCatPanel.SetActive(true);
        });
    }  
    
    void OnInappGetAllClicked()
    {
        InterstitialAdCall.Instance.StartLoading(() =>
        {
            GetAllPanel.SetActive(true);
        });
    }

    //selection

    void PanelPet2()
    {
        InterstitialAdCall.Instance.StartLoading(() =>
        {
            GetPet2Panel.SetActive(true);
        });
    }
    void PanelPet3()
    {
        InterstitialAdCall.Instance.StartLoading(() =>
        {
            GetPet3Panel.SetActive(true);
        });
    }
    void PanelGranny2()
    {
        InterstitialAdCall.Instance.StartLoading(() =>
        {
            GetGran2Panel.SetActive(true);
        });
    }
    void PanelGranny3()
    {
        InterstitialAdCall.Instance.StartLoading(() =>
        {
            GetGran3Panel.SetActive(true);
        });
    }


    /// ////////////////////////////////////////////////////////////////////////////////////////////////

    //CALL FOR INAPPS

    void PurchaseAllGranny()
    {
        GameAppManager.instance.Unlock_All_Grans();
    }

    void PurchaseAllCats()
    {
        GameAppManager.instance.Unlock_All_Pets();
    }

    void PurchaseEverything()
    {
        GameAppManager.instance.Btn_Buy_Everything();
    }

    void PurchasePet2()
    {
        GameAppManager.instance.BuySecondPet();
    }
    void PurchasePet3()
    {
        GameAppManager.instance.BuyThirdPet();
    }
    void PurchaseGrann2()
    {
        GameAppManager.instance.BuySecondGran();
    }
    void PurchaseGrann3()
    {
        GameAppManager.instance.BuyThirdGran();
    }
}
