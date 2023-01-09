using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UserAction{
    void Hit(Vector3 position);
    void restart();
}