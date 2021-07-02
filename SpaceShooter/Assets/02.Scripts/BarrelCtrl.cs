using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    //���� ȿ�� �������� ������ ����
    public GameObject expEffect;
    //��׷��� �巳���� �޽��� ������ �迭
    public Mesh[] meshes;

    //�Ѿ��� ���� Ƚ��
    private int hitCount = 0;
    //Rigibody ������Ʈ�� ������ ����
    private Rigidbody rb;
    //MeshFilter ������Ʈ�� ������ ����
    private MeshFilter meshFilter;

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody ������Ʈ�� ������ ����
        rb = GetComponent<Rigidbody>();
        //MeshFilter ������Ʈ�� ������ ����
        meshFilter = GetComponent<MeshFilter>();
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
        rb.mass = 1.0f;
        //���� �ڱ�ġ�� ���� ����
        rb.AddForce(Vector3.up * 1000.0f);

        //������ �߻�
        int idx = Random.Range(0, meshes.Length);
        //��׷��� �޽��� ����
        meshFilter.sharedMesh = meshes[idx];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
