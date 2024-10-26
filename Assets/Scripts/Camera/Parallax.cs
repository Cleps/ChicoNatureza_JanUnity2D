using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float parallaxEffect; // Ajuste este valor para controlar a intensidade do efeito de paralaxe
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private float textureUnitSizeX;

    void Start()
    {
        // Pega a Transform da Main Camera
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;

        // Calcular o tamanho da textura para fazer o loop quando a câmera se move
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        textureUnitSizeX = spriteRenderer.bounds.size.x;
    }

    void Update()
    {

        ParallaxEffect();

    }

    public void ParallaxEffect()
    {
        // Calcular o deslocamento com base no movimento da câmera
        float distance = (cameraTransform.position.x - lastCameraPosition.x) * parallaxEffect;
        transform.position += new Vector3(distance, 0, 0);

        lastCameraPosition = cameraTransform.position;

        // Manter a textura em loop
        // if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
        // {
        //     float offset = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
        //     transform.position = new Vector3(cameraTransform.position.x + offset, transform.position.y, transform.position.z);
        // }
    }
}
