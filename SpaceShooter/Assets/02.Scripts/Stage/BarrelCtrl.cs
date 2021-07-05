using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    //���� ȿ�� �������� ������ ����
    public GameObject expEffect;
    //��׷��� �巳���� �޽��� ������ �迭
    public Mesh[] meshes;
    //�巳���� �ؽ�ó�� ������ �迭
    public Texture[] textures;

    //�Ѿ��� ���� Ƚ��
    private int hitCount = 0;

    //Rigibody ������Ʈ�� ������ ����
    //private Rigidbody rb;

    //MeshFilter ������Ʈ�� ������ ����
    private MeshFilter meshFilter;
    //meshFilterCollider ������Ʈ�� ������ ����
    private MeshCollider meshFilterCollider;
    //MeshRenderer ������Ʈ�� ������ ����
    private MeshRenderer _renderer;
    //AudioSource ������Ʈ�� ������ ����
    private AudioSource _audio;

    //���� �ݰ�
    public float expRadius = 10.0f;
    //������ ����� Ŭ��
    public AudioClip expSfx;

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody ������Ʈ�� ������ ����
        //rb = GetComponent<Rigidbody>();
        //MeshFilter ������Ʈ�� ������ ����
        meshFilter = GetComponent<MeshFilter>();
        //meshFilterCollider ������Ʈ�� ������ ����
        meshFilterCollider = GetComponent<MeshCollider>();
        //MeshRenderer ������Ʈ�� ������ ����
        _renderer = GetComponent<MeshRenderer>();
        //������ �߻����� �ұ�Ģ���� �ؽ�ó�� ����
        _renderer.material.mainTexture = textures[Random.Range(0, textures.Length)];
        //AudioSource ������Ʈ�� ������ ����
        _audio = GetComponent<AudioSource>();

    }
    //�浹�� �߻����� �� �� �� ȣ��Ǵ� �ݹ� �Լ�
    void OnCollisionEnter(Collision coll)
    {
        //�浹�� ���ӿ�����Ʈ�� �±׸� ��
        if (coll.collider.CompareTag("BULLET"))
        {
            //�Ѿ��� �浹 Ƚ���� ������Ű�� 3�� �̻� �¾Ҵ��� Ȯ��
            if(++hitCount == 5)
            {
                ExpBarrel();
            }
        }
    }
    //���� ȿ���� ó���� �Լ�
    void ExpBarrel()
    {
        //���� ȿ�� �������� �������� ����
        GameObject effect = Instantiate(expEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2.0f);
        //Rigidbody ������Ʈ�� mass�� 1.0���� ������ ���Ը� ������ ��
        //rb.mass = 1.0f;
        //���� �ڱ�ġ�� ���� ����
        //rb.AddForce(Vector3.up * 1000.0f);

        //���߷� ����
        IndirectDamage(transform.position);

        //������ �߻�
        int idx = Random.Range(0, meshes.Length);
        //��׷��� �޽��� ����
        meshFilter.sharedMesh = meshes[idx];
        //Collider ������Ʈ ����
        meshFilterCollider.sharedMesh = meshes[idx];

        //������ ����
        _audio.PlayOneShot(expSfx, 1.0f);
    }

    //���߷��� �ֺ��� �����ϴ� �Լ�
    void IndirectDamage(Vector3 pos)
    {
        //�ֺ��� �ִ� �巳���� ��� ����
        Collider[] colls = Physics.OverlapSphere(pos, expRadius, 1 << 7);
        foreach(var coll in colls)
        {
            //���� ������ ���Ե� �巳���� Rigidbody ������Ʈ ����
            var _rb = coll.GetComponent<Rigidbody>();
            //�巳���� ���Ը� ������ ��
            _rb.mass = 1.0f;
            //���߷��� ����
            _rb.AddExplosionForce(1200.0f, pos, expRadius, 1000.0f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
