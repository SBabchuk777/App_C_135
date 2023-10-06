﻿using UnityEngine;
using UnityEngine.UI;

namespace MatchThreeEngine
{
	public sealed class Tile : MonoBehaviour
	{
		public int x;
		public int y;

		public Image icon;

		public Button button;

		private TileTypeAsset _type;

		public TileTypeAsset Type
		{
			get => _type;

			set
			{
				if (_type == value) return;

				_type = value;

				icon.sprite = _type.sprite;
				icon.SetNativeSize();
				RectTransform iconRect = icon.GetComponent<RectTransform>();
				iconRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,iconRect.rect.width/7f);
				iconRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,iconRect.rect.height/7f);
			}
		}

		public TileData Data => new TileData(x, y, _type.id);
	}
}
