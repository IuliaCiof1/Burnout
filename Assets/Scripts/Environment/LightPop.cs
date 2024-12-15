using UnityEngine;

public class LightbulbScript : MonoBehaviour
{
    [SerializeField] public AudioClip[] LightPopAudio;
    public Light lightSource;
    public float triggerDistance = 5f;
    public Transform player;
    public int popChance = 3;
    private bool isLightChecked = false;
    private bool isLightOff = false;

    void Start()
    {
        if (lightSource == null)
        {
            lightSource = GetComponent<Light>();
        }

        // Ensure light is initially on
        if (lightSource != null)
        {
            lightSource.enabled = true;
        }
    }

    void Update()
    {
        if (player != null && lightSource != null && !isLightChecked)
        {
            // Check distance to the player
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // If the player is within the trigger distance, perform the random check
            if (distanceToPlayer <= triggerDistance)
            {
                isLightChecked = true;
                if (ShouldPop())
                {
                    PopLight();
                }
            }
        }
    }

    private bool ShouldPop()
    {
        
        int randomValue = Random.Range(1, popChance);
        Debug.Log(randomValue);
        if (randomValue > popChance/2)
        {
            return true;
        }
        else return false;
        
    }

    private void PopLight()
    {
        SoundFXManager.instance.PlaySoundFXClips(LightPopAudio, transform, 1f);
        lightSource.enabled = false;
        isLightOff = true; // Ensure the light stays off permanently
    }
}
