using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Player
{
    Transform myTransform;
    Rigidbody myRigidbody;

    Vector3 PlayerMovement;

    float PlayerMaxHP;
    float PlayerCurHealth;
    public float PlayerCurHealth1
    {
        get { return PlayerCurHealth; }
        set { PlayerCurHealth = value; }
    }

    float PlayerMoveSpeed;
    float PlayerHPRegen;

    Slider healthSlider;
    Image damageImage;
    float flashSpeed = 5;
    Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    bool damaged = false;
    bool isDead = false;
    public bool IsDead1
    {
        get { return isDead; }
        set { isDead = value; }
    }

    Transform cameraTransform;
    private float cameraRotation;
    public float minimumY = -65;
    public float maximumY = 65;
    float rotationSpeed = 15;

    public List<Spell> Seplls;

    public Player(Transform mytransform, Rigidbody myrigidbody, List<Spell> p_Spells, float HP, float MoveSpeed, float HPRegen, Slider hpSlider, Image DmgImage)
    {
        Seplls = p_Spells;
        PlayerMaxHP = HP;
        PlayerCurHealth = HP;
        PlayerMoveSpeed = MoveSpeed;
        PlayerHPRegen = HPRegen;

        myTransform = mytransform;
        myRigidbody = myrigidbody;

        hpSlider.maxValue = PlayerMaxHP;
        hpSlider.value = PlayerMaxHP;
        healthSlider = hpSlider;

        damageImage = DmgImage;

        cameraTransform = Camera.main.transform;
    }

    public void Move(float h, float v)
    {
        PlayerMovement.Set(h, 0f, v);

        PlayerMovement = PlayerMovement.normalized * PlayerMoveSpeed * Time.deltaTime;
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

    public void TakeDamage(float Damage)
    {
        damaged = true;

        PlayerCurHealth1 -= Damage;
        healthSlider.value = PlayerCurHealth1;

        if (PlayerCurHealth1 <= 0 && !IsDead1)
        {
            Death();
        }
    }

    void Death()
    {
        IsDead1 = true;
    }
}
