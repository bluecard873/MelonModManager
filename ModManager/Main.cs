using System;
using System.Collections.Generic;
using System.IO;
using MelonLoader;
using SimpleJSON;
using UnityEngine;

namespace ModManager
{
    public class Main : MelonPlugin
    {
        
        //미래의 나를 위한 주석
        
        private bool _isGUIOpen = false,
            _isSettingOpen = false;
        
        private Vector2 _scrollPosition = Vector2.zero,
            _scrollPosition2 = Vector2.zero;
        
        private Dictionary<string, int> modToggle = new Dictionary<string, int>();
        private Dictionary<int, string> Status2Color = new Dictionary<int, string>
        {
            {(int)Status.Online,WindowColor.OnlineText},
            {(int)Status.Offline,WindowColor.OfflineText},
            {(int)Status.Warning,WindowColor.WarningText},
            {(int)Status.Error,WindowColor.ErrorText}
        };
        
        private GUIStyle _titleStyle,
            _areaStyle,
            _areaStyle2,
            _areaStyle3,
            _buttonStyle,
            _buttonStyle2,
            _buttonStyle3,
            _textStyle,
            _textStyle2;

        private Texture2D _texture,
            _texture2,
            _texture3,
            _texture4,
            _texture5,
            _texture6;
        
        private Type _choseType;
        private MelonMod _melonMod;
        private Rect _mWindowRect = new Rect(0, 0, 0, 0);
        private JSONNode _infoJson;
        

        public override void OnApplicationStart()
        {

            //적정 크기 설정
            _mWindowRect.width = (float) (Screen.width / 1.4);
            _mWindowRect.height = (float) (Screen.height / 1.4);
            _mWindowRect.x = Screen.width / 2 - _mWindowRect.width / 2;
            _mWindowRect.y = Screen.height / 2 - _mWindowRect.height / 2;

            FileInfo fileInfo = new FileInfo("UserData/ModManager.json");
            if (!fileInfo.Exists)
            {
                File.WriteAllText(fileInfo.FullName,"{}");
                _infoJson = JSON.Parse("{}");
                return;
            }
            
            string read = File.ReadAllText("UserData/ModManager.json");
            _infoJson = JSON.Parse(read);

            foreach (var k in _infoJson.Keys)
            {
                modToggle[k] = _infoJson[k];
            }
        }
        
        public override void OnApplicationLateStart() 
        {
            
            //OnToggle 가능한 모드들만 실행
            foreach (var mod in MelonHandler.Mods)
            {
                try
                {
                    if (!modToggle.ContainsKey(mod.Info.Name)) continue;
                    if (modToggle[mod.Info.Name] == (int) Status.Online)
                        foreach (var type in mod.Assembly.GetTypes())
                            if (type == mod.Info.SystemType)
                                if (type.GetMethod("OnToggle") == null) modToggle[mod.Info.Name] = (int) Status.Warning;
                                else type.GetMethod("OnToggle")?.Invoke(null, new object[] {true});
                }
                catch (Exception e)
                {
                    MelonLogger.Error(e);
                    modToggle[mod.Info.Name] = (int) Status.Error;
                }
            }
            
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.F10)&&(Input.GetKey(KeyCode.LeftControl)||Input.GetKey(KeyCode.RightControl)))
            {
                _isGUIOpen = !_isGUIOpen;
                _melonMod = null;
            }

            if (Input.GetKeyDown(KeyCode.Escape) && _isGUIOpen)
            {
                _isGUIOpen = false;
                _melonMod = null;
            }
        }

        public override void OnGUI()
        {
            if (!_isGUIOpen) return;
            
            //텍스쳐 설정
            if (_texture == null)
            {
                _texture = Function.makeRectangle(4, 160, 90, 1,
                    15, WindowColor.ModInfoLayout);
                _texture2 = Function.makeRectangle(4, 160 / 7, 90, 1,
                    15, WindowColor.ModListLayout);
                _texture3 =  Function.makeTex(1, 1, WindowColor.Translucent);
                _texture4 = Function.makeTex(1, 1, WindowColor.Invisible);
                _texture5 = Function.makeRectangle(4, 160, 90, 1,
                    10, WindowColor.ButtonColor);
                _texture6 = Function.makeRectangle(4, 160, 40, 1,
                    20, WindowColor.RoundLayout);
            }
            
            //스타일 설정
            if (_titleStyle == null)
            {
                _titleStyle = new GUIStyle(GUI.skin.box);
                _titleStyle.normal.textColor = Color.white;
                _titleStyle.fontSize = 40;
                _titleStyle.font = RDString.GetFontDataForLanguage(RDString.language).font;
                _titleStyle.normal.background = _texture3;

                    _areaStyle = new GUIStyle();
                _areaStyle.normal.background = this._texture;

                _areaStyle2 = new GUIStyle();
                _areaStyle2.normal.background = _texture2;

                _areaStyle3 = new GUIStyle();
                _areaStyle3.normal.textColor = Color.white;
                _areaStyle3.fontSize = 30;
                _areaStyle3.font = RDString.GetFontDataForLanguage(RDString.language).font;
                _areaStyle3.wordWrap = true;
                _areaStyle3.alignment = TextAnchor.UpperCenter;

                _buttonStyle = new GUIStyle();
                _buttonStyle.normal.textColor = Color.white;
                _buttonStyle.normal.background = _texture4;
                _buttonStyle.fontSize = 30;
                _buttonStyle.font = RDString.GetFontDataForLanguage(RDString.language).font;
                _buttonStyle.alignment = TextAnchor.UpperCenter;
                _buttonStyle.wordWrap = true;

                _buttonStyle2 = new GUIStyle(GUI.skin.button);
                _buttonStyle2.normal.textColor = Color.black;
                _buttonStyle2.normal.background = _texture5;
                _buttonStyle2.active.background = _texture5;
                _buttonStyle2.focused.background = _texture5;
                _buttonStyle2.hover.background = _texture5;
                _buttonStyle2.font = RDString.GetFontDataForLanguage(RDString.language).font;
                _buttonStyle2.alignment = TextAnchor.MiddleCenter;
                _buttonStyle2.margin.left = 20;
                _buttonStyle2.margin.right = 20;
                _buttonStyle2.padding.left = 10;
                _buttonStyle2.padding.right = 10;
                _buttonStyle2.fontSize = 30;

                _buttonStyle3 = new GUIStyle(GUI.skin.label);
                _buttonStyle3.alignment = TextAnchor.UpperCenter;
                _buttonStyle3.margin.top = -13;

                _textStyle = new GUIStyle();
                _textStyle.normal.textColor = Color.white;
                _textStyle.wordWrap = true;
                _textStyle.fontSize = 30;
                _textStyle.font = RDString.GetFontDataForLanguage(RDString.language).font;
                _textStyle.alignment = TextAnchor.UpperLeft;
                _textStyle.padding.left = 10;
                _textStyle.padding.right = 10;
                _textStyle.padding.bottom = 5;
                _textStyle.margin.top = -20;
                _textStyle.margin.bottom = 20;

                _textStyle2 = new GUIStyle(_textStyle);
                _textStyle2.normal.background = _texture6;
                _textStyle2.margin.top = 0;
            }

            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "MelonLoader Manager", _titleStyle);
            GUI.Box(_mWindowRect, "", _areaStyle); 

            float f = _mWindowRect.width / 7; 
            float t = _mWindowRect.width - f; 

            GUI.Box(new Rect(_mWindowRect.x, _mWindowRect.y, _mWindowRect.width,30),"",_areaStyle2);
            GUI.Box(new Rect(_mWindowRect.x, _mWindowRect.y, f, _mWindowRect.height), "", _areaStyle2);
            
            GUILayout.BeginArea(new Rect(_mWindowRect.x, _mWindowRect.y, f, _mWindowRect.height), "", _areaStyle2);
            _scrollPosition = GUILayout.BeginScrollView(
                _scrollPosition, GUILayout.Width(f), GUILayout.Height(_mWindowRect.height-60));
            GUILayout.Space(10);
            GUILayout.Label("Mod List", _buttonStyle);
            
            foreach (var mod in MelonHandler.Mods)
            {
                if (GUILayout.Button($"<color={(_melonMod == mod ? WindowColor.SelectText : WindowColor.NotSelectText)}>{mod.Info.Name}</color>",
                    _buttonStyle, GUILayout.Height(50)))
                {
                    _melonMod = mod;
                    foreach (var type in _melonMod.Assembly.GetTypes()) if (type == _melonMod.Info.SystemType) _choseType = type;
                    if (!modToggle.ContainsKey(_melonMod.Info.Name)) modToggle[_melonMod.Info.Name] = (int)Status.Online;
                }
            }
            
            GUILayout.EndScrollView();
            
            if (_melonMod != null&&(modToggle[_melonMod.Info.Name]==(int)Status.Offline||modToggle[_melonMod.Info.Name]==(int)Status.Online))
            {
                if (!modToggle.ContainsKey(_melonMod.Info.Name)) modToggle[_melonMod.Info.Name] = (int)Status.Online;
                if (GUILayout.Button($"{(modToggle[_melonMod.Info.Name]==(int)Status.Online? "Disable":"Enable")}", _buttonStyle2, GUILayout.Height(40)))
                {
                    modToggle[_melonMod.Info.Name] = modToggle[_melonMod.Info.Name] == (int) Status.Online
                        ? (int) Status.Offline
                        : (int) Status.Online;
                    _infoJson[_melonMod.Info.Name] = modToggle[_melonMod.Info.Name];
                    _choseType.GetMethod("OnToggle")?.Invoke(null, new object[] {modToggle[_melonMod.Info.Name]==(int)Status.Online});
                }
            }

            GUILayout.EndArea();

            if (_melonMod == null) return;
            string info = "";
            
            GUILayout.BeginArea(new Rect(_mWindowRect.x+f+40,_mWindowRect.y+20,t-80,_mWindowRect.height-40),"",_areaStyle3);
            _scrollPosition2 = GUILayout.BeginScrollView(
                _scrollPosition2, GUILayout.Width(t-80), GUILayout.Height(_mWindowRect.height-40));
            GUILayout.Label($"<size=40><color={Status2Color[modToggle[_melonMod.Info.Name]]}>●</color></size> <size=50>{_melonMod.Info.Name}</size>",_buttonStyle);
            
            info += $"Author - {_melonMod.Info.Author}\n";
            info += $"Version - v{_melonMod.Info.Version}\n";
            if(_melonMod.Info.DownloadLink!=null) info += $"Link - {_melonMod.Info.DownloadLink}\n";
            if(_choseType.GetField("Description")!=null) info+=$"Description - {_choseType.GetField("Description").GetValue(null)}\n";
            if(_choseType.GetMethod("OnToggle")==null) info += $"{_melonMod.Info.Name} is not compatible with the Manager";

            if (_choseType.GetMethod("OnSettingGUI") != null)
            {
                GUILayout.Label(info.Trim(), _textStyle2);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button($"{(_isSettingOpen ? "<size=40>◢</size>" : "<size=30>▶</size>")}", _buttonStyle3))
                    _isSettingOpen = !_isSettingOpen;

                GUILayout.Label("<size=40>Setting</size>", _textStyle);
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                if (_isSettingOpen)
                {
                    GUILayout.BeginVertical("", _textStyle2);
                    _choseType.GetMethod("OnSettingGUI")?.Invoke(null, null);
                    GUILayout.EndVertical();
                }
            }

            GUILayout.EndScrollView();
            GUILayout.EndArea();
            
        }

        public override void OnApplicationQuit()
        {
            File.WriteAllText("UserData/ModManager.json",_infoJson.ToString());
        }
    }
}
