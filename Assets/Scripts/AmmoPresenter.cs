using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AmmoPresenter : MonoBehaviour
{
    Label label;

    // Start is called before the first frame update
    void OnEnable()
    {
        var uIDoc = GetComponent<UIDocument>();
        var rootVisualElement = uIDoc.rootVisualElement;
        label = rootVisualElement.Q<Label>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
