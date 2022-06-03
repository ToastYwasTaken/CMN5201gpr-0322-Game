/*****************************************************************************
* Project: TANKPATROL
* File   : Parallax.cs
* Date   : 25.05.2022
* Author : Dennis Braunmueller (DB)
*
* Small effect for the MainMenu.
*
* History:
*	25.05.2022	    DB	    Created
*	03.06.2022      DB      Edited
******************************************************************************/
using UnityEngine;

namespace Dennis.UI
{
    public class ParallaxUI : MonoBehaviour
    {
        [SerializeField]
        private float _moveModifier;
        [SerializeField]
        private float _time;

        private Vector2 _startPosition;

        private void Start()
        {
            _startPosition = transform.position;
        }

        private void Update()
        {
            Vector2 position = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            float positionX = Mathf.Lerp(transform.position.x, _startPosition.x + (position.x * _moveModifier), _time * Time.deltaTime);
            float positionY = Mathf.Lerp(transform.position.y, _startPosition.y + (position.y * _moveModifier), _time * Time.deltaTime);

            transform.position = new Vector2(positionX, positionY);
        }
    }
}