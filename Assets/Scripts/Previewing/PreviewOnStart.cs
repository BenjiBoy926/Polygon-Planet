using UnityEngine;
using System.Collections;

// A simple type of previewer that previews at the start of the scene
public class PreviewOnStart : Previewer
{
    protected override void Start()
    {
        base.Start();
        StartPreview();
    }
}
