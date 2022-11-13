using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyException : System.Exception
{
    public MyException(){}
    public MyException(string message) : base(message){}
}

public class DiskFactory : MonoBehaviour
{
    List<GameObject> used;
    List<GameObject> free;
    System.Random rand;

    void Start()
    {
        used = new List<GameObject>();
        free = new List<GameObject>();
        rand = new System.Random();
    }

    void Update()
    {
    }

    public string getDiskName(int seed){
        if(seed == 1)
            return "Prefabs/Bomb1a";
        if(seed == 2)
            return "Prefabs/Bomb1b";
        if(seed == 3)
            return "Prefabs/Bomb1c";
        if(seed == 4)
            return "Prefabs/Disk1a";    
        if(seed == 5)
            return "Prefabs/Disk1b";    
        if(seed == 6)
            return "Prefabs/Disk1c";
        if(seed == 7)
            return "Prefabs/Disk2a";
        if(seed == 8)
            return "Prefabs/Disk2b";
        if(seed == 9)
            return "Prefabs/Bomb5a";
        if(seed == 10)
            return "Prefabs/Disk5a";
        if(seed == 11)
            return "Prefabs/Bomb5b";
        if(seed == 12)
            return "Prefabs/Disk5b";
        if(seed == 13)
            return "Prefabs/Bomb9";
        if(seed == 14)
            return "Prefabs/Disk9";               
        return "Error";
    }

    public GameObject createDisk(int round){
        GameObject disk;
        int right=7;
        if(round == 1)
            right = 7;
        else if(round == 2)
            right = 10;
        else
            right = 14;
            
        if(free.Count != 0){
            disk = free[0];
            free.Remove(disk);
        }
        else{
            string getName = getDiskName(rand.Next(1,right+1));
            disk = GameObject.Instantiate(Resources.Load(getName, typeof(GameObject))) as GameObject;
        }
        int roundX = round*10;
        int roundY = round*6;
        int roundZ = 100+round*20;
        disk.transform.position = new Vector3(rand.Next(-roundX,roundX+1), rand.Next(-roundY,roundY+1), rand.Next(80,roundZ));

        int scale = disk.GetComponent<Test>().diskItem.attributes.size;
        disk.transform.localScale = new Vector3(scale, scale, scale);
        
        used.Add(disk);
        disk.SetActive(true);
        Debug.Log("Generate disk success");
        return disk; 
    }

    public void freeDisk(GameObject disk){
        disk.SetActive(false);
        if (!used.Contains(disk)) {
            throw new MyException("Try to remove a item from a list which doesn't contain it.");
        }
        Debug.Log("Free disk success");
        used.Remove(disk);
        free.Add(disk);
    }
}
