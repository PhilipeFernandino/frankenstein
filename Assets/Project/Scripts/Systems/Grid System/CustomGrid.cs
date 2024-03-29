﻿using System.Collections.Generic;
using UnityEngine;

namespace Systems.Grid_System
{
    public class CustomGrid : MonoBehaviour
    {
        [Tooltip("Size of the grid from -thisValue to thisValue")]
        [SerializeField] private int _gridAxisRange;
        [SerializeField] private int _spritesPoolSize;

        [SerializeField] private SpriteRenderer _gridSpriteRendererPrefab;

        private List<SpriteRenderer> _gridSprites;

        private ITileData[,] _grid;
        private int _offset;
        private int _gridSize;

        private int _lastGridDrawSize = 0;

        private void Update()
        {
            var mousePos = Input.mousePosition;
            mousePos.z = 10f;
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;

            DrawInGrid(pos, new Vector2Int(2, 2));
        }

        public void DrawInGrid(Vector2 position, in Vector2Int size)
        {
            var rounded = new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    int x = rounded.x + i;
                    int y = rounded.y + j;

                    int index = j + i * size.y;

                    _gridSprites[index].transform.position = new Vector2(x, y);

                    if (TryGetTileAt(x, y, out ITileData tile))
                    {
                        _gridSprites[index].color = Color.white;
                    }
                    else
                    {
                        _gridSprites[index].color = Color.red;
                    }

                    _gridSprites[index].enabled = true;
                }
            }

            int gridDrawSize = size.x * size.y;
            for (int i = gridDrawSize; i < _lastGridDrawSize; i++)
            {
                _gridSprites[i].enabled = false;
            }
        }

        public bool TryGetTileAt(int x, int y, out ITileData tile)
        {
            int xGrid = x + _offset;
            int yGrid = y + _offset;

            if ((xGrid < 0 || xGrid >= _gridSize) || (yGrid < 0 || yGrid >= _gridSize))
            {
                tile = null;
                return false;
            }

            tile = _grid[xGrid, yGrid];
            return true;
        }

        private void Awake()
        {
            InitializeGrid();
            InitializeDrawGridSprites();
        }

        private void InitializeGrid()
        {
            if (_gridAxisRange % 2 != 0 || _gridAxisRange == 0)
            {
                Debug.LogError($"{GetType()} - Grid axis size must be pair and above 0");
            }

            _gridSize = _gridAxisRange * 2;
            _grid = new ITileData[_gridSize, _gridSize];
            _offset = _gridAxisRange;
        }

        private void InitializeDrawGridSprites()
        {
            _gridSprites = new(_spritesPoolSize);

            for (int i = 0; i < _spritesPoolSize; i++)
            {
                _gridSprites.Add(Instantiate(_gridSpriteRendererPrefab, transform));
                _gridSprites[i].enabled = false;
            }
        }
    }
}