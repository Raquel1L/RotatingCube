using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class RotatingCube : MonoBehaviour
{
    [SerializeField]
    private Text imageTrackedText;

    [SerializeField]
    private GameObject[] arObjectsToPlace; // "original" GameObjects
    
    [SerializeField]
    private Vector3 scaleFactor = new Vector3(0.1f,0.1f,0.1f);

    private ARTrackedImageManager m_TrackedImageManager;
    //dictionary with the closes
    private Dictionary<string,GameObject> arObjects = new Dictionary<string, GameObject>(); // dictionary with the key = name of the object and value the GameObject

    void Awake()
        {
            m_TrackedImageManager = GetComponent<ARTrackedImageManager>();

            foreach(GameObject arObject in arObjectsToPlace){ // all the gameobjects added from the editor
                GameObject newARObject = Instantiate(arObject,Vector3.zero,Quaternion.identity); // creation of the instanciated object
                newARObject.SetActive(false);
                newARObject.name = arObject.name; // add the name to the clone
                arObjects.Add(arObject.name,newARObject); // save the clone in the dictionary
            }
        
        }

        void OnEnable()
        {
            m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged; // Subscribe the event o .trackedImagesChanges ( check EVENTS on Image Tracking)
        }   // Every time a tracked Image is changed (added,updated,removed) it calls the method OnTrackedImagesChanged

        void OnDisable()
        {
            m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged; // Unsubscribe the event
        }

        void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
        {
            foreach (var trackedImage in eventArgs.added) // 1st detection of the image
                UpdatePrefab(trackedImage);
            foreach (var trackedImage in eventArgs.updated) // everytime it keeps the detection of the image
                UpdatePrefab(trackedImage);
            foreach (var trackedImage in eventArgs.removed) // if its been removed
                arObjects[trackedImage.name].SetActive(false); // the prefab ( cube ) disappear 
            
        }
        void UpdatePrefab(ARTrackedImage trackedImage) // trackedImage.referenceImage = reference image to "find" in the real world
        {  
            imageTrackedText.text = trackedImage.referenceImage.name; // Keep the name of the image
                                                                      // Useful if you want to print it like me on the TEXT component

            AssignGameObject(trackedImage.referenceImage.name,trackedImage.transform.position); // establish the position and activates the "clone" ( show im in the world)
        
            Debug.Log($"trackedImage.referenceImane.name: {trackedImage.referenceImage.name}");
        }

        void AssignGameObject(string name, Vector3 newPosition){
            // The name parameter is important because it allows the code to identify the proper clone we want to add ( the key of the dictionary is this string - name)
            if(arObjectsToPlace != null){ // if you dont forget to fullfull the list of game objects
                arObjects[name].SetActive(true); // its visible
                arObjects[name].transform.position = newPosition; // go to a position (in this case = to the image position)
                arObjects[name].transform.Rotate(5,0,0);
                arObjects[name].transform.localScale = scaleFactor; // scale (default value 10cm(x,y,z))
                foreach(GameObject go in arObjects.Values){ 
                    Debug.Log($"Go in arObjects.Values: {go.name}");
                    if(go.name != name){ // All images go disappear except the right one!
                        go.SetActive(false);
                    }
                }
            }
        }
}
