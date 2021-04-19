using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip _sound;    // this lets you drag in an audio file in the inspector
    private AudioSource audio;

    void Start()
    {
        if (_sound == null)
        {
            Debug.Log("You haven't specified _sound through the inspector");
            this.enabled = false; //disables this script cause there is no sound loaded to play
        }

        audio = gameObject.AddComponent<AudioSource>(); //adds an AudioSource to the game object this script is attached to
        audio.playOnAwake = false; // stops audio from being played once game is loaded
        audio.clip = _sound;
        audio.Stop();
    }

    void Update()
    {
        if (Input.GetButton("Fire1")) //"fire1" listens for the left mouse click to play audio file
        {
            audio.Play();
            audio.volume = 0.2f;
        }
    }
}