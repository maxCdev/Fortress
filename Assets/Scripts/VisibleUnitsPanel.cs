using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class VisibleUnitsPanel : MonoBehaviour {

    public UnitSelector selector;
    ScrollRect scrolView;
	void Start () {
        scrolView = GetComponentInChildren<ScrollRect>();
        selector.OnCollectionChange += () =>
        {
            for (int i = 0; i < scrolView.content.childCount;i++)
            {
                Destroy(scrolView.content.GetChild(i).gameObject);
            }
            int y = 0;
            int x = 0;
            for (int i = 0; i < selector.selectedUnits.Count; i++)
            {
                Transform unitTransform = selector.selectedUnits[i];
                Sprite icon =unitTransform.GetComponent<UnitController>().unit.sprite;
                GameObject image = new GameObject();
                image.AddComponent<RectTransform>();
                image.AddComponent<Image>();
                image.AddComponent<Button>();
                image.GetComponent<Image>().sprite = icon;
                image.GetComponent<Button>().onClick.AddListener(() => 
                {
                    selector.SendMessage("TrySelect", unitTransform);
                });
                image.transform.localScale = new Vector3(0.5f, 0.5f, 0);
                image.transform.SetParent(scrolView.content.transform);
                Debug.Log(0 % 3);
                    if (i % 3 == 0&&i!=0)
                    {
                        y++;
                        x = 0;
                    }
                    image.transform.localPosition = Vector3.right * (30f+x*55f) + Vector3.down * (30f+y*55f);//new Vector3(i * 50 + 5, 0, 0);
                    x++;
            }
            Debug.Log("UpdateVisibleUnits");
        };
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
