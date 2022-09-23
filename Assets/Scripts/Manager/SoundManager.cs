using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] //인스펙터창에서 바로 관리
public class Sound
{
    public string soundName;
    public AudioClip clip;
}

public class SoundManager : MonoSingleton<SoundManager>
{
    public static SoundManager instance; //static 공유변수. 어디서든 쉽체 참조 변경이 가능


    [Header("사운드 등록")]
    [SerializeField] Sound[] bgmSounds;
    [SerializeField] Sound[] sfxSounds;

    [Header("브금 플레이어")]
    [SerializeField] AudioSource bgmPlayer;

    [Header("효과음 플레이어")]
    [SerializeField] AudioSource[] sfxPlayer; //  [] <- 표시는 배열...동시에 여러개가 날수있게..


    public Slider bgmVolume;
    public Slider sfxVolume;

    public bool playSFX = true;

    void Awake()
    {
        instance = this;
    }

    public void PlaySFX(string _soundName)
    {
        //Debug.Log("PlaySFX " + _soundName);
        
        if(!playSFX)
            return;
        
        for (int i = 0; i < sfxSounds.Length; i++) // 사운드에 등록된 갯수만큼 하는 반복문
        {
            if (_soundName == sfxSounds[i].soundName) //조건문을 비교해서 파라미터로 넘어온 사운드 네임과 동일한게 있음 재생
            {
                for (int x = 0; x < sfxPlayer.Length; x++) // 재생중이지않은 플래이어를 찾아야 하기에 반복문을 돌려야함 // 위쪽 반복문에서 i 를 썻기에 x 를씀
                {
                    if (!sfxPlayer[x].isPlaying) // x 번째의 mp3 플레이어가 재생중이지 않다면 만족하는 조건문
                    {
                        sfxPlayer[x].clip = sfxSounds[i].clip; //재생중이지 않은 x번째의 mp3플레이어에 전 조건문에서 찾아낸 i 번째 mp3를 넣어중
                        //sfxPlayer[x].volume = sfxVolume.value; 
                        sfxPlayer[x].Play(); // mp3 재생
                        return; //반복문 끝내기
                    }
                }
                Debug.Log("모든 효과음 플레이어가 사용중");
                return;
            }
        }
        Debug.Log("등록된 효과음 없음");

    }



    public void PlayerRandomBGM() //bgmSounds에 등록한 음악은 2개(배열의 크기는 2. 배열의 index는 0 과 1)
    {
        int random = Random.Range(0, bgmSounds.Length); //정수는 최대값을 리턴 하지 않아 0,2 해야함..
        bgmPlayer.clip = bgmSounds[random].clip;  //clip 이 mp3 플레이어에 들어감
        bgmPlayer.Play(); //클립재생
    }

    public void PlayerStopBGM()
    {
        bgmPlayer.Stop();
    }
    

    public void bgmVolumeValueChanged()
    {
        bgmPlayer.volume = bgmVolume.value; //슬라이더로 bgm볼륨조절
    }


}
