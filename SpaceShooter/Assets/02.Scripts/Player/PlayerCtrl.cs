using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//클래스는 System.Serializable이라는 어트리뷰트(Attribute)를 명시해야
//인스펙터 뷰에 노출됨 노출을 해야 애니메이션이랑 연결을 함. 
//연결하고 나면은 다시 숨겨줌. 근데 숨겨주는거  anim변수를 숨겨줌. ->HideInInspector
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

    //접근해야 하는 컴포넌트는 반드시 변수에 할당한 후 사용
    [SerializeField] private Transform tr;  //serial~~ 적으면 private라도 유니티창에 표시됨
    //이동속도 변수(public 으로 선언되어 Inspector에 노출됨)
    public float moveSpeed = 10.0f;
    //회전속도 변수
    public float rotSpeed = 80.0f;
    //인스펙터 뷰에 표시할 애니메이션 클래스 변수
    public PlayerAnim playerAnim;
    //Animation 컴포넌트를 저장하기 위한 변수
    [HideInInspector]
    public Animation anim;

    void OnEnable()
    {
        GameManager.OnItemChange += UpdateSetup;
    }
    void UpdateSetup()
    {
        moveSpeed = GameManager.instance.gameData.speed;
    }
    // Start is called before the first frame update
    void Start()
    {
        //스크립트가 실행된 후 처음 수행되는 Start 함수에서 Transform 컴포넌트를 할당
        tr = GetComponent<Transform>();
        //Animation 컴포넌트를 변수에 할당
        anim = GetComponent<Animation>();
        //Animation 컴포넌트의 애니메이션 클립을 지정하고 실행
        anim.clip = playerAnim.idle;
        anim.Play();

        //불러온 데이터 값을 moveSpeed에 적용
        moveSpeed = GameManager.instance.gameData.speed;
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
        //전후좌우 이동 방향 벡터 계산
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        //Translate(이동 방향 * 속도 * 변위값(전진,전후진변수)=v(여기서는) * TimedeltaTime, 기준좌표)
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);
        // v라는 변위값 사라짐. 에 벡터라는 자체가 방향성을 가지고 있기에 사라짐.
        // Vector 의 normalized 속성은 정규화 만들어줌. 1.. 인가로 만들어줌.
        //deltaTime라는걸 곱해주는 이유는 Time.deltaTime가 이전 프레임 부터 현재 프레임까지 걸린 시간을 의미하고
        // 저걸 곱해야 프레임레이트(Frame Rate)에 상관없이 지정한 속도로 이동함.
        //곱해야 매 초마다 지정한 수치만큼 이동함 안곱하면 매 프레임마다 이동해버리기때문에 다른 프레임레이트에서는 차이가 남

        //Vector3.up 축을 기준으로 rotSpeed만큼의 속도로 회전
        // 회전할 기준좌표축  * Time.deltaTime * 회전속도 * 변위입력값
        tr.Rotate(Vector3.up * rotSpeed * Time.deltaTime * r);

        //키보드 입력값을 기준으로 동작할 애니메이션 수행
        if (v >= 0.1f)
        {
            anim.CrossFade(playerAnim.runF.name, 0.3f);//전진 애니메이션
            //애니메이션 실행하는 CrossFade 함수
            // 매개변수 1 : 변경할 애니메이션 클립의 명칭
            // 매개변수 2 : 다른 애니메이션 클립으로 페이드아웃되는 시간을 의미
        }
        else if (v <= -0.1f)
        {
            anim.CrossFade(playerAnim.runB.name, 0.3f);// 후진 애니메이션
        }
        else if (h >= 0.1f)
        {
            anim.CrossFade(playerAnim.runR.name, 0.3f);// 오른쪽 이동 애니메이션
        }
        else if (h <= -0.1f)
        {
            anim.CrossFade(playerAnim.runL.name, 0.3f);// 왼쪽 이동 애니메이션
        }
        else
        {
            anim.CrossFade(playerAnim.idle.name, 0.3f);// 정지 시 Idle 애니메이션
        }
    }
}
