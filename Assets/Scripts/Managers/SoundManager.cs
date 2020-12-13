﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance;
	public int numberOfSources;
	public float volumeMultiplayer = 1, musicVolume = 1;
	public AudioSource music;
	public List<AudioClip> musicTracks;
	public bool main;

	public List<managedSource> managedAudioSources = new List<managedSource>();
	List<AudioSource> audioSrcs = new List<AudioSource>();

	[System.Serializable]
	public class managedSource
	{
		public AudioSource AudioSrc;
		public float volumeLimit;
	}

	void Update()
	{
		if(!instance && main)
			instance = this;
	}

	void Awake()
	{
		for(int i = 0; i < numberOfSources; i++)
		{
			audioSrcs.Add(gameObject.AddComponent<AudioSource>());
		}
		if(main)
		{
			instance = this;
			if(music)
			{
				music.clip = musicTracks[Random.Range(0, musicTracks.Count - 1)];
				music.Play();
			}
		}
	}

	public void StopSound(AudioClip sound)
	{
		foreach(AudioSource a in audioSrcs)
		{
			if(a.clip == sound)
			{
				a.Stop();
			}
		}
	}

	public void ChangeMoveSound(bool PausePlay, float pitch)
	{
		if(PausePlay)//True for play/resume, false for pause
			managedAudioSources[1].AudioSrc.UnPause();
		else
			managedAudioSources[1].AudioSrc.Pause();
	}

	public void changeVolume(float newVol)
	{
		volumeMultiplayer = newVol;

		foreach(AudioSource a in audioSrcs)
		{
			a.volume = newVol;
		}
		for(int i = 1; i < managedAudioSources.Count; i++)
		{
			managedAudioSources[i].AudioSrc.volume = volumeMultiplayer * managedAudioSources[i].volumeLimit;
		}
	}

	public void playSound(AudioClip sound, float volume, float pitch)
	{
		int c = 0;
		while(c < audioSrcs.Count)
		{
			if(!audioSrcs[c].isPlaying)
			{
				audioSrcs[c].pitch = pitch;
				audioSrcs[c].clip = sound;
				audioSrcs[c].PlayOneShot(sound);
				audioSrcs[c].volume = volume * volumeMultiplayer;
				break;
			}
			if(audioSrcs[c].isPlaying && c == (audioSrcs.Count - 1))
			{
				audioSrcs.Add(gameObject.AddComponent<AudioSource>());
			}
			else
			{
				c++;
			}
		}
	}

	public void playSound(AudioClip sound)
	{
		int c = 0;
		while(c < audioSrcs.Count)
		{
			if(!audioSrcs[c].isPlaying)
			{
				audioSrcs[c].clip = sound;
				audioSrcs[c].pitch = 1;
				audioSrcs[c].PlayOneShot(sound);
				audioSrcs[c].volume = 1 * volumeMultiplayer;
				break;
			}
			if(audioSrcs[c].isPlaying && c == (audioSrcs.Count - 1))
			{
				audioSrcs.Add(gameObject.AddComponent<AudioSource>());
			}
			else
			{
				c++;
			}
		}
	}

	public bool IsSoundPlaying(AudioClip clip)
	{
		foreach(AudioSource a in audioSrcs)
		{
			if(a.clip == clip)
			{
				return true;
			}
		}
		return false;
	}

	public void PauseSound(AudioClip clip, bool pause)
	{
		foreach(AudioSource a in audioSrcs)
		{
			if(a.clip == clip)
			{
				if(pause)
				{
					a.Pause();
				}
				else
				{
					a.UnPause();
				}
			}
		}
	}

}