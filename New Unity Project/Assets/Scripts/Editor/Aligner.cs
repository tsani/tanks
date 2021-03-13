using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Aligner : EditorWindow
{
  [MenuItem("Window/Aligner")]
  static void Init() {
    Aligner window = (Aligner)EditorWindow.GetWindow(typeof(Aligner));
    window.Show();
  }

  GameObject reference;
  // GameObject target;
  Coordinate alignment;

  float separation;

  void OnGUI() {
    alignment =
      (Coordinate)EditorGUILayout.EnumPopup(
        "Coordinate",
        alignment);

    reference =
      (GameObject)EditorGUILayout.ObjectField(
        "Reference object",
        reference,
        typeof(GameObject),
        true);

    // target =
    //   (GameObject)EditorGUILayout.ObjectField(
    //     "Target object",
    //     target,
    //     typeof(GameObject),
    //     true);

    separation =
      EditorGUILayout.FloatField(
        "Separation",
        separation);

    EditorGUILayout.BeginHorizontal();
    
    if(GUILayout.Button("Reference from selection")) {
      var ts = Selection.transforms;
      if(ts.Length == 1) {
        reference = ts[0].gameObject;
      }
      else
        Debug.LogError("You must select exactly one object in the scene.");
    }

    if(GUILayout.Button("Align selected")) {
      if(null == reference){
        Debug.LogError("A reference object is required.");
        return;
      }

      Align(reference.transform, Selection.transforms);
    }

    EditorGUILayout.EndHorizontal();
  }

  void Align(Transform reference, Transform[] targets) {
    var r = reference.position;
    Func<Vector3, Vector3> step = v =>
      v.Where(alignment, a => a + separation);

    r = step(r);
        
    foreach(var t in targets) {
      t.position = r;
      r = step(r);
    }
  }
}
