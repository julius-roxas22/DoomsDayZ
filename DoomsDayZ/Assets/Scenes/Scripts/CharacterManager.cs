using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;

namespace IndieGameManagersSystem
{
    public class CharacterManager : Singleton<CharacterManager>
    {
        public List<Character> _Characters = new List<Character>();

        public void _AddCharacterList(Character character)
        {
            string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            if (!_Characters.Contains(character))
            {
                if (sceneName == "MainMenu")
                {
                    if (character._AllyType == _EAllyType.Ally) //add only the player
                    {
                        _Characters.Add(character);
                    }
                }
                else
                {
                    _Characters.Add(character); // add all does have character script
                }
            }
        }

        public Character _GetCharacterPlayable()
        {
            foreach (Character c in _Characters)
            {
                if (c._SubController.enabled)
                {
                    return c;
                }
            }
            return null;
        }

        public Animator _GetCharacaterByAnimator(Animator anim)
        {
            foreach (Character c in _Characters)
            {
                if (c._Animator == anim)
                {
                    return c._Animator;
                }
            }
            return null;
        }

        public Character _GetCharacterByIndex(int indexOf)
        {
            if (_Characters.Count > 0 || null != _Characters)
            {
                return _Characters[indexOf];
            }
            return null;
        }

        public int _GetIndexByCharacter(Character c)
        {
            for (int i = 0; i < _Characters.Count; i++)
            {
                if (_Characters[i] == c)
                {
                    return i;
                }
            }
            return 0;
        }

        public Character _GetRandomCharacter => _Characters[Random.Range(0, _Characters.Count)];

    }

}