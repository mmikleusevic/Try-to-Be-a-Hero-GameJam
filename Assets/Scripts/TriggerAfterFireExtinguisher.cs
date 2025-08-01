using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TriggerAfterFireExtinguisher : MonoBehaviour
{
    [SerializeField] private GameObject brotherGameObject;
    [SerializeField] private GameObject fireToDisableGameObject;
    [SerializeField] private GameObject smokeToEnableGameObject;
    [SerializeField] private Image rageBarImage;
    [SerializeField] private AudioClip rageAudioClip;
        
    private Camera mainCamera;
    private PlayerMovement playerMovement;
    private MouseLook mouseLook;
    private void Start()
    {
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        mouseLook = playerMovement.GetComponentInChildren<MouseLook>();
        mainCamera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement playerMovement))
        {
            MouseLook mouseLook = playerMovement.GetComponentInChildren<MouseLook>();
            if (mouseLook)
            {
                PickUpObject pickUpObject = mouseLook.GetCurrentlySelectedObject();
                if (pickUpObject && pickUpObject.name == "FireExtinguisher")
                {
                    playerMovement.enabled = false;
                    mouseLook.enabled = false;
                    brotherGameObject.SetActive(true);
                    fireToDisableGameObject.SetActive(false);
                    smokeToEnableGameObject.SetActive(true);
                    AudioManager.Instance.StopSFXMusic();
                    AudioManager.Instance.PlaySFXMusic(rageAudioClip);
                    StartCoroutine(LerpRageBar());
                    StartCoroutine(ZoomToBrother());
                }
            }
        }
    }

    private IEnumerator LerpRageBar()
    {
        float timer = 0;
        float duration = 1f;
        while (timer < duration)
        {
            rageBarImage.fillAmount = Mathf.Lerp(rageBarImage.fillAmount, 0.66f, timer / duration);
            timer += Time.deltaTime;
            
            yield return null;
        }

        rageBarImage.fillAmount = 0.66f;
        yield return null;
    }
    
    private IEnumerator ZoomToBrother()
    {
        Vector3 originalCamPos = mainCamera.transform.position;
        Quaternion originalCamRot = mainCamera.transform.rotation;
        
        Vector3 zoomTargetPos = brotherGameObject.transform.position + brotherGameObject.transform.forward * +5f + Vector3.up * 5f;
        Quaternion zoomTargetRot = Quaternion.LookRotation(brotherGameObject.transform.position - zoomTargetPos);

        float startFOV = mainCamera.fieldOfView;
        float time = 0f;

        while (time < 1f)
        {
            float t = time / 1f;

            mainCamera.transform.position = Vector3.Lerp(originalCamPos, zoomTargetPos, t);
            mainCamera.transform.rotation = Quaternion.Slerp(originalCamRot, zoomTargetRot, t);
            mainCamera.fieldOfView = Mathf.Lerp(startFOV, 30, t);

            time += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = zoomTargetPos;
        mainCamera.transform.rotation = zoomTargetRot;
        mainCamera.fieldOfView = 30;
        
        yield return new WaitForSeconds(5f);
        
        LevelManager.Instance.LoadScene(Scenes.Level3);
    }
}
