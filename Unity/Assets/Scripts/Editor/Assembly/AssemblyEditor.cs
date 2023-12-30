using System.IO;
using UnityEditor;
using UnityEngine;

namespace ET
{
    public class AssemblyEditor
    {
        [InitializeOnLoadMethod]
        static void Initialize()
        {
             //删掉Library中Unity编译的dll，不然在编辑器下Assembly.Load多个dll时，dll会与Library中的dll引用错乱
            EditorApplication.playModeStateChanged += change =>
            {
                GlobalConfig globalConfig = Resources.Load<GlobalConfig>("GlobalConfig");
                if (change == PlayModeStateChange.ExitingEditMode)
                {
                    if (globalConfig.EnableDll)
                    {
                        foreach (var dll in AssemblyTool.dllNames)
                        {
                            string dllFile = $"{Application.dataPath}/../Library/ScriptAssemblies/{dll}.dll";
                            if (File.Exists(dllFile))
                            {
                                string dllDisableFile = $"{Application.dataPath}/../Library/ScriptAssemblies/{dll}.dll.DISABLE";
                                if (File.Exists(dllDisableFile))
                                {
                                    File.Delete(dllDisableFile);
                                }

                                File.Move(dllFile, dllDisableFile);
                            }

                            string pdbFile = $"{Application.dataPath}/../Library/ScriptAssemblies/{dll}.pdb";
                            if (File.Exists(pdbFile))
                            {
                                string pdbDisableFile = $"{Application.dataPath}/../Library/ScriptAssemblies/{dll}.pdb.DISABLE";
                                if (File.Exists(pdbDisableFile))
                                {
                                    File.Delete(pdbDisableFile);
                                }

                                File.Move(pdbFile, pdbDisableFile);
                            }
                        }
                    }
                    else
                    {
                        foreach (var dll in AssemblyTool.dllNames)
                        {
                            string dllFile = $"{Application.dataPath}/../Library/ScriptAssemblies/{dll}.dll";
                            string dllDisableFile = $"{Application.dataPath}/../Library/ScriptAssemblies/{dll}.dll.DISABLE";
                            if (File.Exists(dllFile))
                            {
                                if (File.Exists(dllDisableFile))
                                {
                                    File.Delete(dllDisableFile);
                                }
                            }
                            else
                            {
                                if (File.Exists(dllDisableFile))
                                {
                                    File.Move(dllDisableFile, dllFile);
                                }
                            }

                            string pdbDisableFile = $"{Application.dataPath}/../Library/ScriptAssemblies/{dll}.pdb.DISABLE";
                            string pdbFile = $"{Application.dataPath}/../Library/ScriptAssemblies/{dll}.pdb";
                            if (File.Exists(pdbFile))
                            {
                                if (File.Exists(pdbDisableFile))
                                {
                                    File.Delete(pdbDisableFile);
                                }
                            }
                            else
                            {
                                if (File.Exists(pdbDisableFile))
                                {
                                    File.Move(pdbDisableFile, pdbFile);
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
