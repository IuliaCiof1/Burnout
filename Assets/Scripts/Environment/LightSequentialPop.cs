using System.Collections;
using UnityEngine;

public class SequentialLightbulb : MonoBehaviour
{
    [SerializeField] public AudioClip[] LightPopAudio;

    // Reference to the light component
    private Light lightSource;

    // Delay between each light turn off
    public float delayBetweenLights = 0.5f;

    // Reference to the next lightbulb in the sequence
    public SequentialLightbulb nextLight;

    // Distance at which the player can trigger the light sequence
    public float triggerDistance = 5f;

    // Reference to the player object
    public Transform player;

    // Reference to the red exit light (to turn on after sequence)
    public Light redExitLight;

    private bool hasTriggered = false;

    private void Start()
    {
        // Get the Light component attached to this object
        lightSource = GetComponent<Light>();

        // Ensure the light is initially on
        if (lightSource != null)
        {
            lightSource.enabled = true;
        }

        // Ensure the red exit light is off initially
        if (redExitLight != null)
        {
            redExitLight.enabled = false;
        }
    }

    private void Update()
    {
        // Only trigger if the light hasn't already been triggered and the player is close enough
        if (!hasTriggered && player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= triggerDistance)
            {
                // Start the sequence if the player is close enough
                hasTriggered = true;
                StartCoroutine(TurnOffLightSequence());
            }
        }
    }

    private IEnumerator TurnOffLightSequence()
    {
        // Wait for a small moment before starting the sequence
        yield return new WaitForSeconds(0.1f);

        // Turn off this light
        if (lightSource != null)
        {
            SoundFXManager.instance.PlaySoundFXClips(LightPopAudio, transform, 1f);
            lightSource.enabled = false;
        }

        // If there's a next light in the sequence, wait and trigger it
        if (nextLight != null)
        {
            yield return new WaitForSeconds(delayBetweenLights);
            nextLight.StartCoroutine(nextLight.TurnOffLightSequence());
        }
        else
        {
            // Once the last light pops, wait for 2 seconds, then turn on the red exit light
            if (redExitLight != null)
            {
                Debug.Log("Red light will turn on in 2 seconds.");
                yield return new WaitForSeconds(2f);
                Debug.Log("Red light should be on now.");
                redExitLight.enabled = true;
            }
            else
            {
                Debug.Log("Red exit light is not assigned.");
            }
        }
    }

    // Optional: If you prefer using a trigger collider for detection
    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            // Start the sequence when the player enters the trigger zone
            hasTriggered = true;
            StartCoroutine(TurnOffLightSequence());
        }
    }
}