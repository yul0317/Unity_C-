using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�Ѿ� �߻�� ������ ����� Ŭ���� ������ ����ü
[System.Serializable]
public struct PlayerSfx
{
    public AudioClip[] fire;
    public AudioClip[] reload;
}

public class FireCtrl : MonoBehaviour
{
    //���� Ÿ��
    public enum WeaponType
    {
        RIFLE=0,
        SHOTGUN
    }
    //���ΰ��� ���� ��� �ִ� ���⸦ ������ ����
    public WeaponType currWeapon = WeaponType.RIFLE;

    //�Ѿ� ������
    public GameObject bullet;
    //ź�� ���� ��ƼŬ
    public ParticleSystem cartridge;
    //�Ѿ� �߻� ��ǥ
    public Transform firePos;
    //�ѱ� ȭ�� ��ƼŬ
    private ParticleSystem muzzleFlash;

    //AudioSource ������Ʈ�� �����Һ���
    private AudioSource _audio;
    //����� Ŭ���� ������ ����
    public PlayerSfx playerSfx;

    // Start is called before the first frame update
    void Start()
    {
        //FirePos ������ �ִ� ������Ʈ ����
        muzzleFlash = firePos.GetComponentInChildren<ParticleSystem>();
        //AudioSource ������Ʈ ����
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //���콺 ���� ��ư�� Ŭ������ �� Fire �Լ� ȣ��
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }
    void Fire()
    {
        //Bullet �������� �������� ����
        Instantiate(bullet, firePos.position, firePos.rotation);
        //��ƼŬ ����
        cartridge.Play();
        //�ѱ�ȭ����ƼŬ ����
        muzzleFlash.Play();
        //���� �߻�
        FireSfx();
    }
    void FireSfx()
    {
        //���� ��� �ִ� ������ ����� Ŭ���� ������
        var _sfx = playerSfx.fire[(int)currWeapon];
        //���� �߻�
        _audio.PlayOneShot(_sfx, 1.0f);
    }
}
