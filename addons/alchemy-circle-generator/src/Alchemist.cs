using Godot;

public class Alchemist
{
    int size;
    int thickness = 4;

    Color color;
    Color backgroundColor;

    public int imageSize; 
    public const float D2R = (Mathf.Pi * 2f) / 360f;

    public Image textureImage;

    public Alchemist(Color color, Color backgroundColor, int thickness, int imageSize)
    {
        this.color = color;
        this.thickness = thickness;
        this.backgroundColor = backgroundColor;
        size = imageSize * thickness;
        textureImage = new Image();
    }

    public void GenerateImage()
    {
        textureImage.Create(size, size, false, Image.Format.Rgbaf);
        GD.Print("Got Here 1a");
        ResetImage(textureImage, size, size, backgroundColor);
        GD.Print("Got Here 2a");
        Generate(0);
    }

    void ResetImage(Image image, int width, int height, Color color)
    {
        image.Lock();
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                image.SetPixel(x, y, color);
            }
        }
    }

    void Generate(int id)
    {
        CiaccoRandom randomer = new CiaccoRandom();
        randomer.SetSeed(id);

        //int ncol = 60;// min_color 0
        //int xcol = 250;// max_color 255

        // draw the hexagon:
        // hexagon's center coordinates and radius
        float hex_x = size / 2;
        float hex_y = size / 2;
        float radius = ((size / 2f) * 3f / 4f);
        
        textureImage.Lock();
        TextureDraw.drawCircle(textureImage, size / 2, size / 2, (int)radius, color, thickness);

        int lati = randomer.GetRand(4, 8);

        TextureDraw.drawPolygon(textureImage, lati, (int)radius, 0, size, color, thickness);
        int l;
        float ang;
        for (l = 0; l < lati; l++)
        {
            ang = D2R * ((360 / (lati))) * l;
            TextureDraw.drawLine(textureImage, (size / 2), (size / 2), (int)((size / 2) + radius * Mathf.Cos(ang)), (int)((size / 2) + radius * Mathf.Sin(ang)), color, thickness);
        }
        int latis;
        if (lati % 2 == 0)
        {
            latis = randomer.GetRand(2, 6);

            while (latis % 2 != 0) latis = randomer.GetRand(3, 6);
            
            TextureDraw.drawFilledPolygon(textureImage, latis, (int)radius, 180, size, color,backgroundColor, thickness);

            for (l = 0; l < latis; l++)
            {
                ang = D2R * ((360 / latis)) * l;
                TextureDraw.drawLine(textureImage, (size / 2), (size / 2), (int)((size / 2) + radius * Mathf.Cos(ang)), (int)((size / 2) + radius * Mathf.Sin(ang)), color, thickness);
            }
        }
        else
        {
            latis = randomer.GetRand(2, 6);
            while (latis % 2 == 0) latis = randomer.GetRand(3, 6);

            TextureDraw.drawFilledPolygon(textureImage, latis,(int) radius, 180, size, color, backgroundColor, thickness);
        }

        if (randomer.GetRand(0, 1) % 2 == 0)
        {
            int ronad = randomer.GetRand(0, 4);

            if (ronad % 2 == 1)
            {
                for (l = 0; l < lati + 4; l++)
                {
                    ang = D2R * ((360 / (lati + 4))) * l;
                    TextureDraw.drawLine(textureImage, (size / 2), (size / 2), (int)((size / 2) + (((radius / 8) * 5) + 2) * Mathf.Cos(ang)), (int)((size / 2) + (((radius / 8) * 5) + 2) * Mathf.Sin(ang)), color, thickness);
                }

                TextureDraw.drawFilledPolygon(textureImage, lati + 4,(int)( radius / 2), 0, size, color, backgroundColor, thickness);
            }
            else if (ronad % 2 == 0)
            {
                for (l = 0; l < lati - 2; l++)
                {
                    ang = D2R * ((360 / (lati - 2))) * l;
                    TextureDraw.drawLine(textureImage, (size / 2), (size / 2), (int)((size / 2) + (((radius / 8) * 5) + 2) * Mathf.Cos(ang)), (int)((size / 2) + (((radius / 8) * 5) + 2) * Mathf.Sin(ang)), color, thickness);
                }

                TextureDraw.drawFilledPolygon(textureImage, lati - 2, (int)(radius / 4), 0, size, color,backgroundColor, thickness);
            }
        }

        if (randomer.GetRand(0, 4) % 2 == 0)
        {
            TextureDraw.drawCircle(textureImage, size / 2, size / 2, (int)((radius / 16f) * 11f), color, thickness);

            if (lati % 2 == 0)
            {
                latis = randomer.GetRand(2, 8);

                while (latis % 2 != 0) latis = randomer.GetRand(3, 8);

                TextureDraw.drawPolygon(textureImage, latis, (int)((radius / 3) * 2), 180, size, color, thickness);
            }
            else
            {
                latis = randomer.GetRand(2, 8);

                while (latis % 2 == 0) latis = randomer.GetRand(3, 8);

                TextureDraw.drawPolygon(textureImage, latis, (int)((radius / 3) * 2), 180, size, color, thickness);
            }
        }

        int caso = randomer.GetRand(0, 3);
        float angdiff, posax, posay;
        if (caso == 0)
        {
            for (int i = 0; i < latis; i++)
            {
                angdiff = (D2R * (360 / (latis)));
                posax = (((radius / 18) * 11) * Mathf.Cos(i * angdiff));
                posay = (((radius / 18) * 11) * Mathf.Sin(i * angdiff));
                TextureDraw.drawFilledCircle(textureImage, (int)(size / 2 + posax), (int)(size / 2 + posay), (int)((radius / 44) * 6), color, backgroundColor, thickness);
            }
        }
        else if (caso == 1)
        {
            for (int i = 0; i < latis; i++)
            {
                angdiff = (D2R * (360 / latis));
                posax = (radius * Mathf.Cos(i * angdiff));
                posay = (radius * Mathf.Sin(i * angdiff));
                TextureDraw.drawFilledCircle(textureImage, (int)(size / 2 + posax), (int)(size / 2 + posay), (int)((radius / 44) * 6), color, backgroundColor, thickness);
            }
        }
        else if (caso == 2)
        {
            TextureDraw.drawCircle(textureImage, size / 2, size / 2, (int)((radius / 18) * 6), color, thickness);
            TextureDraw.drawFilledCircle(textureImage, size / 2, size / 2, (int)((radius / 22) * 6), color, backgroundColor, thickness);
        }
        else if (caso == 3)
        {
            for (int i = 0; i < latis; i++)
            {
                ang = D2R * ((360 / (latis))) * i;
                TextureDraw.drawLine(textureImage, (int)((size / 2) + ((radius / 3) * 2) * Mathf.Cos(ang)), (int)((size / 2) + ((radius / 3) * 2) * Mathf.Sin(ang)), (int)((size / 2) + radius * Mathf.Cos(ang)), (int)((size / 2) + radius * Mathf.Sin(ang)), color, thickness);
            }
            if (latis == lati)
            {
            }
            else
            {
                TextureDraw.drawFilledCircle(textureImage, size / 2, size / 2, (int)((radius / 3) * 2), color, backgroundColor, thickness);
                lati = randomer.GetRand(3, 6);
                TextureDraw.drawPolygon(textureImage, lati, (int)((radius / 4) * 5), 0, size, color,thickness);
                TextureDraw.drawPolygon(textureImage, lati, (int)((radius / 3) * 2), 180, size, color,thickness);
            }
        }
        textureImage.Unlock();
    }
}
