/* 
 *  Author : Missnish
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Types;


public class EnemyDesignerWindow : EditorWindow
{

    Texture2D headerSelectionTexture;
    Texture2D mageSelectionTexture;
    Texture2D warriorSelectionTexture;
    Texture2D rogueSelectionTexture;
    Texture2D mageIconTexture;
    Texture2D warriorIconTexture;
    Texture2D rogueIconTexture;

    Color headerSelectionColor = new Color(0f, 0f, 0f, 1f);

    GUISkin skin;

    Rect headerSelection;
    Rect mageSelection;
    Rect warriorSelection;
    Rect rogueSelection;
    Rect mageIconSeletion;
    Rect warriorIconSeletion;
    Rect rogueIconSeletion;

    float iconSize = 80;

    static MageData mageData;
    static WarriorData warriorData;
    static RogueData rogueData;

    public static MageData MageInfo { get { return mageData;} }
    public static WarriorData WarriorInfo { get { return warriorData;} }
    public static RogueData RogueInfo { get { return rogueData;} }



    [MenuItem("Tool/Enemy Designer")]
    static void OpenWindow()
    {
        EnemyDesignerWindow window = (EnemyDesignerWindow)GetWindow(typeof(EnemyDesignerWindow));
        window.minSize = new Vector2(600, 350);
        window.maxSize = new Vector2(600, 350);
        window.Show();
    }


    /// <summary>
    /// 与Start()或Awake()功能类似，初始化
    /// </summary>
    void OnEnable()
    {
        InitTextures();
        InitData();
        skin = Resources.Load<GUISkin>("StylesGUI/EnemyDesignerSkin");
    }

    /// <summary>
    /// 初始化2D纹理信息
    /// </summary>
    void InitTextures()
    {
        //-----顶部标题栏纹理-----
        headerSelectionTexture = new Texture2D(1, 1);
        headerSelectionTexture.SetPixel(0, 0, headerSelectionColor);
        //headerSelectionTexture = Resources.Load<Texture2D>("UI/Background_GB_rounded");
        headerSelectionTexture.Apply();

        //-----Mage纹理-----
        mageSelectionTexture = Resources.Load<Texture2D>("UI/Slot");
        mageIconTexture = Resources.Load<Texture2D>("UI/Elixir_1_Square_Transparent");

        //-----Warrior纹理-----
        warriorSelectionTexture = Resources.Load<Texture2D>("UI/Slot");
        warriorIconTexture = Resources.Load<Texture2D>("UI/Sword_Square_Transparent");

        //-----Rogue纹理-----
        rogueSelectionTexture = Resources.Load<Texture2D>("UI/Slot");
        rogueIconTexture = Resources.Load<Texture2D>("UI/Poleax_Square_Transparent");
    }

    public static void InitData()
    {
        mageData = (MageData)ScriptableObject.CreateInstance(typeof(MageData));
        warriorData = (WarriorData)ScriptableObject.CreateInstance(typeof(WarriorData));
        rogueData = (RogueData)ScriptableObject.CreateInstance(typeof(RogueData));
    }

    /// <summary>
    /// 与Update()功能类似，区别在于OnGUI()是在交互时调用，而非每帧皆调用
    /// </summary>
    void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
        DrawMage();
        DrawWarrior();
        DrawRogue();
    }

    /// <summary>
    /// 该函数在OnGUI()内部调用，在矩形绘制的基础上定义矩形参数以及纹理参数
    /// </summary>
    void DrawLayouts()
    {
        //-----顶部标题栏-----
        headerSelection.x = 0;
        headerSelection.y = 0;
        headerSelection.width = position.width;
        headerSelection.height = 50;
        //headerSelection.height = 50;
        GUI.DrawTexture(headerSelection, headerSelectionTexture);

        //-----Mage-----
        mageSelection.x = 0;
        mageSelection.y = 50;
        mageSelection.width = position.width / 3f;
        mageSelection.height = position.height - 50;
        GUI.DrawTexture(mageSelection, mageSelectionTexture);

        mageIconSeletion.x = (mageSelection.x + mageSelection.width - iconSize) * 0.5f;
        mageIconSeletion.y = mageSelection.y + 50;
        mageIconSeletion.width = iconSize;
        mageIconSeletion.height = iconSize;
        GUI.DrawTexture(mageIconSeletion, mageIconTexture);

        //-----Warrior-----
        warriorSelection.x = position.width / 3f;
        warriorSelection.y = 50;
        warriorSelection.width = position.width / 3f;
        warriorSelection.height = position.height - 50;
        GUI.DrawTexture(warriorSelection, warriorSelectionTexture);

        warriorIconSeletion.x = warriorSelection.x + (warriorSelection.width - iconSize) * 0.5f;
        warriorIconSeletion.y = warriorSelection.y + 50;
        warriorIconSeletion.width = iconSize;
        warriorIconSeletion.height = iconSize;
        GUI.DrawTexture(warriorIconSeletion, warriorIconTexture);

        //-----Rogue-----
        rogueSelection.x = position.width / 3f * 2f;
        rogueSelection.y = 50;
        rogueSelection.width = position.width / 3f;
        rogueSelection.height = position.height - 50;
        GUI.DrawTexture(rogueSelection, rogueSelectionTexture);

        rogueIconSeletion.x = rogueSelection.x + (rogueSelection.width - iconSize) * 0.5f;
        rogueIconSeletion.y = rogueSelection.y + 50;
        rogueIconSeletion.width = iconSize;
        rogueIconSeletion.height = iconSize;
        GUI.DrawTexture(rogueIconSeletion, rogueIconTexture);
    }

    /// <summary>
    /// 绘制顶部标题栏
    /// </summary>
    void DrawHeader()
    {
        GUILayout.BeginArea(headerSelection);

        GUILayout.Space(8);

        GUILayout.Label("Enemy Designer", skin.GetStyle("TopHeader"));

        GUILayout.EndArea();
    }

    /// <summary>
    /// 绘制Mage部分
    /// </summary>
    void DrawMage()
    {
        GUILayout.BeginArea(mageSelection);

        GUILayout.Space(iconSize + 55);

        GUILayout.Label("Mage", skin.GetStyle("ObjHeader"));

        GUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(20);
        GUILayout.Label("damage", skin.GetStyle("ObjLabel"), GUILayout.Width(60), GUILayout.Height(20));
        mageData.dmgType = (MageDmgType)EditorGUILayout.EnumPopup(mageData.dmgType, skin.GetStyle("Text"));
        GUILayout.Space(20);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(20);
        GUILayout.Label("weapon", skin.GetStyle("ObjLabel"), GUILayout.Width(60), GUILayout.Height(20));
        mageData.wpnType = (MageWpnType)EditorGUILayout.EnumPopup(mageData.wpnType, skin.GetStyle("Text"));
        GUILayout.Space(20);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(35);
        if (GUILayout.Button("Create!", skin.GetStyle("Button"),GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.MAGE);
        }
        GUILayout.Space(30);
        EditorGUILayout.EndHorizontal();


        GUILayout.EndArea();
    }

    /// <summary>
    /// 绘制Warrior部分
    /// </summary>
    void DrawWarrior()
    {
        GUILayout.BeginArea(warriorSelection);

        GUILayout.Space(iconSize + 55);

        GUILayout.Label("Warrior", skin.GetStyle("ObjHeader"));

        GUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(20);
        GUILayout.Label("class", skin.GetStyle("ObjLabel"), GUILayout.Width(60), GUILayout.Height(20));
        warriorData.classType = (WarriorClassType)EditorGUILayout.EnumPopup(warriorData.classType, skin.GetStyle("Text"));
        GUILayout.Space(20);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(20);
        GUILayout.Label("weapon", skin.GetStyle("ObjLabel"), GUILayout.Width(60), GUILayout.Height(20));
        warriorData.wpnType = (WarriorWpnType)EditorGUILayout.EnumPopup(warriorData.wpnType, skin.GetStyle("Text"));
        GUILayout.Space(20);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(35);
        if (GUILayout.Button("Create!", skin.GetStyle("Button"), GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.WARRIOR);
        }
        GUILayout.Space(30);
        EditorGUILayout.EndHorizontal();


        GUILayout.EndArea();
    }

    /// <summary>
    /// 绘制Rogue部分
    /// </summary>
    void DrawRogue()
    {
        GUILayout.BeginArea(rogueSelection);

        GUILayout.Space(iconSize + 55);

        GUILayout.Label("Rogue", skin.GetStyle("ObjHeader"));

        GUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(20);
        GUILayout.Label("strategy", skin.GetStyle("ObjLabel"), GUILayout.Width(60), GUILayout.Height(20));
        GUILayout.Space(5);
        rogueData.strategyType = (RogueStrategyType)EditorGUILayout.EnumPopup(rogueData.strategyType, skin.GetStyle("Text"));
        GUILayout.Space(20);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(20);
        GUILayout.Label("weapon", skin.GetStyle("ObjLabel"), GUILayout.Width(60), GUILayout.Height(20));
        GUILayout.Space(5);
        rogueData.wpnType = (RogueWpnType)EditorGUILayout.EnumPopup(rogueData.wpnType, skin.GetStyle("Text"));
        GUILayout.Space(20);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(35);
        if (GUILayout.Button("Create!", skin.GetStyle("Button"), GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.ROGUE);
        }
        GUILayout.Space(30);
        EditorGUILayout.EndHorizontal();

        GUILayout.EndArea();
    }

}


public class GeneralSettings : EditorWindow
{
    public enum SettingsType
    {
        MAGE,
        WARRIOR,
        ROGUE
    }

    static SettingsType dataSetting;
    static GeneralSettings window;
    GUISkin skin;
    Rect backgroundSelection;
    Texture2D backgroundTexture;

    public static void OpenWindow(SettingsType setting)
    {
        dataSetting = setting;
        window = (GeneralSettings)GetWindow(typeof(GeneralSettings));
        window.minSize = new Vector2(300, 250);
        window.Show();
    }

    void OnEnable()
    {
        InitTextures();
        skin = Resources.Load<GUISkin>("StylesGUI/EnemyDesignerSkin");
    }

    void InitTextures()
    {
        //-----背景图片纹理-----
        backgroundTexture = Resources.Load<Texture2D>("UI/Slot");
    }

    void OnGUI()
    {
        DrawBackground();
        switch(dataSetting)
        {
            case SettingsType.MAGE :
                DrawSettings((CharacterData)EnemyDesignerWindow.MageInfo);
                break;
            case SettingsType.WARRIOR :
                DrawSettings((CharacterData)EnemyDesignerWindow.WarriorInfo);
                break;
            case SettingsType.ROGUE :
                DrawSettings((CharacterData)EnemyDesignerWindow.RogueInfo);
                break;
        }
    }

    void DrawBackground()
    {
        backgroundSelection.x = 0;
        backgroundSelection.y = 0;
        backgroundSelection.width = position.width;
        backgroundSelection.height = position.height;
        GUI.DrawTexture(backgroundSelection, backgroundTexture);
    }

    void DrawSettings(CharacterData characterData)
    {
        GUILayout.Space(25);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(30);
        GUILayout.Label("Prefab", skin.GetStyle("ObjLabel"), GUILayout.Width(80), GUILayout.Height(20));
        characterData.prefab = (GameObject)EditorGUILayout.ObjectField(characterData.prefab, typeof(GameObject), false);
        GUILayout.Space(30);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(30);
        GUILayout.Label("Max Health", skin.GetStyle("ObjLabel"), GUILayout.Width(80), GUILayout.Height(20));
        characterData.maxHealth = EditorGUILayout.FloatField(characterData.maxHealth);
        GUILayout.Space(30);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(30);
        GUILayout.Label("Max Energy", skin.GetStyle("ObjLabel"), GUILayout.Width(80), GUILayout.Height(20));
        characterData.maxEnergy = EditorGUILayout.FloatField(characterData.maxEnergy);
        GUILayout.Space(30);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(30);
        GUILayout.Label("Power", skin.GetStyle("ObjLabel"), GUILayout.Width(80), GUILayout.Height(20));
        characterData.power = EditorGUILayout.Slider(characterData.power, 0, 100);
        GUILayout.Space(30);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(30);
        GUILayout.Label("Crit Chance", skin.GetStyle("ObjLabel"), GUILayout.Width(80), GUILayout.Height(20));
        characterData.critChance = EditorGUILayout.Slider(characterData.critChance, 0, characterData.power);
        GUILayout.Space(30);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(30);
        GUILayout.Label("Name", skin.GetStyle("ObjLabel"), GUILayout.Width(80), GUILayout.Height(20));
        characterData.objName = EditorGUILayout.TextField(characterData.objName);
        GUILayout.Space(30);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(15);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(30);
        if(characterData.prefab == null)
        {
            EditorGUILayout.HelpBox("请拖入[Prefab]以创建Enemy对象!", MessageType.Warning);
        }
        else if(characterData.objName == null || characterData.objName.Length < 1)
        {
            EditorGUILayout.HelpBox("请输入[Name]以创建Enemy对象!", MessageType.Warning);
        }
        else if (GUILayout.Button("Finish and Save", skin.GetStyle("Button"), GUILayout.Height(40)))
        {
            SaveCharacterData();
            window.Close();
        }
        GUILayout.Space(30);
        EditorGUILayout.EndHorizontal();

    }

    void SaveCharacterData()
    {
        string prefabPath;                                          //Prefab初始地址
        string newPrefabPath = "Assets/Prefabs/Character/";         //Prefab保存地址
        string dataPath = "Assets/Resources/CharacterData/Data/";

        switch(dataSetting)
        {
            case SettingsType.MAGE :
            {
                MageData mageInfo = (MageData)Instantiate(EnemyDesignerWindow.MageInfo);                //实例化Mage对象
                dataPath += "Mage/" + mageInfo.objName + ".asset";                                      //创建[asset]文件
                AssetDatabase.CreateAsset(mageInfo, dataPath);
                newPrefabPath += "Mage/" + mageInfo.objName + ".prefab";
                prefabPath = AssetDatabase.GetAssetPath(mageInfo.prefab);                               //得到原来的[Prefab]路径
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);                                     //将指定的[Prefab]存入新路径
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject magePrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                if(!magePrefab.GetComponent<Mage>())
                {
                    magePrefab.AddComponent(typeof(Mage));
                }
                magePrefab.GetComponent<Mage>().mage = EnemyDesignerWindow.MageInfo;
            }
                break;
            case SettingsType.WARRIOR :
            {
                WarriorData warriorInfo = (WarriorData)Instantiate(EnemyDesignerWindow.WarriorInfo);        //实例化Warrior对象

                dataPath += "Warrior/" + warriorInfo.objName + ".asset";                                    //创建[asset]文件             
                AssetDatabase.CreateAsset(warriorInfo, dataPath);
                newPrefabPath += "Warrior/" + warriorInfo.objName + ".prefab";
                prefabPath = AssetDatabase.GetAssetPath(warriorInfo.prefab);                                //得到原来的[Prefab]路径
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);                                         //将指定的[Prefab]存入新路径
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject warriorInfoPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                if(!warriorInfoPrefab.GetComponent<Warrior>())
                {
                    warriorInfoPrefab.AddComponent(typeof(Warrior));
                }
                warriorInfoPrefab.GetComponent<Warrior>().warrior = EnemyDesignerWindow.WarriorInfo;
            }  
                break;
            case SettingsType.ROGUE :
            {
                RogueData rogueInfo = (RogueData)Instantiate(EnemyDesignerWindow.RogueInfo);                //实例化Rogue对象

                dataPath += "Rogue/" + rogueInfo.objName + ".asset";                                        //创建[asset]文件
                AssetDatabase.CreateAsset(rogueInfo, dataPath);
                newPrefabPath += "Rogue/" + rogueInfo.objName + ".prefab";
                prefabPath = AssetDatabase.GetAssetPath(rogueInfo.prefab);                                  //得到原来的[Prefab]路径
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);                                         //将指定的[Prefab]存入新路径
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject rogueInfoPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                if(!rogueInfoPrefab.GetComponent<Rogue>())
                {
                    rogueInfoPrefab.AddComponent(typeof(Rogue));
                }
                rogueInfoPrefab.GetComponent<Rogue>().rogue = EnemyDesignerWindow.RogueInfo;
            }   
                break;
        }
    }
}