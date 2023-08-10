using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Assets.Scripts.AI;
using System.IO;

namespace Unearthed
{
    public static class CreateNeedFromTemplate
    {
        [MenuItem("Assets/Create/AI/New Need", priority = 40)]
        public static void CreateNeedItem()
        {
            string scriptName = "NewScript.cs";
            string templatePath = "Assets/Editor/Templates/Need_Template.cs.txt";
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, scriptName);
            AssetDatabase.Refresh();
        }
    }
}