using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    //�Ѿ� ������
    public GameObject bullet;
    //ź�� ���� ��ƼŬ
    public ParticleSystem cartridge;
    //�Ѿ� �߻� ��ǥ
    public Transform firePos;
    //�ѱ� ȭ�� ��ƼŬ
    private ParticleSystem muzzleFlash;
    // Start is called before the first frame update
    void Start()
    {
        //FirePos ������ �ִ� ������Ʈ ����
        muzzleFlash = firePos.GetComponentInChildren<ParticleSystem>();
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
    }
}
