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
    private GameObject[] arObjectsToPlace; // GameObjects "originais"
    
    [SerializeField]
    private Vector3 scaleFactor = new Vector3(0.1f,0.1f,0.1f);

    private ARTrackedImageManager m_TrackedImageManager;
    //dicionario com os GameObjects Instanciados (clones)
    private Dictionary<string,GameObject> arObjects = new Dictionary<string, GameObject>(); // dicionario com a key = ao nome do objeto e o valor o GameObject instanciado

    void Awake()
        {
            m_TrackedImageManager = GetComponent<ARTrackedImageManager>();

            foreach(GameObject arObject in arObjectsToPlace){ // todos os GameObjects que foram adicionados pelo editor
                GameObject newARObject = Instantiate(arObject,Vector3.zero,Quaternion.identity); // criação do objeto instanciado ( clone do original )
                newARObject.SetActive(false);
                newARObject.name = arObject.name; // adiciona-se o nome ao clone
                arObjects.Add(arObject.name,newARObject); // guarda-se o clone no dicionario!
            }
        
        }

        void OnEnable()
        {
            m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged; // Subscreve o evento .trackedImagesChanges
            // Sempre que uma trackedImage é alterada (adicionada,atualizada,removida) chama o metodo OnTrackedImagesChanged
        }

        void OnDisable()
        {
            m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged; // Deixa de Subscrever ao evento
        }

        void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
        {
            foreach (var trackedImage in eventArgs.added) // sempre que a imagem é detetada pela primeira vez
                UpdatePrefab(trackedImage);
            foreach (var trackedImage in eventArgs.updated) // se continuar a ser mostrada
                UpdatePrefab(trackedImage);
            foreach (var trackedImage in eventArgs.removed) // se for removida
                arObjects[trackedImage.name].SetActive(false); // retira o prefab se a imagem desaparecer
            
        }
        void UpdatePrefab(ARTrackedImage trackedImage) // trackedImage.referenceImage = Imagem de referencia procurada no cenario ( mundo real )
        {  
            imageTrackedText.text = trackedImage.referenceImage.name; // Permite guardar o nome da imagem que foi detetada em questão (QRCODE)
                                                                      // Para depois mostrar na aplicação o nome da imagem (QRCODE)

            AssignGameObject(trackedImage.referenceImage.name,trackedImage.transform.position); // posicionar e "ativar" (mostrar no mundo) os clones
        
            Debug.Log($"trackedImage.referenceImane.name: {trackedImage.referenceImage.name}");
        }

        void AssignGameObject(string name, Vector3 newPosition){
            // o parametro nome permite alterar o colone desejado pois o nome corresponde à chave do dicionario (identificador dos elementos)
            if(arObjectsToPlace != null){ // se forem adicionados os GameObjects no editor 
                arObjects[name].SetActive(true); // fica visivel
                arObjects[name].transform.position = newPosition; // vai para a posição newPosition ( neste caso corresponde à propria posicao da imagem)
                arObjects[name].transform.Rotate(5,0,0);
                arObjects[name].transform.localScale = scaleFactor; // fica com a escala scaleFactor (valor normal - retirado da UNITY - dimensao pequena e visivel)
                foreach(GameObject go in arObjects.Values){ 
                    Debug.Log($"Go in arObjects.Values: {go.name}");
                    if(go.name != name){ // coloca todos inativos menos o atual -> evita sobreposição de imagens
                        go.SetActive(false);
                    }
                }
            }
        }
}
