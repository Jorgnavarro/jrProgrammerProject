using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
  public static MainManager Instance { get; private set; }
  //Almacenamos la variable que contendr� el color que elija el usuario en el menu principal
  public Color TeamColor;
  

    //first version
    /*
    private void Awake()
    {
          Instance = this;
          DontDestroyOnLoad(gameObject);
    }
    */

    //Second version

    //Esto se hace para evitar que, cuando nos movemos de la escena 0 a la escena 1, al regresarnos a la escena 0 desde la escena 1, no se
    //Traiga los elementos y no hayan duplicados con el caso del gameObject MainManager, b�sicamente si la Instancia es diferente de null
    //Se destruye ese gameObject, porque la escena 0 ya lo contiene
    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadColor();
    }

    //Esta clase contiene los datos espec�ficos que necesitamos guardar

    [System.Serializable]
    class SaveData
    {
        public Color TeamColor;
    }

    public void SaveColor()
    {
        //Instancio la clase creada con el encabezado [System.Serializable]
        SaveData data = new SaveData();
        //Creo su clave y valor
        data.TeamColor = TeamColor;
        //La transformo a JSON con JsonUtility.ToJson(data)
        string json = JsonUtility.ToJson(data);
        //Uso el m�todo File.WriteAllText(); para escribir una cadena en un archivo
        //Usa 2 par�metros el primero es la ruta de acceso al archivo. Proporcionar� una carpeta donde puede guardar datos que sobrevivir�n entre la 
        //reinstalaci�n o actualizaci�n de la aplicaci�n. El segundo par�metro es el texto que desea escribir en ese archivo, en este caso nuestro JSON.
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            TeamColor = data.TeamColor;
        }
    }


}
