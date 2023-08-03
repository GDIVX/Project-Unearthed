using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class CreateScriptTemplates 
{
    [MenuItem("Assets/AI/Actions")]
    public static void CreateActionItem()
    {
        string templatePath = "Assets/Editor/Templates/Action_Template.cs.txt";
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, "NewScript.cs");
    }
}
