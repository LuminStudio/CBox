using UnityEngine;

public class ParticleCurve : MonoBehaviour
{
    public Transform startObject;
    public Transform endObject;
    public int particleCount = 100;
    public float curveStrength = 1.0f;
    public float updateInterval = 0.1f;

    private ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particles;
    private float timer;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[particleCount];
        particleSystem.maxParticles = particleCount;
        particleSystem.Emit(particleCount);
        particleSystem.GetParticles(particles);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= updateInterval)
        {
            timer = 0f;
            UpdateParticles();
        }
    }

    void UpdateParticles()
    {
        Vector3 startPos = startObject.position;
        Vector3 endPos = endObject.position;
        Vector3 midPoint = (startPos + endPos) / 2 + Vector3.up * curveStrength;

        for (int i = 0; i < particleCount; i++)
        {
            float t = i / (float)particleCount;
            Vector3 curvePos = CalculateBezierPoint(t, startPos, midPoint, endPos);
            particles[i].position = curvePos;
        }

        particleSystem.SetParticles(particles, particles.Length);
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;

        return p;
    }
}