using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StorageSystem : MonoBehaviour, IObservable
{
    public static StorageSystem Instance;
    public string SaveFilePath { get; private set; }
    public SettingsProfile SettingsProfile;

    private readonly List<IObserver> _actualObservers = new();

    void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        string persistentPath = Application.persistentDataPath;
        SaveFilePath = string.Format("{0}/save.json", persistentPath);

        ReadSaveFile();
    }

    public bool DoesFileExist()
    {
        return File.Exists(SaveFilePath);
    }

    public void ReadSaveFile()
    {
        SettingsProfile newSettingsProfile = new();

        if(DoesFileExist())
        {
            string fileContent = File.ReadAllText(SaveFilePath);
            SettingsProfile loadedSettingsProfile = JsonUtility.FromJson<SettingsProfile>(fileContent); 

            if(loadedSettingsProfile.ValidateData())
                newSettingsProfile = loadedSettingsProfile;
        }

        SettingsProfile = newSettingsProfile;
        Notify();
    }

    public void WriteSaveFile()
    {
        if(!SettingsProfile.ValidateData())
            throw new StorageException("Error Writing Storage Data");

        string fileContent = JsonUtility.ToJson(SettingsProfile, true);
        File.WriteAllText(SaveFilePath, fileContent);
        Notify();
    }


    // IObservable
    public void Attach(IObserver observer)
    {
        _actualObservers.Add(observer);
        Notify();
    }

    public void Detach(IObserver observer)
    {
        _actualObservers.Remove(observer);
    }

    public void Notify() {
        foreach (IObserver observer in _actualObservers)
        {
            observer.ObserverUpdate();
        }
    }
}