using RunnerApp.Properties;
using SFML.Graphics;
using SFML.System;

namespace RunnerApp
{
    public class TextInformation
    {
        private Font font;
        public Text text;

        public TextInformation(string textString, Vector2f textPosition)
        {
            font = new Font(Resources.Dynastium);
            text = new Text(textString, font, 20);

            text.FillColor = new Color(243, 139, 15);
            text.Position = textPosition;
        }
    }
}
