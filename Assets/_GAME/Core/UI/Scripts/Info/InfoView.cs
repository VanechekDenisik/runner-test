using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Info
{
    public class InfoView : MonoBehaviour
    {
        private const float InfoViewOffset = 10f;
        [SerializeField] private InfoViewParameter parameter;
        [SerializeField] private RectTransform localSpaceRect;
        [SerializeField] private RectTransform toMove;
        [SerializeField] private TextMeshProUGUI headerText;
        [SerializeField] private SectionsView sectionTemplate;
        [SerializeField] private RectTransform rectTransformToRebuild;

        private readonly List<SectionsView> _sections = new();

        private void Awake()
        {
            parameter.Value = this;
            _sections.Add(sectionTemplate);
            Hide();
        }

        private void Update()
        {
            if (!gameObject.activeSelf) return;
            UpdateViewPosition();
        }

        private void OnEnable()
        {
            UpdateViewPosition();
        }

        public void Show(string header, string[] descriptions)
        {
            gameObject.SetActive(true);
            Draw(header, descriptions);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void UpdateViewPosition()
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(localSpaceRect, Input.mousePosition, null,
                out var offset);

            foreach (var pivot in new Vector2[] { new(1f, 0f), new(1f, 1f), new(0f, 1f), new(0f, 0f) })
            {
                toMove.pivot = pivot;
                if (WillRectTransformFitScreen(toMove, pivot, offset + PivotToPositionOffset(pivot)))
                    return;
            }

            toMove.pivot = new Vector2(1f, 0f);
        }

        private Vector3 PivotToPositionOffset(Vector2 pivot)
        {
            Vector2 normalizedOffset;
            if (pivot == new Vector2(1f, 0f))
                normalizedOffset = new Vector2(-1f, 1f);
            else if (pivot == new Vector2(1f, 1f))
                normalizedOffset = new Vector2(-1f, -1f);
            else if (pivot == new Vector2(0f, 1f))
                normalizedOffset = new Vector2(1f, -1f);
            else
                normalizedOffset = new Vector2(1f, 1f);
            return InfoViewOffset * normalizedOffset;
        }

        private bool WillRectTransformFitScreen(RectTransform rectTransform, Vector2 pivot, Vector3 position)
        {
            rectTransform.pivot = pivot;
            rectTransform.position = position;

            // Check to ensure the RectTransform remains within the Canvas
            var corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);

            // Convert all corner positions to Overlay Canvas Space
            for (var i = 0; i < corners.Length; i++)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(localSpaceRect, corners[i], null,
                    out var localPoint);
                corners[i] = localPoint;
            }

            // Check if rectTransform would be out-of-bounds
            for (var i = 0; i < corners.Length; i++)
                if (!localSpaceRect.rect.Contains(corners[i]))
                    return false;

            // All corners are within screen
            return true;
        }

        private void Draw(string name, string[] descriptions)
        {
            headerText.text = name;
            SpawnNewSections(descriptions.Length - _sections.Count);
            DrawSections(descriptions);
            SetActiveSections(descriptions.Length);

            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransformToRebuild);
        }

        private void SpawnNewSections(int sectionsToSpawnCount)
        {
            for (var sectionId = 0; sectionId < sectionsToSpawnCount; sectionId++)
            {
                var newSection = Instantiate(sectionTemplate, sectionTemplate.transform.parent);
                _sections.Add(newSection);
            }
        }

        private void DrawSections(string[] descriptions)
        {
            for (var sectionId = 0; sectionId < descriptions.Length; sectionId++)
                _sections[sectionId].Draw(descriptions[sectionId]);
        }

        private void SetActiveSections(int activeSectionsCount)
        {
            for (var sectionId = 0; sectionId < _sections.Count; sectionId++)
                _sections[sectionId].gameObject.SetActive(sectionId < activeSectionsCount);
        }
    }
}