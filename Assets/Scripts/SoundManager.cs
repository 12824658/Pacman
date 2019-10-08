using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create Empty in the Hierarchy, name it SoundManager
// Add Component -> Audio -> Audio Source
// Drag sounds into Sounds Folder
// Create SoundManager.cs
// Drag sounds from folder to SoundManager Inspector
// Drag EatingDots to AudioClip in Inspector

public class SoundManager : MonoBehaviour {

	// Holds the single instance of the SoundManager that 
	// you can access from any script
	public static SoundManager Instance = null;

	// Sound clips for Pac-Man
	public AudioClip eatingDots;
	public AudioClip eatingGhost;
	public AudioClip ghostMove;
	public AudioClip pacmanDies;
	public AudioClip powerupEating;

	// Refers to the audio source used for Pac-Man
	// eating dots
	private AudioSource pacmanAudioSource;

	// Plays Ghost moving loop sound
	private AudioSource ghostAudioSource;

	// Used for playing one shot audio clips
	private AudioSource oneShotAudioSource;

	// Use this for initialization
	void Start() {

		// This is a singleton that makes sure you only
		// ever have one Sound Manager
		// If there is any other Sound Manager created destroy it
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}

		// Use multiple AudioSources
		AudioSource[] audioSources = GetComponents<AudioSource> ();

		// Used for Pac-Man eating dots
		pacmanAudioSource = audioSources[0];

		// Used to play Ghost moving sound
		ghostAudioSource = audioSources[1];

		// Used for one shots
		oneShotAudioSource = audioSources[2];

		// Start Pac-Man eating sound
		PlayClipOnLoop (pacmanAudioSource, eatingDots);

	}

	// Other GameObjects can call this to play sounds
	public void PlayOneShot(AudioClip clip) {
		oneShotAudioSource.PlayOneShot(clip);
	}

	// Used to start playing the Pac-Man eating
	public void PlayClipOnLoop(AudioSource aS, AudioClip clip){
		// Make sure we have an AudioSource and Clip
		if (aS != null && clip != null) {

			// Set the sound to loop
			aS.loop = true;

			// Set the volume of the sound
			aS.volume = 0.2f;

			// Set the clip to play
			aS.clip = clip;

			// Play the sound
			aS.Play();
		}
	}

	// Pauses Pac-Man eating sounds
	public void PausePacman(){
		if (pacmanAudioSource != null && pacmanAudioSource.isPlaying) {
			pacmanAudioSource.Stop ();
		}
	}

	// Restarts ac-Man eating sound
	public void UnPausePacman(){
		if (pacmanAudioSource != null && !pacmanAudioSource.isPlaying)
			pacmanAudioSource.Play ();
	}

	// Pauses Pac-Man eating sounds
	public void PauseGhost(){
		if (ghostAudioSource != null && ghostAudioSource.isPlaying) {
			ghostAudioSource.Stop ();
		}
	}

	// Restarts ac-Man eating sound
	public void UnPauseGhost(){
		if (ghostAudioSource != null && !ghostAudioSource.isPlaying)
			ghostAudioSource.Play ();
	}

}
