using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TriggerZone_EndGame : MonoBehaviour
{
    #region Declarations
    [SerializeField] private float slideSpeed = 5f;
    [SerializeField] private float slideDuration = 2f;
    [SerializeField] private TMP_Text dialogTextUI;
    [SerializeField] private string dialogue;
    [SerializeField] private GameObject textContainer;
   // [SerializeField] private GameObject Monster;
    [SerializeField] private GameObject LookingPoint;
    [SerializeField] private Animator PlayerAnimator;
    [SerializeField] private GameObject Player;
    [SerializeField] private Transform playerCamera; // Assign the player's camera in the Inspector
    [SerializeField] private float cameraRotateDuration = 1f; // Duration for camera rotation
    private bool isSliding = false; // To track if the player is sliding
    private float slideTimeElapsed = 0f; // Timer to track the sliding progress
    [SerializeField] private AudioClip EndSound;
    [SerializeField] private AudioClip ScreamingSound;
    [SerializeField] private Image fadeImage; // Assign fade image from your UI
    [SerializeField] private float fadeDuration = 1f; // Duration for fade effect

    private Controller playerMovement; // Reference to player's movement script
    #endregion

    private void Start()
    {
        // Find and cache the PlayerMovement script (adjust to match your player movement script's name)
        playerMovement = FindObjectOfType<Controller>();
        if (playerMovement == null)
        {
            Debug.LogWarning("PlayerMovement script not found. Ensure your player has the movement script.");
        }
    }
    void Update()
    {
        if (isSliding)
        {
            SlideOnZAxis();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(EndGameSequence());
        }
    }

    #region Methods
    private IEnumerator EndGameSequence()
    {
        // Disable player movement
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        // Disable Monster
        //Monster.SetActive(false);

        // Rotate camera to LookingPoint
        RotateCameraToLookingPoint();
        yield return new WaitForSeconds(1);

        // Play sound effect
        SoundFXManager.instance.PlaySoundFXClip(EndSound, transform, 1f);

        // Start dialogue
        ActionManager.Instance.HandleTrigger(1, dialogue, null, null);


        StartDialogue(dialogue);
        yield return new WaitForSeconds(cameraRotateDuration);

        // Trigger end animation
        PlayerAnimator.SetBool("isEnd", true);
        yield return new WaitForSeconds(4);

        // Fade out and transition to main menu
        yield return StartCoroutine(FadeOutAndLoadScene("Main Menu"));
    }

    private void StartDialogue(string dialogText)
    {
        if (textContainer != null)
        {
            textContainer.SetActive(true);
            dialogTextUI.text = dialogText;
            StartCoroutine(DialogTimer());
            Debug.Log("Dialogue Started: " + dialogText);
        }
        else
        {
            Debug.LogWarning("Text Container is not assigned!");
        }
    }

    private IEnumerator DialogTimer()
    {
        yield return new WaitForSeconds(2);
        if (textContainer != null)
        {
            textContainer.SetActive(false);
        }
    }

    private void RotateCameraToLookingPoint()
    {
        if (playerCamera != null && LookingPoint != null)
        {
            Vector3 direction = LookingPoint.transform.position - playerCamera.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Rotate smoothly to face LookingPoint
            playerCamera.DORotateQuaternion(targetRotation, cameraRotateDuration)
                .SetEase(Ease.InOutSine);
        }
        else
        {
            Debug.LogWarning("Player Camera or LookingPoint is not assigned!");
        }
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        fadeImage.gameObject.SetActive(true);
        Color color = fadeImage.color;
        color.a = 0;
        fadeImage.color = color;
        SoundFXManager.instance.PlaySoundFXClip(ScreamingSound, transform, 1f);
        StartSlide();
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Clamp01(timer / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
    void SlideOnZAxis()
    {
        // Slide for the specified duration
        if (slideTimeElapsed < slideDuration)
        {
            Vector3 movement = Vector3.forward * slideSpeed * Time.deltaTime;
            Player.transform.Translate(movement, Space.World);
            slideTimeElapsed += Time.deltaTime;
        }
        else
        {
            // Stop sliding after the duration is complete
            isSliding = false;
        }
    }
    public void StartSlide()
    {
        // Reset the sliding state and timer
        isSliding = true;
        slideTimeElapsed = 0f;
    }
    #endregion
}
