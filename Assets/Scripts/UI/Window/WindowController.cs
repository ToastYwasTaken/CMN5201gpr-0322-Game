using System.Collections.Generic;
using UnityEngine;
using Dennis.UI.Extensions;

namespace Dennis.UI
{
    public class WindowController : UISingleton<WindowController>
    {
        [SerializeField]
        private Window _mainWindow;
        [SerializeField]
        private GameObject _backButton;

        private List<Window> _windowHistory = new List<Window>();
        private bool _hasNewWindowInFrame;

        public Window CurrentWindow { get; private set; }
        public bool HasWindowHistory { get { return _windowHistory.Count > 0; } }

        protected override void AwakeSingleton()
        {
            var window = GameObject.FindObjectsOfType<Window>();

            foreach (var uiWindow in window)
            {
                uiWindow.SetActive(false);
            }
        }

        private void Start()
        {
            if(_mainWindow != null)
            {
                _mainWindow.Open();
                CurrentWindow = _mainWindow;
            }
        }

        private void Update()
        {
            if(_hasNewWindowInFrame)
            {
                _hasNewWindowInFrame = false;
            }
            else if(Input.GetKeyDown(KeyCode.Escape) && CurrentWindow != null && CurrentWindow != _mainWindow)
            {
                OnBack();
            }

            _backButton.SetActive(HasWindowHistory);
        }

        public void OpenWindow(Window window)
        {
            if(CurrentWindow == window)
            {
                return;
            }

            CloseCurrent();

            _windowHistory.Add(window);
            CurrentWindow = window;
            CurrentWindow.Open();
            _hasNewWindowInFrame = true;
        }

        public void OnBack()
        {
            if(_windowHistory.Count > 0)
            {
                _windowHistory.RemoveAt(_windowHistory.Count - 1);
            }

            CloseCurrent();

            if(_windowHistory.Count > 0)
            {
                CurrentWindow = _windowHistory[_windowHistory.Count - 1];
            }
            else
            {
                CurrentWindow = _mainWindow;
            }

            if(CurrentWindow != null)
            {
                CurrentWindow.Open();
            }
        }

        private void CloseCurrent()
        {
            if(CurrentWindow != null)
            {
                CurrentWindow.Close();
                CurrentWindow = null;
            }
        }
    }
}