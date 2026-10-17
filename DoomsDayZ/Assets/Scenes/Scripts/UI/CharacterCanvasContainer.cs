using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameManagersSystem;
using TMPro;
using UnityEngine.UI;
using IndieGameCharacterControllerSystem;

namespace IndieGameUISystem
{
    public class CharacterCanvasContainer : CanvasContainerBase
    {
        [HideInInspector] public Character _CharacterOwner;
        [HideInInspector] public TMP_Text _DamageTakenText; // assign data programmatically

        [Header("Manually Setup")]
        public Image _VisualDamageIMG; // attached image manually
        public Slider _CharacterHealthBar; // attached slider manually
        public float healthBarIMGTransitionSpeed = 15f;

        protected override void Awake()
        {
            base.Awake();
            _CharacterOwner = GetComponentInParent<Character>();
        }
    }
}

