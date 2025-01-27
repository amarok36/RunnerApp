using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace RunnerApp
{
    public class TextInformation
    {
        private Font font;
        public Text text;

        public TextInformation(byte[] textFont, string textString, Vector2f textPosition)
        {
            font = new Font(textFont);
            text = new Text(textString, font, 20);

            text.FillColor = new Color(243, 139, 15);
            text.Position = textPosition;
        }
    }

}
