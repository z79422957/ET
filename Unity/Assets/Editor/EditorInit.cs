﻿using System;
using Base;
using UnityEditor;
using UnityEngine;
using Model;

namespace MyEditor
{
	[InitializeOnLoad]
	internal class EditorInit
	{
		static EditorInit()
		{
			DisposerManager.Instance.Register("Editor", typeof(EditorInit).Assembly);
			EditorApplication.update += Update;
		}

		private static void Update()
		{
			if (Application.isPlaying)
			{
				return;
			}

			try
			{
				DisposerManager.Instance.Update();
			}
			catch (Exception e)
			{
				DisposerManager.Reset();
				Log.Error(e.ToString());
			}
		}
	}
}
