using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction
{
    void moveBow(Vector3 mousePos);
	void shootArrow (Vector3 mousePos);
	int GetScore();
	void Restart();
	string GetWind();
	void Begin();
	void Init();
	bool isArrowNull();
}
