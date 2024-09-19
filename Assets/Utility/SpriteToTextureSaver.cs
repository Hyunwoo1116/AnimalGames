using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Utility
{
    public class SpriteToTextureSaver : MonoBehaviour
    {
        public List<Sprite> spriteToSave; // 저장할 Sprite 이미지
        public string fileName;
        public string targetDirectory;

        public void Start()
        {
            Debug.Log("Start");
            foreach (var sprite in spriteToSave)
            {
                SaveSpriteAsImage(sprite);
            }
            Debug.Log("End");
        }
        // 이 함수는 Sprite를 PNG 또는 JPG로 저장합니다.
        public void SaveSpriteAsImage(Sprite sprite ,bool saveAsPng = true)
        {
            Texture2D texture = SpriteToTexture2D(sprite);

            // Texture2D를 PNG 또는 JPG 포맷으로 변환
            byte[] imageData;
            imageData = texture.EncodeToPNG(); // PNG로 저장
            fileName = sprite.name + ".png";

            // 파일 경로 설정
            string filePath = Path.Combine(targetDirectory, fileName);

            // 파일 저장
            File.WriteAllBytes(filePath, imageData);
            Debug.Log(filePath);
        }

        // Sprite에서 Texture2D로 변환하는 함수
        private Texture2D SpriteToTexture2D(Sprite sprite)
        {
            // Sprite의 크기 정보
            Rect spriteRect = sprite.rect;
            Texture2D texture = new Texture2D((int)spriteRect.width, (int)spriteRect.height);

            // 원본 Texture2D에서 Sprite 영역의 픽셀을 가져오기
            Color[] pixels = sprite.texture.GetPixels(
                (int)spriteRect.x,
                (int)spriteRect.y,
                (int)spriteRect.width,
                (int)spriteRect.height
            );

            // 새로운 Texture2D에 픽셀 적용
            texture.SetPixels(pixels);
            texture.Apply();

            return texture;
        }
    }
}