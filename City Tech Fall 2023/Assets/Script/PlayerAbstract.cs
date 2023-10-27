using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPlayer : MonoBehaviour
{
    public float damage;
    public void Damage(PlayerMechanics dmgtaken) => dmgtaken.Damage(damage);
}
