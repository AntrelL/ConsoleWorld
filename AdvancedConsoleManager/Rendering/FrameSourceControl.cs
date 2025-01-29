namespace ColdWind.AdvancedConsoleManager;

public class FrameSourceControl
{
    public const int NumberOfRenderingLayers = 128;

    private readonly IFramesSource?[] FramesSources;

    public FrameSourceControl()
    {
        FramesSources = new IFramesSource[NumberOfRenderingLayers];
    }

    public void ConnectSource(IFramesSource framesSource, int renderingLayerNumber)
    {
        if (renderingLayerNumber < 0)
            throw new Exception("The rendering layer number cannot be less than zero.");

        if (renderingLayerNumber >= FramesSources.Length)
        {
            throw new Exception("The rendering layer number must be " +
                "less than the number of rendering layers.");
        }

        if (FramesSources[renderingLayerNumber] != null)
        {
            throw new Exception($"Rendering layer number " +
                $"{renderingLayerNumber} is already occupied");
        }

        FramesSources[renderingLayerNumber] = framesSource;
    }

    public void DisconnectSource(IFramesSource framesSource)
    {
        int index = Array.IndexOf(FramesSources, framesSource);
        FramesSources[index] = null;
    }

    public Frame AssembleComplexFrame(Vector2Int screenSize, Frame.Cell defaultFrameCell)
    {
        var baseFrame = new Frame(screenSize, defaultFrameCell);

        for (int i = FramesSources.Length - 1; i >= 0; i--)
        {
            if (FramesSources[i] == null)
                continue;

            Frame? frame = FramesSources[i]?.GetFrameForRender();

            if (frame == null)
                throw new Exception($"Frame source on layer {i} returned null");

            if (frame.Size != screenSize)
            {
                /*
                throw new Exception($"Frame source on layer {i} returned " +
                    $"an incorrectly sized frame");
                */
                baseFrame.Overlay(ScaleFrame(frame, screenSize, defaultFrameCell));
            }
            else
            {
                baseFrame.Overlay(frame);
            }
        }

        return baseFrame;
    }

    private Frame ScaleFrame(Frame frame, Vector2Int newSize, Frame.Cell defaultFrameCell)
    {
        var baseFrame = new Frame(newSize, defaultFrameCell);

        Vector2Int wrongSize = frame.Size;

        for (int i = 0; i < newSize.X; i++)
        {
            for (int j = 0; j < newSize.Y; j++)
            {
                if (i < wrongSize.X && j < wrongSize.Y)
                {
                    baseFrame.Cells[i, j] = frame.Cells[i, j];
                }
            }
        }

        return baseFrame;
    }
}
