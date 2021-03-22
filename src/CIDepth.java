package src;
import java.io.*;
import java.awt.Color;
import java.lang.Math;
import java.text.DecimalFormat;

import javax.imageio.ImageIO;
import java.awt.image.BufferedImage;

public class CIDepth {

    //#region Color Palette Video
    public static int Terrace(float x, float max, float step) {
        return (int)Math.round((Math.floor((x / max) * (step + 1)) / step) * max);
    }
    public static int Clamp(int x, int min, int max) 
    { return Math.max(min, Math.min(max, x)); }

    public static Color Remap(Color input, int TPC) {
        int r = Clamp(Terrace(input.getRed(), 255, TPC), 0, 255);
        int g = Clamp(Terrace(input.getGreen(), 255, TPC), 0, 255);
        int b = Clamp(Terrace(input.getBlue(), 255, TPC), 0, 255);
        return new Color(r, g, b);
    }

    // Change the Image Color Palette (IsDepth = true: 2^TPC, false : TPC is total Color Variants)
    public static void ChangeImageCP(String InputFileName, String OutputFileName, int TPC, boolean IsDepth) throws IOException {
        if(IsDepth) { TPC = (int)Math.pow(2, TPC); }
        BufferedImage inImg = ImageIO.read(new File(InputFileName));
        BufferedImage outImg = new BufferedImage(inImg.getWidth(), inImg.getHeight(), BufferedImage.TYPE_INT_RGB);
        
        for(int y = 0; y < inImg.getHeight(); y++) {
            for(int x = 0 ; x < inImg.getWidth(); x++) {
                Color outPixel = Remap(new Color(inImg.getRGB(x, y)), TPC);
                outImg.setRGB(x, y, outPixel.getRGB());
            }
        }
        File outputFile = new File(OutputFileName);
        ImageIO.write(outImg, "png", outputFile);
        System.out.printf("Saved file to %s\n", OutputFileName);
    }
    //#endregion

    public static void main(String[] args) throws IOException {
        ChangeImageCP("data/Input.png", "data/Output.png", 4, false);
    }
}