using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class Window : MonoBehaviour
{


    [DllImport("user32.dll")]
    static extern int GetForegroundWindow();

    [DllImport("user32.dll")]
    static extern bool ShowWindowAsync(int hWnd, int nCmdShow);

    [DllImport("user32.dll", EntryPoint = "MoveWindow")]
    static extern int MoveWindow(int hwnd, int x, int y, int nWidth, int nHeight, int bRepaint);

    [DllImport("user32.dll", EntryPoint = "SetWindowLongA")]
    static extern int SetWindowLong(int hwnd, int nIndex, long dwNewLong);

    //
    [DllImport("user32.dll", EntryPoint = "GetWindowLongA")]
    static extern long GetWindowLong(int hwnd, int nIndex);

    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    static extern bool SetWindowPos(int hwnd, int gwndInsertAfter, int x, int y, int cx, int cy, uint uFlag);

    // Start is called before the first frame update
    void Start()
    {
#if !UNITY_EDITOR
        int handle = GetForegroundWindow();

        int fWidth = Screen.width;
        int fHeight = Screen.height;

        long lStyle = GetWindowLong(handle, -16);
        lStyle &= ~(0x00C00000L | 0x00040000L | 0x20000000L | 0x01000000L | 0x00080000L);
        SetWindowLong(handle, -16, lStyle);
        long lExStyle = GetWindowLong(handle, -20);
        lExStyle &= ~(0x00000001L | 0x00000200L | 0x00020000L);
        SetWindowLong(handle, -20, lExStyle);
        SetWindowPos(handle, 0, 0, 0, 0, 0, 0x0020 | 0x0002 | 0x0001 | 0x0004 | 0x0200);
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
