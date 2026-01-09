using UnityEngine;
using UnityEngine.SceneManagement;

namespace DiamondDasherMod
{
    /// <summary>
    /// Entry point for SharpMonoInjector.
    /// Namespace: DiamondDasherMod
    /// Class: Loader
    /// Method: Load
    /// </summary>
    public class Loader
    {
        private static GameObject _modObject;

        /// <summary>
        /// Called by SharpMonoInjector to initialize the mod.
        /// </summary>
        public static void Load()
        {
            _modObject = new GameObject("DiamondDasherMod");
            _modObject.AddComponent<CheatMenu>();
            Object.DontDestroyOnLoad(_modObject);
        }

        /// <summary>
        /// Called by SharpMonoInjector to unload the mod.
        /// </summary>
        public static void Unload()
        {
            if (_modObject != null)
            {
                Object.Destroy(_modObject);
                _modObject = null;
            }
        }
    }

    /// <summary>
    /// Main cheat menu using Unity's built-in IMGUI system.
    /// </summary>
    public class CheatMenu : MonoBehaviour
    {
        private bool _showMenu = true;
        private Rect _windowRect = new Rect(20, 20, 300, 320);
        private float _cameraZoom = 5f;
        private int _currentLevel = 0;
        
        private readonly string[] _levelNames = new string[]
        {
            "Main Menu", "Level 1", "Level 2", "Level 3",
            "Level 4", "Level 5", "Level 6", "Level 7"
        };

        private GUIStyle _titleStyle;
        private GUIStyle _buttonStyle;
        private GUIStyle _activeButtonStyle;
        private bool _stylesInitialized = false;

        private void Update()
        {
            // Toggle menu with INSERT key
            if (Input.GetKeyDown(KeyCode.Insert))
            {
                _showMenu = !_showMenu;
            }
        }

        private void InitStyles()
        {
            if (_stylesInitialized) return;

            _titleStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 16,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            };
            _titleStyle.normal.textColor = new Color(0.4f, 0.8f, 1f);

            _buttonStyle = new GUIStyle(GUI.skin.button)
            {
                fontSize = 12
            };

            _activeButtonStyle = new GUIStyle(GUI.skin.button)
            {
                fontSize = 12
            };
            _activeButtonStyle.normal.textColor = Color.green;
            _activeButtonStyle.hover.textColor = Color.green;

            _stylesInitialized = true;
        }

        private void OnGUI()
        {
            if (!_showMenu) return;

            InitStyles();

            // Dark semi-transparent background
            GUI.backgroundColor = new Color(0.1f, 0.1f, 0.2f, 0.95f);
            _windowRect = GUI.Window(12345, _windowRect, DrawWindow, "");
        }

        private void DrawWindow(int windowId)
        {
            // Title
            GUILayout.Label("Diamond Dasher Cheats", _titleStyle);
            GUILayout.Label("Press INSERT to toggle", GUI.skin.label);
            GUILayout.Space(10);

            // === Level Skip Section ===
            GUI.color = new Color(1f, 0.8f, 0.3f);
            GUILayout.Label("═══ Level Skip ═══");
            GUI.color = Color.white;

            // Get current scene
            _currentLevel = SceneManager.GetActiveScene().buildIndex;
            GUILayout.Label($"Current: {_levelNames[Mathf.Clamp(_currentLevel, 0, 7)]} (Scene {_currentLevel})");
            GUILayout.Space(5);

            // Level buttons in grid
            GUILayout.BeginHorizontal();
            for (int i = 0; i < 4; i++)
            {
                var style = (i == _currentLevel) ? _activeButtonStyle : _buttonStyle;
                if (GUILayout.Button(_levelNames[i], style, GUILayout.Height(30)))
                {
                    SceneManager.LoadScene(i);
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            for (int i = 4; i < 8; i++)
            {
                var style = (i == _currentLevel) ? _activeButtonStyle : _buttonStyle;
                if (GUILayout.Button(_levelNames[i], style, GUILayout.Height(30)))
                {
                    SceneManager.LoadScene(i);
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(15);

            // === Camera Zoom Section ===
            GUI.color = new Color(1f, 0.8f, 0.3f);
            GUILayout.Label("═══ Camera Zoom ═══");
            GUI.color = Color.white;

            var cam = Camera.main;
            if (cam != null)
            {
                _cameraZoom = cam.orthographicSize;
                GUILayout.Label($"Orthographic Size: {_cameraZoom:F2}");

                float newZoom = GUILayout.HorizontalSlider(_cameraZoom, 1f, 20f);
                if (newZoom != _cameraZoom)
                {
                    cam.orthographicSize = newZoom;
                    _cameraZoom = newZoom;
                }

                GUILayout.Space(5);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Zoom In", GUILayout.Height(25)))
                {
                    cam.orthographicSize = Mathf.Max(1f, cam.orthographicSize - 0.5f);
                }
                if (GUILayout.Button("Reset (5)", GUILayout.Height(25)))
                {
                    cam.orthographicSize = 5f;
                }
                if (GUILayout.Button("Zoom Out", GUILayout.Height(25)))
                {
                    cam.orthographicSize = Mathf.Min(20f, cam.orthographicSize + 0.5f);
                }
                GUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.Label("(Camera not available)");
            }

            // Make window draggable
            GUI.DragWindow();
        }
    }
}
