using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ŭ������ System.Serializable�̶�� ��Ʈ����Ʈ(Attribute)�� ����ؾ�
//�ν����� �信 ����� ������ �ؾ� �ִϸ��̼��̶� ������ ��. 
//�����ϰ� ������ �ٽ� ������. �ٵ� �����ִ°�  anim������ ������. ->HideInInspector
[System.Serializable]
public class PlayerAnim
{
    public AnimationClip idle;
    public AnimationClip runF;
    public AnimationClip runB;
    public AnimationClip runL;
    public AnimationClip runR;
}
public class PlayerCtrl : MonoBehaviour
{
    private float h = 0.0f;
    private float v = 0.0f;
    private float r = 0.0f;

    //�����ؾ� �ϴ� ������Ʈ�� �ݵ�� ������ �Ҵ��� �� ���
    [SerializeField] private Transform tr;  //serial~~ ������ private�� ����Ƽâ�� ǥ�õ�
    //�̵��ӵ� ����(public ���� ����Ǿ� Inspector�� �����)
    public float moveSpeed = 10.0f;
    //ȸ���ӵ� ����
    public float rotSpeed = 80.0f;
    //�ν����� �信 ǥ���� �ִϸ��̼� Ŭ���� ����
    public PlayerAnim playerAnim;
    //Animation ������Ʈ�� �����ϱ� ���� ����
    [HideInInspector]
    public Animation anim;


    // Start is called before the first frame update
    void Start()
    {
        //��ũ��Ʈ�� ����� �� ó�� ����Ǵ� Start �Լ����� Transform ������Ʈ�� �Ҵ�
        tr = GetComponent<Transform>();
        //Animation ������Ʈ�� ������ �Ҵ�
        anim = GetComponent<Animation>();
        //Animation ������Ʈ�� �ִϸ��̼� Ŭ���� �����ϰ� ����
        anim.clip = playerAnim.idle;
        anim.Play();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        r = Input.GetAxis("Mouse X");

        Debug.Log("h=" + h.ToString());
        Debug.Log("v=" + v.ToString());
        Debug.Log("r=" + r.ToString());
        //�����¿� �̵� ���� ���� ���
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        //Translate(�̵� ���� * �ӵ� * ������(����,����������)=v(���⼭��) * TimedeltaTime, ������ǥ)
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);
        // v��� ������ �����. �� ���Ͷ�� ��ü�� ���⼺�� ������ �ֱ⿡ �����.
        // Vector �� normalized �Ӽ��� ����ȭ �������. 1.. �ΰ��� �������.
        //deltaTime��°� �����ִ� ������ Time.deltaTime�� ���� ������ ���� ���� �����ӱ��� �ɸ� �ð��� �ǹ��ϰ�
        // ���� ���ؾ� �����ӷ���Ʈ(Frame Rate)�� ������� ������ �ӵ��� �̵���.
        //���ؾ� �� �ʸ��� ������ ��ġ��ŭ �̵��� �Ȱ��ϸ� �� �����Ӹ��� �̵��ع����⶧���� �ٸ� �����ӷ���Ʈ������ ���̰� ��

        //Vector3.up ���� �������� rotSpeed��ŭ�� �ӵ��� ȸ��
        // ȸ���� ������ǥ��  * Time.deltaTime * ȸ���ӵ� * �����Է°�
        tr.Rotate(Vector3.up * rotSpeed * Time.deltaTime * r);

        //Ű���� �Է°��� �������� ������ �ִϸ��̼� ����
        if (v >= 0.1f)
        {
            anim.CrossFade(playerAnim.runF.name, 0.3f);//���� �ִϸ��̼�
            //�ִϸ��̼� �����ϴ� CrossFade �Լ�
            // �Ű����� 1 : ������ �ִϸ��̼� Ŭ���� ��Ī
            // �Ű����� 2 : �ٸ� �ִϸ��̼� Ŭ������ ���̵�ƿ��Ǵ� �ð��� �ǹ�
        }
        else if (v <= -0.1f)
        {
            anim.CrossFade(playerAnim.runB.name, 0.3f);// ���� �ִϸ��̼�
        }
        else if (h >= 0.1f)
        {
            anim.CrossFade(playerAnim.runR.name, 0.3f);// ������ �̵� �ִϸ��̼�
        }
        else if (h <= -0.1f)
        {
            anim.CrossFade(playerAnim.runL.name, 0.3f);// ���� �̵� �ִϸ��̼�
        }
        else
        {
            anim.CrossFade(playerAnim.idle.name, 0.3f);// ���� �� Idle �ִϸ��̼�
        }
    }
}
