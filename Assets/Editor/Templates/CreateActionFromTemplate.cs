using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Unearthed
{
    public static class CreateActionFromTemplate
    {
        [MenuItem("Assets/Create/AI/New Action", priority = 40)]
        public static void CreateActionItem()
        {
            string templatePath = "Assets/Editor/Templates/Action_Template.cs.txt";
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, "NewScript.cs");
            AssetDatabase.Refresh();
        }
    }
}
