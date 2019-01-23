using UnityEngine;

public class ParticleControl : MonoBehaviour
{

    public float maxEmission = 50.0f;
    public float minEmmision = 5.0f;
    public float speedToEmmision = 10.0f;

    private ParticleSystem ps;
    private JetPack jetPack;
    private float emissionAmount = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        jetPack = GameObject.FindObjectOfType<JetPack>();

        var emission = ps.emission;
        emission.rateOverTime = minEmmision;


    }

    // Update is called once per frame
    void Update()
    {
        var emission = ps.emission;

        if (Input.GetButton("Jump"))
        {
            emissionAmount += Time.deltaTime * speedToEmmision;
        }
        else
        {
            emissionAmount -= Time.deltaTime * speedToEmmision;
        }
        emissionAmount = Mathf.Clamp(emissionAmount, minEmmision, maxEmission);

        emission.rateOverTime = (jetPack.CurrentFuel > 0) ? emissionAmount : 0.0f;
    }
}
