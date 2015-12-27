using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Player : Character
{
    Transform myTransform;
    Rigidbody myRigidbody;

    Vector3 PlayerMovement;

    float flashSpeed = 5;
    Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    public static Image damageImage;
    bool damaged;
    public static bool isDead;

    Transform cameraTransform;
    private float cameraRotation;
    public float minimumY = -35;
    public float maximumY = 45;
    float rotationSpeed = 30;

    public static Slider playerHPSlider;

    public List<Spell> Seplls;

    public Player(List<Spell> p_Spells, float p_HP, float p_HPRegen, float p_MoveSpeed)
        : base(0, p_HP, p_HPRegen, p_MoveSpeed)
    {
        isDead = false;
        damaged = false;

        Seplls = p_Spells;

        myTransform = PlayerScript.myTransform;
        myRigidbody = PlayerScript.myRigidBody;

        hpSlider = playerHPSlider;
        hpSlider.maxValue = MaxHP;
        hpSlider.value = MaxHP;

        cameraTransform = myTransform.GetChild(0);//Camera.main.transform;
    }

    public void Move(float h, float v)
    {
        PlayerMovement.Set(h, 0f, v);

        PlayerMovement = PlayerMovement.normalized * MoveSpeed * Time.deltaTime;
        PlayerMovement = myTransform.TransformDirection(PlayerMovement);

        myRigidbody.MovePosition(myTransform.position + PlayerMovement);
    }

    public void Turning()
    {
        myTransform.Rotate(new Vector3(0, Input.GetAxisRaw("Mouse X") * rotationSpeed, 0) * Time.deltaTime);
        cameraRotation -= Input.GetAxisRaw("Mouse Y") * rotationSpeed * Time.deltaTime;
        cameraRotation = Mathf.Clamp(cameraRotation, minimumY, maximumY);
        cameraTransform.localEulerAngles = new Vector3(cameraRotation, cameraTransform.localEulerAngles.y, 0);
    }

    public override void Attac()
    {
        
    }

    public void DamagedEffect()
    {
        if (damaged)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    public void TakeDamageBase(float Damage)
    {
        damaged = true;
        CurHP -= Damage;
        hpSlider.value = CurHP;

        if (CurHP <= 0 && !isDead)
        {
            isDead = true;
        }
    }

    public override void TakeDamage(HZSpell SpellInfo)
    {
        TakeDamageBase(SpellInfo.SpellDamage1);
    }
}
